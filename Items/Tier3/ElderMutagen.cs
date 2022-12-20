using R2API;
using RoR2;
using System.Collections.Generic;
using UnityEngine;
using Hex3Mod.HelperClasses;
using System.Linq;
using Hex3Mod.Utils;
using static Hex3Mod.Main;
using System;

namespace Hex3Mod.Items
{
    /*
    Two reworks in a row, huh?
    I wanted a max hp / regen item because those stats are underrated. Ramps up in a unique way to make lunar seers slightly more useful
    */
    public class ElderMutagen
    {
        static string itemName = "ElderMutagen";
        static string upperName = itemName.ToUpper();
        public static ItemDef itemDef;
        public static GameObject LoadPrefab()
        {
            GameObject pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/VFXPASS3/Models/Prefabs/ElderMutagen.prefab");
            if (Main.debugMode == true)
            {
                foreach (Renderer renderer in pickupModelPrefab.GetComponentsInChildren<Renderer>())
                {
                    renderer.gameObject.AddComponent<MaterialControllerComponents.HGControllerFinder>();
                }
            }
            return pickupModelPrefab;
        }
        public static Sprite LoadSprite()
        {
            Sprite pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/VFXPASS3/Icons/ElderMutagen.png");
            return pickupIconSprite;
        }
        public static Sprite LoadBuffSprite()
        {
            Sprite pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/VFXPASS3/Icons/Buff_Mutagen.png");
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

            item.tags = new ItemTag[]{ ItemTag.Healing, ItemTag.AIBlacklist, ItemTag.BrotherBlacklist};
            item.deprecatedTier = ItemTier.Tier3;
            item.canRemove = true;
            item.hidden = false;
            item.requiredExpansion = Hex3ModExpansion;

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

        public static void AddTokens()
        {
            LanguageAPI.Add("H3_" + upperName + "_NAME", "Elder Mutagen");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Gain permanent max health and regen for each unique monster species killed.");
            LanguageAPI.Add("H3_" + upperName + "_LORE", "<style=cMono>Lab Dissection Analysis File</style> " +
            "\n\nSubject: Strange Blob" +
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
            "\n> Spider uncontrollable" +
            "\n> we killed it" +
            "\n> Blob grabbed onto Jolene's arm" +
            "\n> Somethings wrong" +
            "\n> Timestamping for break");
        }
        public static void UpdateItemStatus(Run run)
        {
            if (!ElderMutagen_Enable.Value)
            {
                LanguageAPI.AddOverlay("H3_" + upperName + "_NAME", "Elder Mutagen" + " <style=cDeath>[DISABLED]</style>");
                if (run && run.availableItems.Contains(itemDef.itemIndex)) { run.availableItems.Remove(itemDef.itemIndex); }
            }
            else
            {
                LanguageAPI.AddOverlay("H3_" + upperName + "_NAME", "Elder Mutagen");
                if (run && !run.availableItems.Contains(itemDef.itemIndex) && run.IsExpansionEnabled(Hex3ModExpansion)) { run.availableItems.Add(itemDef.itemIndex); }
            }
            LanguageAPI.AddOverlay("H3_" + upperName + "_DESC", string.Format("Killing a new monster species grants a permanent <style=cIsHealing>{0} max health</style> and <style=cIsHealing>{1} hp/s regeneration bonus</style>. Each stack allows you to gain this bonus <style=cIsHealing>1</style> more time from all species.", ElderMutagen_MaxHealthFlatAdd.Value, ElderMutagen_RegenAdd.Value));
        }

        private static void AddHooks()
        {
            // On kill, add the victim's name to the dictionary
            void DeathRewards_OnKilledServer(On.RoR2.DeathRewards.orig_OnKilledServer orig, DeathRewards self, DamageReport damageReport)
            {
                orig(self, damageReport);
                if (damageReport.attackerBody && damageReport.victimBody && damageReport.attackerBody.inventory && damageReport.attackerBody.master && damageReport.attackerBody.master.playerCharacterMasterController)
                {
                    if (damageReport.attackerBody.inventory.GetItemCount(itemDef) <= 0)
                    {
                        if (damageReport.attackerBody.master.playerCharacterMasterController.gameObject.GetComponent<MutagenBehavior>())
                        {
                            UnityEngine.Object.Destroy(damageReport.attackerBody.master.playerCharacterMasterController.gameObject.GetComponent<MutagenBehavior>());
                        }
                        return;
                    }
                    Inventory inventory = damageReport.attackerBody.inventory;
                    PlayerCharacterMasterController masterController = damageReport.attackerBody.master.playerCharacterMasterController;
                    if (!masterController.gameObject.GetComponent<MutagenBehavior>())
                    {
                        masterController.gameObject.AddComponent<MutagenBehavior>();
                    }
                    MutagenBehavior behavior = masterController.gameObject.GetComponent<MutagenBehavior>();
                    var speciesDict = behavior.speciesDict;
                    string speciesName = damageReport.victimBody.name.Replace("(Clone)", "").Trim();

                    if (!speciesDict.ContainsKey(speciesName))
                    {
                        speciesDict.Add(speciesName, 1);
                        EffectManager.SimpleImpactEffect(HealthComponent.AssetReferences.permanentDebuffEffectPrefab, damageReport.attackerBody.corePosition, damageReport.attackerBody.corePosition, true);
                        damageReport.attackerBody.RecalculateStats();
                    }
                    else
                    {
                        if (speciesDict[speciesName] < inventory.GetItemCount(itemDef))
                        {
                            speciesDict[speciesName]++;
                            EffectManager.SimpleImpactEffect(HealthComponent.AssetReferences.permanentDebuffEffectPrefab, damageReport.attackerBody.corePosition, damageReport.attackerBody.corePosition, true);
                            damageReport.attackerBody.RecalculateStats();
                        }
                    }
                }
            }

            void GetStatCoefficients(CharacterBody body, RecalculateStatsAPI.StatHookEventArgs args)
            {
                if (body.master && body.master.playerCharacterMasterController)
                {
                    if (body.master.playerCharacterMasterController.gameObject.TryGetComponent(out MutagenBehavior mutagenBehavior))
                    {
                        args.baseRegenAdd += ElderMutagen_RegenAdd.Value * mutagenBehavior.GetSpeciesKilledNumber();
                        args.baseHealthAdd += (float)ElderMutagen_MaxHealthFlatAdd.Value * mutagenBehavior.GetSpeciesKilledNumber();
                        body.SetBuffCount(mutagenStacks.buffIndex, mutagenBehavior.GetSpeciesKilledNumber());
                    }
                    else
                    {
                        body.SetBuffCount(mutagenStacks.buffIndex, 0);
                    }
                }
            }

            On.RoR2.DeathRewards.OnKilledServer += DeathRewards_OnKilledServer;
            RecalculateStatsAPI.GetStatCoefficients += GetStatCoefficients;
        }

        public class MutagenBehavior : MonoBehaviour
        {
            public Dictionary<string, int> speciesDict = new Dictionary<string, int>();

            public int GetSpeciesKilledNumber()
            {
                int killsResult = 0;
                foreach (int kills in speciesDict.Values)
                {
                    killsResult += kills;
                }
                return killsResult;
            }
        }

        public static BuffDef mutagenStacks { get; private set; }
        public static void AddBuffs() // Visual indicator of Apathy stacks
        {
            mutagenStacks = ScriptableObject.CreateInstance<BuffDef>();
            mutagenStacks.buffColor = new Color(0.796f, 0.192f, 0.086f);
            mutagenStacks.canStack = true;
            mutagenStacks.isDebuff = false;
            mutagenStacks.name = "Elder Mutagen Stacks";
            mutagenStacks.isHidden = false;
            mutagenStacks.isCooldown = false;
            mutagenStacks.iconSprite = LoadBuffSprite();
            ContentAddition.AddBuffDef(mutagenStacks);
        }

        public static void Initiate()
        {
            itemDef = CreateItem();
            ItemAPI.Add(new CustomItem(itemDef, CreateDisplayRules()));
            AddTokens();
            UpdateItemStatus(null);
            AddBuffs();
            AddHooks();
        }
    }
}
