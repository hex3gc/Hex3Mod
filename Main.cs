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
        public const string ModVer = "0.4.0";

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

        public static ConfigEntry<bool> Tickets_Enable;

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

        public static ConfigEntry<bool> ElderMutagen_Enable;
        public static ConfigEntry<float> ElderMutagen_Duration;
        public static ConfigEntry<float> ElderMutagen_Chance;

        public static ConfigEntry<bool> CorruptingParasite_Enable;
        public static ConfigEntry<bool> CorruptingParasite_CorruptBossItems;
        public static ConfigEntry<bool> CorruptingParasite_AlternateMode;
        public static ConfigEntry<bool> CorruptingParasite_AltModeOnlyConvert;
        public static ConfigEntry<bool> CorruptingParasite_Replication;

        public static ConfigEntry<bool> NoticeOfAbsence_Enable;
        public static ConfigEntry<float> NoticeOfAbsence_SpeedBuff;
        public static ConfigEntry<float> NoticeOfAbsence_MaxSpeedBuff;

        public static ConfigEntry<bool> DropOfNecrosis_Enable;
        public static ConfigEntry<float> DropOfNecrosis_Damage;
        public static ConfigEntry<float> DropOfNecrosis_DotChance;

        public static ConfigEntry<bool> Discovery_Enable;
        public static ConfigEntry<float> Discovery_ShieldAdd;
        public static ConfigEntry<int> Discovery_MaxStacks;

        public static ConfigEntry<bool> SpatteredCollection_Enable;
        public static ConfigEntry<float> SpatteredCollection_IntervalReduction;
        public static ConfigEntry<float> SpatteredCollection_DotChance;

        public static ConfigEntry<bool> TheHermit_Enable;
        public static ConfigEntry<float> TheHermit_BuffDuration;
        public static ConfigEntry<float> TheHermit_DamageReduction;

        public void Awake()
        {
            Log.Init(Logger);
            Log.LogInfo("Beginning startup functions..."); // Config zone
            Log.LogInfo("Initializing configs...");

            ShardOfGlass_Enable = Config.Bind<bool>(new ConfigDefinition("Common - Shard Of Glass", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>()));
            ShardOfGlass_DamageIncrease = Config.Bind<float>(new ConfigDefinition("Common - Shard Of Glass", "Damage multiplier"), 0.07f, new ConfigDescription("Percentage of base damage this item adds", null, Array.Empty<object>()));

            BucketList_Enable = Config.Bind<bool>(new ConfigDefinition("Common - Bucket List", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>()));
            BucketList_FullBuff = Config.Bind<float>(new ConfigDefinition("Common - Bucket List", "Speed multiplier"), 0.2f, new ConfigDescription("Percent speed increase", null, Array.Empty<object>()));
            BucketList_BuffReduce = Config.Bind<float>(new ConfigDefinition("Common - Bucket List", "Reduced buff multiplier"), 0.75f, new ConfigDescription("Amount that the speed buff is reduced by while fighting a boss", null, Array.Empty<object>()));

            HopooEgg_Enable = Config.Bind<bool>(new ConfigDefinition("Common - Hopoo Egg", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>()));
            HopooEgg_JumpModifier = Config.Bind<float>(new ConfigDefinition("Common - Hopoo Egg", "Jump height multiplier"), 0.15f, new ConfigDescription("Percent jump height increase", null, Array.Empty<object>()));
            HopooEgg_AirControlModifier = Config.Bind<float>(new ConfigDefinition("Common - Hopoo Egg", "Air control multiplier"), 0.1f, new ConfigDescription("Amount of added air control", null, Array.Empty<object>()));

            AtgPrototype_Enable = Config.Bind<bool>(new ConfigDefinition("Common - ATG Prototype", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>()));
            AtgPrototype_Damage = Config.Bind<float>(new ConfigDefinition("Common - ATG Prototype", "Damage per stack"), 1f, new ConfigDescription("Multiplier of base damage the missile deals per stack", null, Array.Empty<object>()));
            AtgPrototype_HitRequirement = Config.Bind<int>(new ConfigDefinition("Common - ATG Prototype", "Hits required per missile"), 10, new ConfigDescription("How many hits it should take to fire each missile", null, Array.Empty<object>()));

            Tickets_Enable = Config.Bind<bool>(new ConfigDefinition("Common - 400 Tickets", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>()));

            ScatteredReflection_Enable = Config.Bind<bool>(new ConfigDefinition("Uncommon - Scattered Reflection", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>()));
            ScatteredReflection_DamageReflectPercent = Config.Bind<float>(new ConfigDefinition("Uncommon - Scattered Reflection", "Damage reflect value"), 0.07f, new ConfigDescription("The percent of all total received damage to be reflected", null, Array.Empty<object>()));
            ScatteredReflection_DamageReflectShardStack = Config.Bind<float>(new ConfigDefinition("Uncommon - Scattered Reflection", "Damage reflect multiplier per shard"), 0.007f, new ConfigDescription("How much of a reflection bonus each Shard Of Glass adds in percentage of total damage (Caps at 90%)", null, Array.Empty<object>()));
            ScatteredReflection_DamageReflectBonus = Config.Bind<float>(new ConfigDefinition("Uncommon - Scattered Reflection", "Reflected damage bonus"), 0.7f, new ConfigDescription("Multiplier of how much bonus damage is added to the reflection", null, Array.Empty<object>()));

            Empathy_Enable = Config.Bind<bool>(new ConfigDefinition("Uncommon - Empathy", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>()));
            Empathy_HealFor = Config.Bind<float>(new ConfigDefinition("Uncommon - Empathy", "Healing amount"), 5f, new ConfigDescription("Healing per sustained hit", null, Array.Empty<object>()));

            Apathy_Enable = Config.Bind<bool>(new ConfigDefinition("Legendary - Apathy", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>()));
            Apathy_Barrier = Config.Bind<float>(new ConfigDefinition("Legendary - Apathy", "Barrier on receiving damage"), 0.03f, new ConfigDescription("Percentage barrier gained and granted to allies when you or your allies are hit", null, Array.Empty<object>()));
            Apathy_BarrierStack = Config.Bind<float>(new ConfigDefinition("Legendary - Apathy", "Barrier on receiving damage per stack"), 0.02f, new ConfigDescription("Percentage barrier gained and granted to allies when you or your allies are hit per stack", null, Array.Empty<object>()));
            Apathy_Reduction = Config.Bind<float>(new ConfigDefinition("Legendary - Apathy", "Barrier damage reduction"), 0.2f, new ConfigDescription("Damage reduction while the item holder has barrier", null, Array.Empty<object>()));
            Apathy_ReductionStack = Config.Bind<float>(new ConfigDefinition("Legendary - Apathy", "Barrier damage reduction per stack"), 0.10f, new ConfigDescription("Damage reduction while the item holder has barrier (Hyperbolic, caps at 90%)", null, Array.Empty<object>()));

            MintCondition_Enable = Config.Bind<bool>(new ConfigDefinition("Legendary - Mint Condition", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>()));
            MintCondition_MoveSpeed = Config.Bind<float>(new ConfigDefinition("Legendary - Mint Condition", "Move speed increase"), 0.2f, new ConfigDescription("Base movement speed increase", null, Array.Empty<object>()));
            MintCondition_MoveSpeedStack = Config.Bind<float>(new ConfigDefinition("Legendary - Mint Condition", "Move speed increase per stack"), 0.4f, new ConfigDescription("Base movement speed increase per additional stack", null, Array.Empty<object>()));
            MintCondition_AddJumps = Config.Bind<int>(new ConfigDefinition("Legendary - Mint Condition", "Additional jumps"), 1, new ConfigDescription("Jump count increase", null, Array.Empty<object>()));
            MintCondition_AddJumpsStack = Config.Bind<int>(new ConfigDefinition("Legendary - Mint Condition", "Additional jumps per stack"), 2, new ConfigDescription("Jump count increase per additional stack", null, Array.Empty<object>()));

            ElderMutagen_Enable = Config.Bind<bool>(new ConfigDefinition("Legendary - Elder Mutagen", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>()));
            ElderMutagen_Duration = Config.Bind<float>(new ConfigDefinition("Legendary - Elder Mutagen", "Duration of received buffs and debuffs"), 5f, new ConfigDescription("How long, in seconds, should each buff/debuff given by this item last per stack", null, Array.Empty<object>()));
            ElderMutagen_Chance = Config.Bind<float>(new ConfigDefinition("Legendary - Elder Mutagen", "Chance to trigger additional buff or debuff"), 15f, new ConfigDescription("Percent chance to trigger an additional buff or debuff. WARNING: Increasing to 100% may cause an infinite loop", null, Array.Empty<object>()));

            CorruptingParasite_Enable = Config.Bind<bool>(new ConfigDefinition("Void - Corrupting Parasite", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>()));
            CorruptingParasite_CorruptBossItems = Config.Bind<bool>(new ConfigDefinition("Void - Corrupting Parasite", "Corrupt boss items"), true, new ConfigDescription("Allows the parasite to corrupt boss items", null, Array.Empty<object>()));
            CorruptingParasite_AlternateMode = Config.Bind<bool>(new ConfigDefinition("Void - Corrupting Parasite", "Enable alternate mode"), false, new ConfigDescription("The parasite will first corrupt any item with available void conversions into their void counterparts. If none are available, it will revert to its normal behavior.", null, Array.Empty<object>()));
            CorruptingParasite_AltModeOnlyConvert = Config.Bind<bool>(new ConfigDefinition("Void - Corrupting Parasite", "Alternate Mode: No random items"), false, new ConfigDescription("If the parasite can't find any items to convert to their void counterparts on Alternate Mode, it will do nothing instead.", null, Array.Empty<object>()));
            CorruptingParasite_Replication = Config.Bind<bool>(new ConfigDefinition("Void - Corrupting Parasite", "Enable replication"), true, new ConfigDescription("The parasite can create more parasites.", null, Array.Empty<object>()));

            NoticeOfAbsence_Enable = Config.Bind<bool>(new ConfigDefinition("Void - Notice Of Absence", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>()));
            NoticeOfAbsence_SpeedBuff = Config.Bind<float>(new ConfigDefinition("Void - Notice Of Absence", "Speed multiplier"), 0.03f, new ConfigDescription("Percentage of base speed per void item", null, Array.Empty<object>()));
            NoticeOfAbsence_MaxSpeedBuff = Config.Bind<float>(new ConfigDefinition("Void - Notice Of Absence", "Maximum speed multiplier"), 5f, new ConfigDescription("Maximum speed multiplier, 500% by default. This is to avoid uncontrollably high speeds during void-focused runs.", null, Array.Empty<object>()));

            DropOfNecrosis_Enable = Config.Bind<bool>(new ConfigDefinition("Void - Drop Of Necrosis", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>()));
            DropOfNecrosis_Damage = Config.Bind<float>(new ConfigDefinition("Void - Drop Of Necrosis", "Added damage per stack"), 0.1f, new ConfigDescription("What fraction of base Blight damage is added to Blight per stack.", null, Array.Empty<object>()));
            DropOfNecrosis_DotChance = Config.Bind<float>(new ConfigDefinition("Void - Drop Of Necrosis", "Chance to inflict Blight"), 10f, new ConfigDescription("Added percent chance of inflicting Blight on hit.", null, Array.Empty<object>()));

            Discovery_Enable = Config.Bind<bool>(new ConfigDefinition("Void - Discovery", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>()));
            Discovery_ShieldAdd = Config.Bind<float>(new ConfigDefinition("Void - Discovery", "Shield value"), 2f, new ConfigDescription("Shield added per world interactable used", null, Array.Empty<object>()));
            Discovery_MaxStacks = Config.Bind<int>(new ConfigDefinition("Void - Discovery", "Maximum uses"), 100, new ConfigDescription("Maximum interactable uses per stack before shield is no longer granted", null, Array.Empty<object>()));

            SpatteredCollection_Enable = Config.Bind<bool>(new ConfigDefinition("Void - Spattered Collection", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>()));
            SpatteredCollection_IntervalReduction = Config.Bind<float>(new ConfigDefinition("Void - Spattered Collection", "Damage interval multiplier"), 0.9f, new ConfigDescription("For each stack of Spattered Collection, multiply time between DoT ticks by this much (Hyperbolic).", null, Array.Empty<object>()));
            SpatteredCollection_DotChance = Config.Bind<float>(new ConfigDefinition("Void - Spattered Collection", "Chance to inflict Blight"), 5f, new ConfigDescription("Added percent chance of inflicting Blight on hit.", null, Array.Empty<object>()));

            TheHermit_Enable = Config.Bind<bool>(new ConfigDefinition("Void - The Hermit", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>()));
            TheHermit_BuffDuration = Config.Bind<float>(new ConfigDefinition("Void - The Hermit", "Buff duration"), 2f, new ConfigDescription("How long in seconds the on-hit buff should last", null, Array.Empty<object>()));
            TheHermit_DamageReduction = Config.Bind<float>(new ConfigDefinition("Void - The Hermit", "Buff damage reduction"), 0.005f, new ConfigDescription("Damage reduced by each buff in percent", null, Array.Empty<object>()));


            Log.LogInfo("Creating assets...");
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Hex3Mod.vfxpass2"))
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

            if (ShardOfGlass_Enable.Value == true){ ShardOfGlass.Initiate(ShardOfGlass_DamageIncrease.Value); }
            if (BucketList_Enable.Value == true){ BucketList.Initiate(BucketList_FullBuff.Value, BucketList_BuffReduce.Value); }
            if (HopooEgg_Enable.Value == true){ HopooEgg.Initiate(HopooEgg_JumpModifier.Value, HopooEgg_AirControlModifier.Value); }
            if (AtgPrototype_Enable.Value == true){ AtgPrototype.Initiate(AtgPrototype_Damage.Value, AtgPrototype_HitRequirement.Value); }
            if (Tickets_Enable.Value == true){ Tickets.Initiate(); }
            if (ScatteredReflection_Enable.Value == true){ ScatteredReflection.Initiate(ScatteredReflection_DamageReflectPercent.Value, ScatteredReflection_DamageReflectShardStack.Value, ScatteredReflection_DamageReflectBonus.Value); }
            if (Empathy_Enable.Value == true){ Empathy.Initiate(Empathy_HealFor.Value); }
            if (Apathy_Enable.Value == true){ Apathy.Initiate(Apathy_Barrier.Value, Apathy_BarrierStack.Value, Apathy_Reduction.Value, Apathy_ReductionStack.Value); }
            if (MintCondition_Enable.Value == true){ MintCondition.Initiate(MintCondition_MoveSpeed.Value, MintCondition_MoveSpeedStack.Value, MintCondition_AddJumps.Value, MintCondition_AddJumpsStack.Value); }
            if (ElderMutagen_Enable.Value == true){ ElderMutagen.Initiate(ElderMutagen_Duration.Value, ElderMutagen_Chance.Value); }
            if (CorruptingParasite_Enable.Value == true){ CorruptingParasite.Initiate(CorruptingParasite_CorruptBossItems.Value, CorruptingParasite_AlternateMode.Value, CorruptingParasite_Replication.Value, CorruptingParasite_AltModeOnlyConvert.Value); }
            if (NoticeOfAbsence_Enable.Value == true){ NoticeOfAbsence.Initiate(NoticeOfAbsence_SpeedBuff.Value, NoticeOfAbsence_MaxSpeedBuff.Value); }
            if (DropOfNecrosis_Enable.Value == true) { DropOfNecrosis.Initiate(DropOfNecrosis_Damage.Value, DropOfNecrosis_DotChance.Value); }
            if (Discovery_Enable.Value == true){ Discovery.Initiate(Discovery_ShieldAdd.Value, Discovery_MaxStacks.Value); }
            if (SpatteredCollection_Enable.Value == true) { SpatteredCollection.Initiate(SpatteredCollection_IntervalReduction.Value, SpatteredCollection_DotChance.Value); }
            if (TheHermit_Enable.Value == true){ TheHermit.Initiate(TheHermit_BuffDuration.Value, TheHermit_DamageReduction.Value); }
            ArtifactOfCorruption.Initiate();

            Log.LogInfo("Done!");
        }
    }
}
