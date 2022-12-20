using R2API;
using RoR2;
using UnityEngine;
using Hex3Mod.HelperClasses;
using UnityEngine.AddressableAssets;
using Hex3Mod.Utils;
using static Hex3Mod.Main;

namespace Hex3Mod.Items
{
    /*
    Balance provides dodge chance the slower you are.
    This balances it out for regular combat, while making it very useful while interacting with menus, being frozen/slowed, or using turrets-- all times when you're most vulnerable.
    */
    public class Balance
    {
        static string itemName = "Balance";
        static string upperName = itemName.ToUpper();
        public static ItemDef itemDef;
        public static GameObject LoadPrefab()
        {
            GameObject pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/BalancePrefab.prefab");
            if (Main.debugMode == true)
            {
                pickupModelPrefab.GetComponentInChildren<Renderer>().gameObject.AddComponent<MaterialControllerComponents.HGControllerFinder>();
            }
            return pickupModelPrefab;
        }
        public static Sprite LoadSprite()
        {
            Sprite pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/Balance.png");
            return pickupIconSprite;
        }
        public static GameObject dodgeFX = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/SprintOutOfCombat/SprintActivate.prefab").WaitForCompletion();
        public static ItemDef CreateItem()
        {
            ItemDef item = ScriptableObject.CreateInstance<ItemDef>();

            item.name = itemName;
            item.nameToken = "H3_" + upperName + "_NAME";
            item.pickupToken = "H3_" + upperName + "_PICKUP";
            item.descriptionToken = "H3_" + upperName + "_DESC";
            item.loreToken = "H3_" + upperName + "_LORE";

            item.tags = new ItemTag[]{ ItemTag.Utility };
            item.deprecatedTier = ItemTier.Tier1;
            item.canRemove = true;
            item.hidden = false;
            item.requiredExpansion = Hex3ModExpansion;

            item.pickupModelPrefab = LoadPrefab();
            item.pickupIconSprite = LoadSprite();

            return item;
        }

        public static ItemDisplayRuleDict CreateDisplayRules()
        {
            GameObject ItemDisplayPrefab = helpers.PrepareItemDisplayModel(PrefabAPI.InstantiateClone(LoadPrefab(), LoadPrefab().name + "Display", false));

            ItemDisplayRuleDict rules = new ItemDisplayRuleDict();
            rules.Add("mdlCommandoDualies", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.15618F, -0.05006F, -0.08006F),
                        localAngles = new Vector3(0.72997F, 145.0286F, 176.085F),
                        localScale = new Vector3(0.30233F, 0.30233F, 0.30233F)
                    }
                }
            );
            rules.Add("mdlHuntress", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.17274F, 0.02019F, -0.03914F),
                        localAngles = new Vector3(27.96266F, 95.99141F, 183.5809F),
                        localScale = new Vector3(0.29202F, 0.29202F, 0.29202F)
                    }
                }
            );
            rules.Add("mdlToolbot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(-1.83193F, 2.64435F, -0.55288F),
                        localAngles = new Vector3(86.11033F, 6.26054F, 7.51271F),
                        localScale = new Vector3(1.89165F, 1.89165F, 1.89165F)
                    }
                }
            );
            rules.Add("mdlEngi", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "HandR",
                        localPos = new Vector3(0.01259F, 0.15088F, -0.02704F),
                        localAngles = new Vector3(33.13659F, 160.7793F, 74.44673F),
                        localScale = new Vector3(0.32359F, 0.32359F, 0.32359F)
                    }
                }
            );
            rules.Add("mdlMage", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Stomach",
                        localPos = new Vector3(0.16528F, 0.08335F, -0.00528F),
                        localAngles = new Vector3(350.7327F, 82.03748F, 11.39287F),
                        localScale = new Vector3(0.18765F, 0.18765F, 0.18765F)
                    }
                }
            );
            rules.Add("mdlMerc", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "UpperArmR",
                        localPos = new Vector3(-0.08865F, 0.00029F, 0.03077F),
                        localAngles = new Vector3(348.0789F, 137.4101F, 190.6102F),
                        localScale = new Vector3(0.29547F, 0.29547F, 0.29547F)
                    }
                }
            );
            rules.Add("mdlTreebot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "PlatformBase",
                        localPos = new Vector3(0.14592F, 0.3429F, 0.86894F),
                        localAngles = new Vector3(335.6998F, 62.86448F, 349.067F),
                        localScale = new Vector3(0.55901F, 0.55901F, 0.55901F)
                    }
                }
            );
            rules.Add("mdlLoader", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "HandR",
                        localPos = new Vector3(-0.00795F, 0.16177F, -0.07468F),
                        localAngles = new Vector3(20.55022F, 336.0829F, 279.2504F),
                        localScale = new Vector3(0.27906F, 0.27906F, 0.27906F)
                    }
                }
            );
            rules.Add("mdlCroco", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "MouthMuzzle",
                        localPos = new Vector3(-0.12153F, 0.05408F, 4.01906F),
                        localAngles = new Vector3(6.67158F, 3.73886F, 180.2775F),
                        localScale = new Vector3(2.36956F, 2.36956F, 2.37884F)
                    }
                }
            );
            rules.Add("mdlCaptain", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(-0.21713F, 0.18106F, -0.10525F),
                        localAngles = new Vector3(333.1481F, 333.2755F, 54.34922F),
                        localScale = new Vector3(0.17073F, 0.17073F, 0.17073F)
                    }
                }
            );
            rules.Add("mdlBandit2", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "MainWeapon",
                        localPos = new Vector3(-0.15246F, 0.4615F, -0.04341F),
                        localAngles = new Vector3(330.1767F, 330.6053F, 90.42426F),
                        localScale = new Vector3(0.19825F, 0.19825F, 0.19825F)
                    }
                }
            );
            rules.Add("EngiTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(-0.0159F, 1.11726F, -1.10342F),
                        localAngles = new Vector3(0F, 0F, 0F),
                        localScale = new Vector3(0.71119F, 0.71119F, 0.71119F)
                    }
                }
            );
            rules.Add("EngiWalkerTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "FootL",
                        localPos = new Vector3(0.03097F, -0.14586F, 0.41797F),
                        localAngles = new Vector3(54.6078F, 302.7847F, 301.0834F),
                        localScale = new Vector3(1.05772F, 1.05772F, 1.05772F)
                    }
                }
            );
            rules.Add("mdlScav", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(0.34979F, 7.9363F, 2.33525F),
                        localAngles = new Vector3(28.96841F, -0.00001F, -0.00002F),
                        localScale = new Vector3(10.26943F, 10.26943F, 10.26943F)
                    }
                }
            );
            rules.Add("mdlRailGunner", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Backpack",
                        localPos = new Vector3(-0.26451F, -0.47452F, -0.00543F),
                        localAngles = new Vector3(280.1236F, 349.913F, 276.8356F),
                        localScale = new Vector3(0.26336F, 0.26336F, 0.26336F)
                    }
                }
            );
            rules.Add("mdlVoidSurvivor", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(-0.0651F, 0.13626F, -0.17599F),
                        localAngles = new Vector3(305.8163F, 50.37189F, 333.2689F),
                        localScale = new Vector3(0.32316F, 0.32316F, 0.32316F)
                    }
                }
            );

            return rules;
        }

        public static void AddTokens()
        {
            LanguageAPI.Add("H3_" + upperName + "_NAME", "Balance");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Dodge more attacks the slower you're moving.");
            LanguageAPI.Add("H3_" + upperName + "_LORE", "\"I think one of them stung me.\"" +
            "\n\n\"The beetles? Really?\"" +
            "\n\n\"What?\"" +
            "\n\n\"Just move faster!\"" +
            "\n\n\"I'm not trying to blow out my hip, you know? I understand that we've got places to be, but can't we just slow down a little?\"" +
            "\n\n\"That's not really an option...\"" +
            "\n\n\"Because 'something's chasing us', right? I still think you're seeing things. The life forms on this planet are all about as intelligent as they look, so you can quit being paranoid about it at your own pace.\"" +
            "\n\n\"You've trusted my judgement until now, Captain.\"" +
            "\n\n\"Yeah, yeah... We should get some rest.\"" +
            "\n\nThe two survivors- only one of them having set aside their anxiety- had made a camp for the night in a small cave. Hitomi rarely let fear get to her, so it was uncharacteristic of her to be nervous on such a routine mission-- and yet, no reasoning could calm the twitching in her muscles or the chills running around her skin. She'd have chalked it up to a fever if her instincts weren't telling her otherwise." +
            "\n\n\"You too. If we're crossing the forest tomorrow, both of us will need the sleep. Alright?\"" +
            "\n\n\"Mhm...\"" +
            "\n\nHitomi took a few deep breaths-- a pattern for which she'd practiced the timing since childhood. Whenever the nerves got to her she'd begin to control her breathing, as in her line of work, nerves were not an option. And she really did need some rest." +
            "\n\nFor a moment, she felt close to drifting off, as her thoughts finally wandered away from the threat of danger. That was until a hand wrapped around the entrance of the cave. Like a skeleton covered in flesh, wrapped with pulsating muscle and vein, and adorned with crimson-spattered spikes." +
            "\n\n\"Captain-\"");
        }
        public static void UpdateItemStatus(Run run)
        {
            if (!Balance_Enable.Value)
            {
                LanguageAPI.AddOverlay("H3_" + upperName + "_NAME", "Balance" + " <style=cDeath>[DISABLED]</style>");
                if (run && run.availableItems.Contains(itemDef.itemIndex)) { run.availableItems.Remove(itemDef.itemIndex); }
            }
            else
            {
                LanguageAPI.AddOverlay("H3_" + upperName + "_NAME", "Balance");
                if (run && !run.availableItems.Contains(itemDef.itemIndex) && run.IsExpansionEnabled(Hex3ModExpansion)) { run.availableItems.Add(itemDef.itemIndex); }
            }
            LanguageAPI.AddOverlay("H3_" + upperName + "_DESC", string.Format("Gain a maximum <style=cWorldEvent>{0}% chance to dodge attacks</style> <style=cStack>(+{0}% per stack, hyperbolic)</style> <style=cWorldEvent>the slower you're moving:</style> <style=cIsUtility>Full chance</style> while not moving, <style=cIsUtility>half chance</style> while walking or receiving a speed debuff, and <style=cIsUtility>no chance</style> while freely sprinting. Unaffected by luck.", Balance_MaxDodge.Value));
        }

        private static void AddHooks()
        {
            void TakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo damageInfo)
            {
                if (self.body && damageInfo.damage > 0 && !damageInfo.rejected && self.body.inventory && self.body.inventory.GetItemCount(itemDef) > 0)
                {
                    CharacterBody body = self.body;
                    int itemCount = body.inventory.GetItemCount(itemDef);
                    bool chanceAlreadySet = false;
                    float chance = 0f;
                    int slowDebuffCount = body.GetBuffCount(BuffCatalog.FindBuffIndex("bdBeetleJuice")) +
                        body.GetBuffCount(BuffCatalog.FindBuffIndex("bdCripple")) +
                        body.GetBuffCount(BuffCatalog.FindBuffIndex("bdSlow50")) +
                        body.GetBuffCount(BuffCatalog.FindBuffIndex("bdSlow60")) +
                        body.GetBuffCount(BuffCatalog.FindBuffIndex("bdSlow80")) +
                        body.GetBuffCount(BuffCatalog.FindBuffIndex("bdWeak")) +
                        body.GetBuffCount(BuffCatalog.FindBuffIndex("bdSlow30")) +
                        body.GetBuffCount(BuffCatalog.FindBuffIndex("bdClayGoo")) +
                        body.GetBuffCount(BuffCatalog.FindBuffIndex("bdArmorBoost ")) + // Power mode
                        body.GetBuffCount(BuffCatalog.FindBuffIndex("bdJailerSlow"));
                    int stopDebuffCount = body.GetBuffCount(BuffCatalog.FindBuffIndex("bdEntangle")) +
                        body.GetBuffCount(BuffCatalog.FindBuffIndex("bdLunarSecondaryRoot")) +
                        body.GetBuffCount(BuffCatalog.FindBuffIndex("bdNullified")) +
                        body.GetBuffCount(BuffCatalog.FindBuffIndex("bdJailerTether"));

                    if (body.notMovingStopwatch > 0f || stopDebuffCount > 0 || self.isInFrozenState) // Not moving at all (100%)
                    {
                        chance = 1f;
                        chanceAlreadySet = true;
                    }
                    if (!chanceAlreadySet && body.isSprinting && slowDebuffCount > 0) // Sprinting with debuffs (50%)
                    {
                        chance = 0.5f;
                        chanceAlreadySet = true;
                    }
                    if (!chanceAlreadySet && body.isSprinting && slowDebuffCount <= 0) // Sprinting without debuffs (0%)
                    {
                        chance = 0f;
                        chanceAlreadySet = true;
                    }
                    if (!chanceAlreadySet && slowDebuffCount > 0) // Walking while debuffed (100%)
                    {
                        chance = 1f;
                        chanceAlreadySet = true;
                    }
                    if (!chanceAlreadySet) // Default (50%)
                    {
                        chance = 0.5f;
                        chanceAlreadySet = true;
                    }

                    if (Util.CheckRoll(Util.ConvertAmplificationPercentageIntoReductionPercentage((Balance_MaxDodge.Value * chance) * (float)itemCount), 0f, null))
                    {
                        EffectData effectData = new EffectData
                        {
                            origin = damageInfo.position,
                            rotation = Util.QuaternionSafeLookRotation((damageInfo.force != Vector3.zero) ? damageInfo.force : UnityEngine.Random.onUnitSphere)
                        };
                        EffectManager.SpawnEffect(dodgeFX, effectData, true);
                        damageInfo.rejected = true;
                    }
                }
                orig(self, damageInfo);
            }

            On.RoR2.HealthComponent.TakeDamage += TakeDamage;
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
