using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using RoR2;
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
using RiskOfOptions;
using RiskOfOptions.Options;
using RiskOfOptions.OptionConfigs;
using UnityEngine.AddressableAssets;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System.Xml.Linq;

namespace Hex3Mod
{
    [BepInPlugin(ModGuid, ModName, ModVer)]
    [BepInDependency("com.bepis.r2api", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.bepis.r2api.content_management", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.bepis.r2api.items", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.bepis.r2api.language", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.bepis.r2api.prefab", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.bepis.r2api.recalculatestats", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.bepis.r2api.networking", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.bepis.r2api.unlockable", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.rune580.riskofoptions", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("Hayaku.VanillaRebalance", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("HIFU.UltimateCustomRun", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency(VoidItemAPI.VoidItemAPI.MODGUID)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    public class Main : BaseUnityPlugin
    {
        public const string ModGuid = "com.Hex3.Hex3Mod";
        public const string ModName = "Hex3Mod";
        public const string ModVer = "2.1.0";

        public static RoR2.ExpansionManagement.ExpansionDef Hex3ModExpansion;

        public static ConfigEntry<int> ConfigVersion;

        // Common
        public static ConfigEntry<bool> ShardOfGlass_Load;
        public static ConfigEntry<bool> ShardOfGlass_Enable;
        public static ConfigEntry<float> ShardOfGlass_DamageIncrease;

        public static ConfigEntry<bool> BucketList_Load;
        public static ConfigEntry<bool> BucketList_Enable;
        public static ConfigEntry<float> BucketList_FullBuff;
        public static ConfigEntry<float> BucketList_BuffReduce;

        public static ConfigEntry<bool> HopooEgg_Load;
        public static ConfigEntry<bool> HopooEgg_Enable;
        public static ConfigEntry<float> HopooEgg_JumpModifier;

        public static ConfigEntry<bool> AtgPrototype_Load;
        public static ConfigEntry<bool> AtgPrototype_Enable;
        public static ConfigEntry<float> AtgPrototype_Damage;
        public static ConfigEntry<int> AtgPrototype_HitRequirement;

        public static ConfigEntry<bool> Tickets_Load;
        public static ConfigEntry<bool> Tickets_Enable;
        public static ConfigEntry<bool> Tickets_Bud;
        public static ConfigEntry<bool> Tickets_Cradle;
        public static ConfigEntry<bool> Tickets_Potential;
        public static ConfigEntry<bool> Tickets_Pool;

        public static ConfigEntry<bool> Balance_Load;
        public static ConfigEntry<bool> Balance_Enable;
        public static ConfigEntry<float> Balance_MaxDodge;

        public static ConfigEntry<bool> MinersHelmet_Load;
        public static ConfigEntry<bool> MinersHelmet_Enable;
        public static ConfigEntry<float> MinersHelmet_CooldownReduction;
        public static ConfigEntry<int> MinersHelmet_GoldPerProc;

        // Uncommon
        public static ConfigEntry<bool> ScatteredReflection_Load;
        public static ConfigEntry<bool> ScatteredReflection_Enable;
        public static ConfigEntry<float> ScatteredReflection_DamageReflectPercent;
        public static ConfigEntry<float> ScatteredReflection_DamageReflectShardStack;
        public static ConfigEntry<float> ScatteredReflection_DamageReflectBonus;

        public static ConfigEntry<bool> Empathy_Load;
        public static ConfigEntry<bool> Empathy_Enable;
        public static ConfigEntry<float> Empathy_HealthPerHit;
        public static ConfigEntry<float> Empathy_Radius;

        public static ConfigEntry<bool> ScavengersPack_Load;
        public static ConfigEntry<bool> ScavengersPack_Enable;
        public static ConfigEntry<int> ScavengersPack_Uses;
        public static ConfigEntry<bool> ScavengersPack_RegenScrap;
        public static ConfigEntry<bool> ScavengersPack_PowerElixir;
        public static ConfigEntry<bool> ScavengersPack_DelicateWatch;
        public static ConfigEntry<bool> ScavengersPack_Dios;
        public static ConfigEntry<bool> ScavengersPack_VoidDios;
        public static ConfigEntry<bool> ScavengersPack_RustedKey;
        public static ConfigEntry<bool> ScavengersPack_EncrustedKey;
        public static ConfigEntry<bool> ScavengersPack_FourHundredTickets;
        public static ConfigEntry<bool> ScavengersPack_OneTicket;
        public static ConfigEntry<bool> ScavengersPack_ShopCard;
        public static ConfigEntry<bool> ScavengersPack_CuteBow;
        public static ConfigEntry<bool> ScavengersPack_GhostApple;
        public static ConfigEntry<bool> ScavengersPack_ClockworkMechanism;
        public static ConfigEntry<bool> ScavengersPack_Vials;
        public static ConfigEntry<bool> ScavengersPack_BrokenChopsticks;
        public static ConfigEntry<bool> ScavengersPack_AbyssalCartridge;
        public static ConfigEntry<bool> ScavengersPack_Singularity;

        public static ConfigEntry<bool> TheUnforgivable_Load;
        public static ConfigEntry<bool> TheUnforgivable_Enable;
        public static ConfigEntry<float> TheUnforgivable_Interval;

        public static ConfigEntry<bool> OverkillOverdrive_Load;
        public static ConfigEntry<bool> OverkillOverdrive_Enable;
        public static ConfigEntry<bool> OverkillOverdrive_TurretBlacklist;
        public static ConfigEntry<float> OverkillOverdrive_ZoneIncrease;
        public static ConfigEntry<bool> Overkilloverdrive_EnableHoldouts;
        public static ConfigEntry<bool> Overkilloverdrive_EnableShrineWoods;
        public static ConfigEntry<bool> Overkilloverdrive_EnableFocusCrystal;
        public static ConfigEntry<bool> Overkilloverdrive_EnableBuffWards;
        public static ConfigEntry<bool> Overkilloverdrive_EnableDeskPlant;
        public static ConfigEntry<bool> Overkilloverdrive_EnableBungus;

        // Legendary
        public static ConfigEntry<bool> Apathy_Load;
        public static ConfigEntry<bool> Apathy_Enable;
        public static ConfigEntry<float> Apathy_Radius;
        public static ConfigEntry<float> Apathy_MoveSpeedAdd;
        public static ConfigEntry<float> Apathy_AttackSpeedAdd;
        public static ConfigEntry<float> Apathy_RegenAdd;
        public static ConfigEntry<float> Apathy_Duration;
        public static ConfigEntry<int> Apathy_RequiredKills;

        public static ConfigEntry<bool> MintCondition_Load;
        public static ConfigEntry<bool> MintCondition_Enable;
        public static ConfigEntry<float> MintCondition_MoveSpeed;
        public static ConfigEntry<float> MintCondition_MoveSpeedStack;
        public static ConfigEntry<int> MintCondition_AddJumps;
        public static ConfigEntry<int> MintCondition_AddJumpsStack;

        public static ConfigEntry<bool> ElderMutagen_Load;
        public static ConfigEntry<bool> ElderMutagen_Enable;
        public static ConfigEntry<int> ElderMutagen_MaxHealthFlatAdd;
        public static ConfigEntry<float> ElderMutagen_RegenAdd;

        public static ConfigEntry<bool> DoNotEat_Load;
        public static ConfigEntry<bool> DoNotEat_Enable;
        public static ConfigEntry<float> DoNotEat_PearlChancePerStack;
        public static ConfigEntry<float> DoNotEat_IrradiantChance;
        public static ConfigEntry<bool> DoNotEat_ShopTerminals;

        // Void
        public static ConfigEntry<bool> CorruptingParasite_Load;
        public static ConfigEntry<bool> CorruptingParasite_Enable;
        public static ConfigEntry<bool> CorruptingParasite_CorruptBossItems;
        public static ConfigEntry<int> CorruptingParasite_ItemsPerStage;
        public static ConfigEntry<bool> CorruptingParasite_NonStageCorrupt;

        public static ConfigEntry<bool> NoticeOfAbsence_Load;
        public static ConfigEntry<bool> NoticeOfAbsence_Enable;
        public static ConfigEntry<float> NoticeOfAbsence_InvisibilityBuff;
        public static ConfigEntry<float> NoticeOfAbsence_InvisibilityBuffStack;

        public static ConfigEntry<bool> DropOfNecrosis_Load;
        public static ConfigEntry<bool> DropOfNecrosis_Enable;
        public static ConfigEntry<float> DropOfNecrosis_Damage;
        public static ConfigEntry<float> DropOfNecrosis_DotChance;

        public static ConfigEntry<bool> CaptainsFavor_Load;
        public static ConfigEntry<bool> CaptainsFavor_Enable;
        public static ConfigEntry<float> CaptainsFavor_InteractableIncrease;

        public static ConfigEntry<bool> Discovery_Load;
        public static ConfigEntry<bool> Discovery_Enable;
        public static ConfigEntry<float> Discovery_ShieldAdd;
        public static ConfigEntry<int> Discovery_MaxStacks;

        public static ConfigEntry<bool> SpatteredCollection_Load;
        public static ConfigEntry<bool> SpatteredCollection_Enable;
        public static ConfigEntry<float> SpatteredCollection_ArmorReduction;
        public static ConfigEntry<float> SpatteredCollection_DotChance;

        public static ConfigEntry<bool> TheHermit_Load;
        public static ConfigEntry<bool> TheHermit_Enable;
        public static ConfigEntry<float> TheHermit_BuffDuration;
        public static ConfigEntry<float> TheHermit_DamageReduction;

        // Lunar
        public static ConfigEntry<bool> OneTicket_Load;
        public static ConfigEntry<bool> OneTicket_Enable;

        // Lunar Equipment
        public static ConfigEntry<bool> BloodOfTheLamb_Load;
        public static ConfigEntry<bool> BloodOfTheLamb_Enable;
        public static ConfigEntry<int> BloodOfTheLamb_ItemsTaken;

        public static bool debugMode = true; // DISABLE BEFORE BUILD

        public static AssetBundle MainAssets;

        public static Dictionary<string, string> ShaderLookup = new Dictionary<string, string>() // Strings of stubbed vs. real shaders
        {
            {"stubbed hopoo games/deferred/standard", "shaders/deferred/hgstandard"},
            {"stubbed hopoo games/fx/cloud intersection remap", "shaders/fx/hgintersectioncloudremap"},
            {"stubbed hopoo games/fx/cloud remap", "shaders/fx/hgcloudremap"},
            {"stubbed hopoo games/fx/opaque cloud remap", "shaders/fx/hgopaquecloudremap"},
            {"stubbed hopoo games/fx/distortion", "shaders/fx/hgdistortion"}
        };

        public static ManualLogSource logger;

        public void Awake()
        {
            Log.Init(Logger);
            Log.LogInfo("Creating assets...");
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Hex3Mod.vfxpass2"))
            {
                MainAssets = AssetBundle.LoadFromStream(stream); // Load mainassets into stream
            }

            foreach (Material material in MainAssets.LoadAllAssets<Material>())
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

            Log.LogInfo("Creating config...");

            // Risk Of Options
            ModSettingsManager.SetModDescription("Adds 25 new items. Fully configurable, now with Risk Of Options!");
            Sprite icon = MainAssets.LoadAsset<Sprite>("Assets/VFXPASS3/Icons/icon.png");
            ModSettingsManager.SetModIcon(icon);

            runItBack:

            ConfigVersion = Config.Bind(new ConfigDefinition("Config Version", "Config Version"), 1, new ConfigDescription("Do not change.", null, Array.Empty<object>()));

            // Common
            ShardOfGlass_Load = Config.Bind(new ConfigDefinition("Common - Shard Of Glass", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            ShardOfGlass_Enable = Config.Bind(new ConfigDefinition("Common - Shard Of Glass", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            ShardOfGlass_DamageIncrease = Config.Bind(new ConfigDefinition("Common - Shard Of Glass", "Damage multiplier"), 0.07f, new ConfigDescription("Percentage of base damage this item adds", null, Array.Empty<object>()));

            BucketList_Load = Config.Bind(new ConfigDefinition("Common - Bucket List", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            BucketList_Enable = Config.Bind(new ConfigDefinition("Common - Bucket List", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            BucketList_FullBuff = Config.Bind(new ConfigDefinition("Common - Bucket List", "Speed multiplier"), 0.24f, new ConfigDescription("Percent speed increase", null, Array.Empty<object>()));
            BucketList_BuffReduce = Config.Bind(new ConfigDefinition("Common - Bucket List", "Reduced buff multiplier"), 0.8f, new ConfigDescription("Multiplier subtracted from the speed buff while fighting a boss", null, Array.Empty<object>()));

            HopooEgg_Load = Config.Bind(new ConfigDefinition("Common - Hopoo Egg", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            HopooEgg_Enable = Config.Bind(new ConfigDefinition("Common - Hopoo Egg", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            HopooEgg_JumpModifier = Config.Bind(new ConfigDefinition("Common - Hopoo Egg", "Jump height multiplier"), 0.3f, new ConfigDescription("Percent jump height increase", null, Array.Empty<object>()));

            AtgPrototype_Load = Config.Bind(new ConfigDefinition("Common - ATG Prototype", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            AtgPrototype_Enable = Config.Bind(new ConfigDefinition("Common - ATG Prototype", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            AtgPrototype_Damage = Config.Bind(new ConfigDefinition("Common - ATG Prototype", "Damage per stack"), 0.8f, new ConfigDescription("Multiplier of base damage the missile deals per stack", null, Array.Empty<object>()));
            AtgPrototype_HitRequirement = Config.Bind(new ConfigDefinition("Common - ATG Prototype", "Hits required per missile"), 10, new ConfigDescription("How many hits it should take to fire each missile", null, Array.Empty<object>()));

            Tickets_Load = Config.Bind(new ConfigDefinition("Common - 400 Tickets", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            Tickets_Enable = Config.Bind(new ConfigDefinition("Common - 400 Tickets", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            Tickets_Bud = Config.Bind(new ConfigDefinition("Common - 400 Tickets", "Works on Lunar Bud"), true, new ConfigDescription("Should 400 Tickets double Lunar Bud drops?", null, Array.Empty<object>()));
            Tickets_Cradle = Config.Bind(new ConfigDefinition("Common - 400 Tickets", "Works on Void Cradle"), true, new ConfigDescription("Should 400 Tickets double Void Cradle drops?", null, Array.Empty<object>()));
            Tickets_Potential = Config.Bind(new ConfigDefinition("Common - 400 Tickets", "Works on Void Potential"), true, new ConfigDescription("Should 400 Tickets double Void Potential and Encrusted Lockbox drops?", null, Array.Empty<object>()));
            Tickets_Pool = Config.Bind(new ConfigDefinition("Common - 400 Tickets", "Works on Cleansing Pool"), false, new ConfigDescription("Should 400 Tickets double Cleansing Pool drops?", null, Array.Empty<object>()));

            Balance_Load = Config.Bind(new ConfigDefinition("Common - Balance", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            Balance_Enable = Config.Bind(new ConfigDefinition("Common - Balance", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            Balance_MaxDodge = Config.Bind(new ConfigDefinition("Common - Balance", "Max added dodge chance"), 20f, new ConfigDescription("Maximum (standing still) chance to dodge, stacking hyperbolically", null, Array.Empty<object>()));

            MinersHelmet_Load = Config.Bind(new ConfigDefinition("Common - Miners Helmet", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            MinersHelmet_Enable = Config.Bind(new ConfigDefinition("Common - Miners Helmet", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            MinersHelmet_CooldownReduction = Config.Bind(new ConfigDefinition("Common - Miners Helmet", "Cooldown reduction"), 2f, new ConfigDescription("Reduce cooldowns by this many seconds every time a small chest's worth is earned", null, Array.Empty<object>()));
            MinersHelmet_GoldPerProc = Config.Bind(new ConfigDefinition("Common - Miners Helmet", "Gold requirement"), 25, new ConfigDescription("Gold required to proc cooldown reduction (Scaling with time)", null, Array.Empty<object>()));

            // Uncommon
            ScatteredReflection_Load = Config.Bind(new ConfigDefinition("Uncommon - Scattered Reflection", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            ScatteredReflection_Enable = Config.Bind(new ConfigDefinition("Uncommon - Scattered Reflection", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            ScatteredReflection_DamageReflectPercent = Config.Bind(new ConfigDefinition("Uncommon - Scattered Reflection", "Damage reflect value"), 0.07f, new ConfigDescription("The percent of all total received damage to be reflected", null, Array.Empty<object>()));
            ScatteredReflection_DamageReflectShardStack = Config.Bind(new ConfigDefinition("Uncommon - Scattered Reflection", "Damage reflect multiplier per shard"), 0.007f, new ConfigDescription("How much of a reflection bonus each Shard Of Glass adds in percentage of total damage (Caps at 90%)", null, Array.Empty<object>()));
            ScatteredReflection_DamageReflectBonus = Config.Bind(new ConfigDefinition("Uncommon - Scattered Reflection", "Reflected damage bonus"), 7f, new ConfigDescription("Multiplier of how much bonus damage is added to the reflection", null, Array.Empty<object>()));

            Empathy_Load = Config.Bind(new ConfigDefinition("Uncommon - Empathy", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            Empathy_Enable = Config.Bind(new ConfigDefinition("Uncommon - Empathy", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            Empathy_HealthPerHit = Config.Bind(new ConfigDefinition("Uncommon - Empathy", "Health per hit"), 3f, new ConfigDescription("Health points restored when an enemy is hit within radius", null, Array.Empty<object>()));
            Empathy_Radius = Config.Bind(new ConfigDefinition("Uncommon - Empathy", "Zone radius"), 13f, new ConfigDescription("Radius of activation zone in meters", null, Array.Empty<object>()));

            ScavengersPack_Load = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            ScavengersPack_Enable = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            ScavengersPack_Uses = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "Maximum uses"), 2, new ConfigDescription("Times the Scavenger's Pouch can be used before being emptied.", null, Array.Empty<object>()));
            ScavengersPack_RegenScrap = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "VANILLA: Regenerating Scrap"), false, new ConfigDescription("Enable item replacement", null, Array.Empty<object>()));
            ScavengersPack_PowerElixir = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "VANILLA: Power Elixir"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>()));
            ScavengersPack_DelicateWatch = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "VANILLA: Delicate Watch"), false, new ConfigDescription("Enable item replacement", null, Array.Empty<object>()));
            ScavengersPack_Dios = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "VANILLA: Dios Best Friend"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>()));
            ScavengersPack_VoidDios = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "VANILLA: Pluripotent Larva"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>()));
            ScavengersPack_RustedKey = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "WOLFO QOL: Rusted Key"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>()));
            ScavengersPack_EncrustedKey = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "WOLFO QOL: Encrusted Key"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>()));
            ScavengersPack_FourHundredTickets = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "HEX3MOD: 400 Tickets"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>()));
            ScavengersPack_OneTicket = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "HEX3MOD: One Ticket"), false, new ConfigDescription("Enable item replacement", null, Array.Empty<object>()));
            ScavengersPack_ShopCard = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "MYSTICS ITEMS: Platinum Card"), false, new ConfigDescription("Enable item replacement", null, Array.Empty<object>()));
            ScavengersPack_CuteBow = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "MYSTICS ITEMS: Cutesy Bow"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>()));
            ScavengersPack_GhostApple = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "MYSTICS ITEMS: Ghost Apple"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>()));
            ScavengersPack_ClockworkMechanism = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "VANILLAVOID: Clockwork Mechanism"), false, new ConfigDescription("Enable item replacement", null, Array.Empty<object>()));
            ScavengersPack_Vials = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "VANILLAVOID: Enhancement Vials"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>()));
            ScavengersPack_BrokenChopsticks = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "HOLYCRAPFORKISBACK: Sharp Chopsticks"), false, new ConfigDescription("Enable item replacement", null, Array.Empty<object>()));
            ScavengersPack_AbyssalCartridge = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "SPIKESTRIP: Abyssal Cartridge"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>()));
            ScavengersPack_Singularity = Config.Bind(new ConfigDefinition("Uncommon - Scavengers Pouch", "SPIKESTRIP: Singularity"), true, new ConfigDescription("Enable item replacement", null, Array.Empty<object>()));

            TheUnforgivable_Load = Config.Bind(new ConfigDefinition("Uncommon - The Unforgivable", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            TheUnforgivable_Enable = Config.Bind(new ConfigDefinition("Uncommon - The Unforgivable", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            TheUnforgivable_Interval = Config.Bind(new ConfigDefinition("Uncommon - The Unforgivable", "Activation interval"), 10f, new ConfigDescription("Activate your on-kill effects every time this amount of seconds passes", null, Array.Empty<object>()));

            OverkillOverdrive_Load = Config.Bind(new ConfigDefinition("Uncommon - Overkill Overdrive", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            OverkillOverdrive_Enable = Config.Bind(new ConfigDefinition("Uncommon - Overkill Overdrive", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            OverkillOverdrive_TurretBlacklist = Config.Bind(new ConfigDefinition("Uncommon - Overkill Overdrive", "Turret blacklist"), false, new ConfigDescription("This item has no effect if used by Engineer turrets. Disabled by default", null, Array.Empty<object>()));
            OverkillOverdrive_ZoneIncrease = Config.Bind(new ConfigDefinition("Uncommon - Overkill Overdrive", "Zone size percentage increase"), 20f, new ConfigDescription("How much larger affected zones should be in percentage", null, Array.Empty<object>()));
            Overkilloverdrive_EnableHoldouts = Config.Bind(new ConfigDefinition("Uncommon - Overkill Overdrive", "Amplify holdout zones"), true, new ConfigDescription("e.g Teleporter, Pillars", null, Array.Empty<object>()));
            Overkilloverdrive_EnableShrineWoods = Config.Bind(new ConfigDefinition("Uncommon - Overkill Overdrive", "Amplify Shrine of the Woods"), true, new ConfigDescription("", null, Array.Empty<object>()));
            Overkilloverdrive_EnableFocusCrystal = Config.Bind(new ConfigDefinition("Uncommon - Overkill Overdrive", "Amplify Focus Crystal"), true, new ConfigDescription("", null, Array.Empty<object>()));
            Overkilloverdrive_EnableBuffWards = Config.Bind(new ConfigDefinition("Uncommon - Overkill Overdrive", "Amplify buff wards"), true, new ConfigDescription("e.g Warbanner, Spinel Tonic", null, Array.Empty<object>()));
            Overkilloverdrive_EnableDeskPlant = Config.Bind(new ConfigDefinition("Uncommon - Overkill Overdrive", "Amplify Interstellar Desk Plant"), true, new ConfigDescription("", null, Array.Empty<object>()));
            Overkilloverdrive_EnableBungus = Config.Bind(new ConfigDefinition("Uncommon - Overkill Overdrive", "Amplify Bustling Fungus"), true, new ConfigDescription("", null, Array.Empty<object>()));

            // Legendary
            Apathy_Load = Config.Bind(new ConfigDefinition("Legendary - Apathy", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            Apathy_Enable = Config.Bind(new ConfigDefinition("Legendary - Apathy", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            Apathy_Radius = Config.Bind(new ConfigDefinition("Legendary - Apathy", "Radius"), 13f, new ConfigDescription("Radius within which kills contribute to Apathy stacks", null, Array.Empty<object>()));
            Apathy_MoveSpeedAdd = Config.Bind(new ConfigDefinition("Legendary - Apathy", "Buff move speed multiplier"), 1f, new ConfigDescription("Move speed multiplier added while buffed", null, Array.Empty<object>()));
            Apathy_AttackSpeedAdd = Config.Bind(new ConfigDefinition("Legendary - Apathy", "Buff attack speed multiplier"), 1f, new ConfigDescription("Attack speed multiplier added while buffed", null, Array.Empty<object>()));
            Apathy_RegenAdd = Config.Bind(new ConfigDefinition("Legendary - Apathy", "Buff regen added"), 20f, new ConfigDescription("Regen hp/second added while buffed", null, Array.Empty<object>()));
            Apathy_Duration = Config.Bind(new ConfigDefinition("Legendary - Apathy", "Buff duration"), 7.5f, new ConfigDescription("Duration of buff", null, Array.Empty<object>()));
            Apathy_RequiredKills = Config.Bind(new ConfigDefinition("Legendary - Apathy", "Kills required for buff"), 10, new ConfigDescription("Required stacks of Apathy to trigger buff", null, Array.Empty<object>()));

            MintCondition_Load = Config.Bind(new ConfigDefinition("Legendary - Mint Condition", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            MintCondition_Enable = Config.Bind(new ConfigDefinition("Legendary - Mint Condition", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            MintCondition_MoveSpeed = Config.Bind(new ConfigDefinition("Legendary - Mint Condition", "Move speed increase"), 0.2f, new ConfigDescription("Base movement speed increase", null, Array.Empty<object>()));
            MintCondition_MoveSpeedStack = Config.Bind(new ConfigDefinition("Legendary - Mint Condition", "Move speed increase per stack"), 0.6f, new ConfigDescription("Base movement speed increase per additional stack", null, Array.Empty<object>()));
            MintCondition_AddJumps = Config.Bind(new ConfigDefinition("Legendary - Mint Condition", "Additional jumps"), 1, new ConfigDescription("Jump count increase", null, Array.Empty<object>()));
            MintCondition_AddJumpsStack = Config.Bind(new ConfigDefinition("Legendary - Mint Condition", "Additional jumps per stack"), 2, new ConfigDescription("Jump count increase per additional stack", null, Array.Empty<object>()));

            ElderMutagen_Load = Config.Bind(new ConfigDefinition("Legendary - Elder Mutagen", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            ElderMutagen_Enable = Config.Bind(new ConfigDefinition("Legendary - Elder Mutagen", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            ElderMutagen_MaxHealthFlatAdd = Config.Bind(new ConfigDefinition("Legendary - Elder Mutagen", "Max health per species"), 10, new ConfigDescription("Amount of max health added per killed species", null, Array.Empty<object>()));
            ElderMutagen_RegenAdd = Config.Bind(new ConfigDefinition("Legendary - Elder Mutagen", "Regen per species"), 0.5f, new ConfigDescription("How much hp per second regen should be added for each killed species", null, Array.Empty<object>()));

            DoNotEat_Load = Config.Bind(new ConfigDefinition("Legendary - Do Not Eat", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            DoNotEat_Enable = Config.Bind(new ConfigDefinition("Legendary - Do Not Eat", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            DoNotEat_PearlChancePerStack = Config.Bind(new ConfigDefinition("Legendary - Do Not Eat", "Pearl Chance"), 10f, new ConfigDescription("Percent chance that a Pearl or Irradiant Pearl will drop from a chest.", null, Array.Empty<object>()));
            DoNotEat_IrradiantChance = Config.Bind(new ConfigDefinition("Legendary - Do Not Eat", "Irradiant Pearl Chance"), 20f, new ConfigDescription("Percent chance that an Irradiant Pearl will drop instead of a Pearl.", null, Array.Empty<object>()));
            DoNotEat_ShopTerminals = Config.Bind(new ConfigDefinition("Legendary - Do Not Eat", "Shop terminals"), true, new ConfigDescription("Shop terminals have a chance to drop pearls.", null, Array.Empty<object>()));

            /*
            TaxManStatement_Enable() { return Config.Bind<bool>(new ConfigDefinition("Legendary - The Tax Mans Statement", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>())); }
            TaxManStatement_ChanceToInflict() { return Config.Bind<float>(new ConfigDefinition("Legendary - The Tax Mans Statement", "Chance to inflict on hit"), 5f, new ConfigDescription("Percent chance that hit enemies will be taxed per stack", null, Array.Empty<object>())); }
            TaxManStatement_DamagePerTax() { return Config.Bind<float>(new ConfigDefinition("Legendary - The Tax Mans Statement", "Damage percentage per tax"), 5f, new ConfigDescription("Percent damage the enemy takes whenever they use an ability. Halved against bosses", null, Array.Empty<object>())); }
            TaxManStatement_BaseGoldPerTax() { return Config.Bind<float>(new ConfigDefinition("Legendary - The Tax Mans Statement", "Gold per tax"), 2f, new ConfigDescription("Base gold gained for each taxation, scaling with time", null, Array.Empty<object>())); }
            */

            // Void
            CorruptingParasite_Load = Config.Bind(new ConfigDefinition("Void - Corrupting Parasite", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            CorruptingParasite_Enable = Config.Bind(new ConfigDefinition("Void - Corrupting Parasite", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            CorruptingParasite_CorruptBossItems = Config.Bind(new ConfigDefinition("Void - Corrupting Parasite", "Corrupt boss items"), false, new ConfigDescription("Allows the parasite to corrupt boss items", null, Array.Empty<object>()));
            CorruptingParasite_ItemsPerStage = Config.Bind(new ConfigDefinition("Void - Corrupting Parasite", "Items per stage"), 1, new ConfigDescription("Number of items corrupted each stage", null, Array.Empty<object>()));
            CorruptingParasite_NonStageCorrupt = Config.Bind(new ConfigDefinition("Void - Corrupting Parasite", "Corrupt items on non-stage scenes"), true, new ConfigDescription("Allows the parasite to corrupt items in non-stage scenes (hidden realms, bazaar)", null, Array.Empty<object>()));

            NoticeOfAbsence_Load = Config.Bind(new ConfigDefinition("Void - Notice Of Absence", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            NoticeOfAbsence_Enable = Config.Bind(new ConfigDefinition("Void - Notice Of Absence", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            NoticeOfAbsence_InvisibilityBuff = Config.Bind(new ConfigDefinition("Void - Notice Of Absence", "Base invisibility duration"), 10f, new ConfigDescription("How long you'll turn invisible at the start of a boss fight in seconds", null, Array.Empty<object>()));
            NoticeOfAbsence_InvisibilityBuffStack = Config.Bind(new ConfigDefinition("Void - Notice Of Absence", "Invisibility duration per stack"), 5f, new ConfigDescription("How much longer you'll turn invisible at the start of a boss fight in seconds, per stack", null, Array.Empty<object>()));

            DropOfNecrosis_Load = Config.Bind(new ConfigDefinition("Void - Drop Of Necrosis", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            DropOfNecrosis_Enable = Config.Bind(new ConfigDefinition("Void - Drop Of Necrosis", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            DropOfNecrosis_Damage = Config.Bind(new ConfigDefinition("Void - Drop Of Necrosis", "Added damage per stack"), 0.05f, new ConfigDescription("What fraction of base Blight damage is added to Blight per stack.", null, Array.Empty<object>()));
            DropOfNecrosis_DotChance = Config.Bind(new ConfigDefinition("Void - Drop Of Necrosis", "Chance to inflict Blight"), 5f, new ConfigDescription("Added percent chance of inflicting Blight on hit.", null, Array.Empty<object>()));

            CaptainsFavor_Load = Config.Bind(new ConfigDefinition("Void - Captains Favor", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            CaptainsFavor_Enable = Config.Bind(new ConfigDefinition("Void - Captains Favor", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));

            Discovery_Load = Config.Bind(new ConfigDefinition("Void - Discovery", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            Discovery_Enable = Config.Bind(new ConfigDefinition("Void - Discovery", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            Discovery_ShieldAdd = Config.Bind(new ConfigDefinition("Void - Discovery", "Shield value"), 3f, new ConfigDescription("Shield added per world interactable used", null, Array.Empty<object>()));
            Discovery_MaxStacks = Config.Bind(new ConfigDefinition("Void - Discovery", "Maximum uses"), 100, new ConfigDescription("Maximum interactable uses per stack before shield is no longer granted", null, Array.Empty<object>()));

            SpatteredCollection_Load = Config.Bind(new ConfigDefinition("Void - Spattered Collection", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            SpatteredCollection_Enable = Config.Bind(new ConfigDefinition("Void - Spattered Collection", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            SpatteredCollection_ArmorReduction = Config.Bind(new ConfigDefinition("Void - Spattered Collection", "Armor reduction per stack"), 2f, new ConfigDescription("For each stack of Spattered Collection, reduce enemies' armor by this much per stack of Blight.", null, Array.Empty<object>()));
            SpatteredCollection_DotChance = Config.Bind(new ConfigDefinition("Void - Spattered Collection", "Chance to inflict Blight"), 10f, new ConfigDescription("Added percent chance of inflicting Blight on hit.", null, Array.Empty<object>()));

            TheHermit_Load = Config.Bind(new ConfigDefinition("Void - The Hermit", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            TheHermit_Enable = Config.Bind(new ConfigDefinition("Void - The Hermit", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            TheHermit_BuffDuration = Config.Bind(new ConfigDefinition("Void - The Hermit", "Debuff duration"), 5f, new ConfigDescription("How long in seconds the on-hit debuff should last", null, Array.Empty<object>()));
            TheHermit_DamageReduction = Config.Bind(new ConfigDefinition("Void - The Hermit", "Debuff damage reduction"), 5f, new ConfigDescription("Enemy damage reduced by each debuff in percent", null, Array.Empty<object>()));

            // Lunar
            OneTicket_Load = Config.Bind(new ConfigDefinition("Lunar - One Ticket", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            OneTicket_Enable = Config.Bind(new ConfigDefinition("Lunar - One Ticket", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));

            // Lunar Equipment
            BloodOfTheLamb_Load = Config.Bind(new ConfigDefinition("Lunar Equipment - Blood Of The Lamb", "Load item"), true, new ConfigDescription("Load the item at startup. <style=cDeath>Requires a restart to take effect!</style>", null, Array.Empty<object>()));
            BloodOfTheLamb_Enable = Config.Bind(new ConfigDefinition("Lunar Equipment - Blood Of The Lamb", "Enable item"), true, new ConfigDescription("Allow the user to find this item in runs. <style=cDeath>Must start a new run to take effect!</style>", null, Array.Empty<object>()));
            BloodOfTheLamb_ItemsTaken = Config.Bind(new ConfigDefinition("Lunar Equipment - Blood Of The Lamb", "Items taken"), 6, new ConfigDescription("Amount of regular items you must sacrifice to gain a boss item", null, Array.Empty<object>()));

            if (ConfigVersion.Value != 1) // Manually set every major update
            {
                Config.Clear();
                Config.Save();
                goto runItBack;
            }

            foreach (ConfigEntryBase configEntryBase in Config.GetConfigEntries())
            {
                if (configEntryBase.DefaultValue.GetType() == typeof(bool))
                {
                    ModSettingsManager.AddOption(new CheckBoxOption((ConfigEntry<bool>)configEntryBase));
                }
                if (configEntryBase.DefaultValue.GetType() == typeof(int))
                {
                    ModSettingsManager.AddOption(new IntSliderOption((ConfigEntry<int>)configEntryBase, new IntSliderConfig() { min = 0, max = ((ConfigEntry<int>)configEntryBase).Value * 10 }));
                }
                if (configEntryBase.DefaultValue.GetType() == typeof(float))
                {
                    ModSettingsManager.AddOption(new StepSliderOption((ConfigEntry<float>)configEntryBase, new StepSliderConfig() { min = 0, max = ((ConfigEntry<float>)configEntryBase).Value * 10f, increment = ((ConfigEntry<float>)configEntryBase).Value  / 10f}));
                }
            }

            if (UltimateCustomRunCompatibility.enabled) { Log.LogInfo("Detected Ultimate Custom Run soft dependency"); }
            else { Log.LogInfo("Did not detect Ultimate Custom Run soft dependency"); }
            if (VanillaRebalanceCompatibility.enabled) { Log.LogInfo("Detected VanillaRebalance soft dependency"); }
            else { Log.LogInfo("Did not detect VanillaRebalance soft dependency"); }

            Log.LogInfo("Creating expansion...");
            Hex3ModExpansion = ScriptableObject.CreateInstance<RoR2.ExpansionManagement.ExpansionDef>();
            Hex3ModExpansion.name = "Hex3Mod";
            Hex3ModExpansion.nameToken = "Hex3Mod";
            Hex3ModExpansion.descriptionToken = "Adds content from 'Hex3Mod' to the game.";
            Hex3ModExpansion.iconSprite = MainAssets.LoadAsset<Sprite>("Assets/VFXPASS3/Icons/expansion.png");
            Hex3ModExpansion.disabledIconSprite = Addressables.LoadAssetAsync<Sprite>("RoR2/Base/Common/MiscIcons/texUnlockIcon.png").WaitForCompletion();
            Hex3ModExpansion.requiredEntitlement = null;
            ContentAddition.AddExpansionDef(Hex3ModExpansion);

            Log.LogInfo("Initializing items...");
            // Common
            Log.LogInfo("Common");
            if (ShardOfGlass_Load.Value){ ShardOfGlass.Initiate();}
            if (BucketList_Load.Value) { BucketList.Initiate();}
            if (HopooEgg_Load.Value) { HopooEgg.Initiate();}
            if (AtgPrototype_Load.Value) { AtgPrototype.Initiate();}
            if (Tickets_Load.Value) { Tickets.Initiate();}
            if (Balance_Load.Value) { Balance.Initiate();}
            if (MinersHelmet_Load.Value) { MinersHelmet.Initiate();}
            // Uncommon
            Log.LogInfo("Uncommon");
            if (ScatteredReflection_Load.Value) { ScatteredReflection.Initiate();}
            if (Empathy_Load.Value) { Empathy.Initiate();}
            if (ScavengersPack_Load.Value) { ScavengersPack.Initiate();}
            if (TheUnforgivable_Load.Value) { TheUnforgivable.Initiate();}
            if (OverkillOverdrive_Load.Value) { OverkillOverdrive.Initiate();}
            // Legendary
            Log.LogInfo("Legendary");
            if (Apathy_Load.Value) { Apathy.Initiate();}
            if (MintCondition_Load.Value) { MintCondition.Initiate();}
            if (ElderMutagen_Load.Value) { ElderMutagen.Initiate();}
            if (DoNotEat_Load.Value) { DoNotEat.Initiate();}
            // if (TaxManStatement_Enable().Value == true) { TaxManStatement.Initiate(TaxManStatement_ChanceToInflict().Value, TaxManStatement_DamagePerTax().Value, TaxManStatement_BaseGoldPerTax().Value); }
            // Void
            Log.LogInfo("Void");
            if (CorruptingParasite_Load.Value) { CorruptingParasite.Initiate(); ArtifactOfCorruption.Initiate(); }
            if (NoticeOfAbsence_Load.Value) { NoticeOfAbsence.Initiate(); }
            if (DropOfNecrosis_Load.Value) { DropOfNecrosis.Initiate(); }
            if (CaptainsFavor_Load.Value) { CaptainsFavor.Initiate(); }
            if (Discovery_Load.Value) { Discovery.Initiate(); }
            if (SpatteredCollection_Load.Value) { SpatteredCollection.Initiate(); }
            if (TheHermit_Load.Value) { TheHermit.Initiate(); }
            // Lunar
            Log.LogInfo("Lunar");
            if (OneTicket_Load.Value) { OneTicket.Initiate(); }
            // Lunar Equipment
            Log.LogInfo("Lunar Equipment");
            if (BloodOfTheLamb_Load.Value) { BloodOfTheLamb.Initiate(); }

            // Make sure disabled items are actually disabled
            bool Run_IsItemAvailable(On.RoR2.Run.orig_IsItemAvailable orig, Run self, ItemIndex itemIndex)
            {
                UpdateItemStatuses(self);
                return orig(self, itemIndex);
            }

            void Run_BuildDropTable(On.RoR2.Run.orig_BuildDropTable orig, Run self)
            {
                UpdateItemStatuses(self);
                orig(self);
            }
            On.RoR2.Run.BuildDropTable += Run_BuildDropTable;
            On.RoR2.Run.IsItemAvailable += Run_IsItemAvailable;

            void UpdateItemStatuses(Run self)
            {
                // Common
                if (ShardOfGlass_Load.Value) { ShardOfGlass.UpdateItemStatus(self); }
                if (BucketList_Load.Value) { BucketList.UpdateItemStatus(self); }
                if (HopooEgg_Load.Value) { HopooEgg.UpdateItemStatus(self); }
                if (AtgPrototype_Load.Value) { AtgPrototype.UpdateItemStatus(self); }
                if (Tickets_Load.Value) { Tickets.UpdateItemStatus(self); }
                if (Balance_Load.Value) { Balance.UpdateItemStatus(self); }
                if (MinersHelmet_Load.Value) { MinersHelmet.UpdateItemStatus(self); }
                // Uncommon
                if (ScatteredReflection_Load.Value) { ScatteredReflection.UpdateItemStatus(self); }
                if (Empathy_Load.Value) { Empathy.UpdateItemStatus(self); }
                if (ScavengersPack_Load.Value) { ScavengersPack.UpdateItemStatus(self); }
                if (TheUnforgivable_Load.Value) { TheUnforgivable.UpdateItemStatus(self); }
                if (OverkillOverdrive_Load.Value) { OverkillOverdrive.UpdateItemStatus(self); }
                // Legendary
                if (Apathy_Load.Value) { Apathy.UpdateItemStatus(self); }
                if (MintCondition_Load.Value) { MintCondition.UpdateItemStatus(self); }
                if (ElderMutagen_Load.Value) { ElderMutagen.UpdateItemStatus(self); }
                if (DoNotEat_Load.Value) { DoNotEat.UpdateItemStatus(self); }
                // Void
                if (CorruptingParasite_Load.Value) { CorruptingParasite.UpdateItemStatus(self); }
                if (NoticeOfAbsence_Load.Value) { NoticeOfAbsence.UpdateItemStatus(self); }
                if (DropOfNecrosis_Load.Value) { DropOfNecrosis.UpdateItemStatus(self); }
                if (CaptainsFavor_Load.Value) { CaptainsFavor.UpdateItemStatus(self); }
                if (SpatteredCollection_Load.Value) { SpatteredCollection.UpdateItemStatus(self); }
                if (TheHermit_Load.Value) { TheHermit.UpdateItemStatus(self); }
                // Lunar
                if (OneTicket_Load.Value) { OneTicket.UpdateItemStatus(self); }
                // Lunar Equipment
                if (BloodOfTheLamb_Load.Value) { BloodOfTheLamb.UpdateItemStatus(self); }
            }

            Log.LogInfo("Done!");
        }
    }
}
