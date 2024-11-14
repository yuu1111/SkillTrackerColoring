using BepInEx;
using HarmonyLib;
using BepInEx.Logging;


namespace SkillTrackerColoring
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        // const
        public const string pluginGuid = "com.github.yuu1111.skilltrackercoloring";
        public const string pluginName = "SkillTrackerColoring Mod";
        public const string pluginVersion = "1.0.1";

        internal static new ManualLogSource Logger;

        public void Awake()
        {

            Logger = base.Logger;
            new Harmony(pluginGuid).PatchAll();
        }
    }
}
