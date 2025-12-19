using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace SkillTrackerColoring
{
    [HarmonyPatch(typeof(WidgetTracker))]
    [HarmonyPatch("Refresh")]
    internal class TextChange
    {
        private static readonly Dictionary<string, Color> AttributeColors = new Dictionary<string, Color>
        {
            { "STR", new Color(0.8f, 0.0f, 0.0f) },      // 筋力 (赤)
            { "END", new Color(0.8f, 0.6f, 0.2f) },      // 耐久 (明るい茶)
            { "DEX", new Color(0.0f, 0.6f, 0.2f) },      // 器用 (緑)
            { "PER", new Color(0.0f, 0.6f, 0.8f) },      // 感覚 (水色)
            { "LER", new Color(0.4f, 0.4f, 0.8f) },      // 学習 (青)
            { "WIL", new Color(0.6f, 0.2f, 0.8f) },      // 意志 (青紫)
            { "MAG", new Color(0.8f, 0.0f, 0.6f) },      // 魔力 (赤紫)
            { "CHA", new Color(1.0f, 0.5f, 0.0f) },      // 魅力 (オレンジ)
        };

        private static Dictionary<string, Color> _elementNameToColor;
        private static bool _cacheBuilt = false;

        static void Postfix(WidgetTracker __instance)
        {
            try
            {
                BuildElementCache();

                Text objtext = __instance.text;
                if (objtext == null || string.IsNullOrEmpty(objtext.text)) return;

                string[] lines = objtext.text.Split('\n');
                string coloredText = "";

                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    Color? lineColor = GetColorForLine(line);

                    if (lineColor.HasValue)
                    {
                        coloredText += ColorizeLine(line, lineColor.Value);
                    }
                    else
                    {
                        coloredText += line;
                    }

                    if (i < lines.Length - 1)
                    {
                        coloredText += "\n";
                    }
                }

                objtext.text = coloredText;
            }
            catch (System.Exception ex)
            {
                Plugin.Logger.LogError("Error in TextChange.Postfix: " + ex);
            }
        }

        private static void BuildElementCache()
        {
            if (_cacheBuilt) return;

            try
            {
                if (EClass.sources?.elements?.rows == null)
                {
                    Plugin.Logger.LogWarning("EClass.sources.elements.rows is null");
                    return;
                }

                _elementNameToColor = new Dictionary<string, Color>();

                foreach (var row in EClass.sources.elements.rows)
                {
                    if (row == null) continue;

                    string elementName = row.GetName();
                    if (string.IsNullOrEmpty(elementName)) continue;

                    string alias = row.alias;
                    string aliasParent = row.aliasParent;

                    // 主属性自体の場合
                    if (!string.IsNullOrEmpty(alias) && AttributeColors.TryGetValue(alias, out Color color))
                    {
                        _elementNameToColor[elementName] = color;
                        continue;
                    }

                    // スキルの場合、親属性を確認
                    if (!string.IsNullOrEmpty(aliasParent) && AttributeColors.TryGetValue(aliasParent, out color))
                    {
                        _elementNameToColor[elementName] = color;
                    }
                }
                _cacheBuilt = true;
            }
            catch (System.Exception ex)
            {
                Plugin.Logger.LogError("Error building element cache: " + ex);
            }
        }

        private static Color? GetColorForLine(string line)
        {
            if (_elementNameToColor == null) return null;

            foreach (var kvp in _elementNameToColor)
            {
                if (line.Contains(kvp.Key))
                {
                    return kvp.Value;
                }
            }

            return null;
        }

        private static string ColorizeLine(string line, Color color)
        {
            string colorHex = ColorUtility.ToHtmlStringRGB(color);
            return $"<color=#{colorHex}>{line}</color>";
        }
    }
}
