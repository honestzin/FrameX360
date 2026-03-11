using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class VerticalImageTabControl : Control
{
    private int selectedIndex = 0;
    private readonly List<Image> tabs;
    private readonly List<Size> tabImageSizes;
    private readonly List<string> tabTexts;
    private int cornerRadius = 8;
    private int spacing = 5;
    private int _pressedIndex = -1;
    private readonly System.Windows.Forms.Timer _pressTimer;

    public event EventHandler SelectedIndexChanged;
    public event EventHandler Tab1Clicked, Tab2Clicked, Tab3Clicked, Tab4Clicked;

    public VerticalImageTabControl()
    {
        int tabCount = 4;
        tabs = new List<Image>(new Image[tabCount]);
        tabImageSizes = new List<Size>();
        tabTexts = new List<string>();

        for (int i = 0; i < tabCount; i++)
        {
            tabImageSizes.Add(new Size(24, 24));
            tabTexts.Add($"Tab {i + 1}");
        }

        DoubleBuffered = true;
        Width = 140;
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
        Cursor = Cursors.Default;
        _pressTimer = new System.Windows.Forms.Timer { Interval = 120 };
        _pressTimer.Tick += (s, ev) => { _pressTimer.Stop(); _pressedIndex = -1; Invalidate(); };
        SelectedIndex = 0;
    }

    [Category("Behavior")]
    public int SelectedIndex
    {
        get => selectedIndex;
        set
        {
            if (value != selectedIndex && value >= 0 && value < tabs.Count)
            {
                selectedIndex = value;
                Invalidate();
                SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    [Category("Appearance")] public Color SlideColor { get; set; } = Color.DodgerBlue;
    [Category("Appearance")] public Color BackgroundColor { get; set; } = Color.FromArgb(240, 240, 240);
    [Category("Appearance")] public Color TextColor { get; set; } = Color.Black;

    [Category("Appearance")]
    public int CornerRadius
    {
        get => cornerRadius;
        set { cornerRadius = Math.Max(0, value); Invalidate(); }
    }

    [Category("Layout")]
    public int Spacing
    {
        get => spacing;
        set { spacing = Math.Max(0, value); Invalidate(); }
    }

    [Category("Images")] public Image Tab1Image { get => tabs[0]; set { tabs[0] = value; Invalidate(); } }
    public Image Tab2Image { get => tabs[1]; set { tabs[1] = value; Invalidate(); } }
    public Image Tab3Image { get => tabs[2]; set { tabs[2] = value; Invalidate(); } }
    public Image Tab4Image { get => tabs[3]; set { tabs[3] = value; Invalidate(); } }

    public string Tab1Text { get => tabTexts[0]; set { tabTexts[0] = value; Invalidate(); } }
    public string Tab2Text { get => tabTexts[1]; set { tabTexts[1] = value; Invalidate(); } }
    public string Tab3Text { get => tabTexts[2]; set { tabTexts[2] = value; Invalidate(); } }
    public string Tab4Text { get => tabTexts[3]; set { tabTexts[3] = value; Invalidate(); } }

    public Size Tab1ImageSize { get => tabImageSizes[0]; set { tabImageSizes[0] = value; Invalidate(); } }
    public Size Tab2ImageSize { get => tabImageSizes[1]; set { tabImageSizes[1] = value; Invalidate(); } }
    public Size Tab3ImageSize { get => tabImageSizes[2]; set { tabImageSizes[2] = value; Invalidate(); } }
    public Size Tab4ImageSize { get => tabImageSizes[3]; set { tabImageSizes[3] = value; Invalidate(); } }

    public void MoveTabContent(int from, int to)
    {
        if (from == to) return;
        if (from < 0 || from >= tabs.Count) return;
        if (to < 0 || to >= tabs.Count) return;

        var img = tabs[from];
        tabs[from] = tabs[to];
        tabs[to] = img;

        var sz = tabImageSizes[from];
        tabImageSizes[from] = tabImageSizes[to];
        tabImageSizes[to] = sz;

        var tx = tabTexts[from];
        tabTexts[from] = tabTexts[to];
        tabTexts[to] = tx;

        Invalidate();
    }

    private int GetSegmentHeight() => (Height - (spacing * (tabs.Count - 1))) / tabs.Count;

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        g.CompositingQuality = CompositingQuality.HighQuality;
        g.Clear(BackgroundColor);

        int segmentHeight = GetSegmentHeight();
        RectangleF selectedRect = new RectangleF(0, selectedIndex * (segmentHeight + spacing), Width, segmentHeight);

        using (GraphicsPath path = CreateTabPath(selectedRect))
        using (SolidBrush brush = new SolidBrush(SlideColor))
        {
            g.FillPath(brush, path);
        }

        for (int i = 0; i < tabs.Count; i++)
        {
            RectangleF tabRect = new RectangleF(0, i * (segmentHeight + spacing), Width, segmentHeight);
            if (_pressedIndex == i)
            {
                using (var path = CreateTabPath(tabRect))
                using (var brush = new SolidBrush(Color.FromArgb(50, 255, 255, 255)))
                    g.FillPath(brush, path);
            }
        }

        using (Font font = new Font("Inter", 10f))
        using (Brush textBrush = new SolidBrush(TextColor))
        {
            for (int i = 0; i < tabs.Count; i++)
            {
                RectangleF tabRect = new RectangleF(0, i * (segmentHeight + spacing), Width, segmentHeight);
                Size imageSize = tabImageSizes[i];

                int imgX = (int)(tabRect.X + 10);
                int imgY = (int)(tabRect.Y + (tabRect.Height - imageSize.Height) / 2f);

                if (tabs[i] != null)
                    g.DrawImage(tabs[i], new Rectangle(imgX, imgY, imageSize.Width, imageSize.Height));

                string text = tabTexts[i];
                SizeF textSize = g.MeasureString(text, font);
                int txtX = imgX + imageSize.Width + 6;
                int txtY = (int)(tabRect.Y + (tabRect.Height - textSize.Height) / 2f) + 2;

                g.DrawString(text, font, textBrush, txtX, txtY);
            }
        }
    }

    private GraphicsPath CreateTabPath(RectangleF rect)
    {
        float r = cornerRadius;
        GraphicsPath path = new GraphicsPath();
        path.AddArc(rect.X, rect.Y, r, r, 180, 90);
        path.AddArc(rect.Right - r, rect.Y, r, r, 270, 90);
        path.AddArc(rect.Right - r, rect.Bottom - r, r, r, 0, 90);
        path.AddArc(rect.X, rect.Bottom - r, r, r, 90, 90);
        path.CloseFigure();
        return path;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        int segmentHeight = GetSegmentHeight();
        for (int i = 0; i < tabs.Count; i++)
        {
            Rectangle tabRect = new Rectangle(0, i * (segmentHeight + spacing), Width, segmentHeight);
            if (tabRect.Contains(e.Location))
            {
                _pressedIndex = i;
                _pressTimer.Stop();
                _pressTimer.Start();
                Invalidate();
                SelectedIndex = i;
                switch (i)
                {
                    case 0: Tab1Clicked?.Invoke(this, EventArgs.Empty); break;
                    case 1: Tab2Clicked?.Invoke(this, EventArgs.Empty); break;
                    case 2: Tab3Clicked?.Invoke(this, EventArgs.Empty); break;
                    case 3: Tab4Clicked?.Invoke(this, EventArgs.Empty); break;
                }
                break;
            }
        }
    }

    protected override void OnMouseMove(MouseEventArgs e) => base.OnMouseMove(e);
    protected override void OnMouseLeave(EventArgs e) => base.OnMouseLeave(e);
}
