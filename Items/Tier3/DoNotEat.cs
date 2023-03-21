using R2API;
using RoR2;
using RoR2.ExpansionManagement;
using System.Linq;
using UnityEngine;
using Hex3Mod.HelperClasses;
using VoidItemAPI;
using UnityEngine.Networking;
using System;
using Hex3Mod.Utils;
using static Hex3Mod.Main;
using System.ComponentModel;
using static Hex3Mod.Items.Tickets;
using System.Collections.Generic;

namespace Hex3Mod.Items
{
    /*
    What if you want a pearl run, but don't want to find a pesky cleansing pool? That's where Do Not Eat comes in
    Opening chests and getting a bonus feels good, plus we needed more max health items to counteract late game
    */
    public static class DoNotEat
    {
        static string itemName = "DoNotEat";
        static string upperName = itemName.ToUpper();
        public static ItemDef itemDef;
        public static GameObject LoadPrefab()
        {
            GameObject pickupModelPrefab = MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/DoNotEatPrefab.prefab");
            if (debugMode)
            {
                pickupModelPrefab.GetComponentInChildren<Renderer>().gameObject.AddComponent<MaterialControllerComponents.HGControllerFinder>();
            }
            return pickupModelPrefab;
        }
        public static Sprite LoadSprite()
        {
            return MainAssets.LoadAsset<Sprite>("Assets/Icons/DoNotEat.png");
        }
        public static ItemDef CreateItem()
        {
            ItemDef item = ScriptableObject.CreateInstance<ItemDef>();

            item.name = itemName;
            item.nameToken = "H3_" + upperName + "_NAME";
            item.pickupToken = "H3_" + upperName + "_PICKUP";
            item.descriptionToken = "H3_" + upperName + "_DESC";
            item.loreToken = "H3_" + upperName + "_LORE";

            item.tags = new ItemTag[]{ ItemTag.Utility, ItemTag.Healing, ItemTag.AIBlacklist, ItemTag.BrotherBlacklist };
            item.deprecatedTier = ItemTier.Tier3;
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
                        localPos = new Vector3(-0.0001F, 0.12851F, 0.18186F),
                        localAngles = new Vector3(321.9487F, 359.8018F, 0.1498F),
                        localScale = new Vector3(0.06886F, 0.06886F, 0.06886F)
                    }
                }
            );
            rules.Add("mdlHuntress", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(0.00101F, 0.06548F, 0.11767F),
                        localAngles = new Vector3(329.2418F, 359.8095F, 0.16626F),
                        localScale = new Vector3(0.04379F, 0.04379F, 0.04379F)
                    }
                }
            );
            rules.Add("mdlToolbot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(-1.98842F, 1.50671F, 3.35734F),
                        localAngles = new Vector3(25.83804F, 356.7572F, 351.4152F),
                        localScale = new Vector3(0.38077F, 0.38077F, 0.38077F)
                    }
                }
            );
            rules.Add("mdlEngi", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "HeadCenter",
                        localPos = new Vector3(-0.00101F, -0.14401F, 0.15288F),
                        localAngles = new Vector3(353.669F, 359.8897F, 0.23848F),
                        localScale = new Vector3(0.06797F, 0.06797F, 0.06797F)
                    }
                }
            );
            rules.Add("mdlMage", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(0.0002F, -0.04308F, 0.12114F),
                        localAngles = new Vector3(0F, 0F, 0F),
                        localScale = new Vector3(0.04209F, 0.04209F, 0.04209F)
                    }
                }
            );
            rules.Add("mdlMerc", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(-0.00012F, 0.0308F, 0.15569F),
                        localAngles = new Vector3(338.8683F, 359.9052F, 0.01971F),
                        localScale = new Vector3(0.05344F, 0.05344F, 0.05344F)
                    }
                }
            );
            rules.Add("mdlTreebot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Base",
                        localPos = new Vector3(0.12404F, 0.61438F, -1.08326F),
                        localAngles = new Vector3(280.3423F, 180.0001F, 179.9999F),
                        localScale = new Vector3(0.10109F, 0.10109F, 0.10109F)
                    }
                }
            );
            rules.Add("mdlLoader", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(0.00013F, -0.02358F, 0.16196F),
                        localAngles = new Vector3(0F, 0F, 0F),
                        localScale = new Vector3(0.05644F, 0.05729F, 0.0604F)
                    }
                }
            );
            rules.Add("mdlCroco", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "MouthMuzzle",
                        localPos = new Vector3(0.04082F, 2.49257F, 4.39971F),
                        localAngles = new Vector3(41.27963F, 317.4682F, 237.4874F),
                        localScale = new Vector3(0.58896F, 0.58896F, 0.59127F)
                    }
                }
            );
            rules.Add("mdlCaptain", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(0.00075F, -0.03739F, 0.14602F),
                        localAngles = new Vector3(337.1049F, 359.118F, 357.7592F),
                        localScale = new Vector3(0.05007F, 0.05007F, 0.05007F)
                    }
                }
            );
            rules.Add("mdlBandit2", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(0.00746F, -0.03999F, 0.12238F),
                        localAngles = new Vector3(347.5478F, 357.5539F, 0.28736F),
                        localScale = new Vector3(0.04863F, 0.04863F, 0.04863F)
                    }
                }
            );
            rules.Add("EngiTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(0F, 0.73386F, -0.51354F),
                        localAngles = new Vector3(324.7035F, 0F, 0F),
                        localScale = new Vector3(0.22695F, 0.22695F, 0.22695F)
                    }
                }
            );
            rules.Add("EngiWalkerTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(0F, 0.20621F, 0.2632F),
                        localAngles = new Vector3(337.459F, 0F, 0F),
                        localScale = new Vector3(0.26621F, 0.26621F, 0.26621F)
                    }
                }
            );
            rules.Add("mdlScav", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(0F, 4.23119F, 2.10605F),
                        localAngles = new Vector3(47.57335F, 0.00004F, 0.00009F),
                        localScale = new Vector3(1.34648F, 1.34648F, 1.34648F)
                    }
                }
            );
            rules.Add("mdlRailGunner", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(0.00064F, -0.01436F, 0.06606F),
                        localAngles = new Vector3(0F, 0F, 0F),
                        localScale = new Vector3(0.0519F, 0.05046F, 0.05046F)
                    }
                }
            );
            rules.Add("mdlVoidSurvivor", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(-0.02663F, 0.01837F, 0.17947F),
                        localAngles = new Vector3(296.8297F, 308.2401F, 40.70057F),
                        localScale = new Vector3(0.06899F, 0.06899F, 0.06899F)
                    }
                }
            );

            return rules;
        }

        public static void AddTokens()
        {
            LanguageAPI.Add("H3_" + upperName + "_NAME", "Do Not Eat");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Chests and shop terminals may also contain a pearl.");
            LanguageAPI.Add("H3_" + upperName + "_LORE", "You know those little silica packets that say \"Do Not Eat\" on them? Or, should I say 'silica' at all?\n\n" +
                "You see, I'm a skeptic. When the government tells me that something is dangerous, I will do it. When the government tells me there's nothing wrong, I won't believe them. The government tells me to wear protective equipment to work, I shrug it off-- and the funny thing is, I'm usually right! When was the last time a mask or a seatbelt did anything but make you uncomfortable?\n\n" +
                "So when the government says, \"Do Not Eat\", my only logical choice is to eat. There must be something in there they don't *want* us to eat, for whatever reason... a chemical that counteracts flouridic mind control, perhaps? Something that decalcifies the pituitary gland? A cure for toxoplasmosis or even cancer? Nobody knows because nobody is willing to find out. As you know- my subscribers- that's a job for me.\n\n" +
                "I've ordered 100 individual packs of Mercurian seaweed, each of which should contain a silica packet. I'll cut them open, pour out those little mysterious orbs and season my meals with them. My workplace is sending me out to a remote system so I won't be able to post for a while, but give me just a few months and I will reward you with the truth. Watch this space...");
        }
        public static void UpdateItemStatus(Run run)
        {
            if (!DoNotEat_Enable.Value)
            {
                LanguageAPI.AddOverlay("H3_" + upperName + "_NAME", "Do Not Eat" + " <style=cDeath>[DISABLED]</style>");
                if (run && run.availableItems.Contains(itemDef.itemIndex)) { run.availableItems.Remove(itemDef.itemIndex); }
            }
            else
            {
                LanguageAPI.AddOverlay("H3_" + upperName + "_NAME", "Do Not Eat");
                if (run && !run.availableItems.Contains(itemDef.itemIndex) && run.IsExpansionEnabled(Hex3ModExpansion)) { run.availableItems.Add(itemDef.itemIndex); }
            }
            LanguageAPI.AddOverlay("H3_" + upperName + "_DESC", "Chests and shop terminals have a <style=cShrine>" + DoNotEat_PearlChancePerStack.Value + "%</style> <style=cStack>(+" + DoNotEat_PearlChancePerStack.Value + "% per stack)</style> chance to also contain a <style=cShrine>Pearl</style> or an <style=cShrine>Irradiant Pearl</style>.");
        }

        private static void AddHooks()
        {
            CostTypeIndex[] blacklistedCostTypes = new CostTypeIndex[8]
            {
                CostTypeIndex.LunarItemOrEquipment,
                CostTypeIndex.WhiteItem,
                CostTypeIndex.GreenItem,
                CostTypeIndex.RedItem,
                CostTypeIndex.BossItem,
                CostTypeIndex.ArtifactShellKillerItem,
                CostTypeIndex.PercentHealth,
                CostTypeIndex.VolatileBattery
            };

            // Give item holders an ItemBehavior
            void CharacterBody_OnInventoryChanged(On.RoR2.CharacterBody.orig_OnInventoryChanged orig, CharacterBody self)
            {
                orig(self);
                if (!self.inventory) { return; }
                int itemCount = self.inventory.GetItemCount(itemDef);
                if (itemCount > 0)
                {
                    DoNotEatBehavior component;
                    component = self.GetComponent<DoNotEatBehavior>();
                    if (!component)
                    {
                        component = self.AddItemBehavior<DoNotEatBehavior>(1);
                    }
                    component.stack = itemCount;
                }
                else
                {
                    if (self.GetComponent<DoNotEatBehavior>())
                    {
                        UnityEngine.Object.Destroy(self.GetComponent<DoNotEatBehavior>());
                    }
                }
            }

            // Purchases mark the chest
            void PurchaseInteraction_OnInteractionBegin(On.RoR2.PurchaseInteraction.orig_OnInteractionBegin orig, PurchaseInteraction self, Interactor activator)
            {
                if (self.isShrine || self.isGoldShrine || blacklistedCostTypes.Contains(self.costType))
                {
                    orig(self, activator);
                    return;
                }
                CharacterBody body = activator.GetComponent<CharacterBody>();
                if (body)
                {
                    DoNotEatBehavior component = body.GetComponent<DoNotEatBehavior>();
                    if (component)
                    {
                        component.interactions.Add(self);
                    }
                }
                orig(self, activator);
            }

            // Chest drops
            void ChestBehavior_ItemDrop(On.RoR2.ChestBehavior.orig_ItemDrop orig, ChestBehavior self)
            {
                if (self.gameObject.GetComponent<PurchaseInteraction>())
                {
                    foreach (var behavior in UnityEngine.Object.FindObjectsOfType<DoNotEatBehavior>())
                    {
                        if (behavior && behavior.interactions.Contains(self.gameObject.GetComponent<PurchaseInteraction>()))
                        {
                            if (behavior.body.master && Util.CheckRoll(DoNotEat_PearlChancePerStack.Value * (float)behavior.stack, behavior.body.master.luck, behavior.body.master))
                            {
                                if (Util.CheckRoll(DoNotEat_IrradiantChance.Value))
                                {
                                    PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(RoR2Content.Items.ShinyPearl.itemIndex), (self.dropTransform ?? self.transform).position, Vector3.up * self.dropUpVelocityStrength + self.dropTransform.forward * -self.dropForwardVelocityStrength);
                                }
                                else
                                {
                                    PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(RoR2Content.Items.Pearl.itemIndex), (self.dropTransform ?? self.transform).position, Vector3.up * self.dropUpVelocityStrength + self.dropTransform.forward * -self.dropForwardVelocityStrength);
                                }
                            }
                            behavior.interactions.Remove(self.gameObject.GetComponent<PurchaseInteraction>());
                            if (behavior.interactions.Count > 500) { behavior.interactions.Clear(); }
                        }
                    }
                }
                orig(self);
            }

            // Shop terminal compatibility
            void ShopTerminalBehavior_DropPickup(On.RoR2.ShopTerminalBehavior.orig_DropPickup orig, ShopTerminalBehavior self)
            {
                foreach (var behavior in UnityEngine.Object.FindObjectsOfType<DoNotEatBehavior>()) if (behavior && behavior.interactions.Contains(self.GetComponent<PurchaseInteraction>()) && !self.gameObject.name.Contains("ShrineCleanse"))
                {
                    Vector3 v = self.transform.TransformVector(self.dropVelocity);
                    if (behavior.body.master && Util.CheckRoll(DoNotEat_PearlChancePerStack.Value * (float)behavior.stack, behavior.body.master.luck, behavior.body.master))
                    {
                        if (Util.CheckRoll(DoNotEat_IrradiantChance.Value))
                        {
                            PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(RoR2Content.Items.ShinyPearl.itemIndex), (self.dropTransform ?? self.transform).position, new Vector3(-v.x, v.y, -v.z));
                        }
                        else
                        {
                            PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(RoR2Content.Items.Pearl.itemIndex), (self.dropTransform ?? self.transform).position, new Vector3(-v.x, v.y, -v.z));
                        }
                    }
                    behavior.interactions.Remove(self.GetComponent<PurchaseInteraction>());
                }
                orig(self);
            }

            On.RoR2.CharacterBody.OnInventoryChanged += CharacterBody_OnInventoryChanged;
            On.RoR2.PurchaseInteraction.OnInteractionBegin += PurchaseInteraction_OnInteractionBegin;
            if (DoNotEat_ShopTerminals.Value) { On.RoR2.ShopTerminalBehavior.DropPickup += ShopTerminalBehavior_DropPickup; }
        }

        public static void Initiate()
        {
            itemDef = CreateItem();
            ItemAPI.Add(new CustomItem(itemDef, CreateDisplayRules()));
            AddTokens();
            UpdateItemStatus(null);
            AddHooks();
        }

        public class DoNotEatBehavior : CharacterBody.ItemBehavior
        {
            public List<PurchaseInteraction> interactions = new List<PurchaseInteraction>();
        }
    }
}
