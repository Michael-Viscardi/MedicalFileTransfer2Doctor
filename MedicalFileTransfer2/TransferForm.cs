using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

/**
 * TransferForm by Michael Viscardi
 * This windows form handles the work of receiving a medical profile. The doctor must press "Begin" to begin listening for a connection.
 */
namespace MedicalFileTransfer2
{
    public partial class TransferForm : Form
    {
        public delegate void MedicalProfileReceivedHandler(object sender, MedicalProfileReceived e);    //Delegate and corresponding event that listens for the transfer
        public event MedicalProfileReceivedHandler ReceivedProfile;                                     //being done.
        private Boolean startedTransfer = false;                    //Prevents the starting of multiple bluetoothThreads.
        private delegate void Delegate(string text, int percent);   //Delegate for updating the UI from the bluetoothThread
        private Thread bluetoothThread;
        private String medicalProfile = "";
        private Boolean bluetoothRunning;       //Boolean that stops bluetoothThread when neccessary.
        

        //Initializes window.
        public TransferForm()
        {
            InitializeComponent();
        }

        //Used in ThreadStart construction
        public void doTransfer()
        {
            bluetoothRunning = true;
            Guid mFTUUID = new Guid("ee53bd91-1af0-4807-a6f9-fe3e748fc1bb");
            BluetoothListener connectionListener = new BluetoothListener(mFTUUID);
            connectionListener.Start();
            BluetoothClient patientDevice = connectionListener.AcceptBluetoothClient();
            if (patientDevice != null)
            {
                updateProgress("Connection established. Please pair the devices.", 50);
            }
            Stream inputStream = patientDevice.GetStream();
            /* StreamReader profileReader = new StreamReader(inputStream);
             String medicalProifle = "";
             medicalProifle += profileReader.ReadToEnd();
             Console.WriteLine(medicalProifle);
             //TODO: Get StreamReader to work so I don't have to scan empty bytes off.
             /*using (StreamReader readStream = new StreamReader(inputStream))
             {
                 String medicalProfile;
                 while ((medicalProfile = readStream.ReadLine()) != null)    //Weird syntax, but I followed the documentation example.
                 {
                     Console.WriteLine(medicalProfile);
                 }
             }*/
            //Dangerous, as it assumes that the first read is good enough.
            while (medicalProfile.Equals("") && bluetoothRunning == true)
            {
                //Possibly needs 2048 bytes as a bigger buffer.
                byte[] medicalProfileBytes = new byte[1024];
                inputStream.Read(medicalProfileBytes, 0, medicalProfileBytes.Length);
                medicalProfile = Encoding.UTF8.GetString(medicalProfileBytes);
                bluetoothRunning = false;
            }
            inputStream.Close();
            updateProgress("Medical File Transfer Finished.", 100);
        }

        //Saves the medicalProfile in Form1.
        private void saveButton_Click(object sender, EventArgs e)
        {
            MedicalProfileReceived args = new MedicalProfileReceived(medicalProfile);
            ReceivedProfile(this, args);
            this.Dispose();
        }

        //The begin button's onClick. Starts bluetoothThread if it has not been started already.
        private void button1_Click(object sender, EventArgs e)
        {
            //Checks to see if Bluetooth is enabled.
            if (!(BluetoothRadio.IsSupported))
            {
                EnableBluetoothForm enableBTForm = new EnableBluetoothForm();
                enableBTForm.Show();
                enableBTForm.Activate();
            }
            else if (startedTransfer != true)
            {
                this.transferMessage.Text = "Starting Transfer";
                this.beginButton.Enabled = false;
                this.beginButton.Visible = false;
                centerText(this.transferMessage);
                this.transferProgressBar.Visible = true;
                this.progressMessage.Visible = true;
                bluetoothThread = new Thread(new ThreadStart(doTransfer));
                bluetoothThread.Start();
                startedTransfer = true;
            }
        }

        //Gets rid of the window. Terminates the thread too.
        private void cancelButton_Click(object sender, EventArgs e)
        {
            bluetoothRunning = false;
            this.Dispose();
            //this.Close(); //Perhaps need to cancel bluetoothThread.
        }

        //Centers dynamic text within the transferThread.
        private void centerText(Control centerMe)
        {
            centerMe.Left = (centerMe.Parent.Width - centerMe.Width) / 2;
        }

        //updateProgress updates the UI from bluetoothThread in a Thread-Safe manner.
        private void updateProgress(string updatedText, int progress)
        {
            if (this.progressMessage.InvokeRequired)
            {
                var updateDelegate = new Delegate(updateProgress);
                this.progressMessage.Invoke(updateDelegate, new object[] { updatedText, progress});
            }
            else
            {
                this.progressMessage.Text = updatedText;
                this.transferProgressBar.Value = this.transferProgressBar.Value = progress;
                if (progress == 100)    //Progress is only set to 100 when the transfer is finished.
                {
                    this.saveButton.Enabled = true;
                }
            }
        }
    }
}
