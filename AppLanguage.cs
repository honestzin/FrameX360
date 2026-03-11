using System;
using System.IO;

namespace FrameX360
{
    public static class AppLanguage
    {
        static string _lang = "en";
        static string LangFilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lang.txt");

        public static string Current => _lang;
        public static bool IsPortuguese => _lang == "pt";

        public static event EventHandler LanguageChanged;

        public static void Initialize()
        {
            string saved = LoadSaved();
            _lang = saved;
            LanguageChanged?.Invoke(null, EventArgs.Empty);
        }

        public static string LoadSaved()
        {
            try
            {
                string path = LangFilePath;
                if (!File.Exists(path)) return "en";
                string line = File.ReadAllText(path).Trim().ToLowerInvariant();
                if (line == "pt" || line == "en") return line;
            }
            catch { }
            return "en";
        }

        public static void Set(string lang)
        {
            if (lang != "en" && lang != "pt") lang = "en";
            if (_lang == lang)
            {
                SaveToFile(lang);
                return;
            }
            _lang = lang;
            SaveToFile(lang);
            LanguageChanged?.Invoke(null, EventArgs.Empty);
        }

        static void SaveToFile(string lang)
        {
            try
            {
                File.WriteAllText(LangFilePath, lang);
            }
            catch { }
        }
    }
}
