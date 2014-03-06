using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SRBC_DataParser
{
    public partial class mainForm : Form
    {
        DataParser parser;

        public mainForm()
        {
            InitializeComponent();
        }

        private void BTN_ChooseFile_Click(object sender, EventArgs e)
        {
            DIA_FileSelectionDialog.ShowDialog();
            TXT_fileName.Text = DIA_FileSelectionDialog.FileName;
        }

        private void BTN_Go_Click(object sender, EventArgs e) {
            char[] delims = { '|' };
            parser = new DataParser(TXT_fileName.Text, delims);
            parser.parseFile();

            LIST_Parameters.Items.AddRange(parser.fields);

            foreach (KeyValuePair<String, int> pair in parser.stationToIDDict) 
                LIST_Stations.Items.Add(pair.Key);                        
        }
    }
}
