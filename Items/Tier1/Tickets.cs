using R2API;
using RoR2;
using UnityEngine;
using Hex3Mod;
using Hex3Mod.HelperClasses;
using EntityStates.AffixVoid;
using Hex3Mod.Utils;
using static Hex3Mod.Main;

namespace Hex3Mod.Items
{
    /*
    400 Tickets is part of the Consumables pack. There are many "break on low health" consumables, so one that has a novel utility seemed like a good idea
    Mostly useless (but still advantageous) if used on a small chest, but a Large Chest or Legendary Chest is where you need one of these.
    */
    public class Tickets
    {
        static string itemName = "FourHundredTickets";
        static string upperName = itemName.ToUpper();
        public static ItemDef itemDef;
        static ItemDef consumedItemDef;
        public static GameObject LoadPrefab()
        {
            GameObject pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/TicketsPrefab.prefab");
            if (Main.debugMode == true)
            {
                pickupModelPrefab.GetComponentInChildren<Renderer>().gameObject.AddComponent<MaterialControllerComponents.HGControllerFinder>();
            }
            return pickupModelPrefab;
        }
        public static Sprite LoadSprite()
        {
            Sprite pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/Tickets.png");
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

            item.tags = new ItemTag[]{ ItemTag.Utility, ItemTag.AIBlacklist, ItemTag.BrotherBlacklist, ItemTag.CannotDuplicate };
            item.deprecatedTier = ItemTier.Tier1;
            item.canRemove = true;
            item.hidden = false;
            item.requiredExpansion = Hex3ModExpansion;

            item.pickupModelPrefab = LoadPrefab();
            item.pickupIconSprite = LoadSprite();

            return item;
        }
        public static ItemDef CreateConsumedItem()
        {
            ItemDef item = ScriptableObject.CreateInstance<ItemDef>();

            item.name = itemName + "Consumed";
            item.nameToken = "H3_" + upperName + "CONSUMED_NAME";
            item.pickupToken = "H3_" + upperName + "CONSUMED_PICKUP";
            item.descriptionToken = "H3_" + upperName + "CONSUMED_DESC";

            item.tags = new ItemTag[] { ItemTag.CannotCopy, ItemTag.CannotDuplicate , ItemTag.AIBlacklist, ItemTag.BrotherBlacklist };
            item.deprecatedTier = ItemTier. NoTier;
            item.canRemove = false;
            item.hidden = false;

            item.pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/TicketsPrefab.prefab");
            item.pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/TicketsConsumed.png");

            return item;
        }

        public static ItemDisplayRuleDict CreateDisplayRules()
        {
            GameObject ItemDisplayPrefab = helpers.PrepareItemDisplayModel(PrefabAPI.InstantiateClone(LoadPrefab(), LoadPrefab().name + "Display", false));

            ItemDisplayRuleDict rules = new ItemDisplayRuleDict();
            rules.Add("mdlCommandoDualies", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "CalfL",
                        localPos = new Vector3(0.00927F, 0.10541F, -0.02883F),
                        localAngles = new Vector3(15.07626F, 184.3583F, 246.3487F),
                        localScale = new Vector3(0.37297F, 0.34407F, 0.34949F)
                    }
                }
            );
            rules.Add("mdlHuntress", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "HeadCenter",
                        localPos = new Vector3(0.05518F, -0.03063F, -0.05223F),
                        localAngles = new Vector3(22.7539F, 112.5206F, 30.06874F),
                        localScale = new Vector3(0.3862F, 0.3862F, 0.3862F)
                    }
                }
            );
            rules.Add("mdlToolbot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "LowerArmL",
                        localPos = new Vector3(0.64832F, 5.44166F, -0.05396F),
                        localAngles = new Vector3(358.7048F, 106.5688F, 270.3022F),
                        localScale = new Vector3(2.94697F, 2.94697F, 2.94697F)
                    }
                }
            );
            rules.Add("mdlEngi", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Stomach",
                        localPos = new Vector3(-0.06484F, -0.0086F, -0.05942F),
                        localAngles = new Vector3(350.3929F, 247.1018F, 264.1864F),
                        localScale = new Vector3(0.62793F, 0.62793F, 0.62793F)
                    }
                }
            );
            rules.Add("mdlMage", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(-0.16061F, 0.26349F, -0.14924F),
                        localAngles = new Vector3(347.4555F, 280.9578F, 297.396F),
                        localScale = new Vector3(-0.43104F, 0.42094F, 0.35703F)
                    }
                }
            );
            rules.Add("mdlMerc", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "HandL",
                        localPos = new Vector3(-0.08698F, 0.10075F, -0.0041F),
                        localAngles = new Vector3(49.75164F, 1.50993F, 181.2459F),
                        localScale = new Vector3(-0.25159F, 0.27408F, 0.2336F)
                    }
                }
            );
            rules.Add("mdlTreebot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "CalfBackR",
                        localPos = new Vector3(0.08805F, 0.61719F, -0.08117F),
                        localAngles = new Vector3(8.24625F, 100.2931F, 272.7766F),
                        localScale = new Vector3(-0.64708F, 0.7225F, 0.7225F)
                    }
                }
            );
            rules.Add("mdlLoader", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(0.14977F, 0.32377F, 0.29649F),
                        localAngles = new Vector3(0F, 0F, 0F),
                        localScale = new Vector3(0.33674F, 0.33674F, 0.33674F)
                    }
                }
            );
            rules.Add("mdlCroco", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "HandL",
                        localPos = new Vector3(0.41647F, 1.1654F, 0.46774F),
                        localAngles = new Vector3(357.1312F, 12.39285F, 124.7832F),
                        localScale = new Vector3(3.67553F, 3.67553F, 3.67553F)
                    }
                }
            );
            rules.Add("mdlCaptain", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "HandL",
                        localPos = new Vector3(-0.04707F, -0.08236F, 0.04182F),
                        localAngles = new Vector3(8.12018F, 300.2912F, 97.09362F),
                        localScale = new Vector3(0.34931F, 0.34931F, 0.34931F)
                    }
                }
            );
            rules.Add("mdlBandit2", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Hat",
                        localPos = new Vector3(-0.09384F, 0.01368F, -0.04171F),
                        localAngles = new Vector3(8.61942F, 231.3094F, 282.6341F),
                        localScale = new Vector3(0.52385F, 0.52385F, 0.52385F)
                    }
                }
            );
            rules.Add("EngiTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "LegBar2",
                        localPos = new Vector3(0.16568F, 0.50018F, -0.06216F),
                        localAngles = new Vector3(354.0227F, 92.69547F, 87.36358F),
                        localScale = new Vector3(1.14751F, 1.14751F, 1.14751F)
                    }
                }
            );
            rules.Add("EngiWalkerTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "LegBar2",
                        localPos = new Vector3(0.16876F, -0.16572F, -0.34877F),
                        localAngles = new Vector3(2.86457F, 92.27133F, 149.7901F),
                        localScale = new Vector3(1.25716F, 1.25716F, 1.25716F)
                    }
                }
            );
            rules.Add("mdlScav", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Backpack",
                        localPos = new Vector3(-2.91533F, 6.70336F, -2.1303F),
                        localAngles = new Vector3(345.0512F, 210.8083F, 4.95444F),
                        localScale = new Vector3(15.02295F, 15.02295F, 15.02295F)
                    }
                }
            );
            rules.Add("mdlRailGunner", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "ThighR",
                        localPos = new Vector3(0.02434F, 0.18574F, 0.11247F),
                        localAngles = new Vector3(343.7317F, 15.11429F, 272.6107F),
                        localScale = new Vector3(0.52614F, 0.52614F, 0.52614F)
                    }
                }
            );
            rules.Add("mdlVoidSurvivor", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "UpperArmR",
                        localPos = new Vector3(0.11415F, 0.06479F, 0.10721F),
                        localAngles = new Vector3(327.629F, 11.98644F, 134.3462F),
                        localScale = new Vector3(0.57511F, 0.57511F, 0.57511F)
                    }
                }
            );

            return rules;
        }

        // Hidden items should not display at all
        public static ItemDisplayRuleDict CreateHiddenDisplayRules()
        {
            return new ItemDisplayRuleDict();
        }

        public static void AddTokens()
        {
            LanguageAPI.Add("H3_" + upperName + "_NAME", "400 Tickets");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "The next chest you open will contain double the items.");
            LanguageAPI.Add("H3_" + upperName + "_DESC", "The next chest, lunar pod or void cradle you open will contain <style=cIsUtility>double the amount of items</style>. <style=cStack>Consumed on use.</style>");
            LanguageAPI.Add("H3_" + upperName + "_LORE", "\"Oh yeah, these. They're old tickets from that 'space casino' they tried to open up. While the adults were playing slots or roulette or whatever in the main floor, their kids could go spend their pocket change on the machines downstairs. These are the tickets they'd get as winnings. Could buy- you know- teddy bears and hoverboards and stuff with them.\"" +
            "\n\n\"That's screwed up. Why do you have those?\"" +
            "\n\n\"Well, there's one crucial detail they forgot about before closing up the casino: All the tickets are still valid.\"" +
            "\n\n\"...Can we still get a hoverboard with them?\"");

            LanguageAPI.Add("H3_" + upperName + "CONSUMED_NAME", "Used Tickets");
            LanguageAPI.Add("H3_" + upperName + "CONSUMED_PICKUP", "No longer valid.");
            LanguageAPI.Add("H3_" + upperName + "CONSUMED_DESC", "No longer valid.");
        }
        public static void UpdateItemStatus(Run run)
        {
            if (!Tickets_Enable.Value)
            {
                LanguageAPI.AddOverlay("H3_" + upperName + "_NAME", "400 Tickets" + " <style=cDeath>[DISABLED]</style>");
                if (run && run.availableItems.Contains(itemDef.itemIndex)) { run.availableItems.Remove(itemDef.itemIndex); }
            }
            else
            {
                LanguageAPI.AddOverlay("H3_" + upperName + "_NAME", "400 Tickets");
                if (run && !run.availableItems.Contains(itemDef.itemIndex) && run.IsExpansionEnabled(Hex3ModExpansion)) { run.availableItems.Add(itemDef.itemIndex); }
            }
        }

        private static void AddHooks()
        {
            // Install item behavior to ticket holders
            void CharacterBody_OnInventoryChanged(On.RoR2.CharacterBody.orig_OnInventoryChanged orig, CharacterBody self)
            {
                orig(self);
                if (self.inventory && self.inventory.GetItemCount(itemDef) > 0 && !self.GetComponent<TicketsBehavior>())
                {
                    self.AddItemBehavior<TicketsBehavior>(1);
                }
            }

            // Purchases mark the chest as ticketed, and also makes sure the purchaser has an itembehavior
            void PurchaseInteraction_OnInteractionBegin(On.RoR2.PurchaseInteraction.orig_OnInteractionBegin orig, PurchaseInteraction self, Interactor activator)
            {
                orig(self, activator);
                if (activator.TryGetComponent(out CharacterBody body) && body.inventory && body.inventory.GetItemCount(itemDef) > 0 && !self.isShrine)
                {
                    if (!body.GetComponent<TicketsBehavior>())
                    {
                        body.AddItemBehavior<TicketsBehavior>(1);
                    }
                    body.GetComponent<TicketsBehavior>().interaction = self;
                }
            }

            // If a chest is the same as an interactor's ticketed chest, drop more items
            void ChestBehavior_ItemDrop(On.RoR2.ChestBehavior.orig_ItemDrop orig, ChestBehavior self)
            {
                if (self.gameObject.GetComponent<PurchaseInteraction>())
                {
                    foreach (TicketsBehavior behavior in Object.FindObjectsOfType<TicketsBehavior>())
                    {
                        if (behavior && behavior.interaction && behavior.interaction == self.gameObject.GetComponent<PurchaseInteraction>())
                        {
                            if (self.gameObject.name == "VoidChest(Clone)" || self.gameObject.name == "VoidChest")
                            {
                                self.dropUpVelocityStrength = 10f;
                                self.dropForwardVelocityStrength = 20f;
                            }
                            self.dropCount += 1;
                            behavior.item = itemDef;
                            behavior.consumedItem = consumedItemDef;
                            behavior.interaction = null;
                            behavior.ExchangeTickets();
                        }
                    }
                }
                orig(self);
            }

            On.RoR2.CharacterBody.OnInventoryChanged += CharacterBody_OnInventoryChanged;
            On.RoR2.PurchaseInteraction.OnInteractionBegin += PurchaseInteraction_OnInteractionBegin;
            On.RoR2.ChestBehavior.ItemDrop += ChestBehavior_ItemDrop;
        }

        public class TicketsBehavior : CharacterBody.ItemBehavior
        {
            public PurchaseInteraction interaction;
            public ItemDef item;
            public ItemDef consumedItem;

            public void ExchangeTickets()
            {
                if (body.inventory && body.master)
                {
                    body.inventory.RemoveItem(item);
                    body.inventory.GiveItem(consumedItem);
                    CharacterMasterNotificationQueue.SendTransformNotification(body.master, item.itemIndex, consumedItem.itemIndex, CharacterMasterNotificationQueue.TransformationType.Default);
                    Util.PlaySound(RouletteChestController.Opening.soundEntryEvent, body.gameObject);
                    if (body.inventory.GetItemCount(item) <= 0)
                    {
                        Destroy(this);
                    }
                }
            }
        }

        public static void Initiate()
        {
            itemDef = CreateItem();
            consumedItemDef = CreateConsumedItem();
            ItemAPI.Add(new CustomItem(itemDef, CreateDisplayRules()));
            ItemAPI.Add(new CustomItem(consumedItemDef, CreateHiddenDisplayRules()));
            AddTokens();
            UpdateItemStatus(null);
            AddHooks();
        }
    }
}
