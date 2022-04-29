using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using R2API;
using R2API.Utils;
using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Hex3Mod.Items;
using Hex3Mod.Logging;

namespace Hex3Mod
{
    [BepInPlugin(ModGuid, ModName, ModVer)]
    [BepInDependency(R2API.R2API.PluginGUID, R2API.R2API.PluginVersion)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    [R2APISubmoduleDependency(nameof(ItemAPI), nameof(LanguageAPI), nameof(RecalculateStatsAPI))]
    public class Main : BaseUnityPlugin
    {
        public const string ModGuid = "com.Hex3.Hex3Mod";
        public const string ModName = "Hex3Mod";
        public const string ModVer = "0.1.2";

        public static AssetBundle MainAssets;

        public static ManualLogSource logger;

        // Define all config options
        public static ConfigEntry<bool> ShardOfGlass_Enable;
        public static ConfigEntry<float> ShardOfGlass_DamageIncrease;

        public static ConfigEntry<bool> BucketList_Enable;
        public static ConfigEntry<float> BucketList_FullBuff;
        public static ConfigEntry<float> BucketList_BuffReduce;

        public static ConfigEntry<bool> HopooEgg_Enable;
        public static ConfigEntry<float> HopooEgg_JumpModifier;
        public static ConfigEntry<float> HopooEgg_AirControlModifier;

        public static ConfigEntry<bool> ScatteredReflection_Enable;
        public static ConfigEntry<float> ScatteredReflection_DamageReflectPercent;
        public static ConfigEntry<float> ScatteredReflection_DamageReflectShardStack;
        public static ConfigEntry<float> ScatteredReflection_DamageReflectBonus;

        public static ConfigEntry<bool> Empathy_Enable;
        public static ConfigEntry<float> Empathy_HealFor;

        public void Awake()
        {
            Log.Init(Logger);
            Log.LogInfo("Beginning startup functions...");
            Log.LogInfo("Initializing configs...");
            ShardOfGlass_Enable = Config.Bind<bool>(new ConfigDefinition("Shard Of Glass", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>()));
            ShardOfGlass_DamageIncrease = Config.Bind<float>(new ConfigDefinition("Shard Of Glass", "Damage multiplier"), 0.07f, new ConfigDescription("Percentage of base damage this item adds", null, Array.Empty<object>()));

            BucketList_Enable = Config.Bind<bool>(new ConfigDefinition("Bucket List", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>()));
            BucketList_FullBuff = Config.Bind<float>(new ConfigDefinition("Bucket List", "Speed multiplier"), 0.2f, new ConfigDescription("Percent speed increase", null, Array.Empty<object>()));
            BucketList_BuffReduce = Config.Bind<float>(new ConfigDefinition("Bucket List", "Reduced buff multiplier"), 0.75f, new ConfigDescription("Amount that the speed buff is reduced by while fighting a boss", null, Array.Empty<object>()));

            HopooEgg_Enable = Config.Bind<bool>(new ConfigDefinition("Hopoo Egg", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>()));
            HopooEgg_JumpModifier = Config.Bind<float>(new ConfigDefinition("Hopoo Egg", "Jump height multiplier"), 0.1f, new ConfigDescription("Percent jump height increase", null, Array.Empty<object>()));
            HopooEgg_AirControlModifier = Config.Bind<float>(new ConfigDefinition("Hopoo Egg", "Air control multiplier"), 0.05f, new ConfigDescription("Amount of added air control", null, Array.Empty<object>()));

            ScatteredReflection_Enable = Config.Bind<bool>(new ConfigDefinition("Scattered Reflection", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>()));
            ScatteredReflection_DamageReflectPercent = Config.Bind<float>(new ConfigDefinition("Scattered Reflection", "Damage reflect value"), 0.07f, new ConfigDescription("The percent of all total received damage to be reflected", null, Array.Empty<object>()));
            ScatteredReflection_DamageReflectShardStack = Config.Bind<float>(new ConfigDefinition("Scattered Reflection", "Damage reflect multiplier per shard"), 0.007f, new ConfigDescription("How much of a reflection bonus each Shard Of Glass adds in percentage of total damage", null, Array.Empty<object>()));
            ScatteredReflection_DamageReflectBonus = Config.Bind<float>(new ConfigDefinition("Scattered Reflection", "Reflected damage bonus"), 0.7f, new ConfigDescription("Multiplier of how much bonus damage is added to the reflection", null, Array.Empty<object>()));

            Empathy_Enable = Config.Bind<bool>(new ConfigDefinition("Empathy", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>()));
            Empathy_HealFor = Config.Bind<float>(new ConfigDefinition("Empathy", "Healing amount"), 5f, new ConfigDescription("Healing per sustained hit", null, Array.Empty<object>()));

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Hex3Mod.hex3modassets"))
            {
                MainAssets = AssetBundle.LoadFromStream(stream);
            }

            Log.LogInfo("Loading items...");
            if (ShardOfGlass_Enable.Value == true)
            {
                ShardOfGlass.Initiate(ShardOfGlass_DamageIncrease.Value);
            }
            if (BucketList_Enable.Value == true)
            {
                BucketList.Initiate(BucketList_FullBuff.Value, BucketList_BuffReduce.Value);
            }
            if (HopooEgg_Enable.Value == true)
            {
                HopooEgg.Initiate(HopooEgg_JumpModifier.Value, HopooEgg_AirControlModifier.Value);
            }
            if (ScatteredReflection_Enable.Value == true)
            {
                ScatteredReflection.Initiate(ScatteredReflection_DamageReflectPercent.Value, ScatteredReflection_DamageReflectShardStack.Value, ScatteredReflection_DamageReflectBonus.Value);
            }
            if (Empathy_Enable.Value == true)
            {
                Empathy.Initiate(Empathy_HealFor.Value);
            }

            Log.LogInfo("Done!");
        }
    }
}
