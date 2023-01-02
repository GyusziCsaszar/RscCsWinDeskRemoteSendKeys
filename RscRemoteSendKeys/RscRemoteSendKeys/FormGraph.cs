using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Ressive.Utils;

namespace RscRemoteSendKeys
{
    public partial class FormGraph : Form
    {
        public FormGraph()
        {
            InitializeComponent();

            tbHost.Text = StorageRegistry.Read("Host", "");

            int iPort = StorageRegistry.Read("Port", 0);
            if (iPort > 0)
                tbPort.Text = iPort.ToString();
            else
                tbPort.Text = "9000";
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            DoApply();
        }

        private bool DoApply()
        {
            int iPort;
            if (!Int32.TryParse(tbPort.Text, out iPort))
            {
                tbPort.Focus();
                MessageBoxEx.Show("Port value is not a number!", FormMain.csAPP_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error, true /*bTopMost*/);
                return false;
            }
            if (iPort <= 1)
            {
                tbPort.Focus();
                MessageBoxEx.Show("Port value is not valid!", FormMain.csAPP_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error, true /*bTopMost*/);
                return false;
            }
            if (tbHost.Text.Length == 0)
            {
                tbHost.Focus();
                MessageBoxEx.Show("Host value is not valid!", FormMain.csAPP_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error, true /*bTopMost*/);
                return false;
            }

            StorageRegistry.Write("Host", tbHost.Text);
            StorageRegistry.Write("Port", iPort);

            return true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (DoApply())
            {
                DialogResult = DialogResult.OK;
            }
        }
    }
}
