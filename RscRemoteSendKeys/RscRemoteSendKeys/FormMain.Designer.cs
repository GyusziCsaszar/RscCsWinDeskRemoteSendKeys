namespace RscRemoteSendKeys
{
    partial class FormMain
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
            this.btnClose = new System.Windows.Forms.Button();
            this.lblBatteryFullLifetime = new System.Windows.Forms.Label();
            this.lblBatteryFullLifetimeValue = new System.Windows.Forms.Label();
            this.tmrRefresh = new System.Windows.Forms.Timer(this.components);
            this.chbAutoStart = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnHide = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.chbShowOneKey = new System.Windows.Forms.CheckBox();
            this.tbKeys = new System.Windows.Forms.TextBox();
            this.lLastKey = new System.Windows.Forms.Label();
            this.lLastKeyPressedValue = new System.Windows.Forms.Label();
            this.lKeys = new System.Windows.Forms.Label();
            this.lHost = new System.Windows.Forms.Label();
            this.lHostValue = new System.Windows.Forms.Label();
            this.chbBeepOnFullBuffer = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(338, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(26, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "X";
            this.toolTip1.SetToolTip(this.btnClose, "Exit application");
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblBatteryFullLifetime
            // 
            this.lblBatteryFullLifetime.AutoSize = true;
            this.lblBatteryFullLifetime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblBatteryFullLifetime.ForeColor = System.Drawing.Color.White;
            this.lblBatteryFullLifetime.Location = new System.Drawing.Point(28, 127);
            this.lblBatteryFullLifetime.Name = "lblBatteryFullLifetime";
            this.lblBatteryFullLifetime.Size = new System.Drawing.Size(136, 17);
            this.lblBatteryFullLifetime.TabIndex = 8;
            this.lblBatteryFullLifetime.Text = "Battery Full Lifetime:";
            // 
            // lblBatteryFullLifetimeValue
            // 
            this.lblBatteryFullLifetimeValue.AutoSize = true;
            this.lblBatteryFullLifetimeValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblBatteryFullLifetimeValue.ForeColor = System.Drawing.Color.White;
            this.lblBatteryFullLifetimeValue.Location = new System.Drawing.Point(170, 127);
            this.lblBatteryFullLifetimeValue.Name = "lblBatteryFullLifetimeValue";
            this.lblBatteryFullLifetimeValue.Size = new System.Drawing.Size(31, 17);
            this.lblBatteryFullLifetimeValue.TabIndex = 9;
            this.lblBatteryFullLifetimeValue.Text = "N/A";
            // 
            // tmrRefresh
            // 
            this.tmrRefresh.Enabled = true;
            this.tmrRefresh.Tick += new System.EventHandler(this.tmrRefresh_Tick);
            // 
            // chbAutoStart
            // 
            this.chbAutoStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chbAutoStart.AutoSize = true;
            this.chbAutoStart.Location = new System.Drawing.Point(272, 12);
            this.chbAutoStart.Name = "chbAutoStart";
            this.chbAutoStart.Size = new System.Drawing.Size(15, 14);
            this.chbAutoStart.TabIndex = 5;
            this.toolTip1.SetToolTip(this.chbAutoStart, "Start with Windows");
            this.chbAutoStart.UseVisualStyleBackColor = true;
            this.chbAutoStart.CheckedChanged += new System.EventHandler(this.chbAutoStart_CheckedChanged);
            // 
            // btnHide
            // 
            this.btnHide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHide.ForeColor = System.Drawing.Color.White;
            this.btnHide.Location = new System.Drawing.Point(6, 6);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(26, 23);
            this.btnHide.TabIndex = 2;
            this.btnHide.Text = "V";
            this.toolTip1.SetToolTip(this.btnHide, "Hide this window");
            this.btnHide.UseVisualStyleBackColor = true;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.ForeColor = System.Drawing.Color.White;
            this.btnSettings.Location = new System.Drawing.Point(293, 6);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(26, 23);
            this.btnSettings.TabIndex = 6;
            this.btnSettings.Text = "...";
            this.toolTip1.SetToolTip(this.btnSettings, "Open Settings...");
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(238, 6);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(26, 23);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "-";
            this.toolTip1.SetToolTip(this.btnClear, "Clear typed text");
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // chbShowOneKey
            // 
            this.chbShowOneKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chbShowOneKey.AutoSize = true;
            this.chbShowOneKey.Location = new System.Drawing.Point(7, 33);
            this.chbShowOneKey.Name = "chbShowOneKey";
            this.chbShowOneKey.Size = new System.Drawing.Size(15, 14);
            this.chbShowOneKey.TabIndex = 16;
            this.toolTip1.SetToolTip(this.chbShowOneKey, "Show only one Keystroke below.");
            this.chbShowOneKey.UseVisualStyleBackColor = true;
            this.chbShowOneKey.CheckedChanged += new System.EventHandler(this.chbShowOneKey_CheckedChanged);
            // 
            // tbKeys
            // 
            this.tbKeys.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbKeys.BackColor = System.Drawing.Color.DimGray;
            this.tbKeys.ForeColor = System.Drawing.Color.White;
            this.tbKeys.Location = new System.Drawing.Point(6, 49);
            this.tbKeys.Multiline = true;
            this.tbKeys.Name = "tbKeys";
            this.tbKeys.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbKeys.Size = new System.Drawing.Size(358, 161);
            this.tbKeys.TabIndex = 1;
            // 
            // lLastKey
            // 
            this.lLastKey.AutoSize = true;
            this.lLastKey.ForeColor = System.Drawing.Color.White;
            this.lLastKey.Location = new System.Drawing.Point(3, 216);
            this.lLastKey.Name = "lLastKey";
            this.lLastKey.Size = new System.Drawing.Size(90, 13);
            this.lLastKey.TabIndex = 11;
            this.lLastKey.Text = "Last key pressed:";
            // 
            // lLastKeyPressedValue
            // 
            this.lLastKeyPressedValue.BackColor = System.Drawing.Color.DimGray;
            this.lLastKeyPressedValue.ForeColor = System.Drawing.Color.White;
            this.lLastKeyPressedValue.Location = new System.Drawing.Point(99, 215);
            this.lLastKeyPressedValue.Name = "lLastKeyPressedValue";
            this.lLastKeyPressedValue.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.lLastKeyPressedValue.Size = new System.Drawing.Size(265, 17);
            this.lLastKeyPressedValue.TabIndex = 12;
            // 
            // lKeys
            // 
            this.lKeys.AutoSize = true;
            this.lKeys.ForeColor = System.Drawing.Color.White;
            this.lKeys.Location = new System.Drawing.Point(24, 33);
            this.lKeys.Name = "lKeys";
            this.lKeys.Size = new System.Drawing.Size(160, 13);
            this.lKeys.TabIndex = 13;
            this.lKeys.Text = "Type below to Send Keystrokes!";
            // 
            // lHost
            // 
            this.lHost.AutoSize = true;
            this.lHost.ForeColor = System.Drawing.Color.White;
            this.lHost.Location = new System.Drawing.Point(38, 12);
            this.lHost.Name = "lHost";
            this.lHost.Size = new System.Drawing.Size(32, 13);
            this.lHost.TabIndex = 14;
            this.lHost.Text = "Host:";
            // 
            // lHostValue
            // 
            this.lHostValue.BackColor = System.Drawing.Color.DimGray;
            this.lHostValue.ForeColor = System.Drawing.Color.White;
            this.lHostValue.Location = new System.Drawing.Point(76, 10);
            this.lHostValue.Name = "lHostValue";
            this.lHostValue.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.lHostValue.Size = new System.Drawing.Size(152, 17);
            this.lHostValue.TabIndex = 15;
            // 
            // chbBeepOnFullBuffer
            // 
            this.chbBeepOnFullBuffer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chbBeepOnFullBuffer.AutoSize = true;
            this.chbBeepOnFullBuffer.Location = new System.Drawing.Point(349, 33);
            this.chbBeepOnFullBuffer.Name = "chbBeepOnFullBuffer";
            this.chbBeepOnFullBuffer.Size = new System.Drawing.Size(15, 14);
            this.chbBeepOnFullBuffer.TabIndex = 17;
            this.toolTip1.SetToolTip(this.chbBeepOnFullBuffer, "Beep when KeyBuffer is full");
            this.chbBeepOnFullBuffer.UseVisualStyleBackColor = true;
            this.chbBeepOnFullBuffer.CheckedChanged += new System.EventHandler(this.chbBeepOnFullBuffer_CheckedChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(372, 235);
            this.Controls.Add(this.chbBeepOnFullBuffer);
            this.Controls.Add(this.chbShowOneKey);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lHostValue);
            this.Controls.Add(this.lHost);
            this.Controls.Add(this.lKeys);
            this.Controls.Add(this.lLastKeyPressedValue);
            this.Controls.Add(this.lLastKey);
            this.Controls.Add(this.tbKeys);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnHide);
            this.Controls.Add(this.chbAutoStart);
            this.Controls.Add(this.lblBatteryFullLifetimeValue);
            this.Controls.Add(this.lblBatteryFullLifetime);
            this.Controls.Add(this.btnClose);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "FormMain";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblBatteryFullLifetime;
        private System.Windows.Forms.Label lblBatteryFullLifetimeValue;
        private System.Windows.Forms.Timer tmrRefresh;
        private System.Windows.Forms.CheckBox chbAutoStart;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnHide;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.TextBox tbKeys;
        private System.Windows.Forms.Label lLastKey;
        private System.Windows.Forms.Label lLastKeyPressedValue;
        private System.Windows.Forms.Label lKeys;
        private System.Windows.Forms.Label lHost;
        private System.Windows.Forms.Label lHostValue;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.CheckBox chbShowOneKey;
        private System.Windows.Forms.CheckBox chbBeepOnFullBuffer;
    }
}

