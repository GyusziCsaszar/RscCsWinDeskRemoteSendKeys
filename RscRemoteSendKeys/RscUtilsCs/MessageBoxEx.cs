using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ressive.Utils
{
    public partial class MessageBoxEx : Form
    {

        private const int ciCAPTION_NONCLIENT_WIDTH = 70;
        private const int ciMAX_TX_WIDTH = 388;

        private const int CP_NOCLOSE_BUTTON = 0x200;

        private static bool s_bDisableX = false;

        public static Font CustomFont = null;

        public static bool DarkMode = false;

        public static DialogResult Show(string text)
        {
            return Show(text, "");
        }

        public static DialogResult Show(string text, string caption)
        {
            return Show(text, caption, MessageBoxButtons.OK); // Default button is OK!
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons MessageBoxButtons)
        {
            return Show(text, caption, MessageBoxButtons, MessageBoxIcon.None);
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons MessageBoxButtons, MessageBoxIcon icon)
        {
            return Show(text, caption, MessageBoxButtons, icon, false);
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons MessageBoxButtons, MessageBoxIcon icon, bool bTopMost)
        {
            MessageBoxEx dlg = new MessageBoxEx();

            if (bTopMost) dlg.TopMost = true;

            dlg.Text = caption;

            dlg.tbText.Text = text;

            s_bDisableX = false;

            if (DarkMode)
            {
                dlg.BackColor = Color.Black;
                dlg.ForeColor = Color.White;

                dlg.tbText.BackColor = Color.Black;
                dlg.tbText.ForeColor = Color.White;

                dlg.pbIcon.BackColor = Color.Black;

                dlg.pnlButtons.BackColor = Color.FromArgb(64, 64, 64); //Color.DimGray;
                dlg.pnlButtons.ForeColor = Color.White;

                dlg.btn1.BackColor = Color.Black;
                dlg.btn1.ForeColor = Color.White;
                dlg.btn1.FlatStyle = FlatStyle.Flat;

                dlg.btn2.BackColor = Color.Black;
                dlg.btn2.ForeColor = Color.White;
                dlg.btn2.FlatStyle = FlatStyle.Flat;

                dlg.btn3.BackColor = Color.Black;
                dlg.btn3.ForeColor = Color.White;
                dlg.btn3.FlatStyle = FlatStyle.Flat;
            }

            if (icon != MessageBoxIcon.None)
            {
                dlg.pbIcon.Visible = true;

                dlg.tbText.Left  += ((dlg.pbIcon.Left / 2) + dlg.pbIcon.Width);
                dlg.tbText.Width -= ((dlg.pbIcon.Left / 2) + dlg.pbIcon.Width);

                switch (icon)
                {

                    case MessageBoxIcon.Asterisk:
                  //case MessageBoxIcon.Information:
                        dlg.pbIcon.Image = System.Drawing.SystemIcons.Asterisk.ToBitmap();
                        System.Media.SystemSounds.Asterisk.Play();
                        break;

                    case MessageBoxIcon.Error:
                  //case MessageBoxIcon.Hand:
                  //case MessageBoxIcon.Stop:
                        dlg.pbIcon.Image = System.Drawing.SystemIcons.Error.ToBitmap();
                        System.Media.SystemSounds.Hand.Play();
                        break;

                    case MessageBoxIcon.Exclamation:
                  //case MessageBoxIcon.Warning:
                        dlg.pbIcon.Image = System.Drawing.SystemIcons.Exclamation.ToBitmap();
                        System.Media.SystemSounds.Exclamation.Play();
                        break;

                    case MessageBoxIcon.Question:
                        dlg.pbIcon.Image = System.Drawing.SystemIcons.Question.ToBitmap();
                        System.Media.SystemSounds.Question.Play();
                        break;

                }
            }

            switch (MessageBoxButtons)
            {

                case System.Windows.Forms.MessageBoxButtons.OK:
                {
                    dlg.btn1.Tag = DialogResult.OK;
                    dlg.btn1.Text = "OK";

                    dlg.AcceptButton = dlg.btn1;    // ENTER when no Button in focus...
                    dlg.CancelButton = dlg.btn1;    // ESC

                    break;
                }

                case System.Windows.Forms.MessageBoxButtons.OKCancel:
                {
                    dlg.btn2.Tag = DialogResult.OK;
                    dlg.btn2.Text = "OK";
                    dlg.btn2.Visible = true;

                    dlg.btn1.Tag = DialogResult.Cancel;
                    dlg.btn1.Text = "Mégse";

                    dlg.AcceptButton = dlg.btn2;    // ENTER when no Button in focus...
                    dlg.CancelButton = dlg.btn1;    // ESC

                    break;
                }

                case System.Windows.Forms.MessageBoxButtons.YesNo:
                {
                    dlg.btn2.Tag = DialogResult.Yes;
                    dlg.btn2.Text = "Igen";
                    dlg.btn2.Visible = true;

                    dlg.btn1.Tag = DialogResult.No;
                    dlg.btn1.Text = "Nem";

                    s_bDisableX = true;

                    dlg.AcceptButton = dlg.btn2;    // ENTER when no Button in focus...
                    //dlg.CancelButton = dlg.btn1;    // ESC

                    break;
                }

                case System.Windows.Forms.MessageBoxButtons.YesNoCancel:
                {
                    dlg.btn3.Tag = DialogResult.Yes;
                    dlg.btn3.Text = "Igen";
                    dlg.btn3.Visible = true;

                    dlg.btn2.Tag = DialogResult.No;
                    dlg.btn2.Text = "Nem";
                    dlg.btn2.Visible = true;

                    dlg.btn1.Tag = DialogResult.Cancel;
                    dlg.btn1.Text = "Mégse";

                    dlg.AcceptButton = dlg.btn3;    // ENTER when no Button in focus...
                    dlg.CancelButton = dlg.btn1;    // ESC

                    break;
                }

                case System.Windows.Forms.MessageBoxButtons.RetryCancel:
                {
                    dlg.btn2.Tag = DialogResult.Retry;
                    dlg.btn2.Text = "Ismét";
                    dlg.btn2.Visible = true;

                    dlg.btn1.Tag = DialogResult.Cancel;
                    dlg.btn1.Text = "Mégse";

                    dlg.AcceptButton = dlg.btn2;    // ENTER when no Button in focus...
                    dlg.CancelButton = dlg.btn1;    // ESC

                    break;
                }

                case System.Windows.Forms.MessageBoxButtons.AbortRetryIgnore:
                {
                    dlg.btn3.Tag = DialogResult.Abort;
                    dlg.btn3.Text = "Leállítás";
                    dlg.btn3.Visible = true;

                    dlg.btn2.Tag = DialogResult.Retry;
                    dlg.btn2.Text = "Ismét";
                    dlg.btn2.Visible = true;

                    dlg.btn1.Tag = DialogResult.Ignore;
                    dlg.btn1.Text = "Kihagyás";

                    s_bDisableX = true;

                    dlg.AcceptButton = dlg.btn2;    // ENTER when no Button in focus...
                    //dlg.CancelButton = dlg.btn1;    // ESC

                    break;
                }

            }

            DialogResult dlgRes = dlg.ShowDialog();

            // FIX: Original behaviour!
            if (dlgRes == DialogResult.Cancel)
            {
                switch (MessageBoxButtons)
                {
                    case MessageBoxButtons.OK: dlgRes = DialogResult.OK; break;
                }
            }

            return dlgRes;
        }

        protected override CreateParams CreateParams
        {
            // SRC: https://stackoverflow.com/questions/7301825/how-to-hide-only-the-close-x-button#:~:text=You%20can%27t%20hide%20it,CreateParams%20property%20of%20the%20form.&text=We%20can%20hide%20close%20button,all%20of%20those%20sizing%20buttons.

            get
            {
                CreateParams myCp = base.CreateParams;

                if (MessageBoxEx.s_bDisableX)
                {
                    myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                }

                return myCp;
            }
        }

        public MessageBoxEx()
        {
            InitializeComponent();
        }

        private void MessageBoxEx_Shown(object sender, EventArgs e)
        {
            if (CustomFont != null)
            {
                this.Font = CustomFont;
                tbText.Font = CustomFont;
                btn1.Font = CustomFont;
                btn2.Font = CustomFont;
                btn3.Font = CustomFont;
            }

            if (btn3.Visible)
            {
                int iCX = btn1.Left - btn3.Left;

                Width += iCX;
                Left = Math.Max(10, Left - (iCX / 2));
            }
            else if (btn2.Visible)
            {
                int iCX = btn1.Left - btn2.Left;

                Width += iCX;
                Left = Math.Max(10, Left - (iCX / 2));
            }

            ApplyCaptionSize();
            ApplyTextSize();

            // FIX!
            Rectangle rect = Screen.FromControl(this).WorkingArea; // Bounds;
            if (Top + Height > rect.Top + rect.Height)
            {
                Top = (rect.Top + rect.Height) - Height;
            }
            if (Left + Width > rect.Left + rect.Width)
            {
                Left = (rect.Left + rect.Width) - Width;
            }
        }

        private void ApplyCaptionSize()
        {
            Size szCap = TextRenderer.MeasureText(this.Text, this.Font);

            if (szCap.Width > (this.Width - ciCAPTION_NONCLIENT_WIDTH))
            {

                int iCX = Math.Min(szCap.Width, ciMAX_TX_WIDTH) - (this.Width - ciCAPTION_NONCLIENT_WIDTH);

                Width += iCX;
                Left = Math.Max(10, Left - (iCX / 2));
            }
        }

        private void ApplyTextSize()
        {
            Size szMg = TextRenderer.MeasureText("Mg", tbText.Font);

            Size szTx = TextRenderer.MeasureText(tbText.Text, tbText.Font);

            if ((szTx.Width > ciMAX_TX_WIDTH) || (tbText.Text.IndexOf('\n') >= 0))
            {

                szTx = TextRenderer.MeasureText(tbText.Text, tbText.Font, new Size(ciMAX_TX_WIDTH, 0), TextFormatFlags.WordBreak);

                if (szTx.Height > szMg.Height)
                {

                    //int iCY = tbText.Height * (szTx.Height / szMg.Height);
                    int iCY = szTx.Height - szMg.Height;

                    Height += iCY;
                    Top = Math.Max(10, Top - (iCY / 2));

                    if (pbIcon.Visible)
                    {
                        pbIcon.Top += szMg.Height / 2;
                    }
                }
            }

            if (szTx.Width > tbText.Width)
            {

                int iCX = Math.Min(szTx.Width, ciMAX_TX_WIDTH) - tbText.Width;

                Width += iCX;
                Left = Math.Max(10, Left - (iCX / 2));
            }
        }

        private void btnX_Click(object sender, EventArgs e)
        {
            DialogResult = (DialogResult) (sender as Button).Tag;
        }
    }
}
