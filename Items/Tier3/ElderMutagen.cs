using BepInEx;
using BepInEx.Configuration;
using R2API;
using R2API.Utils;
using RoR2;
using RoR2.Orbs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Hex3Mod;
using Hex3Mod.Logging;
using Hex3Mod.HelperClasses;

namespace Hex3Mod.Items
{
    /*
    Elder Mutagen is a buff to Death Mark and to debuff/buff builds, as if they needed one.
    It's been reworked in order to make its effect more consistent.
    */
    public class ElderMutagen
    {
        // Create functions here for defining the ITEM, TOKENS, HOOKS and CONFIG. This is simpler than doing it in Main
        static string itemName = "ElderMutagen"; // Change this name when making a new item
        static string upperName = itemName.ToUpper();
        static ItemDef itemDefinition = CreateItem();
        public static GameObject LoadPrefab()
        {
            GameObject pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/ElderMutagenPrefab.prefab");
            return pickupModelPrefab;
        }
        public static Sprite LoadSprite()
        {
            Sprite pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/ElderMutagen.png");
            return pickupIconSprite;
        }
        public static ItemDef CreateItem()
        {
            ItemDef item = ScriptableObject.CreateInstance<ItemDef>();

            item.name = itemName;
            item.nameToken = "H3_" + upperName + "_NAME";
            item.pickupToken = "H3_" + upperName + "_PICKUP";
            item.descriptionToken = "H3_" + upperName + "_DESC";
            item.loreToken = "H3_" + upperName + "_LORE";

            item.tags = new ItemTag[]{ ItemTag.Utility, ItemTag.Damage };
            item.deprecatedTier = ItemTier.Tier3;
            item.canRemove = true;
            item.hidden = false;

            item.pickupModelPrefab = LoadPrefab();
            item.pickupIconSprite = LoadSprite();

            return item;
        }

        public static ItemDisplayRuleDict CreateDisplayRules() // We've figured item displays out!
        {
            GameObject ItemDisplayPrefab = helpers.PrepareItemDisplayModel(PrefabAPI.InstantiateClone(LoadPrefab(), LoadPrefab().name + "Display", false));

            ItemDisplayRuleDict rules = new ItemDisplayRuleDict();
            rules.Add("mdlCommandoDualies", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(0.13142F, 0.1467F, 0.08031F),
                        localAngles = new Vector3(297.3017F, 14.59079F, 48.55595F),
                        localScale = new Vector3(0.06707F, 0.13088F, 0.06707F)
                    }
                }
            );
            rules.Add("mdlHuntress", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(-0.01099F, 0.04634F, -0.01131F),
                        localAngles = new Vector3(4.94392F, 0.02119F, -0.00515F),
                        localScale = new Vector3(0.10607F, 0.10607F, 0.10607F)
                    }
                }
            );
            rules.Add("mdlToolbot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "UpperArmL",
                        localPos = new Vector3(-0.21565F, 3.81528F, -1.77649F),
                        localAngles = new Vector3(13.28516F, 342.2303F, 346.2823F),
                        localScale = new Vector3(0.66386F, 0.66386F, 0.66386F)
                    }
                }
            );
            rules.Add("mdlEngi", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "ThighR",
                        localPos = new Vector3(-0.07713F, 0.19162F, -0.10058F),
                        localAngles = new Vector3(74.02536F, 297.283F, 256.941F),
                        localScale = new Vector3(0.09095F, 0.09095F, 0.09095F)
                    }
                }
            );
            rules.Add("mdlMage", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "FootL",
                        localPos = new Vector3(0.00348F, 0.06752F, 0.08452F),
                        localAngles = new Vector3(53.72561F, 15.16084F, 192.0224F),
                        localScale = new Vector3(-0.04578F, 0.04536F, 0.03792F)
                    }
                }
            );
            rules.Add("mdlMerc", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "ThighL",
                        localPos = new Vector3(0.06946F, 0.29215F, -0.10201F),
                        localAngles = new Vector3(84.85436F, 229.5896F, 265.6841F),
                        localScale = new Vector3(0.08755F, 0.08755F, 0.08755F)
                    }
                }
            );
            rules.Add("mdlTreebot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "PlatformBase",
                        localPos = new Vector3(0F, 0F, 0F),
                        localAngles = new Vector3(0.00012F, 74.58047F, -0.00001F),
                        localScale = new Vector3(0.49053F, 0.49053F, 0.49053F)
                    }
                }
            );
            rules.Add("mdlLoader", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "MechLowerArmL",
                        localPos = new Vector3(0.0653F, 0.22464F, 0.04163F),
                        localAngles = new Vector3(84.2281F, 267.1171F, 268.1745F),
                        localScale = new Vector3(0.13033F, 0.13033F, 0.13033F)
                    }
                }
            );
            rules.Add("mdlCroco", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Stomach",
                        localPos = new Vector3(1.40725F, 0.29892F, 1.91719F),
                        localAngles = new Vector3(307.247F, 344.692F, 52.17693F),
                        localScale = new Vector3(0.86716F, 0.86716F, 0.86716F)
                    }
                }
            );
            rules.Add("mdlCaptain", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "ClavicleR",
                        localPos = new Vector3(0.04858F, 0.13547F, -0.12345F),
                        localAngles = new Vector3(72.32738F, 243.9777F, 85.64837F),
                        localScale = new Vector3(0.09552F, 0.09552F, 0.09552F)
                    }
                }
            );
            rules.Add("mdlBandit2", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "ThighL",
                        localPos = new Vector3(0.05238F, 0.36679F, 0.07032F),
                        localAngles = new Vector3(69.67028F, 312.9913F, 272.3289F),
                        localScale = new Vector3(0.05981F, 0.05981F, 0.05981F)
                    }
                }
            );
            rules.Add("EngiTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Base",
                        localPos = new Vector3(0F, 0.86834F, 0F),
                        localAngles = new Vector3(0F, 0F, 0F),
                        localScale = new Vector3(0.42677F, 0.42677F, 0.42677F)
                    }
                }
            );
            rules.Add("EngiWalkerTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Base",
                        localPos = new Vector3(0F, 0.87072F, 0F),
                        localAngles = new Vector3(0F, 0F, 0F),
                        localScale = new Vector3(0.44815F, 0.44815F, 0.44815F)
                    }
                }
            );
            rules.Add("mdlScav", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(-5.16409F, 3.66659F, -0.02804F),
                        localAngles = new Vector3(337.9631F, 359.9089F, 24.0611F),
                        localScale = new Vector3(-1.41672F, -3.30552F, -1.41672F)
                    }
                }
            ); 
            rules.Add("mdlRailGunner", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Backpack",
                        localPos = new Vector3(-0.26249F, 0.2179F, 0.00295F),
                        localAngles = new Vector3(304.3797F, 175.19F, 274.0369F),
                        localScale = new Vector3(0.11333F, 0.1129F, 0.1129F)
                    }
                }
            );
            rules.Add("mdlVoidSurvivor", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "ForeArmR",
                        localPos = new Vector3(0.11695F, 0.14536F, -0.20407F),
                        localAngles = new Vector3(79.43037F, 354.2926F, 354.4371F),
                        localScale = new Vector3(0.12869F, 0.12869F, 0.12869F)
                    }
                }
            );

            return rules;
        }

        // Hidden items should not display at all
        public static ItemDisplayRuleDict CreateHiddenDisplayRules()
        {
            return new ItemDisplayRuleDict();
        }

        public static void AddTokens(float ElderMutagen_Duration, float ElderMutagen_Chance, float ElderMutagen_Interval)
        {
            LanguageAPI.Add("H3_" + upperName + "_NAME", "Elder Mutagen");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Your hits have a small chance to inflict random debuffs. Every " + ElderMutagen_Interval + " seconds, gain a random buff.");
            LanguageAPI.Add("H3_" + upperName + "_DESC", "Your hits have a <style=cIsDamage>" + ElderMutagen_Chance + "%</style> chance to inflict a <style=cIsDamage>random debuff</style> that lasts <style=cIsDamage>" + ElderMutagen_Duration + "</style> seconds <style=cStack>(+" + ElderMutagen_Duration + " per stack)</style>. Every " + ElderMutagen_Interval + " seconds, gain a <style=cIsHealing>random buff</style> that lasts <style=cIsHealing>" + ElderMutagen_Duration + "</style> seconds <style=cStack>(+" + ElderMutagen_Duration + " per stack)</style>.");
            LanguageAPI.Add("H3_" + upperName + "_LORE", "<style=cMono>Lab Dissection Analysis File</style> " +
            "\n\nSubject: Elder Mutagen" +
            "\nTechnician: Alex [REDACTED]" +
            "\nTable Spec: Table" +
            "\nNotes:" +
            "\n\n> I don't know how to use this" +
            "\n> Oh that's the button" +
            "\n> We found something strange. We aren't scientists but we need to make sure it's safe to transport." +
            "\n> Blob of living jelly, air around it changes from cold to hot" +
            "\n> Poking it" +
            "\n> Blob tried to latch onto tweezers, had to give them up" +
            "\n> Tweezers growing spiky growths, twisting into spirals?" +
            "\n> Throwing on a spider" +
            "\n> Blob let the spider go" +
            "\n> Spider fast uncontrollable" +
            "\n> we killed it" +
            "\n> Blob grabbed onto Jolene's arm" +
            "\n> Somethings wrong" +
            "\n> Timestamping for break");
        }

        private static void AddHooks(ItemDef itemDefToHooks, float ElderMutagen_Duration, float ElderMutagen_Chance, float ElderMutagen_Interval) // Insert hooks here
        {
            List<string> mutagenBuffWhitelist = new List<string> // All the buffs that SHOULD be considered for the item
            {
                // Unknown
                "INS_BUFF_CHEESEWHEEL_NAME", "INS_BUFF_SHOCKED_NAME", "INS_BUFF_WRENCHATTACK_NAME", "INS_BUFF_WRENCHDAMAGE_NAME", "INS_BUFF_WRENCHMOVE_NAME", "Hysteria",
                "Strikes", "EliteReworksSlow80", "TKSATEngiSpeedispenserBuff", "TKSATHealsToDamage", "TKSATPixieArmor", "TKSATPixieAttackSpeed", "TKSATPixieDamage",
                "TKSATPixieMoveSpeed", "TKSATShrink", "BuffDefScintillatingJet", "BuffDefRecursionBullets", "MysticsItems_AllyDeathRevenge", "MysticsItems_CoffeeBoost",
                "MysticsItems_Crystallized", "MysticsItems_RhythmCombo",
                "DoubleItemsBuff", "HunterBoost", "Stealthed", "ZetPoached", "ZetSapped", "ZetShredded", "MoffeinHANDOverclock",
                "RiskyMod_BerzerkBuff", "RiskyMod_CrocoRegen", "RiskyMod_FreezeDebuff", "RiskyMod_ScytheBuff", "MeltingPot_BucketOn", "MeltingPot_Enraged",
                "MeltingPot_Mosquito",
                
                // Definitely works
                "bdArmorBoost", "bdAttackSpeedOnCrit", "bdBeetleJuice",
                "bdBleeding", "bdBlight", "bdClayGoo", "bdCloak", "bdCripple", "bdCloakSpeed", "bdCrocoRegen", "bdDeathMark", "bdElephantArmorBoost", "bdEnergized", "bdEntangle",
                "bdFruiting", "bdFullCrit", "bdHealingDisabled", "bdLunarSecondaryRoot", "bdNoCooldowns", "bdNullified",
                "bdOnFire", "bdOverheat", "bdPoisoned", "bdPulverized", "bdSlow50", "bdSlow60", "bdSlow80", "bdSmallArmorBoost", "bdSuperBleed", "bdTeamWarCry",
                "bdWarbanner", "bdWarCryBuff", "bdWeak", "bdBodyArmor", "bdMeatRegenBoost", "bdFracture",
                "bdStrongerBurn", "bdKillMoveSpeed", "bdPowerBuff", "bdTonicBuff"
                // FlatItemBuff and MysticsItems contain DOTs which would be nice to have in this, but I have no clue how to implement them
                // A problem for another day
            };

            List<BuffDef> allBuffDefs = new List<BuffDef> { }; // We need all the existing buffdefs so we can cycle through to look for valid ones

            // Hitting an enemy has a 5% chance of inflicting a random debuff
            On.RoR2.GlobalEventManager.OnHitEnemy += (orig, self, damageInfo, victim) =>
            {
                orig(self, damageInfo, victim);
                if (damageInfo.attacker && damageInfo.attacker.GetComponent<CharacterBody>() && damageInfo.attacker.GetComponent<CharacterBody>().inventory && victim.GetComponent<CharacterBody>() && damageInfo.attacker.GetComponent<CharacterBody>().master)
                {
                    Inventory attackerInventory = damageInfo.attacker.GetComponent<CharacterBody>().inventory;
                    CharacterMaster attackerMaster = damageInfo.attacker.GetComponent<CharacterBody>().master;
                    CharacterBody victimBody = victim.GetComponent<CharacterBody>();
                    if (attackerInventory.GetItemCount(itemDefToHooks) > 0)
                    {
                        // Get the first debuff available from a random list
                        Xoroshiro128Plus seed = new Xoroshiro128Plus(Run.instance.seed);
                        Util.ShuffleList(mutagenBuffWhitelist, seed);
                        Util.ShuffleList(allBuffDefs, seed);
                        foreach (BuffDef i in allBuffDefs)
                        {
                            if (i.isDebuff || i.name == "bdBleeding" || i.name == "bdBlight" || i.name == "bdOnFire" || i.name == "bdOverheat" || i.name == "bdPoisoned" || i.name == "bdSuperBleed" || i.name == "bdFracture" || i.name == "bdStrongerBurn" || i.name == "bdVoidFogMild" || i.name == "bdVoidFogStrong" || i.name == "MysticsItems_Crystallized")
                            {
                                foreach (string debuffName in mutagenBuffWhitelist)
                                {
                                    if (debuffName == i.name)
                                    {
                                        if (Util.CheckRoll(ElderMutagen_Chance * damageInfo.procCoefficient, attackerMaster.luck, attackerMaster))
                                        {
                                            if (i.name == "bdBleeding" || i.name == "bdBlight" || i.name == "bdOnFire" || i.name == "bdOverheat" || i.name == "bdPoisoned" || i.name == "bdSuperBleed" || i.name == "bdFracture" || i.name == "bdStrongerBurn")
                                            {
                                                switch (i.name) // Handles given DOTs separately from buffs/debuffs
                                                {
                                                    case "bdBleeding": DotController.InflictDot(victim, damageInfo.attacker, DotController.DotIndex.Bleed, ElderMutagen_Duration * attackerInventory.GetItemCount(itemDefToHooks)); break;
                                                    case "bdBlight": DotController.InflictDot(victim, damageInfo.attacker, DotController.DotIndex.Blight, ElderMutagen_Duration * attackerInventory.GetItemCount(itemDefToHooks)); break;
                                                    case "bdOnFire": DotController.InflictDot(victim, damageInfo.attacker, DotController.DotIndex.Burn, ElderMutagen_Duration * attackerInventory.GetItemCount(itemDefToHooks)); break;
                                                    case "bdOverHeat": DotController.InflictDot(victim, damageInfo.attacker, DotController.DotIndex.PercentBurn, ElderMutagen_Duration * attackerInventory.GetItemCount(itemDefToHooks)); break;
                                                    case "bdPoisoned": DotController.InflictDot(victim, damageInfo.attacker, DotController.DotIndex.Poison, ElderMutagen_Duration * attackerInventory.GetItemCount(itemDefToHooks)); break;
                                                    case "bdSuperBleed": DotController.InflictDot(victim, damageInfo.attacker, DotController.DotIndex.SuperBleed, ElderMutagen_Duration * attackerInventory.GetItemCount(itemDefToHooks)); break;
                                                    case "bdFracture": DotController.InflictDot(victim, damageInfo.attacker, DotController.DotIndex.Fracture, ElderMutagen_Duration * attackerInventory.GetItemCount(itemDefToHooks)); break;
                                                    case "bdStrongerBurn": DotController.InflictDot(victim, damageInfo.attacker, DotController.DotIndex.StrongerBurn, ElderMutagen_Duration * attackerInventory.GetItemCount(itemDefToHooks)); break;
                                                }
                                                return;
                                            }
                                            else
                                            {
                                                victimBody.AddTimedBuff(i, ElderMutagen_Duration * attackerInventory.GetItemCount(itemDefToHooks));
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            // Every 15 seconds, add a random buff to the item user
            On.RoR2.CharacterBody.Update += (orig, self) =>
            {
                orig(self);
                if (self.GetComponent<MutagenItemBehavior>() != null)
                {
                    MutagenItemBehavior mutagenBehavior = self.GetComponent<MutagenItemBehavior>();
                    if (self.inventory && self.inventory.GetItemCount(itemDefToHooks) > 0)
                    {
                        mutagenBehavior.buffTimer += Time.deltaTime;
                        if (mutagenBehavior.buffTimer > ElderMutagen_Interval)
                        {
                            // Get the first buff available from a random list
                            Xoroshiro128Plus seed = new Xoroshiro128Plus(Run.instance.seed);
                            Util.ShuffleList(mutagenBuffWhitelist, seed);
                            Util.ShuffleList(allBuffDefs, seed);
                            foreach (BuffDef i in allBuffDefs)
                            {
                                if (!i.isDebuff && i.name != "bdBleeding" && i.name != "bdBlight" && i.name != "bdOnFire" && i.name != "bdOverheat" && i.name != "bdPoisoned" && i.name != "bdSuperBleed" && i.name != "bdFracture" && i.name != "bdStrongerBurn" && i.name != "bdVoidFogMild" && i.name != "bdVoidFogStrong" && i.name != "MysticsItems_Crystallized")
                                {
                                    foreach (string buffName in mutagenBuffWhitelist)
                                    {
                                        if (buffName == i.name)
                                        {
                                            self.AddTimedBuff(i, ElderMutagen_Duration * self.inventory.GetItemCount(itemDefToHooks));
                                            mutagenBehavior.buffTimer = 0f;
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            // Whenever the inventory is changed, check for Elder Mutagens and add a MutagenItemBehavior if applicable
            On.RoR2.CharacterBody.OnInventoryChanged += (orig, self) =>
            {
                orig(self);
                if (self.inventory.GetItemCount(itemDefToHooks) > 0 && self.GetComponent<MutagenItemBehavior>() == null)
                {
                    self.AddItemBehavior<MutagenItemBehavior>(self.inventory.GetItemCount(itemDefToHooks));
                }
            };

            // Retrieve all buffs in the game- including modded- and put them in a list
            void GetAllBuffs()
            {
                foreach (BuffDef def in BuffCatalog.buffDefs)
                {
                    allBuffDefs.Add(def);
                }
            }

            RoR2Application.onLoad += GetAllBuffs;
        }

        // Class that each CharacterBody gets to help store their individual buff timers
        public class MutagenItemBehavior : CharacterBody.ItemBehavior
        {
            public float buffTimer = 0f;
        }

        public static void Initiate(float ElderMutagen_Duration, float ElderMutagen_Chance, float ElderMutagen_Interval) // Finally, initiate the item and all of its features
        {
            ItemAPI.Add(new CustomItem(itemDefinition, CreateDisplayRules()));
            AddTokens(ElderMutagen_Duration, ElderMutagen_Chance, ElderMutagen_Interval);
            AddHooks(itemDefinition, ElderMutagen_Duration, ElderMutagen_Chance, ElderMutagen_Interval);
        }
    }
}
