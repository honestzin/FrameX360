namespace FrameX360.UC
{
    partial class Home
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            this.cuiPictureBox1 = new CuoreUI.Controls.cuiPictureBox();
            this.pnlWelcome = new FadePanel();
            this.lblWelcomeTitle = new CustomLabel();
            this.lblWelcomeDesc = new CustomLabel();
            this.pnlFeatures = new FadePanel();
            this.lblFeaturesTitle = new CustomLabel();
            this.lblFeature1 = new CustomLabel();
            this.lblFeature2 = new CustomLabel();
            this.lblFeature3 = new CustomLabel();
            this.lblVersion = new CustomLabel();
            this.pnlWelcome.SuspendLayout();
            this.pnlFeatures.SuspendLayout();
            this.SuspendLayout();
            // 
            // cuiPictureBox1
            // 
            this.cuiPictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cuiPictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.cuiPictureBox1.Content = ((System.Drawing.Image)(resources.GetObject("cuiPictureBox1.Content")));
            this.cuiPictureBox1.ForeColor = System.Drawing.Color.Transparent;
            this.cuiPictureBox1.ImageTint = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cuiPictureBox1.Location = new System.Drawing.Point(20, 20);
            this.cuiPictureBox1.Name = "cuiPictureBox1";
            this.cuiPictureBox1.OutlineThickness = 1F;
            this.cuiPictureBox1.PanelOutlineColor = System.Drawing.Color.Empty;
            this.cuiPictureBox1.Rotation = 0;
            this.cuiPictureBox1.Rounding = new System.Windows.Forms.Padding(6);
            this.cuiPictureBox1.Size = new System.Drawing.Size(613, 142);
            this.cuiPictureBox1.TabIndex = 1;
            // 
            // pnlWelcome
            // 
            this.pnlWelcome.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlWelcome.BackColor = System.Drawing.Color.Transparent;
            this.pnlWelcome.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(17)))), ((int)(((byte)(18)))));
            this.pnlWelcome.BorderThickness = 2;
            this.pnlWelcome.Controls.Add(this.lblWelcomeTitle);
            this.pnlWelcome.Controls.Add(this.lblWelcomeDesc);
            this.pnlWelcome.CornerRadius = 8;
            this.pnlWelcome.CurveDensity = 90;
            this.pnlWelcome.FadeLength = 40;
            this.pnlWelcome.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.pnlWelcome.HorizontalLineLength = 0;
            this.pnlWelcome.Location = new System.Drawing.Point(20, 172);
            this.pnlWelcome.Name = "pnlWelcome";
            this.pnlWelcome.SecondaryBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(17)))), ((int)(((byte)(18)))));
            this.pnlWelcome.Size = new System.Drawing.Size(613, 120);
            this.pnlWelcome.TabIndex = 2;
            this.pnlWelcome.VerticalLineLength = 0;
            // 
            // lblWelcomeTitle
            // 
            this.lblWelcomeTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblWelcomeTitle.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblWelcomeTitle.ForeColor = System.Drawing.Color.Chartreuse;
            this.lblWelcomeTitle.Location = new System.Drawing.Point(20, 16);
            this.lblWelcomeTitle.Name = "lblWelcomeTitle";
            this.lblWelcomeTitle.Size = new System.Drawing.Size(350, 28);
            this.lblWelcomeTitle.TabIndex = 0;
            this.lblWelcomeTitle.Text = "Bem-vindo ao FrameX360";
            // 
            // lblWelcomeDesc
            // 
            this.lblWelcomeDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWelcomeDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblWelcomeDesc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblWelcomeDesc.ForeColor = System.Drawing.Color.White;
            this.lblWelcomeDesc.Location = new System.Drawing.Point(20, 48);
            this.lblWelcomeDesc.Name = "lblWelcomeDesc";
            this.lblWelcomeDesc.Size = new System.Drawing.Size(573, 58);
            this.lblWelcomeDesc.TabIndex = 1;
            this.lblWelcomeDesc.Text = "Ferramenta para aplicar patches em XEX (Xbox 360). Use FrameX para descriptografa" +
    "r e aplicar patches de FPS e compatibilidade. Os patches podem vir do repositóri" +
    "o local ou do Xenia.";
            // 
            // pnlFeatures
            // 
            this.pnlFeatures.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFeatures.BackColor = System.Drawing.Color.Transparent;
            this.pnlFeatures.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(17)))), ((int)(((byte)(18)))));
            this.pnlFeatures.BorderThickness = 2;
            this.pnlFeatures.Controls.Add(this.lblFeaturesTitle);
            this.pnlFeatures.Controls.Add(this.lblFeature1);
            this.pnlFeatures.Controls.Add(this.lblFeature2);
            this.pnlFeatures.Controls.Add(this.lblFeature3);
            this.pnlFeatures.CornerRadius = 8;
            this.pnlFeatures.CurveDensity = 90;
            this.pnlFeatures.FadeLength = 40;
            this.pnlFeatures.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.pnlFeatures.HorizontalLineLength = 0;
            this.pnlFeatures.Location = new System.Drawing.Point(20, 302);
            this.pnlFeatures.Name = "pnlFeatures";
            this.pnlFeatures.SecondaryBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(17)))), ((int)(((byte)(18)))));
            this.pnlFeatures.Size = new System.Drawing.Size(613, 140);
            this.pnlFeatures.TabIndex = 3;
            this.pnlFeatures.VerticalLineLength = 0;
            // 
            // lblFeaturesTitle
            // 
            this.lblFeaturesTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblFeaturesTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblFeaturesTitle.ForeColor = System.Drawing.Color.White;
            this.lblFeaturesTitle.Location = new System.Drawing.Point(20, 14);
            this.lblFeaturesTitle.Name = "lblFeaturesTitle";
            this.lblFeaturesTitle.Size = new System.Drawing.Size(200, 22);
            this.lblFeaturesTitle.TabIndex = 0;
            this.lblFeaturesTitle.Text = "Recursos";
            // 
            // lblFeature1
            // 
            this.lblFeature1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFeature1.BackColor = System.Drawing.Color.Transparent;
            this.lblFeature1.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblFeature1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.lblFeature1.Location = new System.Drawing.Point(20, 40);
            this.lblFeature1.Name = "lblFeature1";
            this.lblFeature1.Size = new System.Drawing.Size(573, 22);
            this.lblFeature1.TabIndex = 1;
            this.lblFeature1.Text = "• Descriptografar e descomprimir XEX (RGH/JTAG)";
            // 
            // lblFeature2
            // 
            this.lblFeature2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFeature2.BackColor = System.Drawing.Color.Transparent;
            this.lblFeature2.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblFeature2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.lblFeature2.Location = new System.Drawing.Point(20, 62);
            this.lblFeature2.Name = "lblFeature2";
            this.lblFeature2.Size = new System.Drawing.Size(573, 22);
            this.lblFeature2.TabIndex = 2;
            this.lblFeature2.Text = "• Patches de FPS e compatibilidade (local e Xenia)";
            // 
            // lblFeature3
            // 
            this.lblFeature3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFeature3.BackColor = System.Drawing.Color.Transparent;
            this.lblFeature3.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblFeature3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.lblFeature3.Location = new System.Drawing.Point(20, 84);
            this.lblFeature3.Name = "lblFeature3";
            this.lblFeature3.Size = new System.Drawing.Size(573, 38);
            this.lblFeature3.TabIndex = 3;
            this.lblFeature3.Text = "• Busca por nome no GitHub e scan de padrões no arquivo XEX";
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(148)))), ((int)(((byte)(158)))));
            this.lblVersion.Location = new System.Drawing.Point(20, 458);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(280, 20);
            this.lblVersion.TabIndex = 4;
            this.lblVersion.Text = "FrameX360 — by @honest";
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.pnlFeatures);
            this.Controls.Add(this.pnlWelcome);
            this.Controls.Add(this.cuiPictureBox1);
            this.Name = "Home";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 20, 20);
            this.Size = new System.Drawing.Size(653, 512);
            this.pnlWelcome.ResumeLayout(false);
            this.pnlFeatures.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CuoreUI.Controls.cuiPictureBox cuiPictureBox1;
        private FadePanel pnlWelcome;
        private CustomLabel lblWelcomeTitle;
        private CustomLabel lblWelcomeDesc;
        private FadePanel pnlFeatures;
        private CustomLabel lblFeaturesTitle;
        private CustomLabel lblFeature1;
        private CustomLabel lblFeature2;
        private CustomLabel lblFeature3;
        private CustomLabel lblVersion;
    }
}
