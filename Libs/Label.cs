using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class CustomLabel : Control
{
    public CustomLabel()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.ResizeRedraw |
                 ControlStyles.UserPaint |
                 ControlStyles.SupportsTransparentBackColor, true);

        Font = new Font("Arial", 11.25f, FontStyle.Regular);
        ForeColor = Color.White;
        BackColor = Color.Transparent;
        DoubleBuffered = true;
        Size = new Size(100, 25);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics g = e.Graphics;

        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        g.CompositingQuality = CompositingQuality.HighQuality;
        g.CompositingMode = CompositingMode.SourceOver;
        g.PixelOffsetMode = PixelOffsetMode.None;

        using (SolidBrush brush = new SolidBrush(ForeColor))
        {
            StringFormat sf = new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center
            };

            g.DrawString(Text, Font, brush, ClientRectangle, sf);
        }
    }
}
