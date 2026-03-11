using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class CustomControlBox : Control
{
    public enum ControlBoxTypeEnum { Close, Minimize }

    private Color fillColor = Color.FromArgb(18, 18, 20);
    private Color iconColor = Color.FromArgb(100, 100, 100);
    private Color hoverIconColor = Color.DarkSlateBlue;
    private bool useTransparentBackground = false;
    private int borderRadius = 0;
    private float simbolTickness = 2f;
    private ControlBoxTypeEnum controlBoxType = ControlBoxTypeEnum.Close;

    private float fadeProgress = 0f;
    private Timer fadeTimer;
    private bool isHovering = false;

    [Category("Custom")]
    public int BorderRadius
    {
        get => borderRadius;
        set { borderRadius = value < 0 ? 0 : value; Invalidate(); }
    }

    [Category("Custom")]
    public Color FillColor { get => fillColor; set { fillColor = value; Invalidate(); } }

    [Category("Custom")]
    public Color IconColor { get => iconColor; set { iconColor = value; Invalidate(); } }

    [Category("Custom")]
    public Color HoverIconColor { get => hoverIconColor; set { hoverIconColor = value; Invalidate(); } }

    [Category("Custom")]
    public bool UseTransparentBackground { get => useTransparentBackground; set { useTransparentBackground = value; Invalidate(); } }

    [Category("Custom")]
    public float SimbolTickness { get => simbolTickness; set { simbolTickness = value; Invalidate(); } }

    [Category("Custom")]
    public ControlBoxTypeEnum ControlBoxType { get => controlBoxType; set { controlBoxType = value; Invalidate(); } }

    public CustomControlBox()
    {
        this.Size = new Size(40, 30);
        this.Cursor = Cursors.Default;
        this.DoubleBuffered = true;

        fadeTimer = new Timer();
        fadeTimer.Interval = 15;
        fadeTimer.Tick += FadeTimer_Tick;

        this.MouseEnter += (s, e) => { isHovering = true; fadeTimer.Start(); };
        this.MouseLeave += (s, e) => { isHovering = false; fadeTimer.Start(); };
        this.Click += (s, e) =>
        {
            if (controlBoxType == ControlBoxTypeEnum.Close)
                this.FindForm()?.Close();
            else if (controlBoxType == ControlBoxTypeEnum.Minimize)
                this.FindForm().WindowState = FormWindowState.Minimized;
        };
    }

    private void FadeTimer_Tick(object sender, EventArgs e)
    {
        float step = 0.08f;
        if (isHovering && fadeProgress < 1f)
        {
            fadeProgress += step;
            if (fadeProgress > 1f) fadeProgress = 1f;
            Invalidate();
        }
        else if (!isHovering && fadeProgress > 0f)
        {
            fadeProgress -= step;
            if (fadeProgress < 0f) fadeProgress = 0f;
            Invalidate();
        }
        else
        {
            fadeTimer.Stop();
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        Graphics g = e.Graphics;
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.CompositingQuality = CompositingQuality.HighQuality;
        g.CompositingMode = CompositingMode.SourceOver;
        g.PixelOffsetMode = PixelOffsetMode.None;

        GraphicsPath path = RoundedRect(this.ClientRectangle, borderRadius);
        this.Region = new Region(path); 

        if (!UseTransparentBackground)
        {
            using (SolidBrush b = new SolidBrush(FillColor))
                g.FillPath(b, path);
        }

        Color currentColor = InterpolateColor(IconColor, HoverIconColor, fadeProgress);

        using (Pen p = new Pen(currentColor, SimbolTickness))
        {
            int margin = 10;
            if (ControlBoxType == ControlBoxTypeEnum.Close)
            {
                g.DrawLine(p, margin, margin, Width - margin, Height - margin);
                g.DrawLine(p, Width - margin, margin, margin, Height - margin);
            }
            else if (ControlBoxType == ControlBoxTypeEnum.Minimize)
            {
                g.DrawLine(p, margin, Height - margin - 2, Width - margin, Height - margin - 2);
            }
        }
    }

    private GraphicsPath RoundedRect(Rectangle bounds, int radius)
    {
        GraphicsPath path = new GraphicsPath();
        if (radius <= 0)
        {
            path.AddRectangle(bounds);
            return path;
        }

        int diameter = radius * 2;
        Rectangle arcRect = new Rectangle(bounds.Location, new Size(diameter, diameter));

        path.AddArc(arcRect, 180, 90);

        arcRect.X = bounds.Right - diameter;
        path.AddArc(arcRect, 270, 90);

        arcRect.Y = bounds.Bottom - diameter;
        path.AddArc(arcRect, 0, 90);

        arcRect.X = bounds.Left;
        path.AddArc(arcRect, 90, 90);

        path.CloseFigure();
        return path;
    }

    private Color InterpolateColor(Color from, Color to, float t)
    {
        return Color.FromArgb(
            (int)(from.A + (to.A - from.A) * t),
            (int)(from.R + (to.R - from.R) * t),
            (int)(from.G + (to.G - from.G) * t),
            (int)(from.B + (to.B - from.B) * t)
        );
    }
}
