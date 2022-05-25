using BepInEx;
using BepInEx.Configuration;
using R2API;
using R2API.Utils;
using RoR2;
using RoR2.ExpansionManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Hex3Mod;
using Hex3Mod.Logging;
using Hex3Mod.HelperClasses;

namespace Hex3Mod.Items
{
    /*
    The Corrupting Parasite is an item idea thought up by conq and kking. I liked it and was given permission to make it into a full item, so here we are
    It should be a good way to form void builds and pave the way for more void items in the future
    */
    public class CorruptingParasite
    {
        // Create functions here for defining the ITEM, TOKENS, HOOKS and CONFIG. This is simpler than doing it in Main
        static string itemName = "CorruptingParasite"; // Change this name when making a new item
        static string upperName = itemName.ToUpper();
        public static ItemDef itemDefinition = CreateItem();
        public static GameObject LoadPrefab()
        {
            GameObject pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/CorruptingParasitePrefab.prefab");
            return pickupModelPrefab;
        }
        public static Sprite LoadSprite()
        {
            Sprite pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/CorruptingParasite.png");
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

            item.tags = new ItemTag[]{ ItemTag.Utility, ItemTag.AIBlacklist, ItemTag.BrotherBlacklist }; // It would be useless on enemies
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
                        childName = "ThighR",
                        localPos = new Vector3(-0.08455F, -0.01497F, 0.0869F),
                        localAngles = new Vector3(330.4019F, 148.6668F, 168.6579F),
                        localScale = new Vector3(0.31789F, 0.29327F, 0.29788F)
                    }
                }
            );
            rules.Add("mdlHuntress", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.09894F, 0.0666F, 0.14715F),
                        localAngles = new Vector3(286.2381F, 34.64287F, 176.1205F),
                        localScale = new Vector3(0.19941F, 0.19941F, 0.19941F)
                    }
                }
            );
            rules.Add("mdlToolbot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "ThighR",
                        localPos = new Vector3(-0.32609F, 1.50743F, 0.58917F),
                        localAngles = new Vector3(287.1428F, 344.6615F, 146.7554F),
                        localScale = new Vector3(3.07578F, 3.07578F, 3.07578F)
                    }
                }
            );
            rules.Add("mdlEngi", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "CannonHeadL",
                        localPos = new Vector3(0.04581F, 0.02192F, 0.00216F),
                        localAngles = new Vector3(25.12274F, 137.6865F, 142.4892F),
                        localScale = new Vector3(0.38887F, 0.38887F, 0.38887F)
                    }
                }
            );
            rules.Add("mdlMage", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "CalfL",
                        localPos = new Vector3(-0.0374F, 0.24028F, 0.07721F),
                        localAngles = new Vector3(319.5006F, 60.6742F, 83.30266F),
                        localScale = new Vector3(0.31656F, 0.31656F, 0.31656F)
                    }
                }
            );
            rules.Add("mdlMerc", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(-0.113F, 0.21553F, 0.08614F),
                        localAngles = new Vector3(280.9289F, 23.77603F, 136.4629F),
                        localScale = new Vector3(0.36894F, 0.36894F, 0.36894F)
                    }
                }
            );
            rules.Add("mdlTreebot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "PlatformBase",
                        localPos = new Vector3(-0.33058F, 0.22093F, -0.88125F),
                        localAngles = new Vector3(49.63772F, 221.0377F, 37.76935F),
                        localScale = new Vector3(0.83672F, 0.83672F, 0.83672F)
                    }
                }
            );
            rules.Add("mdlLoader", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(-0.13442F, 0.36149F, 0.23423F),
                        localAngles = new Vector3(331.5598F, 180.9432F, 359.8354F),
                        localScale = new Vector3(0.26171F, 0.26171F, 0.26171F)
                    }
                }
            );
            rules.Add("mdlCroco", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "UpperArmL",
                        localPos = new Vector3(-0.57796F, 3.79938F, 0.0451F),
                        localAngles = new Vector3(355.382F, 354.386F, 83.46042F),
                        localScale = new Vector3(3.48469F, 3.48469F, 3.48469F)
                    }
                }
            );
            rules.Add("mdlCaptain", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "ThighL",
                        localPos = new Vector3(0.09961F, 0.39041F, -0.07873F),
                        localAngles = new Vector3(29.06793F, 171.0644F, 91.19601F),
                        localScale = new Vector3(0.39495F, 0.39935F, 0.39495F)
                    }
                }
            );
            rules.Add("mdlBandit2", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Stomach",
                        localPos = new Vector3(0.17586F, 0.14657F, -0.04721F),
                        localAngles = new Vector3(31.56213F, 188.1448F, 85.46602F),
                        localScale = new Vector3(0.25929F, 0.25929F, 0.25929F)
                    }
                }
            );
            rules.Add("EngiTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(-0.60931F, 0.757F, -1.06356F),
                        localAngles = new Vector3(17.09353F, 229.7854F, 359.3452F),
                        localScale = new Vector3(1.04019F, 1.04019F, 1.04019F)
                    }
                }
            );
            rules.Add("EngiWalkerTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(-0.69553F, 1.1678F, -1.17419F),
                        localAngles = new Vector3(30.41575F, 236.0284F, 15.85494F),
                        localScale = new Vector3(0.84911F, 0.84911F, 0.84911F)
                    }
                }
            );
            rules.Add("mdlScav", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Backpack",
                        localPos = new Vector3(-7.20899F, 11.39804F, 1.42232F),
                        localAngles = new Vector3(334.2978F, 178.9926F, 359.245F),
                        localScale = new Vector3(3.81427F, 3.81427F, 3.81427F)
                    }
                }
            );
            rules.Add("mdlRailGunner", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "UpperArmR",
                        localPos = new Vector3(-0.01926F, 0.13436F, 0.02021F),
                        localAngles = new Vector3(9.62257F, 233.6922F, 250.2488F),
                        localScale = new Vector3(0.23791F, 0.23791F, 0.23791F)
                    }
                }
            );
            rules.Add("mdlVoidSurvivor", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(-0.00862F, 0.18012F, -0.15892F),
                        localAngles = new Vector3(84.5425F, 187.6659F, 4.56151F),
                        localScale = new Vector3(0.42825F, 0.42825F, 0.42825F)
                    }
                }
            );

            return rules;
        }

        public static void AddTokens(bool CorruptingParasite_AlternateMode, bool CorruptingParasite_AltModeOnlyConvert)
        {
            LanguageAPI.Add("H3_" + upperName + "_NAME", "Corrupting Parasite");
            if (CorruptingParasite_AlternateMode == true)
            {
                if (CorruptingParasite_AltModeOnlyConvert == true)
                {
                    LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Corrupts one of your items each stage.");
                    LanguageAPI.Add("H3_" + upperName + "_DESC", "<style=cIsVoid>Corrupts</style> 1 <style=cStack>(+1 per stack)</style> item in your inventory into its <style=cIsVoid>void counterpart</style> each stage. Prefers more common items [60/<style=cIsHealing>30</style>/<style=cIsHealth>5</style>/<style=cShrine>5</style>]%.");
                }
                else
                {
                    LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Corrupts one of your items each stage.");
                    LanguageAPI.Add("H3_" + upperName + "_DESC", "<style=cIsVoid>Corrupts</style> 1 <style=cStack>(+1 per stack)</style> item in your inventory into its <style=cIsVoid>void counterpart</style> each stage. Prefers more common items [60/<style=cIsHealing>30</style>/<style=cIsHealth>5</style>/<style=cShrine>5</style>]%. If no items with void conversions are available, an item will turn into a <style=cIsVoid>random void item</style>.");
                }
            }
            else
            {
                LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Corrupts one of your items into a random void item each stage.");
                LanguageAPI.Add("H3_" + upperName + "_DESC", "<style=cIsVoid>Corrupts</style> 1 <style=cStack>(+1 per stack)</style> item in your inventory into a <style=cIsVoid>random void item</style> of the same rarity each stage. Prefers more common items [60/<style=cIsHealing>30</style>/<style=cIsHealth>5</style>/<style=cShrine>5</style>]%.");
            }
            
            LanguageAPI.Add("H3_" + upperName + "_LORE", "Order: Bugs" +
            "\nTracking Number: 977******" +
            "\nEstimated Delivery: Not approved" +
            "\nShipping Method: Priority" +
            "\nShipping Address: Earth, Unspecified address" +
            "\n\nBugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs?" +
            "\n\nBugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs?");
        }

        private static void AddHooks(ItemDef itemDefToHooks, bool CorruptingParasite_CorruptBossItems, bool CorruptingParasite_AlternateMode, bool CorruptingParasite_Replication, bool CorruptingParasite_AltModeOnlyConvert) // Insert hooks here
        {
            System.Random parasiteRarityRng = new System.Random(); // Create the random table for choosing the item tier to corrupt. There are 100 items in the array, so one entry = 1% chance
            int[] parasiteRarityTable = new int[100];

            // 60% chance common, 30% chance uncommon, 5% chance boss and legendary
            // 0 = common, 1 = uncommon, 2 = legendary, 3 = boss
            for (int i = 0; i < parasiteRarityTable.Length; i++)
            {
                if (i < 60)
                {
                    parasiteRarityTable[i] = 0;
                }
                if (i >= 60 & i < 90)
                {
                    parasiteRarityTable[i] = 1;
                }
                if (CorruptingParasite_CorruptBossItems == true)
                {
                    if (i >= 90 & i < 95)
                    {
                        parasiteRarityTable[i] = 2;
                    }
                    if (i >= 95)
                    {
                        parasiteRarityTable[i] = 3;
                    }
                }
                else
                {
                    if (i >= 90)
                    {
                        parasiteRarityTable[i] = 2;
                    }
                }
            }

            int ParasitePickRarity() // Choose an item from the rarity array at random, then return it
            {
                int randomEntry = parasiteRarityRng.Next(parasiteRarityTable.Length);
                return parasiteRarityTable[randomEntry];
            }

            /* 
            Now that we have a rarity picker, we must do two things for each instance of a parasite:
            1 - Check when the stage advances
            2 - Pick an item in your inventory at random
            3 - Pick a void item of the same tier at random
            4 - Remove the inventory item and add the void item
            */

            int GetCorruptibleItemCount(List<ItemDef> altModeDefList, CharacterMaster master) // Create method to set alternate mode flag
            {
                int finalCorruptibles = 0;
                foreach (ItemDef.Pair pair in ItemCatalog.GetItemPairsForRelationship(DLC1Content.ItemRelationshipTypes.ContagiousItem))
                {
                    if (master.inventory.GetItemCount(pair.itemDef1) > 0) // If we have this item in our inventory, AND it's not a void item, add it to the list of possible conversions
                    {
                        if (pair.itemDef1.deprecatedTier != ItemTier.VoidTier1 && pair.itemDef1.deprecatedTier != ItemTier.VoidTier2 && pair.itemDef1.deprecatedTier != ItemTier.VoidTier3 && pair.itemDef1.deprecatedTier != ItemTier.VoidBoss)
                        {
                            altModeDefList.Add(pair.itemDef1);
                            finalCorruptibles += 1;
                        }
                    }
                    if (master.inventory.GetItemCount(pair.itemDef2) > 0)
                    {
                        if (pair.itemDef2.deprecatedTier != ItemTier.VoidTier1 && pair.itemDef2.deprecatedTier != ItemTier.VoidTier2 && pair.itemDef2.deprecatedTier != ItemTier.VoidTier3 && pair.itemDef2.deprecatedTier != ItemTier.VoidBoss)
                        {
                            altModeDefList.Add(pair.itemDef2);
                            finalCorruptibles += 1;
                        }
                    }
                }
                return finalCorruptibles;
            }

            void ParasiteTradeItems(CharacterMaster master)
            {
                int itemCount = master.inventory.GetItemCount(itemDefToHooks);
                if (itemCount > 0)
                {
                    // Get every existing item of the 4 void tiers
                    List<PickupIndex> listVoidTier1 = new List<PickupIndex>(Run.instance.availableVoidTier1DropList);
                    List<PickupIndex> listVoidTier2 = new List<PickupIndex>(Run.instance.availableVoidTier2DropList);
                    List<PickupIndex> listVoidTier3 = new List<PickupIndex>(Run.instance.availableVoidTier3DropList);
                    List<PickupIndex> listVoidTierBoss = new List<PickupIndex>(Run.instance.availableVoidBossDropList);
                    // Get list of items in the player's inventory
                    List<ItemIndex> listPlayerItems = new List<ItemIndex>(master.inventory.itemAcquisitionOrder);
                    // And get a random list of the player's items to draw from
                    Xoroshiro128Plus listPlayerItemsSeed = new Xoroshiro128Plus(Run.instance.seed);

                    for (int i = 0; i < itemCount;) // For each parasite...
                    {
                        int validItemCount = master.inventory.GetTotalItemCountOfTier(ItemTier.Tier1) + master.inventory.GetTotalItemCountOfTier(ItemTier.Tier2) + master.inventory.GetTotalItemCountOfTier(ItemTier.Tier3) + master.inventory.GetTotalItemCountOfTier(ItemTier.Boss);
                        List<ItemDef> altModeDefList = new List<ItemDef>();

                        if (validItemCount < 1) // If no items are available to corrupt, break the loop to begin with
                        {
                            break;
                        }

                        if (CorruptingParasite_AlternateMode == true)
                        {
                            if (GetCorruptibleItemCount(altModeDefList, master) > 0) // Activate alt mode if there are any items to corrupt
                            {
                                Util.ShuffleList(altModeDefList, listPlayerItemsSeed);
                                ItemDef amdCandidate = altModeDefList.First();
                                foreach (ItemDef.Pair pair in ItemCatalog.GetItemPairsForRelationship(DLC1Content.ItemRelationshipTypes.ContagiousItem))
                                {
                                    if (pair.itemDef1 == altModeDefList.First())
                                    {
                                        master.inventory.RemoveItem(amdCandidate);
                                        master.inventory.GiveItem(pair.itemDef2);
                                        CharacterMasterNotificationQueue.PushItemTransformNotification(master, amdCandidate.itemIndex, pair.itemDef2.itemIndex, CharacterMasterNotificationQueue.TransformationType.Default);
                                        i++;
                                        break;
                                    }
                                    if (pair.itemDef2 == altModeDefList.First())
                                    {
                                        master.inventory.RemoveItem(amdCandidate);
                                        master.inventory.GiveItem(pair.itemDef1);
                                        CharacterMasterNotificationQueue.PushItemTransformNotification(master, amdCandidate.itemIndex, pair.itemDef1.itemIndex, CharacterMasterNotificationQueue.TransformationType.Default);
                                        i++;
                                        break;
                                    }
                                }
                                continue;
                            }
                            if (GetCorruptibleItemCount(altModeDefList, master) == 0 && CorruptingParasite_AltModeOnlyConvert == true)
                            {
                                break;
                            }
                        }

                        // Shuffle all of the lists
                        Util.ShuffleList(listPlayerItems, listPlayerItemsSeed);
                        Util.ShuffleList(listVoidTier1, listPlayerItemsSeed);
                        Util.ShuffleList(listVoidTier2, listPlayerItemsSeed);
                        Util.ShuffleList(listVoidTier3, listPlayerItemsSeed);
                        Util.ShuffleList(listVoidTierBoss, listPlayerItemsSeed);
                        int chosenRarity = ParasitePickRarity();

                        if (CorruptingParasite_Replication == false) // If replication is disabled, shuffle the voidtier1 list until we get a non-parasite item
                        {
                            while (listVoidTier1[0].pickupDef.itemIndex == itemDefToHooks.itemIndex)
                            {
                                Util.ShuffleList(listVoidTier1, listPlayerItemsSeed);
                            }
                        }

                        if (chosenRarity == 0) // If common
                        {
                            foreach (ItemIndex eachItem in listPlayerItems)
                            {
                                ItemDef eachItemDef = ItemCatalog.GetItemDef(eachItem);
                                if (eachItemDef.deprecatedTier == ItemTier.Tier1)
                                {
                                    master.inventory.RemoveItem(eachItemDef);
                                    master.inventory.GiveItem(listVoidTier1[0].pickupDef.itemIndex);
                                    CharacterMasterNotificationQueue.PushItemTransformNotification(master, eachItem, listVoidTier1[0].pickupDef.itemIndex, CharacterMasterNotificationQueue.TransformationType.Default);
                                    i++;
                                    break;
                                }
                            }
                        }
                        if (chosenRarity == 1) // If uncommon
                        {
                            foreach (ItemIndex eachItem in listPlayerItems)
                            {
                                ItemDef eachItemDef = ItemCatalog.GetItemDef(eachItem);
                                if (eachItemDef.deprecatedTier == ItemTier.Tier2)
                                {
                                    master.inventory.RemoveItem(eachItemDef);
                                    master.inventory.GiveItem(listVoidTier2[0].pickupDef.itemIndex);
                                    CharacterMasterNotificationQueue.PushItemTransformNotification(master, eachItem, listVoidTier2[0].pickupDef.itemIndex, CharacterMasterNotificationQueue.TransformationType.Default);
                                    i++;
                                    break;
                                }
                            }
                        }
                        if (chosenRarity == 2) // If legendary
                        {
                            foreach (ItemIndex eachItem in listPlayerItems)
                            {
                                ItemDef eachItemDef = ItemCatalog.GetItemDef(eachItem);
                                if (eachItemDef.deprecatedTier == ItemTier.Tier3)
                                {
                                    master.inventory.RemoveItem(eachItemDef);
                                    master.inventory.GiveItem(listVoidTier3[0].pickupDef.itemIndex);
                                    CharacterMasterNotificationQueue.PushItemTransformNotification(master, eachItem, listVoidTier3[0].pickupDef.itemIndex, CharacterMasterNotificationQueue.TransformationType.Default);
                                    i++;
                                    break;
                                }
                            }
                        }
                        if (chosenRarity == 3) // If boss
                        {
                            foreach (ItemIndex eachItem in listPlayerItems)
                            {
                                ItemDef eachItemDef = ItemCatalog.GetItemDef(eachItem);
                                if (eachItemDef.deprecatedTier == ItemTier.Boss)
                                {
                                    master.inventory.RemoveItem(eachItemDef);
                                    master.inventory.GiveItem(listVoidTierBoss[0].pickupDef.itemIndex);
                                    CharacterMasterNotificationQueue.PushItemTransformNotification(master, eachItem, listVoidTierBoss[0].pickupDef.itemIndex, CharacterMasterNotificationQueue.TransformationType.Default);
                                    i++;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            On.RoR2.CharacterMaster.OnServerStageBegin += (orig, self, stage) =>
            {
                orig(self, stage);
                ParasiteTradeItems(self);
            };
        }

        public static void Initiate(bool CorruptingParasite_CorruptBossItems, bool CorruptingParasite_AlternateMode, bool CorruptingParasite_Replication, bool CorruptingParasite_AltModeOnlyConvert) // Finally, initiate the item and all of its features
        {
            CreateItem();
            ItemAPI.Add(new CustomItem(itemDefinition, CreateDisplayRules()));
            AddTokens(CorruptingParasite_AlternateMode, CorruptingParasite_AltModeOnlyConvert);
            AddHooks(itemDefinition, CorruptingParasite_CorruptBossItems, CorruptingParasite_AlternateMode, CorruptingParasite_Replication, CorruptingParasite_AltModeOnlyConvert);
        }
    }
}
