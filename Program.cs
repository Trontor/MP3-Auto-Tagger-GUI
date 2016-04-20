using System;
using System.Threading;
using System.Windows.Forms;

namespace MP3_Auto_Tagger_GUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            foreach (var str in args)
            {
                if (str == "startup")
                { 
                    Thread.Sleep(60000);
                }
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
