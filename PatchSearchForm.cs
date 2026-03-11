using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FrameX360;

namespace FrameX360
{
    public class PatchSearchForm : Form
    {
        static readonly Color C_BG = Color.FromArgb(18, 18, 18);
        static readonly Color C_PANEL = Color.FromArgb(12, 12, 12);
        static readonly Color C_BORDER = Color.FromArgb(16, 17, 18);
        static readonly Color C_TEXT = Color.White;
        static readonly Color C_MUTED = Color.FromArgb(139, 148, 158);
        static readonly Color C_INPUT = Color.FromArgb(16, 17, 18);
        static readonly Color C_HOVER = Color.Green;

        readonly ListBox _list;
        readonly TextBox _filter;
        readonly List<PatchIndexEntry> _all;
        readonly string _lang;

        public PatchIndexEntry SelectedEntry { get; private set; }

        public PatchSearchForm(List<PatchIndexEntry> index, string lang = "en")
        {
            _all = index ?? new List<PatchIndexEntry>();
            _lang = lang ?? "en";
            Text = _lang == "pt" ? "Buscar patches" : "Search patches";
            Size = new Size(520, 420);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.Sizable;
            MinimizeBox = false;
            MaximizeBox = true;
            BackColor = C_BG;
            ForeColor = C_TEXT;
            Font = new Font("Segoe UI", 9.5f);

            var pnlMain = new FadePanel
            {
                Location = new Point(12, 12),
                Size = new Size(480, 360),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                FillColor = C_PANEL,
                BorderColor = C_BORDER,
                SecondaryBorderColor = C_BORDER,
                BorderThickness = 2,
                CornerRadius = 8
            };

            var lbl = new CustomLabel
            {
                Text = _lang == "pt" ? "Digite para filtrar:" : "Type to filter:",
                Location = new Point(8, 11),
                AutoSize = true,
                ForeColor = C_MUTED,
                Font = new Font("Segoe UI", 9.5f),
                BackColor = Color.Transparent
            };
            _filter = new TextBox
            {
                Location = new Point(16, 38),
                Size = new Size(448, 24),
                BackColor = C_INPUT,
                ForeColor = C_TEXT,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 9.5f)
            };
            _filter.TextChanged += (s, e) => ApplyFilter();

            _list = new ListBox
            {
                Location = new Point(16, 68),
                Size = new Size(448, 220),
                BackColor = C_INPUT,
                ForeColor = C_TEXT,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 9.5f)
            };
            _list.DoubleClick += (s, e) => ConfirmSelection();
            _list.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) ConfirmSelection(); };

            var btnOk = new CustomControls.AnimatedButton
            {
                Text = "OK",
                Location = new Point(284, 298),
                Size = new Size(88, 30),
                InsideColor = C_INPUT,
                BorderColor = C_BORDER,
                HoverColor = C_HOVER,
                TextColor = C_TEXT,
                TextHoverColor = C_TEXT,
                CornerRadius = 6,
                Font = new Font("Segoe UI", 9.5f)
            };
            btnOk.Click += (s, e) => ConfirmSelection();

            var btnCancel = new CustomControls.AnimatedButton
            {
                Text = _lang == "pt" ? "Cancelar" : "Cancel",
                Location = new Point(378, 298),
                Size = new Size(86, 30),
                InsideColor = C_INPUT,
                BorderColor = C_BORDER,
                HoverColor = Color.FromArgb(80, 80, 80),
                TextColor = C_TEXT,
                TextHoverColor = C_TEXT,
                CornerRadius = 6,
                Font = new Font("Segoe UI", 9.5f)
            };
            btnCancel.Click += (s, e) => { SelectedEntry = null; DialogResult = DialogResult.Cancel; Close(); };

            pnlMain.Controls.Add(lbl);
            pnlMain.Controls.Add(_filter);
            pnlMain.Controls.Add(_list);
            pnlMain.Controls.Add(btnOk);
            pnlMain.Controls.Add(btnCancel);
            Controls.Add(pnlMain);

            ApplyFilter();
            if (_list.Items.Count > 0) _list.SelectedIndex = 0;
        }

        List<PatchIndexEntry> _filtered = new List<PatchIndexEntry>();

        void ApplyFilter()
        {
            string q = (_filter?.Text ?? "").Trim().ToLowerInvariant();
            _filtered = string.IsNullOrEmpty(q)
                ? _all.ToList()
                : _all.Where(e =>
                    (e.TitleId + " " + e.Name).ToLowerInvariant().Contains(q)).ToList();
            _list.Items.Clear();
            foreach (var e in _filtered)
                _list.Items.Add(e.TitleId + " - " + e.Name);
            if (_list.Items.Count > 0 && _list.SelectedIndex < 0) _list.SelectedIndex = 0;
        }

        void ConfirmSelection()
        {
            int idx = _list.SelectedIndex;
            if (idx >= 0 && idx < _filtered.Count)
            {
                SelectedEntry = _filtered[idx];
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
