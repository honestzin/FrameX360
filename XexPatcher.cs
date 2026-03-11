using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace FrameX360
{
    // ── Data structures ────────────────────────────────────────────────────────
    class PatchEntry
    {
        public string  Name              { get; set; } = "";
        public bool    IsEnabled         { get; set; } = true;
        // Built-in fields
        public int     Offset            { get; set; }
        public byte[]  Find              { get; set; } = new byte[0];
        public byte[]  Replace           { get; set; } = new byte[0];
        public byte[]  ScanPattern       { get; set; } = new byte[0];
        public bool    IsBully           { get; set; }
        public int[]   OffsetsToTry      { get; set; } = new int[0];
        public byte    ExpectedByte      { get; set; } = 0x2E;
        public byte    PatchByte         { get; set; } = 0x00;
        public byte[]  CorePattern       { get; set; } = new byte[0];
        public int     CorePatchIndex    { get; set; } = 3;
        public byte[]  PatternFind       { get; set; } = new byte[0];
        public int     PatternPatchIndex { get; set; } = 7;
        public byte    PatternPatchValue { get; set; } = 0x00;
        // TOML ops: "be8", "be16", "be32", "be64", "f32", "f64" → list of ops
        public Dictionary<string, List<TomlOp>> Ops { get; set; } = new Dictionary<string, List<TomlOp>>();
    }

    class TomlOp
    {
        public long   Address    { get; set; }
        public long   IntValue   { get; set; }
        public double FloatValue { get; set; }
        public bool   IsFloat    { get; set; }
    }

    // ── Built-in patches ───────────────────────────────────────────────────────
    static class BuiltInGames
    {
        public static readonly Dictionary<string, List<PatchEntry>> All =
            new Dictionary<string, List<PatchEntry>>
            {
                ["GTA San Andreas"] = new List<PatchEntry>
                {
                    new PatchEntry
                    {
                        Name   = "Unlock FPS (complete)",
                        Offset = 0x917E1C,
                        Find    = new byte[] { 0x81,0x5F,0x2F,0xFC,0x55,0x6B,0x40,0x2E },
                        Replace = new byte[] { 0x39,0x60,0x00,0x01,0x55,0x6B,0x40,0x03 },
                        ScanPattern = new byte[]
                        {
                            0x81,0x5F,0x2F,0xFC,0x55,0x6B,0x40,0x2E,0x80,0x9F,0x00,0x30,
                            0x55,0x4A,0x4E,0x7E,0x81,0x3F,0x00,0x38,0x7D,0x4B,0x5B,0x78,
                            0x7F,0x04,0x48,0x40,0x7D,0x7E,0xEB,0x78,0x40,0x99,0x00,0x10,
                        },
                    },
                    new PatchEntry
                    {
                        Name   = "Unlock FPS (VSync ON)",
                        Offset = 0x917E1C,
                        Find    = new byte[] { 0x81,0x5F,0x2F,0xFC },
                        Replace = new byte[] { 0x39,0x60,0x00,0x01 },
                        ScanPattern = new byte[]
                        {
                            0x81,0x5F,0x2F,0xFC,0x55,0x6B,0x40,0x2E,0x80,0x9F,0x00,0x30,
                            0x55,0x4A,0x4E,0x7E,0x81,0x3F,0x00,0x38,0x7D,0x4B,0x5B,0x78,
                            0x7F,0x04,0x48,0x40,0x7D,0x7E,0xEB,0x78,0x40,0x99,0x00,0x10,
                        },
                    },
                },
                ["Bully"] = new List<PatchEntry>
                {
                    new PatchEntry
                    {
                        Name             = "Unlock FPS",
                        Offset           = 0x8E8F88,
                        IsBully          = true,
                        OffsetsToTry     = new int[] { 0x8E979F, 0x8E8F8F, 0x8E979C, 0x8E8F88 },
                        ExpectedByte     = 0x2E,
                        PatchByte        = 0x00,
                        CorePattern      = new byte[] { 0x55,0x48,0x40,0x2E,0x3D,0x40 },
                        CorePatchIndex   = 3,
                        PatternFind      = new byte[]
                        {
                            0x81,0x3F,0x2E,0x4C,0x55,0x48,0x40,0x2E,0x3D,0x40,0x82,0x8E,
                            0x55,0x29,0x4E,0x7E,0x38,0xCA,0x5C,0xE8,0x7D,0x2A,0x43,0x78,
                            0x38,0xA0,0x00,0x00,0x7D,0x47,0xE3,0x78,0x7D,0x64,0x5B,0x78,
                            0x7F,0xE3,0xFB,0x78,
                        },
                        PatternPatchIndex = 7,
                        PatternPatchValue = 0x00,
                    },
                },
                ["Assassin's Creed III"] = new List<PatchEntry>
                {
                    new PatchEntry
                    {
                        Name   = "Unlock FPS",
                        Offset = 0x51DE1C,
                        Find    = new byte[] { 0x81,0x7F,0x36,0xB0 },
                        Replace = new byte[] { 0x39,0x60,0x00,0x03 },
                        ScanPattern = new byte[]
                        {
                            0x81,0x7F,0x36,0xB0,0x2B,0x0B,0x00,0x00,0x41,0x9A,0x00,0x34,
                            0x2B,0x0B,0x00,0x01,0x41,0x9A,0x00,0x2C,0x2B,0x0B,0x00,0x02,
                            0x41,0x9A,0x00,0x1C,0x39,0x6B,0xFF,0xFC,0x39,0x40,0x00,0x03,
                        },
                    },
                    new PatchEntry
                    {
                        Name   = "Unlock FPS (alternative — more smoothness, more tearing)",
                        Offset = 0x51DE58,
                        Find    = new byte[] { 0x39,0x60,0x00,0x01 },
                        Replace = new byte[] { 0x39,0x60,0x00,0x00 },
                        ScanPattern = new byte[] { 0x39,0x60,0x00,0x01 },
                    },
                },
            };

        /// <summary>Title ID (8 hex) → (Display key for combo, key in All). Used so built-in games appear without .patch.toml files.</summary>
        public static readonly Dictionary<string, (string DisplayKey, string GameKey)> ByTitleId =
            new Dictionary<string, (string, string)>(StringComparer.OrdinalIgnoreCase)
            {
                ["545408B8"] = ("545408B8 - GTA San Andreas", "GTA San Andreas"),
                ["5454081A"] = ("5454081A - Bully", "Bully"),
                ["555308AE"] = ("555308AE - Assassin's Creed III", "Assassin's Creed III"),
            };
    }

    // ── Scanner: acha o BYTE EXATO do limitador de FPS (loop de espera PowerPC) ───────────────────
    //
    // Padrão real de "wait 30 FPS" no PowerPC:
    //   loop:  lwz/cmpwi  rX, 30      ; compara contador com 30
    //          blt       loop         ; se < 30, volta (branch PARA TRÁS)
    // Decodificamos o branch: se o próximo instr é bc/b com offset NEGATIVO = loop de espera.
    // Retornamos UM ÚNICO patch: o byte da constante (30/60/0x2E) → 0xFF nesse ponto exato.
    // ─────────────────────────────────────────────────────────────────────────────────────────────
    static class XexScanner
    {
        static uint ReadBE32(byte[] d, int i)
        {
            return ((uint)d[i] << 24) | ((uint)d[i + 1] << 16) | ((uint)d[i + 2] << 8) | d[i + 3];
        }

        /// <summary>Retorna true se em offset+4 há um branch condicional que aponta PARA TRÁS (loop de espera).</summary>
        static bool IsBackwardBranch(byte[] data, int offset)
        {
            if (offset + 8 > data.Length) return false;
            byte op = data[offset + 4];
            if (op < 0x40 || op > 0x43) return false;
            uint inst = ReadBE32(data, offset + 4);
            int bd = (int)((inst >> 16) & 0x3FFF);
            if ((bd & 0x2000) != 0) bd -= 0x4000;
            return bd < 0;
        }

        /// <summary>Busca o exato byte que limita FPS: cmpwi/cmplwi N com branch para trás. Um patch só.</summary>
        public static List<PatchEntry> ScanXexForPatches(byte[] data)
        {
            if (data == null || data.Length < 4) return new List<PatchEntry>();
            if (data[0] != 'X' || data[1] != 'E' || data[2] != 'X' || data[3] != '2') return new List<PatchEntry>();

            var (execStart, execLen) = XexUtils.GetExecutableRange(data);
            int execEnd = execStart + execLen;
            var result = new List<PatchEntry>();

            int bestOffset = -1;
            byte bestB0 = 0, bestB1 = 0, bestB3 = 0;
            string bestName = null;

            for (int i = execStart; i + 8 <= execEnd; i += 4)
            {
                byte b0 = data[i], b1 = data[i + 1], b2 = data[i + 2], b3 = data[i + 3];
                if (b2 != 0x00) continue;
                if (b3 == 0xFF) continue;

                bool is30 = (b3 == 0x1E), is60 = (b3 == 0x3C), isVsync = (b3 == 0x2E);
                if (!is30 && !is60 && !isVsync) continue;

                if (b0 != 0x2C && b0 != 0x28) continue;

                if (!IsBackwardBranch(data, i)) continue;

                string name = is30 ? "Unlock FPS (30) — byte exato" : is60 ? "Unlock FPS (60) — byte exato" : "Unlock FPS (vsync) — byte exato";
                bestOffset = i;
                bestB0 = b0; bestB1 = b1; bestB3 = b3;
                bestName = name;
                break;
            }

            if (bestOffset >= 0 && bestName != null)
            {
                byte[] find = new byte[] { bestB0, bestB1, 0x00, bestB3 };
                byte[] repl = new byte[] { bestB0, bestB1, 0x00, 0xFF };
                result.Add(new PatchEntry
                {
                    Name = bestName + " @ 0x" + bestOffset.ToString("X"),
                    Offset = bestOffset,
                    Find = find,
                    Replace = repl,
                    ScanPattern = find
                });
                return result;
            }

            for (int i = execStart; i + 8 <= execEnd; i += 4)
            {
                byte b0 = data[i], b1 = data[i + 1], b2 = data[i + 2], b3 = data[i + 3];
                if (b2 != 0x00 || b3 == 0xFF) continue;
                if (b3 != 0x1E && b3 != 0x3C && b3 != 0x2E) continue;
                if (b0 != 0x38) continue;
                if (!IsBackwardBranch(data, i)) continue;
                string name = (b3 == 0x1E) ? "Unlock FPS (li 30) — byte exato" : (b3 == 0x3C) ? "Unlock FPS (li 60) — byte exato" : "Unlock FPS (li vsync) — byte exato";
                byte[] find = new byte[] { b0, b1, 0x00, b3 };
                byte[] repl = new byte[] { b0, b1, 0x00, 0xFF };
                result.Add(new PatchEntry
                {
                    Name = name + " @ 0x" + i.ToString("X"),
                    Offset = i,
                    Find = find,
                    Replace = repl,
                    ScanPattern = find
                });
                return result;
            }

            for (int i = execStart; i + 4 <= execEnd; i += 4)
            {
                if (data[i] == 0x41 && data[i + 1] == 0xF0 && data[i + 2] == 0x00 && data[i + 3] == 0x00)
                {
                    result.Add(new PatchEntry
                    {
                        Name = "Frame time 30.0 → 60.0 (float) @ 0x" + i.ToString("X"),
                        Offset = i,
                        Find = new byte[] { 0x41, 0xF0, 0x00, 0x00 },
                        Replace = new byte[] { 0x42, 0x48, 0x00, 0x00 },
                        ScanPattern = new byte[] { 0x41, 0xF0, 0x00, 0x00 }
                    });
                    return result;
                }
                if (data[i] == 0x42 && data[i + 1] == 0x48 && data[i + 2] == 0x00 && data[i + 3] == 0x00)
                {
                    result.Add(new PatchEntry
                    {
                        Name = "Frame time 60.0 → ilimitado (float) @ 0x" + i.ToString("X"),
                        Offset = i,
                        Find = new byte[] { 0x42, 0x48, 0x00, 0x00 },
                        Replace = new byte[] { 0x00, 0x00, 0x00, 0x00 },
                        ScanPattern = new byte[] { 0x42, 0x48, 0x00, 0x00 }
                    });
                    return result;
                }
            }

            return result;
        }

        static PatchEntry ClonePatch(PatchEntry p)
        {
            var c = new PatchEntry
            {
                Name = p.Name,
                IsEnabled = p.IsEnabled,
                Offset = p.Offset,
                Find = p.Find ?? new byte[0],
                Replace = p.Replace ?? new byte[0],
                ScanPattern = p.ScanPattern ?? new byte[0],
                IsBully = p.IsBully,
                OffsetsToTry = p.OffsetsToTry ?? new int[0],
                ExpectedByte = p.ExpectedByte,
                PatchByte = p.PatchByte,
                CorePattern = p.CorePattern ?? new byte[0],
                CorePatchIndex = p.CorePatchIndex,
                PatternFind = p.PatternFind ?? new byte[0],
                PatternPatchIndex = p.PatternPatchIndex,
                PatternPatchValue = p.PatternPatchValue,
            };
            if (p.Ops != null && p.Ops.Count > 0)
            {
                c.Ops = new Dictionary<string, List<TomlOp>>();
                foreach (var kv in p.Ops)
                    c.Ops[kv.Key] = new List<TomlOp>(kv.Value);
            }
            return c;
        }
    }

    // ── XEX header utilities ───────────────────────────────────────────────────
    static class XexUtils
    {
        static uint ReadBEU32(byte[] d, int o) =>
            ((uint)d[o] << 24) | ((uint)d[o+1] << 16) | ((uint)d[o+2] << 8) | d[o+3];

        public static uint GetBaseAddress(byte[] data)
        {
            try
            {
                if (data.Length < 0x20 || data[0]!='X'||data[1]!='E'||data[2]!='X'||data[3]!='2')
                    return 0x82000000;
                uint count = ReadBEU32(data, 0x14);
                for (int i = 0; i < Math.Min((int)count, 512); i++)
                {
                    int off = 0x18 + i * 8;
                    if (off + 8 > data.Length) break;
                    uint key = ReadBEU32(data, off);
                    uint val = ReadBEU32(data, off + 4);
                    if (key == 0x00040006 && val + 0x14 <= data.Length)
                    {
                        uint ba = ReadBEU32(data, (int)val + 0x10);
                        if (ba >= 0x80000000u && ba <= 0x90000000u) return ba;
                    }
                }
            }
            catch { }
            return 0x82000000;
        }

        public static int VirtToFileOffset(byte[] data, long virt)
        {
            try
            {
                if (data.Length < 0x10 || data[0]!='X'||data[1]!='E'||data[2]!='X'||data[3]!='2')
                    return -1;
                long baseAddr = GetBaseAddress(data);
                long exeOff   = ReadBEU32(data, 0x08);
                long off = exeOff + (virt - baseAddr);
                return off >= 0 ? (int)off : -1;
            }
            catch { return -1; }
        }

        public static string ReadTitleId(byte[] data)
        {
            try
            {
                if (data.Length < 0x20 || data[0]!='X'||data[1]!='E'||data[2]!='X'||data[3]!='2')
                    return null;
                uint count = ReadBEU32(data, 0x14);
                for (int i = 0; i < Math.Min((int)count, 512); i++)
                {
                    int off = 0x18 + i * 8;
                    if (off + 8 > data.Length) break;
                    uint key = ReadBEU32(data, off);
                    uint val = ReadBEU32(data, off + 4);
                    if (key == 0x00040006 && val + 0x10 <= data.Length)
                        return ReadBEU32(data, (int)val + 0x0C).ToString("X8");
                }
            }
            catch { }
            return null;
        }

        public static (string TitleId, string Format) ReadFromFile(string path)
        {
            try
            {
                using (var f = File.OpenRead(path))
                {
                    var magic = new byte[4];
                    if (f.Read(magic, 0, 4) < 4) return (null, "");
                    uint mv = ((uint)magic[0]<<24)|((uint)magic[1]<<16)|((uint)magic[2]<<8)|magic[3];

                    if (mv == 0x58455832) // XEX2
                    {
                        f.Seek(0, SeekOrigin.Begin);
                        var hdr = new byte[(int)Math.Min(f.Length, 0x8000)];
                        f.Read(hdr, 0, hdr.Length);
                        string tid = ReadTitleId(hdr);
                        return (tid, "XEX2");
                    }
                    if (mv == 0x434F4E20 || mv == 0x4C495645 || mv == 0x50495253) // GOD
                    {
                        f.Seek(0x164, SeekOrigin.Begin);
                        var buf = new byte[4];
                        if (f.Read(buf, 0, 4) < 4) return (null, "");
                        uint tv = ((uint)buf[0]<<24)|((uint)buf[1]<<16)|((uint)buf[2]<<8)|buf[3];
                        if (tv == 0) return (null, "");
                        string fmt = mv == 0x434F4E20 ? "GOD/CON" : mv == 0x4C495645 ? "GOD/LIVE" : "GOD/PIRS";
                        return (tv.ToString("X8"), fmt);
                    }
                }
            }
            catch { }
            return (null, "");
        }

        /// <summary>Returns (startOffset, length) of the executable/code region (from PE offset to end of file).</summary>
        public static (int start, int length) GetExecutableRange(byte[] data)
        {
            if (data == null || data.Length < 0x10 || data[0] != 'X' || data[1] != 'E' || data[2] != 'X' || data[3] != '2')
                return (0, data?.Length ?? 0);
            int exeOff = (int)ReadBEU32(data, 0x08);
            if (exeOff <= 0 || exeOff >= data.Length) return (0, data.Length);
            return (exeOff, data.Length - exeOff);
        }

        public static int IndexOf(byte[] haystack, byte[] needle, int start = 0)
        {
            for (int i = start; i <= haystack.Length - needle.Length; i++)
            {
                bool ok = true;
                for (int j = 0; j < needle.Length; j++)
                    if (haystack[i+j] != needle[j]) { ok = false; break; }
                if (ok) return i;
            }
            return -1;
        }

        // Slice helper (replaces data[start..end] range syntax)
        public static byte[] Slice(byte[] data, int start, int length)
        {
            var result = new byte[length];
            Array.Copy(data, start, result, 0, length);
            return result;
        }
    }

    // ── TOML parser ────────────────────────────────────────────────────────────
    static class TomlParser
    {
        public static List<PatchEntry> Parse(string text)
        {
            var patches   = new List<PatchEntry>();
            PatchEntry    cur      = null;
            string        curKey   = null;
            TomlOp        curOp    = null;

            foreach (var rawLine in text.Split('\n'))
            {
                var line = rawLine.Trim();
                if (line.Length == 0 || line.StartsWith("#")) continue;

                var opM = Regex.Match(line, @"^\[\[patch\.(be\d+|f32|f64)\]\]");
                if (opM.Success)
                {
                    curKey = opM.Groups[1].Value;
                    curOp  = new TomlOp();
                    if (cur != null)
                    {
                        if (!cur.Ops.ContainsKey(curKey)) cur.Ops[curKey] = new List<TomlOp>();
                        cur.Ops[curKey].Add(curOp);
                    }
                    continue;
                }

                if (line == "[[patch]]")
                {
                    cur = new PatchEntry(); patches.Add(cur);
                    curKey = null; curOp = null;
                    continue;
                }

                var kv = Regex.Match(line, @"^(\w+)\s*=\s*(.*)");
                if (!kv.Success) continue;

                string key = kv.Groups[1].Value;
                string vs  = kv.Groups[2].Value.Trim();

                // strip inline comments
                if (vs.StartsWith("\""))
                {
                    int q = vs.IndexOf('"', 1);
                    if (q >= 0) vs = vs.Substring(0, q + 1);
                }
                else if (vs.Contains("#"))
                    vs = vs.Substring(0, vs.IndexOf('#')).Trim();

                // parse value
                if (vs.StartsWith("\"") && vs.EndsWith("\""))
                {
                    string sv = vs.Substring(1, vs.Length - 2);
                    if (cur != null && key == "name") cur.Name = sv;
                }
                else if (vs == "true" || vs == "false")
                {
                    bool bv = vs == "true";
                    if (cur != null && key == "is_enabled") cur.IsEnabled = bv;
                }
                else if ((vs.StartsWith("0x") || vs.StartsWith("0X")))
                {
                    if (long.TryParse(vs.Substring(2), System.Globalization.NumberStyles.HexNumber, null, out long hv))
                    {
                        if (curOp != null)
                        {
                            if (key == "address") curOp.Address  = hv;
                            else if (key == "value") curOp.IntValue = hv;
                        }
                    }
                }
                else if (long.TryParse(vs, out long lv))
                {
                    if (curOp != null && key == "value") curOp.IntValue = lv;
                }
                else if (double.TryParse(vs, System.Globalization.NumberStyles.Float,
                             System.Globalization.CultureInfo.InvariantCulture, out double dv))
                {
                    if (curOp != null && key == "value") { curOp.FloatValue = dv; curOp.IsFloat = true; }
                }
            }

            return patches;
        }
    }

    // ── Patch applier ──────────────────────────────────────────────────────────
    static class PatchApplier
    {
        public static bool ApplyBuiltIn(byte[] data, PatchEntry p, Action<string, string> log)
        {
            if (p.IsBully)
            {
                byte exp = p.ExpectedByte, pat = p.PatchByte;
                var  seq = p.PatternFind;
                int  pi  = p.PatternPatchIndex;

                if (p.Offset > 0)
                {
                    int target = p.Offset;
                    if (target < data.Length)
                    {
                        if (data[target] == pat) { log("  [INFO] " + p.Name + ": already patched", "dim"); return true; }
                        if (data[target] == exp) { data[target] = pat; log($"  [OK] {p.Name}: 0x{target:X}  {exp:X2} \u2192 00", "ok"); return true; }
                    }
                }

                int idx = seq != null && seq.Length > 0 ? XexUtils.IndexOf(data, seq) : -1;
                if (idx >= 0)
                {
                    int target = idx + pi;
                    if (data[target] == pat) { log("  [INFO] " + p.Name + ": already patched", "dim"); return true; }
                    byte old = data[target];
                    data[target] = pat;
                    log($"  [OK] {p.Name}: 0x{target:X}  {old:X2} \u2192 00", "ok");
                    return true;
                }

                foreach (int off in p.OffsetsToTry)
                {
                    if (off >= data.Length) continue;
                    byte v = data[off];
                    if (v == pat) { log($"  [INFO] already 0x00 at 0x{off:X}", "dim"); return true; }
                    if (v == exp)
                    {
                        data[off] = pat;
                        log($"  [OK] {p.Name}: 0x{off:X}  {exp:X2} \u2192 00", "ok");
                        return true;
                    }
                }

                if (p.CorePattern.Length > 0)
                {
                    int cidx = XexUtils.IndexOf(data, p.CorePattern);
                    if (cidx >= 0)
                    {
                        int cpi = cidx + p.CorePatchIndex;
                        byte old = data[cpi];
                        if (old == pat) { log("  [INFO] already patched (core)", "dim"); return true; }
                        data[cpi] = pat;
                        log($"  [OK] {p.Name}: 0x{cpi:X}  {old:X2} \u2192 00 (core)", "ok");
                        return true;
                    }
                }

                log($"  [ERROR] {p.Name}: pattern not found — XEX encrypted?", "err");
                return false;
            }

            // GTA-style
            byte[] find = p.Find; byte[] repl = p.Replace;
            if (p.Offset + find.Length <= data.Length)
            {
                var actual = XexUtils.Slice(data, p.Offset, find.Length);
                if (BytesEq(actual, repl)) { log($"  [INFO] {p.Name}: already applied", "dim"); return true; }
                if (BytesEq(actual, find))
                {
                    Array.Copy(repl, 0, data, p.Offset, repl.Length);
                    log($"  [OK] {p.Name}: 0x{p.Offset:X} patched", "ok");
                    return true;
                }
                log($"  [WARN] bytes mismatch at 0x{p.Offset:X} — scanning...", "warn");
            }

            if (p.ScanPattern.Length > 0)
            {
                int idx = XexUtils.IndexOf(data, p.ScanPattern);
                if (idx >= 0)
                {
                    var actual = XexUtils.Slice(data, idx, find.Length);
                    if (BytesEq(actual, repl)) { log("  [INFO] already applied (scan)", "dim"); return true; }
                    if (BytesEq(actual, find))
                    {
                        Array.Copy(repl, 0, data, idx, repl.Length);
                        log($"  [OK] {p.Name}: 0x{idx:X} patched (scan)", "ok");
                        return true;
                    }
                }
                log($"  [ERROR] {p.Name}: sequence not found — XEX encrypted?", "err");
                return false;
            }

            log($"  [ERROR] {p.Name}: no match", "err");
            return false;
        }

        public static bool ApplyToml(byte[] data, PatchEntry patch, Action<string, string> log)
        {
            var intSizes = new Dictionary<string, int> { { "be8",1 }, { "be16",2 }, { "be32",4 }, { "be64",8 } };
            var fltSizes = new Dictionary<string, int> { { "f32",4 }, { "f64",8 } };
            string pn = patch.Name;
            int applied = 0;

            foreach (var kv in intSizes)
            {
                string sk = kv.Key; int nb = kv.Value;
                List<TomlOp> ops;
                if (!patch.Ops.TryGetValue(sk, out ops)) continue;
                foreach (var op in ops)
                {
                    int fo = XexUtils.VirtToFileOffset(data, op.Address);
                    if (fo < 0 || fo + nb > data.Length)
                    {
                        log($"  [WARN] {pn}: 0x{op.Address:X8} out of range", "warn");
                        continue;
                    }
                    var newBytes = ToBeBytes(op.IntValue, nb);
                    var cur      = XexUtils.Slice(data, fo, nb);
                    if (BytesEq(cur, newBytes)) { log($"  [INFO] {pn}: 0x{op.Address:X8} already applied", "dim"); applied++; continue; }
                    Array.Copy(newBytes, 0, data, fo, nb);
                    log($"  [OK] {pn}: 0x{op.Address:X8}  {BitConverter.ToString(cur)} \u2192 {BitConverter.ToString(newBytes)}", "ok");
                    applied++;
                }
            }

            foreach (var kv in fltSizes)
            {
                string sk = kv.Key; int nb = kv.Value;
                List<TomlOp> ops;
                if (!patch.Ops.TryGetValue(sk, out ops)) continue;
                foreach (var op in ops)
                {
                    int fo = XexUtils.VirtToFileOffset(data, op.Address);
                    if (fo < 0 || fo + nb > data.Length) continue;
                    byte[] newBytes = sk == "f32" ? FloatToBE((float)op.FloatValue) : DoubleToBE(op.FloatValue);
                    var    cur      = XexUtils.Slice(data, fo, nb);
                    if (BytesEq(cur, newBytes)) { applied++; continue; }
                    Array.Copy(newBytes, 0, data, fo, nb);
                    log($"  [OK] {pn}: 0x{op.Address:X8} \u2192 {BitConverter.ToString(newBytes)}", "ok");
                    applied++;
                }
            }

            return applied > 0;
        }

        static bool BytesEq(byte[] a, byte[] b)
        {
            if (a.Length != b.Length) return false;
            for (int i = 0; i < a.Length; i++) if (a[i] != b[i]) return false;
            return true;
        }

        static byte[] ToBeBytes(long v, int size)
        {
            var b = new byte[size];
            for (int i = size - 1; i >= 0; i--) { b[i] = (byte)(v & 0xFF); v >>= 8; }
            return b;
        }

        static byte[] FloatToBE(float v)
        {
            var b = BitConverter.GetBytes(v);
            return new byte[] { b[3], b[2], b[1], b[0] };
        }

        static byte[] DoubleToBE(double v)
        {
            var b = BitConverter.GetBytes(v);
            return new byte[] { b[7], b[6], b[5], b[4], b[3], b[2], b[1], b[0] };
        }
    }

    // ── Xextool runner ─────────────────────────────────────────────────────────
    static class XextoolRunner
    {
        // Check if XEX file is already decrypted/decompressed
        private static bool IsAlreadyDecrypted(string xexPath, Action<string, string> log = null)
        {
            try
            {
                using (var fs = File.OpenRead(xexPath))
                {
                    if (fs.Length < 4) return false;
                    
                    var magic = new byte[4];
                    if (fs.Read(magic, 0, 4) < 4) return false;
                    
                    // Check for XEX2 magic (0x58455832 = "XEX2")
                    uint magicValue = ((uint)magic[0] << 24) | ((uint)magic[1] << 16) | ((uint)magic[2] << 8) | magic[3];
                    
                    if (magicValue == 0x58455832)
                    {
                        // XEX2 header found - check if it's a valid structure
                        // Encrypted XEX files usually don't have valid XEX2 headers
                        // If we can read the header structure, it's likely already decrypted
                        fs.Seek(0, SeekOrigin.Begin);
                        var header = new byte[Math.Min(0x1000, (int)fs.Length)];
                        fs.Read(header, 0, header.Length);
                        
                        // Try to read Title ID - if we can, it's likely decrypted
                        string tid = XexUtils.ReadTitleId(header);
                        if (!string.IsNullOrEmpty(tid))
                        {
                            if (log != null) log($"[INFO] Detected XEX2 format with Title ID: {tid}", "warn");
                            // Valid XEX2 structure detected - likely already decrypted
                            return true;
                        }
                    }
                    else
                    {
                        // Not XEX2 magic - might be encrypted or different format
                        // Encrypted XEX files often start with different bytes
                        // If xextool fails, we'll try to use it anyway as fallback
                        if (log != null)
                        {
                            log($"[INFO] File magic: 0x{magicValue:X8} (not XEX2)", "dim");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (log != null) log($"[WARN] Error checking file: {ex.Message}", "warn");
            }
            return false;
        }

        public static bool Run(string xextool, string xexIn, string xexOut, Action<string, string> log)
        {
            // Validate input file before running
            if (!File.Exists(xexIn))
            {
                log($"[ERROR] Input file not found: {xexIn}", "err");
                return false;
            }

            try
            {
                var fileInfo = new FileInfo(xexIn);
                if (fileInfo.Length == 0)
                {
                    log("[ERROR] Input file is empty (0 bytes)", "err");
                    return false;
                }

                // Check if file is already decrypted
                if (IsAlreadyDecrypted(xexIn, log))
                {
                    log("[INFO] File appears to be already decrypted/decompressed", "warn");
                    log("[INFO] Copying file to output location...", "warn");
                    try
                    {
                        File.Copy(xexIn, xexOut, true);
                        log("[OK] File copied successfully (already decrypted)!", "ok");
                        return true;
                    }
                    catch (Exception ex)
                    {
                        log($"[ERROR] Failed to copy file: {ex.Message}", "err");
                        return false;
                    }
                }

                // Check if file is readable
                using (var fs = File.OpenRead(xexIn))
                {
                    if (fs.Length < 4)
                    {
                        log("[ERROR] File is too small to be a valid XEX file", "err");
                        return false;
                    }
                }

                // Check if output directory exists
                string outDir = Path.GetDirectoryName(xexOut);
                if (!string.IsNullOrEmpty(outDir) && !Directory.Exists(outDir))
                {
                    try
                    {
                        Directory.CreateDirectory(outDir);
                    }
                    catch (Exception ex)
                    {
                        log($"[ERROR] Cannot create output directory: {ex.Message}", "err");
                        return false;
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                log("[ERROR] Access denied. The file may be in use or you don't have permission.", "err");
                return false;
            }
            catch (Exception ex)
            {
                log($"[ERROR] Cannot access input file: {ex.Message}", "err");
                return false;
            }

            string args = $"-c u -e u -o \"{xexOut}\" \"{xexIn}\"";
            log($"  $ {System.IO.Path.GetFileName(xextool)} {args}", "dim");
            
            try
            {
                var psi = new ProcessStartInfo(xextool, args)
                {
                    UseShellExecute        = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError  = true,
                    CreateNoWindow         = true,
                    WorkingDirectory       = Path.GetDirectoryName(xextool) ?? "",
                };
                
                using (var proc = Process.Start(psi))
                {
                    if (proc == null)
                    {
                        log("[ERROR] Failed to start xextool.exe process", "err");
                        return false;
                    }

                    string stdout = proc.StandardOutput.ReadToEnd();
                    string stderr = proc.StandardError.ReadToEnd();
                    proc.WaitForExit(120_000);
                    
                    if (!string.IsNullOrWhiteSpace(stdout)) log(stdout.Trim(), "");
                    if (!string.IsNullOrWhiteSpace(stderr)) log(stderr.Trim(), "");
                    
                    if (proc.ExitCode != 0)
                    {
                        log($"[ERROR] xextool.exe exited with code {proc.ExitCode}", "err");
                        
                        // Provide helpful error messages based on common issues
                        if (stderr.Contains("Error reading xex file"))
                        {
                            log("[INFO] xextool cannot read the XEX file", "warn");
                            
                            // Check if file might already be decrypted (fallback check)
                            log("[INFO] Checking if file is already decrypted...", "dim");
                            if (IsAlreadyDecrypted(xexIn, log))
                            {
                                log("[INFO] File appears to be already decrypted/decompressed", "warn");
                                log("[INFO] Attempting to copy file directly...", "warn");
                                try
                                {
                                    File.Copy(xexIn, xexOut, true);
                                    log("[OK] File copied successfully (already decrypted)!", "ok");
                                    log("[INFO] You can now use this file directly for patching", "ok");
                                    return true;
                                }
                                catch (Exception ex)
                                {
                                    log($"[ERROR] Failed to copy: {ex.Message}", "err");
                                }
                            }
                            else
                            {
                                // File is not in XEX2 format - might be encrypted or invalid
                                log("[INFO] File does not appear to be in XEX2 format", "warn");
                                
                                // Offer to use the file directly anyway (might work for patching)
                                log("[INFO] Since xextool failed, you can try using the original file directly", "warn");
                                log("[INFO] Some XEX files may work for patching even if xextool can't decrypt them", "warn");
                                
                                log("[INFO] Possible causes:", "warn");
                                log("  • File is encrypted and xextool cannot decrypt it", "warn");
                                log("  • File is already decrypted but in a format xextool doesn't recognize", "warn");
                                log("  • File is corrupted or incomplete", "warn");
                                log("  • File format is not supported by xextool v6.3", "warn");
                                log("  • File may need a different decryption tool or method", "warn");
                                log("  • Try extracting the XEX from the game container first", "warn");
                                log("", "dim");
                                log("[TIP] You can try using the original file for patching if it's already decrypted", "warn");
                            }
                        }
                        else if (stderr.Contains("not found") || stderr.Contains("cannot find"))
                        {
                            log("[INFO] File path issue detected", "warn");
                        }
                        
                        return false;
                    }
                    
                    if (!File.Exists(xexOut))
                    {
                        log("[ERROR] Output file was not created", "err");
                        return false;
                    }
                    
                    log("[OK] Decrypt done!", "ok");
                    return true;
                }
            }
            catch (Exception ex)
            {
                log($"[ERROR] Exception: {ex.Message}", "err");
                if (ex.InnerException != null)
                    log($"[ERROR] Inner: {ex.InnerException.Message}", "err");
                return false;
            }
        }
    }
}
