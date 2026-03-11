using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class YinYangSpinner : Control
{
    public float Radius { get; set; } = 30f;
    public float Thickness { get; set; } = 4f;
    public float Speed { get; set; } = 2.8f;
    public float Angle { get; set; } = (float)(Math.PI * 0.7);
    public bool Reverse { get; set; } = false;
    public float YangDeltaRadius { get; set; } = 5f;
    public Color ColorI { get; set; } = Color.White;
    public Color ColorY { get; set; } = Color.White;
    public int Mode { get; set; } = 0;

    private Timer timer;
    private float time = 0f;

    public YinYangSpinner()
    {
        this.DoubleBuffered = true;
        this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
              ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
        this.BackColor = Color.Transparent;
        this.Size = new Size(100, 100);
        timer = new Timer();
        timer.Interval = 16;
        timer.Tick += (s, e) => { time += 0.016f; Invalidate(); };
        timer.Start();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        Graphics g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;

        float numSegments = 60f;
        float angleOffset = Angle / numSegments;
        float th = Thickness / numSegments;

        PointF center = new PointF(Width / 2f, Height / 2f);
        float startI = time * Speed;
        float startY = time * (Speed + (YangDeltaRadius > 0f ? Clamp(YangDeltaRadius * 0.5f, 0.5f, 2f) : 0f));

        for (int i = 0; i < numSegments; i++)
        {
            float ab = Ease(Mode, startI + i * (float)Math.PI / 2 / numSegments, (float)Math.PI, 1f, 0f);
            float a = startI + ab + (i * angleOffset);
            float a1 = startI + ab + ((i + 1) * angleOffset);
            DrawLine(g, center, Radius, a, a1, ColorI, th * i);
        }

        float ab_end = Ease(Mode, startI + (float)Math.PI / 2, (float)Math.PI, 1f, 0f);
        float ai_end = startI + ab_end + (numSegments * angleOffset);
        PointF circle_i_center = GetCirclePoint(center, Radius, ai_end);
        g.FillEllipse(new SolidBrush(ColorI), circle_i_center.X - Thickness / 2, circle_i_center.Y - Thickness / 2, Thickness, Thickness);

        float rv = Reverse ? -1f : 1f;
        float yangRadius = Radius - YangDeltaRadius;
        for (int i = 0; i < numSegments; i++)
        {
            float ae = Ease(Mode, startI + i * (float)Math.PI / 2 / numSegments, (float)Math.PI, 1f, 0f);
            float a = startY - ae + (float)Math.PI + (i * angleOffset);
            float a1 = startY - ae + (float)Math.PI + ((i + 1) * angleOffset);
            DrawLine(g, center, yangRadius, a * rv, a1 * rv, ColorY, th * i);
        }

        float ae_end = Ease(Mode, startI + (float)Math.PI / 2, (float)Math.PI, 1f, 0f);
        float ay_end = startY - ae_end + (float)Math.PI + (numSegments * angleOffset);
        PointF circle_y_center = GetCirclePoint(center, yangRadius, ay_end * rv);
        g.FillEllipse(new SolidBrush(ColorY), circle_y_center.X - Thickness / 2, circle_y_center.Y - Thickness / 2, Thickness, Thickness);
    }

    private void DrawLine(Graphics g, PointF center, float radius, float angle1, float angle2, Color color, float width)
    {
        PointF p1 = GetCirclePoint(center, radius, angle1);
        PointF p2 = GetCirclePoint(center, radius, angle2);
        using (Pen pen = new Pen(color, width))
        {
            g.DrawLine(pen, p1, p2);
        }
    }

    private static PointF GetCirclePoint(PointF center, float radius, float angle)
    {
        return new PointF(
            center.X + (float)Math.Cos(angle) * radius,
            center.Y + (float)Math.Sin(angle) * radius
        );
    }

    private static float Clamp(float val, float min, float max) => Math.Max(min, Math.Min(max, val));

    private static float Ease(int mode, float t, float d, float b, float c)
    {
        return c * (t / d) + b;
    }
}
