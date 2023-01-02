namespace RscRemoteSendKeys
{
    partial class FormGraph
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
            this.lblLogPath = new System.Windows.Forms.Label();
            this.tbLogPath = new System.Windows.Forms.TextBox();
            this.btnLogPath = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chbDoLog = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblLogPath
            // 
            this.lblLogPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLogPath.AutoSize = true;
            this.lblLogPath.Location = new System.Drawing.Point(13, 148);
            this.lblLogPath.Name = "lblLogPath";
            this.lblLogPath.Size = new System.Drawing.Size(72, 13);
            this.lblLogPath.TabIndex = 0;
            this.lblLogPath.Text = "LOG file path:";
            // 
            // tbLogPath
            // 
            this.tbLogPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLogPath.BackColor = System.Drawing.Color.Gainsboro;
            this.tbLogPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbLogPath.ForeColor = System.Drawing.Color.Black;
            this.tbLogPath.Location = new System.Drawing.Point(91, 145);
            this.tbLogPath.Name = "tbLogPath";
            this.tbLogPath.Size = new System.Drawing.Size(301, 20);
            this.tbLogPath.TabIndex = 2;
            // 
            // btnLogPath
            // 
            this.btnLogPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogPath.Location = new System.Drawing.Point(398, 142);
            this.btnLogPath.Name = "btnLogPath";
            this.btnLogPath.Size = new System.Drawing.Size(27, 23);
            this.btnLogPath.TabIndex = 1;
            this.btnLogPath.Text = "...";
            this.btnLogPath.UseVisualStyleBackColor = true;
            this.btnLogPath.Click += new System.EventHandler(this.btnLogPath_Click);
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApply.Location = new System.Drawing.Point(171, 190);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 4;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(261, 190);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(352, 190);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // chbDoLog
            // 
            this.chbDoLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbDoLog.AutoSize = true;
            this.chbDoLog.Location = new System.Drawing.Point(16, 180);
            this.chbDoLog.Name = "chbDoLog";
            this.chbDoLog.Size = new System.Drawing.Size(117, 17);
            this.chbDoLog.TabIndex = 3;
            this.chbDoLog.Text = "Log Battery Charge";
            this.chbDoLog.UseVisualStyleBackColor = true;
            // 
            // FormGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(437, 225);
            this.Controls.Add(this.chbDoLog);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnLogPath);
            this.Controls.Add(this.tbLogPath);
            this.Controls.Add(this.lblLogPath);
            this.ForeColor = System.Drawing.Color.White;
            this.MinimizeBox = false;
            this.Name = "FormGraph";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLogPath;
        private System.Windows.Forms.TextBox tbLogPath;
        private System.Windows.Forms.Button btnLogPath;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chbDoLog;
    }
}