using R2API;
using RoR2;
using System;
using UnityEngine;
using Hex3Mod.HelperClasses;
using Hex3Mod.Logging;
using System.Collections.Generic;

namespace Hex3Mod.Items
{
    /*
    Rewrite of Scavenger's Pack that tightens its functionality. It was seriously bloated...
    */
    public class ScavengersPack
    {
        static string itemName = "ScavengersPack";
        static string upperName = itemName.ToUpper();
        static ItemDef itemDefinition = CreateItem();
        public static ItemDef consumedItemDefinition = CreateConsumedItem();
        public static ItemDef hiddenItemDefinition = CreateHiddenItem();
        public static GameObject LoadPrefab()
        {
            GameObject pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/ScavengersPackPrefab.prefab");
            return pickupModelPrefab;
        }
        public static Sprite LoadSprite()
        {
            Sprite pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/ScavengersPack.png");
            return pickupIconSprite;
        }
        public static Sprite LoadBuffSprite()
        {
            Sprite pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/ScavengersPack.png");
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

            item.pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/ScavengersPackPrefab.prefab");
            item.pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/ScavengersPackConsumed.png");

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

        public static void AddTokens(int ScavengersPack_Uses)
        {
            LanguageAPI.Add("H3_" + upperName + "_NAME", "Scavenger's Pack");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "When an item is consumed, replace it with a brand new one. Occurs up to " + ScavengersPack_Uses + " times.");
            LanguageAPI.Add("H3_" + upperName + "_DESC", String.Format("When an item is <style=cStack>consumed or broken</style>, <style=cIsUtility>replace it</style> with a brand new one. This may occur up to <style=cIsUtility>{0}</style> times before the pack is empty.", ScavengersPack_Uses));
            LanguageAPI.Add("H3_" + upperName + "_LORE", "\"What are these?\"" +
            "\n\n\"I think they're... Eclipse sneakers?\"" +
            "\n\nAlex and Jolene had just cleaned up following their daily slaughter of local wildlife. The more keen-eyed of the two had discovered something lying on the ground beside one of the bodies, and insisted that Jolene (the stronger of the two) take it back to their ship. What she had found turned out to be a small pouch full of knick-knacks and artifacts, all with seemingly no purpose." +
            "\n\n\"Maybe he was a collector. Those things go for thousands, you know?\"" +
            "\n\n\"But there's also a teddy bear in here... and-\" Alex slid her railgun aside, allowing more space on the table to pour out the satchel's contents. \"A bag of mushrooms, couple of drumsticks, a credit card, some feathers-\"" +
            "\n\nAlex felt a foreign poking at her arm, and she nearly jumped out of her seat. Jolene chuckled and revealed the device. \"Lookit these, little grabby arms!\"" +
            "\n\n\"I'd prefer you keep those in the bag. Also, don't drink this, please.\" She pointed at a flask full of strange, dark liquid. Jolene was taken aback." +
            "\n\n\"You really think I'd drink that?\"" +
            "\n\n\"Yes.\"" +
            "\n\n\"I mean... what does it smell like?\"" +
            "\n\nAlex continued her search until she was interrupted by an unusual feeling. Something at the very back of the bag was beckoning her-- beckoning her inside. First, she inserted an arm, and then the other. As if possessed, she crept over the table, her head now entering the mouth of the bag which had so kindly invited her." +
            "\n\nJolene grimaced at the tonic's strong taste, but soon noticed that something else was off. Namely that her friend had vanished." +
            "\n\n\"Alex?\""
            );

            LanguageAPI.Add("H3_" + upperName + "CONSUMED_NAME", "Empty Pack");
            LanguageAPI.Add("H3_" + upperName + "CONSUMED_PICKUP", "No longer useful.");
            LanguageAPI.Add("H3_" + upperName + "CONSUMED_DESC", "No longer useful.");
        }

        private static void AddHooks(ItemDef itemDef, ItemDef consumeditemDef, ItemDef hiddenItemDef, int ScavengersPack_Uses, bool ScavengersPack_PowerElixir, bool ScavengersPack_DelicateWatch, bool ScavengersPack_Dios, bool ScavengersPack_VoidDios, bool ScavengersPack_RustedKey, bool ScavengersPack_EncrustedKey, bool ScavengersPack_FourHundredTickets, bool ScavengersPack_OneTicket, bool ScavengersPack_ShopCard, bool ScavengersPack_CuteBow, bool ScavengersPack_ClockworkMechanism, bool ScavengersPack_Vials, bool ScavengersPack_BrokenChopsticks, bool ScavengersPack_AbyssalCartridge, bool ScavengersPack_Singularity)
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

            // Remove dictionary keys based on config values
            if (!ScavengersPack_PowerElixir) { itemPairs.Remove("HealingPotionConsumed"); }
            if (!ScavengersPack_DelicateWatch) { itemPairs.Remove("FragileDamagebonusConsumed"); }
            if (!ScavengersPack_Dios) { itemPairs.Remove("ExtraLifeConsumed"); }
            if (!ScavengersPack_VoidDios) { itemPairs.Remove("ExtraLifeVoidConsumed"); }
            if (!ScavengersPack_RustedKey) { itemPairs.Remove("TreasureCacheConsumed"); }
            if (!ScavengersPack_EncrustedKey) { itemPairs.Remove("TreasureCacheVoidConsumed"); }
            if (!ScavengersPack_FourHundredTickets) { itemPairs.Remove("FourHundredTicketsConsumed"); }
            if (!ScavengersPack_OneTicket) { itemPairs.Remove("OneTicketConsumed"); }
            if (!ScavengersPack_ShopCard) { itemPairs.Remove("MysticsItems_KeepShopTerminalOpenConsumed"); }
            if (!ScavengersPack_CuteBow) { itemPairs.Remove("MysticsItems_LimitedArmorBroken"); }
            if (!ScavengersPack_ClockworkMechanism) { itemPairs.Remove("ITEM_BROKEN_MESS"); }
            if (!ScavengersPack_Vials) { itemPairs.Remove("ITEM_EMPTY_VIALS"); }
            if (!ScavengersPack_BrokenChopsticks) { itemPairs.Remove("HCFB_ITEM_BROKEN_CHOPSTICKS"); }
            if (!ScavengersPack_AbyssalCartridge) { itemPairs.Remove("ITEM_CARTRIDGECONSUMED"); }
            if (!ScavengersPack_Singularity) { itemPairs.Remove("ITEM_SINGULARITYCONSUMED"); }

            void Inventory_RpcItemAdded(On.RoR2.Inventory.orig_RpcItemAdded orig, Inventory self, ItemIndex itemIndex)
            {
                orig(self, itemIndex);

                if (self.GetItemCount(itemDef) > 0 && itemPairs.ContainsKey(ItemCatalog.GetItemDef(itemIndex).name)) // Should not call on itself, as it never adds consumed items
                {
                    itemPairs.TryGetValue(ItemCatalog.GetItemDef(itemIndex).name, out string value);
                    if (ItemCatalog.FindItemIndex(value) != ItemIndex.None && self.gameObject.TryGetComponent(out CharacterMaster master))
                    {
                        self.RemoveItem(itemIndex); // Restore 1 broken item
                        self.GiveItemString(value);

                        Util.PlaySound(EntityStates.ScavMonster.FindItem.sound, self.gameObject); // Play effects
                        EffectData effectData = new EffectData
                        {
                            origin = self.transform.position
                        };
                        effectData.SetNetworkedObjectReference(self.gameObject);
                        EffectManager.SpawnEffect(HealthComponent.AssetReferences.crowbarImpactEffectPrefab, effectData, true);
                        notificationsToClear++;

                        self.GiveItem(hiddenItemDef);

                        if (self.GetItemCount(hiddenItemDef) >= ScavengersPack_Uses) // Give empty pack if uses over 3, reset hidden items
                        {
                            self.RemoveItem(itemDef);
                            self.RemoveItem(hiddenItemDef, ScavengersPack_Uses);
                            self.GiveItem(consumeditemDef);
                            CharacterMasterNotificationQueue.PushItemTransformNotification(master, itemDef.itemIndex, consumeditemDef.itemIndex, CharacterMasterNotificationQueue.TransformationType.Default);
                        }
                    }
                }

                if (self.gameObject.TryGetComponent(out CharacterMaster master1) && master1.GetBody())
                {
                    if (self.GetItemCount(itemDef) > 0)
                    {
                        master1.GetBody().SetBuffCount(scavengerUses.buffIndex, ScavengersPack_Uses - self.GetItemCount(hiddenItemDef));
                    }
                    else
                    {
                        master1.GetBody().RemoveBuff(scavengerUses.buffIndex);
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
            scavengerUses.buffColor = new Color(1f, 1f, 1f);
            scavengerUses.canStack = true;
            scavengerUses.isDebuff = false;
            scavengerUses.name = "Scavenger's Pack Uses Left";
            scavengerUses.isHidden = false;
            scavengerUses.isCooldown = false;
            scavengerUses.iconSprite = LoadBuffSprite();
            ContentAddition.AddBuffDef(scavengerUses);
        }

        public static void Initiate(int ScavengersPack_Uses, bool ScavengersPack_PowerElixir, bool ScavengersPack_DelicateWatch, bool ScavengersPack_Dios, bool ScavengersPack_VoidDios, bool ScavengersPack_RustedKey, bool ScavengersPack_EncrustedKey, bool ScavengersPack_FourHundredTickets, bool ScavengersPack_OneTicket, bool ScavengersPack_ShopCard, bool ScavengersPack_CuteBow, bool ScavengersPack_ClockworkMechanism, bool ScavengersPack_Vials, bool ScavengersPack_BrokenChopsticks, bool ScavengersPack_AbyssalCartridge, bool ScavengersPack_Singularity)
        {
            ItemAPI.Add(new CustomItem(itemDefinition, CreateDisplayRules()));
            ItemAPI.Add(new CustomItem(consumedItemDefinition, CreateHiddenDisplayRules()));
            ItemAPI.Add(new CustomItem(hiddenItemDefinition, CreateHiddenDisplayRules()));
            AddBuffs();
            AddTokens(ScavengersPack_Uses);
            AddHooks(itemDefinition, consumedItemDefinition, hiddenItemDefinition, ScavengersPack_Uses, ScavengersPack_PowerElixir, ScavengersPack_DelicateWatch, ScavengersPack_Dios, ScavengersPack_VoidDios, ScavengersPack_RustedKey, ScavengersPack_EncrustedKey, ScavengersPack_FourHundredTickets, ScavengersPack_OneTicket, ScavengersPack_ShopCard, ScavengersPack_CuteBow, ScavengersPack_ClockworkMechanism, ScavengersPack_Vials, ScavengersPack_BrokenChopsticks, ScavengersPack_AbyssalCartridge, ScavengersPack_Singularity);
        }
    }
}
