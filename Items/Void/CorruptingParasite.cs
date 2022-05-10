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

            item.pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/CorruptingParasitePrefab.prefab");
            item.pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Textures/Icons/CorruptingParasite.png");

            return item;
        }

        public static ItemDisplayRuleDict CreateDisplayRules() // We'll figure item displays out... when our models get better
        {
            return new ItemDisplayRuleDict();
        }

        public static void AddTokens()
        {
            LanguageAPI.Add("H3_" + upperName + "_NAME", "Corrupting Parasite");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Corrupts one of your items into a random void item each stage.");
            LanguageAPI.Add("H3_" + upperName + "_DESC", "<style=cIsVoid>Corrupts</style> 1 <style=cStack>(+1 per stack)</style> item in your inventory into a <style=cIsVoid>random void item</style> of the same rarity each stage. Prefers more common items [60/<style=cIsHealing>30</style>/<style=cIsHealth>5</style>/<style=cShrine>5</style>]%.");
            LanguageAPI.Add("H3_" + upperName + "_LORE", "Order: Bugs" +
            "\nTracking Number: 977******" +
            "\nEstimated Delivery: Not approved" +
            "\nShipping Method: Priority" +
            "\nShipping Address: Earth, Unspecified address" +
            "\n\nBugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs?" +
            "\n\nBugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs? Bugs?");
        }

        public static void AddHooks(ItemDef itemDefToHooks, bool CorruptingParasite_CorruptBossItems) // Insert hooks here
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
                        if (validItemCount < 1)
                        {
                            break;
                        }

                        // Shuffle all of the lists
                        Util.ShuffleList(listPlayerItems, listPlayerItemsSeed);
                        Util.ShuffleList(listVoidTier1, listPlayerItemsSeed);
                        Util.ShuffleList(listVoidTier2, listPlayerItemsSeed);
                        Util.ShuffleList(listVoidTier3, listPlayerItemsSeed);
                        Util.ShuffleList(listVoidTierBoss, listPlayerItemsSeed);
                        int chosenRarity = ParasitePickRarity();

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

        public static void Initiate(bool CorruptingParasite_CorruptBossItems) // Finally, initiate the item and all of its features
        {
            CreateItem();
            ItemAPI.Add(new CustomItem(itemDefinition, CreateDisplayRules()));
            AddTokens();
            AddHooks(itemDefinition, CorruptingParasite_CorruptBossItems);
        }
    }
}
