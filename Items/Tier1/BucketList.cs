using BepInEx;
using BepInEx.Configuration;
using R2API;
using R2API.Utils;
using RoR2;
using System;
using UnityEngine;
using Hex3Mod;
using Hex3Mod.Logging;

namespace Hex3Mod.Items
{
    /*
    Bucket List increases your movement drastically speed outside of the teleporter/boss event, but it also gives a small boost while in teleporter/boss events to ease the jarring change in speed
    This item is meant to be a great early-game boost for those who want to loot the stage in good time, but as a tradeoff not provide a big power boost for the important fights
    */
    public class BucketList
    {
        // Create functions here for defining the ITEM, TOKENS, HOOKS and CONFIG. This is simpler than doing it in Main
        static string itemName = "BucketList"; // Change this name when making a new item
        static string upperName = itemName.ToUpper();
        static ItemDef itemDefinition = CreateItem();
        public static ItemDef CreateItem()
        {
            ItemDef item = ScriptableObject.CreateInstance<ItemDef>();

            item.name = itemName;
            item.nameToken = "H3_" + upperName + "_NAME";
            item.pickupToken = "H3_" + upperName + "_PICKUP";
            item.descriptionToken = "H3_" + upperName + "_DESC";
            item.loreToken = "H3_" + upperName + "_LORE";

            item.tags = new ItemTag[]{ ItemTag.Utility }; // Also change these when making a new item
            item.deprecatedTier = ItemTier.Tier1;  // This tier definition apparently causes issues, but it works. Go to this first if there's an error
            item.canRemove = true;
            item.hidden = false;

            item.pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/BucketListPrefab.prefab");
            item.pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Textures/Icons/BucketList.png");

            return item;
        }

        public static ItemDisplayRuleDict CreateDisplayRules() // We'll figure item displays out... some day...
        {
            return new ItemDisplayRuleDict();
        }

        public static void AddTokens(float BucketList_FullBuff, float BucketList_BuffReduce)
        {
            float BucketList_FullBuff_Readable = BucketList_FullBuff * 100f;
            float BucketList_BuffReduce_Readable = BucketList_BuffReduce * 100f;

            LanguageAPI.Add("H3_" + upperName + "_NAME", "Bucket List");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Move faster before teleporter events and boss fights.");
            LanguageAPI.Add("H3_" + upperName + "_DESC", "Move <style=cIsUtility>" + BucketList_FullBuff_Readable + "%</style> faster <style=cStack>(+" + BucketList_FullBuff_Readable + "% per stack)</style>. Reduce this bonus by <style=cDeath>" + BucketList_BuffReduce_Readable + "%</style> during boss fights.");
            LanguageAPI.Add("H3_" + upperName + "_LORE", "- go to Saturn and see the night lights\n\n- visit grandma\n\n- see Bovine Joni in concert\n\n- try Mercurian Salts (get Jaden to make sure im ok after)\n\n- pet a gip");
        }

        public static void AddHooks(ItemDef itemDefToHooks, float BucketList_FullBuff, float BucketList_BuffReduce)
        {
            float FullBuff = BucketList_FullBuff;
            float ReducedBuff = (BucketList_FullBuff - (BucketList_FullBuff * BucketList_BuffReduce));

            // Check every time the RecalculateStatsAPI.GetStatCoefficients activates
            // IF a boss exists, then change the item's argsmultspeed to 0.25x the value
            // ELSE, set it to 1x the value

            void H3_recalcStatsCharacter(CharacterBody character, RecalculateStatsAPI.StatHookEventArgs args)
            {
                if (character.inventory)
                {
                    Inventory inventory = character.inventory;
                    int itemCount = inventory.GetItemCount(itemDefToHooks);
                    if (itemCount > 0)
                    {
                        var monsters = TeamComponent.GetTeamMembers(TeamIndex.Monster);
                        if (monsters != null)
                        {
                            int bossCount = 0;
                            foreach (var monster in monsters)
                            {
                                if (monster.body)
                                {
                                    if (monster.body.isBoss)
                                    {
                                        bossCount += 1;
                                    }
                                }
                            }
                            if (bossCount > 0) // Boss present: Reduced buff
                            {
                                args.moveSpeedMultAdd += (ReducedBuff + ((itemCount - 1) * ReducedBuff));
                            }
                            else // Boss not present: Full buff
                            {
                                args.moveSpeedMultAdd += (FullBuff + ((itemCount - 1) * FullBuff));
                            }
                        }
                    }
                }
            }

            RecalculateStatsAPI.GetStatCoefficients += H3_recalcStatsCharacter;
        }

        public static void Initiate(float BucketList_FullBuff, float BucketList_BuffReduce) // Finally, initiate the item and all of its features
        {
            CreateItem();
            ItemAPI.Add(new CustomItem(itemDefinition, CreateDisplayRules()));
            AddTokens(BucketList_FullBuff, BucketList_BuffReduce);
            AddHooks(itemDefinition, BucketList_FullBuff, BucketList_BuffReduce);
        }
    }
}
