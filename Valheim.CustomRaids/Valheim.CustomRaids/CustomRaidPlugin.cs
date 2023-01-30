﻿using BepInEx;
using HarmonyLib;
using Valheim.CustomRaids.Configuration;
using Valheim.CustomRaids.Core;
using Valheim.CustomRaids.PreConfiguredRaids;

namespace Valheim.CustomRaids
{
    [BepInDependency("asharppen.valheim.enhanced_progress_tracker", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("asharppen.valheim.spawn_that", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInPlugin(ModId, PluginName, Version)]
    public class CustomRaidPlugin : BaseUnityPlugin
    {
        public const string ModId = "asharppen.valheim.custom_raids";
        public const string PluginName = "Custom Raids";
        public const string Version = "1.7.1";

        void Awake()
        {
            Log.Logger = Logger;

            Logger.LogInfo("Loading configurations...");

            ConfigurationManager.GeneralConfig =  ConfigurationManager.LoadGeneralConfigurations();

            if (ConfigurationManager.GeneralConfig.GeneratePresetRaids.Value)
            {
                new Ragnarok().CreateConfigIfMissing();
                new DeathsquitoSeason().CreateConfigIfMissing();
            }

            EnhancedProgressTrackerInstalled = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("asharppen.valheim.enhanced_progress_tracker");

            if (EnhancedProgressTrackerInstalled)
            {
                Log.LogDebug("Detected installation of Enhanced Progress Tracker.");
            }

            var harmony = new Harmony(ModId);

            harmony.PatchAll();
        }

        public static bool EnhancedProgressTrackerInstalled { get; private set; }
    }
}
