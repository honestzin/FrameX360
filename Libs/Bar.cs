using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

public class SeparatorGradient : Panel
{
    private Color borderColor = Color.Red;
    private int borderThickness = 2;
    private int trailWidth = 200;
    private int animationSpeed = 10;
    private float offset;
    private bool running = true;

    [Category("Custom")]
    public Color BorderColor
    {
        get => borderColor;
        set { borderColor = value; Invalidate(); }
    }

    [Category("Custom")]
    public int BorderThickness
    {
        get => borderThickness;
        set { borderThickness = Math.Max(1, value); Invalidate(); }
    }

    [Category("Custom")]
    public int TrailWidth
    {
        get => trailWidth;
        set { trailWidth = Math.Max(20, value); Invalidate(); }
    }

    [Category("Custom")]
    public int AnimationSpeed
    {
        get => animationSpeed;
        set { animationSpeed = Math.Max(1, Math.Min(value, 20)); }
    }
    private Thread animationThread;
    private bool isRunning = true;

    public SeparatorGradient()
    {
        DoubleBuffered = true;
        ResizeRedraw = true;
        BackColor = Color.FromArgb(15, 15, 15);

        offset = Width;

        animationThread = new Thread(() =>
        {
            while (isRunning)
            {
                offset -= animationSpeed;

                if (offset + trailWidth <= 0)
                    offset = Width;

                if (!IsDisposed && IsHandleCreated)
                {
                    Invoke((MethodInvoker)(() => Invalidate()));
                }

                Thread.Sleep(16); // ~60fps
            }
        });

        animationThread.IsBackground = true;
        animationThread.Start();
    }

    private async void RunAnimationLoop()
    {
        while (running && !IsDisposed)
        {
            offset -= animationSpeed;
            if (offset + trailWidth <= 0)
                offset = Width;

            if (IsHandleCreated)
                BeginInvoke((MethodInvoker)(() => Invalidate()));

            await Task.Delay(16); // ~60 FPS
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        var g = e.Graphics;

        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.CompositingQuality = CompositingQuality.HighQuality;
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;

        if (Width <= 0 || borderThickness <= 0)
            return;

        float centerY = Height / 2f;
        DrawMainTrail(g, offset, centerY);
    }

    private void DrawMainTrail(Graphics g, float posX, float y)
    {
        float startX = posX;
        float endX = posX + trailWidth;
        float drawStartX = Math.Max(startX, 0);
        float drawEndX = Math.Min(endX, Width);

        if (drawStartX >= drawEndX)
            return;

        using (var brush = CreateGradientBrush(startX, endX))
        using (var pen = new Pen(brush, borderThickness))
        {
            pen.StartCap = LineCap.Flat;
            pen.EndCap = LineCap.Flat;
            g.DrawLine(pen, drawStartX, y, drawEndX, y);
        }
    }

    private LinearGradientBrush CreateGradientBrush(float startX, float endX)
    {
        RectangleF rect = new RectangleF(startX, 0, endX - startX, Height);
        var brush = new LinearGradientBrush(rect, borderColor, Color.Transparent, LinearGradientMode.Horizontal);

        brush.InterpolationColors = new ColorBlend
        {
            Colors = new[]
            {
                Color.FromArgb(255, borderColor),
                Color.FromArgb(180, borderColor),
                Color.FromArgb(120, borderColor),
                Color.FromArgb(70, borderColor),
                Color.FromArgb(30, borderColor),
                Color.FromArgb(10, borderColor),
                Color.FromArgb(0, borderColor)
            },
            Positions = new[] { 0f, 0.15f, 0.35f, 0.55f, 0.75f, 0.9f, 1f }
        };

        return brush;
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        if (offset + trailWidth <= 0)
            offset = Width;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            isRunning = false;
            animationThread?.Join();
            animationThread?.Abort();
        }

        base.Dispose(disposing);
    }
}
