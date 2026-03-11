using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FrameX360;

namespace FrameX360.UC
{
    public partial class FrameX : UserControl
    {
        static readonly Color C_OK    = Color.FromArgb(63, 185, 80);
        static readonly Color C_WARN = Color.FromArgb(210, 153, 34);
        static readonly Color C_ERR  = Color.FromArgb(248, 81, 73);
        static readonly Color C_CYAN = Color.FromArgb(121, 192, 255);
        static readonly Color C_ACCENT = Color.FromArgb(88, 166, 255);
        static readonly Color C_DIM   = Color.FromArgb(110, 118, 129);
        static readonly Color C_TEXT  = Color.FromArgb(201, 209, 217);
        static readonly Color C_MUTED = Color.FromArgb(139, 148, 158);
        static readonly Color C_BG_INPUT = Color.FromArgb(22, 27, 34);
        static readonly Color C_BG_PANEL = Color.FromArgb(12, 12, 12);

        static readonly Dictionary<string, Dictionary<string, string>> STR =
            new Dictionary<string, Dictionary<string, string>>
            {
                ["en"] = new Dictionary<string, string>
                {
                    ["ready"] = "Ready", ["working"] = "Working...", ["done"] = "Done", ["error"] = "Error",
                    ["browse"] = "Browse", ["decrypt"] = "Decrypt / Decompress", ["refresh"] = "Refresh",
                    ["apply"] = "Apply Selected Patches", ["diag"] = "Diagnose", ["clearlog"] = "Clear",
                    ["gh_load"] = "Fetching patches from GitHub...", ["gh_ok"] = "GitHub — {0} patch files indexed.",
                    ["gh_fail"] = "GitHub unreachable — built-in patches only.",
                    ["detected"] = "{0} match(es) found for this Title ID.", ["no_match"] = "No patch for this Title ID. Use \"Search GitHub\" to find by name.",
                    ["gh_search"] = "Searching GitHub for patches...", ["gh_found"] = "GitHub — {0} patch(es) for this game.",
                    ["dl_patch"] = "Downloading patch file...", ["err_xex"] = "Select a .xex file first.",
                    ["err_game"] = "Select a game from the list.", ["err_tool"] = "xextool.exe not found.",
                    ["warn_sel"] = "No patch selected.", ["dec_start"] = "Starting decrypt / decompress...",
                    ["err_xex_invalid"] = "Invalid XEX file or file is already decrypted.",
                    ["err_xex_corrupt"] = "File appears to be corrupted or incomplete.",
                    ["p_ok"] = "{0}/{1} patches applied.", ["backup"] = "Backup  →  {0}",
                    ["no_apply"] = "No patches could be applied.", ["builtin"] = "Built-in patch",
                    ["search_by_name"] = "— Search GitHub by game name —",
                    ["no_match_suggestions"] = "No exact patch. Try suggestions below or search by name.",
                    ["xex_file"] = "XEX file", ["output"] = "Output", ["title_id"] = "Title ID", ["game"] = "Game",
                    ["path_placeholder"] = "Path to .xex file", ["gh_search_btn"] = "Search patches",
                },
                ["pt"] = new Dictionary<string, string>
                {
                    ["ready"] = "Pronto", ["working"] = "Processando...", ["done"] = "Concluido", ["error"] = "Erro",
                    ["browse"] = "Selecionar", ["decrypt"] = "Decrypt / Decompress", ["refresh"] = "Atualizar",
                    ["apply"] = "Aplicar Patches", ["diag"] = "Diagnostico", ["clearlog"] = "Limpar",
                    ["gh_load"] = "Buscando patches do GitHub...", ["gh_ok"] = "GitHub — {0} arquivos indexados.",
                    ["gh_fail"] = "GitHub inacessivel — patches internos.",
                    ["detected"] = "{0} compativel(is) encontrado(s).", ["no_match"] = "Nenhum patch para este Title ID. Use \"Buscar GitHub\" para procurar por nome.",
                    ["gh_search"] = "Buscando patches no GitHub...", ["gh_found"] = "GitHub — {0} patch(es) para este jogo.",
                    ["dl_patch"] = "Baixando patch...", ["err_xex"] = "Selecione um arquivo .xex.",
                    ["err_game"] = "Selecione um jogo da lista.", ["err_tool"] = "xextool.exe nao encontrado.",
                    ["warn_sel"] = "Nenhum patch selecionado.", ["dec_start"] = "Iniciando decrypt / decompress...",
                    ["err_xex_invalid"] = "Arquivo XEX invalido ou ja descriptografado.",
                    ["err_xex_corrupt"] = "Arquivo parece estar corrompido ou incompleto.",
                    ["p_ok"] = "{0}/{1} patches aplicados.", ["backup"] = "Backup  →  {0}",
                    ["no_apply"] = "Nenhum patch foi aplicado.", ["builtin"] = "Patch interno",
                    ["search_by_name"] = "— Buscar patch no GitHub por nome —",
                    ["no_match_suggestions"] = "Sem patch exato. Tente as sugestões abaixo ou busque por nome.",
                    ["xex_file"] = "Arquivo XEX", ["output"] = "Saída", ["title_id"] = "Title ID", ["game"] = "Jogo",
                    ["path_placeholder"] = "Caminho do arquivo .xex", ["gh_search_btn"] = "Buscar patches",
                },
            };

        string _lang = "en";
        Dictionary<string, List<PatchEntry>> _localPatches = new Dictionary<string, List<PatchEntry>>();
        Dictionary<string, string> _githubUrlByKey = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        bool _patchesLoaded = false;
        string _patchSrc = "builtin";
        List<PatchEntry> _patchEntries = new List<PatchEntry>();
        bool _busy = false;
        readonly List<CheckBox> _checks = new List<CheckBox>();
        readonly Timer _animTimer = new Timer { Interval = 14 };
        int _animVal = 0, _animDir = 1;

        public FrameX()
        {
            InitializeComponent();
            AppLanguage.LanguageChanged += (s, ev) => ApplyLanguage();
        }

        public void ApplyLanguage()
        {
            _lang = AppLanguage.Current;
            lblXex.Text = T("xex_file");
            lblOut.Text = T("output");
            lblTid.Text = T("title_id");
            lblGame.Text = T("game");
            txXex.PlaceholderText = T("path_placeholder");
            btnBrowse.Text = T("browse");
            btnDecrypt.Text = T("decrypt");
            btnApply.Text = T("apply");
            btnSearchPatches.Text = T("gh_search_btn");
            lblGameStatus.Text = T("ready");
        }

        public void OnLoad()
        {
            txXex.TextChanged += (s, e) => OnXexChanged();
            btnBrowse.Click += (s, e) => BrowseXex();
            btnDecrypt.Click += async (s, e) => await RunDecrypt();
            cmbGame.SelectedIndexChanged += (s, e) => OnGameSelected();
            btnRefresh.Click += (s, e) => LoadLocalPatches();
            btnSearchPatches.Click += (s, e) => OpenPatchSearchForm();
            btnApply.Click += async (s, e) => await RunPatch();
            _animTimer.Tick += AnimTick;
            ApplyLanguage();
            LoadLocalPatches();
        }

        string T(string key, params object[] args)
        {
            string s = STR[_lang].TryGetValue(key, out var v) ? v : key;
            return args.Length > 0 ? string.Format(s, args) : s;
        }

        static string XextoolPath()
        {
            string[] candidates = {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "xextool.exe"),
                Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) ?? "", "xextool.exe"),
                @"C:\FrameX360\xextool.exe",
            };
            foreach (var p in candidates)
                if (File.Exists(p)) return p;
            return null;
        }

        void BrowseXex()
        {
            using (var dlg = new OpenFileDialog { Title = "Select XEX file", Filter = "XEX files (*.xex)|*.xex|All files (*.*)|*.*" })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                    txXex.Text = dlg.FileName;
            }
        }

        void OnXexChanged()
        {
            string path = txXex.Text.Trim();
            if (!File.Exists(path)) return;
            string noExt = Path.Combine(Path.GetDirectoryName(path) ?? "", Path.GetFileNameWithoutExtension(path));
            txOutput.Text = noExt + "_decrypted" + Path.GetExtension(path);
            var (tid, fmt) = XexUtils.ReadFromFile(path);
            txTitleId.Text = tid ?? "";
            lblTidFmt.Text = tid != null ? fmt : "";
            if (tid != null) AutoDetect(tid);
        }

        void LoadLocalPatches()
        {
            Log("Loading patches from local folder...", "hi");
            _localPatches.Clear();
            cmbGame.Items.Clear();
            _patchesLoaded = false;
            string patchesDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "patches");
            if (!Directory.Exists(patchesDir))
            {
                Log($"Patches folder not found: {patchesDir}", "warn");
                _patchesLoaded = true;
                return;
            }
            int loaded = 0;
            try
            {
                foreach (string filePath in Directory.GetFiles(patchesDir, "*.patch.toml", SearchOption.TopDirectoryOnly))
                {
                    try
                    {
                        string toml = File.ReadAllText(filePath);
                        if (string.IsNullOrWhiteSpace(toml)) continue;
                        var patches = TomlParser.Parse(toml);
                        if (patches.Count == 0) continue;
                        string fileName = Path.GetFileNameWithoutExtension(filePath).Replace(".patch", "");
                        var tidMatch = System.Text.RegularExpressions.Regex.Match(fileName, @"^([0-9A-Fa-f]{8})\b");
                        if (!tidMatch.Success) continue;
                        string tid = tidMatch.Groups[1].Value.ToUpper();
                        string gameName = fileName.Length > 9 ? fileName.Substring(9).Trim() : tid;
                        string key = gameName.StartsWith("-") ? $"{tid}{gameName}" : $"{tid} - {gameName}";
                        _localPatches[key] = patches;
                        loaded++;
                    }
                    catch (Exception ex) { Log($"Error loading {Path.GetFileName(filePath)}: {ex.Message}", "err"); }
                }
            }
            catch (Exception ex) { Log($"Error reading patches folder: {ex.Message}", "err"); }
            _patchesLoaded = true;
            Log($"Loaded {loaded} patch files from local folder", "ok");
            foreach (var key in _localPatches.Keys.OrderBy(k => k)) cmbGame.Items.Add(key);
            string searchKey = T("search_by_name");
            if (!cmbGame.Items.Contains(searchKey)) cmbGame.Items.Add(searchKey);
            string tidCurrent = txTitleId.Text.Trim();
            if (!string.IsNullOrEmpty(tidCurrent)) AutoDetect(tidCurrent);
            else if (cmbGame.Items.Count > 0) Log($"Total: {cmbGame.Items.Count} games (local patches)", "acc");
        }

        void AutoDetect(string tid)
        {
            if (!_patchesLoaded) return;
            tid = tid.ToUpper();
            var matches = _localPatches.Keys.Where(k =>
                k.StartsWith(tid + " - ", StringComparison.OrdinalIgnoreCase) ||
                k.StartsWith(tid + "-", StringComparison.OrdinalIgnoreCase) ||
                k.StartsWith(tid + " ", StringComparison.OrdinalIgnoreCase)).ToList();
            cmbGame.Items.Clear();
            if (matches.Count > 0)
            {
                foreach (var m in matches.OrderBy(x => x)) cmbGame.Items.Add(m);
                string searchKey = T("search_by_name");
                if (!cmbGame.Items.Contains(searchKey)) cmbGame.Items.Add(searchKey);
                cmbGame.SelectedIndex = 0;
                SetGameStatus(T("detected", matches.Count), C_OK);
                OnGameSelected();
            }
            else
            {
                foreach (var key in _localPatches.Keys.OrderBy(k => k)) cmbGame.Items.Add(key);
                string searchKey = T("search_by_name");
                if (!cmbGame.Items.Contains(searchKey)) cmbGame.Items.Add(searchKey);
                cmbGame.SelectedIndex = -1;
                BuildCheckboxes(new List<PatchEntry>());
                SetGameStatus(T("no_match"), C_WARN);
            }
        }

        async void OpenPatchSearchForm()
        {
            SetGameStatus(T("gh_load"), C_MUTED);
            List<PatchIndexEntry> index;
            try
            {
                index = await GitHubClient.FetchIndexAsync();
            }
            catch
            {
                SetGameStatus(T("gh_fail"), C_WARN);
                return;
            }
            if (index == null || index.Count == 0)
            {
                SetGameStatus(T("gh_fail"), C_WARN);
                return;
            }
            var dlg = new PatchSearchForm(index, _lang);
            try { dlg.ShowDialog(FindForm()); }
            catch { dlg.ShowDialog(); }
            if (dlg.DialogResult != DialogResult.OK || dlg.SelectedEntry == null)
            {
                SetGameStatus(T("ready"), C_MUTED);
                return;
            }
            var entry = dlg.SelectedEntry;
            string display = $"{entry.TitleId} - {entry.Name} (GitHub)";
            if (!_githubUrlByKey.ContainsKey(display))
                _githubUrlByKey[display] = entry.DownloadUrl;
            if (!cmbGame.Items.Contains(display))
                cmbGame.Items.Add(display);
            cmbGame.SelectedItem = display;
            _patchSrc = "github";
            _ = LoadGitHubPatchesForSelectedAsync();
        }

        async System.Threading.Tasks.Task LoadGitHubPatchesForSelectedAsync()
        {
            string? game = cmbGame.SelectedItem?.ToString()?.Trim();
            if (string.IsNullOrEmpty(game)) return;
            if (!_githubUrlByKey.TryGetValue(game, out var url)) return;
            SetGameStatus(T("gh_load"), C_MUTED);
            var patches = await GitHubClient.FetchPatchesAsync(url);
            if (patches != null && patches.Count > 0)
            {
                _patchSrc = "github"; _patchEntries = patches; BuildCheckboxes(patches); SetGameStatus(T("gh_found", patches.Count), C_OK);
            }
            else
                SetGameStatus(T("gh_fail"), C_WARN);
        }

        void SetGameStatus(string text, Color color)
        {
            if (lblGameStatus.InvokeRequired) lblGameStatus.BeginInvoke(new MethodInvoker(() => SetGameStatus(text, color)));
            else { lblGameStatus.Text = text; lblGameStatus.ForeColor = color; }
        }

        async void OnGameSelected()
        {
            string game = cmbGame.Text.Trim();
            if (string.IsNullOrEmpty(game)) return;
            await OnGameSelectedAsync(game);
        }

        async System.Threading.Tasks.Task OnGameSelectedAsync(string game)
        {
            if (game == T("search_by_name"))
            {
                OpenPatchSearchForm();
                return;
            }
            if (_localPatches.TryGetValue(game, out var local))
            {
                _patchSrc = "local"; _patchEntries = local; BuildCheckboxes(local); SetGameStatus($"Local — {local.Count} patches", C_MUTED); return;
            }
            if (_githubUrlByKey.TryGetValue(game, out var url))
            {
                SetGameStatus(T("gh_load"), C_MUTED);
                var patches = await GitHubClient.FetchPatchesAsync(url);
                if (patches != null && patches.Count > 0)
                {
                    _patchSrc = "github"; _patchEntries = patches; BuildCheckboxes(patches); SetGameStatus(T("gh_found", patches.Count), C_OK);
                    return;
                }
            }
            BuildCheckboxes(new List<PatchEntry>()); SetGameStatus("No patches found", C_ERR);
        }

        void BuildCheckboxes(List<PatchEntry> patches)
        {
            if (pnlPatches.InvokeRequired) { pnlPatches.BeginInvoke(new MethodInvoker(() => BuildCheckboxes(patches))); return; }
            pnlPatches.Controls.Clear();
            _checks.Clear();
            if (patches.Count == 0)
            {
                pnlPatches.Controls.Add(new Label { Text = "no patches available", ForeColor = C_MUTED, Font = new Font("Segoe UI", 9f, FontStyle.Italic), Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter, BackColor = C_BG_PANEL });
                return;
            }
            var flp = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                AutoSize = false,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = C_BG_PANEL,
                Padding = new Padding(8, 6, 8, 6),
                Margin = Padding.Empty
            };
            pnlPatches.Controls.Add(flp);
            foreach (var p in patches)
            {
                bool def = _patchSrc == "local" ? p.IsEnabled : true;
                var cb = new CheckBox
                {
                    Text = p.Name,
                    Checked = def,
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 9.5f),
                    BackColor = C_BG_PANEL,
                    AutoSize = true,
                    Margin = new Padding(0, 2, 12, 2)
                };
                flp.Controls.Add(cb);
                _checks.Add(cb);
            }
            flp.PerformLayout();
        }

        async Task RunDecrypt()
        {
            string xex = txXex.Text.Trim(), output = txOutput.Text.Trim();
            if (!File.Exists(xex)) { MessageBox.Show(T("err_xex"), "FrameX360", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            string xt = XextoolPath();
            if (xt == null) { MessageBox.Show(T("err_tool"), "FrameX360", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            SetBusy(true, progDecrypt);
            bool ok = await Task.Run(() => XextoolRunner.Run(xt, xex, output, Log));
            SetBusy(false, progDecrypt, ok);
            if (ok && File.Exists(output)) txXex.Text = output;
        }

        async Task RunPatch()
        {
            string xex = txXex.Text.Trim();
            if (!File.Exists(xex)) { MessageBox.Show(T("err_xex"), "FrameX360", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (_patchEntries.Count == 0) { MessageBox.Show(T("err_game"), "FrameX360", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            var selected = new List<PatchEntry>();
            for (int i = 0; i < _checks.Count && i < _patchEntries.Count; i++)
                if (_checks[i].Checked) selected.Add(_patchEntries[i]);
            if (selected.Count == 0) { MessageBox.Show(T("warn_sel"), "FrameX360", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            SetBusy(true, progPatch);
            string src = _patchSrc;
            bool ok = await Task.Run(() =>
            {
                byte[] data = File.ReadAllBytes(xex);
                string bak = xex + ".bak";
                if (!File.Exists(bak)) File.Copy(xex, bak);
                int applied = 0;
                foreach (var p in selected)
                {
                    bool res = (src == "local" || src == "github") ? PatchApplier.ApplyToml(data, p, Log) : PatchApplier.ApplyBuiltIn(data, p, Log);
                    if (res) applied++;
                }
                if (applied == 0) return false;
                File.WriteAllBytes(xex, data);
                return true;
            });
            SetBusy(false, progPatch, ok);
            if (ok) MessageBox.Show(T("p_ok", selected.Count, selected.Count), "FrameX360", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void RunDiagnose()
        {
            string xex = txXex.Text.Trim();
            if (!File.Exists(xex)) { MessageBox.Show(T("err_xex"), "FrameX360", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            Task.Run(() =>
            {
                long sz = new FileInfo(xex).Length;
                byte[] hdr = new byte[4];
                using (var f = File.OpenRead(xex))
                {
                    f.Read(hdr, 0, 4);
                    bool isXex = hdr[0] == 'X' && hdr[1] == 'E' && hdr[2] == 'X' && hdr[3] == '2';
                    byte[] smp = new byte[3840];
                    f.Seek(256, SeekOrigin.Begin);
                    f.Read(smp, 0, smp.Length);
                    int nz = 0;
                    foreach (var b in smp) if (b != 0) nz++;
                    bool likelyDecrypted = nz <= 3000;
                    if (InvokeRequired) BeginInvoke(new MethodInvoker(() =>
                        MessageBox.Show($"Size: {sz:N0} bytes\nXEX2: {isXex}\n{(likelyDecrypted ? "Entropy looks normal (decrypted)." : "May still be encrypted/compressed.")}", "Diagnose", MessageBoxButtons.OK)));
                    else MessageBox.Show($"Size: {sz:N0} bytes\nXEX2: {isXex}\n{(likelyDecrypted ? "Entropy looks normal (decrypted)." : "May still be encrypted/compressed.")}", "Diagnose", MessageBoxButtons.OK);
                }
            });
        }

        void SetBusy(bool busy, ProgressBar prog, bool ok = true)
        {
            if (InvokeRequired) { BeginInvoke(new MethodInvoker(() => SetBusy(busy, prog, ok))); return; }
            _busy = busy;
            if (busy)
            {
                prog.Visible = true; prog.Value = 0; _animTimer.Tag = prog; _animVal = 0; _animDir = 1; _animTimer.Start();
            }
            else
            {
                _animTimer.Stop(); prog.Value = ok ? 100 : 0;
                var hide = new Timer { Interval = 1400, Tag = prog };
                hide.Tick += (_, __) => { hide.Stop(); hide.Dispose(); prog.Visible = false; };
                hide.Start();
            }
        }

        void AnimTick(object sender, EventArgs e)
        {
            if (_animTimer.Tag is ProgressBar prog && !prog.IsDisposed)
            {
                _animVal += _animDir * 2;
                if (_animVal >= 100) { _animVal = 100; _animDir = -1; }
                else if (_animVal <= 0) { _animVal = 0; _animDir = 1; }
                prog.Value = _animVal;
            }
        }

        void Log(string msg, string tag = "")
        {
        }
    }
}
