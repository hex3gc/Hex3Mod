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
        public const string ModVer = "0.4.2";

        public static AssetBundle MainAssets;

        public static Dictionary<string, string> ShaderLookup = new Dictionary<string, string>() // Strings of stubbed vs. real shaders
        {
            {"stubbed hopoo games/deferred/standard", "shaders/deferred/hgstandard"}
        };

        public static ManualLogSource logger;

        // Common
        public ConfigEntry<bool> ShardOfGlass_Enable() { return Config.Bind<bool>(new ConfigDefinition("Common - Shard Of Glass", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }
        public ConfigEntry<float> ShardOfGlass_DamageIncrease() { return Config.Bind<float>(new ConfigDefinition("Common - Shard Of Glass", "Damage multiplier"), 0.07f, new ConfigDescription("Percentage of base damage this item adds", null, Array.Empty<object>())); }

        public ConfigEntry<bool> BucketList_Enable() { return Config.Bind<bool>(new ConfigDefinition("Common - Bucket List", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }
        public ConfigEntry<float> BucketList_FullBuff() { return Config.Bind<float>(new ConfigDefinition("Common - Bucket List", "Speed multiplier"), 0.2f, new ConfigDescription("Percent speed increase", null, Array.Empty<object>())); }
        public ConfigEntry<float> BucketList_BuffReduce() { return Config.Bind<float>(new ConfigDefinition("Common - Bucket List", "Reduced buff multiplier"), 0.75f, new ConfigDescription("Amount that the speed buff is reduced by while fighting a boss", null, Array.Empty<object>())); }

        public ConfigEntry<bool> HopooEgg_Enable() { return Config.Bind<bool>(new ConfigDefinition("Common - Hopoo Egg", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }
        public ConfigEntry<float> HopooEgg_JumpModifier() { return Config.Bind<float>(new ConfigDefinition("Common - Hopoo Egg", "Jump height multiplier"), 0.15f, new ConfigDescription("Percent jump height increase", null, Array.Empty<object>())); }
        public ConfigEntry<float> HopooEgg_AirControlModifier() { return Config.Bind<float>(new ConfigDefinition("Common - Hopoo Egg", "Air control multiplier"), 0.1f, new ConfigDescription("Amount of added air control", null, Array.Empty<object>())); }

        public ConfigEntry<bool> AtgPrototype_Enable() { return Config.Bind<bool>(new ConfigDefinition("Common - ATG Prototype", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }
        public ConfigEntry<float> AtgPrototype_Damage() { return Config.Bind<float>(new ConfigDefinition("Common - ATG Prototype", "Damage per stack"), 1f, new ConfigDescription("Multiplier of base damage the missile deals per stack", null, Array.Empty<object>())); }
        public ConfigEntry<int> AtgPrototype_HitRequirement() { return Config.Bind<int>(new ConfigDefinition("Common - ATG Prototype", "Hits required per missile"), 10, new ConfigDescription("How many hits it should take to fire each missile", null, Array.Empty<object>())); }

        public ConfigEntry<bool> Tickets_Enable() { return Config.Bind<bool>(new ConfigDefinition("Common - 400 Tickets", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }

        // Uncommon
        public ConfigEntry<bool> ScatteredReflection_Enable() { return Config.Bind<bool>(new ConfigDefinition("Uncommon - Scattered Reflection", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }
        public ConfigEntry<float> ScatteredReflection_DamageReflectPercent() { return Config.Bind<float>(new ConfigDefinition("Uncommon - Scattered Reflection", "Damage reflect value"), 0.07f, new ConfigDescription("The percent of all total received damage to be reflected", null, Array.Empty<object>())); }
        public ConfigEntry<float> ScatteredReflection_DamageReflectShardStack() { return Config.Bind<float>(new ConfigDefinition("Uncommon - Scattered Reflection", "Damage reflect multiplier per shard"), 0.007f, new ConfigDescription("How much of a reflection bonus each Shard Of Glass adds in percentage of total damage (Caps at 90%)", null, Array.Empty<object>())); }
        public ConfigEntry<float> ScatteredReflection_DamageReflectBonus() { return Config.Bind<float>(new ConfigDefinition("Uncommon - Scattered Reflection", "Reflected damage bonus"), 0.7f, new ConfigDescription("Multiplier of how much bonus damage is added to the reflection", null, Array.Empty<object>())); }

        public ConfigEntry<bool> Empathy_Enable() { return Config.Bind<bool>(new ConfigDefinition("Uncommon - Empathy", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }
        public ConfigEntry<float> Empathy_HealFor() { return Config.Bind<float>(new ConfigDefinition("Uncommon - Empathy", "Healing amount"), 5f, new ConfigDescription("Healing per sustained hit", null, Array.Empty<object>())); }

        // Legendary
        public ConfigEntry<bool> Apathy_Enable() { return Config.Bind<bool>(new ConfigDefinition("Legendary - Apathy", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }
        public ConfigEntry<float> Apathy_Barrier() { return Config.Bind<float>(new ConfigDefinition("Legendary - Apathy", "Barrier on receiving damage"), 0.03f, new ConfigDescription("Percentage barrier gained and granted to allies when you or your allies are hit", null, Array.Empty<object>())); }
        public ConfigEntry<float> Apathy_BarrierStack() { return Config.Bind<float>(new ConfigDefinition("Legendary - Apathy", "Barrier on receiving damage per stack"), 0.02f, new ConfigDescription("Percentage barrier gained and granted to allies when you or your allies are hit per stack", null, Array.Empty<object>())); }
        public ConfigEntry<float> Apathy_Reduction() { return Config.Bind<float>(new ConfigDefinition("Legendary - Apathy", "Barrier damage reduction"), 0.2f, new ConfigDescription("Damage reduction while the item holder has barrier", null, Array.Empty<object>())); }
        public ConfigEntry<float> Apathy_ReductionStack() { return Config.Bind<float>(new ConfigDefinition("Legendary - Apathy", "Barrier damage reduction per stack"), 0.10f, new ConfigDescription("Damage reduction while the item holder has barrier (Hyperbolic, caps at 90%)", null, Array.Empty<object>())); }

        public ConfigEntry<bool> MintCondition_Enable() { return Config.Bind<bool>(new ConfigDefinition("Legendary - Mint Condition", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }
        public ConfigEntry<float> MintCondition_MoveSpeed() { return Config.Bind<float>(new ConfigDefinition("Legendary - Mint Condition", "Move speed increase"), 0.2f, new ConfigDescription("Base movement speed increase", null, Array.Empty<object>())); }
        public ConfigEntry<float> MintCondition_MoveSpeedStack() { return Config.Bind<float>(new ConfigDefinition("Legendary - Mint Condition", "Move speed increase per stack"), 0.4f, new ConfigDescription("Base movement speed increase per additional stack", null, Array.Empty<object>())); }
        public ConfigEntry<int> MintCondition_AddJumps() { return Config.Bind<int>(new ConfigDefinition("Legendary - Mint Condition", "Additional jumps"), 1, new ConfigDescription("Jump count increase", null, Array.Empty<object>())); }
        public ConfigEntry<int> MintCondition_AddJumpsStack() { return Config.Bind<int>(new ConfigDefinition("Legendary - Mint Condition", "Additional jumps per stack"), 2, new ConfigDescription("Jump count increase per additional stack", null, Array.Empty<object>())); }

        public ConfigEntry<bool> ElderMutagen_Enable() { return Config.Bind<bool>(new ConfigDefinition("Legendary - Elder Mutagen", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }
        public ConfigEntry<float> ElderMutagen_Duration() { return Config.Bind<float>(new ConfigDefinition("Legendary - Elder Mutagen", "Duration of received buffs and debuffs"), 5f, new ConfigDescription("How long, in seconds, should each buff/debuff given by this item last per stack", null, Array.Empty<object>())); }
        public ConfigEntry<float> ElderMutagen_Chance() { return Config.Bind<float>(new ConfigDefinition("Legendary - Elder Mutagen", "Chance to trigger additional buff or debuff"), 15f, new ConfigDescription("Percent chance to trigger an additional buff or debuff. WARNING: Increasing to 100% may cause an infinite loop", null, Array.Empty<object>())); }

        // Void
        public ConfigEntry<bool> CorruptingParasite_Enable() { return Config.Bind<bool>(new ConfigDefinition("Void - Corrupting Parasite", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }
        public ConfigEntry<bool> CorruptingParasite_CorruptBossItems() { return Config.Bind<bool>(new ConfigDefinition("Void - Corrupting Parasite", "Corrupt boss items"), true, new ConfigDescription("Allows the parasite to corrupt boss items", null, Array.Empty<object>())); }
        public ConfigEntry<bool> CorruptingParasite_AlternateMode() { return Config.Bind<bool>(new ConfigDefinition("Void - Corrupting Parasite", "Enable alternate mode"), false, new ConfigDescription("The parasite will first corrupt any item with available void conversions into their void counterparts. If none are available, it will revert to its normal behavior.", null, Array.Empty<object>())); }
        public ConfigEntry<bool> CorruptingParasite_AltModeOnlyConvert() { return Config.Bind<bool>(new ConfigDefinition("Void - Corrupting Parasite", "Alternate Mode: No random items"), false, new ConfigDescription("If the parasite can't find any items to convert to their void counterparts on Alternate Mode, it will do nothing instead.", null, Array.Empty<object>())); }
        public ConfigEntry<bool> CorruptingParasite_Replication() { return Config.Bind<bool>(new ConfigDefinition("Void - Corrupting Parasite", "Enable replication"), true, new ConfigDescription("The parasite can create more parasites.", null, Array.Empty<object>())); }

        public ConfigEntry<bool> NoticeOfAbsence_Enable() { return Config.Bind<bool>(new ConfigDefinition("Void - Notice Of Absence", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }
        public ConfigEntry<float> NoticeOfAbsence_SpeedBuff() { return Config.Bind<float>(new ConfigDefinition("Void - Notice Of Absence", "Speed multiplier"), 0.03f, new ConfigDescription("Percentage of base speed per void item", null, Array.Empty<object>())); }
        public ConfigEntry<float> NoticeOfAbsence_MaxSpeedBuff() { return Config.Bind<float>(new ConfigDefinition("Void - Notice Of Absence", "Maximum speed multiplier"), 5f, new ConfigDescription("Maximum speed multiplier, 500% by default. This is to avoid uncontrollably high speeds during void-focused runs.", null, Array.Empty<object>())); }

        public ConfigEntry<bool> DropOfNecrosis_Enable() { return Config.Bind<bool>(new ConfigDefinition("Void - Drop Of Necrosis", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }
        public ConfigEntry<float> DropOfNecrosis_Damage() { return Config.Bind<float>(new ConfigDefinition("Void - Drop Of Necrosis", "Added damage per stack"), 0.1f, new ConfigDescription("What fraction of base Blight damage is added to Blight per stack.", null, Array.Empty<object>())); }
        public ConfigEntry<float> DropOfNecrosis_DotChance() { return Config.Bind<float>(new ConfigDefinition("Void - Drop Of Necrosis", "Chance to inflict Blight"), 10f, new ConfigDescription("Added percent chance of inflicting Blight on hit.", null, Array.Empty<object>())); }

        public ConfigEntry<bool> Discovery_Enable() { return Config.Bind<bool>(new ConfigDefinition("Void - Discovery", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }
        public ConfigEntry<float> Discovery_ShieldAdd() { return Config.Bind<float>(new ConfigDefinition("Void - Discovery", "Shield value"), 2f, new ConfigDescription("Shield added per world interactable used", null, Array.Empty<object>())); }
        public ConfigEntry<int> Discovery_MaxStacks() { return Config.Bind<int>(new ConfigDefinition("Void - Discovery", "Maximum uses"), 100, new ConfigDescription("Maximum interactable uses per stack before shield is no longer granted", null, Array.Empty<object>())); }

        public ConfigEntry<bool> SpatteredCollection_Enable() { return Config.Bind<bool>(new ConfigDefinition("Void - Spattered Collection", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }
        public ConfigEntry<float> SpatteredCollection_IntervalReduction() { return Config.Bind<float>(new ConfigDefinition("Void - Spattered Collection", "Damage interval multiplier"), 0.9f, new ConfigDescription("For each stack of Spattered Collection, multiply time between DoT ticks by this much (Hyperbolic).", null, Array.Empty<object>())); }
        public ConfigEntry<float> SpatteredCollection_DotChance() { return Config.Bind<float>(new ConfigDefinition("Void - Spattered Collection", "Chance to inflict Blight"), 5f, new ConfigDescription("Added percent chance of inflicting Blight on hit.", null, Array.Empty<object>())); }

        public ConfigEntry<bool> TheHermit_Enable() { return Config.Bind<bool>(new ConfigDefinition("Void - The Hermit", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }
        public ConfigEntry<float> TheHermit_BuffDuration() { return Config.Bind<float>(new ConfigDefinition("Void - The Hermit", "Buff duration"), 2f, new ConfigDescription("How long in seconds the on-hit buff should last", null, Array.Empty<object>())); }
        public ConfigEntry<float> TheHermit_DamageReduction() { return Config.Bind<float>(new ConfigDefinition("Void - The Hermit", "Buff damage reduction"), 0.005f, new ConfigDescription("Damage reduced by each buff in percent", null, Array.Empty<object>())); }

        public void Awake()
        {
            Log.Init(Logger);
            Log.LogInfo("Beginning startup functions..."); // Config zone
            Log.LogInfo("Initializing configs...");

            Log.LogInfo("Creating assets...");
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Hex3Mod.vfxpass2"))
            {
                MainAssets = AssetBundle.LoadFromStream(stream); // Load mainassets into stream
            }

            var materialAssets = MainAssets.LoadAllAssets<Material>();
            foreach (Material material in materialAssets)
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

            // Common
            if (ShardOfGlass_Enable().Value == true){ ShardOfGlass.Initiate(ShardOfGlass_DamageIncrease().Value); }
            if (BucketList_Enable().Value == true){ BucketList.Initiate(BucketList_FullBuff().Value, BucketList_BuffReduce().Value); }
            if (HopooEgg_Enable().Value == true){ HopooEgg.Initiate(HopooEgg_JumpModifier().Value, HopooEgg_AirControlModifier().Value); }
            if (AtgPrototype_Enable().Value == true){ AtgPrototype.Initiate(AtgPrototype_Damage().Value, AtgPrototype_HitRequirement().Value); }
            if (Tickets_Enable().Value == true){ Tickets.Initiate(); }
            // Uncommon
            if (ScatteredReflection_Enable().Value == true){ ScatteredReflection.Initiate(ScatteredReflection_DamageReflectPercent().Value, ScatteredReflection_DamageReflectShardStack().Value, ScatteredReflection_DamageReflectBonus().Value); }
            if (Empathy_Enable().Value == true){ Empathy.Initiate(Empathy_HealFor().Value); }
            // Legendary
            if (Apathy_Enable().Value == true){ Apathy.Initiate(Apathy_Barrier().Value, Apathy_BarrierStack().Value, Apathy_Reduction().Value, Apathy_ReductionStack().Value); }
            if (MintCondition_Enable().Value == true){ MintCondition.Initiate(MintCondition_MoveSpeed().Value, MintCondition_MoveSpeedStack().Value, MintCondition_AddJumps().Value, MintCondition_AddJumpsStack().Value); }
            if (ElderMutagen_Enable().Value == true){ ElderMutagen.Initiate(ElderMutagen_Duration().Value, ElderMutagen_Chance().Value); }
            // Void
            if (CorruptingParasite_Enable().Value == true){ CorruptingParasite.Initiate(CorruptingParasite_CorruptBossItems().Value, CorruptingParasite_AlternateMode().Value, CorruptingParasite_Replication().Value, CorruptingParasite_AltModeOnlyConvert().Value); }
            if (NoticeOfAbsence_Enable().Value == true){ NoticeOfAbsence.Initiate(NoticeOfAbsence_SpeedBuff().Value, NoticeOfAbsence_MaxSpeedBuff().Value); }
            if (DropOfNecrosis_Enable().Value == true) { DropOfNecrosis.Initiate(DropOfNecrosis_Damage().Value, DropOfNecrosis_DotChance().Value); }
            if (Discovery_Enable().Value == true){ Discovery.Initiate(Discovery_ShieldAdd().Value, Discovery_MaxStacks().Value); }
            if (SpatteredCollection_Enable().Value == true) { SpatteredCollection.Initiate(SpatteredCollection_IntervalReduction().Value, SpatteredCollection_DotChance().Value); }
            if (TheHermit_Enable().Value == true){ TheHermit.Initiate(TheHermit_BuffDuration().Value, TheHermit_DamageReduction().Value); }
            // Artifacts
            ArtifactOfCorruption.Initiate();

            Log.LogInfo("Done!");
        }
    }
}
