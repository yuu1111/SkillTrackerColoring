using System;
using System.Collections.Generic;
using System.Text;

namespace SkillTrackerColoring
{
    internal sealed class TrackerTextFormatter
    {
        private string _rawText;
        private string _coloredText;
        private int _revision = -1;

        internal bool NeedsFormatting(string text, int revision)
        {
            return text != _coloredText || revision != _revision;
        }

        internal string Format(string text, IDictionary<string, string> colors, int revision)
        {
            if (!NeedsFormatting(text, revision)) return text;
            // Refresh leaves our output intact when the game's raw text has not changed.
            var raw = text == _coloredText ? _rawText : text;
            if (string.IsNullOrEmpty(raw)) return raw;
            var lines = raw.Split('\n');
            var result = new StringBuilder(raw.Length);
            for (var i = 0; i < lines.Length; i++)
            {
                if (i > 0) result.Append('\n');
                var line = lines[i];
                var separator = line.IndexOf("  ", StringComparison.Ordinal);
                string color;
                if (separator > 0 && colors.TryGetValue(line.Substring(0, separator), out color))
                {
                    result.Append("<color=#").Append(color).Append('>')
                        .Append(line, 0, separator).Append("</color>")
                        .Append(line, separator, line.Length - separator);
                }
                else result.Append(line);
            }
            _rawText = raw;
            _coloredText = result.ToString();
            _revision = revision;
            return _coloredText;
        }

        internal static string NormalizeHex(string value, string fallback)
        {
            var hex = (value ?? "").Trim();
            if (hex.StartsWith("#", StringComparison.Ordinal)) hex = hex.Substring(1);
            if (hex.Length != 6) return fallback;
            foreach (var c in hex)
            {
                if (!(c >= '0' && c <= '9') && !(c >= 'a' && c <= 'f') && !(c >= 'A' && c <= 'F'))
                    return fallback;
            }
            return hex;
        }
    }
}
