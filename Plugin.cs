using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using BepInEx.Logging;
using UnityEngine;


namespace SkillTrackerColoring
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        // const
        public const string PluginGuid = "com.github.yuu1111.skilltrackercoloring";
        public const string PluginName = "SkillTrackerColoring Mod";
        public const string PluginVersion = "1.0.1";

        // config
        public static ConfigEntry<Color> StrengthColor;
        public static ConfigEntry<Color> EnduranceColor;
        public static ConfigEntry<Color> DexterityColor;
        public static ConfigEntry<Color> PerceptionColor;
        public static ConfigEntry<Color> LearningColor;
        public static ConfigEntry<Color> WillColor;
        public static ConfigEntry<Color> MagicColor;
        public static ConfigEntry<Color> CharismaColor;

        internal static new ManualLogSource Logger;

        public void Awake()
        {

            /*Color strengthColor = new Color(0.8f, 0.0f, 0.0f); // 筋力（赤）
            Color enduranceColor = new Color(0.8f, 0.6f, 0.2f); // 耐久（明るい茶）
            Color dexterityColor = new Color(0.0f, 0.6f, 0.2f); // 器用（緑）
            Color perceptionColor = new Color(0.0f, 0.6f, 0.8f); // 感覚（水色）
            Color learningColor = new Color(0.4f, 0.4f, 0.8f); // 学習（青）
            Color willColor = new Color(0.6f, 0.2f, 0.8f); // 意志（青紫）
            Color magicColor = new Color(0.8f, 0.0f, 0.6f); // 魔力（赤紫）
            Color charismaColor = new Color(1.0f, 0.5f, 0.0f); // 魅力（オレンジ）*/
]
            StrengthColor = Config.Bind(
                "General",
                "StrengthColor",
                new Color(0.8f, 0.0f, 0.0f),
                "This is the color of strength."
            );

            EnduranceColor = Config.Bind(
                "General",
                "EnduranceColor",
                new Color(0.8f, 0.6f, 0.2f),
                "This is the color of endurance."
            );

            DexterityColor = Config.Bind(
                "General",
                "DexterityColor",
                new Color(0.0f, 0.6f, 0.2f),
                "This is the color of dexterity."
            );

            PerceptionColor = Config.Bind(
                "General",
                "PerceptionColor",
                new Color(0.0f, 0.6f, 0.8f),
                "This is the color of perception."
            );

            LearningColor = Config.Bind(
                "General",
                "LearningColor",
                new Color(0.4f, 0.4f, 0.8f),
                "This is the color of learning."
            );

            WillColor = Config.Bind(
                "General",
                "WillColor",
                new Color(0.6f, 0.2f, 0.8f),
                "This is the color of will."
            );

            MagicColor = Config.Bind(
                "General",
                "MagicColor",
                new Color(0.8f, 0.0f, 0.6f),
                "This is the color of magic."
            );

            CharismaColor = Config.Bind(
                "General",
                "CharismaColor",
                new Color(1.0f, 0.5f, 0.0f),
                "This is the color of charisma."
            );

            Logger = base.Logger;
            new Harmony(PluginGuid).PatchAll();
        }
    }
}