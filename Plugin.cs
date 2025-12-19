using BepInEx;
using HarmonyLib;
using BepInEx.Logging;

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
        /// プラグイン初期化時に呼び出される
        /// </summary>
        public void Awake()
        {
            Logger = base.Logger;
            Logger.LogInfo(PluginName + " v" + PluginVersion + " is loaded!");

            new Harmony(PluginGuid).PatchAll();
        }
    }
}
