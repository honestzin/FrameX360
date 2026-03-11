namespace FrameX360.UC
{
    partial class About
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new CustomLabel();
            this.lblDesc = new CustomLabel();
            this.lblVersion = new CustomLabel();
            this.SuspendLayout();
            //
            // lblTitle
            //
            this.lblTitle.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.Chartreuse;
            this.lblTitle.Location = new System.Drawing.Point(24, 24);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(300, 28);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "FrameX360";
            //
            // lblDesc
            //
            this.lblDesc.ForeColor = System.Drawing.Color.White;
            this.lblDesc.Location = new System.Drawing.Point(24, 64);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(580, 120);
            this.lblDesc.TabIndex = 1;
            this.lblDesc.Text = "Xbox 360 XEX Patcher — decrypt and apply patches to XEX executables for RGH/JTAG.";
            //
            // lblVersion
            //
            this.lblVersion.ForeColor = System.Drawing.Color.Gray;
            this.lblVersion.Location = new System.Drawing.Point(24, 200);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(200, 20);
            this.lblVersion.TabIndex = 2;
            this.lblVersion.Text = "Version 1.0 — by @honest";
            //
            // About
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.lblVersion);
            this.Name = "About";
            this.Size = new System.Drawing.Size(653, 512);
            this.ResumeLayout(false);
        }

        private CustomLabel lblTitle;
        private CustomLabel lblDesc;
        private CustomLabel lblVersion;
    }
}
