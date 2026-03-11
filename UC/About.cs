using System.Windows.Forms;
using FrameX360;

namespace FrameX360.UC
{
    public partial class About : UserControl
    {
        public About()
        {
            InitializeComponent();
            AppLanguage.LanguageChanged += (s, ev) => ApplyLanguage();
        }

        public void ApplyLanguage()
        {
            bool en = AppLanguage.Current == "en";
            lblTitle.Text = "FrameX360";
            lblDesc.Text = en
                ? "Xbox 360 XEX Patcher — decrypt and apply patches to XEX executables for RGH/JTAG."
                : "Patcher de XEX para Xbox 360 — descriptografar e aplicar patches em executáveis XEX para RGH/JTAG.";
            lblVersion.Text = en ? "Version 1.0 — by @honest" : "Versão 1.0 — por @honest";
        }
    }
}
