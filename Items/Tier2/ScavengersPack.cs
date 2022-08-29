using R2API;
using RoR2;
using System;
using UnityEngine;
using Hex3Mod.HelperClasses;

namespace Hex3Mod.Items
{
    /*
    The Scavenger's Pack is a valuable item, but it's not as simple to use as it seems. Getting 400 Tickets while having a scav pack means you MUST use all 3 charges on chests
    Therefore, the player must keep track of their inventory and determine what they want to use these charges on-- elixir for fragile items? regen scrap? tickets? dio's?
    All of the possible synergies are quite powerful, so this should be a fun item regardless of what you use it on
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
            item.canRemove = true;
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
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Prevent consumable items and equipment from being consumed up to 3 times.");
            LanguageAPI.Add("H3_" + upperName + "_DESC", String.Format("<style=cIsUtility>Prevents</style> your consumable items and equipment from being consumed up to <style=cIsUtility>{0}</style> times before breaking. <style=cDeath>Fragile</style> items are unaffected.", ScavengersPack_Uses));
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
            "\n\nJolene grimaced at the tonic's strong taste, but soon noticed that something else was off. Namely that her friend was gone." +
            "\n\n\"Alex?\""
            );

            LanguageAPI.Add("H3_" + upperName + "CONSUMED_NAME", "Empty Pack");
            LanguageAPI.Add("H3_" + upperName + "CONSUMED_PICKUP", "No longer useful.");
            LanguageAPI.Add("H3_" + upperName + "CONSUMED_DESC", "No longer useful.");
        }

        private static void AddHooks(ItemDef itemDef, ItemDef consumeditemDef, int ScavengersPack_Uses, bool ScavengersPack_RegenerativeScrap, bool Mystic1, bool Mystic2, bool tricorn, bool terminal, bool elixir, bool tickets, bool dios, bool larva) // Insert hooks here
        {
            // Add or modify our item behavior when the character's inventory changes
            On.RoR2.CharacterBody.OnInventoryChanged += (orig, self) =>
            {
                orig(self); // Enabling/disabling and stack behaviors/timer
                if (self.inventory && self.inventory.GetItemCount(itemDef) > 0)
                {
                    if (!self.GetComponent<ScavengerPackBehavior>())
                    {
                        self.AddItemBehavior<ScavengerPackBehavior>(self.inventory.GetItemCount(itemDef));
                        self.GetComponent<ScavengerPackBehavior>().stack = self.inventory.GetItemCount(itemDef);
                        if (MysticsCompatibility.enabled && Mystic2 == true)
                        {
                            self.GetComponent<ScavengerPackBehavior>().platCardEnabled = true;
                        }
                    }
                    if (self.GetComponent<ScavengerPackBehavior>())
                    {
                        self.GetComponent<ScavengerPackBehavior>().stack = self.inventory.GetItemCount(itemDef);
                        if (MysticsCompatibility.enabled && self.inventory.GetItemCount(itemDef) > 0 && Mystic2 == true)
                        {
                            if (self.inventory.GetItemCount(MysticsCompatibility.ShopTerminalCardConsumed) > 0)
                            {
                                self.inventory.RemoveItem(MysticsCompatibility.ShopTerminalCardConsumed, self.inventory.GetItemCount(MysticsCompatibility.ShopTerminalCardConsumed));
                            }
                        }
                    }
                }
                // Regenerating Scrap
                if (self.inventory && self.inventory.GetItemCount(itemDef) > 0 && self.inventory.GetItemCount(DLC1Content.Items.RegeneratingScrapConsumed) > 0 && self.master && ScavengersPack_RegenerativeScrap == true)
                {
                    self.master.TryRegenerateScrap();
                    self.inventory.GiveItem(hiddenItemDefinition);
                    TryBreakPack(self, self.inventory);
                    Util.PlaySound(EntityStates.ScavMonster.FindItem.sound, self.gameObject);
                }
                if (self.inventory && self.inventory.GetItemCount(itemDef) <= 0 && self.GetComponent<ScavengerPackBehavior>())
                {
                    self.GetComponent<ScavengerPackBehavior>().enable = false;
                    self.inventory.RemoveItem(hiddenItemDefinition, self.inventory.GetItemCount(hiddenItemDefinition));
                }
            };

            // Method to remove uses and break the scav pack
            void TryBreakPack(CharacterBody characterBody, Inventory inventory)
            {
                EffectData effectData = new EffectData
                {
                    origin = characterBody.transform.position
                };
                effectData.SetNetworkedObjectReference(characterBody.gameObject);
                EffectManager.SpawnEffect(HealthComponent.AssetReferences.crowbarImpactEffectPrefab, effectData, true);

                if (inventory.GetItemCount(itemDef) > 0 && inventory.GetItemCount(hiddenItemDefinition) >= ScavengersPack_Uses && characterBody.master)
                {
                    inventory.RemoveItem(itemDef);
                    inventory.GiveItem(consumeditemDef);
                    inventory.RemoveItem(hiddenItemDefinition, inventory.GetItemCount(hiddenItemDefinition));
                    CharacterMasterNotificationQueue.SendTransformNotification(characterBody.master, itemDef.itemIndex, consumeditemDef.itemIndex, CharacterMasterNotificationQueue.TransformationType.Default);
                }
            }

            // Whenever a character transform notification is sent, compare its constituent items and reverse the transformation if necessary
            On.RoR2.CharacterMasterNotificationQueue.SendTransformNotification_CharacterMaster_ItemIndex_ItemIndex_TransformationType += (orig, master, removedItem, gainedItem, transformationType) =>
            {
                bool scavPackUsed = false;
                if (master.GetBody() && master.GetBody().inventory && master.GetBody().inventory.GetItemCount(itemDef) > 0)
                {
                    CharacterBody body = master.GetBody();
                    Inventory inventory = master.GetBody().inventory;

                    // Tickets
                    if (ItemCatalog.GetItemDef(removedItem).name == "FourHundredTickets" && ItemCatalog.GetItemDef(gainedItem).name == "FourHundredTicketsConsumed" && tickets == true)
                    {
                        inventory.GiveItem(hiddenItemDefinition);
                        inventory.GiveItem(Tickets.itemDefinition);
                        inventory.RemoveItem(Tickets.consumedItemDefinition);
                        TryBreakPack(body, inventory);
                        Util.PlaySound(EntityStates.ScavMonster.FindItem.sound, body.gameObject);
                        scavPackUsed = true;
                    }
                    // Power Elixir
                    if (removedItem == DLC1Content.Items.HealingPotion.itemIndex && gainedItem == DLC1Content.Items.HealingPotionConsumed.itemIndex && elixir == true)
                    {
                        inventory.GiveItem(hiddenItemDefinition);
                        inventory.GiveItem(DLC1Content.Items.HealingPotion);
                        inventory.RemoveItem(DLC1Content.Items.HealingPotionConsumed);
                        TryBreakPack(body, inventory);
                        Util.PlaySound(EntityStates.ScavMonster.FindItem.sound, body.gameObject);
                        scavPackUsed = true;
                    }
                    // Dio's Best Friend
                    if (removedItem == RoR2Content.Items.ExtraLife.itemIndex && gainedItem == RoR2Content.Items.ExtraLifeConsumed.itemIndex && dios == true)
                    {
                        inventory.GiveItem(hiddenItemDefinition);
                        inventory.GiveItem(RoR2Content.Items.ExtraLife);
                        inventory.RemoveItem(RoR2Content.Items.ExtraLifeConsumed);
                        TryBreakPack(body, inventory);
                        Util.PlaySound(EntityStates.ScavMonster.FindItem.sound, body.gameObject);
                        scavPackUsed = true;
                    }
                    // Pluripotent Larva
                    if (removedItem == DLC1Content.Items.ExtraLifeVoid.itemIndex && gainedItem == DLC1Content.Items.ExtraLifeVoidConsumed.itemIndex && larva == true)
                    {
                        inventory.GiveItem(hiddenItemDefinition);
                        inventory.GiveItem(DLC1Content.Items.ExtraLifeVoid);
                        inventory.RemoveItem(DLC1Content.Items.ExtraLifeVoidConsumed);
                        TryBreakPack(body, inventory);
                        Util.PlaySound(EntityStates.ScavMonster.FindItem.sound, body.gameObject);
                        scavPackUsed = true;
                    }
                }
                // If no valid transformations are detected, go through with the transformation
                if (scavPackUsed == false)
                {
                    orig(master, removedItem, gainedItem, transformationType);
                }
            };

            // Handles pushed transforms
            On.RoR2.CharacterMasterNotificationQueue.PushItemTransformNotification += (orig, master, removedItem, gainedItem, transformationType) =>
            {
                bool scavPackUsed = false;

                if (master.GetBody() && master.GetBody().inventory && master.GetBody().inventory.GetItemCount(itemDef) > 0)
                {
                    CharacterBody body = master.GetBody();
                    Inventory inventory = master.GetBody().inventory;

                    // Mystics: Cutesy Bow
                    if (ItemCatalog.GetItemDef(removedItem).name == "MysticsItems_LimitedArmor" && ItemCatalog.GetItemDef(gainedItem).name == "MysticsItems_LimitedArmorBroken" && Mystic1 == true)
                    {
                        inventory.GiveItem(hiddenItemDefinition);
                        inventory.GiveItem(removedItem);
                        inventory.RemoveItem(gainedItem);
                        TryBreakPack(body, inventory);
                        Util.PlaySound(EntityStates.ScavMonster.FindItem.sound, body.gameObject);
                        scavPackUsed = true;
                    }
                }
                // If no valid transformations are detected, go through with the transformation
                if (scavPackUsed == false)
                {
                    orig(master, removedItem, gainedItem, transformationType);
                }
            };

            // Handles equipment instead
            On.RoR2.CharacterMasterNotificationQueue.SendTransformNotification_CharacterMaster_EquipmentIndex_EquipmentIndex_TransformationType += (orig, master, removedEquip, gainedEquip, transformationType) =>
            {
                bool scavPackUsed = false;
                if (master.GetBody() && master.GetBody().inventory && master.GetBody().inventory.GetItemCount(itemDef) > 0)
                {
                    CharacterBody body = master.GetBody();
                    Inventory inventory = master.GetBody().inventory;

                    // Trophy Hunter's Tricorn
                    if (removedEquip == DLC1Content.Equipment.BossHunter.equipmentIndex && gainedEquip == DLC1Content.Equipment.BossHunterConsumed.equipmentIndex && tricorn == true)
                    {
                        inventory.GiveItem(hiddenItemDefinition);
                        body.GetComponent<ScavengerPackBehavior>().tricornFlag = true;
                        TryBreakPack(body, inventory);
                        Util.PlaySound(EntityStates.ScavMonster.FindItem.sound, body.gameObject);
                        scavPackUsed = true;
                    }
                }
                // If no valid transformations are detected, go through with the transformation
                if (scavPackUsed == false)
                {
                    orig(master, removedEquip, gainedEquip, transformationType);
                }
            };
            On.RoR2.Inventory.SetEquipmentIndex += (orig, self, equipmentIndex) =>
            {
                if (self && self.gameObject.GetComponent<CharacterMaster>() && tricorn == true)
                {
                    CharacterMaster master = self.gameObject.GetComponent<CharacterMaster>();
                    if (master.GetBody() && master.GetBody().GetComponent<ScavengerPackBehavior>() && master.GetBody().GetComponent<ScavengerPackBehavior>().tricornFlag == true && self.currentEquipmentIndex == DLC1Content.Equipment.BossHunter.equipmentIndex)
                    {
                        master.GetBody().GetComponent<ScavengerPackBehavior>().tricornFlag = false;
                    }
                    else
                    {
                        orig(self, equipmentIndex);
                    }
                }
                else
                {
                    orig(self, equipmentIndex);
                }
            };

            // Command Terminal
            On.RoR2.Inventory.SetEquipment += (orig, self, equipmentState, slot) =>
            {
                var currentEquip = self.currentEquipmentIndex;
                orig(self, equipmentState, slot);
                if (TinkersCompatibility.enabled == true && terminal == true)
                {
                    if (equipmentState.equipmentIndex == EquipmentIndex.None && equipmentState.charges == 0 && EquipmentCatalog.GetEquipmentDef(currentEquip) && EquipmentCatalog.GetEquipmentDef(currentEquip).name == "TKSATReviveOnce" && self.GetItemCount(itemDef) > 0 && self.gameObject.GetComponent<CharacterMaster>() && self.gameObject.GetComponent<CharacterMaster>().GetBody())
                    {
                        CharacterBody body = self.gameObject.GetComponent<CharacterMaster>().GetBody();
                        self.SetEquipmentIndex(currentEquip);
                        self.GiveItem(hiddenItemDefinition);
                        TryBreakPack(body, self);
                        Util.PlaySound(EntityStates.ScavMonster.FindItem.sound, body.gameObject);
                    }
                }
            };

            // Platinum Card
            On.RoR2.Items.MultiShopCardUtils.OnPurchase += (orig, context, moneyCost) =>
            {
                orig(context, moneyCost);
                if (MysticsCompatibility.enabled == true && Mystic2 == true)
                {
                    CharacterMaster activatorMaster = context.activatorMaster;
                    if (activatorMaster && activatorMaster.hasBody && activatorMaster.inventory && activatorMaster.inventory.GetItemCount(MysticsCompatibility.ShopTerminalCard) > 0 && context.purchasedObject)
                    {
                        ShopTerminalBehavior shopTerminalBehavior = context.purchasedObject.GetComponent<ShopTerminalBehavior>();
                        if (shopTerminalBehavior && shopTerminalBehavior.serverMultiShopController && activatorMaster.inventory.GetItemCount(itemDef) > 0 && activatorMaster.GetBody())
                        {
                            activatorMaster.inventory.GiveItem(MysticsCompatibility.ShopTerminalCard);
                            activatorMaster.inventory.RemoveItem(MysticsCompatibility.ShopTerminalCardConsumed);
                            activatorMaster.inventory.GiveItem(hiddenItemDefinition);
                            TryBreakPack(activatorMaster.GetBody(), activatorMaster.inventory);
                            Util.PlaySound(EntityStates.ScavMonster.FindItem.sound, activatorMaster.GetBodyObject());
                        }
                    }
                }
            };
        }

        public class ScavengerPackBehavior : CharacterBody.ItemBehavior
        {
            public bool tricornFlag = false;
            public bool platCardEnabled = false;
            public bool platCardHandler = false;
            public bool enable = true;

            private void Update()
            {
                if (body && enable == false) // Plat card doesn't play nice with the item exchanges, so extra measures were needed to prevent duping
                {
                    if (MysticsCompatibility.enabled == true && platCardEnabled == true && body.inventory && body.inventory.GetItemCount(MysticsCompatibility.ShopTerminalCard) > 0)
                    {
                        platCardHandler = true;
                    }
                    if (MysticsCompatibility.enabled == true && platCardHandler == true && body.inventory)
                    {
                        body.inventory.RemoveItem(MysticsCompatibility.ShopTerminalCardConsumed);
                        platCardHandler = false;
                    }
                    if (platCardHandler == false)
                    {
                        Destroy(this);
                    }
                }
            }
        }

        public static void Initiate(int ScavengersPack_Uses, bool ScavengersPack_RegenerativeScrap, bool Mystic1, bool Mystic2, bool tricorn, bool terminal, bool elixir, bool tickets, bool dios, bool larva)
        {
            ItemAPI.Add(new CustomItem(itemDefinition, CreateDisplayRules()));
            ItemAPI.Add(new CustomItem(consumedItemDefinition, CreateHiddenDisplayRules()));
            ItemAPI.Add(new CustomItem(hiddenItemDefinition, CreateHiddenDisplayRules()));
            AddTokens(ScavengersPack_Uses);
            AddHooks(itemDefinition, consumedItemDefinition, ScavengersPack_Uses, ScavengersPack_RegenerativeScrap, Mystic1, Mystic2, tricorn, terminal, elixir, tickets, dios, larva);
        }
    }
}
