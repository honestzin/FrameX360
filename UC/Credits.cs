using System.Windows.Forms;
using FrameX360;

namespace FrameX360.UC
{
    public partial class Credits : UserControl
    {
        public Credits()
        {
            InitializeComponent();
            AppLanguage.LanguageChanged += (s, ev) => ApplyLanguage();
        }

        public void ApplyLanguage()
        {
            bool en = AppLanguage.Current == "en";
            lblTitle.Text = en ? "Credits" : "Créditos";
            lblCredits.Text = en
                ? "FrameX360 — Developed by @honest\n\nXEX patching logic and built-in patches.\nDesign and UI framework (Libs)."
                : "FrameX360 — Desenvolvido por @honest\n\nLógica de patches XEX e patches internos.\nDesign e framework de UI (Libs).";
        }
    }
}
