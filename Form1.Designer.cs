namespace FrameX360
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.TabAcima = new Guna.UI2.WinForms.Guna2Panel();
            this.customLabel1 = new CustomLabel();
            this._btnLangEn = new System.Windows.Forms.Button();
            this._btnLangPt = new System.Windows.Forms.Button();
            this.guna2ControlBox2 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.BarGradient = new SeparatorGradient();
            this.TabEsquerda = new Guna.UI2.WinForms.Guna2Panel();
            this.TabVertical = new VerticalImageTabControl();
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.pnlContent = new System.Windows.Forms.Panel();
            this.home1 = new FrameX360.UC.Home();
            this.frameX1 = new FrameX360.UC.FrameX();
            this.about1 = new FrameX360.UC.About();
            this.credits1 = new FrameX360.UC.Credits();
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.TabAcima.SuspendLayout();
            this.TabEsquerda.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabAcima
            // 
            this.TabAcima.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.TabAcima.Controls.Add(this.customLabel1);
            this.TabAcima.Controls.Add(this._btnLangEn);
            this.TabAcima.Controls.Add(this._btnLangPt);
            this.TabAcima.Controls.Add(this.guna2ControlBox2);
            this.TabAcima.Controls.Add(this.guna2ControlBox1);
            this.TabAcima.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.TabAcima.Location = new System.Drawing.Point(0, 0);
            this.TabAcima.Name = "TabAcima";
            this.TabAcima.Size = new System.Drawing.Size(850, 42);
            this.TabAcima.TabIndex = 4;
            // 
            // customLabel1
            // 
            this.customLabel1.BackColor = System.Drawing.Color.Transparent;
            this.customLabel1.Font = new System.Drawing.Font("Arial", 11.25F);
            this.customLabel1.ForeColor = System.Drawing.Color.Chartreuse;
            this.customLabel1.Location = new System.Drawing.Point(380, 9);
            this.customLabel1.Name = "customLabel1";
            this.customLabel1.Size = new System.Drawing.Size(90, 25);
            this.customLabel1.TabIndex = 7;
            this.customLabel1.Text = "FrameX360";
            // 
            // _btnLangEn
            // 
            this._btnLangEn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnLangEn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this._btnLangEn.FlatAppearance.BorderSize = 0;
            this._btnLangEn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnLangEn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._btnLangEn.ForeColor = System.Drawing.Color.White;
            this._btnLangEn.Location = new System.Drawing.Point(678, 6);
            this._btnLangEn.Name = "_btnLangEn";
            this._btnLangEn.Size = new System.Drawing.Size(42, 28);
            this._btnLangEn.TabIndex = 9;
            this._btnLangEn.Text = "EN";
            this._btnLangEn.UseVisualStyleBackColor = false;
            this._btnLangEn.Click += new System.EventHandler(this._btnLangEn_Click);
            // 
            // _btnLangPt
            // 
            this._btnLangPt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnLangPt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this._btnLangPt.FlatAppearance.BorderSize = 0;
            this._btnLangPt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnLangPt.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._btnLangPt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(148)))), ((int)(((byte)(158)))));
            this._btnLangPt.Location = new System.Drawing.Point(726, 6);
            this._btnLangPt.Name = "_btnLangPt";
            this._btnLangPt.Size = new System.Drawing.Size(48, 28);
            this._btnLangPt.TabIndex = 10;
            this._btnLangPt.Text = "PT-BR";
            this._btnLangPt.UseVisualStyleBackColor = false;
            this._btnLangPt.Click += new System.EventHandler(this._btnLangPt_Click);
            // 
            // guna2ControlBox2
            // 
            this.guna2ControlBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox2.Animated = true;
            this.guna2ControlBox2.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            this.guna2ControlBox2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.guna2ControlBox2.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox2.Location = new System.Drawing.Point(776, 5);
            this.guna2ControlBox2.Name = "guna2ControlBox2";
            this.guna2ControlBox2.Size = new System.Drawing.Size(32, 29);
            this.guna2ControlBox2.TabIndex = 8;
            // 
            // guna2ControlBox1
            // 
            this.guna2ControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox1.Animated = true;
            this.guna2ControlBox1.ControlBoxStyle = Guna.UI2.WinForms.Enums.ControlBoxStyle.Custom;
            this.guna2ControlBox1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.guna2ControlBox1.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox1.Location = new System.Drawing.Point(814, 5);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.Size = new System.Drawing.Size(32, 29);
            this.guna2ControlBox1.TabIndex = 7;
            this.guna2ControlBox1.Click += new System.EventHandler(this.guna2ControlBox1_Click);
            // 
            // BarGradient
            // 
            this.BarGradient.AnimationSpeed = 15;
            this.BarGradient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.BarGradient.BorderColor = System.Drawing.Color.Green;
            this.BarGradient.BorderThickness = 2;
            this.BarGradient.Location = new System.Drawing.Point(-16, 38);
            this.BarGradient.Name = "BarGradient";
            this.BarGradient.Size = new System.Drawing.Size(882, 10);
            this.BarGradient.TabIndex = 6;
            this.BarGradient.TrailWidth = 230;
            // 
            // TabEsquerda
            // 
            this.TabEsquerda.Controls.Add(this.TabVertical);
            this.TabEsquerda.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.TabEsquerda.Location = new System.Drawing.Point(0, 38);
            this.TabEsquerda.Name = "TabEsquerda";
            this.TabEsquerda.Size = new System.Drawing.Size(199, 522);
            this.TabEsquerda.TabIndex = 7;
            // 
            // TabVertical
            // 
            this.TabVertical.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.TabVertical.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.TabVertical.CornerRadius = 8;
            this.TabVertical.Cursor = System.Windows.Forms.Cursors.Default;
            this.TabVertical.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabVertical.Location = new System.Drawing.Point(13, 27);
            this.TabVertical.Name = "TabVertical";
            this.TabVertical.SelectedIndex = 0;
            this.TabVertical.Size = new System.Drawing.Size(173, 215);
            this.TabVertical.SlideColor = System.Drawing.Color.Green;
            this.TabVertical.Spacing = 5;
            this.TabVertical.Tab1Image = ((System.Drawing.Image)(resources.GetObject("TabVertical.Tab1Image")));
            this.TabVertical.Tab1ImageSize = new System.Drawing.Size(20, 20);
            this.TabVertical.Tab1Text = "Home";
            this.TabVertical.Tab2Image = ((System.Drawing.Image)(resources.GetObject("TabVertical.Tab2Image")));
            this.TabVertical.Tab2ImageSize = new System.Drawing.Size(20, 20);
            this.TabVertical.Tab2Text = "FrameX";
            this.TabVertical.Tab3Image = ((System.Drawing.Image)(resources.GetObject("TabVertical.Tab3Image")));
            this.TabVertical.Tab3ImageSize = new System.Drawing.Size(20, 20);
            this.TabVertical.Tab3Text = "About";
            this.TabVertical.Tab4Image = ((System.Drawing.Image)(resources.GetObject("TabVertical.Tab4Image")));
            this.TabVertical.Tab4ImageSize = new System.Drawing.Size(20, 20);
            this.TabVertical.Tab4Text = "Credits";
            this.TabVertical.TabIndex = 11;
            this.TabVertical.TextColor = System.Drawing.Color.White;
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.DockIndicatorTransparencyValue = 1D;
            this.guna2DragControl1.DragStartTransparencyValue = 1D;
            this.guna2DragControl1.TargetControl = this.TabAcima;
            this.guna2DragControl1.TransparentWhileDrag = false;
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.pnlContent.Controls.Add(this.home1);
            this.pnlContent.Controls.Add(this.frameX1);
            this.pnlContent.Controls.Add(this.about1);
            this.pnlContent.Controls.Add(this.credits1);
            this.pnlContent.Location = new System.Drawing.Point(197, 48);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(653, 512);
            this.pnlContent.TabIndex = 13;
            // 
            // home1
            // 
            this.home1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.home1.Location = new System.Drawing.Point(0, 0);
            this.home1.Name = "home1";
            this.home1.Padding = new System.Windows.Forms.Padding(0, 0, 20, 20);
            this.home1.Size = new System.Drawing.Size(653, 512);
            this.home1.TabIndex = 0;
            // 
            // frameX1
            // 
            this.frameX1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.frameX1.Location = new System.Drawing.Point(0, 0);
            this.frameX1.Name = "frameX1";
            this.frameX1.Padding = new System.Windows.Forms.Padding(0, 0, 20, 20);
            this.frameX1.Size = new System.Drawing.Size(653, 512);
            this.frameX1.TabIndex = 1;
            this.frameX1.Visible = false;
            // 
            // about1
            // 
            this.about1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.about1.Location = new System.Drawing.Point(0, 0);
            this.about1.Name = "about1";
            this.about1.Size = new System.Drawing.Size(653, 512);
            this.about1.TabIndex = 2;
            this.about1.Visible = false;
            // 
            // credits1
            // 
            this.credits1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.credits1.Location = new System.Drawing.Point(0, 0);
            this.credits1.Name = "credits1";
            this.credits1.Size = new System.Drawing.Size(653, 512);
            this.credits1.TabIndex = 3;
            this.credits1.Visible = false;
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.BorderRadius = 12;
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 1D;
            this.guna2BorderlessForm1.DragStartTransparencyValue = 1D;
            this.guna2BorderlessForm1.HasFormShadow = false;
            this.guna2BorderlessForm1.ResizeForm = false;
            this.guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(17)))), ((int)(((byte)(18)))));
            this.ClientSize = new System.Drawing.Size(850, 560);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.BarGradient);
            this.Controls.Add(this.TabAcima);
            this.Controls.Add(this.TabEsquerda);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.TabAcima.ResumeLayout(false);
            this.TabEsquerda.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel TabAcima;
        private SeparatorGradient BarGradient;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox2;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private CustomLabel customLabel1;
        private Guna.UI2.WinForms.Guna2Panel TabEsquerda;
        private VerticalImageTabControl TabVertical;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
        private System.Windows.Forms.Panel pnlContent;
        private UC.Home home1;
        private UC.FrameX frameX1;
        private UC.About about1;
        private UC.Credits credits1;
        private System.Windows.Forms.Button _btnLangEn;
        private System.Windows.Forms.Button _btnLangPt;
        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
    }
}