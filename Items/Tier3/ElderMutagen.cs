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
    On modded saves this might be broken, so let's keep the values low
    All of these buffs should be time limited
    */
    public class ElderMutagen
    {
        // Create functions here for defining the ITEM, TOKENS, HOOKS and CONFIG. This is simpler than doing it in Main
        static string itemName = "ElderMutagen"; // Change this name when making a new item
        static string upperName = itemName.ToUpper();
        static ItemDef itemDefinition = CreateItem();
        public static ItemDef hiddenItemDefinition = CreateHiddenItem();
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

            item.tags = new ItemTag[]{ ItemTag.Utility, ItemTag.Damage, ItemTag.AIBlacklist, ItemTag.BrotherBlacklist }; // Also change these when making a new item
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

        public static void AddTokens(float ElderMutagen_Duration, float ElderMutagen_Chance)
        {
            LanguageAPI.Add("H3_" + upperName + "_NAME", "Elder Mutagen");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Your buffs and inflicted debuffs have a chance to induce another, random buff or debuff.");
            LanguageAPI.Add("H3_" + upperName + "_DESC", "The <style=cIsUtility>first stack</style> of any <style=cIsHealing>buffs</style> you gain or <style=cDeath>debuffs</style> you inflict have a <style=cIsUtility>" + ElderMutagen_Chance + "%</style> chance to add an additional, random buff or debuff that lasts " + ElderMutagen_Duration + " seconds <style=cStack>(+" + ElderMutagen_Duration + " seconds per stack)</style>.");
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

        private static void AddHooks(ItemDef itemDefToHooks, ItemDef hiddenItemDefToHooks, float ElderMutagen_Duration, float ElderMutagen_Chance) // Insert hooks here
        {
            List<string> mutagenBuffWhitelist = new List<string> // All the buffs that SHOULD be considered for the item
            {
                // Unknown
                "The Hermit's Protection", "INS_BUFF_CHEESEWHEEL_NAME", "INS_BUFF_SHOCKED_NAME", "INS_BUFF_WRENCHATTACK_NAME", "INS_BUFF_WRENCHDAMAGE_NAME", "INS_BUFF_WRENCHMOVE_NAME", "Hysteria",
                "Strikes", "EliteReworksSlow80", "TKSATEngiSpeedispenserBuff", "TKSATHealsToDamage", "TKSATPixieArmor", "TKSATPixieAttackSpeed", "TKSATPixieDamage",
                "TKSATPixieMoveSpeed", "TKSATShrink", "BuffDefScintillatingJet", "BuffDefRecursionBullets", "MysticsItems_AllyDeathRevenge", "MysticsItems_CoffeeBoost",
                "MysticsItems_Crystallized", "MysticsItems_RhythmCombo",
                "DoubleItemsBuff", "HunterBoost", "Stealthed", "ZetPoached", "ZetSapped", "ZetShredded", "MoffeinHANDOverclock",
                "RiskyMod_BerzerkBuff", "RiskyMod_CrocoRegen", "RiskyMod_FreezeDebuff", "RiskyMod_ScytheBuff", "MeltingPot_BucketOn", "MeltingPot_Enraged",
                "MeltingPot_Mosquito",
                
                // Definitely works
                "bdArmorBoost", "bdAttackSpeedOnCrit", "bdBeetleJuice",
                "bdBleeding", "bdBlight", "bdClayGoo", "bdCloak", "bdCripple", "bdCloakSpeed", "bdCrocoRegen", "bdDeathMark", "bdElephantArmorBoost", "bdEnergized", "bdEntangle",
                "bdFruiting", "bdFullCrit", "bdHealingDisabled", "bdLifeSteal", "bdLunarSecondaryRoot", "bdMercExpose", "bdNoCooldowns", "bdNullified",
                "bdOnFire", "bdOverheat", "bdPoisoned", "bdPulverized", "bdSlow50", "bdSlow60", "bdSlow80", "bdSmallArmorBoost", "bdSuperBleed", "bdTeamWarCry",
                "bdWarbanner", "bdWarCryBuff", "bdWeak", "bdBodyArmor", "bdMeatRegenBoost", "bdFracture",
                "bdOutOfCombatArmorBuff", "bdStrongerBurn", "bdKillMoveSpeed", "bdPowerBuff", "bdWhipBoost", "bdTonicBuff"
                // FlatItemBuff and MysticsItems contain DOTs which would be nice to have in this, but I have no clue how to implement them
                // A problem for another day
            };

            List<BuffDef> allBuffDefs = new List<BuffDef> { }; // We need all the existing buffdefs so we can cycle through to look for valid ones

            void MutagenAddbuffs(BuffDef buffDef, CharacterBody body)
            {
                if (body.teamComponent && body.healthComponent && body.inventory)
                {
                    foreach (string buff in mutagenBuffWhitelist)
                    {
                        if (buffDef.name == buff) // If the acquired buff is on the whitelist, do the rest
                        {
                            Xoroshiro128Plus allBuffDefsSeed = new Xoroshiro128Plus(Run.instance.seed); // For SOME reason DOTs are not treated as debuffs, so we have to make some exceptions
                            if (buffDef.isDebuff == true || buffDef.name == "bdBleeding" || buffDef.name == "bdBlight" || buffDef.name == "bdOnFire" || buffDef.name == "bdOverheat" || buffDef.name == "bdPoisoned" || buffDef.name == "bdSuperBleed" || buffDef.name == "bdFracture" || buffDef.name == "bdStrongerBurn")
                            {
                                if (body.inventory.GetItemCount(hiddenItemDefToHooks) > 0 && body.teamComponent.teamIndex != TeamIndex.Player) // If body has the hidden item inflicted by the Mutagen, all debuffs will be duplicated
                                {
                                    Util.ShuffleList(allBuffDefs, allBuffDefsSeed);
                                    foreach (BuffDef i in allBuffDefs)
                                    {
                                        if (mutagenBuffWhitelist.Contains(i.name))
                                        {
                                            if (i.isDebuff == true || i.name == "bdBleeding" || i.name == "bdBlight" || i.name == "bdOnFire" || i.name == "bdOverheat" || i.name == "bdPoisoned" || i.name == "bdSuperBleed" || i.name == "bdFracture" || i.name == "bdStrongerBurn")
                                            {
                                                int numberOfPlayers = 0; // Block of code to check the average luckiness of the player team
                                                float luckAggregate = 0f;
                                                int mutagenAggregate = 0;
                                                TeamComponent mutagenLastOwner = new TeamComponent();

                                                foreach (TeamComponent player in TeamComponent.GetTeamMembers(TeamIndex.Player))
                                                {
                                                    if (player.GetComponent<CharacterBody>() != null && player.GetComponent<CharacterBody>().master && player.GetComponent<CharacterBody>().inventory)
                                                    {
                                                        luckAggregate += player.GetComponent<CharacterBody>().master.luck;
                                                        mutagenAggregate += player.GetComponent<CharacterBody>().inventory.GetItemCount(itemDefToHooks);
                                                        mutagenLastOwner = player;
                                                        numberOfPlayers += 1;
                                                    }
                                                }
                                                float totalLuck = luckAggregate / numberOfPlayers;

                                                if (Util.CheckRoll(ElderMutagen_Chance, totalLuck) == true) // holy moly
                                                {
                                                    if (i.name == "bdBleeding" || i.name == "bdBlight" || i.name == "bdOnFire" || i.name == "bdOverheat" || i.name == "bdPoisoned" || i.name == "bdSuperBleed" || i.name == "bdFracture" || i.name == "bdStrongerBurn" )
                                                    {
                                                        switch (i.name) // Handles given DOTs separately from buffs/debuffs
                                                        {
                                                            case "bdBleeding": DotController.InflictDot(body.gameObject, mutagenLastOwner.gameObject, DotController.DotIndex.Bleed, ElderMutagen_Duration * mutagenAggregate); break;
                                                            case "bdBlight": DotController.InflictDot(body.gameObject, mutagenLastOwner.gameObject, DotController.DotIndex.Blight, ElderMutagen_Duration * mutagenAggregate); break;
                                                            case "bdOnFire": DotController.InflictDot(body.gameObject, mutagenLastOwner.gameObject, DotController.DotIndex.Burn, ElderMutagen_Duration * mutagenAggregate); break;
                                                            case "bdOverHeat": DotController.InflictDot(body.gameObject, mutagenLastOwner.gameObject, DotController.DotIndex.PercentBurn, ElderMutagen_Duration * mutagenAggregate); break;
                                                            case "bdPoisoned": DotController.InflictDot(body.gameObject, mutagenLastOwner.gameObject, DotController.DotIndex.Poison, ElderMutagen_Duration * mutagenAggregate); break;
                                                            case "bdSuperBleed": DotController.InflictDot(body.gameObject, mutagenLastOwner.gameObject, DotController.DotIndex.SuperBleed, ElderMutagen_Duration * mutagenAggregate); break;
                                                            case "bdFracture": DotController.InflictDot(body.gameObject, mutagenLastOwner.gameObject, DotController.DotIndex.Fracture, ElderMutagen_Duration * mutagenAggregate); break;
                                                            case "bdStrongerBurn": DotController.InflictDot(body.gameObject, mutagenLastOwner.gameObject, DotController.DotIndex.StrongerBurn, ElderMutagen_Duration * mutagenAggregate); break;
                                                        }
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        body.AddTimedBuff(i, ElderMutagen_Duration * mutagenAggregate);
                                                        break;
                                                    }
                                                }
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            if (body.inventory.GetItemCount(itemDefToHooks) > 0)
                            {
                                if (buffDef.isDebuff == false && buffDef.name != "bdBleeding" && buffDef.name != "bdBlight" && buffDef.name != "bdOnFire" && buffDef.name != "bdOverheat" && buffDef.name != "bdPoisoned" && buffDef.name != "bdSuperBleed" && buffDef.name != "bdFracture" && buffDef.name != "bdStrongerBurn" && buffDef.name != "bdVoidFogMild" && buffDef.name != "bdVoidFogStrong")
                                {
                                    Util.ShuffleList(allBuffDefs, allBuffDefsSeed);
                                    foreach (BuffDef i in allBuffDefs)
                                    {
                                        if (mutagenBuffWhitelist.Contains(i.name))
                                        {
                                            if (i.isDebuff == false && i.name != "bdBleeding" && i.name != "bdBlight" && i.name != "bdOnFire" && i.name != "bdOverheat" && i.name != "bdPoisoned" && i.name != "bdSuperBleed" && i.name != "bdFracture" && i.name != "bdStrongerBurn" && buffDef.name != "bdVoidFogMild" && buffDef.name != "bdVoidFogStrong")
                                            {
                                                if (body.master && Util.CheckRoll(ElderMutagen_Chance, body.master.luck))
                                                {
                                                    body.AddTimedBuff(i, ElderMutagen_Duration * body.inventory.GetItemCount(itemDefToHooks));
                                                    break;
                                                }
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            void GetAllBuffs()
            {
                foreach (BuffDef def in BuffCatalog.buffDefs)
                {
                    allBuffDefs.Add(def);
                }
            }

            On.RoR2.CharacterBody.OnBuffFirstStackGained += (orig, self, buffDef) =>
            {
                orig(self, buffDef);
                MutagenAddbuffs(buffDef, self);
            };

            On.RoR2.HealthComponent.TakeDamage += (orig, self, damageInfo) => // If the attacker has the Mutagen, add a hidden item to the victim, marking them for debuff duplication
            {
                if (damageInfo.attacker && damageInfo.attacker.GetComponent<CharacterBody>() != null && damageInfo.attacker.GetComponent<CharacterBody>().inventory && damageInfo.attacker.GetComponent<CharacterBody>().inventory.GetItemCount(itemDefToHooks) > 0)
                {
                    if (self.GetComponent<CharacterBody>() != null && self.GetComponent<CharacterBody>().inventory)
                    {
                        self.GetComponent<CharacterBody>().inventory.GiveItem(hiddenItemDefToHooks);
                    }
                }
                orig(self, damageInfo);
            };

            RoR2Application.onLoad += GetAllBuffs;
        }

        public static ItemDef CreateHiddenItem()
        {
            ItemDef item = ScriptableObject.CreateInstance<ItemDef>(); // New hidden item to keep track of who can receive extra debuffs

            item.name = "ElderMutagenHidden";
            item.nameToken = "H3_" + upperName + "_NAME";
            item.pickupToken = "H3_" + upperName + "_PICKUP";
            item.descriptionToken = "H3_" + upperName + "_DESC";
            item.loreToken = "H3_" + upperName + "_LORE";

            item.tags = new ItemTag[] { ItemTag.CannotCopy, ItemTag.CannotSteal, ItemTag.CannotDuplicate };
            item.deprecatedTier = ItemTier.NoTier;
            item.canRemove = false;
            item.hidden = true;

            item.pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/ElderMutagenPrefab.prefab");
            item.pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Textures/Icons/ElderMutagen.png");

            return item;
        }

        public static void Initiate(float ElderMutagen_Duration, float ElderMutagen_Chance) // Finally, initiate the item and all of its features
        {
            ItemAPI.Add(new CustomItem(itemDefinition, CreateDisplayRules()));
            ItemAPI.Add(new CustomItem(hiddenItemDefinition, CreateHiddenDisplayRules()));
            AddTokens(ElderMutagen_Duration, ElderMutagen_Chance);
            AddHooks(itemDefinition, hiddenItemDefinition, ElderMutagen_Duration, ElderMutagen_Chance);
        }
    }
}
