using BepInEx;
using BepInEx.Configuration;
using R2API;
using R2API.Utils;
using RoR2;
using RoR2.ExpansionManagement;
using System;
using System.Linq;
using UnityEngine;
using Hex3Mod;
using Hex3Mod.Logging;
using VoidItemAPI;

namespace Hex3Mod.Items
{
    /*
    If the Corrupting Parasite exists, your items may all be turned into void items by the time you get enough movement to counteract this.
    The Notice Of Absence should provide a way to gain movement speed with a fully void build, with the downside of being quite bad without void items.
    */
    public class NoticeOfAbsence
    {
        // Create functions here for defining the ITEM, TOKENS, HOOKS and CONFIG. This is simpler than doing it in Main
        static string itemName = "NoticeOfAbsence"; // Change this name when making a new item
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

            item.tags = new ItemTag[]{ ItemTag.Utility };
            item.deprecatedTier = ItemTier.VoidTier1;
            item.canRemove = true;
            item.hidden = false;
            item.requiredExpansion = ExpansionCatalog.expansionDefs.FirstOrDefault(x => x.nameToken == "DLC1_NAME");

            item.pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/NoticeOfAbsencePrefab.prefab");
            item.pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Textures/Icons/NoticeOfAbsence.png");

            return item;
        }

        public static ItemDisplayRuleDict CreateDisplayRules() // We'll figure item displays out... when our models get better
        {
            return new ItemDisplayRuleDict();
        }

        public static void AddTokens(float NoticeOfAbsence_SpeedBuff)
        {
            float NoticeOfAbsence_SpeedBuffReadable = NoticeOfAbsence_SpeedBuff * 100f;

            LanguageAPI.Add("H3_" + upperName + "_NAME", "Notice Of Absence");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Move faster the more void items you have. <style=cIsVoid>Corrupts all Bucket Lists.</style>");
            LanguageAPI.Add("H3_" + upperName + "_DESC", "For each <style=cIsVoid>void item</style> in your inventory, move <style=cIsUtility>" + NoticeOfAbsence_SpeedBuffReadable + "%</style> faster per stack. <style=cIsVoid>Corrupts all Bucket Lists.</style>");
            LanguageAPI.Add("H3_" + upperName + "_LORE", "I'm leaving tomorrow\n\nYou wouldn't understand why\n\n- Alex");
        }

        public static void AddHooks(ItemDef itemDefToHooks, float NoticeOfAbsence_SpeedBuff) // Insert hooks here
        {
            // Void transformation
            VoidTransformation.CreateTransformation(itemDefToHooks, "BucketList");

            void NoticeOfAbsenceRecalculateStats(CharacterBody body, RecalculateStatsAPI.StatHookEventArgs args)
            {
                if (body.inventory && body.inventory.GetItemCount(itemDefToHooks) > 0)
                {
                    float voidItemCount = 0f;
                    voidItemCount += body.inventory.GetTotalItemCountOfTier(ItemTier.VoidTier1);
                    voidItemCount += body.inventory.GetTotalItemCountOfTier(ItemTier.VoidTier2);
                    voidItemCount += body.inventory.GetTotalItemCountOfTier(ItemTier.VoidTier3);
                    voidItemCount += body.inventory.GetTotalItemCountOfTier(ItemTier.VoidBoss);
                    args.moveSpeedMultAdd += NoticeOfAbsence_SpeedBuff * (voidItemCount * body.inventory.GetItemCount(itemDefToHooks));
                }
            }
            RecalculateStatsAPI.GetStatCoefficients += NoticeOfAbsenceRecalculateStats;
        }

        public static void Initiate(float NoticeOfAbsence_SpeedBuff) // Finally, initiate the item and all of its features
        {
            CreateItem();
            ItemAPI.Add(new CustomItem(itemDefinition, CreateDisplayRules()));
            AddTokens(NoticeOfAbsence_SpeedBuff);
            AddHooks(itemDefinition, NoticeOfAbsence_SpeedBuff);
        }
    }
}
