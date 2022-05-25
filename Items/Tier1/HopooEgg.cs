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
using On.EntityStates;

namespace Hex3Mod.Items
{
    /*
    Hopoo Egg increases base jump height and give the player slightly more air control
    This is to provide an alternative to the Hopoo Feather for upward mobility, as there are few options for some survivors
    It also synergizes with the feather by increasing the height of each jump
    The added air control should also be a nice utility
    */
    public class HopooEgg
    {
        // Create functions here for defining the ITEM, TOKENS, HOOKS and CONFIG. This is simpler than doing it in Main
        static string itemName = "HopooEgg"; // Change this name when making a new item
        static string upperName = itemName.ToUpper();
        static ItemDef itemDefinition = CreateItem();
        public static GameObject LoadPrefab()
        {
            GameObject pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/HopooEggPrefab.prefab");
            return pickupModelPrefab;
        }
        public static Sprite LoadSprite()
        {
            Sprite pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/HopooEgg.png");
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

            item.tags = new ItemTag[]{ ItemTag.Utility }; // Also change these when making a new item
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

        public static void AddTokens(float HopooEgg_JumpModifier, float HopooEgg_AirControlModifier)
        {
            float HopooEgg_JumpModifier_Readable = HopooEgg_JumpModifier * 100f;
            float HopooEgg_AirControlModifier_Readable = HopooEgg_AirControlModifier * 100f;

            LanguageAPI.Add("H3_" + upperName + "_NAME", "Hopoo Egg");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Jump higher and with more control.");
            LanguageAPI.Add("H3_" + upperName + "_DESC", "Jump <style=cIsUtility>" + HopooEgg_JumpModifier_Readable + "%</style> higher <style=cStack>(+" + HopooEgg_JumpModifier_Readable + "% per stack)</style>. While in the air, you can control your movement <style=cIsUtility>" + HopooEgg_AirControlModifier_Readable + "%</style> more <style=cStack>(+" + HopooEgg_AirControlModifier_Readable + "% per stack)</style>.");
            LanguageAPI.Add("H3_" + upperName + "_LORE", "\"...The hopoo's chicks are independent, being the only Europan nesting birds that hunt for themselves from birth. One is able to leave its nest as a newborn thanks to the low gravity environment, and it is born nimble enough to navigate down any rough terrain that lies between it and its food. This creates a unique problem for many newborn hopoos, however, as they often find themselves unable to go back up. The hopoo, out of reach of its nest, usually becomes nomadic and lives on its lonesome, having forgotten about its home by the time it has grown into an adult. This is where they get their common nickname, the 'hobo bird'.\"\n\n- Europan Wildlife Guide");
        }

        private static void AddHooks(ItemDef itemDefToHooks, float HopooEgg_JumpModifier, float HopooEgg_AirControlModifier) // Insert hooks here
        {
            float jumpModifier = HopooEgg_JumpModifier;
            float airControlModifier = HopooEgg_AirControlModifier;

            void H3_JumpVelocity(GenericCharacterMain.orig_ApplyJumpVelocity orig, CharacterMotor motor, CharacterBody body, float horiz, float vert, bool vault)
            {
                if ((body != null) && (body.inventory != null))
                {
                    int itemCount = body.inventory.GetItemCount(itemDefToHooks);
                    if (itemCount > 0)
                    {
                        vert *= 1f + jumpModifier * (float)itemCount;
                        motor.airControl = 0.25f + (airControlModifier * (float)itemCount);
                    }
                }
                orig.Invoke(motor, body, horiz, vert, vault);
            }

            GenericCharacterMain.ApplyJumpVelocity += new GenericCharacterMain.hook_ApplyJumpVelocity(H3_JumpVelocity);
        }

        public static void Initiate(float HopooEgg_JumpModifier, float HopooEgg_AirControlModifier) // Finally, initiate the item and all of its features
        {
            CreateItem();
            ItemAPI.Add(new CustomItem(itemDefinition, CreateDisplayRules()));
            AddTokens(HopooEgg_JumpModifier, HopooEgg_AirControlModifier);
            AddHooks(itemDefinition, HopooEgg_JumpModifier, HopooEgg_AirControlModifier);
        }
    }
}
