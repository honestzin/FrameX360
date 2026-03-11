using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FrameX360;

namespace FrameX360
{
    public class GitHubSearchForm : Form
    {
        readonly ListBox _list;
        readonly TextBox _filter;
        readonly List<PatchIndexEntry> _all;
        readonly string _lang;

        public PatchIndexEntry SelectedEntry { get; private set; }

        public GitHubSearchForm(List<PatchIndexEntry> index, string lang = "en")
        {
            _all = index ?? new List<PatchIndexEntry>();
            _lang = lang ?? "en";
            Text = _lang == "pt" ? "Buscar patch no GitHub por nome" : "Search GitHub patches by name";
            Size = new Size(520, 400);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.Sizable;
            MinimizeBox = false;
            MaximizeBox = true;
            BackColor = Color.FromArgb(22, 27, 34);
            ForeColor = Color.FromArgb(201, 209, 217);
            Font = new Font("Segoe UI", 9.5f);

            var lbl = new Label
            {
                Text = _lang == "pt" ? "Digite para filtrar (ex: Tomb Raider, Resident Evil):" : "Type to filter (e.g. Tomb Raider, Resident Evil):",
                Location = new Point(12, 10),
                AutoSize = true,
                ForeColor = Color.FromArgb(201, 209, 217)
            };
            _filter = new TextBox
            {
                Location = new Point(12, 34),
                Size = new Size(470, 24),
                BackColor = Color.FromArgb(16, 17, 18),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            _filter.TextChanged += (s, e) => ApplyFilter();

            _list = new ListBox
            {
                Location = new Point(12, 64),
                Size = new Size(470, 250),
                BackColor = Color.FromArgb(16, 17, 18),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            _list.DoubleClick += (s, e) => ConfirmSelection();
            _list.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) ConfirmSelection(); };

            var btnOk = new Button
            {
                Text = "OK",
                Location = new Point(280, 324),
                Size = new Size(100, 28),
                BackColor = Color.FromArgb(63, 185, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnOk.Click += (s, e) => ConfirmSelection();

            var btnCancel = new Button
            {
                Text = _lang == "pt" ? "Cancelar" : "Cancel",
                Location = new Point(390, 324),
                Size = new Size(92, 28),
                BackColor = Color.FromArgb(48, 54, 61),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnCancel.Click += (s, e) => { SelectedEntry = null; DialogResult = DialogResult.Cancel; Close(); };

            Controls.Add(lbl);
            Controls.Add(_filter);
            Controls.Add(_list);
            Controls.Add(btnOk);
            Controls.Add(btnCancel);

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
