using R2API;
using RoR2;
using UnityEngine;
using Hex3Mod.HelperClasses;
using Hex3Mod.Utils;

namespace Hex3Mod.Items
{
    /*
    Bucket List increases your movement drastically speed outside of the teleporter/boss event, but it also gives a small boost while in teleporter/boss events to ease the jarring change in speed
    This item is meant to be a great early-game boost for those who want to loot the stage in good time, but as a tradeoff not provide a big power boost for the important fights
    */
    public class BucketList
    {
        static string itemName = "BucketList";
        static string upperName = itemName.ToUpper();
        static ItemDef itemDefinition = CreateItem();
        public static GameObject LoadPrefab()
        {
            GameObject pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/BucketListPrefab.prefab");
            if (Main.debugMode == true)
            {
                pickupModelPrefab.GetComponentInChildren<Renderer>().gameObject.AddComponent<MaterialControllerComponents.HGControllerFinder>();
            }
            return pickupModelPrefab;
        }
        public static Sprite LoadSprite()
        {
            Sprite pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/BucketList.png");
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

            item.tags = new ItemTag[]{ ItemTag.Utility };
            item.deprecatedTier = ItemTier.Tier1; // Deprecated, consider changing soon
            item.canRemove = true;
            item.hidden = false;

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
                        childName = "Chest",
                        localPos = new Vector3(-0.10073F, 0.38218F, -0.18139F),
                        localAngles = new Vector3(9.36944F, 278.8746F, 310.1887F),
                        localScale = new Vector3(0.08095F, 0.08095F, 0.08095F)
                    }
                }
            );
            rules.Add("mdlHuntress", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "UpperArmR",
                        localPos = new Vector3(0.11547F, 0.20849F, -0.0178F),
                        localAngles = new Vector3(46.56613F, 57.35534F, 182.9626F),
                        localScale = new Vector3(0.07288F, 0.07288F, 0.07288F)
                    }
                }
            );
            rules.Add("mdlToolbot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(0.57561F, 1.24094F, -2.02817F),
                        localAngles = new Vector3(0.99974F, 269.7086F, 279.4616F),
                        localScale = new Vector3(0.58278F, 0.58278F, 0.58278F)
                    }
                }
            );
            rules.Add("mdlEngi", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(0.00408F, -0.10955F, -0.29658F),
                        localAngles = new Vector3(0.39293F, 272.1572F, 331.0313F),
                        localScale = new Vector3(0.08894F, 0.08894F, 0.08894F)
                    }
                }
            );
            rules.Add("mdlMage", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "ThighR",
                        localPos = new Vector3(-0.09991F, 0.25726F, 0.1397F),
                        localAngles = new Vector3(2.81892F, 220.6833F, 169.9032F),
                        localScale = new Vector3(0.05989F, 0.05989F, 0.05989F)
                    }
                }
            );
            rules.Add("mdlMerc", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "CalfR",
                        localPos = new Vector3(0.02725F, 0.15356F, 0.10612F),
                        localAngles = new Vector3(353.0927F, 300.1326F, 185.1725F),
                        localScale = new Vector3(0.08555F, 0.08555F, 0.08555F)
                    }
                }
            );
            rules.Add("mdlTreebot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "PlatformBase",
                        localPos = new Vector3(1.02708F, 0.55997F, -0.72409F),
                        localAngles = new Vector3(0.89484F, 209.4194F, 312.5518F),
                        localScale = new Vector3(0.1491F, 0.1491F, 0.1491F)
                    }
                }
            );
            rules.Add("mdlLoader", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "UpperArmR",
                        localPos = new Vector3(-0.0333F, 0.28791F, -0.06443F),
                        localAngles = new Vector3(3.56069F, 113.5405F, 183.7697F),
                        localScale = new Vector3(0.06259F, 0.06259F, 0.06259F)
                    }
                }
            );
            rules.Add("mdlCroco", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "ThighR",
                        localPos = new Vector3(-1.33423F, 0.58262F, -0.48353F),
                        localAngles = new Vector3(48.1975F, 167.5462F, 165.5803F),
                        localScale = new Vector3(0.70165F, 0.70165F, 0.7044F)
                    }
                }
            );
            rules.Add("mdlCaptain", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "LowerArmR",
                        localPos = new Vector3(0.05101F, 0.19129F, -0.08542F),
                        localAngles = new Vector3(46.21507F, 73.36964F, 172.1877F),
                        localScale = new Vector3(0.07115F, 0.07115F, 0.07115F)
                    }
                }
            );
            rules.Add("mdlBandit2", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Hat",
                        localPos = new Vector3(-0.12198F, -0.18451F, -0.19129F),
                        localAngles = new Vector3(341.7549F, 302.1947F, 0.85298F),
                        localScale = new Vector3(0.05254F, 0.05254F, 0.05254F)
                    }
                }
            );
            rules.Add("EngiTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Neck",
                        localPos = new Vector3(0F, 0.53944F, -0.35561F),
                        localAngles = new Vector3(359.1785F, 269.1755F, 315.1067F),
                        localScale = new Vector3(0.32385F, 0.32385F, 0.32385F)
                    }
                }
            );
            rules.Add("EngiWalkerTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Neck",
                        localPos = new Vector3(0F, 0.52947F, -0.54235F),
                        localAngles = new Vector3(359.6508F, 270.7413F, 301.8058F),
                        localScale = new Vector3(0.45203F, 0.45203F, 0.45203F)
                    }
                }
            );
            rules.Add("mdlScav", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(6.88809F, 2.92391F, 1.48334F),
                        localAngles = new Vector3(329.7213F, 186.0749F, 308.0473F),
                        localScale = new Vector3(0.95553F, 0.95553F, 0.95553F)
                    }
                }
            );
            rules.Add("mdlRailGunner", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "ThighL",
                        localPos = new Vector3(0.11912F, 0.19155F, 0.01379F),
                        localAngles = new Vector3(335.6619F, 355.9344F, 189.3831F),
                        localScale = new Vector3(0.06674F, 0.06488F, 0.06488F)
                    }
                }
            );
            rules.Add("mdlVoidSurvivor", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "ForeArmL",
                        localPos = new Vector3(0.03632F, 0.2663F, -0.06864F),
                        localAngles = new Vector3(279.6642F, 275.8685F, 281.2188F),
                        localScale = new Vector3(0.06786F, 0.06786F, 0.06786F)
                    }
                }
            );

            return rules;
        }

        public static void AddTokens(float BucketList_FullBuff, float BucketList_BuffReduce)
        {
            float BucketList_FullBuff_Readable = BucketList_FullBuff * 100f;
            float BucketList_BuffReduce_Readable = BucketList_BuffReduce * 100f;

            LanguageAPI.Add("H3_" + upperName + "_NAME", "Bucket List");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Move faster before teleporter events and boss fights.");
            LanguageAPI.Add("H3_" + upperName + "_DESC", "Move <style=cIsUtility>" + BucketList_FullBuff_Readable + "%</style> faster <style=cStack>(+" + BucketList_FullBuff_Readable + "% per stack)</style>. Reduce this bonus by <style=cDeath>" + BucketList_BuffReduce_Readable + "%</style> during boss fights.");
            LanguageAPI.Add("H3_" + upperName + "_LORE", "- go to Saturn and see the night lights\n\n- visit grandma\n\n- see Bovine Joni in concert\n\n- try Mercurian Salts (get Jaden to make sure im ok after)\n\n- pet a gip");
        }

        private static void AddHooks(ItemDef itemDef, float BucketList_FullBuff, float BucketList_BuffReduce)
        {
            float ReducedBuff = (BucketList_FullBuff - (BucketList_FullBuff * BucketList_BuffReduce));

            void CheckForBosses(CharacterBody body, RecalculateStatsAPI.StatHookEventArgs args)
            {
                if (body.inventory && body.inventory.GetItemCount(itemDef) > 0)
                {
                    var monsters = TeamComponent.GetTeamMembers(TeamIndex.Monster);
                    if (monsters != null)
                    {
                        bool bossFound = false;
                        foreach (var monster in monsters)
                        {
                            if (monster.body && monster.body.isBoss)
                            {
                                bossFound = true;
                                break;
                            }
                        }
                        if (bossFound == true) // Boss present: Reduced buff
                        {
                            args.moveSpeedMultAdd += (ReducedBuff + ((body.inventory.GetItemCount(itemDef) - 1) * ReducedBuff));
                        }
                        else // Boss not present: Full buff
                        {
                            args.moveSpeedMultAdd += (BucketList_FullBuff + ((body.inventory.GetItemCount(itemDef) - 1) * BucketList_FullBuff));
                        }
                    }
                }
            }

            RecalculateStatsAPI.GetStatCoefficients += CheckForBosses;
        }

        public static void Initiate(float BucketList_FullBuff, float BucketList_BuffReduce)
        {
            ItemAPI.Add(new CustomItem(itemDefinition, CreateDisplayRules()));
            AddTokens(BucketList_FullBuff, BucketList_BuffReduce);
            AddHooks(itemDefinition, BucketList_FullBuff, BucketList_BuffReduce);
        }
    }
}
