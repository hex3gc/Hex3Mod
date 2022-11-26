using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using R2API;
using R2API.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Hex3Mod.Items;
using Hex3Mod.Artifacts;
using Hex3Mod.Logging;
using Hex3Mod.HelperClasses;
using BetterUI;

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
        public const string ModVer = "2.0.0";

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
        public ConfigEntry<float> HopooEgg_JumpModifier() { return Config.Bind<float>(new ConfigDefinition("Common - Hopoo Egg", "Jump height multiplier"), 0.2f, new ConfigDescription("Percent jump height increase", null, Array.Empty<object>())); }
        public ConfigEntry<float> HopooEgg_FallDamageReduction() { return Config.Bind<float>(new ConfigDefinition("Common - Hopoo Egg", "Fall damage reduction"), 0.15f, new ConfigDescription("How much of all received fall damage is reduced per stack", null, Array.Empty<object>())); }

        public ConfigEntry<bool> AtgPrototype_Enable() { return Config.Bind<bool>(new ConfigDefinition("Common - ATG Prototype", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }
        public ConfigEntry<float> AtgPrototype_Damage() { return Config.Bind<float>(new ConfigDefinition("Common - ATG Prototype", "Damage per stack"), 0.8f, new ConfigDescription("Multiplier of base damage the missile deals per stack", null, Array.Empty<object>())); }
        public ConfigEntry<int> AtgPrototype_HitRequirement() { return Config.Bind<int>(new ConfigDefinition("Common - ATG Prototype", "Hits required per missile"), 10, new ConfigDescription("How many hits it should take to fire each missile", null, Array.Empty<object>())); }

        public ConfigEntry<bool> Tickets_Enable() { return Config.Bind<bool>(new ConfigDefinition("Common - 400 Tickets", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }

        public ConfigEntry<bool> Balance_Enable() { return Config.Bind<bool>(new ConfigDefinition("Common - Balance", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }
        public ConfigEntry<float> Balance_MaxDodge() { return Config.Bind<float>(new ConfigDefinition("Common - Balance", "Max added dodge chance"), 20f, new ConfigDescription("Maximum (standing still) chance to dodge, stacking hyperbolically", null, Array.Empty<object>())); }

        public ConfigEntry<bool> MinersHelmet_Enable() { return Config.Bind<bool>(new ConfigDefinition("Common - Miners Helmet", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }
        public ConfigEntry<float> MinersHelmet_CooldownReduction() { return Config.Bind<float>(new ConfigDefinition("Common - Miners Helmet", "Cooldown reduction"), 2f, new ConfigDescription("Reduce cooldowns by this many seconds every time a small chest's worth is earned", null, Array.Empty<object>())); }
        public ConfigEntry<int> MinersHelmet_GoldPerProc() { return Config.Bind<int>(new ConfigDefinition("Common - Miners Helmet", "Gold requirement"), 25, new ConfigDescription("Gold required to proc cooldown reduction (Scaling with time)", null, Array.Empty<object>())); }

        // Uncommon
        public ConfigEntry<bool> ScatteredReflection_Enable() { return Config.Bind<bool>(new ConfigDefinition("Uncommon - Scattered Reflection", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }
        public ConfigEntry<float> ScatteredReflection_DamageReflectPercent() { return Config.Bind<float>(new ConfigDefinition("Uncommon - Scattered Reflection", "Damage reflect value"), 0.07f, new ConfigDescription("The percent of all total received damage to be reflected", null, Array.Empty<object>())); }
        public ConfigEntry<float> ScatteredReflection_DamageReflectShardStack() { return Config.Bind<float>(new ConfigDefinition("Uncommon - Scattered Reflection", "Damage reflect multiplier per shard"), 0.007f, new ConfigDescription("How much of a reflection bonus each Shard Of Glass adds in percentage of total damage (Caps at 90%)", null, Array.Empty<object>())); }
        public ConfigEntry<float> ScatteredReflection_DamageReflectBonus() { return Config.Bind<float>(new ConfigDefinition("Uncommon - Scattered Reflection", "Reflected damage bonus"), 0.7f, new ConfigDescription("Multiplier of how much bonus damage is added to the reflection", null, Array.Empty<object>())); }

        public ConfigEntry<bool> Empathy_Enable() { return Config.Bind<bool>(new ConfigDefinition("Uncommon - Empathy", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }
        public ConfigEntry<float> Empathy_HealingFactor() { return Config.Bind<float>(new ConfigDefinition("Uncommon - Empathy", "Healing factor"), 0.2f, new ConfigDescription("Fraction of ally's received damage converted into healing per stack", null, Array.Empty<object>())); }

        public ConfigEntry<bool> ScavengersPack_Enable() { return Config.Bind<bool>(new ConfigDefinition("Uncommon - Scavengers Pack", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }
        public ConfigEntry<int> ScavengersPack_Uses() { return Config.Bind<int>(new ConfigDefinition("Uncommon - Scavengers Pack", "Maximum uses"), 3, new ConfigDescription("Times the Scavenger's Pack can be used before breaking.", null, Array.Empty<object>())); }
        public ConfigEntry<bool> ScavengersPack_RegenScrap() { return Config.Bind<bool>(new ConfigDefinition("Uncommon - Scavengers Pack", "VANILLA: Regenerating Scrap"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>())); }
        public ConfigEntry<bool> ScavengersPack_PowerElixir() { return Config.Bind<bool>(new ConfigDefinition("Uncommon - Scavengers Pack", "VANILLA: Power Elixir"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>())); }
        public ConfigEntry<bool> ScavengersPack_DelicateWatch() { return Config.Bind<bool>(new ConfigDefinition("Uncommon - Scavengers Pack", "VANILLA: Delicate Watch"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>())); }
        public ConfigEntry<bool> ScavengersPack_Dios() { return Config.Bind<bool>(new ConfigDefinition("Uncommon - Scavengers Pack", "VANILLA: Dios Best Friend"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>())); }
        public ConfigEntry<bool> ScavengersPack_VoidDios() { return Config.Bind<bool>(new ConfigDefinition("Uncommon - Scavengers Pack", "VANILLA: Pluripotent Larva"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>())); }
        public ConfigEntry<bool> ScavengersPack_RustedKey() { return Config.Bind<bool>(new ConfigDefinition("Uncommon - Scavengers Pack", "WOLFO QOL: Rusted Key"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>())); }
        public ConfigEntry<bool> ScavengersPack_EncrustedKey() { return Config.Bind<bool>(new ConfigDefinition("Uncommon - Scavengers Pack", "WOLFO QOL: Encrusted Key"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>())); }
        public ConfigEntry<bool> ScavengersPack_FourHundredTickets() { return Config.Bind<bool>(new ConfigDefinition("Uncommon - Scavengers Pack", "HEX3MOD: 400 Tickets"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>())); }
        public ConfigEntry<bool> ScavengersPack_OneTicket() { return Config.Bind<bool>(new ConfigDefinition("Uncommon - Scavengers Pack", "HEX3MOD: One Ticket"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>())); }
        public ConfigEntry<bool> ScavengersPack_ShopCard() { return Config.Bind<bool>(new ConfigDefinition("Uncommon - Scavengers Pack", "MYSTICS ITEMS: Platinum Card"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>())); }
        public ConfigEntry<bool> ScavengersPack_CuteBow() { return Config.Bind<bool>(new ConfigDefinition("Uncommon - Scavengers Pack", "MYSTICS ITEMS: Cutesy Bow"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>())); }
        public ConfigEntry<bool> ScavengersPack_ClockworkMechanism() { return Config.Bind<bool>(new ConfigDefinition("Uncommon - Scavengers Pack", "VANILLAVOID: Clockwork Mechanism"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>())); }
        public ConfigEntry<bool> ScavengersPack_Vials() { return Config.Bind<bool>(new ConfigDefinition("Uncommon - Scavengers Pack", "VANILLAVOID: Enhancement Vials"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>())); }
        public ConfigEntry<bool> ScavengersPack_BrokenChopsticks() { return Config.Bind<bool>(new ConfigDefinition("Uncommon - Scavengers Pack", "HOLYCRAPFORKISBACK: Sharp Chopsticks"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>())); }
        public ConfigEntry<bool> ScavengersPack_AbyssalCartridge() { return Config.Bind<bool>(new ConfigDefinition("Uncommon - Scavengers Pack", "SPIKESTRIP: Abyssal Cartridge"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>())); }
        public ConfigEntry<bool> ScavengersPack_Singularity() { return Config.Bind<bool>(new ConfigDefinition("Uncommon - Scavengers Pack", "SPIKESTRIP: Singularity"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>())); }

        public ConfigEntry<bool> TheUnforgivable_Enable() { return Config.Bind<bool>(new ConfigDefinition("Uncommon - The Unforgivable", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }
        public ConfigEntry<float> TheUnforgivable_Interval() { return Config.Bind<float>(new ConfigDefinition("Uncommon - The Unforgivable", "Activation interval"), 8f, new ConfigDescription("Activate your on-kill effects every time this amount of seconds passes", null, Array.Empty<object>())); }

        public ConfigEntry<bool> OverkillOverdrive_Enable() { return Config.Bind<bool>(new ConfigDefinition("Uncommon - Overkill Overdrive", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }
        public ConfigEntry<float> OverkillOverdrive_ZoneIncrease() { return Config.Bind<float>(new ConfigDefinition("Uncommon - Overkill Overdrive", "Zone size percentage increase"), 20f, new ConfigDescription("How much larger affected zones should be in percentage", null, Array.Empty<object>())); }
        public ConfigEntry<bool> OverkillOverdrive_AltMode() { return Config.Bind<bool>(new ConfigDefinition("Uncommon - Overkill Overdrive", "Alt mode"), false, new ConfigDescription("Overkill Overdrive no longer affects holdout zones. Instead, it increases area buffs by Percentage Increase * 1.5", null, Array.Empty<object>())); }
        // Legendary
        public ConfigEntry<bool> Apathy_Enable() { return Config.Bind<bool>(new ConfigDefinition("Legendary - Apathy", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }
        public ConfigEntry<float> Apathy_HealthIncrease() { return Config.Bind<float>(new ConfigDefinition("Legendary - Apathy", "Max health gain on ally death"), 0.04f, new ConfigDescription("Percent max health gained per ally death", null, Array.Empty<object>())); }
        public ConfigEntry<float> Apathy_DamageIncrease() { return Config.Bind<float>(new ConfigDefinition("Legendary - Apathy", "Damage increase on ally death"), 0.04f, new ConfigDescription("Percent damage increase per ally death", null, Array.Empty<object>())); }

        public ConfigEntry<bool> MintCondition_Enable() { return Config.Bind<bool>(new ConfigDefinition("Legendary - Mint Condition", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }
        public ConfigEntry<float> MintCondition_MoveSpeed() { return Config.Bind<float>(new ConfigDefinition("Legendary - Mint Condition", "Move speed increase"), 0.2f, new ConfigDescription("Base movement speed increase", null, Array.Empty<object>())); }
        public ConfigEntry<float> MintCondition_MoveSpeedStack() { return Config.Bind<float>(new ConfigDefinition("Legendary - Mint Condition", "Move speed increase per stack"), 0.4f, new ConfigDescription("Base movement speed increase per additional stack", null, Array.Empty<object>())); }
        public ConfigEntry<int> MintCondition_AddJumps() { return Config.Bind<int>(new ConfigDefinition("Legendary - Mint Condition", "Additional jumps"), 1, new ConfigDescription("Jump count increase", null, Array.Empty<object>())); }
        public ConfigEntry<int> MintCondition_AddJumpsStack() { return Config.Bind<int>(new ConfigDefinition("Legendary - Mint Condition", "Additional jumps per stack"), 2, new ConfigDescription("Jump count increase per additional stack", null, Array.Empty<object>())); }

        public ConfigEntry<bool> ElderMutagen_Enable() { return Config.Bind<bool>(new ConfigDefinition("Legendary - Elder Mutagen", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }
        public ConfigEntry<float> ElderMutagen_AddDuration() { return Config.Bind<float>(new ConfigDefinition("Legendary - Elder Mutagen", "Added duration to buffs and debuffs"), 3f, new ConfigDescription("How much longer, in seconds, should each buff/debuff affected by this item last per stack", null, Array.Empty<object>())); }
        public ConfigEntry<float> ElderMutagen_CooldownReduction() { return Config.Bind<float>(new ConfigDefinition("Legendary - Elder Mutagen", "Item cooldown reduction"), 2f, new ConfigDescription("How much shorter, in seconds, should each item cooldown be", null, Array.Empty<object>())); }

        public ConfigEntry<bool> DoNotEat_Enable() { return Config.Bind<bool>(new ConfigDefinition("Legendary - Do Not Eat", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }
        public ConfigEntry<float> DoNotEat_PearlChancePerStack() { return Config.Bind<float>(new ConfigDefinition("Legendary - Do Not Eat", "Pearl Chance"), 8f, new ConfigDescription("Percent chance that a Pearl or Irradiant Pearl will drop from a chest.", null, Array.Empty<object>())); }
        public ConfigEntry<float> DoNotEat_IrradiantChance() { return Config.Bind<float>(new ConfigDefinition("Legendary - Do Not Eat", "Irradiant Pearl Chance"), 20f, new ConfigDescription("Percent chance that an Irradiant Pearl will drop instead of a Pearl.", null, Array.Empty<object>())); }

        public ConfigEntry<bool> TaxManStatement_Enable() { return Config.Bind<bool>(new ConfigDefinition("Legendary - The Tax Mans Statement", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }
        public ConfigEntry<float> TaxManStatement_ChanceToInflict() { return Config.Bind<float>(new ConfigDefinition("Legendary - The Tax Mans Statement", "Chance to inflict on hit"), 5f, new ConfigDescription("Percent chance that hit enemies will be taxed per stack", null, Array.Empty<object>())); }
        public ConfigEntry<float> TaxManStatement_DamagePerTax() { return Config.Bind<float>(new ConfigDefinition("Legendary - The Tax Mans Statement", "Damage percentage per tax"), 5f, new ConfigDescription("Percent damage the enemy takes whenever they use an ability. Halved against bosses", null, Array.Empty<object>())); }
        public ConfigEntry<float> TaxManStatement_BaseGoldPerTax() { return Config.Bind<float>(new ConfigDefinition("Legendary - The Tax Mans Statement", "Gold per tax"), 2f, new ConfigDescription("Base gold gained for each taxation, scaling with time", null, Array.Empty<object>())); }

        // Void
        public ConfigEntry<bool> CorruptingParasite_Enable() { return Config.Bind<bool>(new ConfigDefinition("Void - Corrupting Parasite", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }
        public ConfigEntry<bool> CorruptingParasite_CorruptBossItems() { return Config.Bind<bool>(new ConfigDefinition("Void - Corrupting Parasite", "Corrupt boss items"), false, new ConfigDescription("Allows the parasite to corrupt boss items", null, Array.Empty<object>())); }
        public ConfigEntry<int> CorruptingParasite_ItemsPerStage() { return Config.Bind<int>(new ConfigDefinition("Void - Corrupting Parasite", "Items per stage"), 1, new ConfigDescription("Number of items corrupted each stage", null, Array.Empty<object>())); }

        public ConfigEntry<bool> NoticeOfAbsence_Enable() { return Config.Bind<bool>(new ConfigDefinition("Void - Notice Of Absence", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }
        public ConfigEntry<float> NoticeOfAbsence_SpeedBuff() { return Config.Bind<float>(new ConfigDefinition("Void - Notice Of Absence", "Speed multiplier"), 0.03f, new ConfigDescription("Percentage of base speed per void item", null, Array.Empty<object>())); }
        public ConfigEntry<float> NoticeOfAbsence_MaxSpeedBuff() { return Config.Bind<float>(new ConfigDefinition("Void - Notice Of Absence", "Maximum speed multiplier"), 5f, new ConfigDescription("Maximum speed multiplier, 500% by default. This is to avoid uncontrollably high speeds during void-focused runs.", null, Array.Empty<object>())); }

        public ConfigEntry<bool> DropOfNecrosis_Enable() { return Config.Bind<bool>(new ConfigDefinition("Void - Drop Of Necrosis", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }
        public ConfigEntry<float> DropOfNecrosis_Damage() { return Config.Bind<float>(new ConfigDefinition("Void - Drop Of Necrosis", "Added damage per stack"), 0.1f, new ConfigDescription("What fraction of base Blight damage is added to Blight per stack.", null, Array.Empty<object>())); }
        public ConfigEntry<float> DropOfNecrosis_DotChance() { return Config.Bind<float>(new ConfigDefinition("Void - Drop Of Necrosis", "Chance to inflict Blight"), 10f, new ConfigDescription("Added percent chance of inflicting Blight on hit.", null, Array.Empty<object>())); }

        public ConfigEntry<bool> CaptainsFavor_Enable() { return Config.Bind<bool>(new ConfigDefinition("Void - Captains Favor", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }
        public ConfigEntry<float> CaptainsFavor_InteractableIncrease() { return Config.Bind<float>(new ConfigDefinition("Void - Captains Favor", "Interactables increase"), 10f, new ConfigDescription("Percentage value that interactable credits should be increased by.", null, Array.Empty<object>())); }

        public ConfigEntry<bool> Discovery_Enable() { return Config.Bind<bool>(new ConfigDefinition("Void - Discovery", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }
        public ConfigEntry<float> Discovery_ShieldAdd() { return Config.Bind<float>(new ConfigDefinition("Void - Discovery", "Shield value"), 3f, new ConfigDescription("Shield added per world interactable used", null, Array.Empty<object>())); }
        public ConfigEntry<int> Discovery_MaxStacks() { return Config.Bind<int>(new ConfigDefinition("Void - Discovery", "Maximum uses"), 100, new ConfigDescription("Maximum interactable uses per stack before shield is no longer granted", null, Array.Empty<object>())); }

        public ConfigEntry<bool> SpatteredCollection_Enable() { return Config.Bind<bool>(new ConfigDefinition("Void - Spattered Collection", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }
        public ConfigEntry<float> SpatteredCollection_IntervalReduction() { return Config.Bind<float>(new ConfigDefinition("Void - Spattered Collection", "Damage interval multiplier"), 0.9f, new ConfigDescription("For each stack of Spattered Collection, multiply time between DoT ticks by this much (Hyperbolic).", null, Array.Empty<object>())); }
        public ConfigEntry<float> SpatteredCollection_DotChance() { return Config.Bind<float>(new ConfigDefinition("Void - Spattered Collection", "Chance to inflict Blight"), 5f, new ConfigDescription("Added percent chance of inflicting Blight on hit.", null, Array.Empty<object>())); }

        public ConfigEntry<bool> TheHermit_Enable() { return Config.Bind<bool>(new ConfigDefinition("Void - The Hermit", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }
        public ConfigEntry<float> TheHermit_BuffDuration() { return Config.Bind<float>(new ConfigDefinition("Void - The Hermit", "Debuff duration"), 5f, new ConfigDescription("How long in seconds the on-hit debuff should last", null, Array.Empty<object>())); }
        public ConfigEntry<float> TheHermit_DamageReduction() { return Config.Bind<float>(new ConfigDefinition("Void - The Hermit", "Debuff damage reduction"), 0.01f, new ConfigDescription("Enemy damage reduced by each debuff in percent", null, Array.Empty<object>())); }

        // Lunar
        public ConfigEntry<bool> OneTicket_Enable() { return Config.Bind<bool>(new ConfigDefinition("Lunar - One Ticket", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }
        public ConfigEntry<int> OneTicket_ItemIncrease() { return Config.Bind<int>(new ConfigDefinition("Lunar - One Ticket", "Chest Drop Increase"), 5, new ConfigDescription("Amount of extra items your next chest contains", null, Array.Empty<object>())); }
        public ConfigEntry<float> OneTicket_MovementBuff() { return Config.Bind<float>(new ConfigDefinition("Lunar - One Ticket", "Enemy movement speed buff"), 50f, new ConfigDescription("While you're holding the ticket, enemies are this percentage faster", null, Array.Empty<object>())); }
        public ConfigEntry<float> OneTicket_AttackSpeedBuff() { return Config.Bind<float>(new ConfigDefinition("Lunar - One Ticket", "Enemy attack speed buff"), 100f, new ConfigDescription("While you're holding the ticket, enemies attack this percentage faster", null, Array.Empty<object>())); }
        public ConfigEntry<float> OneTicket_CooldownReduction() { return Config.Bind<float>(new ConfigDefinition("Lunar - One Ticket", "Enemy cooldown reduction"), 50f, new ConfigDescription("While you're holding the ticket, enemies' cooldowns are reduced by this percentage", null, Array.Empty<object>())); }

        // Lunar Equipment
        public ConfigEntry<bool> BloodOfTheLamb_Enable() { return Config.Bind<bool>(new ConfigDefinition("Lunar Equipment - Blood Of The Lamb", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs.", null, Array.Empty<object>())); }
        public ConfigEntry<int> BloodOfTheLamb_ItemsTaken() { return Config.Bind<int>(new ConfigDefinition("Lunar Equipment - Blood Of The Lamb", "Items taken"), 6, new ConfigDescription("Amount of regular items you must sacrifice to gain a boss item", null, Array.Empty<object>())); }

        public void Awake()
        {
            Log.Init(Logger);
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

            Log.LogInfo("Initializing items...");
            // Common
            Log.LogInfo("Common");
            if (ShardOfGlass_Enable().Value == true){ ShardOfGlass.Initiate(ShardOfGlass_DamageIncrease().Value); }
            if (BucketList_Enable().Value == true){ BucketList.Initiate(BucketList_FullBuff().Value, BucketList_BuffReduce().Value); }
            if (HopooEgg_Enable().Value == true){ HopooEgg.Initiate(HopooEgg_JumpModifier().Value, HopooEgg_FallDamageReduction().Value); }
            if (AtgPrototype_Enable().Value == true){ AtgPrototype.Initiate(AtgPrototype_Damage().Value, AtgPrototype_HitRequirement().Value); }
            if (Tickets_Enable().Value == true){ Tickets.Initiate(); }
            if (Balance_Enable().Value == true) { Balance.Initiate(Balance_MaxDodge().Value); }
            if (MinersHelmet_Enable().Value == true) { MinersHelmet.Initiate(MinersHelmet_CooldownReduction().Value, MinersHelmet_GoldPerProc().Value); }
            // Uncommon
            Log.LogInfo("Uncommon");
            if (ScatteredReflection_Enable().Value == true){ ScatteredReflection.Initiate(ScatteredReflection_DamageReflectPercent().Value, ScatteredReflection_DamageReflectShardStack().Value, ScatteredReflection_DamageReflectBonus().Value); }
            if (Empathy_Enable().Value == true){ Empathy.Initiate(Empathy_HealingFactor().Value); }
            if (ScavengersPack_Enable().Value == true) { ScavengersPack.Initiate(ScavengersPack_Uses().Value, ScavengersPack_PowerElixir().Value, ScavengersPack_DelicateWatch().Value, ScavengersPack_Dios().Value, ScavengersPack_VoidDios().Value, ScavengersPack_RustedKey().Value, ScavengersPack_EncrustedKey().Value, ScavengersPack_FourHundredTickets().Value, ScavengersPack_OneTicket().Value, ScavengersPack_ShopCard().Value, ScavengersPack_CuteBow().Value, ScavengersPack_ClockworkMechanism().Value, ScavengersPack_Vials().Value, ScavengersPack_BrokenChopsticks().Value, ScavengersPack_AbyssalCartridge().Value, ScavengersPack_Singularity().Value); }
            if (TheUnforgivable_Enable().Value == true) { TheUnforgivable.Initiate(TheUnforgivable_Interval().Value); }
            if (OverkillOverdrive_Enable().Value == true) { OverkillOverdrive.Initiate(OverkillOverdrive_ZoneIncrease().Value, OverkillOverdrive_AltMode().Value); }
            // Legendary
            Log.LogInfo("Legendary");
            if (Apathy_Enable().Value == true){ Apathy.Initiate(Apathy_HealthIncrease().Value, Apathy_DamageIncrease().Value); }
            if (MintCondition_Enable().Value == true){ MintCondition.Initiate(MintCondition_MoveSpeed().Value, MintCondition_MoveSpeedStack().Value, MintCondition_AddJumps().Value, MintCondition_AddJumpsStack().Value); }
            if (ElderMutagen_Enable().Value == true){ ElderMutagen.Initiate(ElderMutagen_AddDuration().Value, ElderMutagen_CooldownReduction().Value); }
            if (DoNotEat_Enable().Value == true) { DoNotEat.Initiate(DoNotEat_PearlChancePerStack().Value, DoNotEat_IrradiantChance().Value); }
            if (TaxManStatement_Enable().Value == true) { TaxManStatement.Initiate(TaxManStatement_ChanceToInflict().Value, TaxManStatement_DamagePerTax().Value, TaxManStatement_BaseGoldPerTax().Value); }
            // Void
            Log.LogInfo("Void");
            if (CorruptingParasite_Enable().Value == true){ CorruptingParasite.Initiate(CorruptingParasite_CorruptBossItems().Value, CorruptingParasite_ItemsPerStage().Value); }
            if (NoticeOfAbsence_Enable().Value == true){ NoticeOfAbsence.Initiate(NoticeOfAbsence_SpeedBuff().Value, NoticeOfAbsence_MaxSpeedBuff().Value); }
            if (DropOfNecrosis_Enable().Value == true) { DropOfNecrosis.Initiate(DropOfNecrosis_Damage().Value, DropOfNecrosis_DotChance().Value); }
            if (CaptainsFavor_Enable().Value == true) { CaptainsFavor.Initiate(CaptainsFavor_InteractableIncrease().Value); }
            if (Discovery_Enable().Value == true){ Discovery.Initiate(Discovery_ShieldAdd().Value, Discovery_MaxStacks().Value); }
            if (SpatteredCollection_Enable().Value == true) { SpatteredCollection.Initiate(SpatteredCollection_IntervalReduction().Value, SpatteredCollection_DotChance().Value); }
            if (TheHermit_Enable().Value == true){ TheHermit.Initiate(TheHermit_BuffDuration().Value, TheHermit_DamageReduction().Value); }
            // Lunar
            Log.LogInfo("Lunar");
            if (OneTicket_Enable().Value == true) { OneTicket.Initiate(OneTicket_ItemIncrease().Value, OneTicket_MovementBuff().Value, OneTicket_AttackSpeedBuff().Value, OneTicket_CooldownReduction().Value); }
            // Lunar Equipment
            Log.LogInfo("Lunar Equipment");
            if (BloodOfTheLamb_Enable().Value == true) { BloodOfTheLamb.Initiate(BloodOfTheLamb_ItemsTaken().Value); }
            // Artifacts
            Log.LogInfo("Initializing artifacts...");
            ArtifactOfCorruption.Initiate();

            /*
            
            TODO
            New Items
            - The Tax Man's Statement
            - One Ticket

            Reworks
            - Empathy
            - Apathy
            - Elder Mutagen
            - Drop of Necrosis
            - Spattered Collection
            - The Hermit
            */

            Log.LogInfo("Done!");
        }
    }
}
