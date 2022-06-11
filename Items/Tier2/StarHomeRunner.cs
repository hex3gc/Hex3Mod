using R2API;
using RoR2;
using UnityEngine;
using Hex3Mod.HelperClasses;
using Hex3Mod.Behaviors;

namespace Hex3Mod.Items
{
    /*
    Star Home Runner would be the shuriken of the melee fighter's kit. It could still serve a good Crowd Control / AOE purpose for ranged survivors, too.
    Not to mention band procs
    */
    public class StarHomeRunner
    {
        // Create functions here for defining the ITEM, TOKENS, HOOKS and CONFIG. This is simpler than doing it in Main
        static string itemName = "StarHomeRunner"; // Change this name when making a new item
        static string upperName = itemName.ToUpper();
        static ItemDef itemDefinition = CreateItem();
        public static GameObject LoadPrefab()
        {
            GameObject pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/EmpathyPrefab.prefab");
            return pickupModelPrefab;
        }
        public static Sprite LoadSprite()
        {
            Sprite pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/Empathy.png");
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

            item.tags = new ItemTag[]{ ItemTag.Healing }; // Also change these when making a new item
            item.deprecatedTier = ItemTier.Tier2;
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
                        childName = "Pelvis",
                        localPos = new Vector3(0.46303F, -0.05688F, 0.22363F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.04992F, 0.04992F, 0.04992F)
                    }
                }
            );
            rules.Add("mdlHuntress", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.46303F, -0.06435F, 0.21564F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.04496F, 0.04496F, 0.04496F)
                    }
                }
            );
            rules.Add("mdlToolbot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Hip",
                        localPos = new Vector3(-3.67415F, -5.35767F, 3.49506F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.37776F, 0.37776F, 0.37776F)
                    }
                }
            );
            rules.Add("mdlEngi", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.70594F, -0.32829F, 0.46413F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.04899F, 0.04899F, 0.04899F)
                    }
                }
            );
            rules.Add("mdlMage", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.46303F, -0.44582F, 0.16459F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.04752F, 0.04752F, 0.04752F)
                    }
                }
            );
            rules.Add("mdlMerc", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.46303F, -0.45782F, 0.25012F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.0447F, 0.0447F, 0.0447F)
                    }
                }
            );
            rules.Add("mdlTreebot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "PlatformBase",
                        localPos = new Vector3(1.49471F, 1.49075F, -1.24891F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.11992F, 0.11992F, 0.11992F)
                    }
                }
            );
            rules.Add("mdlLoader", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.73526F, -0.48415F, 0.3866F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.04441F, 0.04441F, 0.04441F)
                    }
                }
            );
            rules.Add("mdlCroco", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Hip",
                        localPos = new Vector3(5.36278F, -1.64505F, 3.96713F),
                        localAngles = new Vector3(319.2833F, 0F, 0F),
                        localScale = new Vector3(0.32944F, 0.32944F, 0.33073F)
                    }
                }
            );
            rules.Add("mdlCaptain", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.55942F, -0.62916F, 0.15856F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.04415F, 0.04415F, 0.04415F)
                    }
                }
            );
            rules.Add("mdlBandit2", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.48541F, -0.15744F, -0.15702F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.0428F, 0.0428F, 0.0428F)
                    }
                }
            );
            rules.Add("EngiTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Base",
                        localPos = new Vector3(1.59118F, 2.13328F, 0.22363F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.19256F, 0.19256F, 0.19256F)
                    }
                }
            );
            rules.Add("EngiWalkerTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Base",
                        localPos = new Vector3(1.55029F, 2.32694F, 0.22363F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.18841F, 0.18841F, 0.18841F)
                    }
                }
            );
            rules.Add("mdlScav", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(14.30769F, -3.86581F, 1.33207F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(1.14461F, 1.14461F, 1.14461F)
                    }
                }
            );
            rules.Add("mdlRailGunner", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.46303F, -0.10003F, 0.24969F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.04316F, 0.04316F, 0.04316F)
                    }
                }
            );
            rules.Add("mdlVoidSurvivor", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(-0.53176F, -0.30046F, 0.2652F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.04685F, 0.04685F, 0.04685F)
                    }
                }
            );

            return rules;
        }

        public static void AddTokens()
        {
            LanguageAPI.Add("H3_" + upperName + "_NAME", "Star Home Runner");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Using your primary fire will also swing a stunning bat.");
            LanguageAPI.Add("H3_" + upperName + "_DESC", "Using your primary fire will also swing a bat in a wide arc in front of you, stunning enemies and dealing 300% (+300% per stack) damage. Recharges every 4 seconds.");
            LanguageAPI.Add("H3_" + upperName + "_LORE", "\"Escaping from your purpose is impossible.\"\n\n<style=cStack>- Inscription found on the handle of the bat, author unknown</style>");
        }

        private static void AddHooks(ItemDef itemDefToHooks) // Insert hooks here
        {
            // Add or modify our item behavior when the character's inventory changes
            On.RoR2.CharacterBody.OnInventoryChanged += (orig, self) =>
            {
                orig(self); // Enabling/disabling and stack behaviors
                if (self.inventory && self.inventory.GetItemCount(itemDefToHooks) > 0 && !self.GetComponent<StarHomeRunnerBehavior>())
                {
                    self.AddItemBehavior<StarHomeRunnerBehavior>(self.inventory.GetItemCount(itemDefToHooks));
                    StarHomeRunnerBehavior behavior = self.GetComponent<StarHomeRunnerBehavior>();
                    behavior.enabled = true;
                    behavior.totalReloadTime = 4f;
                    behavior.damageCoefficientBase = 3f;
                    behavior.damageCoefficientPerStack = 3f;
                    behavior.force = 0f;
                }
                if (self.inventory && self.inventory.GetItemCount(itemDefToHooks) > 0 && self.GetComponent<StarHomeRunnerBehavior>())
                {
                    self.GetComponent<StarHomeRunnerBehavior>().stack = self.inventory.GetItemCount(itemDefToHooks);
                    self.GetComponent<StarHomeRunnerBehavior>().enabled = true;
                }
                if (self.inventory && self.inventory.GetItemCount(itemDefToHooks) <= 0 && self.GetComponent<StarHomeRunnerBehavior>())
                {
                    self.GetComponent<StarHomeRunnerBehavior>().enabled = false;
                }
            };
        }

        public static void Initiate() // Finally, initiate the item and all of its features
        {
            CreateItem();
            ItemAPI.Add(new CustomItem(itemDefinition, CreateDisplayRules()));
            AddTokens();
            AddHooks(itemDefinition);
        }
    }
}
