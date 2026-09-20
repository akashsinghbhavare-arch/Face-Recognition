using System;
using System.IO;
using System.Windows.Forms;

namespace MultiFaceRec
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string logFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "startup.log");
            try
            {
                File.WriteAllText(logFile, "Starting at " + DateTime.Now + "\r\n");
                Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                File.AppendAllText(logFile, "Directory set. Enabling visual styles...\r\n");
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                File.AppendAllText(logFile, "Starting Application.Run...\r\n");
                Application.Run(new FrmPrincipal());
                File.AppendAllText(logFile, "Application.Run finished.\r\n");
            }
            catch (Exception ex)
            {
                File.AppendAllText(logFile, "EXCEPTION: " + ex.ToString() + "\r\n");
                MessageBox.Show("An error occurred starting FaceRecPro:\n\n" + ex.Message + "\n\nDetails:\n" + ex.ToString(),
                    "FaceRecPro Startup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
