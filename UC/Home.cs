using System;
using System.Windows.Forms;
using FrameX360;

namespace FrameX360.UC
{
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
            AppLanguage.LanguageChanged += (s, ev) => ApplyLanguage();
        }

        public void ApplyLanguage()
        {
            bool en = AppLanguage.Current == "en";
            lblWelcomeTitle.Text = en ? "Welcome to FrameX360" : "Bem-vindo ao FrameX360";
            lblWelcomeDesc.Text = en
                ? "Tool to apply patches to XEX (Xbox 360). Use FrameX to decrypt and apply FPS and compatibility patches. Patches can come from the local repository or Xenia."
                : "Ferramenta para aplicar patches em XEX (Xbox 360). Use FrameX para descriptografar e aplicar patches de FPS e compatibilidade. Os patches podem vir do repositório local ou do Xenia.";
            lblFeaturesTitle.Text = en ? "Features" : "Recursos";
            lblFeature1.Text = en
                ? "• Decrypt and decompress XEX (RGH/JTAG)"
                : "• Descriptografar e descomprimir XEX (RGH/JTAG)";
            lblFeature2.Text = en
                ? "• FPS and compatibility patches (local and Xenia)"
                : "• Patches de FPS e compatibilidade (local e Xenia)";
            lblFeature3.Text = en
                ? "• Search by name on GitHub and pattern scan in XEX file"
                : "• Busca por nome no GitHub e scan de padrões no arquivo XEX";
            lblVersion.Text = "FrameX360 — by @honest";
        }
    }
}
