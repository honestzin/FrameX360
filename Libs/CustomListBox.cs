using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Hoenst.All.Framework
{
    [ToolboxBitmap(typeof(ListBox))]
    public class CustomListBox : ListBox
    {
        public Dictionary<string, Icon> AppIcons = new Dictionary<string, Icon>();

        public CustomListBox()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);

            UpdateStyles();
            DrawMode = DrawMode.OwnerDrawFixed;
            BorderStyle = BorderStyle.None;
            ItemHeight = 36;
            ForeColor = Color.FromArgb(84, 84, 84);
            SelectionMode = SelectionMode.One;
            Font = new Font("Segoe UI", 9, FontStyle.Regular);
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= Items.Count) return;

            string itemText = Items[e.Index].ToString();
            e.DrawBackground();

            Icon icon = AppIcons.ContainsKey(itemText) ? AppIcons[itemText] : SystemIcons.Application;
            Rectangle iconRect = new Rectangle(e.Bounds.X + 4, e.Bounds.Y + 4, 28, 28);
            Rectangle textRect = new Rectangle(e.Bounds.X + 40, e.Bounds.Y + 8, e.Bounds.Width - 40, e.Bounds.Height - 8);

            if (e.State.HasFlag(DrawItemState.Selected))
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, 120, 215)), e.Bounds);
                e.Graphics.DrawString(itemText, e.Font, Brushes.White, textRect);
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(BackColor), e.Bounds);
                e.Graphics.DrawString(itemText, e.Font, new SolidBrush(ForeColor), textRect);
            }

            try
            {
                e.Graphics.DrawIcon(icon, iconRect);
            }
            catch { }

            e.DrawFocusRectangle();
        }
    }
}