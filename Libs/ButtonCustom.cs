using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CustomControls
{
    [ToolboxItem(true)]
    [Description("Botão animado com efeito de preenchimento LeftToRight e estilo sólido")]
    public class AnimatedButton : Control
    {
        public enum FillDirection
        {
            LeftToRight
        }

        public enum FillStyle
        {
            Solid
        }

        private float closingAnim = 0f;
        private float closingAlpha = 0f;
        private float labelAlpha = 0f;

        private Rectangle buttonRect;
        private GraphicsPath buttonPath;
        private Timer animationTimer;
        private ToolTip toolTip = new ToolTip();

        private Color insideSolidColor = Color.FromArgb(14, 14, 14);
        private Color outsideSolidColor = Color.FromArgb(24, 23, 25);
        private Color hoverColor = Color.FromArgb(86, 61, 122);
        private Color textColor = Color.FromArgb(150, 150, 150);
        private Color textHoverColor = Color.FromArgb(200, 200, 200);

        private bool isHovered = false;
        private float animationSpeed = 0.8f;
        private int cornerRadius = 6;
        private int animationInterval = 10;

        private bool showToolTip = false;
        private string toolTipMessage = "";
        private string toolTipIcon = "";

        [Category("Appearance")]
        public FillStyle AnimationFillStyle
        {
            get => FillStyle.Solid;
            set { }
        }

        [Category("Appearance")]
        public FillDirection AnimationFillDirection
        {
            get => FillDirection.LeftToRight;
            set { }
        }

        [Category("Appearance")]
        public Color InsideColor
        {
            get => insideSolidColor;
            set { insideSolidColor = value; Invalidate(); }
        }

        [Category("Appearance")]
        public Color BorderColor
        {
            get => outsideSolidColor;
            set { outsideSolidColor = value; Invalidate(); }
        }

        [Category("Appearance")]
        public Color HoverColor
        {
            get => hoverColor;
            set { hoverColor = value; Invalidate(); }
        }

        [Category("Appearance")]
        public Color TextColor
        {
            get => textColor;
            set { textColor = value; Invalidate(); }
        }

        [Category("Appearance")]
        public Color TextHoverColor
        {
            get => textHoverColor;
            set { textHoverColor = value; Invalidate(); }
        }

        [Category("Behavior")]
        public float AnimationSpeed
        {
            get => animationSpeed;
            set { animationSpeed = Math.Max(0.1f, Math.Min(5.0f, value)); Invalidate(); }
        }

        [Category("Appearance")]
        public int CornerRadius
        {
            get => cornerRadius;
            set { cornerRadius = Math.Max(0, Math.Min(20, value)); UpdateButtonPath(); Invalidate(); }
        }

        [Category("Behavior")]
        public bool ShowToolTip
        {
            get => showToolTip;
            set { showToolTip = value; UpdateToolTip(); }
        }

        [Category("Behavior")]
        public string ToolTipMessage
        {
            get => toolTipMessage;
            set { toolTipMessage = value; UpdateToolTip(); }
        }

        [Category("Behavior")]
        public string ToolTipIcon
        {
            get => toolTipIcon;
            set { toolTipIcon = value; UpdateToolTip(); }
        }

        public AnimatedButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint |
                     ControlStyles.SupportsTransparentBackColor, true);

            Size = new Size(150, 40);
            Text = "Button";
            BackColor = Color.Transparent;
            Font = new Font("Outfit", 11.25F);
            Cursor = Cursors.Default;

            animationTimer = new Timer { Interval = animationInterval };
            animationTimer.Tick += AnimationTimer_Tick;
            animationTimer.Start();

            toolTip.ShowAlways = true;
            toolTip.IsBalloon = true;

            UpdateButtonPath();
        }

        private void UpdateButtonPath()
        {
            buttonRect = new Rectangle(0, 0, Width - 1, Height - 1);
            buttonPath?.Dispose();
            buttonPath = CreateRoundedRectPath(buttonRect, cornerRadius);
        }

        private GraphicsPath CreateRoundedRectPath(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();

            if (radius <= 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);

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

            try
            {
                using (SolidBrush brush = new SolidBrush(insideSolidColor))
                    g.FillPath(brush, buttonPath);

                using (Pen pen = new Pen(outsideSolidColor))
                    g.DrawPath(pen, buttonPath);

                if (closingAlpha > 0 && closingAnim > 0)
                {
                    using (SolidBrush brush = new SolidBrush(Color.FromArgb((int)closingAlpha, hoverColor)))
                    {
                        int animSize = (int)closingAnim;
                        Rectangle fillRect = new Rectangle(0, 0, animSize, Height);
                        using (GraphicsPath fillPath = CreateRoundedRectPath(fillRect, cornerRadius))
                            g.FillPath(brush, fillPath);
                    }
                }

                StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(255 - (int)labelAlpha, textColor)))
                using (SolidBrush textHoverBrush = new SolidBrush(Color.FromArgb((int)labelAlpha, textHoverColor)))
                {
                    g.DrawString(Text, Font, textBrush, buttonRect, sf);
                    g.DrawString(Text, Font, textHoverBrush, buttonRect, sf);
                }
            }
            catch
            {
                using (SolidBrush brush = new SolidBrush(SystemColors.Control))
                    g.FillRectangle(brush, ClientRectangle);
                using (Pen pen = new Pen(SystemColors.ControlDark))
                    g.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
                TextRenderer.DrawText(g, Text, Font, ClientRectangle, ForeColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            bool needRedraw = false;
            float deltaTime = 0.01f * animationSpeed;
            bool newHoverState = ClientRectangle.Contains(PointToClient(Cursor.Position));

            if (isHovered != newHoverState)
            {
                isHovered = newHoverState;
                needRedraw = true;
            }

            float step = Width * deltaTime * 3f;

            if (isHovered)
            {
                closingAnim = Math.Min(Width, closingAnim + step);
                closingAlpha = Math.Min(255, closingAlpha + deltaTime * 500);
                labelAlpha = Math.Min(255, labelAlpha + deltaTime * 500);
                needRedraw = true;
            }
            else
            {
                closingAnim = Math.Max(0, closingAnim - step);
                closingAlpha = Math.Max(0, closingAlpha - deltaTime * 500);
                labelAlpha = Math.Max(0, labelAlpha - deltaTime * 500);
                needRedraw = true;
            }

            if (needRedraw)
                Invalidate();
        }

        private void UpdateToolTip()
        {
            if (showToolTip && !string.IsNullOrEmpty(toolTipMessage))
            {
                toolTip.SetToolTip(this, toolTipMessage);
            }
            else
            {
                toolTip.RemoveAll();
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Cursor = Cursors.Default;
            isHovered = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            Cursor = Cursors.Default;
            isHovered = false;
            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateButtonPath();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                animationTimer?.Dispose();
                buttonPath?.Dispose();
                toolTip?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
