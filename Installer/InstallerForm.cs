using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
// using IWshRuntimeLibrary; // COM reference for shortcuts

namespace FrameX360Installer
{
    public partial class InstallerForm : Form
    {
        private const string INSTALL_DIR = @"C:\FrameX360";
        private const string APP_NAME = "FrameX360";
        private string _lang = "en";

        private Label _lblTitle;
        private Label _lblInstallTo;
        private TextBox _txtInstallPath;
        private Button _btnBrowse;
        private CheckBox _chkDesktop;
        private ProgressBar _progressBar;
        private Label _lblStatus;
        private Button _btnInstall;
        private Button _btnCancel;
        private Button _btnLang;

        public InstallerForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "FrameX360 Setup";
            this.Size = new System.Drawing.Size(500, 350);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            _lblTitle = new Label
            {
                Text = "FrameX360 Setup",
                Font = new System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold),
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(450, 30),
                AutoSize = false
            };

            _lblInstallTo = new Label
            {
                Text = "Install to:",
                Location = new System.Drawing.Point(20, 70),
                Size = new System.Drawing.Size(100, 23)
            };

            _txtInstallPath = new TextBox
            {
                Text = INSTALL_DIR,
                Location = new System.Drawing.Point(20, 95),
                Size = new System.Drawing.Size(350, 23),
                ReadOnly = true
            };

            _btnBrowse = new Button
            {
                Text = "Browse...",
                Location = new System.Drawing.Point(380, 94),
                Size = new System.Drawing.Size(80, 25)
            };
            _btnBrowse.Click += (s, e) => BrowseFolder();

            _chkDesktop = new CheckBox
            {
                Text = "Create desktop shortcut",
                Checked = true,
                Location = new System.Drawing.Point(20, 130),
                Size = new System.Drawing.Size(200, 23)
            };

            _progressBar = new ProgressBar
            {
                Location = new System.Drawing.Point(20, 170),
                Size = new System.Drawing.Size(440, 23),
                Style = ProgressBarStyle.Continuous
            };

            _lblStatus = new Label
            {
                Text = "Ready to install.",
                Location = new System.Drawing.Point(20, 200),
                Size = new System.Drawing.Size(440, 23)
            };

            _btnInstall = new Button
            {
                Text = "Install",
                Location = new System.Drawing.Point(280, 240),
                Size = new System.Drawing.Size(85, 30),
                DialogResult = DialogResult.OK
            };
            _btnInstall.Click += BtnInstall_Click;

            _btnCancel = new Button
            {
                Text = "Cancel",
                Location = new System.Drawing.Point(375, 240),
                Size = new System.Drawing.Size(85, 30),
                DialogResult = DialogResult.Cancel
            };

            _btnLang = new Button
            {
                Text = "PT-BR",
                Location = new System.Drawing.Point(20, 240),
                Size = new System.Drawing.Size(60, 30)
            };
            _btnLang.Click += (s, e) =>
            {
                _lang = _lang == "en" ? "pt" : "en";
                UpdateLanguage();
            };

            this.Controls.AddRange(new Control[]
            {
                _lblTitle, _lblInstallTo, _txtInstallPath, _btnBrowse,
                _chkDesktop, _progressBar, _lblStatus, _btnInstall, _btnCancel, _btnLang
            });
        }

        private void UpdateLanguage()
        {
            _btnLang.Text = _lang == "en" ? "PT-BR" : "EN";
            if (_lang == "pt")
            {
                this.Text = "Instalador FrameX360";
                _lblTitle.Text = "Instalador FrameX360";
                _lblInstallTo.Text = "Instalar em:";
                _btnBrowse.Text = "Procurar...";
                _chkDesktop.Text = "Criar atalho na área de trabalho";
                _btnInstall.Text = "Instalar";
                _btnCancel.Text = "Cancelar";
                _lblStatus.Text = "Pronto para instalar.";
            }
            else
            {
                this.Text = "FrameX360 Setup";
                _lblTitle.Text = "FrameX360 Setup";
                _lblInstallTo.Text = "Install to:";
                _btnBrowse.Text = "Browse...";
                _chkDesktop.Text = "Create desktop shortcut";
                _btnInstall.Text = "Install";
                _btnCancel.Text = "Cancel";
                _lblStatus.Text = "Ready to install.";
            }
        }

        private void BrowseFolder()
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.SelectedPath = _txtInstallPath.Text;
                if (dlg.ShowDialog() == DialogResult.OK)
                    _txtInstallPath.Text = dlg.SelectedPath;
            }
        }

        private void BtnInstall_Click(object sender, EventArgs e)
        {
            _btnInstall.Enabled = false;
            _btnCancel.Enabled = false;

            try
            {
                string installPath = _txtInstallPath.Text;
                string sourceDir = Path.GetDirectoryName(Application.ExecutablePath);
                string parentDir = Directory.GetParent(sourceDir)?.FullName ?? sourceDir;

                UpdateStatus(_lang == "pt" ? "Criando pasta..." : "Creating folder...", 10);
                Directory.CreateDirectory(installPath);

                UpdateStatus(_lang == "pt" ? "Copiando arquivos..." : "Copying files...", 30);
                CopyFile(parentDir, installPath, "FrameX360.exe");
                CopyFile(parentDir, installPath, "FrameX360.exe.config");
                CopyFile(parentDir, installPath, "xextool.exe");
                CopyFile(parentDir, installPath, "icon.ico");

                UpdateStatus(_lang == "pt" ? "Copiando patches..." : "Copying patches...", 60);
                string patchesSrc = Path.Combine(parentDir, "patches");
                string patchesDst = Path.Combine(installPath, "patches");
                if (Directory.Exists(patchesSrc))
                {
                    if (Directory.Exists(patchesDst))
                        Directory.Delete(patchesDst, true);
                    CopyDirectory(patchesSrc, patchesDst);
                }

                if (_chkDesktop.Checked)
                {
                    UpdateStatus(_lang == "pt" ? "Criando atalho..." : "Creating shortcut...", 85);
                    CreateDesktopShortcut(Path.Combine(installPath, "FrameX360.exe"));
                }

                UpdateStatus(_lang == "pt" ? "Instalação concluída!" : "Installation complete!", 100);
                MessageBox.Show(
                    _lang == "pt"
                        ? $"{APP_NAME} instalado em:\n{installPath}"
                        : $"{APP_NAME} installed to:\n{installPath}",
                    _lang == "pt" ? "Concluído" : "Done",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                Application.Exit();
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show(
                    _lang == "pt"
                        ? "Execute o instalador como Administrador."
                        : "Run the installer as Administrator.",
                    _lang == "pt" ? "Permissão negada" : "Permission denied",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                _btnInstall.Enabled = true;
                _btnCancel.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    _lang == "pt" ? "Erro" : "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                _btnInstall.Enabled = true;
                _btnCancel.Enabled = true;
            }
        }

        private void UpdateStatus(string text, int progress)
        {
            _lblStatus.Text = text;
            _progressBar.Value = progress;
            Application.DoEvents();
        }

        private void CopyFile(string sourceDir, string destDir, string fileName)
        {
            string src = Path.Combine(sourceDir, fileName);
            string dst = Path.Combine(destDir, fileName);
            if (File.Exists(src))
                File.Copy(src, dst, true);
        }

        private void CopyDirectory(string sourceDir, string destDir)
        {
            Directory.CreateDirectory(destDir);
            foreach (string file in Directory.GetFiles(sourceDir))
                File.Copy(file, Path.Combine(destDir, Path.GetFileName(file)), true);
            foreach (string dir in Directory.GetDirectories(sourceDir))
                CopyDirectory(dir, Path.Combine(destDir, Path.GetFileName(dir)));
        }

        private void CreateDesktopShortcut(string targetPath)
        {
            try
            {
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string shortcutPath = Path.Combine(desktop, "FrameX360.lnk");

                // Use COM interop for creating shortcuts
                Type shellType = Type.GetTypeFromProgID("WScript.Shell");
                object shell = Activator.CreateInstance(shellType);
                object shortcut = shellType.InvokeMember("CreateShortcut", System.Reflection.BindingFlags.InvokeMethod, null, shell, new object[] { shortcutPath });

                Type shortcutType = shortcut.GetType();
                shortcutType.InvokeMember("TargetPath", System.Reflection.BindingFlags.SetProperty, null, shortcut, new object[] { targetPath });
                shortcutType.InvokeMember("WorkingDirectory", System.Reflection.BindingFlags.SetProperty, null, shortcut, new object[] { Path.GetDirectoryName(targetPath) });
                shortcutType.InvokeMember("IconLocation", System.Reflection.BindingFlags.SetProperty, null, shortcut, new object[] { Path.Combine(Path.GetDirectoryName(targetPath), "icon.ico") });
                shortcutType.InvokeMember("Description", System.Reflection.BindingFlags.SetProperty, null, shortcut, new object[] { "FrameX360 - Xbox 360 XEX Patcher" });
                shortcutType.InvokeMember("Save", System.Reflection.BindingFlags.InvokeMethod, null, shortcut, null);
            }
            catch
            {
                // If shortcut creation fails, continue anyway
            }
        }
    }
}
