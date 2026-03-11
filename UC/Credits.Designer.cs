namespace FrameX360.UC
{
    partial class Credits
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
            this.lblCredits = new CustomLabel();
            this.SuspendLayout();
            //
            // lblTitle
            //
            this.lblTitle.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.Chartreuse;
            this.lblTitle.Location = new System.Drawing.Point(24, 24);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(200, 28);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Credits";
            //
            // lblCredits
            //
            this.lblCredits.ForeColor = System.Drawing.Color.White;
            this.lblCredits.Location = new System.Drawing.Point(24, 64);
            this.lblCredits.Name = "lblCredits";
            this.lblCredits.Size = new System.Drawing.Size(580, 200);
            this.lblCredits.TabIndex = 1;
            this.lblCredits.Text = "FrameX360 — Developed by @honest\n\nXEX patching logic and built-in patches.\nDesign and UI framework (Libs).";
            //
            // Credits
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblCredits);
            this.Name = "Credits";
            this.Size = new System.Drawing.Size(653, 512);
            this.ResumeLayout(false);
        }

        private CustomLabel lblTitle;
        private CustomLabel lblCredits;
    }
}
