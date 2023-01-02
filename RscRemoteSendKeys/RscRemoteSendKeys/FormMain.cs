﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Reflection;
using System.Runtime.InteropServices;

using Ressive.Utils;

namespace RscRemoteSendKeys
{
    public partial class FormMain : Form
    {

        public const string csAPP_TITLE = "Rsc Remote SendKeys v1.00";
        protected const string csAPP_NAME = "RscRemoteSendKeys";

        private NotifyIcon m_notifyIcon = null;

        private bool m_bDoLog;
        private string m_sLogPath;

        // SRC: https://stackoverflow.com/questions/12026664/a-generic-error-occurred-in-gdi-when-calling-bitmap-gethicon
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = CharSet.Auto)]
        extern static bool DestroyIcon(IntPtr handle);

        // SRC: https://stackoverflow.com/questions/156046/show-a-form-without-stealing-focus
        private const int SW_SHOWNOACTIVATE = 4;
        private const int HWND_TOPMOST = -1;
        private const uint SWP_NOACTIVATE = 0x0010;
        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        static extern bool SetWindowPos(
             int hWnd,             // Window handle
             int hWndInsertAfter,  // Placement-order handle
             int X,                // Horizontal position
             int Y,                // Vertical position
             int cx,               // Width
             int cy,               // Height
             uint uFlags);         // Window positioning flags
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        static void ShowInactiveTopmost(Form frm)
        {
            ShowWindow(frm.Handle, SW_SHOWNOACTIVATE);
            SetWindowPos(frm.Handle.ToInt32(), HWND_TOPMOST,
            frm.Left, frm.Top, frm.Width, frm.Height,
            SWP_NOACTIVATE);
        }

        public FormMain()
        {
            InitializeComponent();

            StorageRegistry.m_sAppName  = csAPP_NAME;
            this.Text                   = csAPP_TITLE;

            m_sLogPath = StorageRegistry.Read("LogPath", "");
            m_bDoLog = (System.IO.File.Exists(m_sLogPath)) && (StorageRegistry.Read("DoLog", 0) > 0);

            MessageBoxEx.DarkMode = true;

            //Hide Caption Bar
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            PlaceWindow();

            chbAutoStart.Checked = IsAppStartWithWindowsOn();

            if (m_bDoLog)
            {
                try
                {
                    System.IO.File.AppendAllText(m_sLogPath, "0000;00;00;00;00;00;000;000;?\r\n"); //Mark App Start...
                }
                catch (Exception exc)
                {
                    MessageBoxEx.Show("Unable to write LOG file (" + m_sLogPath + ")!\r\n\r\nError: " + exc.Message, FormMain.csAPP_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error, true /*bTopMost*/);
                }
            }

            RefreshNotifyIcon();
        }

        private void PlaceWindow()
        {
            Rectangle rect = Screen.FromControl(this).WorkingArea; // Bounds;
            this.Left = rect.Left + (rect.Width - (this.Width + 5));
            this.Top = rect.Top + (rect.Height - (this.Height + 5));
        }

        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.Visible = false;
            }
            else
            {
                PlaceWindow();

                RefreshNotifyIcon();

                //this.Visible = true;
                ShowInactiveTopmost(this);
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_notifyIcon != null)
            {
                m_notifyIcon.Visible = false;

                IntPtr hIcon = IntPtr.Zero;
                if (m_notifyIcon.Icon != null)
                {
                    hIcon = m_notifyIcon.Icon.Handle;

                    m_notifyIcon.Icon = null;

                    // SRC: https://stackoverflow.com/questions/12026664/a-generic-error-occurred-in-gdi-when-calling-bitmap-gethicon
                    if (hIcon != IntPtr.Zero)
                    {
                        DestroyIcon(hIcon);
                    }
                }

                m_notifyIcon = null;
            }
        }

        private void RefreshNotifyIcon()
        {

            bool bJustCreated = false;

            if (m_notifyIcon == null)
            {
                bJustCreated = true;

                m_notifyIcon = new NotifyIcon();

                m_notifyIcon.Click += NotifyIcon_Click;

            }

            if (bJustCreated || true)
            {
                string sBattLevel = "Mg";

                string sInfo = "TODO...";
                m_notifyIcon.Text = sInfo;

                Color clrTx = SystemColors.InfoText;
                Color clrBk = SystemColors.Info;
                int iCY = 2;

                //m_NotifyIcon.Icon = SystemIcons.Exclamation;

                // SRC: https://stackoverflow.com/questions/25403169/get-application-icon-of-c-sharp-winforms-app
                //m_NotifyIcon.Icon = Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetExecutingAssembly().Location);

                // SRC: https://stackoverflow.com/questions/34075264/i-want-to-display-numbers-on-the-system-tray-notification-icons-on-windows
                Brush brush = new SolidBrush(clrTx);
                Brush brushBk = new SolidBrush(clrBk);
                Pen penBk = new Pen(clrBk);
                // Create a bitmap and draw text on it
                Bitmap bitmap = new Bitmap(24, 24); // 32, 32); // 16, 16);
                Graphics graphics = Graphics.FromImage(bitmap);
                //graphics.DrawRectangle(new Pen(Color.Red), new Rectangle(0, 0, 23, 23));
                graphics.FillEllipse(brushBk, new Rectangle(3, 0, 23 - 4, 23 - 12));
                graphics.DrawEllipse(penBk, new Rectangle(3, 0, 23 - 4, 23 - 12));
                graphics.FillEllipse(brushBk, new Rectangle(3, 12, 23 - 4, 23 - 12));
                graphics.DrawEllipse(penBk, new Rectangle(3, 12, 23 - 4, 23 - 12));
                /*
                graphics.FillRectangle(brushBk, new Rectangle(3, 6, 23 - 5, 23 - 10));
                graphics.DrawRectangle(penBk, new Rectangle(3, 6, 23 - 5, 23 - 10));
                */
                graphics.FillRectangle(brushBk, new Rectangle(1, 6, 23 - 1, 23 - 10));
                graphics.DrawRectangle(penBk, new Rectangle(1, 6, 23 - 1, 23 - 10));
                Font font = new Font("Tahoma", 12); //14);
                int iCX = 0;
                if (sBattLevel.Length < 2) iCX += 5;
                graphics.DrawString(sBattLevel, font, brush, iCX, iCY);
                // Convert the bitmap with text to an Icon

                IntPtr hIconOld = IntPtr.Zero;
                if (m_notifyIcon.Icon != null)
                {
                    hIconOld = m_notifyIcon.Icon.Handle;
                }

                m_notifyIcon.Icon = Icon.FromHandle(bitmap.GetHicon());

                // SRC: https://stackoverflow.com/questions/12026664/a-generic-error-occurred-in-gdi-when-calling-bitmap-gethicon
                if (hIconOld != IntPtr.Zero)
                {
                    DestroyIcon(hIconOld);
                }

                if (!m_notifyIcon.Visible)
                {
                    m_notifyIcon.Visible = true;
                }

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (m_notifyIcon != null && m_notifyIcon.Visible)
            {
                if (DialogResult.Yes == MessageBoxEx.Show("Notification area icon is visible for this app!\r\n\r\nDo you really want to close the application?\r\n\r\nPress No to hide instead!", csAPP_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, true /*bTopMost*/))
                {
                    Close();
                }
                else
                {
                    Visible = false;
                }
            }
            else
            {
                Close();
            }
        }

        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            tmrRefresh.Enabled = false;

            RefreshNotifyIcon();

            tmrRefresh.Enabled = true;
        }

        private void chbAutoStart_CheckedChanged(object sender, EventArgs e)
        {
            // SRC: https://stackoverflow.com/questions/5089601/how-to-run-a-c-sharp-application-at-windows-startup
            Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey
                        ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (chbAutoStart.Checked)
            {
                // BUG: Dll Path!!!
                //string sAppPath = Application.ExecutablePath;
                //string sAppPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                //string sAppPath = System.Reflection.Assembly.GetEntryAssembly().Location;

                // FIX
                string sAppPath = Application.ExecutablePath;
                if (sAppPath.Length > 4)
                {
                    if (sAppPath.Substring(sAppPath.Length - 4).ToLower() == ".dll")
                    {
                        sAppPath = sAppPath.Substring(0, sAppPath.Length - 4) + ".exe";
                    }
                }

                registryKey.SetValue(csAPP_NAME, sAppPath);
            }
            else
            {
                registryKey.DeleteValue(csAPP_NAME);
            }

            registryKey.Dispose();
        }

        public bool IsAppStartWithWindowsOn()
        {
            Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey
                        ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            string sValue = (string)registryKey.GetValue(csAPP_NAME, "");

            // BUG: Dll Path!!!
            //string sAppPath = Application.ExecutablePath;
            //string sAppPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //string sAppPath = System.Reflection.Assembly.GetEntryAssembly().Location;

            // FIX
            string sAppPath = Application.ExecutablePath;
            if (sAppPath.Length > 4)
            {
                if (sAppPath.Substring(sAppPath.Length - 4).ToLower() == ".dll")
                {
                    sAppPath = sAppPath.Substring(0, sAppPath.Length - 4) + ".exe";
                }
            }

            return (sValue == sAppPath);
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            if (m_notifyIcon != null && m_notifyIcon.Visible)
            {
                Visible = false;
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {

            FormGraph frmSettings = new FormGraph();
            try
            {
                DialogResult dr = frmSettings.ShowDialog();
            }
            finally
            {
                frmSettings = null;
            }

            m_sLogPath = StorageRegistry.Read("LogPath", "");
            m_bDoLog = (System.IO.File.Exists(m_sLogPath)) && (StorageRegistry.Read("DoLog", 0) > 0);
        }
    }
}
