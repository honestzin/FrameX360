using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class FadePanel : Panel
{
    private int _borderThickness = 1;
    private Color _borderColor = Color.FromArgb(48, 54, 62);
    private Color _secondaryBorderColor = Color.FromArgb(30, 30, 30);
    private Color _fillColor = Color.FromArgb(20, 20, 20);
    private int _fadeLength = 40;
    private int _cornerRadius = 10;
    private int _verticalLineLength = 100;
    private int _horizontalLineLength = 100;
    private int _curveDensity = 90;

    public int BorderThickness { get => _borderThickness; set { _borderThickness = Math.Max(0, Math.Min(2, value)); Invalidate(); } }
    public Color BorderColor { get => _borderColor; set { _borderColor = value; Invalidate(); } }
    public Color SecondaryBorderColor { get => _secondaryBorderColor; set { _secondaryBorderColor = value; Invalidate(); } }
    public Color FillColor { get => _fillColor; set { _fillColor = value; Invalidate(); } }
    public int FadeLength { get => _fadeLength; set { _fadeLength = value; Invalidate(); } }
    public int CornerRadius { get => _cornerRadius; set { _cornerRadius = value; Invalidate(); } }
    public int VerticalLineLength { get => _verticalLineLength; set { _verticalLineLength = value; Invalidate(); } }
    public int HorizontalLineLength { get => _horizontalLineLength; set { _horizontalLineLength = value; Invalidate(); } }
    public int CurveDensity { get => _curveDensity; set { _curveDensity = Math.Max(0, Math.Min(90, value)); Invalidate(); } }

    public FadePanel()
    {
        SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        BackColor = Color.Transparent;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        if (Width <= 0 || Height <= 0) return;

        Graphics g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.PixelOffsetMode = PixelOffsetMode.HighQuality;

        int r = Math.Max(0, Math.Min(CornerRadius, Math.Min(Width, Height) / 4));
        int thickness = BorderThickness > 0 ? 1 : 0;
        int inset = thickness;

        Rectangle fillRect = new Rectangle(inset, inset, Math.Max(0, Width - 2 * inset), Math.Max(0, Height - 2 * inset));
        Rectangle borderRect = new Rectangle(0, 0, Width - 1, Height - 1);

        if (fillRect.Width > 0 && fillRect.Height > 0)
        {
            int fillR = Math.Max(0, r - inset);
            using (GraphicsPath fillPath = RoundedRect(fillRect, fillR))
            using (SolidBrush fillBrush = new SolidBrush(FillColor))
            {
                g.FillPath(fillBrush, fillPath);
            }
        }

        if (BorderThickness > 0 && borderRect.Width > 0 && borderRect.Height > 0)
        {
            using (GraphicsPath borderPath = RoundedRect(borderRect, r))
            using (Pen borderPen = new Pen(BorderColor, 1f))
            {
                g.DrawPath(borderPen, borderPath);
            }
        }

        if (VerticalLineLength > 0 && BorderThickness > 0)
        {
            int startY = r + 1;
            int endY = startY + VerticalLineLength;
            int fadeStartY = endY - FadeLength;
            int xPos = 1;

            using (Pen solidPen = new Pen(BorderColor, 1f))
            {
                g.DrawLine(solidPen, xPos, startY, xPos, Math.Min(fadeStartY, Height));
            }

            if (FadeLength > 0 && fadeStartY < Height)
            {
                float fadeH = Math.Min(FadeLength, Height - fadeStartY);
                if (fadeH > 0)
                {
                    RectangleF fadeRect = new RectangleF(xPos - 0.5f, fadeStartY, 2f, fadeH);
                    using (LinearGradientBrush fadeBrush = new LinearGradientBrush(fadeRect, BorderColor, Color.Transparent, LinearGradientMode.Vertical))
                    using (Pen fadePen = new Pen(fadeBrush, 1f))
                    {
                        g.DrawLine(fadePen, xPos, fadeStartY, xPos, Math.Min(endY, Height));
                    }
                }
            }
        }

        if (HorizontalLineLength > 0 && BorderThickness > 0)
        {
            int startX = r + 1;
            int endX = startX + HorizontalLineLength;
            int fadeStartX = endX - FadeLength;
            int yPos = 1;

            using (Pen solidPen = new Pen(BorderColor, 1f))
            {
                g.DrawLine(solidPen, startX, yPos, Math.Min(fadeStartX, Width), yPos);
            }

            if (FadeLength > 0 && fadeStartX < Width)
            {
                float fadeW = Math.Min(FadeLength, Width - fadeStartX);
                if (fadeW > 0)
                {
                    RectangleF fadeRect = new RectangleF(fadeStartX, yPos - 0.5f, fadeW, 2f);
                    using (LinearGradientBrush fadeBrush = new LinearGradientBrush(fadeRect, BorderColor, Color.Transparent, LinearGradientMode.Horizontal))
                    using (Pen fadePen = new Pen(fadeBrush, 1f))
                    {
                        g.DrawLine(fadePen, fadeStartX, yPos, Math.Min(endX, Width), yPos);
                    }
                }
            }
        }
    }

    private GraphicsPath RoundedRect(Rectangle bounds, int radius)
    {
        if (radius <= 0)
        {
            var rectPath = new GraphicsPath();
            rectPath.AddRectangle(bounds);
            return rectPath;
        }
        int d = Math.Min(radius * 2, Math.Min(bounds.Width, bounds.Height));
        GraphicsPath roundedPath = new GraphicsPath();
        roundedPath.StartFigure();
        roundedPath.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
        roundedPath.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
        roundedPath.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
        roundedPath.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
        roundedPath.CloseFigure();
        return roundedPath;
    }
}
