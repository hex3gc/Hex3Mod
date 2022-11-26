using R2API;
using RoR2;
using UnityEngine;
using Hex3Mod.HelperClasses;
using System;
using UnityEngine.PlayerLoop;
using RoR2.Achievements;
using UnityEngine.AddressableAssets;
using RoR2.Items;

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
        static ItemDef itemDefinition = CreateItem();
        public static GameObject LoadPrefab()
        {
            GameObject pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/MinersHelmetPrefab.prefab");
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
                        childName = "Chest",
                        localPos = new Vector3(0.21708F, 0.49989F, -0.0198F),
                        localAngles = new Vector3(300.1077F, 191.7878F, 31.30767F),
                        localScale = new Vector3(0.10416F, 0.10416F, 0.10416F)
                    }
                }
            );
            rules.Add("mdlHuntress", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "UpperArmR",
                        localPos = new Vector3(-0.04204F, 0.3909F, -0.08947F),
                        localAngles = new Vector3(305.158F, 128.5434F, 241.4366F),
                        localScale = new Vector3(0.0859F, 0.0859F, 0.0859F)
                    }
                }
            );
            rules.Add("mdlToolbot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(-1.98627F, 2.50041F, -0.81139F),
                        localAngles = new Vector3(6.48878F, 50.8491F, 42.82001F),
                        localScale = new Vector3(0.78954F, 0.78954F, 0.78954F)
                    }
                }
            );
            rules.Add("mdlEngi", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "HandL",
                        localPos = new Vector3(-0.06379F, 0.15714F, -0.02627F),
                        localAngles = new Vector3(70.9774F, 52.81272F, 261.3786F),
                        localScale = new Vector3(0.14769F, 0.14769F, 0.14769F)
                    }
                }
            );
            rules.Add("mdlMage", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Stomach",
                        localPos = new Vector3(-0.12536F, 0.14136F, 0.10847F),
                        localAngles = new Vector3(310.494F, 40.69888F, 318.9477F),
                        localScale = new Vector3(0.09897F, 0.09897F, 0.09897F)
                    }
                }
            );
            rules.Add("mdlMerc", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "FootR",
                        localPos = new Vector3(0.00465F, 0.10424F, -0.15497F),
                        localAngles = new Vector3(348.0789F, 137.4101F, 190.6102F),
                        localScale = new Vector3(0.088F, 0.088F, 0.088F)
                    }
                }
            );
            rules.Add("mdlTreebot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "PlatformBase",
                        localPos = new Vector3(-0.40513F, 0.74024F, 0.42998F),
                        localAngles = new Vector3(9.43343F, 55.30598F, 41.14965F),
                        localScale = new Vector3(0.20768F, 0.20768F, 0.20768F)
                    }
                }
            );
            rules.Add("mdlLoader", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(-0.00024F, 0.12501F, 0.25337F),
                        localAngles = new Vector3(25.9752F, 47.40374F, 25.39513F),
                        localScale = new Vector3(0.11278F, 0.11278F, 0.11278F)
                    }
                }
            );
            rules.Add("mdlCroco", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "SpineChest1",
                        localPos = new Vector3(1.40569F, 1.67903F, 1.83F),
                        localAngles = new Vector3(335.2653F, 21.50072F, 285.631F),
                        localScale = new Vector3(0.8967F, 0.8967F, 0.90021F)
                    }
                }
            );
            rules.Add("mdlCaptain", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "CalfL",
                        localPos = new Vector3(-0.21595F, 0.37524F, 0.0112F),
                        localAngles = new Vector3(327.5428F, 347.9001F, 28.43982F),
                        localScale = new Vector3(0.17073F, 0.17073F, 0.17073F)
                    }
                }
            );
            rules.Add("mdlBandit2", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(-0.2345F, -0.1407F, 0.05543F),
                        localAngles = new Vector3(53.97828F, 36.91263F, 81.21328F),
                        localScale = new Vector3(0.10738F, 0.10738F, 0.10738F)
                    }
                }
            );
            rules.Add("EngiTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Base",
                        localPos = new Vector3(0.29738F, 1.32457F, 0.25339F),
                        localAngles = new Vector3(2.91952F, 16.66828F, 290.7995F),
                        localScale = new Vector3(0.36967F, 0.36967F, 0.36967F)
                    }
                }
            );
            rules.Add("EngiWalkerTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Base",
                        localPos = new Vector3(-0.17481F, 1.25268F, 0.36514F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.36967F, 0.36967F, 0.36967F)
                    }
                }
            );
            rules.Add("mdlScav", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "HandL",
                        localPos = new Vector3(0.34979F, 2.53159F, -0.53631F),
                        localAngles = new Vector3(68.08115F, 20.07728F, 313.8858F),
                        localScale = new Vector3(1.69716F, 1.69716F, 1.69716F)
                    }
                }
            );
            rules.Add("mdlRailGunner", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "CalfL",
                        localPos = new Vector3(0.04085F, 0.3535F, -0.03918F),
                        localAngles = new Vector3(339.1311F, 105.2301F, 215.211F),
                        localScale = new Vector3(0.17307F, 0.17307F, 0.17307F)
                    }
                }
            );
            rules.Add("mdlVoidSurvivor", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(0.17212F, 0.19905F, 0.12029F),
                        localAngles = new Vector3(353.8923F, 134.0595F, 356.0528F),
                        localScale = new Vector3(0.10439F, 0.10439F, 0.10439F)
                    }
                }
            );

            return rules;
        }

        public static void AddTokens(float MinersHelmet_CooldownReduction, int MinersHelmet_GoldPerProc)
        {
            LanguageAPI.Add("H3_" + upperName + "_NAME", "Miner's Helmet");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Earning enough money reduces your skill cooldowns.");
            LanguageAPI.Add("H3_" + upperName + "_DESC", string.Format("<style=cShrine>Every time you earn ${1}</style> <style=cStack>(Scaling with time)</style>, reduce your <style=cIsUtility>skill cooldowns</style> by <style=cIsUtility>{0}</style> seconds <style=cStack>(+{0} per stack)</style>.", MinersHelmet_CooldownReduction, MinersHelmet_GoldPerProc));
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

        private static void AddHooks(ItemDef itemDef, float MinersHelmet_CooldownReduction, int MinersHelmet_GoldPerProc)
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
                            body.GetComponent<MinersHelmetBehavior>().MinersHelmet_CooldownReduction = MinersHelmet_CooldownReduction;
                            body.GetComponent<MinersHelmetBehavior>().MinersHelmet_GoldPerProc = MinersHelmet_GoldPerProc;
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

        public static void Initiate(float MinersHelmet_CooldownReduction, int MinersHelmet_GoldPerProc)
        {
            ItemAPI.Add(new CustomItem(itemDefinition, CreateDisplayRules()));
            AddTokens(MinersHelmet_CooldownReduction, MinersHelmet_GoldPerProc);
            AddHooks(itemDefinition, MinersHelmet_CooldownReduction, MinersHelmet_GoldPerProc);
        }
    }
}
