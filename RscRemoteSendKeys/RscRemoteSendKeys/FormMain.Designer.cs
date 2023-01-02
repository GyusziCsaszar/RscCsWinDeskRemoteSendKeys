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
            this.tbKeys = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(250, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(26, 23);
            this.btnClose.TabIndex = 2;
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
            this.tmrRefresh.Interval = 60000;
            this.tmrRefresh.Tick += new System.EventHandler(this.tmrRefresh_Tick);
            // 
            // chbAutoStart
            // 
            this.chbAutoStart.AutoSize = true;
            this.chbAutoStart.Location = new System.Drawing.Point(184, 12);
            this.chbAutoStart.Name = "chbAutoStart";
            this.chbAutoStart.Size = new System.Drawing.Size(15, 14);
            this.chbAutoStart.TabIndex = 3;
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
            this.btnHide.TabIndex = 1;
            this.btnHide.Text = "V";
            this.toolTip1.SetToolTip(this.btnHide, "Hide this window");
            this.btnHide.UseVisualStyleBackColor = true;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.ForeColor = System.Drawing.Color.White;
            this.btnSettings.Location = new System.Drawing.Point(205, 6);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(26, 23);
            this.btnSettings.TabIndex = 4;
            this.btnSettings.Text = "...";
            this.toolTip1.SetToolTip(this.btnSettings, "Open Settings...");
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // tbKeys
            // 
            this.tbKeys.BackColor = System.Drawing.Color.DimGray;
            this.tbKeys.ForeColor = System.Drawing.Color.White;
            this.tbKeys.Location = new System.Drawing.Point(6, 35);
            this.tbKeys.Multiline = true;
            this.tbKeys.Name = "tbKeys";
            this.tbKeys.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbKeys.Size = new System.Drawing.Size(270, 85);
            this.tbKeys.TabIndex = 10;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(284, 126);
            this.Controls.Add(this.tbKeys);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnHide);
            this.Controls.Add(this.chbAutoStart);
            this.Controls.Add(this.lblBatteryFullLifetimeValue);
            this.Controls.Add(this.lblBatteryFullLifetime);
            this.Controls.Add(this.btnClose);
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
    }
}

