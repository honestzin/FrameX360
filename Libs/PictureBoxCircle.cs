using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class CirclePictureBox : PictureBox
{
    private int borderThickness = 2;
    private Color borderColor = Color.Transparent;

    [Category("Appearance")]
    [DefaultValue(2)]
    public int BorderThickness
    {
        get => borderThickness;
        set
        {
            borderThickness = Math.Max(0, value);
            Invalidate();
        }
    }

    [Category("Appearance")]
    [DefaultValue(typeof(Color), "Transparent")]
    public Color BorderColor
    {
        get => borderColor;
        set
        {
            borderColor = value;
            Invalidate();
        }
    }

    public CirclePictureBox()
    {
        SetStyle(ControlStyles.UserPaint |
                 ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.ResizeRedraw, true);

        SizeMode = PictureBoxSizeMode.Zoom;
        BackColor = Color.Transparent;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        using (BufferedGraphics buffer = BufferedGraphicsManager.Current.Allocate(e.Graphics, ClientRectangle))
        {
            Graphics g = buffer.Graphics;
            g.Clear(Parent?.BackColor ?? Color.Transparent);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.CompositingMode = CompositingMode.SourceOver;
            g.PixelOffsetMode = PixelOffsetMode.None;

            int diameter = Math.Min(Width, Height) - borderThickness * 2;
            Rectangle circleBounds = new Rectangle(
                (Width - diameter) / 2,
                (Height - diameter) / 2,
                diameter,
                diameter
            );

            using (GraphicsPath circlePath = new GraphicsPath())
            {
                circlePath.AddEllipse(circleBounds);

                g.SetClip(circlePath);

                if (Image != null)
                {
                    Rectangle imageRect = GetCroppedImageRect(Image.Size, circleBounds);
                    g.DrawImage(Image, imageRect);
                }

                g.ResetClip();

                if (borderThickness > 0 && borderColor.A > 0)
                {
                    using (Pen pen = new Pen(borderColor, borderThickness))
                    {
                        g.DrawEllipse(pen, circleBounds);
                    }
                }
            }

            buffer.Render(e.Graphics);
        }
    }

    private Rectangle GetCroppedImageRect(Size imageSize, Rectangle targetCircle)
    {
        float imageRatio = (float)imageSize.Width / imageSize.Height;
        float circleRatio = (float)targetCircle.Width / targetCircle.Height;

        int finalWidth, finalHeight;

        if (imageRatio > circleRatio)
        {
            finalHeight = targetCircle.Height;
            finalWidth = (int)(finalHeight * imageRatio);
        }
        else
        {
            finalWidth = targetCircle.Width;
            finalHeight = (int)(finalWidth / imageRatio);
        }

        int x = targetCircle.X + (targetCircle.Width - finalWidth) / 2;
        int y = targetCircle.Y + (targetCircle.Height - finalHeight) / 2;

        return new Rectangle(x, y, finalWidth, finalHeight);
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        ApplyRegion();
    }

    private void ApplyRegion()
    {
        int diameter = Math.Min(Width, Height);
        using (GraphicsPath path = new GraphicsPath())
        {
            path.AddEllipse(new Rectangle((Width - diameter) / 2, (Height - diameter) / 2, diameter, diameter));
            Region = new Region(path);
        }
    }

    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        ApplyRegion();
    }
}
