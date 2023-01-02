using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;

using System.Reflection;
using System.Runtime.InteropServices;

using Ressive.Utils;

using SnagFree.GlobalKeyboardHook;

namespace RscRemoteSendKeys
{
    public partial class FormMain : Form
    {

        const int ciMAX_KEY_CNT_TO_SEND_ONCE = 20; //4;

        public const string csAPP_TITLE = "Rsc Remote SendKeys v2.02";
        protected const string csAPP_NAME = "RscRemoteSendKeys";

        private GlobalKeyboardHook m_globalKeyboardHook;

        private KeyBuffer m_keyBuffer = new KeyBuffer();
        private int m_iToDoCount_Prev = -1;

        private NotifyIcon m_notifyIcon = null;

        private string m_sHost;
        private int m_iPort;

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

            m_sHost = StorageRegistry.Read("Host", "").Trim();
            m_iPort = StorageRegistry.Read("Port", 9000);
            if (m_sHost.Length == 0)
                lHostValue.Text = "N/A";
            else
                lHostValue.Text = m_sHost + ":" + m_iPort.ToString();

            chbShowOneKey.Checked = (StorageRegistry.Read("ShowOneKeyOnly", 0) > 0);
            if (chbShowOneKey.Checked)
            {
                tbKeys.Text = "";
                tbKeys.TextAlign = HorizontalAlignment.Center;
            }
            else
            {
                tbKeys.TextAlign = HorizontalAlignment.Left;
            }

            chbBeepOnFullBuffer.Checked = (StorageRegistry.Read("BeepOnFullBuffer", 1) > 0);

            MessageBoxEx.DarkMode = true;

            //Hide Caption Bar
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            PlaceWindow();

            chbAutoStart.Checked = IsAppStartWithWindowsOn();

            RefreshNotifyIcon();

            SetupKeyboardHooks();
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

            m_globalKeyboardHook.Dispose();
        }

        private void RefreshNotifyIcon()
        {
            UpdateNotifyIcon();

            if (m_keyBuffer.GetToDoCount() > 0)
            {
                if (Visible)
                {
                    Send();
                }

                if (m_notifyIcon == null)
                    return; //User Close on Error...

                if (m_iToDoCount_Prev != m_keyBuffer.GetToDoCount())
                {
                    UpdateNotifyIcon();
                }
            }
        }

        private void UpdateNotifyIcon()
        {

            bool bJustCreated = false;

            if (m_notifyIcon == null)
            {
                bJustCreated = true;

                m_notifyIcon = new NotifyIcon();

                m_notifyIcon.Click += NotifyIcon_Click;

            }

            int iToDoCount = m_keyBuffer.GetToDoCount();

            if (bJustCreated || (iToDoCount != m_iToDoCount_Prev))
            {
                m_iToDoCount_Prev = iToDoCount;

                string sTx = iToDoCount.ToString();

                string sInfo = "Number of keys to send";
                m_notifyIcon.Text = sInfo;

                Color clrTx = SystemColors.InfoText;
                Color clrBk = SystemColors.Info;
                int iCY = 2;

                if (m_keyBuffer.IsFull())
                {
                    clrTx = Color.White;
                    clrBk = Color.DarkRed;
                }

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
                if (sTx.Length < 2) iCX += 5;
                graphics.DrawString(sTx, font, brush, iCX, iCY);
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

        private void Send()
        {
            if (m_keyBuffer.GetToDoItem() == null)
                return;

            byte[] bytes = new byte[1024];

            // DO NOT!!! //if (m_sHost.Length == 0) return;
            bool bDoSend = true;
            if (m_sHost.Length == 0) bDoSend = false;

            // Connect to a remote device.  
            try
            {
                IPEndPoint remoteEP = null;
                Socket sender = null;

                if (bDoSend)
                {
                    // Establish the remote endpoint for the socket.  
                    // This example uses port 11000 on the local computer.
                    IPHostEntry ipHostInfo = Dns.GetHostEntry(m_sHost); //Dns.GetHostName());
                    IPAddress ipAddress = ipHostInfo.AddressList[0];
                    remoteEP = new IPEndPoint(ipAddress, m_iPort);

                    // Create a TCP/IP  socket.  
                    sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                }

                // Connect the socket to the remote endpoint. Catch any errors.  
                try
                {
                    if (bDoSend)
                    {
                        sender.Connect(remoteEP);
                        //Console.WriteLine("Socket connected to {0}", sender.RemoteEndPoint.ToString());
                    }


                    for (int iCyc = 0; iCyc < ciMAX_KEY_CNT_TO_SEND_ONCE; iCyc++)
                    {
                        KeyBufferItem oKey = m_keyBuffer.GetToDoItem();
                        if (oKey == null)
                            break;
                        bool bHasNextToDoItem = m_keyBuffer.HasNextToDoItem();

                        string sMsg = "";
                        if (oKey.cKey != '\0')
                        {
                            sMsg = ((int) oKey.cKey).ToString();
                        }
                        else if (oKey.sKeyName.Length > 0)
                        {
                            sMsg = oKey.sKeyName;
                        }

                        bool bBadMsg = false;
                        if ((sMsg.Length > 0) && (sMsg.IndexOf('\r') < 0) && (sMsg.IndexOf('\n') < 0))
                        {
                            // Encode the data string into a byte array.
                            if (iCyc > 0)
                            {
                                sMsg = "\r" + sMsg;
                            }

                            if ((iCyc >= (ciMAX_KEY_CNT_TO_SEND_ONCE - 1)) || (!bHasNextToDoItem))
                            {
                                sMsg = sMsg + "\n";
                            }

                            if (bDoSend)
                            {
                                byte[] msg = Encoding.ASCII.GetBytes(sMsg);
                                if (msg != null)
                                {
                                    // Send the data through the socket.  
                                    int bytesSent = sender.Send(msg);

                                    // Receive the response from the remote device.
                                    /*
                                    int bytesRec = sender.Receive(bytes);
                                    Console.WriteLine("Echoed test = {0}",
                                        Encoding.ASCII.GetString(bytes, 0, bytesRec));
                                    */
                                }
                            }
                        }
                        else
                        {
                            bBadMsg = true;
                        }

                        if (oKey.cKey != '\0')
                        {
                            if (chbShowOneKey.Checked)
                                tbKeys.Text = oKey.cKey.ToString();
                            else
                                tbKeys.AppendText(oKey.cKey.ToString());
                        }
                        else if (oKey.sKeyName.Length > 0)
                        {
                            if (oKey.sKeyName[0] == '{')
                            {
                                if (chbShowOneKey.Checked)
                                    tbKeys.Text = oKey.sKeyName;
                                else
                                    tbKeys.AppendText(oKey.sKeyName);
                            }
                            else
                            {
                                lLastKeyPressedValue.BackColor = Color.Red;
                            }
                        }
                        if (bBadMsg)
                        {
                            lLastKeyPressedValue.BackColor = Color.Red;
                        }

                        m_keyBuffer.SetToDoItemDone();

                        if (!bHasNextToDoItem)
                            break; //Have to do so... ...we've sent '\r'!!!
                    }

                    if (bDoSend)
                    {
                        // Release the socket.  
                        sender.Shutdown(SocketShutdown.Both);
                        sender.Close();
                    }
                }
                catch (ArgumentNullException ane)
                {
                    DialogResult dr = MessageBoxEx.Show("Error sending keystroke to " + lHostValue.Text + " !\r\n\r\nArgumentNullException: " + ane.ToString(), csAPP_TITLE, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error, true /*bTopMost*/);
                    if (dr == DialogResult.Cancel)
                    {
                        //Visible = false;
                        Close();
                    }
                }
                catch (SocketException se)
                {
                    DialogResult dr = MessageBoxEx.Show("Error sending keystroke to " + lHostValue.Text + " !\r\n\r\nSocketException: " + se.ToString(), csAPP_TITLE, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error, true /*bTopMost*/);
                    if (dr == DialogResult.Cancel)
                    {
                        //Visible = false;
                        Close();
                    }
                }
                catch (Exception e)
                {
                    DialogResult dr = MessageBoxEx.Show("Error sending keystroke to " + lHostValue.Text + " !\r\n\r\nException: " + e.ToString(), csAPP_TITLE, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error, true /*bTopMost*/);
                    if (dr == DialogResult.Cancel)
                    {
                        //Visible = false;
                        Close();
                    }
                }

            }
            catch (Exception e)
            {
                DialogResult dr = MessageBoxEx.Show("Error sending keystroke to " + lHostValue.Text + " !\r\n\r\nException: " + e.ToString(), csAPP_TITLE, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error, true /*bTopMost*/);
                if (dr == DialogResult.Cancel)
                {
                    //Visible = false;
                    Close();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            tbKeys.Focus();

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
            tbKeys.Focus();

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
            tbKeys.Focus();

            if (m_notifyIcon != null && m_notifyIcon.Visible)
            {
                Visible = false;
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            tbKeys.Focus();

            FormGraph frmSettings = new FormGraph();
            try
            {
                DialogResult dr = frmSettings.ShowDialog();
            }
            finally
            {
                frmSettings = null;
            }

            m_sHost = StorageRegistry.Read("Host", "").Trim();
            m_iPort = StorageRegistry.Read("Port", 9000);
            if (m_sHost.Length == 0)
                lHostValue.Text = "N/A";
            else
                lHostValue.Text = m_sHost + ":" + m_iPort.ToString();
        }

        public void SetupKeyboardHooks()
        {
            m_globalKeyboardHook = new GlobalKeyboardHook();
            m_globalKeyboardHook.KeyboardPressed += OnKeyPressed;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbKeys.Focus();

            tbKeys.Clear();
        }

        private void chbShowOneKey_CheckedChanged(object sender, EventArgs e)
        {
            tbKeys.Focus();

            StorageRegistry.Write("ShowOneKeyOnly", chbShowOneKey.Checked ? 1 : 0);

            if (chbShowOneKey.Checked)
            {
                tbKeys.Text = "";
                tbKeys.TextAlign = HorizontalAlignment.Center;
            }
            else
            {
                tbKeys.TextAlign = HorizontalAlignment.Left;
            }
        }

        private void chbBeepOnFullBuffer_CheckedChanged(object sender, EventArgs e)
        {
            tbKeys.Focus();

            StorageRegistry.Write("BeepOnFullBuffer", chbBeepOnFullBuffer.Checked ? 1 : 0);
        }

        private void OnKeyPressed(object sender, GlobalKeyboardHookEventArgs e)
        {
            //Debug.WriteLine(e.KeyboardData.VirtualCode);

            //if (e.KeyboardData.VirtualCode != GlobalKeyboardHook.VkSnapshot)
            //    return;

            // seems, not needed in the life.
            //if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.SysKeyDown &&
            //    e.KeyboardData.Flags == GlobalKeyboardHook.LlkhfAltdown)
            //{
            //    MessageBox.Show("Alt + Print Screen");
            //    e.Handled = true;
            //}
            //else

            if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown)
            {
                if (!Visible) return;
                if (!tbKeys.Focused) return;

                bool bSetHandled = false;

                string sChr = "";

                switch (e.KeyboardData.VirtualCode)
                {

                    case /*VK_CANCEL*/      0x03: { sChr = "^c";            bSetHandled = true;         break; }

                    case /*VK_BACK*/        0x08: { sChr = "{BACKSPACE}";   bSetHandled = true;         break; }

                    case /*VK_TAB*/         0x09: { sChr = "{TAB}";         bSetHandled = true;         break; }

                    case /*VK_CLEAR*/       0x0C: { sChr = "^l";            bSetHandled = true;         break; }

                    case /*VK_RETURN*/      0x0D: { sChr = "{ENTER}";       bSetHandled = true;         break; }

                    case /*VK_PAUSE*/       0x13: { sChr = "{BREAK}";       bSetHandled = true;         break; }

                    case /*VK_CAPITAL*/	    0x14: { sChr = "{CAPSLOCK}";    bSetHandled = true;         break; }

                    case /*VK_ESCAPE*/      0x1B: { sChr = "{ESC}";         bSetHandled = true;         break; }

                    case /*VK_SPACE*/       0x20: { sChr = " ";             bSetHandled = true;         break; }

                    case /*VK_PRIOR*/	    0x21: { sChr = "{PGUP}";        bSetHandled = true;         break; }

                    case /*VK_NEXT*/	    0x22: { sChr = "{PGDN}";        bSetHandled = true;         break; }

                    case /*VK_END*/         0x23: { sChr = "{END}";         bSetHandled = true;         break; }

                    case /*VK_HOME*/        0x24: { sChr = "{HOME}";        bSetHandled = true;         break; }

                    case /*VK_LEFT*/        0x25: { sChr = "{LEFT}";        bSetHandled = true;         break; }

                    case /*VK_UP*/          0x26: { sChr = "{UP}";          bSetHandled = true;         break; }

                    case /*VK_RIGHT*/       0x27: { sChr = "{RIGHT}";       bSetHandled = true;         break; }

                    case /*VK_DOWN*/        0x28: { sChr = "{DOWN}";        bSetHandled = true;         break; }

                    //VK_SELECT	    0x29
                    //VK_PRINT	    0x2A
                    //VK_EXECUTE	0x2B

                    case /*VK_SNAPSHOT*/    0x2C: { sChr = "{INSERT}";      bSetHandled = true;         break; }

                    case /*VK_INSERT*/      0x2D: { sChr = "{PRTSC}";       bSetHandled = true;         break; }

                    case /*VK_DELETE*/      0x2E: { sChr = "{DELETE}";      bSetHandled = true;         break; }

                    case /*VK_HELP*/	    0x2F: { sChr = "{HELP}";        bSetHandled = true;         break; }

                    case /*VK_LWIN*/	    0x5B: { sChr = "{VK_LWIN}";     bSetHandled = true;         break; }

                    case /*VK_RWIN*/	    0x5C: { sChr = "{VK_RWIN}";     bSetHandled = true;         break; }

                    case /*VK_APPS*/	    0x5D: { sChr = "{VK_APPS}";     bSetHandled = true;         break; }

                    case /*VK_NUMPAD0*/	    0x60: { sChr = "0";             bSetHandled = true;         break; }
                    case /*VK_NUMPAD1*/	    0x61: { sChr = "1";             bSetHandled = true;         break; }
                    case /*VK_NUMPAD2*/	    0x62: { sChr = "2";             bSetHandled = true;         break; }
                    case /*VK_NUMPAD3*/	    0x63: { sChr = "3";             bSetHandled = true;         break; }
                    case /*VK_NUMPAD4*/	    0x64: { sChr = "4";             bSetHandled = true;         break; }
                    case /*VK_NUMPAD5*/	    0x65: { sChr = "5";             bSetHandled = true;         break; }
                    case /*VK_NUMPAD6*/	    0x66: { sChr = "6";             bSetHandled = true;         break; }
                    case /*VK_NUMPAD7*/	    0x67: { sChr = "7";             bSetHandled = true;         break; }
                    case /*VK_NUMPAD8*/	    0x68: { sChr = "8";             bSetHandled = true;         break; }
                    case /*VK_NUMPAD9*/	    0x69: { sChr = "9";             bSetHandled = true;         break; }

                    case /*VK_MULTIPLY*/	0x6A: { sChr = "{MULTIPLY}";    bSetHandled = true;         break; }

                    case /*VK_ADD*/         0x6B: { sChr = "{ADD}";         bSetHandled = true;         break; }

                    case /*VK_SEPARATOR*/	0x6C:
                    {
                        System.Globalization.CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentCulture;
                        //ci.NumberFormat.CurrencyDecimalSeparator;
                        sChr = ci.NumberFormat.CurrencyGroupSeparator;

                        bSetHandled = true;

                        break;
                    }

                    case /*VK_SUBTRACT*/	0x6D: { sChr = "{SUBTRACT}";    bSetHandled = true;         break; }

                    case /*VK_DECIMAL*/     0x6E:
                    {
                        System.Globalization.CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentCulture;
                        sChr = ci.NumberFormat.CurrencyDecimalSeparator;
                        //ci.NumberFormat.CurrencyGroupSeparator;

                        bSetHandled = true;

                        break;
                    }

                    case /*VK_DIVIDE*/      0x6F: { sChr = "{DIVIDE}";      bSetHandled = true;         break; }

                    case /*VK_F1*/	        0x70: { sChr = "{F1}";          bSetHandled = true;         break; }
                    case /*VK_F2*/	        0x71: { sChr = "{F2}";          bSetHandled = true;         break; }
                    case /*VK_F3*/	        0x72: { sChr = "{F3}";          bSetHandled = true;         break; }
                    case /*VK_F4*/	        0x73: { sChr = "{F4}";          bSetHandled = true;         break; }
                    case /*VK_F5*/	        0x74: { sChr = "{F5}";          bSetHandled = true;         break; }
                    case /*VK_F6*/	        0x75: { sChr = "{F6}";          bSetHandled = true;         break; }
                    case /*VK_F7*/	        0x76: { sChr = "{F7}";          bSetHandled = true;         break; }
                    case /*VK_F8*/	        0x77: { sChr = "{F8}";          bSetHandled = true;         break; }
                    case /*VK_F9*/	        0x78: { sChr = "{F9}";          bSetHandled = true;         break; }
                    case /*VK_F10*/	        0x79: { sChr = "{F10}";         bSetHandled = true;         break; }
                    case /*VK_F11*/	        0x7A: { sChr = "{F11}";         bSetHandled = true;         break; }
                    case /*VK_F12*/	        0x7B: { sChr = "{F12}";         bSetHandled = true;         break; }
                    case /*VK_F13*/	        0x7C: { sChr = "{F13}";         bSetHandled = true;         break; }
                    case /*VK_F14*/	        0x7D: { sChr = "{F14}";         bSetHandled = true;         break; }
                    case /*VK_F15*/	        0x7E: { sChr = "{F15}";         bSetHandled = true;         break; }
                    case /*VK_F16*/	        0x7F: { sChr = "{F16}";         bSetHandled = true;         break; }

                    case /*VK_NUMLOCK*/     0x90: { sChr = "{NUMLOCK}";                                 break; }

                    case /*VK_SCROLL*/	    0x91: { sChr = "{SCROLLLOCK}";  bSetHandled = true;         break; }

                    case /*VK_LSHIFT*/      0xA0:
                    case /*VK_RSHIFT*/      0xA1:
                    case /*VK_LCONTROL*/    0xA2:
                    case /*VK_RCONTROL*/    0xA3:
                    case /*VK_LMENU*/       0xA4:
                    case /*VK_RMENU*/       0xA5:
                    {
                        break;
                    }

                    default:
                    {
                        bSetHandled = true;

                        KeysConverter kc = new KeysConverter();
                        string sVK = kc.ConvertToString(e.KeyboardData.VirtualCode);

                        // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // //

                        byte[] keyboardState = new byte[256];
                        bool keyboardStateStatus = GetKeyboardState(keyboardState);

                        if ((GetAsyncKeyState(/*VK_SHIFT*/	0x10) & 0x8001) > 0) { keyboardState[(int)Keys.ShiftKey] = 0xFF; }
                        if ((GetAsyncKeyState(/*VK_RSHIFT*/ 0xA1) & 0x8001) > 0) { keyboardState[(int)Keys.RShiftKey] = 0xFF; keyboardState[(int)Keys.ShiftKey] = 0xFF; }
                        if ((GetAsyncKeyState(/*VK_RSHIFT*/ 0xA1) & 0x8001) > 0) { keyboardState[(int)Keys.RShiftKey] = 0xFF; keyboardState[(int)Keys.ShiftKey] = 0xFF; }

                        if ((GetAsyncKeyState(/*VK_CONTROL*/  0x11) & 0x8001) > 0) { keyboardState[(int)Keys.ControlKey] = 0xFF; }
                        if ((GetAsyncKeyState(/*VK_LCONTROL*/ 0xA2) & 0x8001) > 0) { keyboardState[(int)Keys.LControlKey] = 0xFF; keyboardState[(int)Keys.ControlKey] = 0xFF; }
                        if ((GetAsyncKeyState(/*VK_RCONTROL*/ 0xA3) & 0x8001) > 0) { keyboardState[(int)Keys.RControlKey] = 0xFF; keyboardState[(int)Keys.ControlKey] = 0xFF; }

                        if ((GetAsyncKeyState(/*VK_MENU*/	0x12) & 0x8001) > 0) { keyboardState[(int)Keys.Menu] = 0xFF; }
                        if ((GetAsyncKeyState(/*VK_LMENU*/	0xA4) & 0x8001) > 0) { keyboardState[(int)Keys.LMenu] = 0xFF; keyboardState[(int)Keys.Menu] = 0xFF; }
                        if ((GetAsyncKeyState(/*VK_RMENU*/	0xA5) & 0x8001) > 0) { keyboardState[(int)Keys.RMenu] = 0xFF; keyboardState[(int)Keys.Menu] = 0xFF; }

                        // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // //

                        IntPtr inputLocaleIdentifier = GetKeyboardLayout(0);

                        // 0 - MAPVK_VK_TO_VSC
                        // 1 - MAPVK_VSC_TO_VK
                        // 2 - MAPVK_VK_TO_CHAR
                        //uint scanCode = MapVirtualKey((uint) e.KeyboardData.VirtualCode, 0);
                        //uint scanCode = MapVirtualKeyEx((uint)e.KeyboardData.VirtualCode, 0, inputLocaleIdentifier);

                        StringBuilder result = new StringBuilder();
                        int iRes = ToUnicodeEx((uint)e.KeyboardData.VirtualCode, 0 /*scanCode*/, keyboardState, result, (int)5, (uint)0, inputLocaleIdentifier);
                        switch (iRes)
                        {

                            case -1:
                                sChr += "dead key";
                                break;

                            case 0:
                                sChr += "no idea!";
                                break;

                            case 1:
                            case 2:
                            case 3:
                            case 4:
                                sChr += result.ToString();
                                break;
                        }

                        if (sVK.Length != 1)
                        {
                            if ((sVK.Length > 3) && (sVK.Substring(0, 3) == "Oem"))
                            {
                                //NOP...
                            }
                            else
                            {
                                sChr += " " + sVK;
                            }
                        }

                        break;
                    }
                }

                if (sChr.Length > 0)
                {
                    bool bOk = false;
                    if (sChr.Length == 1)
                    {
                        // SRC: https://stackoverflow.com/questions/18299216/send-special-character-with-sendkeys/18299388
                        char[] acExceptionChars = { '+', '^', '%', '~', '(', ')' };
                        if (sChr.IndexOfAny(acExceptionChars) >= 0)
                        {
                            sChr = "{" + sChr + "}";
                            bOk = m_keyBuffer.Add(sChr);
                        }
                        else if (sChr[0] == '{' || sChr[0] == '}')
                        {
                            sChr = "{" + sChr + "}";
                            bOk = m_keyBuffer.Add(sChr);
                        }
                        else
                        {
                            bOk = m_keyBuffer.Add(sChr[0]);
                        }
                    }
                    else
                    {
                        bOk = m_keyBuffer.Add(sChr);
                    }

                    //if (bOk)
                    {
                        //tbKeys.AppendText(sChr);
                        lLastKeyPressedValue.Text = sChr;

                        if (bOk)
                        {
                            lLastKeyPressedValue.BackColor = Color.DimGray;
                        }
                        else
                        {
                            if (chbBeepOnFullBuffer.Checked)
                            {
                                System.Media.SystemSounds.Beep.Play();
                            }

                            lLastKeyPressedValue.BackColor = Color.DarkRed;
                        }
                    }
                }

                if (Visible)
                {
                    e.Handled = bSetHandled;
                }
            }
        }

        [DllImport("user32.dll")]
        static extern bool GetKeyboardState(byte[] lpKeyState);

        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int vKey);

        [DllImport("user32.dll")]
        static extern uint MapVirtualKey(uint uCode, uint uMapType);

        [DllImport("user32.dll")]
        static extern uint MapVirtualKeyEx(uint uCode, uint uMapType, IntPtr HKL);

        [DllImport("user32.dll")]
        static extern IntPtr GetKeyboardLayout(uint idThread);

        [DllImport("user32.dll")]
        static extern int ToUnicodeEx(uint wVirtKey, uint wScanCode, byte[] lpKeyState, [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszBuff, int cchBuff, uint wFlags, IntPtr dwhkl);
    }
}
