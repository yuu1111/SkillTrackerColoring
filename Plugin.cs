using BepInEx;
using HarmonyLib;
using BepInEx.Logging;


namespace SkillTrackerColoring
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        // const
        private const string PluginGuid = "com.github.yuu1111.skilltrackercoloring";
        private const string PluginName = "SkillTrackerColoring";
        private const string PluginVersion = "1.0.4";

        internal new static ManualLogSource Logger;

        public void Awake()
        {
            Logger = base.Logger;
            Logger.LogInfo(PluginName + " v" + PluginVersion + " is loaded!");

            new Harmony(PluginGuid).PatchAll();
        }
    }
}
