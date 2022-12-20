using R2API;
using RoR2;
using UnityEngine;
using Hex3Mod.HelperClasses;
using System;
using UnityEngine.PlayerLoop;
using RoR2.Achievements;
using UnityEngine.AddressableAssets;
using RoR2.Items;
using Hex3Mod.Utils;
using static Hex3Mod.Main;

namespace Hex3Mod.Items
{
    /*
    Based off of the Miner survivor's ability, this helmet has a slightly different function
    It should provide a good use for money-earning items (such as The Tax Man's Statement) but still be useful otherwise.
    */
    public class MinersHelmet
    {
        static string itemName = "MinersHelmet";
        static string upperName = itemName.ToUpper();
        public static ItemDef itemDef;
        public static GameObject LoadPrefab()
        {
            GameObject pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/MinersHelmetPrefab.prefab");
            if (Main.debugMode == true)
            {
                pickupModelPrefab.GetComponentInChildren<Renderer>().gameObject.AddComponent<MaterialControllerComponents.HGControllerFinder>();
            }
            return pickupModelPrefab;
        }
        public static Sprite LoadSprite()
        {
            Sprite pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/MinersHelmet.png");
            return pickupIconSprite;
        }
        public static Sprite LoadAchievementSprite()
        {
            Sprite achievementIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/MinersHelmetAchievement.png");
            return achievementIconSprite;
        }
        public static GameObject miningFX = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Common/VFX/CoinImpact.prefab").WaitForCompletion();
        public static ItemDef CreateItem()
        {
            ItemDef item = ScriptableObject.CreateInstance<ItemDef>();
            UnlockableDef minersHelmetUnlock = ScriptableObject.CreateInstance<UnlockableDef>();

            minersHelmetUnlock.cachedName = "MinersHelmetUnlock";
            minersHelmetUnlock.nameToken = upperName + "_UNLOCK_NAME";
            minersHelmetUnlock.achievementIcon = LoadAchievementSprite();

            item.name = itemName;
            item.nameToken = "H3_" + upperName + "_NAME";
            item.pickupToken = "H3_" + upperName + "_PICKUP";
            item.descriptionToken = "H3_" + upperName + "_DESC";
            item.loreToken = "H3_" + upperName + "_LORE";

            item.tags = new ItemTag[]{ ItemTag.Utility, ItemTag.BrotherBlacklist, ItemTag.AIBlacklist}; // Useless on monsters
            item.deprecatedTier = ItemTier.Tier1;
            item.canRemove = true;
            item.hidden = false;
            item.requiredExpansion = Hex3ModExpansion;

            item.pickupModelPrefab = LoadPrefab();
            item.pickupIconSprite = LoadSprite();

            ContentAddition.AddUnlockableDef(minersHelmetUnlock);
            item.unlockableDef = minersHelmetUnlock;
            return item;
        }

        public static ItemDisplayRuleDict CreateDisplayRules()
        {
            GameObject ItemDisplayPrefab = helpers.PrepareItemDisplayModel(PrefabAPI.InstantiateClone(LoadPrefab(), LoadPrefab().name + "Display", false));

            ItemDisplayRuleDict rules = new ItemDisplayRuleDict();
            rules.Add("mdlCommandoDualies", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(0.00068F, 0.32852F, 0.00065F),
                        localAngles = new Vector3(359.9512F, 0.35731F, 357.4169F),
                        localScale = new Vector3(0.33462F, 0.33462F, 0.33462F)
                    }
                }
            );
            rules.Add("mdlHuntress", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(-0.00392F, 0.24688F, -0.07316F),
                        localAngles = new Vector3(335.9612F, 359.8617F, 0.09343F),
                        localScale = new Vector3(0.32652F, 0.32652F, 0.32652F)
                    }
                }
            );
            rules.Add("mdlToolbot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(-0.01578F, 2.38932F, 1.03125F),
                        localAngles = new Vector3(287.2017F, 178.5396F, 0.71412F),
                        localScale = new Vector3(2.59556F, 2.59556F, 2.59556F)
                    }
                }
            );
            rules.Add("mdlEngi", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "HeadCenter",
                        localPos = new Vector3(0.00021F, 0.04583F, -0.01136F),
                        localAngles = new Vector3(0F, 0F, 0.3786F),
                        localScale = new Vector3(0.42619F, 0.42619F, 0.42619F)
                    }
                }
            );
            rules.Add("mdlMage", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(-0.00058F, 0.1209F, -0.1029F),
                        localAngles = new Vector3(323.2114F, 359.7625F, 0.0695F),
                        localScale = new Vector3(0.24341F, 0.24341F, 0.24341F)
                    }
                }
            );
            rules.Add("mdlMerc", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(-0.00077F, 0.18063F, 0.02227F),
                        localAngles = new Vector3(0F, 0F, 0F),
                        localScale = new Vector3(0.31446F, 0.31446F, 0.31446F)
                    }
                }
            );
            rules.Add("mdlTreebot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "PlatformBase",
                        localPos = new Vector3(0.79265F, 2.62522F, -0.31152F),
                        localAngles = new Vector3(40.41008F, 0.00004F, 332.3408F),
                        localScale = new Vector3(0.33679F, 0.33679F, 0.33679F)
                    }
                }
            );
            rules.Add("mdlLoader", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(-0.00104F, 0.19781F, 0.00938F),
                        localAngles = new Vector3(0F, 0F, 0F),
                        localScale = new Vector3(0.29817F, 0.29817F, 0.29817F)
                    }
                }
            );
            rules.Add("mdlCroco", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(-0.01549F, 0.61271F, 1.18961F),
                        localAngles = new Vector3(279.3741F, 359.0403F, 182.7696F),
                        localScale = new Vector3(4.07411F, 4.07411F, 4.09006F)
                    }
                }
            );
            rules.Add("mdlCaptain", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(-0.00433F, 0.23898F, -0.01396F),
                        localAngles = new Vector3(332.1655F, 358.2135F, 2.67275F),
                        localScale = new Vector3(0.36612F, 0.36612F, 0.36612F)
                    }
                }
            );
            rules.Add("mdlBandit2", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Hat",
                        localPos = new Vector3(0.00388F, 0.02513F, -0.01429F),
                        localAngles = new Vector3(336.42F, 354.6921F, 2.05082F),
                        localScale = new Vector3(0.29852F, 0.29852F, 0.29852F)
                    }
                }
            );
            rules.Add("EngiTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(0F, 0.8848F, -0.93834F),
                        localAngles = new Vector3(358.0704F, 0F, 0F),
                        localScale = new Vector3(1.24942F, 1.24942F, 1.24942F)
                    }
                }
            );
            rules.Add("EngiWalkerTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(0F, 1.42949F, -0.51307F),
                        localAngles = new Vector3(0F, 0F, 0F),
                        localScale = new Vector3(1.28713F, 1.28713F, 1.28713F)
                    }
                }
            );
            rules.Add("mdlScav", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(0F, 4.34397F, 2.3054F),
                        localAngles = new Vector3(332.695F, 149.7359F, 15.02011F),
                        localScale = new Vector3(12.1353F, 12.1353F, 12.1353F)
                    }
                }
            );
            rules.Add("mdlRailGunner", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(-0.00023F, 0.12726F, -0.01826F),
                        localAngles = new Vector3(0F, 0F, 0F),
                        localScale = new Vector3(0.33867F, 0.33867F, 0.33867F)
                    }
                }
            );
            rules.Add("mdlVoidSurvivor", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(0.02281F, 0.10709F, -0.0658F),
                        localAngles = new Vector3(321.6797F, 339.2903F, 3.40509F),
                        localScale = new Vector3(0.35401F, 0.35401F, 0.35401F)
                    }
                }
            );

            return rules;
        }

        public static void AddTokens()
        {
            LanguageAPI.Add("H3_" + upperName + "_NAME", "Miner's Helmet");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Earning enough money reduces your skill cooldowns.");
            LanguageAPI.Add("H3_" + upperName + "_LORE", "Order: Mining Equipment" +
            "\nTracking Number: 90***********" +
            "\nEstimated Delivery: 7/10/2056" +
            "\nShipping Method:  Standard" +
            "\nShipping Address: Loading Bay 01, Martian Minerals Ltd." +
            "\nShipping Details:" +
            "\n\nHere's that replacement equipment you've been bugging us about. Don't blame me for some of them being knock-offs though, you told me to get 'em in a rush." +
            "\n\nAlso, if an inspector comes by, maybe hide the helmets... These classic-looking yellow ones aren't up to code anymore. Rock hazards are pretty rare anyway, right?");

            LanguageAPI.Add("ACHIEVEMENT_" + upperName + "_NAME", "We're Rich!");
            LanguageAPI.Add("ACHIEVEMENT_" + upperName + "_DESCRIPTION", "Carry enough gold to buy three legendary chests.");
            LanguageAPI.Add(upperName + "_UNLOCK_NAME", "We're Rich!");
        }
        public static void UpdateItemStatus(Run run)
        {
            if (!MinersHelmet_Enable.Value)
            {
                LanguageAPI.AddOverlay("H3_" + upperName + "_NAME", "Miner's Helmet" + " <style=cDeath>[DISABLED]</style>");
                if (run && run.availableItems.Contains(itemDef.itemIndex)) { run.availableItems.Remove(itemDef.itemIndex); }
            }
            else
            {
                LanguageAPI.AddOverlay("H3_" + upperName + "_NAME", "Miner's Helmet");
                if (run && !run.availableItems.Contains(itemDef.itemIndex) && run.IsExpansionEnabled(Hex3ModExpansion)) { run.availableItems.Add(itemDef.itemIndex); }
            }
            LanguageAPI.AddOverlay("H3_" + upperName + "_DESC", string.Format("<style=cShrine>Every time you earn ${1}</style> <style=cStack>(Scaling with time)</style>, reduce your <style=cIsUtility>skill cooldowns</style> by <style=cIsUtility>{0}</style> seconds <style=cStack>(+{0} per stack)</style>.", MinersHelmet_CooldownReduction.Value, MinersHelmet_GoldPerProc.Value));
        }

        private static void AddHooks()
        {
            miningFX.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            void OnGoldCollected (On.RoR2.Stats.StatManager.orig_OnGoldCollected orig, CharacterMaster master, ulong amount)
            {
                orig(master, amount);
                if (amount > 0 && master && master.GetBody())
                {
                    CharacterBody body = master.GetBody();
                    if (body.inventory && body.inventory.GetItemCount(itemDef) > 0)
                    {
                        if (body.GetComponent<MinersHelmetBehavior>() == null)
                        {
                            body.AddItemBehavior<MinersHelmetBehavior>(1);
                            body.GetComponent<MinersHelmetBehavior>().MinersHelmet_CooldownReduction = MinersHelmet_CooldownReduction.Value;
                            body.GetComponent<MinersHelmetBehavior>().MinersHelmet_GoldPerProc = MinersHelmet_GoldPerProc.Value;
                        }
                        MinersHelmetBehavior behavior = body.GetComponent<MinersHelmetBehavior>();
                        behavior.stack = body.inventory.GetItemCount(itemDef);
                        behavior.goldInventory += (int)amount;
                    }
                    if (body.inventory && body.inventory.GetItemCount(itemDef) < 1 && body.GetComponent<MinersHelmetBehavior>() != null)
                    {
                        UnityEngine.Object.Destroy(body.GetComponent<MinersHelmetBehavior>());
                    }
                }
            };

            On.RoR2.Stats.StatManager.OnGoldCollected += OnGoldCollected;
        }

        public class MinersHelmetBehavior : CharacterBody.ItemBehavior
        {
            public int goldInventory = 0;
            public float MinersHelmet_CooldownReduction = 0f;
            public int MinersHelmet_GoldPerProc = 0;

            void FixedUpdate()
            {
                if (goldInventory >= Run.instance.GetDifficultyScaledCost(MinersHelmet_GoldPerProc)) // If the player gets over small chest's value, proc as many times as necessary
                {
                    int goldLeft = goldInventory;
                    for (int i = 0; i <= (goldLeft - Run.instance.GetDifficultyScaledCost(MinersHelmet_GoldPerProc)); i += Run.instance.GetDifficultyScaledCost(MinersHelmet_GoldPerProc))
                    {
                        foreach (SkillSlot skillSlot in Enum.GetValues(typeof(SkillSlot)))
                        {
                            if (body.skillLocator.GetSkill(skillSlot))
                            {
                                GenericSkill skill = body.skillLocator.GetSkill(skillSlot); // Only recharge if necessary, to prevent janky behavior
                                if ((MinersHelmet_CooldownReduction * stack) <= skill.cooldownRemaining)
                                {
                                    skill.rechargeStopwatch += MinersHelmet_CooldownReduction * stack;
                                }
                                else
                                {
                                    skill.rechargeStopwatch += skill.cooldownRemaining;
                                }
                            }

                        }
                        goldInventory -= Run.instance.GetDifficultyScaledCost(MinersHelmet_GoldPerProc);
                    }
                    EffectData effectData = new EffectData
                    {
                        origin = body.corePosition
                    };
                    EffectManager.SpawnEffect(miningFX, effectData, false);
                    Util.PlaySound("Play_item_proc_moneyOnKill_loot", body.gameObject);
                }
            }
        }

        [RegisterAchievement("MinersHelmet", "MinersHelmetUnlock", null, typeof(MinersHelmetAchievement))]
        public class MinersHelmetAchievement : BaseAchievement
        {
            public override void OnInstall()
            {
                base.OnInstall();
                RoR2Application.onUpdate += GrantIfGold;
            }

            public override void OnUninstall()
            {
                RoR2Application.onUpdate -= GrantIfGold;
                base.OnUninstall();
            }

            private void GrantIfGold()
            {
                if (localUser != null && localUser.cachedBody)
                {
                    if (localUser.cachedBody.master.money >= Run.instance.GetDifficultyScaledCost(1200))
                    {
                        Grant();
                    }
                }
            }
        }

        public static void Initiate()
        {
            itemDef = CreateItem();
            ItemAPI.Add(new CustomItem(itemDef, CreateDisplayRules()));
            AddTokens();
            UpdateItemStatus(null);
            AddHooks();
        }
    }
}
