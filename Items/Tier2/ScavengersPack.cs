using R2API;
using RoR2;
using System;
using UnityEngine;
using Hex3Mod.HelperClasses;
using Hex3Mod.Logging;
using System.Collections.Generic;
using Hex3Mod.Utils;
using static Hex3Mod.Main;

namespace Hex3Mod.Items
{
    /*
    It's a pouch now!
    */
    public class ScavengersPack
    {
        static string itemName = "ScavengersPouch";
        static string upperName = itemName.ToUpper();
        static ItemDef itemDef;
        static ItemDef consumedItemDef;
        static ItemDef hiddenItemDef;
        public static GameObject LoadPrefab()
        {
            GameObject pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/VFXPASS3/Models/Prefabs/ScavengersPack.prefab");
            if (Main.debugMode == true)
            {
                pickupModelPrefab.GetComponentInChildren<Renderer>().gameObject.AddComponent<MaterialControllerComponents.HGControllerFinder>();
            }
            return pickupModelPrefab;
        }
        public static Sprite LoadSprite()
        {
            Sprite pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/VFXPASS3/Icons/ScavengersPack.png");
            return pickupIconSprite;
        }
        public static Sprite LoadBuffSprite()
        {
            Sprite pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/VFXPASS3/Icons/Buff_ScavengersPack.png");
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

            item.tags = new ItemTag[]{ ItemTag.Utility, ItemTag.CannotDuplicate };
            item.deprecatedTier = ItemTier.Tier2;
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

            item.tags = new ItemTag[] { ItemTag.CannotCopy, ItemTag.CannotDuplicate, ItemTag.AIBlacklist, ItemTag.BrotherBlacklist }; // Need to make sure the item can't be given or cloned
            item.deprecatedTier = ItemTier.NoTier;
            item.canRemove = false;
            item.hidden = false;

            item.pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/VFXPASS3/Models/Prefabs/ScavengersPack.prefab");
            item.pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/VFXPASS3/Icons/ScavengersPackConsumed.png");

            return item;
        }
        // Use tracking needs to be a hidden item in order to persist through Dio's and stages
        public static ItemDef CreateHiddenItem()
        {
            ItemDef item = ScriptableObject.CreateInstance<ItemDef>();

            item.name = itemName + "Hidden";
            item.nameToken = "H3_" + upperName + "HIDDEN_NAME";
            item.pickupToken = "H3_" + upperName + "HIDDEN_NAME";
            item.descriptionToken = "H3_" + upperName + "HIDDEN_NAME";

            item.tags = new ItemTag[] { ItemTag.CannotCopy, ItemTag.CannotDuplicate, ItemTag.AIBlacklist, ItemTag.BrotherBlacklist }; // Need to make sure the item can't be given or cloned
            item.deprecatedTier = ItemTier.NoTier;
            item.canRemove = true;
            item.hidden = true;

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
                        localPos = new Vector3(0.21495F, -0.02356F, -0.01167F),
                        localAngles = new Vector3(0F, 0F, 165.4815F),
                        localScale = new Vector3(0.14233F, 0.14233F, 0.14233F)
                    }
                }
            );
            rules.Add("mdlHuntress", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(0.14502F, 0.18348F, -0.10846F),
                        localAngles = new Vector3(0F, 213.9222F, 0F),
                        localScale = new Vector3(0.19746F, 0.19746F, 0.19746F)
                    }
                }
            );
            rules.Add("mdlToolbot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(0.10988F, -0.43475F, -2.2333F),
                        localAngles = new Vector3(0F, 270.8051F, 0F),
                        localScale = new Vector3(2.31606F, 2.31606F, 2.31606F)
                    }
                }
            );
            rules.Add("mdlEngi", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(0.00468F, 0.03157F, -0.31797F),
                        localAngles = new Vector3(0F, 268.7704F, 0F),
                        localScale = new Vector3(0.33998F, 0.33998F, 0.33998F)
                    }
                }
            );
            rules.Add("mdlMage", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "LowerArmR",
                        localPos = new Vector3(-0.10315F, 0.18605F, 0.06003F),
                        localAngles = new Vector3(0.53043F, 211.9651F, 180.331F),
                        localScale = new Vector3(0.15227F, 0.15227F, 0.15227F)
                    }
                }
            );
            rules.Add("mdlMerc", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.00058F, 0.20754F, 0.12777F),
                        localAngles = new Vector3(359.8385F, 269.5961F, 180F),
                        localScale = new Vector3(0.23683F, 0.23683F, 0.23683F)
                    }
                }
            );
            rules.Add("mdlTreebot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "PlatformBase",
                        localPos = new Vector3(0.31701F, 0.10658F, -0.68847F),
                        localAngles = new Vector3(0F, 230.1804F, 341.9065F),
                        localScale = new Vector3(0.50244F, 0.50244F, 0.50244F)
                    }
                }
            );
            rules.Add("mdlLoader", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(0.00591F, 0.07302F, -0.38484F),
                        localAngles = new Vector3(0F, 269.1644F, 0F),
                        localScale = new Vector3(0.32994F, 0.32994F, 0.32994F)
                    }
                }
            );
            rules.Add("mdlCroco", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "SpineChest2",
                        localPos = new Vector3(-0.0511F, 1.01677F, 1.28564F),
                        localAngles = new Vector3(358.8412F, 91.6448F, 253.0454F),
                        localScale = new Vector3(2.00874F, 2.00874F, 2.0166F)
                    }
                }
            );
            rules.Add("mdlCaptain", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(0.00019F, -0.00002F, -0.28779F),
                        localAngles = new Vector3(0F, 269.9389F, 0F),
                        localScale = new Vector3(0.35404F, 0.35404F, 0.35404F)
                    }
                }
            );
            rules.Add("mdlBandit2", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.23395F, 0.02212F, 0F),
                        localAngles = new Vector3(12.33505F, 357.6442F, 169.0994F),
                        localScale = new Vector3(0.14804F, 0.14804F, 0.14804F)
                    }
                }
            );
            rules.Add("EngiTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(0.76116F, -0.00008F, -0.90052F),
                        localAngles = new Vector3(3.09926F, 191.9193F, 24.39832F),
                        localScale = new Vector3(0.67443F, 0.67443F, 0.67443F)
                    }
                }
            );
            rules.Add("EngiWalkerTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(-0.00089F, 0.06048F, -1.26502F),
                        localAngles = new Vector3(0F, 271.9116F, 23.82345F),
                        localScale = new Vector3(0.99339F, 0.99339F, 0.99339F)
                    }
                }
            );
            rules.Add("mdlScav", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Backpack",
                        localPos = new Vector3(5.56247F, 4.47193F, -3.63742F),
                        localAngles = new Vector3(0F, 242.0367F, 0F),
                        localScale = new Vector3(4.85416F, 4.85416F, 4.85416F)
                    }
                }
            );
            rules.Add("mdlRailGunner", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Backpack",
                        localPos = new Vector3(-0.00002F, -0.04411F, -0.18043F),
                        localAngles = new Vector3(0F, 271.5774F, 0F),
                        localScale = new Vector3(0.36282F, 0.36282F, 0.36282F)
                    }
                }
            );
            rules.Add("mdlVoidSurvivor", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(-0.00316F, -0.04828F, -0.21829F),
                        localAngles = new Vector3(0F, 271.446F, 4.96136F),
                        localScale = new Vector3(0.30417F, 0.30417F, 0.30417F)
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
            LanguageAPI.Add("H3_" + upperName + "_NAME", "Scavenger's Pouch");
            LanguageAPI.Add("H3_" + upperName + "_LORE", "========================================\n" +
            "====   MyBabel Machine Translator   ====\n" +
            "====     [Version 12.45.1.010 ]   ======\n" +
            "========================================\n" +
            "Training… <100000000 cycles>\n" +
            "Training… <100000000 cycles>\n" +
            "Training... <100000000 cycles>\n" +
            "Training... <102515 cycles>\n" +
            "Complete!\n" +
            "Display result? Y/N\n" +
            "Y\n" +
            "========================================\n\n" +
            "I love my stuff.\n\n" +
            "I don't have many friends. Other Scavengers think I'm too small and that I don't have enough things. Can't they see that it's not about how many things you have, but how valuable they are?\n\n" +
            "I only have three things. I guess I'm a little picky.\n\n" +
            "One of them is a cool blob I found. It makes me feel strong even when times are looking tough.\n\n" +
            "One of them is a yellow box with wires sticking out. I can plug it into things and watch them get ten times more powerful.\n\n" +
            "The last is a helmet with a light on it. I don't know why, but it makes me feel rich even though I don't have much.\n\n" +
            "I bet those other Scavengers couldn't find anything half as good as my stuff!"
            );

            LanguageAPI.Add("H3_" + upperName + "CONSUMED_NAME", "Empty Pouch");
            LanguageAPI.Add("H3_" + upperName + "CONSUMED_PICKUP", "No longer useful.");
            LanguageAPI.Add("H3_" + upperName + "CONSUMED_DESC", "No longer useful.");
        }
        public static void UpdateItemStatus()
        {
            if (!ScavengersPack_Enable.Value)
            {
                LanguageAPI.AddOverlay("H3_" + upperName + "_NAME", "Scavenger's Pouch" + " <style=cDeath>[DISABLED]</style>");
            }
            else
            {
                LanguageAPI.AddOverlay("H3_" + upperName + "_NAME", "Scavenger's Pouch");
            }
            LanguageAPI.AddOverlay("H3_" + upperName + "_PICKUP", "When an item is consumed, replace it with a brand new one. Occurs up to " + ScavengersPack_Uses.Value + " times.");
            LanguageAPI.AddOverlay("H3_" + upperName + "_DESC", String.Format("When an item is <style=cStack>consumed or broken</style>, <style=cIsUtility>replace it</style> with a brand new one. This may occur up to <style=cIsUtility>{0}</style> times before the pouch is empty.", ScavengersPack_Uses.Value));
        }

        private static void AddHooks()
        {
            int notificationsToClear = 0;

            var itemPairs = new Dictionary<string, string> // Consumed item - Consumable item
            {
                { "RegeneratingScrapConsumed", "RegeneratingScrap" }, // Vanilla
                { "HealingPotionConsumed", "HealingPotion" },
                { "FragileDamagebonusConsumed", "FragileDamageBonus" },
                { "ExtraLifeConsumed", "ExtraLife" },
                { "ExtraLifeVoidConsumed", "ExtraLifeVoid" },
                { "FourHundredTicketsConsumed", "FourHundredTickets" }, // Hex3Mod
                { "OneTicketConsumed", "OneTicket" },
                { "TreasureCacheConsumed", "TreasureCache" }, // WolfoQOL
                { "TreasureCacheVoidConsumed", "TreasureCacheVoid" },
                { "MysticsItems_KeepShopTerminalOpenConsumed", "MysticsItems_KeepShopTerminalOpen" }, // MysticsItems
                { "MysticsItems_LimitedArmorBroken", "MysticsItems_LimitedArmor" },
                { "ITEM_BROKEN_MESS", "ITEM_CLOCKWORK_ITEM" }, // VanillaVoid
                { "ITEM_EMPTY_VIALS", "ITEM_EHANCE_VIALS_ITEM" },
                { "HCFB_ITEM_BROKEN_CHOPSTICKS", "HCFB_ITEM_CHOPSTICKS" }, // Fork Is Back
                { "ITEM_SINGULARITYCONSUMED", "ITEM_SINGULARITY" }, // Spikestrip
                { "ITEM_CARTRIDGECONSUMED", "ITEM_AbyssalMedkit" }
            };

            void ValidateKeys()
            {
                // Dynamically change the dict depending on config values
                if (!ScavengersPack_RegenScrap.Value) { itemPairs.Remove("RegeneratingScrapConsumed"); }
                else if (!itemPairs.ContainsKey("RegeneratingScrapConsumed")) { itemPairs.Add("RegeneratingScrapConsumed", "RegeneratingScrap"); }
                if (!ScavengersPack_PowerElixir.Value) { itemPairs.Remove("HealingPotionConsumed"); }
                else if (!itemPairs.ContainsKey("HealingPotionConsumed")) { itemPairs.Add("HealingPotionConsumed", "HealingPotion"); }
                if (!ScavengersPack_DelicateWatch.Value) { itemPairs.Remove("FragileDamagebonusConsumed"); }
                else if (!itemPairs.ContainsKey("FragileDamagebonusConsumed")) { itemPairs.Add("FragileDamagebonusConsumed", "FragileDamageBonus"); }
                if (!ScavengersPack_Dios.Value) { itemPairs.Remove("ExtraLifeConsumed"); }
                else if (!itemPairs.ContainsKey("ExtraLifeConsumed")) { itemPairs.Add("ExtraLifeConsumed", "ExtraLife"); }
                if (!ScavengersPack_VoidDios.Value) { itemPairs.Remove("ExtraLifeVoidConsumed"); }
                else if (!itemPairs.ContainsKey("ExtraLifeVoidConsumed")) { itemPairs.Add("ExtraLifeVoidConsumed", "ExtraLifeVoid"); }
                if (!ScavengersPack_RustedKey.Value) { itemPairs.Remove("TreasureCacheConsumed"); }
                else if (!itemPairs.ContainsKey("TreasureCacheConsumed")) { itemPairs.Add("TreasureCacheConsumed", "TreasureCache"); }
                if (!ScavengersPack_EncrustedKey.Value) { itemPairs.Remove("TreasureCacheVoidConsumed"); }
                else if (!itemPairs.ContainsKey("TreasureCacheVoidConsumed")) { itemPairs.Add("TreasureCacheVoidConsumed", "TreasureCacheVoid"); }
                if (!ScavengersPack_FourHundredTickets.Value) { itemPairs.Remove("FourHundredTicketsConsumed"); }
                else if (!itemPairs.ContainsKey("FourHundredTicketsConsumed")) { itemPairs.Add("FourHundredTicketsConsumed", "FourHundredTickets"); }
                if (!ScavengersPack_OneTicket.Value) { itemPairs.Remove("OneTicket"); }
                else if (!itemPairs.ContainsKey("OneTicketConsumed")) { itemPairs.Add("OneTicketConsumed", "OneTicket"); }
                if (!ScavengersPack_ShopCard.Value) { itemPairs.Remove("MysticsItems_KeepShopTerminalOpenConsumed"); }
                else if (!itemPairs.ContainsKey("MysticsItems_KeepShopTerminalOpenConsumed")) { itemPairs.Add("MysticsItems_KeepShopTerminalOpenConsumed", "MysticsItems_KeepShopTerminalOpen"); }
                if (!ScavengersPack_CuteBow.Value) { itemPairs.Remove("MysticsItems_LimitedArmorBroken"); }
                else if (!itemPairs.ContainsKey("MysticsItems_LimitedArmorBroken")) { itemPairs.Add("MysticsItems_LimitedArmorBroken", "MysticsItems_LimitedArmor"); }
                if (!ScavengersPack_ClockworkMechanism.Value) { itemPairs.Remove("ITEM_BROKEN_MESS"); }
                else if (!itemPairs.ContainsKey("ITEM_BROKEN_MESS")) { itemPairs.Add("ITEM_BROKEN_MESS", "ITEM_CLOCKWORK_ITEM"); }
                if (!ScavengersPack_Vials.Value) { itemPairs.Remove("ITEM_EMPTY_VIALS"); }
                else if (!itemPairs.ContainsKey("ITEM_EMPTY_VIALS")) { itemPairs.Add("ITEM_EMPTY_VIALS", "ITEM_EHANCE_VIALS_ITEM"); }
                if (!ScavengersPack_BrokenChopsticks.Value) { itemPairs.Remove("HCFB_ITEM_BROKEN_CHOPSTICKS"); }
                else if (!itemPairs.ContainsKey("HCFB_ITEM_BROKEN_CHOPSTICKS")) { itemPairs.Add("HCFB_ITEM_BROKEN_CHOPSTICKS", "HCFB_ITEM_CHOPSTICKS"); }
                if (!ScavengersPack_AbyssalCartridge.Value) { itemPairs.Remove("ITEM_CARTRIDGECONSUMED"); }
                else if (!itemPairs.ContainsKey("ITEM_CARTRIDGECONSUMED")) { itemPairs.Add("ITEM_CARTRIDGECONSUMED", "ITEM_AbyssalMedkit"); }
                if (!ScavengersPack_Singularity.Value) { itemPairs.Remove("ITEM_SINGULARITYCONSUMED"); }
                else if (!itemPairs.ContainsKey("ITEM_SINGULARITYCONSUMED")) { itemPairs.Add("ITEM_SINGULARITYCONSUMED", "ITEM_SINGULARITY"); }
            }

            void Inventory_RpcItemAdded(On.RoR2.Inventory.orig_RpcItemAdded orig, Inventory self, ItemIndex itemIndex)
            {
                orig(self, itemIndex);
                ValidateKeys();
                if (self.GetItemCount(itemDef) > 0 && itemPairs.ContainsKey(ItemCatalog.GetItemDef(itemIndex).name)) // Should not call on itself, as it never adds consumed items
                {
                    itemPairs.TryGetValue(ItemCatalog.GetItemDef(itemIndex).name, out string value);
                    if (ItemCatalog.FindItemIndex(value) != ItemIndex.None && self.gameObject.TryGetComponent(out CharacterMaster master) && master.GetBody())
                    {
                        self.RemoveItem(itemIndex); // Restore 1 broken item
                        self.GiveItemString(value);

                        Util.PlaySound(EntityStates.ScavMonster.FindItem.sound, master.GetBody().gameObject); // Play effects
                        EffectData effectData = new EffectData
                        {
                            origin = master.GetBody().transform.position
                        };
                        effectData.SetNetworkedObjectReference(master.GetBody().gameObject);
                        EffectManager.SpawnEffect(HealthComponent.AssetReferences.crowbarImpactEffectPrefab, effectData, true);
                        notificationsToClear++;

                        self.GiveItem(hiddenItemDef);

                        if (self.GetItemCount(hiddenItemDef) >= ScavengersPack_Uses.Value) // Give empty pack if uses over 3, reset hidden items
                        {
                            self.RemoveItem(itemDef);
                            self.RemoveItem(hiddenItemDef, ScavengersPack_Uses.Value);
                            self.GiveItem(consumedItemDef);
                            CharacterMasterNotificationQueue.PushItemTransformNotification(master, itemDef.itemIndex, consumedItemDef.itemIndex, CharacterMasterNotificationQueue.TransformationType.Default);
                        }
                    }
                }

                if (self.gameObject.TryGetComponent(out CharacterMaster master1) && master1.GetBody())
                {
                    if (self.GetItemCount(itemDef) > 0)
                    {
                        master1.GetBody().SetBuffCount(scavengerUses.buffIndex, ScavengersPack_Uses.Value - self.GetItemCount(hiddenItemDef));
                    }
                    else
                    {
                        master1.GetBody().SetBuffCount(scavengerUses.buffIndex, 0);
                    }
                }
            }

            // Prevent consumable notifications
            void CharacterMasterNotificationQueue_PushItemTransformNotification(On.RoR2.CharacterMasterNotificationQueue.orig_PushItemTransformNotification orig, CharacterMaster characterMaster, ItemIndex oldIndex, ItemIndex newIndex, CharacterMasterNotificationQueue.TransformationType transformationType)
            {
                if (notificationsToClear > 0 && transformationType == CharacterMasterNotificationQueue.TransformationType.Default && itemPairs.ContainsKey(ItemCatalog.GetItemDef(newIndex).name))
                {
                    notificationsToClear--;
                    return;
                }
                orig(characterMaster, oldIndex, newIndex, transformationType);
            }

            On.RoR2.CharacterMasterNotificationQueue.PushItemTransformNotification += CharacterMasterNotificationQueue_PushItemTransformNotification;
            On.RoR2.Inventory.RpcItemAdded += Inventory_RpcItemAdded;
        }

        public static BuffDef scavengerUses { get; private set; }
        public static void AddBuffs() // Shows how many uses are left in a pack
        {
            scavengerUses = ScriptableObject.CreateInstance<BuffDef>();
            scavengerUses.buffColor = new Color(0.855f, 0.761f, 0.529f);
            scavengerUses.canStack = true;
            scavengerUses.isDebuff = false;
            scavengerUses.name = "Scavenger's Pouch Uses Left";
            scavengerUses.isHidden = false;
            scavengerUses.isCooldown = false;
            scavengerUses.iconSprite = LoadBuffSprite();
            ContentAddition.AddBuffDef(scavengerUses);
        }

        public static void Initiate()
        {
            itemDef = CreateItem();
            consumedItemDef = CreateConsumedItem();
            hiddenItemDef = CreateHiddenItem();
            ItemAPI.Add(new CustomItem(itemDef, CreateDisplayRules()));
            ItemAPI.Add(new CustomItem(consumedItemDef, CreateHiddenDisplayRules()));
            ItemAPI.Add(new CustomItem(hiddenItemDef, CreateHiddenDisplayRules()));
            AddBuffs();
            AddTokens();
            UpdateItemStatus();
            AddHooks();
        }
    }
}
