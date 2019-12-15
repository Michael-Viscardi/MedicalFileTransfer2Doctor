using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using InTheHand.Net.Bluetooth;
/**
 * MedicalFileTransfer2 Doctor Program by Michael Viscardi.
 * This Windows Forms Application targets the 4.7.2 .NET Framework and requires Bluetooth RFCOMM capabilities.
 * It will allow a doctor to Receive a Medical Profiles from patients using the Android app and saves these profiles into a directory.
 * 
 * Form1 by Michael Viscardi (Bad name I know, but I'm afraid to change it because of strict design rules in Windows Forms) 
 * Form1 is the main window of the Doctor Program. It allows the doctor to view saved files, specify the save directory, and start Transfer.
 */
namespace MedicalFileTransfer2
{
    public partial class Form1 : Form
    {
        ArrayList pathArray = new ArrayList();  //Path array is an array of the paths of saved medical profiles.
        //Initializes the window.
        public Form1()
        {
            InitializeComponent();
            fillComponents();
            checkBluetooth();
        }

        //fillComponenets updates Form1's UI
        private void fillComponents()
        {
            this.filesListBox.Items.Clear();
            pathArray.Clear();
            string saveDirectoryPath = ((String)Properties.Settings.Default["currentSaveDirectory"]);
            if (!(Directory.Exists(saveDirectoryPath)))     //Checks if saved directory is present. If not, it shows an alert.
            {
                var directoryGoneAlert = MessageBox.Show("Your medical profile directory is missing. Please return it or select a new one.", "Alert", MessageBoxButtons.OK);
                this.filesLabel.Text = "Old Directory Lost!";
                this.filesListBox.Enabled = false;
                this.recieveFileButton.Enabled = false;
            }
            else
            {
                //All is good, enable features.
                this.filesLabel.Text = "Directory " + saveDirectoryPath;
                this.filesListBox.Enabled = true;
                this.recieveFileButton.Enabled = true;
                try
                {   //Populates files listBox.
                    var savedProfile = Directory.EnumerateFiles(saveDirectoryPath, "*.txt");
                    foreach (String currentProfile in savedProfile)
                    {
                        this.filesListBox.Items.Add(Path.GetFileNameWithoutExtension(currentProfile));
                        pathArray.Add(currentProfile);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Bummer at Directory Enumeration");
                }
            }
        }

        //checkBluetooth checks if bluetooth is enabled on the current device.
        private void checkBluetooth()
        {
            if (!(BluetoothRadio.IsSupported)) {
                EnableBluetoothForm enableBTForm = new EnableBluetoothForm();
                enableBTForm.Show();
                enableBTForm.Activate();
            }
        }

        //Starts the transferForm. This form recieves the medical profile from the Android app.
        private void recieveFileButton_Click(object sender, EventArgs e)
        {
            TransferForm transferForm = new TransferForm();
            transferForm.ReceivedProfile += new TransferForm.MedicalProfileReceivedHandler(saveTransfer);
            transferForm.Show();
        }

        //saveTransfer is used when transferForm is done with its work and the "Save" option is clicked.
        private void saveTransfer(object sender, MedicalProfileReceived e)
        {
            String savePath = ((String) Properties.Settings.Default["currentSaveDirectory"]);
            string[] securityDelimiter = new string[1];
            securityDelimiter[0] = "[END]";
            string[] getFixedMedicalProfile = e.medicalProfile.Split(securityDelimiter, System.StringSplitOptions.None);
            string fixedMedicalProfile = getFixedMedicalProfile[1];
            string[] delimiter = new string[1];
            delimiter[0] = "[INPUT]";
            string[] nameParsed = fixedMedicalProfile.Split(delimiter, System.StringSplitOptions.None);        //Parses name data to use as the transferred profile's name.
            string saveFileName;
            int month = calculateMonth(nameParsed[4]);
            if (nameParsed[1].Equals("[EMPTY]"))        //If no middle name is specified, it is saved as FIRST LAST, otherwise, it is FIRST MIDDLE LAST.
            {
                saveFileName = nameParsed[0] + " " + nameParsed[2] + "   " + month + "-" + nameParsed[5] + "-" + nameParsed[6];
            } else
            {
                saveFileName = nameParsed[0] + " " + nameParsed[1] + " " + nameParsed[2] + "   " + month + "-" + nameParsed[5] + "-" + nameParsed[6];
            }
            string nameWithoutExtension = saveFileName;
            saveFileName += ".txt";
            /*if (File.Exists(Path.Combine(savePath, saveFileName))) //TODO: Logic for repeat files. This code never runs.
            {
                int fileExistCount = 1;
                string newFileName = saveFileName;
                while (File.Exists(savePath + newFileName))
                {
                    newFileName = saveFileName + "(" + fileExistCount + ")";
                }
                saveFileName = newFileName;
            }*/
            //Saves medical profile.
            string finalPath = Path.Combine(savePath, saveFileName);
            using (StreamWriter saveFile = File.CreateText(finalPath))
            {
                saveFile.WriteLine("[MEDICALFILETRANSFER][END]" + fixedMedicalProfile.Trim() + "[END]");
            }
            fillComponents();
            this.filesListBox.SetSelected(this.filesListBox.Items.IndexOf(nameWithoutExtension), true);
        }

        //Loads the SelectedProfile onto the screen in the file view.
        private void loadProfile(String path)
        {
            string unparsedMedicalProfile = "";
            using (StreamReader readProfile = File.OpenText(path))
            {
                String read;
                while ((read = readProfile.ReadLine()) != null)
                {
                    unparsedMedicalProfile += read;
                }
            }
            string[] delimiter = { "[END]" };
            string[] checkIfCorrect = unparsedMedicalProfile.Split(delimiter, System.StringSplitOptions.None);
            if (!(checkIfCorrect[0].Equals("[MEDICALFILETRANSFER]")))
            {
                this.openedFileTextBox.Text = "ERROR: INVALID FILE";
            }
            else
            {
                ArrayList displayProfile = new ArrayList();
                string medicalProfile = checkIfCorrect[1];
                delimiter[0] = "[DYNAMIC]";
                string[] dynamicParsed = medicalProfile.Split(delimiter, System.StringSplitOptions.None);

                string first14 = dynamicParsed[0];      //Static information like name, address, etc.
                string dynamics = dynamicParsed[1];     //Dynamic information like current medications or allergies to medications
                string socialHistory = dynamicParsed[2];    //Static information that records drugs and alcohol use
                string familyHistory = dynamicParsed[3];    //Static information that records family conditions.

                delimiter[0] = "[INPUT]";
                string[] first14Parsed = first14.Split(delimiter, System.StringSplitOptions.None);
                if (first14Parsed[1].Equals("[EMPTY]"))
                {
                    displayProfile.Add("NAME:                  " + first14Parsed[0] + " " + first14Parsed[2]);
                }
                else
                {
                    displayProfile.Add("NAME:                  " + first14Parsed[0] + " " + first14Parsed[1] + " " + first14Parsed[2]);
                }
                displayProfile.Add("GENDER:                " + first14Parsed[3]);
                displayProfile.Add("DATE OF BIRTH:         " + first14Parsed[4] + " " + first14Parsed[5] + ", " + first14Parsed[6]);
                //Apartment Number
                if (first14Parsed[8].Equals("[EMPTY]"))
                {
                    displayProfile.Add("ADDRESS:               " + first14Parsed[7] + " " + first14Parsed[9] + " " + first14Parsed[10] + " " + first14Parsed[11]);
                }
                else
                {
                    displayProfile.Add("ADDRESS:               " + first14Parsed[7] + " " + first14Parsed[8] + " " + first14Parsed[9] + " " + first14Parsed[10] + " " + first14Parsed[11]);
                }
                displayProfile.Add("INSURANCE PROVIDER:    " + first14Parsed[12]);
                displayProfile.Add("INSURANCE ID#:         " + first14Parsed[13]);
                displayProfile.Add("PRIMARY CARE PROVIDER: " + first14Parsed[14]);

                delimiter[0] = "[FIELD]";
                string[] parseDynamics = dynamics.Split(delimiter, System.StringSplitOptions.None);
                string pastMedicalHistory = parseDynamics[0];
                string currentMedication = parseDynamics[1];
                string allergiesToMedications = parseDynamics[2];
                string pastSurgeries = parseDynamics[3];

                delimiter[0] = "[SET]";
                string[] parsedPMH = pastMedicalHistory.Split(delimiter, System.StringSplitOptions.None);
                displayProfile.Add("");
                if (parsedPMH[0].Equals("[EMPTY-FIELD]"))
                {
                    displayProfile.Add("PAST MEDICAL HISTORY: NONE");
                }
                else
                {
                    displayProfile.Add("PAST MEDICAL HISTORY:");
                    for (int i = 0; i < parsedPMH.Length; i++)
                    {
                        displayProfile.Add("\t- " + parsedPMH[i]);
                    }
                }

                delimiter[0] = "[SET]";
                string[] parsedCM = currentMedication.Split(delimiter, System.StringSplitOptions.None);
                displayProfile.Add("");
                if (parsedCM[0].Equals("[EMPTY-FIELD]"))
                {
                    displayProfile.Add("CURRENT MEDICATION: None");
                }
                else
                {
                    displayProfile.Add("CURRENT MEDICATION(S):");
                    delimiter[0] = "[INPUT]";
                    for (int i = 0; i < parsedCM.Length; i++)
                    {
                        string[] nestedCM = parsedCM[i].Split(delimiter, System.StringSplitOptions.None);

                        displayProfile.Add("\t- " + nestedCM[0]);
                        displayProfile.Add("\t\tFREQUENCY:   " + nestedCM[1]);
                        displayProfile.Add("\t\tDOSAGE:      " + nestedCM[2]);
                    }

                    delimiter[0] = "[SET]";
                    displayProfile.Add("");
                    string[] parsedATM = allergiesToMedications.Split(delimiter, System.StringSplitOptions.None);
                    if (parsedATM[0].Equals("[EMPTY-FIELD]"))
                    {
                        displayProfile.Add("ALLERGIES TO MEDICATION(S): NONE");
                    }
                    else
                    {
                        displayProfile.Add("ALLERGIES TO MEDICATIONS:");
                        delimiter[0] = "[INPUT]";
                        for (int i = 0; i < parsedATM.Length; i++)
                        {
                            string[] nestedATM = parsedATM[i].Split(delimiter, System.StringSplitOptions.None);

                            displayProfile.Add("\t- " + nestedATM[0]);
                            displayProfile.Add("\t\tREACTION:    " + nestedATM[1]);
                        }
                    }
                }

                delimiter[0] = "[SET]";
                displayProfile.Add("");
                string[] parsedPS = pastSurgeries.Split(delimiter, System.StringSplitOptions.None);
                if (parsedPS[0].Equals("[EMPTY-FIELD]"))
                {
                    displayProfile.Add("PAST SURGERIES: NONE");
                }
                else
                {
                    displayProfile.Add("PAST SURGERIES:");
                    delimiter[0] = "[INPUT]";
                    for (int i = 0; i < parsedPS.Length; i++)
                    {
                        string[] nestedPS = parsedPS[i].Split(delimiter, System.StringSplitOptions.None);
                        displayProfile.Add("\t- " + nestedPS[0] + " (" + nestedPS[1] + ")");

                    }
                }

                delimiter[0] = "[SET]";
                displayProfile.Add("");
                displayProfile.Add("SOCIAL HISTORY:");
                string[] parsedSH = socialHistory.Split(delimiter, System.StringSplitOptions.None);
                //Tobacco
                if (parsedSH[0].Equals("[EMPTY-FIELD]"))
                {
                    displayProfile.Add("\t- TOBACCO:    No");
                }
                else
                {
                    delimiter[0] = "[INPUT]";
                    string[] parsedTobacco = parsedSH[0].Split(delimiter, System.StringSplitOptions.None);
                    if (parsedTobacco[0].Equals("Yes"))
                    {
                        if (!(parsedTobacco[1].Equals("[EMPTY]")))
                        {
                            displayProfile.Add("\t- SMOKING:    " + parsedTobacco[0] + ",  " + parsedTobacco[1]);
                        }
                        else
                        {
                            displayProfile.Add("\t- SMOKING:    " + parsedTobacco[0] + ",  " + "[NO FREQUENCY GIVEN]");
                        }
                    }
                    else
                    {
                        displayProfile.Add("\t- SMOKING:    " + parsedTobacco[0]);
                    }

                    if (parsedTobacco[2].Equals("Yes"))
                    {
                        if (!(parsedTobacco[3].Equals("[EMPTY]")))
                        {
                            displayProfile.Add("\t- VAPING:     " + parsedTobacco[2] + ",  " + parsedTobacco[3]);
                        }
                        else
                        {
                            displayProfile.Add("\t- VAPING:     " + parsedTobacco[2] + ",  " + "[NO FREQUENCY GIVEN]");
                        }
                    }
                    else
                    {
                        displayProfile.Add("\t- VAPING:     " + parsedTobacco[2]);
                    }

                    if (parsedTobacco[4].Equals("Yes"))
                    {
                        if (!(parsedTobacco[5].Equals("[EMPTY]")))
                        {
                            displayProfile.Add("\t- CHEWING:    " + parsedTobacco[4] + ",  " + parsedTobacco[5]);
                        }
                        else
                        {
                            displayProfile.Add("\t- CHEWING:    " + parsedTobacco[4] + ",  " + "[NO FREQUENCY GIVEN]");
                        }
                    }
                    else
                    {
                        displayProfile.Add("\t- CHEWING:    " + parsedTobacco[4]);
                    }
                }

                delimiter[0] = "[INPUT]";
                string[] parsedAlcohol = parsedSH[1].Split(delimiter, StringSplitOptions.None);
                if (parsedAlcohol[0].Equals("No"))
                {
                    displayProfile.Add("\t- ALCOHOL:    " + parsedAlcohol[0]);
                }
                else
                {
                    if (parsedAlcohol[1].Equals("[EMPTY]"))
                    {
                        displayProfile.Add("\t- ALCOHOL:    " + parsedAlcohol[0] + ",  [NO FREQUENCY GIVEN]");
                    }
                    else
                    {
                        displayProfile.Add("\t- ALCOHOL:    " + parsedAlcohol[0] + ",  " + parsedAlcohol[1]);
                    }
                }

                string[] parsedMarijuana = parsedSH[2].Split(delimiter, StringSplitOptions.None);
                if (parsedMarijuana[0].Equals("No"))
                {
                    displayProfile.Add("\t- MARIJUANA:  " + parsedMarijuana[0]);
                }
                else
                {
                    if (parsedMarijuana[1].Equals("[EMPTY]"))
                    {
                        displayProfile.Add("\t- MARIJUANA:  " + parsedMarijuana[0] + ",  [NO FREQUENCY GIVEN]");
                    }
                    else
                    {
                        displayProfile.Add("\t- MARIJUANA:  " + parsedMarijuana[0] + ",  " + parsedMarijuana[1]);
                    }
                }

                string[] parsedIllicit = parsedSH[3].Split(delimiter, StringSplitOptions.None);
                if (parsedIllicit[0].Equals("No"))
                {
                    displayProfile.Add("\t- ILLICITS:   " + parsedIllicit[0]);
                }
                else
                {
                    if (parsedIllicit[1].Equals("[EMPTY]"))
                    {
                        displayProfile.Add("\t- ILLICITS:   " + parsedIllicit[0] + ",  [NO OTHER ILLICIT SUBSTANCES GIVEN]");
                    }
                    else
                    {
                        displayProfile.Add("\t- ILLICITS:   " + parsedIllicit[0] + ",  " + parsedIllicit[1]);
                    }
                }

                displayProfile.Add("");
                displayProfile.Add("FAMILY HISTORY");
                string[] parsedFH = familyHistory.Split(delimiter, System.StringSplitOptions.None);
                if (parsedFH[0].Equals("[EMPTY]"))
                {
                    displayProfile.Add("\t-FAHTER'S CONDITIONS:    NONE");
                }
                else
                {
                    displayProfile.Add("\t-FATHER'S CONDITIONS:    " + parsedFH[0]);
                }
                if (parsedFH[1].Equals("[EMPTY]"))
                {
                    displayProfile.Add("\t-MOTHER'S CONDITIONS:    NONE");
                }
                else
                {
                    displayProfile.Add("\t-MOTHER'S CONDITIONS:    " + parsedFH[1]);
                }
                if (parsedFH[2].Equals("[EMPTY]"))
                {
                    displayProfile.Add("\t-SIBLING(S)' CONDITIONS: NONE");
                }
                else
                {
                    displayProfile.Add("\t-SIBLING(S)' CONDITIONS: " + parsedFH[2]);
                }




                string[] displayMe = new string[displayProfile.Count];
                for (int i = 0; i < displayMe.Length; i++)
                {
                    displayMe[i] = (string)displayProfile[i];
                }
                this.openedFileTextBox.Lines = displayMe;
            }

        }

        //onClick for Set Directory button. Changes the User's settings to remember the selected file for future use.
        private void setDirectoryButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog selectSaveDirectory = new FolderBrowserDialog();
            selectSaveDirectory.Description = "Select a new Save Directory";
            if (selectSaveDirectory.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default["currentSaveDirectory"] = selectSaveDirectory.SelectedPath;
                Properties.Settings.Default.Save();
            }
            fillComponents();
        }
        
        //Helper method. Returns a number representing a month. This method is used in determining the date of birth to generate a name in the saveTransfer method.
        private int calculateMonth(String month) 
        {
            int monthAsNumber = -1;
            string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"};
            for (int i = 0; i < months.Length; i++)
            {
                if (months[i].Equals(month))
                {
                    monthAsNumber = i + 1;
                }
            }
            return monthAsNumber;
        }

        //Listener for when the doctor selects a saved file.
        private void filesListBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            { 
                int selectedIndex = this.filesListBox.SelectedIndex;
                if (selectedIndex > this.filesListBox.Items.Count && selectedIndex != -1)
                {
                    this.currentProfileView.Text = "Current Profile: " + ((String)this.filesListBox.SelectedItem);
                }
                loadProfile((String)pathArray[selectedIndex]);
                deleteButton.Enabled = true;
            } 
            catch (ArgumentOutOfRangeException aOE)
            {
                //Do nothing. Just prevents crash from raw onClick.
            }
        }

        //onClick for deleteButton.
        private void deleteButton_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.filesListBox.SelectedIndex;
            String deleteFileName = (String)this.filesListBox.SelectedItem + ".txt";
            String directoryPath = (String)Properties.Settings.Default["currentSaveDirectory"];

            //Makes the doctor confirm that they want to delete the currently selected medical profile.
            var confirmed = MessageBox.Show("Are you sure you want to delete " + Path.GetFileNameWithoutExtension(deleteFileName) + "?", "Confirm", MessageBoxButtons.YesNo);
            if (confirmed == DialogResult.Yes)
            {
                this.filesListBox.Items.RemoveAt(selectedIndex);
                pathArray.Remove(pathArray[selectedIndex]);
                String deleteMe = (Path.Combine(directoryPath, deleteFileName));
                File.Delete(deleteMe);
            }
            fillComponents();
        }

        //Does nothing. Accidentally added. Pain to remove because it breaks the Layout Editor..
        private void label1_Click(object sender, EventArgs e)
        {
            //LITERALLY SO USELESS.
        }
    }
}

//TODO: Create default directory.