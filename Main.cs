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
using Hex3Mod.Artifacts;
using Hex3Mod.Logging;

namespace Hex3Mod
{
    [BepInPlugin(ModGuid, ModName, ModVer)]
    [BepInDependency(R2API.R2API.PluginGUID, R2API.R2API.PluginVersion)]
    [BepInDependency(VoidItemAPI.VoidItemAPI.MODGUID)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    [R2APISubmoduleDependency(nameof(ItemAPI), nameof(LanguageAPI), nameof(RecalculateStatsAPI), nameof(PrefabAPI))]
    public class Main : BaseUnityPlugin
    {
        public const string ModGuid = "com.Hex3.Hex3Mod";
        public const string ModName = "Hex3Mod";
        public const string ModVer = "0.2.4";

        public static AssetBundle MainAssets;

        public static Dictionary<string, string> ShaderLookup = new Dictionary<string, string>() // Strings of stubbed vs. real shaders
        {
            {"stubbed hopoo games/deferred/standard", "shaders/deferred/hgstandard"}
        };

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

        public static ConfigEntry<bool> AtgPrototype_Enable;
        public static ConfigEntry<float> AtgPrototype_Damage;
        public static ConfigEntry<int> AtgPrototype_HitRequirement;

        public static ConfigEntry<bool> ScatteredReflection_Enable;
        public static ConfigEntry<float> ScatteredReflection_DamageReflectPercent;
        public static ConfigEntry<float> ScatteredReflection_DamageReflectShardStack;
        public static ConfigEntry<float> ScatteredReflection_DamageReflectBonus;

        public static ConfigEntry<bool> Empathy_Enable;
        public static ConfigEntry<float> Empathy_HealFor;

        public static ConfigEntry<bool> Apathy_Enable;
        public static ConfigEntry<float> Apathy_Barrier;
        public static ConfigEntry<float> Apathy_BarrierStack;
        public static ConfigEntry<float> Apathy_Reduction;
        public static ConfigEntry<float> Apathy_ReductionStack;

        public static ConfigEntry<bool> MintCondition_Enable;
        public static ConfigEntry<float> MintCondition_MoveSpeed;
        public static ConfigEntry<float> MintCondition_MoveSpeedStack;
        public static ConfigEntry<int> MintCondition_AddJumps;
        public static ConfigEntry<int> MintCondition_AddJumpsStack;

        public static ConfigEntry<bool> CorruptingParasite_Enable;
        public static ConfigEntry<bool> CorruptingParasite_CorruptBossItems;

        public static ConfigEntry<bool> NoticeOfAbsence_Enable;
        public static ConfigEntry<float> NoticeOfAbsence_SpeedBuff;

        public static ConfigEntry<bool> Discovery_Enable;
        public static ConfigEntry<float> Discovery_ShieldAdd;
        public static ConfigEntry<int> Discovery_MaxStacks;

        public static ConfigEntry<bool> TheHermit_Enable;
        public static ConfigEntry<float> TheHermit_BuffDuration;
        public static ConfigEntry<float> TheHermit_DamageReduction;

        public void Awake()
        {
            Log.Init(Logger);
            Log.LogInfo("Beginning startup functions..."); // Config zone
            Log.LogInfo("Initializing configs...");
            ShardOfGlass_Enable = Config.Bind<bool>(new ConfigDefinition("Shard Of Glass", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>()));
            ShardOfGlass_DamageIncrease = Config.Bind<float>(new ConfigDefinition("Shard Of Glass", "Damage multiplier"), 0.07f, new ConfigDescription("Percentage of base damage this item adds", null, Array.Empty<object>()));

            BucketList_Enable = Config.Bind<bool>(new ConfigDefinition("Bucket List", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>()));
            BucketList_FullBuff = Config.Bind<float>(new ConfigDefinition("Bucket List", "Speed multiplier"), 0.2f, new ConfigDescription("Percent speed increase", null, Array.Empty<object>()));
            BucketList_BuffReduce = Config.Bind<float>(new ConfigDefinition("Bucket List", "Reduced buff multiplier"), 0.75f, new ConfigDescription("Amount that the speed buff is reduced by while fighting a boss", null, Array.Empty<object>()));

            HopooEgg_Enable = Config.Bind<bool>(new ConfigDefinition("Hopoo Egg", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>()));
            HopooEgg_JumpModifier = Config.Bind<float>(new ConfigDefinition("Hopoo Egg", "Jump height multiplier"), 0.15f, new ConfigDescription("Percent jump height increase", null, Array.Empty<object>()));
            HopooEgg_AirControlModifier = Config.Bind<float>(new ConfigDefinition("Hopoo Egg", "Air control multiplier"), 0.1f, new ConfigDescription("Amount of added air control", null, Array.Empty<object>()));

            AtgPrototype_Enable = Config.Bind<bool>(new ConfigDefinition("ATG Prototype", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>()));
            AtgPrototype_Damage = Config.Bind<float>(new ConfigDefinition("ATG Prototype", "Damage per stack"), 1f, new ConfigDescription("Multiplier of base damage the missile deals per stack", null, Array.Empty<object>()));
            AtgPrototype_HitRequirement = Config.Bind<int>(new ConfigDefinition("ATG Prototype", "Hits required per missile"), 10, new ConfigDescription("How many hits it should take to fire each missile", null, Array.Empty<object>()));

            ScatteredReflection_Enable = Config.Bind<bool>(new ConfigDefinition("Scattered Reflection", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>()));
            ScatteredReflection_DamageReflectPercent = Config.Bind<float>(new ConfigDefinition("Scattered Reflection", "Damage reflect value"), 0.07f, new ConfigDescription("The percent of all total received damage to be reflected", null, Array.Empty<object>()));
            ScatteredReflection_DamageReflectShardStack = Config.Bind<float>(new ConfigDefinition("Scattered Reflection", "Damage reflect multiplier per shard"), 0.007f, new ConfigDescription("How much of a reflection bonus each Shard Of Glass adds in percentage of total damage (Caps at 90%)", null, Array.Empty<object>()));
            ScatteredReflection_DamageReflectBonus = Config.Bind<float>(new ConfigDefinition("Scattered Reflection", "Reflected damage bonus"), 0.7f, new ConfigDescription("Multiplier of how much bonus damage is added to the reflection", null, Array.Empty<object>()));

            Empathy_Enable = Config.Bind<bool>(new ConfigDefinition("Empathy", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>()));
            Empathy_HealFor = Config.Bind<float>(new ConfigDefinition("Empathy", "Healing amount"), 5f, new ConfigDescription("Healing per sustained hit", null, Array.Empty<object>()));

            Apathy_Enable = Config.Bind<bool>(new ConfigDefinition("Apathy", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>()));
            Apathy_Barrier = Config.Bind<float>(new ConfigDefinition("Apathy", "Barrier on receiving damage"), 0.03f, new ConfigDescription("Percentage barrier gained and granted to allies when you or your allies are hit", null, Array.Empty<object>()));
            Apathy_BarrierStack = Config.Bind<float>(new ConfigDefinition("Apathy", "Barrier on receiving damage per stack"), 0.02f, new ConfigDescription("Percentage barrier gained and granted to allies when you or your allies are hit per stack", null, Array.Empty<object>()));
            Apathy_Reduction = Config.Bind<float>(new ConfigDefinition("Apathy", "Barrier damage reduction"), 0.2f, new ConfigDescription("Damage reduction while the item holder has barrier", null, Array.Empty<object>()));
            Apathy_ReductionStack = Config.Bind<float>(new ConfigDefinition("Apathy", "Barrier damage reduction per stack"), 0.10f, new ConfigDescription("Damage reduction while the item holder has barrier (Hyperbolic, caps at 90%)", null, Array.Empty<object>()));

            MintCondition_Enable = Config.Bind<bool>(new ConfigDefinition("Mint Condition", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>()));
            MintCondition_MoveSpeed = Config.Bind<float>(new ConfigDefinition("Mint Condition", "Move speed increase"), 0.2f, new ConfigDescription("Base movement speed increase", null, Array.Empty<object>()));
            MintCondition_MoveSpeedStack = Config.Bind<float>(new ConfigDefinition("Mint Condition", "Move speed increase per stack"), 0.4f, new ConfigDescription("Base movement speed increase per additional stack", null, Array.Empty<object>()));
            MintCondition_AddJumps = Config.Bind<int>(new ConfigDefinition("Mint Condition", "Additional jumps"), 1, new ConfigDescription("Jump count increase", null, Array.Empty<object>()));
            MintCondition_AddJumpsStack = Config.Bind<int>(new ConfigDefinition("Mint Condition", "Additional jumps per stack"), 2, new ConfigDescription("Jump count increase per additional stack", null, Array.Empty<object>()));

            CorruptingParasite_Enable = Config.Bind<bool>(new ConfigDefinition("Corrupting Parasite", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>()));
            CorruptingParasite_CorruptBossItems = Config.Bind<bool>(new ConfigDefinition("Corrupting Parasite", "Corrupt boss items"), true, new ConfigDescription("Allows the parasite to corrupt boss items", null, Array.Empty<object>()));

            NoticeOfAbsence_Enable = Config.Bind<bool>(new ConfigDefinition("Notice Of Absence", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>()));
            NoticeOfAbsence_SpeedBuff = Config.Bind<float>(new ConfigDefinition("Notice Of Absence", "Damage multiplier"), 0.03f, new ConfigDescription("Percentage of base speed per void item", null, Array.Empty<object>()));

            Discovery_Enable = Config.Bind<bool>(new ConfigDefinition("Discovery", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>()));
            Discovery_ShieldAdd = Config.Bind<float>(new ConfigDefinition("Discovery", "Shield value"), 2f, new ConfigDescription("Shield added per world interactable used", null, Array.Empty<object>()));
            Discovery_MaxStacks = Config.Bind<int>(new ConfigDefinition("Discovery", "Maximum uses"), 100, new ConfigDescription("Maximum interactable uses per stack before shield is no longer granted", null, Array.Empty<object>()));

            TheHermit_Enable = Config.Bind<bool>(new ConfigDefinition("The Hermit", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>()));
            TheHermit_BuffDuration = Config.Bind<float>(new ConfigDefinition("The Hermit", "Buff duration"), 1f, new ConfigDescription("How long in seconds the on-hit buff should last", null, Array.Empty<object>()));
            TheHermit_DamageReduction = Config.Bind<float>(new ConfigDefinition("The Hermit", "Buff damage reduction"), 0.01f, new ConfigDescription("Damage reduced by each buff in percent", null, Array.Empty<object>()));

            Log.LogInfo("Creating assets...");
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Hex3Mod.hex3modassets"))
            {
                MainAssets = AssetBundle.LoadFromStream(stream); // Load mainassets into stream
            }

            var materialAssets = MainAssets.LoadAllAssets<Material>();
            foreach (Material material in materialAssets) // Oh christ this was annoying
            {
                if (!material.shader.name.StartsWith("Stubbed Hopoo Games"))
                {
                    continue;
                }
                var replacementShader = Resources.Load<Shader>(ShaderLookup[material.shader.name.ToLower()]);
                if (replacementShader)
                {
                    material.shader = replacementShader;
                }
            }

            Log.LogInfo("Loading items...");
            if (ShardOfGlass_Enable.Value == true) // Only initiate items if they are enabled via config
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
            if (AtgPrototype_Enable.Value == true)
            {
                AtgPrototype.Initiate(AtgPrototype_Damage.Value, AtgPrototype_HitRequirement.Value);
            }
            if (ScatteredReflection_Enable.Value == true)
            {
                ScatteredReflection.Initiate(ScatteredReflection_DamageReflectPercent.Value, ScatteredReflection_DamageReflectShardStack.Value, ScatteredReflection_DamageReflectBonus.Value);
            }
            if (Empathy_Enable.Value == true)
            {
                Empathy.Initiate(Empathy_HealFor.Value);
            }
            if (Apathy_Enable.Value == true)
            {
                Apathy.Initiate(Apathy_Barrier.Value, Apathy_BarrierStack.Value, Apathy_Reduction.Value, Apathy_ReductionStack.Value);
            }
            if (MintCondition_Enable.Value == true)
            {
                MintCondition.Initiate(MintCondition_MoveSpeed.Value, MintCondition_MoveSpeedStack.Value, MintCondition_AddJumps.Value, MintCondition_AddJumpsStack.Value);
            }
            if (CorruptingParasite_Enable.Value == true)
            {
                CorruptingParasite.Initiate(CorruptingParasite_CorruptBossItems.Value);
            }
            if (NoticeOfAbsence_Enable.Value == true)
            {
                NoticeOfAbsence.Initiate(NoticeOfAbsence_SpeedBuff.Value);
            }
            if (Discovery_Enable.Value == true)
            {
                Discovery.Initiate(Discovery_ShieldAdd.Value, Discovery_MaxStacks.Value);
            }
            if (TheHermit_Enable.Value == true)
            {
                TheHermit.Initiate(TheHermit_BuffDuration.Value, TheHermit_DamageReduction.Value);
            }

            ArtifactOfCorruption.Initiate();

            Log.LogInfo("Done!");
        }
    }
}
