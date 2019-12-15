namespace MedicalFileTransfer2
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonsPanel = new System.Windows.Forms.Panel();
            this.setDirectoryButton = new System.Windows.Forms.Button();
            this.recieveFileButton = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.filesListBox = new System.Windows.Forms.ListBox();
            this.filesTextView = new System.Windows.Forms.Panel();
            this.filesLabel = new System.Windows.Forms.Label();
            this.openedFileTextBox = new System.Windows.Forms.TextBox();
            this.currentFilePanel = new System.Windows.Forms.Panel();
            this.deleteButton = new System.Windows.Forms.Button();
            this.currentProfileView = new System.Windows.Forms.Label();
            this.buttonsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.filesTextView.SuspendLayout();
            this.currentFilePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonsPanel
            // 
            this.buttonsPanel.Controls.Add(this.setDirectoryButton);
            this.buttonsPanel.Controls.Add(this.recieveFileButton);
            this.buttonsPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonsPanel.Location = new System.Drawing.Point(684, 0);
            this.buttonsPanel.Name = "buttonsPanel";
            this.buttonsPanel.Size = new System.Drawing.Size(100, 561);
            this.buttonsPanel.TabIndex = 0;
            // 
            // setDirectoryButton
            // 
            this.setDirectoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.setDirectoryButton.Location = new System.Drawing.Point(6, 497);
            this.setDirectoryButton.Name = "setDirectoryButton";
            this.setDirectoryButton.Size = new System.Drawing.Size(82, 23);
            this.setDirectoryButton.TabIndex = 1;
            this.setDirectoryButton.Text = "Set Directory";
            this.setDirectoryButton.UseVisualStyleBackColor = true;
            this.setDirectoryButton.Click += new System.EventHandler(this.setDirectoryButton_Click);
            // 
            // recieveFileButton
            // 
            this.recieveFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.recieveFileButton.Location = new System.Drawing.Point(6, 526);
            this.recieveFileButton.Name = "recieveFileButton";
            this.recieveFileButton.Size = new System.Drawing.Size(82, 23);
            this.recieveFileButton.TabIndex = 0;
            this.recieveFileButton.Text = "Receive File";
            this.recieveFileButton.UseVisualStyleBackColor = true;
            this.recieveFileButton.Click += new System.EventHandler(this.recieveFileButton_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.filesListBox);
            this.splitContainer1.Panel1.Controls.Add(this.filesTextView);
            this.splitContainer1.Panel1MinSize = 50;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.openedFileTextBox);
            this.splitContainer1.Panel2.Controls.Add(this.currentFilePanel);
            this.splitContainer1.Panel2MinSize = 200;
            this.splitContainer1.Size = new System.Drawing.Size(684, 561);
            this.splitContainer1.SplitterDistance = 200;
            this.splitContainer1.TabIndex = 0;
            // 
            // filesListBox
            // 
            this.filesListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filesListBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filesListBox.FormattingEnabled = true;
            this.filesListBox.Location = new System.Drawing.Point(4, 34);
            this.filesListBox.Name = "filesListBox";
            this.filesListBox.Size = new System.Drawing.Size(194, 524);
            this.filesListBox.TabIndex = 1;
            this.filesListBox.SelectedIndexChanged += new System.EventHandler(this.filesListBox_SelectedIndexChanged_1);
            // 
            // filesTextView
            // 
            this.filesTextView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filesTextView.Controls.Add(this.filesLabel);
            this.filesTextView.Location = new System.Drawing.Point(3, 0);
            this.filesTextView.Name = "filesTextView";
            this.filesTextView.Size = new System.Drawing.Size(195, 28);
            this.filesTextView.TabIndex = 0;
            // 
            // filesLabel
            // 
            this.filesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filesLabel.AutoSize = true;
            this.filesLabel.Location = new System.Drawing.Point(3, 4);
            this.filesLabel.Name = "filesLabel";
            this.filesLabel.Size = new System.Drawing.Size(81, 13);
            this.filesLabel.TabIndex = 0;
            this.filesLabel.Text = "Directory: None";
            this.filesLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // openedFileTextBox
            // 
            this.openedFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.openedFileTextBox.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openedFileTextBox.Location = new System.Drawing.Point(3, 34);
            this.openedFileTextBox.Multiline = true;
            this.openedFileTextBox.Name = "openedFileTextBox";
            this.openedFileTextBox.ReadOnly = true;
            this.openedFileTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.openedFileTextBox.Size = new System.Drawing.Size(471, 524);
            this.openedFileTextBox.TabIndex = 1;
            this.openedFileTextBox.Text = "(No File Selected)\r\n";
            this.openedFileTextBox.WordWrap = false;
            // 
            // currentFilePanel
            // 
            this.currentFilePanel.Controls.Add(this.deleteButton);
            this.currentFilePanel.Controls.Add(this.currentProfileView);
            this.currentFilePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.currentFilePanel.Location = new System.Drawing.Point(0, 0);
            this.currentFilePanel.Name = "currentFilePanel";
            this.currentFilePanel.Size = new System.Drawing.Size(480, 28);
            this.currentFilePanel.TabIndex = 0;
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteButton.Enabled = false;
            this.deleteButton.Location = new System.Drawing.Point(390, 3);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(87, 23);
            this.deleteButton.TabIndex = 3;
            this.deleteButton.Text = "Delete Profile";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // currentProfileView
            // 
            this.currentProfileView.AutoSize = true;
            this.currentProfileView.Location = new System.Drawing.Point(4, 4);
            this.currentProfileView.Name = "currentProfileView";
            this.currentProfileView.Size = new System.Drawing.Size(105, 13);
            this.currentProfileView.TabIndex = 0;
            this.currentProfileView.Text = "Current Profile: None";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.buttonsPanel);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "Form1";
            this.Text = "Medical File Transfer (Doctor)";
            this.buttonsPanel.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.filesTextView.ResumeLayout(false);
            this.filesTextView.PerformLayout();
            this.currentFilePanel.ResumeLayout(false);
            this.currentFilePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel buttonsPanel;
        private System.Windows.Forms.Button recieveFileButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel currentFilePanel;
        private System.Windows.Forms.Panel filesTextView;
        private System.Windows.Forms.Label filesLabel;
        private System.Windows.Forms.Label currentProfileView;
        private System.Windows.Forms.Button setDirectoryButton;
        private System.Windows.Forms.ListBox filesListBox;
        private System.Windows.Forms.TextBox openedFileTextBox;
        private System.Windows.Forms.Button deleteButton;
    }
}

