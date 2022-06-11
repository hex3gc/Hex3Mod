using R2API;
using RoR2;
using RoR2.ExpansionManagement;
using System.Linq;
using UnityEngine;
using Hex3Mod.HelperClasses;
using VoidItemAPI;

namespace Hex3Mod.Items
{
    /*
    If the Corrupting Parasite exists, your items may all be turned into void items by the time you get enough movement to counteract this.
    The Notice Of Absence should provide a way to gain movement speed with a fully void build, with the downside of being quite bad without void items.
    */
    public class NoticeOfAbsence
    {
        // Create functions here for defining the ITEM, TOKENS, HOOKS and CONFIG. This is simpler than doing it in Main
        static string itemName = "NoticeOfAbsence"; // Change this name when making a new item
        static string upperName = itemName.ToUpper();
        public static ItemDef itemDefinition = CreateItem();
        public static GameObject LoadPrefab()
        {
            GameObject pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/NoticeOfAbsencePrefab.prefab");
            return pickupModelPrefab;
        }
        public static Sprite LoadSprite()
        {
            Sprite pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/NoticeOfAbsence.png");
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
            item.deprecatedTier = ItemTier.VoidTier1;
            item.canRemove = true;
            item.hidden = false;
            item.requiredExpansion = ExpansionCatalog.expansionDefs.FirstOrDefault(x => x.nameToken == "DLC1_NAME");

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
                        localPos = new Vector3(0.17951F, 0.21885F, 0.02222F),
                        localAngles = new Vector3(1.86163F, 179.3628F, 353.7818F),
                        localScale = new Vector3(0.32288F, 0.32288F, 0.32288F)
                    }
                }
            );
            rules.Add("mdlHuntress", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "HandR",
                        localPos = new Vector3(-0.04362F, 0.08892F, 0.01537F),
                        localAngles = new Vector3(28.9446F, 254.4845F, 189.9268F),
                        localScale = new Vector3(0.27425F, 0.27425F, 0.27425F)
                    }
                }
            );
            rules.Add("mdlToolbot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(-2.52482F, 1.78186F, 2.56912F),
                        localAngles = new Vector3(358.4692F, 0.78226F, 7.2713F),
                        localScale = new Vector3(2.0352F, 2.0352F, 2.0352F)
                    }
                }
            );
            rules.Add("mdlEngi", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(-0.28299F, 0.1997F, 0.16822F),
                        localAngles = new Vector3(9.76533F, 47.7655F, 17.03097F),
                        localScale = new Vector3(0.24124F, 0.24124F, 0.24124F)
                    }
                }
            );
            rules.Add("mdlMage", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(0.23014F, 0.19052F, -0.1893F),
                        localAngles = new Vector3(348.8875F, 172.4447F, 359.4133F),
                        localScale = new Vector3(0.18689F, 0.18689F, 0.18689F)
                    }
                }
            );
            rules.Add("mdlMerc", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "UpperArmR",
                        localPos = new Vector3(-0.10187F, 0.12912F, -0.16131F),
                        localAngles = new Vector3(357.5486F, 114.5002F, 192.0381F),
                        localScale = new Vector3(0.21017F, 0.21017F, 0.21017F)
                    }
                }
            );
            rules.Add("mdlTreebot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "FootBackR",
                        localPos = new Vector3(0.00002F, 0.6729F, -0.07867F),
                        localAngles = new Vector3(1.27361F, 90.22883F, 190.2533F),
                        localScale = new Vector3(0.61643F, 0.61643F, 0.61643F)
                    }
                }
            );
            rules.Add("mdlLoader", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "MechHandL",
                        localPos = new Vector3(0.01266F, 0.19225F, 0.19404F),
                        localAngles = new Vector3(2.99238F, 260.9033F, 162.4056F),
                        localScale = new Vector3(0.35349F, 0.35349F, 0.35349F)
                    }
                }
            );
            rules.Add("mdlCroco", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "LowerArmR",
                        localPos = new Vector3(0.39907F, 1.67308F, 0.80923F),
                        localAngles = new Vector3(82.91222F, 0.61286F, 219.5068F),
                        localScale = new Vector3(2.46411F, 2.46411F, 2.47376F)
                    }
                }
            );
            rules.Add("mdlCaptain", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "HandR",
                        localPos = new Vector3(0.03991F, 0.19772F, 0.01788F),
                        localAngles = new Vector3(332.518F, 22.20132F, 153.7587F),
                        localScale = new Vector3(0.30647F, 0.30647F, 0.30647F)
                    }
                }
            );
            rules.Add("mdlBandit2", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Hat",
                        localPos = new Vector3(0.24202F, -0.0034F, 0.03772F),
                        localAngles = new Vector3(11.23946F, 172.4814F, 346.6523F),
                        localScale = new Vector3(0.26425F, 0.26425F, 0.26425F)
                    }
                }
            );
            rules.Add("EngiTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(0.87174F, 0.48917F, -0.83972F),
                        localAngles = new Vector3(359.6133F, 179.1287F, 0.86641F),
                        localScale = new Vector3(0.90225F, 0.90225F, 0.90225F)
                    }
                }
            );
            rules.Add("EngiWalkerTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(0.76911F, 1.02329F, -0.48043F),
                        localAngles = new Vector3(0F, 180.0867F, 0F),
                        localScale = new Vector3(0.79526F, 0.79526F, 0.79526F)
                    }
                }
            );
            rules.Add("mdlScav", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Backpack",
                        localPos = new Vector3(8.16326F, 4.40511F, -0.58616F),
                        localAngles = new Vector3(355.0437F, 196.4816F, 3.06855F),
                        localScale = new Vector3(5.07629F, 5.07629F, 5.07629F)
                    }
                }
            );
            rules.Add("mdlRailGunner", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "GunRoot",
                        localPos = new Vector3(-0.00392F, 0.19377F, 0.01339F),
                        localAngles = new Vector3(1.85989F, 271.3306F, 270.8604F),
                        localScale = new Vector3(0.26937F, 0.26937F, 0.26937F)
                    }
                }
            );
            rules.Add("mdlVoidSurvivor", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Hand",
                        localPos = new Vector3(-0.07327F, 0.16054F, -0.01304F),
                        localAngles = new Vector3(320.762F, 175.2019F, 0.1112F),
                        localScale = new Vector3(0.35643F, 0.35643F, 0.35643F)
                    }
                }
            );

            return rules;
        }

        public static void AddTokens(float NoticeOfAbsence_SpeedBuff)
        {
            float NoticeOfAbsence_SpeedBuffReadable = NoticeOfAbsence_SpeedBuff * 100f;

            LanguageAPI.Add("H3_" + upperName + "_NAME", "Notice Of Absence");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Move faster the more void items you have. <style=cIsVoid>Corrupts all Bucket Lists.</style>");
            LanguageAPI.Add("H3_" + upperName + "_DESC", "For each <style=cIsVoid>void item</style> in your inventory, move <style=cIsUtility>" + NoticeOfAbsence_SpeedBuffReadable + "%</style> faster per stack. <style=cIsVoid>Corrupts all Bucket Lists.</style>");
            LanguageAPI.Add("H3_" + upperName + "_LORE", "I'm leaving tomorrow\n\nYou wouldn't understand why\n\n- Alex");
        }

        private static void AddHooks(ItemDef itemDefToHooks, float NoticeOfAbsence_SpeedBuff, float NoticeOfAbsence_MaxSpeedBuff) // Insert hooks here
        {
            // Void transformation
            VoidTransformation.CreateTransformation(itemDefToHooks, "BucketList");

            void NoticeOfAbsenceRecalculateStats(CharacterBody body, RecalculateStatsAPI.StatHookEventArgs args)
            {
                if (body.inventory && body.inventory.GetItemCount(itemDefToHooks) > 0)
                {
                    float voidItemCount = 0f;
                    voidItemCount += body.inventory.GetTotalItemCountOfTier(ItemTier.VoidTier1);
                    voidItemCount += body.inventory.GetTotalItemCountOfTier(ItemTier.VoidTier2);
                    voidItemCount += body.inventory.GetTotalItemCountOfTier(ItemTier.VoidTier3);
                    voidItemCount += body.inventory.GetTotalItemCountOfTier(ItemTier.VoidBoss);
                    float finalSpeedBuff = NoticeOfAbsence_SpeedBuff * (voidItemCount * body.inventory.GetItemCount(itemDefToHooks));
                    if (finalSpeedBuff > NoticeOfAbsence_MaxSpeedBuff)
                    {
                        finalSpeedBuff = NoticeOfAbsence_MaxSpeedBuff;
                    }
                    args.moveSpeedMultAdd += finalSpeedBuff;
                }
            }
            RecalculateStatsAPI.GetStatCoefficients += NoticeOfAbsenceRecalculateStats;
        }

        public static void Initiate(float NoticeOfAbsence_SpeedBuff, float NoticeOfAbsence_MaxSpeedBuff) // Finally, initiate the item and all of its features
        {
            CreateItem();
            ItemAPI.Add(new CustomItem(itemDefinition, CreateDisplayRules()));
            AddTokens(NoticeOfAbsence_SpeedBuff);
            AddHooks(itemDefinition, NoticeOfAbsence_SpeedBuff, NoticeOfAbsence_MaxSpeedBuff);
        }
    }
}
