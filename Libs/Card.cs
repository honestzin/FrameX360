using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CustomControls
{
    public class ImageCardPanel : Panel
    {
        private ImageLayout _backgroundImageLayout = ImageLayout.Stretch;
        private int _borderRadius = 20;
        private int _cardCornerRadius = 10;  
        private int _borderThickness = 2;
        private int _imagePadding = 4;

        [Category("Appearance")]
        [DefaultValue(null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new Image BackgroundImage { get; set; }

        [Category("Appearance")]
        [DefaultValue(ImageLayout.Stretch)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new ImageLayout BackgroundImageLayout
        {
            get => _backgroundImageLayout;
            set
            {
                _backgroundImageLayout = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [DefaultValue(typeof(Color), "40, 40, 45")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color CardColor { get; set; } = Color.FromArgb(40, 40, 45);

        [Category("Appearance")]
        [DefaultValue(50)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int CardHeight { get; set; } = 50;

        [Category("Appearance")]
        [DefaultValue(20)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int CornerRadius
        {
            get => _borderRadius;
            set
            {
                _borderRadius = Math.Max(0, value);
                Invalidate();
            }
        }

        [Category("Appearance")]
        [DefaultValue(10)] 
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int CardCornerRadius
        {
            get => _cardCornerRadius;
            set
            {
                _cardCornerRadius = Math.Max(0, value);
                Invalidate();
            }
        }

        [Category("Appearance")]
        [DefaultValue(2)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int BorderThickness
        {
            get => _borderThickness;
            set
            {
                _borderThickness = Math.Max(0, value);
                Invalidate();
            }
        }

        [Category("Appearance")]
        [DefaultValue(4)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int ImagePadding
        {
            get => _imagePadding;
            set
            {
                _imagePadding = Math.Max(0, value);
                Invalidate();
            }
        }

        public ImageCardPanel()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            Rectangle panelRect = new Rectangle(0, 0, Width, Height);
            using (GraphicsPath panelPath = RoundedRect(panelRect, _borderRadius))
            {
                using (Region clipRegion = new Region(panelPath))
                {
                    g.Clip = clipRegion;


                    if (BackgroundImage != null)
                    {
                        Rectangle paddedRect = new Rectangle(
                            -_imagePadding,
                            -_imagePadding,
                            Width + 2 * _imagePadding,
                            Height + 2 * _imagePadding
                        );

                        switch (BackgroundImageLayout)
                        {
                            case ImageLayout.Stretch:
                                g.DrawImage(BackgroundImage, paddedRect);
                                break;
                            case ImageLayout.Center:
                                int x = (Width - BackgroundImage.Width) / 2;
                                int y = (Height - BackgroundImage.Height) / 2;
                                g.DrawImage(BackgroundImage, x, y);
                                break;
                            case ImageLayout.Zoom:
                                Size imgSize = GetZoomedSize(BackgroundImage.Size, ClientSize);
                                int zx = (Width - imgSize.Width) / 2;
                                int zy = (Height - imgSize.Height) / 2;
                                g.DrawImage(BackgroundImage, new Rectangle(zx, zy, imgSize.Width, imgSize.Height));
                                break;
                            case ImageLayout.Tile:
                                using (TextureBrush brush = new TextureBrush(BackgroundImage))
                                {
                                    g.FillRectangle(brush, paddedRect);
                                }
                                break;
                            case ImageLayout.None:
                                g.DrawImage(BackgroundImage, Point.Empty);
                                break;
                        }

                        if (_borderThickness > 0)
                        {
                            using (Pen pen = new Pen(Color.FromArgb(40, 40, 45), _borderThickness))
                            {
                                g.DrawPath(pen, panelPath);
                            }
                        }
                    }

                    g.ResetClip();
                }

                Rectangle cardRect = new Rectangle(10, Height - CardHeight - 10, Width - 20, CardHeight);
                using (GraphicsPath cardPath = RoundedRect(cardRect, _cardCornerRadius))
                using (SolidBrush brush = new SolidBrush(CardColor))
                {
                    g.FillPath(brush, cardPath);
                }
            }
        }

        private Size GetZoomedSize(Size imageSize, Size targetSize)
        {
            float ratio = Math.Min((float)targetSize.Width / imageSize.Width, (float)targetSize.Height / imageSize.Height);
            return new Size((int)(imageSize.Width * ratio), (int)(imageSize.Height * ratio));
        }

        private GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            GraphicsPath path = new GraphicsPath();
            bounds.Inflate(-1, -1);
            path.StartFigure();
            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
