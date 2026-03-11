using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FrameX360
{
    public class PatchIndexEntry
    {
        public string Name        { get; }
        public string TitleId     { get; }
        public string DownloadUrl { get; }
        public PatchIndexEntry(string name, string titleId, string downloadUrl)
        { Name = name; TitleId = titleId; DownloadUrl = downloadUrl; }
    }

    static class GitHubClient
    {
        static readonly HttpClient Http = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(30),
        };
        static GitHubClient()
        {
            Http.DefaultRequestHeaders.Add("User-Agent", "FrameX360/2.1");
            Http.MaxResponseContentBufferSize = 10 * 1024 * 1024;
        }

        static readonly Dictionary<string, List<PatchEntry>> Cache = new Dictionary<string, List<PatchEntry>>();

        const string API = "https://api.github.com/repos/xenia-canary/game-patches/contents/patches";

        static List<PatchIndexEntry> _cachedIndex;
        public static List<PatchIndexEntry> GetCachedIndex() => _cachedIndex;

        public static async Task<List<PatchIndexEntry>> FetchIndexAsync()
        {
            var result = new List<PatchIndexEntry>();
            try
            {
                string json = await Http.GetStringAsync(API);

                if (string.IsNullOrEmpty(json))
                {
                    System.Diagnostics.Debug.WriteLine("GitHub API returned empty response");
                    return result;
                }

                var nameMatches = Regex.Matches(json, @"""name""\s*:\s*""([^""]+\.patch\.toml)""", RegexOptions.Singleline);
                foreach (Match nameM in nameMatches)
                {
                    string name = nameM.Groups[1].Value;
                    int start = nameM.Index + nameM.Length;
                    var urlM = Regex.Match(json.Substring(start), @"""download_url""\s*:\s*""([^""]+)""", RegexOptions.Singleline);
                    if (!urlM.Success) continue;

                    string tid = TidFromFilename(name);
                    if (tid == null) continue;

                    string display = name.Replace(".patch.toml", "");
                    string url = urlM.Groups[1].Value.Replace(@"\/", "/");
                    result.Add(new PatchIndexEntry(display, tid, url));
                }
                _cachedIndex = result;
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                System.Diagnostics.Debug.WriteLine($"GitHub API HTTP error: {ex.Message}");
                throw;
            }
            catch (TaskCanceledException ex)
            {
                System.Diagnostics.Debug.WriteLine($"GitHub API timeout: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GitHub API error: {ex.GetType().Name} - {ex.Message}");
                throw;
            }
            
            return result;
        }

        public static async Task<List<PatchEntry>?> FetchPatchesAsync(string url)
        {
            if (Cache.TryGetValue(url, out var cached)) return cached;
            try
            {
                string toml    = await Http.GetStringAsync(url);
                var    patches = TomlParser.Parse(toml);
                Cache[url] = patches;
                return patches;
            }
            catch { return null; }
        }

        static string? TidFromFilename(string name)
        {
            var m = Regex.Match(name, @"^([0-9A-Fa-f]{8})\b");
            return m.Success ? m.Groups[1].Value.ToUpper() : (string?)null;
        }
    }
}
