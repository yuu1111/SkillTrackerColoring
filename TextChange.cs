using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HarmonyLib;

namespace SkillTrackerColoring
{
    [HarmonyPatch(typeof(WidgetTracker), nameof(WidgetTracker.Refresh))]
    internal class SkillTrackerColorPatch
    {
        private static readonly ConditionalWeakTable<WidgetTracker, TrackerTextFormatter> Formatters =
            new ConditionalWeakTable<WidgetTracker, TrackerTextFormatter>();
        private static bool _errorLogged;

        static void Postfix(WidgetTracker __instance)
        {
            try
            {
                var text = __instance.text;
                if (text == null || string.IsNullOrEmpty(text.text) || EClass.player == null || EClass.pc == null)
                    return;
                var formatter = Formatters.GetValue(__instance, _ => new TrackerTextFormatter());
                if (!formatter.NeedsFormatting(text.text, Plugin.ColorRevision)) return;

                var colors = new Dictionary<string, string>(StringComparer.Ordinal);
                foreach (var id in EClass.player.trackedElements)
                {
                    var element = EClass.pc.elements.GetElement(id);
                    if (element == null) continue;
                    var row = element.source;
                    var color = GetAttributeColor(row.alias) ?? GetAttributeColor(row.aliasParent);
                    var name = element.Name;
                    if (color != null && !string.IsNullOrEmpty(name)) colors[name] = color;
                }
                var colored = formatter.Format(text.text, colors, Plugin.ColorRevision);
                if (text.text != colored) text.text = colored;
                _errorLogged = false;
            }
            catch (Exception ex)
            {
                if (_errorLogged) return;
                _errorLogged = true;
                Plugin.Logger.LogError("Error in SkillTrackerColorPatch.Postfix: " + ex);
            }
        }

        private static string GetAttributeColor(string alias)
        {
            switch (alias)
            {
                case "STR": return Plugin.GetColorHex(Plugin.ColorConfig.StrColor);
                case "END": return Plugin.GetColorHex(Plugin.ColorConfig.EndColor);
                case "DEX": return Plugin.GetColorHex(Plugin.ColorConfig.DexColor);
                case "PER": return Plugin.GetColorHex(Plugin.ColorConfig.PerColor);
                case "LER": return Plugin.GetColorHex(Plugin.ColorConfig.LerColor);
                case "WIL": return Plugin.GetColorHex(Plugin.ColorConfig.WilColor);
                case "MAG": return Plugin.GetColorHex(Plugin.ColorConfig.MagColor);
                case "CHA": return Plugin.GetColorHex(Plugin.ColorConfig.ChaColor);
                default: return null;
            }
        }
    }
}
