using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace SkillTrackerColoring
{
    /// <summary>
    /// WidgetTracker.Refreshメソッドへのパッチ
    /// スキル名を主属性に応じた色でハイライトする
    /// </summary>
    [HarmonyPatch(typeof(WidgetTracker))]
    [HarmonyPatch("Refresh")]
    internal class SkillTrackerColorPatch
    {
        /// <summary>
        /// スキル名からHex色文字列へのキャッシュ
        /// </summary>
        private static Dictionary<string, string> _elementNameToColorHex;

        /// <summary>
        /// キャッシュ構築済みフラグ
        /// </summary>
        private static bool _cacheBuilt;

        /// <summary>
        /// 設定から属性エイリアスとHex色文字列の対応を取得する
        /// </summary>
        private static Dictionary<string, string> GetAttributeColorHex()
        {
            return new Dictionary<string, string>
            {
                { "STR", Plugin.GetColorHex(Plugin.ColorConfig.StrColor) },
                { "END", Plugin.GetColorHex(Plugin.ColorConfig.EndColor) },
                { "DEX", Plugin.GetColorHex(Plugin.ColorConfig.DexColor) },
                { "PER", Plugin.GetColorHex(Plugin.ColorConfig.PerColor) },
                { "LER", Plugin.GetColorHex(Plugin.ColorConfig.LerColor) },
                { "WIL", Plugin.GetColorHex(Plugin.ColorConfig.WilColor) },
                { "MAG", Plugin.GetColorHex(Plugin.ColorConfig.MagColor) },
                { "CHA", Plugin.GetColorHex(Plugin.ColorConfig.ChaColor) },
            };
        }

        /// <summary>
        /// WidgetTracker.Refresh実行後に呼び出されるパッチ
        /// テキストの各行をスキルの主属性に応じた色で装飾する
        /// </summary>
        /// <param name="__instance">パッチ対象のWidgetTrackerインスタンス</param>
        static void Postfix(WidgetTracker __instance)
        {
            try
            {
                BuildElementCache();

                var text = __instance.text;
                if (text == null || string.IsNullOrEmpty(text.text)) return;

                var lines = text.text.Split('\n');
                var sb = new StringBuilder();

                for (int i = 0; i < lines.Length; i++)
                {
                    if (i > 0) sb.Append('\n');

                    var line = lines[i];
                    var colorHex = GetColorHexForLine(line);

                    if (colorHex != null)
                    {
                        sb.Append("<color=#").Append(colorHex).Append('>').Append(line).Append("</color>");
                    }
                    else
                    {
                        sb.Append(line);
                    }
                }

                text.text = sb.ToString();
            }
            catch (System.Exception ex)
            {
                Plugin.Logger.LogError("Error in SkillTrackerColorPatch.Postfix: " + ex);
            }
        }

        /// <summary>
        /// ゲームのSourceElementデータからスキル名と色の対応キャッシュを構築する
        /// aliasParentフィールドを使用して各スキルの親属性を判定する
        /// </summary>
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

                var attributeColorHex = GetAttributeColorHex();
                _elementNameToColorHex = new Dictionary<string, string>();

                foreach (var row in EClass.sources.elements.rows)
                {
                    if (row == null) continue;

                    var elementName = row.GetName();
                    if (string.IsNullOrEmpty(elementName)) continue;

                    var alias = row.alias;
                    var aliasParent = row.aliasParent;

                    // 主属性自体の場合
                    if (!string.IsNullOrEmpty(alias) && attributeColorHex.TryGetValue(alias, out var colorHex))
                    {
                        _elementNameToColorHex[elementName] = colorHex;
                        continue;
                    }

                    // スキルの場合、親属性を確認
                    if (!string.IsNullOrEmpty(aliasParent) && attributeColorHex.TryGetValue(aliasParent, out colorHex))
                    {
                        _elementNameToColorHex[elementName] = colorHex;
                    }
                }
                _cacheBuilt = true;
            }
            catch (System.Exception ex)
            {
                Plugin.Logger.LogError("Error building element cache: " + ex);
            }
        }

        /// <summary>
        /// 指定された行に含まれるスキル名から対応するHex色文字列を取得する
        /// </summary>
        /// <param name="line">検索対象の行</param>
        /// <returns>対応するHex色文字列、見つからない場合はnull</returns>
        private static string GetColorHexForLine(string line)
        {
            if (_elementNameToColorHex == null) return null;

            foreach (var kvp in _elementNameToColorHex)
            {
                if (line.Contains(kvp.Key))
                {
                    return kvp.Value;
                }
            }

            return null;
        }
    }
}
