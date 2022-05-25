using BepInEx;
using BepInEx.Configuration;
using R2API;
using R2API.Utils;
using RoR2;
using System;
using UnityEngine;
using Hex3Mod;
using Hex3Mod.Logging;
using Hex3Mod.HelperClasses;

namespace Hex3Mod.Items
{
    /*
    Shard Of Glass is a very simple stat increase, an item you'd never be unhappy to find
    Its secondary purpose, however, is to synergize with Shattered Reflection, forming a unique common-uncommon synergy and giving this simple item more utility
    */
    public class ShardOfGlass
    {
        // Create functions here for defining the ITEM, TOKENS, HOOKS and CONFIG. This is simpler than doing it in Main
        static string itemName = "ShardOfGlass"; // Change this name when making a new item
        static string upperName = itemName.ToUpper();
        static ItemDef itemDefinition = CreateItem();
        public static GameObject LoadPrefab()
        {
            GameObject pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/ShardOfGlassPrefab.prefab");
            return pickupModelPrefab;
        }
        public static Sprite LoadSprite()
        {
            Sprite pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/ShardOfGlass.png");
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

            item.tags = new ItemTag[]{ ItemTag.Damage }; // Also change these when making a new item
            item.deprecatedTier = ItemTier.Tier1;
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

        public static void AddTokens(float DamageIncrease_Config)
        {
            float DamageIncrease_Config_Readable = DamageIncrease_Config * 100f;

            LanguageAPI.Add("H3_" + upperName + "_NAME", "Shard Of Glass");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Increase your damage.");
            LanguageAPI.Add("H3_" + upperName + "_DESC", "Increase your <style=cIsDamage>base damage</style> by <style=cIsDamage>" + DamageIncrease_Config_Readable + "%</style> <style=cStack>(+" + DamageIncrease_Config_Readable + "% per stack)</style>.");
            LanguageAPI.Add("H3_" + upperName + "_LORE", "The essence of a broken body\n\nYou collect the pieces carefully\n\nAn air of arrogance surrounds these shards\n\nYou feel like you can take on the world and never break beneath it");
        }

        private static void AddHooks(ItemDef itemDefToHooks, float DamageIncrease_Config) // Insert hooks here
        {
            float DamageIncrease = DamageIncrease_Config;

            void H3_RecalculateStatsAPI_GetStatCoefficients(CharacterBody sender, RecalculateStatsAPI.StatHookEventArgs args)
            {
                if (sender.inventory)
                {
                    Inventory inventory = sender.inventory;
                    int itemCount = inventory.GetItemCount(itemDefToHooks);
                    if (itemCount > 0)
                    {
                        args.damageMultAdd += (DamageIncrease * itemCount);
                    }
                }
            }

            RecalculateStatsAPI.GetStatCoefficients += H3_RecalculateStatsAPI_GetStatCoefficients;
        }

        public static void Initiate(float DamageIncrease_Config) // Finally, initiate the item and all of its features
        {
            CreateItem();
            ItemAPI.Add(new CustomItem(itemDefinition, CreateDisplayRules()));
            AddTokens(DamageIncrease_Config);
            AddHooks(itemDefinition, DamageIncrease_Config);
        }
    }
}
