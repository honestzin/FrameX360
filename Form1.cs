using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace FrameX360
{
    public partial class Form1 : Form
    {
        private bool _frameXLoaded;
        private int _lastTabIndex = 0;
        private readonly Control[] _tabPanels;
        private const int ContentWidth = 653;
        private readonly System.Windows.Forms.Timer _slideTimer;
        private float _animT;
        private const int AnimSteps = 24;
        private const int AnimIntervalMs = 12;
        private Control _animOut;
        private Control _animIn;
        private int _targetTabIndex = -1;

        public Form1()
        {
            InitializeComponent();
            SetDoubleBuffered(this);
            SetDoubleBuffered(pnlContent);
            _tabPanels = new Control[] { home1, frameX1, about1, credits1 };
            foreach (var p in _tabPanels)
                SetDoubleBuffered(p);
            TabVertical.SelectedIndexChanged += TabVertical_SelectedIndexChanged;
            _slideTimer = new System.Windows.Forms.Timer { Interval = AnimIntervalMs };
            _slideTimer.Tick += SlideTimer_Tick;

            AppLanguage.LanguageChanged += OnLanguageChanged;
            AppLanguage.Initialize();
            UpdateTabTexts();
            ApplyLangButtons();
        }

        static void SetDoubleBuffered(Control c)
        {
            try
            {
                typeof(Control).InvokeMember("DoubleBuffered",
                    BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                    null, c, new object[] { true });
            }
            catch { }
        }

        private void OnLanguageChanged(object sender, EventArgs e)
        {
            UpdateTabTexts();
            home1.ApplyLanguage();
            frameX1.ApplyLanguage();
            about1.ApplyLanguage();
            credits1.ApplyLanguage();
            _tabPanels[_lastTabIndex].Refresh();
            TabVertical.Invalidate();
            pnlContent.Invalidate();
        }

        private void _btnLangEn_Click(object sender, EventArgs e)
        {
            AppLanguage.Set("en");
            ApplyLangButtons();
        }

        private void _btnLangPt_Click(object sender, EventArgs e)
        {
            AppLanguage.Set("pt");
            ApplyLangButtons();
        }

        private void ApplyLangButtons()
        {
            bool en = AppLanguage.Current == "en";
            _btnLangEn.BackColor = Color.FromArgb(en ? 48 : 24, en ? 48 : 24, en ? 48 : 24);
            _btnLangEn.ForeColor = Color.White;
            _btnLangPt.BackColor = Color.FromArgb(en ? 24 : 48, en ? 24 : 48, en ? 24 : 48);
            _btnLangPt.ForeColor = en ? Color.FromArgb(139, 148, 158) : Color.White;
        }

        private void UpdateTabTexts()
        {
            bool en = AppLanguage.Current == "en";
            TabVertical.Tab1Text = en ? "Home" : "Início";
            TabVertical.Tab2Text = "FrameX";
            TabVertical.Tab3Text = en ? "About" : "Sobre";
            TabVertical.Tab4Text = en ? "Credits" : "Créditos";
        }

        private static float EaseOutCubic(float t)
        {
            float u = 1f - t;
            return 1f - (u * u * u);
        }

        private void TabVertical_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = TabVertical.SelectedIndex;
            if (idx == _lastTabIndex) return;

            _targetTabIndex = idx;
            _animOut = _tabPanels[_lastTabIndex];
            _animIn = _tabPanels[idx];

            _animIn.Location = new Point(ContentWidth, 0);
            _animIn.Visible = true;
            _animOut.BringToFront();
            _animIn.BringToFront();
            _animT = 0f;
            _slideTimer.Start();
        }

        private void SlideTimer_Tick(object sender, EventArgs e)
        {
            _animT += 1f / AnimSteps;
            if (_animT >= 1f)
            {
                _slideTimer.Stop();
                _animOut.Visible = false;
                _animOut.Location = new Point(0, 0);
                _animIn.Location = new Point(0, 0);
                _lastTabIndex = _targetTabIndex;
                if (_lastTabIndex == 1 && !_frameXLoaded)
                {
                    _frameXLoaded = true;
                    frameX1.OnLoad();
                }
                return;
            }
            float t = EaseOutCubic(_animT);
            int outX = -(int)(ContentWidth * t);
            int inX = ContentWidth - (int)(ContentWidth * t);
            _animOut.Location = new Point(outX, 0);
            _animIn.Location = new Point(inX, 0);
            pnlContent.Invalidate();
        }

        public void SelectTab(int index)
        {
            const int tabCount = 4;
            if (index >= 0 && index < tabCount)
                TabVertical.SelectedIndex = index;
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
