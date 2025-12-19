using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace SkillTrackerColoring
{
    /// <summary>
    /// スキルトラッカーウィジェットのスキル名を主属性に応じて色分けするBepInExプラグイン
    /// </summary>
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        private const string PluginGuid = "com.github.yuu1111.skilltrackercoloring";
        private const string PluginName = "SkillTrackerColoring";
        private const string PluginVersion = "1.0.4";

        /// <summary>
        /// プラグイン用のログ出力インスタンス
        /// </summary>
        internal new static ManualLogSource Logger;

        /// <summary>
        /// 属性色の設定
        /// </summary>
        internal static class ColorConfig
        {
            public static ConfigEntry<string> StrColor;
            public static ConfigEntry<string> EndColor;
            public static ConfigEntry<string> DexColor;
            public static ConfigEntry<string> PerColor;
            public static ConfigEntry<string> LerColor;
            public static ConfigEntry<string> WilColor;
            public static ConfigEntry<string> MagColor;
            public static ConfigEntry<string> ChaColor;
        }

        /// <summary>
        /// プラグイン初期化時に呼び出される
        /// </summary>
        public void Awake()
        {
            Logger = base.Logger;

            InitializeConfig();

            Logger.LogInfo(PluginName + " v" + PluginVersion + " is loaded!");

            new Harmony(PluginGuid).PatchAll();
        }

        /// <summary>
        /// 設定ファイルの初期化
        /// </summary>
        private void InitializeConfig()
        {
            const string section = "Colors";

            ColorConfig.StrColor = Config.Bind(section, "STR_Strength", "#CC0000",
                "筋力系スキルの色 (Hex形式: #RRGGBB)");

            ColorConfig.EndColor = Config.Bind(section, "END_Endurance", "#CC9933",
                "耐久系スキルの色 (Hex形式: #RRGGBB)");

            ColorConfig.DexColor = Config.Bind(section, "DEX_Dexterity", "#009933",
                "器用系スキルの色 (Hex形式: #RRGGBB)");

            ColorConfig.PerColor = Config.Bind(section, "PER_Perception", "#0099CC",
                "感覚系スキルの色 (Hex形式: #RRGGBB)");

            ColorConfig.LerColor = Config.Bind(section, "LER_Learning", "#6666CC",
                "学習系スキルの色 (Hex形式: #RRGGBB)");

            ColorConfig.WilColor = Config.Bind(section, "WIL_Will", "#9933CC",
                "意志系スキルの色 (Hex形式: #RRGGBB)");

            ColorConfig.MagColor = Config.Bind(section, "MAG_Magic", "#CC0099",
                "魔力系スキルの色 (Hex形式: #RRGGBB)");

            ColorConfig.ChaColor = Config.Bind(section, "CHA_Charisma", "#FF8000",
                "魅力系スキルの色 (Hex形式: #RRGGBB)");
        }

        /// <summary>
        /// Hex文字列から色を取得 (#を除去して返す)
        /// </summary>
        internal static string GetColorHex(ConfigEntry<string> config)
        {
            var hex = config.Value;
            if (hex.StartsWith("#"))
            {
                hex = hex.Substring(1);
            }
            return hex;
        }
    }
}
