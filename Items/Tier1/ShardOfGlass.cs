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
    Shard Of Glass is a very simple stat increase, an item you'd never be unhappy to find
    Its secondary purpose, however, is to synergize with Shattered Reflection, forming a unique common-uncommon synergy and giving this simple item more utility
    */
    public class ShardOfGlass
    {
        // Create functions here for defining the ITEM, TOKENS, HOOKS and CONFIG. This is simpler than doing it in Main
        static string itemName = "ShardOfGlass"; // Change this name when making a new item
        static string upperName = itemName.ToUpper();
        public static ItemDef itemDefinition = CreateItem(); // Must be public for ScatteredReflection to read
        public static ItemDef CreateItem()
        {
            ItemDef item = ScriptableObject.CreateInstance<ItemDef>();

            item.name = itemName;
            item.nameToken = "H3_" + upperName + "_NAME";
            item.pickupToken = "H3_" + upperName + "_PICKUP";
            item.descriptionToken = "H3_" + upperName + "_DESC";
            item.loreToken = "H3_" + upperName + "_LORE";

            item.tags = new ItemTag[]{ ItemTag.Damage }; // Also change these when making a new item
            item.deprecatedTier = ItemTier.Tier1;
            item.canRemove = true;
            item.hidden = false;

            item.pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/Items/ShardOfGlassPrefab.prefab");
            item.pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Materials/Icons/ShardOfGlassIcon.png");

            return item;
        }

        public static ItemDisplayRuleDict CreateDisplayRules() // We'll figure item displays out... some day...
        {
            return new ItemDisplayRuleDict();
        }

        public static void AddTokens(float DamageIncrease_Config)
        {
            float DamageIncrease_Config_Readable = DamageIncrease_Config * 100f;

            LanguageAPI.Add("H3_" + upperName + "_NAME", "Shard Of Glass");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Increase your damage.");
            LanguageAPI.Add("H3_" + upperName + "_DESC", "Increase your <style=cIsDamage>base damage</style> by <style=cIsDamage>" + DamageIncrease_Config_Readable + "%</style> <style=cStack>(+" + DamageIncrease_Config_Readable + "% per stack)</style>.");
            LanguageAPI.Add("H3_" + upperName + "_LORE", "The essence of a broken body\n\nYou collect the pieces carefully\n\nAn air of arrogance surrounds these shards\n\nYou feel like you can take on the world and never break beneath it");
        }

        public static void AddHooks(ItemDef itemDefToHooks, float DamageIncrease_Config) // Insert hooks here
        {
            float DamageIncrease = DamageIncrease_Config;

            void RecalculateStatsAPI_GetStatCoefficients(CharacterBody sender, RecalculateStatsAPI.StatHookEventArgs args)
            {
                Inventory inventory = sender.inventory;
                if (inventory)
                {
                    int itemCount = inventory.GetItemCount(itemDefToHooks);
                    if (itemCount > 0)
                    {
                        args.damageMultAdd += (DamageIncrease * itemCount);
                    }
                }
            }

            RecalculateStatsAPI.GetStatCoefficients += RecalculateStatsAPI_GetStatCoefficients;
        }

        public static void Initiate(float DamageIncrease_Config) // Finally, initiate the item and all of its features
        {
            CreateItem();
            ItemAPI.Add(new CustomItem(itemDefinition, CreateDisplayRules()));
            AddTokens(DamageIncrease_Config);
            AddHooks(itemDefinition, DamageIncrease_Config);
        }
    }
}
