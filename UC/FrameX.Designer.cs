namespace FrameX360.UC
{
    partial class FrameX
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlFile = new FadePanel();
            this.lblXex = new CustomLabel();
            this.txXex = new AdvancedTextBox();
            this.btnBrowse = new CustomControls.AnimatedButton();
            this.lblOut = new CustomLabel();
            this.txOutput = new AdvancedTextBox();
            this.lblTid = new CustomLabel();
            this.txTitleId = new AdvancedTextBox();
            this.lblTidFmt = new CustomLabel();
            this.btnDecrypt = new CustomControls.AnimatedButton();
            this.progDecrypt = new System.Windows.Forms.ProgressBar();
            this.pnlMain = new FadePanel();
            this.pnlPatchView = new FadePanel();
            this.pnlBottomBar = new System.Windows.Forms.Panel();
            this.pnlTopBar = new System.Windows.Forms.Panel();
            this.btnApply = new CustomControls.AnimatedButton();
            this.progPatch = new System.Windows.Forms.ProgressBar();
            this.lblGame = new CustomLabel();
            this.cmbGame = new System.Windows.Forms.ComboBox();
            this.btnSearchPatches = new CustomControls.AnimatedButton();
            this.btnRefresh = new CustomControls.AnimatedButton();
            this.lblGameStatus = new CustomLabel();
            this.pnlPatches = new FadePanel();
            this.pnlFile.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlPatchView.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlFile
            // 
            this.pnlFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFile.BackColor = System.Drawing.Color.Transparent;
            this.pnlFile.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(17)))), ((int)(((byte)(18)))));
            this.pnlFile.BorderThickness = 2;
            this.pnlFile.Controls.Add(this.lblXex);
            this.pnlFile.Controls.Add(this.txXex);
            this.pnlFile.Controls.Add(this.btnBrowse);
            this.pnlFile.Controls.Add(this.lblOut);
            this.pnlFile.Controls.Add(this.txOutput);
            this.pnlFile.Controls.Add(this.lblTid);
            this.pnlFile.Controls.Add(this.txTitleId);
            this.pnlFile.Controls.Add(this.lblTidFmt);
            this.pnlFile.Controls.Add(this.btnDecrypt);
            this.pnlFile.Controls.Add(this.progDecrypt);
            this.pnlFile.CornerRadius = 8;
            this.pnlFile.CurveDensity = 90;
            this.pnlFile.FadeLength = 40;
            this.pnlFile.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.pnlFile.HorizontalLineLength = 0;
            this.pnlFile.Location = new System.Drawing.Point(20, 20);
            this.pnlFile.Name = "pnlFile";
            this.pnlFile.SecondaryBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(17)))), ((int)(((byte)(18)))));
            this.pnlFile.Size = new System.Drawing.Size(613, 232);
            this.pnlFile.TabIndex = 0;
            this.pnlFile.VerticalLineLength = 0;
            // 
            // lblXex
            // 
            this.lblXex.BackColor = System.Drawing.Color.Transparent;
            this.lblXex.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblXex.ForeColor = System.Drawing.Color.White;
            this.lblXex.Location = new System.Drawing.Point(20, 20);
            this.lblXex.Name = "lblXex";
            this.lblXex.Size = new System.Drawing.Size(80, 22);
            this.lblXex.TabIndex = 0;
            this.lblXex.Text = "XEX file";
            // 
            // txXex
            // 
            this.txXex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txXex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(17)))), ((int)(((byte)(18)))));
            this.txXex.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(17)))), ((int)(((byte)(18)))));
            this.txXex.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txXex.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(17)))), ((int)(((byte)(18)))));
            this.txXex.BorderRadius = 6;
            this.txXex.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txXex.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.txXex.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txXex.HoverColor = System.Drawing.Color.Transparent;
            this.txXex.Location = new System.Drawing.Point(20, 44);
            this.txXex.Name = "txXex";
            this.txXex.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.txXex.PlaceholderColor = System.Drawing.Color.Gray;
            this.txXex.PlaceholderText = "Path to .xex file";
            this.txXex.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(75)))));
            this.txXex.SelectionLength = 0;
            this.txXex.SelectionStart = 0;
            this.txXex.Size = new System.Drawing.Size(406, 30);
            this.txXex.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.AnimationFillDirection = CustomControls.AnimatedButton.FillDirection.LeftToRight;
            this.btnBrowse.AnimationFillStyle = CustomControls.AnimatedButton.FillStyle.Solid;
            this.btnBrowse.AnimationSpeed = 0.8F;
            this.btnBrowse.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowse.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnBrowse.CornerRadius = 6;
            this.btnBrowse.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnBrowse.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnBrowse.HoverColor = System.Drawing.Color.Green;
            this.btnBrowse.InsideColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(17)))), ((int)(((byte)(18)))));
            this.btnBrowse.Location = new System.Drawing.Point(438, 42);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.ShowToolTip = false;
            this.btnBrowse.Size = new System.Drawing.Size(155, 32);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.TextColor = System.Drawing.Color.White;
            this.btnBrowse.TextHoverColor = System.Drawing.Color.White;
            this.btnBrowse.ToolTipIcon = "";
            this.btnBrowse.ToolTipMessage = "";
            // 
            // lblOut
            // 
            this.lblOut.BackColor = System.Drawing.Color.Transparent;
            this.lblOut.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblOut.ForeColor = System.Drawing.Color.White;
            this.lblOut.Location = new System.Drawing.Point(20, 88);
            this.lblOut.Name = "lblOut";
            this.lblOut.Size = new System.Drawing.Size(80, 22);
            this.lblOut.TabIndex = 3;
            this.lblOut.Text = "Output";
            // 
            // txOutput
            // 
            this.txOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txOutput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(17)))), ((int)(((byte)(18)))));
            this.txOutput.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(17)))), ((int)(((byte)(18)))));
            this.txOutput.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txOutput.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(17)))), ((int)(((byte)(18)))));
            this.txOutput.BorderRadius = 6;
            this.txOutput.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txOutput.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.txOutput.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txOutput.HoverColor = System.Drawing.Color.Transparent;
            this.txOutput.Location = new System.Drawing.Point(20, 112);
            this.txOutput.Name = "txOutput";
            this.txOutput.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.txOutput.PlaceholderColor = System.Drawing.Color.Gray;
            this.txOutput.PlaceholderText = "";
            this.txOutput.ReadOnly = true;
            this.txOutput.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(75)))));
            this.txOutput.SelectionLength = 0;
            this.txOutput.SelectionStart = 0;
            this.txOutput.Size = new System.Drawing.Size(573, 30);
            this.txOutput.TabIndex = 4;
            // 
            // lblTid
            // 
            this.lblTid.BackColor = System.Drawing.Color.Transparent;
            this.lblTid.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTid.ForeColor = System.Drawing.Color.White;
            this.lblTid.Location = new System.Drawing.Point(20, 156);
            this.lblTid.Name = "lblTid";
            this.lblTid.Size = new System.Drawing.Size(52, 22);
            this.lblTid.TabIndex = 5;
            this.lblTid.Text = "Title ID";
            // 
            // txTitleId
            // 
            this.txTitleId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(17)))), ((int)(((byte)(18)))));
            this.txTitleId.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(17)))), ((int)(((byte)(18)))));
            this.txTitleId.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txTitleId.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(17)))), ((int)(((byte)(18)))));
            this.txTitleId.BorderRadius = 6;
            this.txTitleId.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txTitleId.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.txTitleId.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txTitleId.HoverColor = System.Drawing.Color.Transparent;
            this.txTitleId.Location = new System.Drawing.Point(78, 154);
            this.txTitleId.Name = "txTitleId";
            this.txTitleId.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.txTitleId.PlaceholderColor = System.Drawing.Color.Gray;
            this.txTitleId.PlaceholderText = "";
            this.txTitleId.ReadOnly = true;
            this.txTitleId.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(75)))));
            this.txTitleId.SelectionLength = 0;
            this.txTitleId.SelectionStart = 0;
            this.txTitleId.Size = new System.Drawing.Size(220, 28);
            this.txTitleId.TabIndex = 6;
            // 
            // lblTidFmt
            // 
            this.lblTidFmt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTidFmt.BackColor = System.Drawing.Color.Transparent;
            this.lblTidFmt.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTidFmt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(148)))), ((int)(((byte)(158)))));
            this.lblTidFmt.Location = new System.Drawing.Point(306, 158);
            this.lblTidFmt.Name = "lblTidFmt";
            this.lblTidFmt.Size = new System.Drawing.Size(287, 20);
            this.lblTidFmt.TabIndex = 7;
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDecrypt.AnimationFillDirection = CustomControls.AnimatedButton.FillDirection.LeftToRight;
            this.btnDecrypt.AnimationFillStyle = CustomControls.AnimatedButton.FillStyle.Solid;
            this.btnDecrypt.AnimationSpeed = 0.8F;
            this.btnDecrypt.BackColor = System.Drawing.Color.Transparent;
            this.btnDecrypt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnDecrypt.CornerRadius = 6;
            this.btnDecrypt.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnDecrypt.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnDecrypt.HoverColor = System.Drawing.Color.Green;
            this.btnDecrypt.InsideColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(17)))), ((int)(((byte)(18)))));
            this.btnDecrypt.Location = new System.Drawing.Point(20, 188);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.ShowToolTip = false;
            this.btnDecrypt.Size = new System.Drawing.Size(573, 36);
            this.btnDecrypt.TabIndex = 8;
            this.btnDecrypt.Text = "Decrypt / Decompress";
            this.btnDecrypt.TextColor = System.Drawing.Color.White;
            this.btnDecrypt.TextHoverColor = System.Drawing.Color.White;
            this.btnDecrypt.ToolTipIcon = "";
            this.btnDecrypt.ToolTipMessage = "";
            // 
            // progDecrypt
            // 
            this.progDecrypt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progDecrypt.ForeColor = System.Drawing.Color.Green;
            this.progDecrypt.Location = new System.Drawing.Point(20, 228);
            this.progDecrypt.Name = "progDecrypt";
            this.progDecrypt.Size = new System.Drawing.Size(573, 4);
            this.progDecrypt.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progDecrypt.TabIndex = 9;
            this.progDecrypt.Visible = false;
            // 
            // pnlMain
            // 
            this.pnlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMain.BackColor = System.Drawing.Color.Transparent;
            this.pnlMain.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(17)))), ((int)(((byte)(18)))));
            this.pnlMain.BorderThickness = 2;
            this.pnlMain.Controls.Add(this.pnlPatchView);
            this.pnlMain.CornerRadius = 8;
            this.pnlMain.CurveDensity = 90;
            this.pnlMain.FadeLength = 40;
            this.pnlMain.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.pnlMain.HorizontalLineLength = 0;
            this.pnlMain.Location = new System.Drawing.Point(20, 260);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.SecondaryBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(17)))), ((int)(((byte)(18)))));
            this.pnlMain.Size = new System.Drawing.Size(613, 349);
            this.pnlMain.TabIndex = 1;
            this.pnlMain.VerticalLineLength = 0;
            // 
            // pnlPatchView
            // 
            this.pnlPatchView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPatchView.BackColor = System.Drawing.Color.Transparent;
            this.pnlPatchView.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(62)))), ((int)(((byte)(72)))));
            this.pnlPatchView.BorderThickness = 0;
            this.pnlPatchView.Controls.Add(this.pnlPatches);
            this.pnlPatchView.Controls.Add(this.pnlTopBar);
            this.pnlPatchView.Controls.Add(this.pnlBottomBar);
            this.pnlBottomBar.Controls.Add(this.btnApply);
            this.pnlBottomBar.Controls.Add(this.progPatch);
            this.pnlTopBar.Controls.Add(this.lblGame);
            this.pnlTopBar.Controls.Add(this.cmbGame);
            this.pnlTopBar.Controls.Add(this.btnRefresh);
            this.pnlTopBar.Controls.Add(this.lblGameStatus);
            this.pnlPatchView.CornerRadius = 8;
            this.pnlPatchView.CurveDensity = 90;
            this.pnlPatchView.FadeLength = 40;
            this.pnlPatchView.FillColor = System.Drawing.Color.Transparent;
            this.pnlPatchView.HorizontalLineLength = 0;
            this.pnlPatchView.Location = new System.Drawing.Point(0, 0);
            this.pnlPatchView.Name = "pnlPatchView";
            this.pnlPatchView.SecondaryBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(52)))));
            this.pnlPatchView.Size = new System.Drawing.Size(613, 349);
            this.pnlPatchView.TabIndex = 0;
            this.pnlPatchView.VerticalLineLength = 0;
            // 
            // pnlBottomBar
            // 
            this.pnlBottomBar.BackColor = System.Drawing.Color.Transparent;
            this.pnlBottomBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottomBar.Location = new System.Drawing.Point(0, 301);
            this.pnlBottomBar.MinimumSize = new System.Drawing.Size(0, 48);
            this.pnlBottomBar.Name = "pnlBottomBar";
            this.pnlBottomBar.Size = new System.Drawing.Size(613, 48);
            this.pnlBottomBar.TabIndex = 10;
            // 
            // pnlTopBar
            // 
            this.pnlTopBar.BackColor = System.Drawing.Color.Transparent;
            this.pnlTopBar.Controls.Add(this.lblGame);
            this.pnlTopBar.Controls.Add(this.cmbGame);
            this.pnlTopBar.Controls.Add(this.btnSearchPatches);
            this.pnlTopBar.Controls.Add(this.btnRefresh);
            this.pnlTopBar.Controls.Add(this.lblGameStatus);
            this.pnlTopBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopBar.Location = new System.Drawing.Point(0, 0);
            this.pnlTopBar.MinimumSize = new System.Drawing.Size(0, 96);
            this.pnlTopBar.Name = "pnlTopBar";
            this.pnlTopBar.Size = new System.Drawing.Size(613, 96);
            this.pnlTopBar.TabIndex = 9;
            // 
            // lblGame
            // 
            this.lblGame.BackColor = System.Drawing.Color.Transparent;
            this.lblGame.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGame.ForeColor = System.Drawing.Color.White;
            this.lblGame.Location = new System.Drawing.Point(20, 16);
            this.lblGame.Name = "lblGame";
            this.lblGame.Size = new System.Drawing.Size(60, 22);
            this.lblGame.TabIndex = 0;
            this.lblGame.Text = "Game";
            // 
            // cmbGame
            // 
            this.cmbGame.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbGame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(17)))), ((int)(((byte)(18)))));
            this.cmbGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbGame.ForeColor = System.Drawing.Color.White;
            this.cmbGame.FormattingEnabled = true;
            this.cmbGame.Location = new System.Drawing.Point(20, 42);
            this.cmbGame.Name = "cmbGame";
            this.cmbGame.Size = new System.Drawing.Size(433, 21);
            this.cmbGame.TabIndex = 1;
            // 
            // btnSearchPatches
            // 
            this.btnSearchPatches.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchPatches.AnimationFillDirection = CustomControls.AnimatedButton.FillDirection.LeftToRight;
            this.btnSearchPatches.AnimationFillStyle = CustomControls.AnimatedButton.FillStyle.Solid;
            this.btnSearchPatches.AnimationSpeed = 0.8F;
            this.btnSearchPatches.BackColor = System.Drawing.Color.Transparent;
            this.btnSearchPatches.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnSearchPatches.CornerRadius = 6;
            this.btnSearchPatches.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnSearchPatches.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSearchPatches.HoverColor = System.Drawing.Color.Green;
            this.btnSearchPatches.InsideColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(17)))), ((int)(((byte)(18)))));
            this.btnSearchPatches.Location = new System.Drawing.Point(457, 40);
            this.btnSearchPatches.Name = "btnSearchPatches";
            this.btnSearchPatches.ShowToolTip = false;
            this.btnSearchPatches.Size = new System.Drawing.Size(88, 28);
            this.btnSearchPatches.TabIndex = 4;
            this.btnSearchPatches.Text = "Buscar patches";
            this.btnSearchPatches.TextColor = System.Drawing.Color.White;
            this.btnSearchPatches.TextHoverColor = System.Drawing.Color.White;
            this.btnSearchPatches.ToolTipIcon = "";
            this.btnSearchPatches.ToolTipMessage = "";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.AnimationFillDirection = CustomControls.AnimatedButton.FillDirection.LeftToRight;
            this.btnRefresh.AnimationFillStyle = CustomControls.AnimatedButton.FillStyle.Solid;
            this.btnRefresh.AnimationSpeed = 0.8F;
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnRefresh.CornerRadius = 6;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnRefresh.HoverColor = System.Drawing.Color.Green;
            this.btnRefresh.InsideColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(17)))), ((int)(((byte)(18)))));
            this.btnRefresh.Location = new System.Drawing.Point(549, 40);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.ShowToolTip = false;
            this.btnRefresh.Size = new System.Drawing.Size(44, 28);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "↻";
            this.btnRefresh.TextColor = System.Drawing.Color.White;
            this.btnRefresh.TextHoverColor = System.Drawing.Color.White;
            this.btnRefresh.ToolTipIcon = "";
            this.btnRefresh.ToolTipMessage = "";
            // 
            // lblGameStatus
            // 
            this.lblGameStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGameStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblGameStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblGameStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(148)))), ((int)(((byte)(158)))));
            this.lblGameStatus.Location = new System.Drawing.Point(20, 72);
            this.lblGameStatus.Name = "lblGameStatus";
            this.lblGameStatus.Size = new System.Drawing.Size(573, 20);
            this.lblGameStatus.TabIndex = 3;
            // 
            // pnlPatches
            // 
            this.pnlPatches.AutoScroll = true;
            this.pnlPatches.BackColor = System.Drawing.Color.Transparent;
            this.pnlPatches.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(17)))), ((int)(((byte)(18)))));
            this.pnlPatches.BorderThickness = 0;
            this.pnlPatches.CornerRadius = 6;
            this.pnlPatches.CurveDensity = 90;
            this.pnlPatches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPatches.FadeLength = 40;
            this.pnlPatches.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.pnlPatches.HorizontalLineLength = 0;
            this.pnlPatches.Location = new System.Drawing.Point(0, 96);
            this.pnlPatches.Name = "pnlPatches";
            this.pnlPatches.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.pnlPatches.SecondaryBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(17)))), ((int)(((byte)(18)))));
            this.pnlPatches.Size = new System.Drawing.Size(613, 205);
            this.pnlPatches.TabIndex = 4;
            this.pnlPatches.VerticalLineLength = 0;
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.AnimationFillDirection = CustomControls.AnimatedButton.FillDirection.LeftToRight;
            this.btnApply.AnimationFillStyle = CustomControls.AnimatedButton.FillStyle.Solid;
            this.btnApply.AnimationSpeed = 0.8F;
            this.btnApply.BackColor = System.Drawing.Color.Transparent;
            this.btnApply.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnApply.CornerRadius = 6;
            this.btnApply.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnApply.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnApply.HoverColor = System.Drawing.Color.Green;
            this.btnApply.InsideColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(17)))), ((int)(((byte)(18)))));
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Location = new System.Drawing.Point(20, 6);
            this.btnApply.Name = "btnApply";
            this.btnApply.ShowToolTip = false;
            this.btnApply.Size = new System.Drawing.Size(573, 32);
            this.btnApply.TabIndex = 5;
            this.btnApply.Text = "Apply Selected Patches";
            this.btnApply.TextColor = System.Drawing.Color.White;
            this.btnApply.TextHoverColor = System.Drawing.Color.White;
            this.btnApply.ToolTipIcon = "";
            this.btnApply.ToolTipMessage = "";
            // 
            // progPatch
            // 
            this.progPatch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progPatch.Location = new System.Drawing.Point(20, 42);
            this.progPatch.Name = "progPatch";
            this.progPatch.Size = new System.Drawing.Size(573, 6);
            this.progPatch.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progPatch.TabIndex = 7;
            this.progPatch.Visible = false;
            // 
            // FrameX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.Controls.Add(this.pnlFile);
            this.Controls.Add(this.pnlMain);
            this.Name = "FrameX";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 20, 20);
            this.Size = new System.Drawing.Size(653, 629);
            this.pnlFile.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlPatchView.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private FadePanel pnlFile;
        private CustomLabel lblXex;
        private AdvancedTextBox txXex;
        private CustomControls.AnimatedButton btnBrowse;
        private CustomLabel lblOut;
        private AdvancedTextBox txOutput;
        private CustomLabel lblTid;
        private AdvancedTextBox txTitleId;
        private CustomLabel lblTidFmt;
        private CustomControls.AnimatedButton btnDecrypt;
        private FadePanel pnlMain;
        private FadePanel pnlPatchView;
        private System.Windows.Forms.Panel pnlBottomBar;
        private System.Windows.Forms.Panel pnlTopBar;
        private CustomLabel lblGame;
        private System.Windows.Forms.ComboBox cmbGame;
        private CustomControls.AnimatedButton btnRefresh;
        private FadePanel pnlPatches;
        private CustomControls.AnimatedButton btnApply;
        private System.Windows.Forms.ProgressBar progPatch;
        private System.Windows.Forms.ProgressBar progDecrypt;
        private CustomLabel lblGameStatus;
        private CustomControls.AnimatedButton btnSearchPatches;
    }
}
