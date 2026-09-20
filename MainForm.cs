
//Multiple face detection and recognition in real time
//Using EmguCV cross platform .Net wrapper to the Intel OpenCV image processing library for C#.Net
//Writed by Sergio Andrés Guitérrez Rojas
//"Serg3ant" for the delveloper comunity
// Sergiogut1805@hotmail.com
//Regards from Bucaramanga-Colombia ;)

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.IO;
using System.Diagnostics;

namespace MultiFaceRec
{
    public partial class FrmPrincipal : Form
    {
        //Declararation of all variables, vectors and haarcascades
        Image<Bgr, Byte> currentFrame;
        Capture grabber;
        HaarCascade face;
        HaarCascade eye;
        MCvFont font = new MCvFont(FONT.CV_FONT_HERSHEY_TRIPLEX, 0.5d, 0.5d);
        Image<Gray, byte> result, TrainedFace = null;
        Image<Gray, byte> gray = null;
        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();
        List<string> labels= new List<string>();
        List<string> NamePersons = new List<string>();
        int ContTrain, NumLabels, t;
        string name, names = null;

        private RecognitionListForm activeRecognitionListForm = null;

        // Recognition Sensitivity & Accuracy
        private double currentThreshold = 2500;
        private EigenObjectRecognizer recognizer = null;
        private bool recognizerNeedsRebuild = true;

        // Multi-Shot / Burst Training fields
        private bool isBurstModeActive = false;
        private int burstTargetCount = 8;
        private int burstCapturedCount = 0;
        private string burstTargetName = "";
        private DateTime lastBurstCaptureTime = DateTime.MinValue;
        private List<Image<Gray, byte>> burstImagesBuffer = new List<Image<Gray, byte>>();


        public FrmPrincipal()
        {
            InitializeComponent();
            //Load haarcascades for face detection
            string cascadePath = Path.Combine(Application.StartupPath, "haarcascade_frontalface_default.xml");
            face = new HaarCascade(cascadePath);
            //eye = new HaarCascade("haarcascade_eye.xml");
            try
            {
                string labelPath = Path.Combine(Application.StartupPath, "TrainedFaces", "TrainedLabels.txt");
                if (File.Exists(labelPath))
                {
                    string Labelsinfo = File.ReadAllText(labelPath);
                    string[] Labels = Labelsinfo.Split('%');
                    if (Labels.Length > 0 && !string.IsNullOrEmpty(Labels[0]))
                    {
                        NumLabels = Convert.ToInt16(Labels[0]);
                        ContTrain = NumLabels;
                        for (int tf = 1; tf < NumLabels + 1; tf++)
                        {
                            string loadFaces = "face" + tf + ".bmp";
                            string facePath = Path.Combine(Application.StartupPath, "TrainedFaces", loadFaces);
                            if (File.Exists(facePath))
                            {
                                trainingImages.Add(new Image<Gray, byte>(facePath));
                                labels.Add(Labels[tf]);
                            }
                        }
                    }
                }
            }
            catch
            {
                NumLabels = 0;
                ContTrain = 0;
            }

        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            CenterContent();
        }

        private void FrmPrincipal_Resize(object sender, EventArgs e)
        {
            CenterContent();
        }

        private void CenterContent()
        {
            if (panelContent != null)
            {
                panelContent.Left = Math.Max(0, (this.ClientSize.Width - panelContent.Width) / 2);
                panelContent.Top = Math.Max(0, (this.ClientSize.Height - panelContent.Height) / 2);
            }
        }

        public class RegisteredPerson
        {
            public string Name { get; set; }
            public int SampleCount { get; set; }
        }

        public List<string> GetLabels()
        {
            return new List<string>(labels);
        }

        public List<RegisteredPerson> GetRegisteredPersons()
        {
            List<RegisteredPerson> result = new List<RegisteredPerson>();
            Dictionary<string, RegisteredPerson> map = new Dictionary<string, RegisteredPerson>(StringComparer.OrdinalIgnoreCase);

            for (int i = 0; i < labels.Count; i++)
            {
                string raw = labels[i];
                if (string.IsNullOrEmpty(raw)) continue;
                string cleanName = raw.Trim();
                if (string.IsNullOrEmpty(cleanName)) continue;

                RegisteredPerson person;
                if (map.TryGetValue(cleanName, out person))
                {
                    person.SampleCount++;
                }
                else
                {
                    person = new RegisteredPerson { Name = cleanName, SampleCount = 1 };
                    map[cleanName] = person;
                    result.Add(person);
                }
            }

            return result;
        }

        public int GetTotalSampleCount()
        {
            return labels.Count;
        }

        public bool UpdatePersonName(string oldName, string newName)
        {
            if (string.IsNullOrEmpty(oldName) || string.IsNullOrEmpty(newName)) return false;
            string cleanOld = oldName.Trim();
            string cleanNew = newName.Trim();
            if (string.IsNullOrEmpty(cleanNew)) return false;

            bool found = false;
            for (int i = 0; i < labels.Count; i++)
            {
                if (string.Equals(labels[i].Trim(), cleanOld, StringComparison.OrdinalIgnoreCase))
                {
                    labels[i] = cleanNew;
                    found = true;
                }
            }

            if (found)
            {
                try
                {
                    SaveAllTrainedFaces();
                    recognizerNeedsRebuild = true;
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving updated name: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return false;
        }

        public bool DeletePerson(string personName)
        {
            if (string.IsNullOrEmpty(personName)) return false;
            string cleanTarget = personName.Trim();

            bool found = false;
            for (int i = labels.Count - 1; i >= 0; i--)
            {
                if (string.Equals(labels[i].Trim(), cleanTarget, StringComparison.OrdinalIgnoreCase))
                {
                    if (i < trainingImages.Count)
                    {
                        trainingImages.RemoveAt(i);
                    }
                    labels.RemoveAt(i);
                    found = true;
                }
            }

            if (found)
            {
                try
                {
                    SaveAllTrainedFaces();
                    recognizerNeedsRebuild = true;
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting person: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return false;
        }

        public bool UpdateFaceName(int index, string newName)
        {
            if (index < 0 || index >= labels.Count) return false;
            if (string.IsNullOrEmpty(newName) || string.IsNullOrEmpty(newName.Trim())) return false;

            labels[index] = newName.Trim();
            try
            {
                string labelPath = Path.Combine(Application.StartupPath, "TrainedFaces", "TrainedLabels.txt");
                File.WriteAllText(labelPath, trainingImages.Count.ToString() + "%");
                for (int i = 0; i < labels.Count; i++)
                {
                    File.AppendAllText(labelPath, labels[i] + "%");
                }
                recognizerNeedsRebuild = true;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating label file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool DeleteFace(int index)
        {
            if (index < 0 || index >= trainingImages.Count || index >= labels.Count) return false;

            try
            {
                trainingImages.RemoveAt(index);
                labels.RemoveAt(index);

                SaveAllTrainedFaces();
                recognizerNeedsRebuild = true;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting face: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void buttonRecognitionList_Click(object sender, EventArgs e)
        {
            if (activeRecognitionListForm == null || activeRecognitionListForm.IsDisposed)
            {
                activeRecognitionListForm = new RecognitionListForm(this);
                activeRecognitionListForm.Show(this);
            }
            else
            {
                activeRecognitionListForm.BringToFront();
                activeRecognitionListForm.LoadData();
            }
        }

        private void trackBarThreshold_Scroll(object sender, EventArgs e)
        {
            currentThreshold = trackBarThreshold.Value * 100;
            string sensitivityName = "Balanced";
            if (currentThreshold <= 1800)
                sensitivityName = "Strict (High Accuracy)";
            else if (currentThreshold >= 3500)
                sensitivityName = "Loose (Easy Match)";

            lblThresholdVal.Text = string.Format("Sensitivity: {0} (Threshold: {1})", sensitivityName, currentThreshold);

            if (recognizer != null)
            {
                recognizer.EigenDistanceThreshold = currentThreshold;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Initialize the capture device
            grabber = new Capture();
            grabber.QueryFrame();
            //Initialize the FrameGraber event
            Application.Idle += new EventHandler(FrameGrabber);
            button1.Enabled = false;
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            if (grabber == null)
            {
                MessageBox.Show("Please click '1. Detect and recognize' to turn on the camera first.", "Camera Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string inputName = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(inputName))
            {
                MessageBox.Show("Please enter a name for the person first.", "Name Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return;
            }

            if (chkBurstMode.Checked)
            {
                // Start Multi-Shot / Burst Training
                burstTargetCount = (int)numBurstShots.Value;
                burstCapturedCount = 0;
                burstTargetName = inputName;
                burstImagesBuffer.Clear();
                lastBurstCaptureTime = DateTime.MinValue;
                isBurstModeActive = true;

                button2.Enabled = false;
                progressBarBurst.Visible = true;
                progressBarBurst.Maximum = burstTargetCount;
                progressBarBurst.Value = 0;
                lblBurstStatus.Text = string.Format("0/{0} - Move head slightly...", burstTargetCount);
            }
            else
            {
                // Single-shot training
                CaptureSingleFace(inputName);
            }
        }

        private void CaptureSingleFace(string personName)
        {
            try
            {
                if (currentFrame == null)
                {
                    MessageBox.Show("No camera frame available yet. Look at the camera.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                gray = currentFrame.Convert<Gray, Byte>();
                MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(
                    face,
                    1.2,
                    10,
                    Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
                    new Size(20, 20));

                if (facesDetected[0].Length == 0)
                {
                    MessageBox.Show("No face detected in the frame.\nPlease position your face clearly in front of the camera.", "No Face Detected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MCvAvgComp f = facesDetected[0][0];
                TrainedFace = currentFrame.Copy(f.rect).Convert<Gray, byte>().Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                trainingImages.Add(TrainedFace);
                labels.Add(personName);
                imageBox1.Image = TrainedFace;

                SaveAllTrainedFaces();
                recognizerNeedsRebuild = true;

                MessageBox.Show(personName + "´s face detected and added :)", "Training OK", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (activeRecognitionListForm != null && !activeRecognitionListForm.IsDisposed)
                {
                    activeRecognitionListForm.LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error capturing face: " + ex.Message, "Training Fail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void FinishBurstTraining()
        {
            isBurstModeActive = false;
            button2.Enabled = true;
            progressBarBurst.Visible = false;

            for (int i = 0; i < burstImagesBuffer.Count; i++)
            {
                trainingImages.Add(burstImagesBuffer[i]);
                labels.Add(burstTargetName);
            }

            SaveAllTrainedFaces();
            recognizerNeedsRebuild = true;

            lblBurstStatus.Text = string.Format("Saved {0} shots!", burstCapturedCount);

            if (activeRecognitionListForm != null && !activeRecognitionListForm.IsDisposed)
            {
                activeRecognitionListForm.LoadData();
            }

            System.Media.SystemSounds.Asterisk.Play();
            MessageBox.Show(string.Format("Burst training complete!\nSuccessfully captured {0} multi-angle shots for \"{1}\".\nRecognition accuracy has been upgraded!", burstCapturedCount, burstTargetName),
                "Burst Training Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SaveAllTrainedFaces()
        {
            string trainedPath = Path.Combine(Application.StartupPath, "TrainedFaces");
            if (Directory.Exists(trainedPath))
            {
                string[] oldFiles = Directory.GetFiles(trainedPath, "face*.bmp");
                foreach (string f in oldFiles)
                {
                    try { File.Delete(f); } catch { }
                }
            }
            else
            {
                Directory.CreateDirectory(trainedPath);
            }

            for (int i = 0; i < trainingImages.Count; i++)
            {
                trainingImages[i].Save(Path.Combine(trainedPath, "face" + (i + 1) + ".bmp"));
            }

            string labelPath = Path.Combine(trainedPath, "TrainedLabels.txt");
            File.WriteAllText(labelPath, trainingImages.Count.ToString() + "%");
            for (int i = 0; i < labels.Count; i++)
            {
                File.AppendAllText(labelPath, labels[i] + "%");
            }

            ContTrain = trainingImages.Count;
            NumLabels = trainingImages.Count;
        }

        void FrameGrabber(object sender, EventArgs e)
        {
            label3.Text = "0";
            NamePersons.Clear();

            //Get the current frame from capture device
            currentFrame = grabber.QueryFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);

            //Convert it to Grayscale
            gray = currentFrame.Convert<Gray, Byte>();

            //Face Detector
            MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(
                face,
                1.2,
                10,
                Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
                new Size(20, 20));

            //Action for each face detected
            foreach (MCvAvgComp f in facesDetected[0])
            {
                t = t + 1;
                result = currentFrame.Copy(f.rect).Convert<Gray, byte>().Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);

                if (isBurstModeActive)
                {
                    // Draw cyan burst capture box
                    currentFrame.Draw(f.rect, new Bgr(Color.Cyan), 3);
                    string burstText = string.Format("Burst: {0}/{1}", burstCapturedCount + 1, burstTargetCount);
                    currentFrame.Draw(burstText, ref font, new Point(f.rect.X, f.rect.Y - 6), new Bgr(Color.Yellow));

                    if ((DateTime.Now - lastBurstCaptureTime).TotalMilliseconds >= 200)
                    {
                        lastBurstCaptureTime = DateTime.Now;
                        burstImagesBuffer.Add(result);
                        burstCapturedCount++;
                        imageBox1.Image = result;
                        progressBarBurst.Value = Math.Min(burstTargetCount, burstCapturedCount);
                        lblBurstStatus.Text = string.Format("{0}/{1} captured...", burstCapturedCount, burstTargetCount);

                        if (burstCapturedCount >= burstTargetCount)
                        {
                            FinishBurstTraining();
                            break;
                        }
                    }
                    NamePersons.Add("Capturing Burst...");
                    continue;
                }

                // Normal recognition mode
                if (trainingImages.Count != 0)
                {
                    if (recognizer == null || recognizerNeedsRebuild)
                    {
                        MCvTermCriteria termCrit = new MCvTermCriteria(ContTrain, 0.001);
                        recognizer = new EigenObjectRecognizer(
                            trainingImages.ToArray(),
                            labels.ToArray(),
                            currentThreshold,
                            ref termCrit);
                        recognizerNeedsRebuild = false;
                    }
                    else
                    {
                        recognizer.EigenDistanceThreshold = currentThreshold;
                    }

                    float eigenDistance = 0f;
                    name = recognizer.Recognize(result, out eigenDistance);

                    if (!string.IsNullOrEmpty(name))
                    {
                        // Recognized face with confidence score
                        int confidence = (int)Math.Max(20, Math.Min(99, (1.0 - (eigenDistance / (currentThreshold * 1.3))) * 100));
                        string displayTag = string.Format("{0} ({1}%)", name, confidence);
                        currentFrame.Draw(f.rect, new Bgr(Color.LightGreen), 2);
                        currentFrame.Draw(displayTag, ref font, new Point(f.rect.X - 2, f.rect.Y - 6), new Bgr(Color.LightGreen));
                        NamePersons.Add(displayTag);
                    }
                    else
                    {
                        // Unknown person (below confidence threshold)
                        currentFrame.Draw(f.rect, new Bgr(Color.Red), 2);
                        currentFrame.Draw("Unknown", ref font, new Point(f.rect.X - 2, f.rect.Y - 6), new Bgr(Color.OrangeRed));
                        NamePersons.Add("Unknown");
                    }
                }
                else
                {
                    currentFrame.Draw(f.rect, new Bgr(Color.Red), 2);
                    currentFrame.Draw("No Data", ref font, new Point(f.rect.X - 2, f.rect.Y - 6), new Bgr(Color.Yellow));
                    NamePersons.Add("No Data");
                }
            }
            t = 0;

            //Names concatenation of persons recognized
            if (NamePersons.Count > 0)
            {
                for (int nnn = 0; nnn < NamePersons.Count; nnn++)
                {
                    names = names + NamePersons[nnn] + ", ";
                }
            }
            else
            {
                names = "Nobody";
            }

            //Show the faces processed and recognized
            imageBoxFrameGrabber.Image = currentFrame;
            label4.Text = names;
            names = "";

            //Set the number of faces detected on the scene
            label3.Text = facesDetected[0].Length.ToString();
        }
    }
}