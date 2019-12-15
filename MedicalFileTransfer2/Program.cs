using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalFileTransfer2
{
    //This program requires Bluetooth.
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    //Event for receiving of file
    public class MedicalProfileReceived : System.EventArgs
    {
        private String argsMedicalProfile;

        public MedicalProfileReceived(String received)
        {
            argsMedicalProfile = received;
        }

        public string medicalProfile
        {
            get
            {
                return this.argsMedicalProfile;
            }
        }
    }
}
