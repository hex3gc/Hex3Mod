using R2API;
using RoR2;
using UnityEngine;
using Hex3Mod.HelperClasses;
using Hex3Mod.Utils;
using static Hex3Mod.Main;

namespace Hex3Mod.Items
{
    /*
    Hopoo Egg rework: Increases jump power significantly, and decreases fall damage compensate
    The air control modifier was janky anyway
    */
    public static class HopooEgg
    {
        static string itemName = "HopooEgg";
        static string upperName = itemName.ToUpper();
        public static ItemDef itemDef;
        public static GameObject LoadPrefab()
        {
            GameObject pickupModelPrefab = MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/HopooEggPrefab.prefab");
            if (debugMode)
            {
                pickupModelPrefab.GetComponentInChildren<Renderer>().gameObject.AddComponent<MaterialControllerComponents.HGControllerFinder>();
            }
            return pickupModelPrefab;
        }
        public static Sprite LoadSprite()
        {
            return MainAssets.LoadAsset<Sprite>("Assets/Icons/HopooEgg.png");
        }
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
                        childName = "Head",
                        localPos = new Vector3(-0.00105F, 0.43577F, -0.04965F),
                        localAngles = new Vector3(21.67887F, 180.6768F, 3.72264F),
                        localScale = new Vector3(0.06769F, 0.06769F, 0.06769F)
                    }
                }
            );
            rules.Add("mdlHuntress", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "HandR",
                        localPos = new Vector3(-0.00568F, 0.09545F, -0.08172F),
                        localAngles = new Vector3(28.9446F, 254.4845F, 189.9268F),
                        localScale = new Vector3(0.06683F, 0.06683F, 0.06683F)
                    }
                }
            );
            rules.Add("mdlToolbot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "HandR",
                        localPos = new Vector3(0.18165F, 0.77192F, -0.0244F),
                        localAngles = new Vector3(0.72025F, 12.41585F, 355.8723F),
                        localScale = new Vector3(0.58278F, 0.58278F, 0.58278F)
                    }
                }
            );
            rules.Add("mdlEngi", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "HeadCenter",
                        localPos = new Vector3(-0.00002F, -0.00158F, 0.00361F),
                        localAngles = new Vector3(0F, 0F, 0F),
                        localScale = new Vector3(0.1954F, 0.1954F, 0.1954F)
                    }
                }
            );
            rules.Add("mdlMage", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(-0.00036F, 0.06969F, 0.00063F),
                        localAngles = new Vector3(65.80627F, 179.2708F, 178.897F),
                        localScale = new Vector3(0.12659F, 0.12659F, 0.12659F)
                    }
                }
            );
            rules.Add("mdlMerc", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "UpperArmR",
                        localPos = new Vector3(-0.07783F, -0.08495F, -0.05357F),
                        localAngles = new Vector3(357.5486F, 114.5002F, 192.0381F),
                        localScale = new Vector3(0.08555F, 0.08555F, 0.08555F)
                    }
                }
            );
            rules.Add("mdlTreebot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Eye",
                        localPos = new Vector3(0.00002F, 0.77768F, -0.00306F),
                        localAngles = new Vector3(358.7064F, 89.96999F, 1.38596F),
                        localScale = new Vector3(0.1491F, 0.1491F, 0.1491F)
                    }
                }
            );
            rules.Add("mdlLoader", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "MechUpperArmR",
                        localPos = new Vector3(0.0015F, -0.0873F, -0.04348F),
                        localAngles = new Vector3(357.426F, 261.74F, 143.9231F),
                        localScale = new Vector3(0.0846F, 0.0846F, 0.0846F)
                    }
                }
            );
            rules.Add("mdlCroco", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "MouthMuzzle",
                        localPos = new Vector3(0.00139F, 0.26948F, 4.37557F),
                        localAngles = new Vector3(359.6767F, 179.2953F, 180.7232F),
                        localScale = new Vector3(0.70165F, 0.70165F, 0.7044F)
                    }
                }
            );
            rules.Add("mdlCaptain", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Stomach",
                        localPos = new Vector3(-0.00018F, 0.10047F, 0.16349F),
                        localAngles = new Vector3(0F, 0F, 0F),
                        localScale = new Vector3(0.07115F, 0.07115F, 0.07115F)
                    }
                }
            );
            rules.Add("mdlBandit2", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(-0.00066F, 0.05823F, 0.13437F),
                        localAngles = new Vector3(80.90355F, 100.6095F, 95.12868F),
                        localScale = new Vector3(0.02686F, 0.02686F, 0.02686F)
                    }
                }
            );
            rules.Add("EngiTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(0F, 0.53947F, 0F),
                        localAngles = new Vector3(0F, 0F, 0F),
                        localScale = new Vector3(0.32385F, 0.32385F, 0.32385F)
                    }
                }
            );
            rules.Add("EngiWalkerTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "HeadCenter",
                        localPos = new Vector3(0F, 0.15551F, 0.181F),
                        localAngles = new Vector3(90F, 1F, 0F),
                        localScale = new Vector3(0.45203F, 0.45203F, 0.45203F)
                    }
                }
            );
            rules.Add("mdlScav", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Backpack",
                        localPos = new Vector3(-3.96765F, 13.22962F, -0.34322F),
                        localAngles = new Vector3(359.9811F, 0.27615F, 10.74268F),
                        localScale = new Vector3(0.86256F, 0.86256F, 0.86256F)
                    }
                }
            );
            rules.Add("mdlRailGunner", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(-0.00072F, 0.25889F, -0.06334F),
                        localAngles = new Vector3(344.1768F, 359.9716F, 0.15762F),
                        localScale = new Vector3(0.05404F, 0.05404F, 0.05404F)
                    }
                }
            );
            rules.Add("mdlVoidSurvivor", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(-0.12838F, 0.30064F, -0.30613F),
                        localAngles = new Vector3(41.41888F, 126.1885F, 330.7735F),
                        localScale = new Vector3(0.04376F, 0.04376F, 0.04376F)
                    }
                }
            );

            return rules;
        }

        public static void AddTokens()
        {
            LanguageAPI.Add("H3_" + upperName + "_NAME", "Hopoo Egg");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Jump higher and take less fall damage.");
            LanguageAPI.Add("H3_" + upperName + "_LORE", "\"...The hopoo's chicks are independent, being the only Europan nesting birds that hunt for themselves from birth. One is able to leave its nest as a newborn thanks to the low gravity environment, and it is born nimble enough to navigate down any rough terrain that lies between it and its food. This creates a unique problem for many newborn hopoos, however, as they often find themselves unable to go back up. The hopoo, out of reach of its nest, usually becomes nomadic and lives on its lonesome, having forgotten about its home by the time it has grown into an adult. This is where they get their common nickname, the 'hobo bird'.\"\n\n- Europan Wildlife Guide");
        }
        public static void UpdateItemStatus(Run run)
        {
            if (!HopooEgg_Enable.Value)
            {
                LanguageAPI.AddOverlay("H3_" + upperName + "_NAME", "Hopoo Egg" + " <style=cDeath>[DISABLED]</style>");
                if (run && run.availableItems.Contains(itemDef.itemIndex)) { run.availableItems.Remove(itemDef.itemIndex); }
            }
            else
            {
                LanguageAPI.AddOverlay("H3_" + upperName + "_NAME", "Hopoo Egg");
                if (run && !run.availableItems.Contains(itemDef.itemIndex) && run.IsExpansionEnabled(Hex3ModExpansion)) { run.availableItems.Add(itemDef.itemIndex); }
            }
            LanguageAPI.AddOverlay("H3_" + upperName + "_DESC", "Jump <style=cIsUtility>" + HopooEgg_JumpModifier.Value * 100f + "%</style> higher <style=cStack>(+" + HopooEgg_JumpModifier.Value * 100f + "% per stack)</style>. <style=cIsUtility>Take reduced fall damage.</style>");
        }

        private static void AddHooks()
        {
            void GetStatCoefficients(CharacterBody body, RecalculateStatsAPI.StatHookEventArgs args)
            {
                if (body.inventory && body.inventory.GetItemCount(itemDef) > 0)
                {
                    args.jumpPowerMultAdd += HopooEgg_JumpModifier.Value * body.inventory.GetItemCount(itemDef);
                }
            }

            RecalculateStatsAPI.GetStatCoefficients += GetStatCoefficients;
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
