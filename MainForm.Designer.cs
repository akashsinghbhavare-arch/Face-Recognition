namespace MultiFaceRec
{
    partial class FrmPrincipal
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelContent = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.imageBox1 = new Emgu.CV.UI.ImageBox();
            this.chkBurstMode = new System.Windows.Forms.CheckBox();
            this.numBurstShots = new System.Windows.Forms.NumericUpDown();
            this.lblShots = new System.Windows.Forms.Label();
            this.progressBarBurst = new System.Windows.Forms.ProgressBar();
            this.lblBurstStatus = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonRecognitionList = new System.Windows.Forms.Button();
            this.imageBoxFrameGrabber = new Emgu.CV.UI.ImageBox();
            this.groupBoxThreshold = new System.Windows.Forms.GroupBox();
            this.lblThresholdVal = new System.Windows.Forms.Label();
            this.trackBarThreshold = new System.Windows.Forms.TrackBar();
            this.lblStrict = new System.Windows.Forms.Label();
            this.lblLoose = new System.Windows.Forms.Label();
            this.panelContent.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBurstShots)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxFrameGrabber)).BeginInit();
            this.groupBoxThreshold.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarThreshold)).BeginInit();
            this.SuspendLayout();
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.imageBoxFrameGrabber);
            this.panelContent.Controls.Add(this.groupBoxThreshold);
            this.panelContent.Controls.Add(this.groupBox1);
            this.panelContent.Controls.Add(this.groupBox2);
            this.panelContent.Location = new System.Drawing.Point(12, 12);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(729, 328);
            this.panelContent.TabIndex = 0;
            // 
            // imageBoxFrameGrabber
            // 
            this.imageBoxFrameGrabber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBoxFrameGrabber.Location = new System.Drawing.Point(0, 0);
            this.imageBoxFrameGrabber.Name = "imageBoxFrameGrabber";
            this.imageBoxFrameGrabber.Size = new System.Drawing.Size(320, 240);
            this.imageBoxFrameGrabber.TabIndex = 4;
            this.imageBoxFrameGrabber.TabStop = false;
            // 
            // groupBoxThreshold
            // 
            this.groupBoxThreshold.Controls.Add(this.lblThresholdVal);
            this.groupBoxThreshold.Controls.Add(this.trackBarThreshold);
            this.groupBoxThreshold.Controls.Add(this.lblStrict);
            this.groupBoxThreshold.Controls.Add(this.lblLoose);
            this.groupBoxThreshold.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxThreshold.Location = new System.Drawing.Point(0, 244);
            this.groupBoxThreshold.Name = "groupBoxThreshold";
            this.groupBoxThreshold.Size = new System.Drawing.Size(320, 84);
            this.groupBoxThreshold.TabIndex = 5;
            this.groupBoxThreshold.TabStop = false;
            this.groupBoxThreshold.Text = "Recognition Sensitivity & Accuracy";
            // 
            // lblThresholdVal
            // 
            this.lblThresholdVal.AutoSize = true;
            this.lblThresholdVal.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThresholdVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.lblThresholdVal.Location = new System.Drawing.Point(8, 17);
            this.lblThresholdVal.Name = "lblThresholdVal";
            this.lblThresholdVal.Size = new System.Drawing.Size(195, 13);
            this.lblThresholdVal.TabIndex = 0;
            this.lblThresholdVal.Text = "Sensitivity: Balanced (Threshold: 2500)";
            // 
            // trackBarThreshold
            // 
            this.trackBarThreshold.AutoSize = false;
            this.trackBarThreshold.LargeChange = 5;
            this.trackBarThreshold.Location = new System.Drawing.Point(4, 34);
            this.trackBarThreshold.Maximum = 50;
            this.trackBarThreshold.Minimum = 10;
            this.trackBarThreshold.Name = "trackBarThreshold";
            this.trackBarThreshold.Size = new System.Drawing.Size(312, 28);
            this.trackBarThreshold.SmallChange = 1;
            this.trackBarThreshold.TabIndex = 1;
            this.trackBarThreshold.TickFrequency = 5;
            this.trackBarThreshold.Value = 25;
            this.trackBarThreshold.Scroll += new System.EventHandler(this.trackBarThreshold_Scroll);
            // 
            // lblStrict
            // 
            this.lblStrict.AutoSize = true;
            this.lblStrict.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStrict.ForeColor = System.Drawing.Color.DimGray;
            this.lblStrict.Location = new System.Drawing.Point(8, 65);
            this.lblStrict.Name = "lblStrict";
            this.lblStrict.Size = new System.Drawing.Size(107, 12);
            this.lblStrict.TabIndex = 2;
            this.lblStrict.Text = "Strict (Less False Matches)";
            // 
            // lblLoose
            // 
            this.lblLoose.AutoSize = true;
            this.lblLoose.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoose.ForeColor = System.Drawing.Color.DimGray;
            this.lblLoose.Location = new System.Drawing.Point(220, 65);
            this.lblLoose.Name = "lblLoose";
            this.lblLoose.Size = new System.Drawing.Size(95, 12);
            this.lblLoose.TabIndex = 3;
            this.lblLoose.Text = "Loose (Easy Detection)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.imageBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.chkBurstMode);
            this.groupBox1.Controls.Add(this.numBurstShots);
            this.groupBox1.Controls.Add(this.lblShots);
            this.groupBox1.Controls.Add(this.progressBarBurst);
            this.groupBox1.Controls.Add(this.lblBurstStatus);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Location = new System.Drawing.Point(330, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(184, 328);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Training: ";
            // 
            // imageBox1
            // 
            this.imageBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBox1.Location = new System.Drawing.Point(10, 18);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(163, 120);
            this.imageBox1.TabIndex = 5;
            this.imageBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 146);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Name: ";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(50, 143);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(124, 20);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = "Sergio";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chkBurstMode
            // 
            this.chkBurstMode.AutoSize = true;
            this.chkBurstMode.Checked = true;
            this.chkBurstMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBurstMode.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBurstMode.Location = new System.Drawing.Point(8, 172);
            this.chkBurstMode.Name = "chkBurstMode";
            this.chkBurstMode.Size = new System.Drawing.Size(86, 17);
            this.chkBurstMode.TabIndex = 9;
            this.chkBurstMode.Text = "Burst Mode:";
            this.chkBurstMode.UseVisualStyleBackColor = true;
            // 
            // numBurstShots
            // 
            this.numBurstShots.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numBurstShots.Location = new System.Drawing.Point(96, 170);
            this.numBurstShots.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numBurstShots.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numBurstShots.Name = "numBurstShots";
            this.numBurstShots.Size = new System.Drawing.Size(40, 22);
            this.numBurstShots.TabIndex = 10;
            this.numBurstShots.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // lblShots
            // 
            this.lblShots.AutoSize = true;
            this.lblShots.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShots.Location = new System.Drawing.Point(138, 173);
            this.lblShots.Name = "lblShots";
            this.lblShots.Size = new System.Drawing.Size(34, 13);
            this.lblShots.TabIndex = 11;
            this.lblShots.Text = "shots";
            // 
            // progressBarBurst
            // 
            this.progressBarBurst.Location = new System.Drawing.Point(10, 196);
            this.progressBarBurst.Name = "progressBarBurst";
            this.progressBarBurst.Size = new System.Drawing.Size(163, 12);
            this.progressBarBurst.TabIndex = 12;
            // 
            // lblBurstStatus
            // 
            this.lblBurstStatus.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBurstStatus.ForeColor = System.Drawing.Color.DimGray;
            this.lblBurstStatus.Location = new System.Drawing.Point(8, 212);
            this.lblBurstStatus.Name = "lblBurstStatus";
            this.lblBurstStatus.Size = new System.Drawing.Size(168, 16);
            this.lblBurstStatus.TabIndex = 13;
            this.lblBurstStatus.Text = "Ready to train";
            this.lblBurstStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button2.Location = new System.Drawing.Point(24, 235);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(136, 44);
            this.button2.TabIndex = 3;
            this.button2.Text = "2. Add face";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.buttonRecognitionList);
            this.groupBox2.Location = new System.Drawing.Point(520, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(209, 328);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Results: ";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(6, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(197, 18);
            this.label5.TabIndex = 17;
            this.label5.Text = "Persons present in the scene:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(6, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(197, 44);
            this.label4.TabIndex = 16;
            this.label4.Text = "Nobody";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(197, 18);
            this.label2.TabIndex = 14;
            this.label2.Text = "Number of faces detected: ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(6, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(197, 22);
            this.label3.TabIndex = 15;
            this.label3.Text = "0";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button1.Location = new System.Drawing.Point(24, 142);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 48);
            this.button1.TabIndex = 2;
            this.button1.Text = "1. Detect and recognize";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonRecognitionList
            // 
            this.buttonRecognitionList.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonRecognitionList.Location = new System.Drawing.Point(24, 202);
            this.buttonRecognitionList.Name = "buttonRecognitionList";
            this.buttonRecognitionList.Size = new System.Drawing.Size(160, 48);
            this.buttonRecognitionList.TabIndex = 18;
            this.buttonRecognitionList.Text = "List of Recognition";
            this.buttonRecognitionList.UseVisualStyleBackColor = true;
            this.buttonRecognitionList.Click += new System.EventHandler(this.buttonRecognitionList_Click);
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 354);
            this.Controls.Add(this.panelContent);
            this.Name = "FrmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Serg3ant\'s face detector and recgonizer :D";
            this.Load += new System.EventHandler(this.FrmPrincipal_Load);
            this.Resize += new System.EventHandler(this.FrmPrincipal_Resize);
            this.panelContent.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBurstShots)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxFrameGrabber)).EndInit();
            this.groupBoxThreshold.ResumeLayout(false);
            this.groupBoxThreshold.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarThreshold)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Button button2;
        private Emgu.CV.UI.ImageBox imageBoxFrameGrabber;
        private Emgu.CV.UI.ImageBox imageBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkBurstMode;
        private System.Windows.Forms.NumericUpDown numBurstShots;
        private System.Windows.Forms.Label lblShots;
        private System.Windows.Forms.ProgressBar progressBarBurst;
        private System.Windows.Forms.Label lblBurstStatus;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonRecognitionList;
        private System.Windows.Forms.GroupBox groupBoxThreshold;
        private System.Windows.Forms.Label lblThresholdVal;
        private System.Windows.Forms.TrackBar trackBarThreshold;
        private System.Windows.Forms.Label lblStrict;
        private System.Windows.Forms.Label lblLoose;
    }
}

