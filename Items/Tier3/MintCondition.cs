using BepInEx;
using BepInEx.Configuration;
using R2API;
using R2API.Utils;
using RoR2;
using RoR2.Orbs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Hex3Mod;
using Hex3Mod.Logging;

namespace Hex3Mod.Items
{
    /*
    Mint Condition is an attempt at making a mobility red item. This is good for both Benthic Bloom's playability and even more mobility options in a movement-focused game
    Ben's Raincoat used to be a powerful item for its debuff blocking power, but possibly too powerful. Restricting this item to only some debuffs should make it a more focused effect
    */
    public class MintCondition
    {
        // Create functions here for defining the ITEM, TOKENS, HOOKS and CONFIG. This is simpler than doing it in Main
        static string itemName = "MintCondition"; // Change this name when making a new item
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

            item.tags = new ItemTag[]{ItemTag.Utility}; // Also change these when making a new item
            item.deprecatedTier = ItemTier.Tier3;
            item.canRemove = true;
            item.hidden = false;

            item.pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/MintConditionPrefab.prefab");
            item.pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Textures/Icons/MintCondition.png");

            return item;
        }

        public static ItemDisplayRuleDict CreateDisplayRules() // We'll figure item displays out... some day...
        {
            return new ItemDisplayRuleDict();
        }

        public static void AddTokens(float MintCondition_MoveSpeed, float MintCondition_MoveSpeedStack, int MintCondition_AddJumps, int MintCondition_AddJumpsStack)
        {
            float MintCondition_MoveSpeed_Readable = MintCondition_MoveSpeed * 100f;
            float MintCondition_MoveSpeedStack_Readable = MintCondition_MoveSpeedStack * 100f;

            LanguageAPI.Add("H3_" + upperName + "_NAME", "Mint Condition");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "You are immune to movement restricting status effects. Gain movement speed and extra jumps.");
            LanguageAPI.Add("H3_" + upperName + "_DESC", "Provides immunity to all movement restricting status effects. Gain <style=cIsUtility>" + MintCondition_MoveSpeed_Readable + "%</style> <style=cStack>(+" + MintCondition_MoveSpeedStack_Readable + "% per stack)</style> movement speed and <style=cIsUtility>" + MintCondition_AddJumps + "</style> <style=cStack>(+" + MintCondition_AddJumpsStack + " per stack)</style> extra jumps.");
            LanguageAPI.Add("H3_" + upperName + "_LORE", 
                "\nOrder: Eclipse 380 Boosts(Mint Condition)" +
                "\nTracking Number: 240******" +
                "\nEstimated Delivery: 08/29/2056" +
                "\nShipping Method: Priority" +
                "\nShipping Address: 70 Marshall Road, Chicago, Earth" +
                "\nShipping Details:" +
                "\n\nHandle carefully. Do not turn upside-down and keep shoes in vacuum seal. If nobody answers the door for the delivery, send it to the post office for safe-keeping, don't just leave it on the doorstep." +
                "\n\nEdit by Customer 240****** (02/22/2056):" +
                "\nHello my son recently used your site to buy some of your shoes. Are you kidding me? $4350 for shoes? I can assure you this purchase was NOT cleared by me (the home owner) and will be immediately cancelled. Your support team has not answered my emails and calls but still has the money for the shoes. If you do not CANCEL and REFUND this order immediately I will have to take this up with the company, and if they dont answer me either you'll be sued" +
                "\n\nEdit by Customer 240****** (03/02/2056)" +
                "\n\"Lost in transit\" are you joking??? you STILL HAVE the money it has not been refunded. I will sue your [REDACTED] [REDACTED] to mars and back if you keep turning down my calls, believe it you [REDACTED]. You don't know what kind of [REDACTED] youve gotten yourselves into now");
        }

        public static void AddHooks(ItemDef itemDefToHooks, float MintCondition_MoveSpeed, float MintCondition_MoveSpeedStack, int MintCondition_AddJumps, int MintCondition_AddJumpsStack) // Insert hooks here
        {
            string[] AllBuffNames = new string[512];
            void H3_GetAllBuffNames() // At the start of each run, get every buff name in the game and store them as strings for later comparison. I sure hope this doesn't result in space issues...
            {
                int AllBuffsIndex = 0;
                foreach (BuffDef buffID in BuffCatalog.buffDefs)
                {
                    AllBuffNames[AllBuffsIndex] = buffID.name;
                    AllBuffsIndex++;
                }
            }

            void H3_MobilityIncreaseRoR(CharacterBody body) // I don't know how to effectively add base move speed, so doing it separately through r2api is the safe option
            {
                if (body && body.inventory)
                {
                    int itemCount = body.inventory.GetItemCount(itemDefToHooks);
                    if (itemCount > 0)
                    {
                        body.maxJumpCount += MintCondition_AddJumps + (MintCondition_AddJumpsStack * (itemCount - 1));
                    }
                }
            }

            void H3_MobilityIncreaseR2API(CharacterBody body, RecalculateStatsAPI.StatHookEventArgs args)
            {
                if (body && body.inventory)
                {
                    int itemCount = body.inventory.GetItemCount(itemDefToHooks);
                    if (itemCount > 0)
                    {
                        args.baseMoveSpeedAdd += MintCondition_MoveSpeed + (MintCondition_MoveSpeedStack * (itemCount - 1));
                    }
                }
            }

            void H3_PreventFreeze(DamageReport damageReport) // Make sure that no received damage can be stunning or freezing
            {
                if (damageReport.victim && damageReport.victim.body && damageReport.victim.body.inventory)
                {
                    int itemCount = damageReport.victim.body.inventory.GetItemCount(itemDefToHooks);
                    if (itemCount > 0 && damageReport.damageInfo != null)
                    {
                        SetStateOnHurt component = damageReport.victim.body.GetComponent<SetStateOnHurt>();
                        if (component)
                        {
                            component.canBeFrozen = false;
                            component.canBeStunned = false;
                        }
                    }
                }
            }

            void H3_RemoveMovementBuff(CharacterBody body, BuffDef receivedBuff)
            {
                if (body && body.master && body.inventory)
                {
                    int itemCount = body.inventory.GetItemCount(itemDefToHooks); // Now, for a looong list of all the movement-restricting buffs I could find...
                    if (itemCount > 0)
                    {
                        if (receivedBuff.name == "bdBeetleJuice" || receivedBuff.name == "bdClayGoo" || receivedBuff.name == "bdCripple" || receivedBuff.name == "bdNullified" || receivedBuff.name == "bdNullifyStack" || receivedBuff.name == "bdSlow50" || receivedBuff.name == "bdSlow60" || receivedBuff.name == "bdSlow80" || receivedBuff.name == "bdWeak" || receivedBuff.name == "bdLunarSecondaryRoot" || receivedBuff.name == "bdEntangle" || receivedBuff.name == "bdJailerSlow" || receivedBuff.name == "bdJailerTether" || receivedBuff.name == "bdSlow30")
                        {
                            body.RemoveBuff(receivedBuff); // Remove the buff if it matches the bill
                        }
                    }
                }
                // Use this secret button to retrieve all buffs in the game and put them in chat
                /*
                foreach (BuffDef buffID in BuffCatalog.buffDefs)
                {
                    Chat.AddMessage("" + buffID.name);
                }
                */
            }

            On.RoR2.CharacterBody.RecalculateStats += (orig, self) => // Finally, add the hooks... surely none could go wrong
            {
                orig(self);
                H3_MobilityIncreaseRoR(self);
            };
            On.RoR2.CharacterBody.OnBuffFirstStackGained += (orig, self, receivedBuff) =>
            {
                orig(self, receivedBuff);
                H3_RemoveMovementBuff(self, receivedBuff);
            };
            On.RoR2.PreGameController.StartRun += (orig, self) =>
            {
                orig(self);
                H3_GetAllBuffNames();
            };
            On.RoR2.SetStateOnHurt.OnTakeDamageServer += (orig, self, damageReport) =>
            {
                H3_PreventFreeze(damageReport);
                orig(self, damageReport);
            };
            RecalculateStatsAPI.GetStatCoefficients += H3_MobilityIncreaseR2API;
        }

        public static void Initiate(float MintCondition_MoveSpeed, float MintCondition_MoveSpeedStack, int MintCondition_AddJumps, int MintCondition_AddJumpsStack) // Finally, initiate the item and all of its features
        {
            CreateItem();
            ItemAPI.Add(new CustomItem(itemDefinition, CreateDisplayRules()));
            AddTokens(MintCondition_MoveSpeed, MintCondition_MoveSpeedStack, MintCondition_AddJumps, MintCondition_AddJumpsStack);
            AddHooks(itemDefinition, MintCondition_MoveSpeed, MintCondition_MoveSpeedStack, MintCondition_AddJumps, MintCondition_AddJumpsStack);
        }
    }
}
