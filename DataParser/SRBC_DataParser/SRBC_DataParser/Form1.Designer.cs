namespace SRBC_DataParser
{
    partial class mainForm
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
            this.components = new System.ComponentModel.Container();
            this.informationPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.LIST_Stations = new System.Windows.Forms.ListBox();
            this.LIST_Parameters = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.TXT_OutputLog = new System.Windows.Forms.RichTextBox();
            this.bar_ParsingProgressBar = new System.Windows.Forms.ProgressBar();
            this.BTN_Go = new System.Windows.Forms.Button();
            this.BTN_ChooseFile = new System.Windows.Forms.Button();
            this.TXT_fileName = new System.Windows.Forms.TextBox();
            this.DIA_FileSelectionDialog = new System.Windows.Forms.OpenFileDialog();
            this.TMR_ProcessMonitor = new System.Windows.Forms.Timer(this.components);
            this.informationPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // informationPanel
            // 
            this.informationPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.informationPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.informationPanel.Controls.Add(this.label2);
            this.informationPanel.Controls.Add(this.LIST_Stations);
            this.informationPanel.Controls.Add(this.LIST_Parameters);
            this.informationPanel.Controls.Add(this.label1);
            this.informationPanel.Location = new System.Drawing.Point(12, 12);
            this.informationPanel.Name = "informationPanel";
            this.informationPanel.Size = new System.Drawing.Size(635, 317);
            this.informationPanel.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(129, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Unique Stations:";
            // 
            // LIST_Stations
            // 
            this.LIST_Stations.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LIST_Stations.FormattingEnabled = true;
            this.LIST_Stations.Location = new System.Drawing.Point(132, 20);
            this.LIST_Stations.Name = "LIST_Stations";
            this.LIST_Stations.Size = new System.Drawing.Size(120, 288);
            this.LIST_Stations.TabIndex = 2;
            // 
            // LIST_Parameters
            // 
            this.LIST_Parameters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LIST_Parameters.FormattingEnabled = true;
            this.LIST_Parameters.Location = new System.Drawing.Point(6, 20);
            this.LIST_Parameters.Name = "LIST_Parameters";
            this.LIST_Parameters.Size = new System.Drawing.Size(120, 288);
            this.LIST_Parameters.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Parameters:";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.TXT_OutputLog);
            this.panel2.Controls.Add(this.bar_ParsingProgressBar);
            this.panel2.Controls.Add(this.BTN_Go);
            this.panel2.Controls.Add(this.BTN_ChooseFile);
            this.panel2.Controls.Add(this.TXT_fileName);
            this.panel2.Location = new System.Drawing.Point(12, 335);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(635, 197);
            this.panel2.TabIndex = 1;
            // 
            // TXT_OutputLog
            // 
            this.TXT_OutputLog.BackColor = System.Drawing.Color.White;
            this.TXT_OutputLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_OutputLog.Location = new System.Drawing.Point(3, 32);
            this.TXT_OutputLog.Name = "TXT_OutputLog";
            this.TXT_OutputLog.ReadOnly = true;
            this.TXT_OutputLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.TXT_OutputLog.Size = new System.Drawing.Size(628, 133);
            this.TXT_OutputLog.TabIndex = 4;
            this.TXT_OutputLog.Text = "";
            // 
            // bar_ParsingProgressBar
            // 
            this.bar_ParsingProgressBar.Location = new System.Drawing.Point(2, 169);
            this.bar_ParsingProgressBar.Name = "bar_ParsingProgressBar";
            this.bar_ParsingProgressBar.Size = new System.Drawing.Size(628, 23);
            this.bar_ParsingProgressBar.TabIndex = 3;
            // 
            // BTN_Go
            // 
            this.BTN_Go.BackColor = System.Drawing.Color.PaleGreen;
            this.BTN_Go.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_Go.Location = new System.Drawing.Point(556, 3);
            this.BTN_Go.Name = "BTN_Go";
            this.BTN_Go.Size = new System.Drawing.Size(75, 23);
            this.BTN_Go.TabIndex = 2;
            this.BTN_Go.Text = "GO";
            this.BTN_Go.UseVisualStyleBackColor = false;
            this.BTN_Go.Click += new System.EventHandler(this.BTN_Go_Click);
            // 
            // BTN_ChooseFile
            // 
            this.BTN_ChooseFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_ChooseFile.Location = new System.Drawing.Point(3, 3);
            this.BTN_ChooseFile.Name = "BTN_ChooseFile";
            this.BTN_ChooseFile.Size = new System.Drawing.Size(75, 23);
            this.BTN_ChooseFile.TabIndex = 1;
            this.BTN_ChooseFile.Text = "Choose File";
            this.BTN_ChooseFile.UseVisualStyleBackColor = true;
            this.BTN_ChooseFile.Click += new System.EventHandler(this.BTN_ChooseFile_Click);
            // 
            // TXT_fileName
            // 
            this.TXT_fileName.BackColor = System.Drawing.Color.White;
            this.TXT_fileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_fileName.Location = new System.Drawing.Point(86, 6);
            this.TXT_fileName.Name = "TXT_fileName";
            this.TXT_fileName.Size = new System.Drawing.Size(464, 20);
            this.TXT_fileName.TabIndex = 0;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 544);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.informationPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "mainForm";
            this.Text = "Data Importer";
            this.informationPanel.ResumeLayout(false);
            this.informationPanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel informationPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox LIST_Stations;
        private System.Windows.Forms.ListBox LIST_Parameters;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox TXT_OutputLog;
        private System.Windows.Forms.ProgressBar bar_ParsingProgressBar;
        private System.Windows.Forms.Button BTN_Go;
        private System.Windows.Forms.Button BTN_ChooseFile;
        private System.Windows.Forms.TextBox TXT_fileName;
        private System.Windows.Forms.OpenFileDialog DIA_FileSelectionDialog;
        private System.Windows.Forms.Timer TMR_ProcessMonitor;
    }
}

