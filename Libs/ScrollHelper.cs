using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spoofer.Helpers
{
    public class SmoothScrollHelper : IDisposable
    {
        private readonly Control container;
        private readonly Panel scrollPanel;
        private readonly Control[] controls;
        private readonly float factor = 0.5f;

        private float targetScroll = 0, currentScroll = 0;
        private bool scrolling = false, dragging = false;
        private int dragOffsetY = 0, dragStartPanelTop = 0;
        private int maxScroll = 0;
        private int[] originalTops;

        private const int ThumbMarginTop = 10, ThumbMarginBottom = 10;

        public SmoothScrollHelper(Control parent, Panel panelScroll)
        {
            container = parent;
            scrollPanel = panelScroll;

            int count = container.Controls.Count;
            controls = new Control[count];
            container.Controls.CopyTo(controls, 0);

            originalTops = new int[count];
            for (int i = 0; i < count; i++)
                originalTops[i] = controls[i].Top;

            container.MouseWheel += Wheel;
            scrollPanel.MouseDown += ScrollPanel_MouseDown;
            scrollPanel.MouseMove += ScrollPanel_MouseMove;
            scrollPanel.MouseUp += ScrollPanel_MouseUp;

            Recalculate();
        }

        public void Recalculate()
        {
            int total = 0;
            for (int i = 0; i < controls.Length; i++)
            {
                var c = controls[i];
                if (c == scrollPanel) continue;
                int b = c.Top + c.Height;
                if (b > total) total = b;
            }

            total += 20;
            maxScroll = Math.Max(0, total - container.Height);

            int barHeight = container.Height;
            int thumbHeight = (int)(barHeight * (barHeight / (float)(total == 0 ? 1 : total)) * 0.7f);
            if (thumbHeight < 30) thumbHeight = 30;
            scrollPanel.Height = Math.Min(thumbHeight, barHeight);

            UpdatePositions();
        }

        private void Wheel(object s, MouseEventArgs e)
        {
            targetScroll = Clamp(targetScroll + (e.Delta > 0 ? -30 : 30), 0, maxScroll);
            StartScroll();
        }

        private void ScrollPanel_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragOffsetY = e.Y;
            dragStartPanelTop = scrollPanel.Top;
        }

        private void ScrollPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (!dragging) return;

            int newTop = dragStartPanelTop + (e.Y - dragOffsetY);
            int barMax = container.Height - scrollPanel.Height - ThumbMarginBottom;
            newTop = Clamp(newTop, ThumbMarginTop, barMax);

            float percent = (float)(newTop - ThumbMarginTop) / (barMax - ThumbMarginTop);
            targetScroll = percent * maxScroll;
            StartScroll();
        }

        private void ScrollPanel_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void UpdatePositions()
        {
            for (int i = 0; i < controls.Length; i++)
            {
                var c = controls[i];
                if (c == scrollPanel) continue;
                c.Top = originalTops[i] - (int)currentScroll;
            }

            int barMax = container.Height - scrollPanel.Height - ThumbMarginBottom;
            float percent = maxScroll == 0 ? 0 : currentScroll / maxScroll;
            scrollPanel.Top = (int)(percent * (barMax - ThumbMarginTop)) + ThumbMarginTop;
        }

        private async void StartScroll()
        {
            if (scrolling) return;
            scrolling = true;

            while (Math.Abs(targetScroll - currentScroll) > 0.5f)
            {
                currentScroll += (targetScroll - currentScroll) * factor;
                currentScroll = Clamp(currentScroll, 0, maxScroll);
                UpdatePositions();
                await Task.Delay(15);
            }

            currentScroll = targetScroll;
            UpdatePositions();
            scrolling = false;
        }

        private int Clamp(int val, int min, int max) => Math.Max(min, Math.Min(val, max));
        private float Clamp(float val, float min, float max) => Math.Max(min, Math.Min(val, max));

        public void Dispose()
        {
            container.MouseWheel -= Wheel;
            scrollPanel.MouseDown -= ScrollPanel_MouseDown;
            scrollPanel.MouseMove -= ScrollPanel_MouseMove;
            scrollPanel.MouseUp -= ScrollPanel_MouseUp;
        }
    }
}
