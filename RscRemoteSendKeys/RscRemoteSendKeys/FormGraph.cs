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

            tbLogPath.Text = StorageRegistry.Read("LogPath", "");

            chbDoLog.Checked = (StorageRegistry.Read("DoLog", 0) > 0);
        }

        private void btnLogPath_Click(object sender, EventArgs e)
        {

            SaveFileDialog dlg = new SaveFileDialog();

            dlg.Filter = "Log Files (*.log)|*.log|Text Files (*.txt)|*.txt|CSV Files (*.csv)|*.csv";
            dlg.FilterIndex = 0;
            dlg.DefaultExt = "log";

            if (DialogResult.OK == dlg.ShowDialog())
            {
                tbLogPath.Text = dlg.FileName;
            }

        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            DoApply();
        }

        private bool DoApply()
        {
            if (tbLogPath.Text.Length > 0)
            {
                if (!System.IO.File.Exists(tbLogPath.Text))
                {
                    try
                    {
                        System.IO.File.AppendAllText(tbLogPath.Text, "YYYY;MM;DD;hh;mm;ss;fff;ppp;s\r\n");
                    }
                    catch (Exception exc)
                    {
                        MessageBoxEx.Show("LOG path is not accessible!\r\n\r\nError: " + exc.Message, FormMain.csAPP_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error, true /*bTopMost*/);
                        return false;
                    }
                }
            }

            StorageRegistry.Write("LogPath", tbLogPath.Text);

            StorageRegistry.Write("DoLog", chbDoLog.Checked ? 1 : 0);

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
