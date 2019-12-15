using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalFileTransfer2
{
    public partial class EnableBluetoothForm : Form
    {
        //Initializes Window and plays a cute little error sound.
        public EnableBluetoothForm()
        {
            InitializeComponent();
            SoundPlayer simpleSound = new SoundPlayer(@"c:\Windows\media\Windows Background.wav");
            simpleSound.Play();
        }

        //Opens bluetooth settings for the user to enable bluetooth.
        private void openSettingsButton_Click(object sender, EventArgs e)
        {
            Process.Start("bthprops.cpl");
        }

        //Finishes the form.
        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
