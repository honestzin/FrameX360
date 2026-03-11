using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class CustomSlider : Control
{
    public float Minimum { get; set; } = 0;
    public float Maximum { get; set; } = 100;
    public float Value { get; set; } = 50;

    public Color FillColor { get; set; } = Color.FromArgb(40, 40, 40);
    public Color ProgressColor { get; set; } = Color.FromArgb(90, 170, 250);
    public int CornerRadius { get; set; } = 3;

    public int TrackHeight { get; set; } = 10;

    private float animatedFill = 0f;
    private Timer animTimer;

    public CustomSlider()
    {
        DoubleBuffered = true;
        Height = 40;

        animTimer = new Timer { Interval = 10 };
        animTimer.Tick += (s, e) =>
        {
            float target = GetSliderPosition();
            animatedFill += (target - animatedFill) * 0.15f;
            if (Math.Abs(animatedFill - target) < 0.5f)
                animatedFill = target;

            Invalidate();
        };
        animTimer.Start();
    }

    private float GetSliderPosition()
    {
        float percent = (Value - Minimum) / (Maximum - Minimum);
        return percent * (Width - 40);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.CompositingQuality = CompositingQuality.HighQuality;
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        g.PixelOffsetMode = PixelOffsetMode.HighQuality;

        float trackY = Height / 2f - TrackHeight / 2f;
        RectangleF sliderArea = new RectangleF(20, trackY, Width - 40, TrackHeight);
        RectangleF progressArea = new RectangleF(sliderArea.X, sliderArea.Y, animatedFill, sliderArea.Height);

        using (SolidBrush fillBrush = new SolidBrush(FillColor))
            FillRoundedRect(g, fillBrush, sliderArea, CornerRadius);

        using (SolidBrush progressBrush = new SolidBrush(ProgressColor))
            FillRoundedRect(g, progressBrush, progressArea, CornerRadius);
    }

    private void FillRoundedRect(Graphics g, Brush b, RectangleF bounds, float radius)
    {
        using (GraphicsPath path = RoundedRect(bounds, radius))
            g.FillPath(b, path);
    }

    private GraphicsPath RoundedRect(RectangleF bounds, float radius)
    {
        float diameter = radius * 2f;
        GraphicsPath path = new GraphicsPath();

        if (radius <= 0f)
        {
            path.AddRectangle(bounds);
            return path;
        }

        RectangleF arc = new RectangleF(bounds.Location, new SizeF(diameter, diameter));
        path.AddArc(arc, 180, 90);
        arc.X = bounds.Right - diameter;
        path.AddArc(arc, 270, 90);
        arc.Y = bounds.Bottom - diameter;
        path.AddArc(arc, 0, 90);
        arc.X = bounds.Left;
        path.AddArc(arc, 90, 90);
        path.CloseFigure();

        return path;
    }
}