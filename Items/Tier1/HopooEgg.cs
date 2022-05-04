using BepInEx;
using BepInEx.Configuration;
using R2API;
using R2API.Utils;
using RoR2;
using System;
using UnityEngine;
using Hex3Mod;
using Hex3Mod.Logging;
using On.EntityStates;

namespace Hex3Mod.Items
{
    /*
    Hopoo Egg increases base jump height and give the player slightly more air control
    This is to provide an alternative to the Hopoo Feather for upward mobility, as there are few options for some survivors
    It also synergizes with the feather by increasing the height of each jump
    The added air control should also be a nice utility
    */
    public class HopooEgg
    {
        // Create functions here for defining the ITEM, TOKENS, HOOKS and CONFIG. This is simpler than doing it in Main
        static string itemName = "HopooEgg"; // Change this name when making a new item
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
            item.deprecatedTier = ItemTier.Tier1;
            item.canRemove = true;
            item.hidden = false;

            item.pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/HopooEggPrefab.prefab");
            item.pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Textures/Icons/HopooEgg.png");

            return item;
        }

        public static ItemDisplayRuleDict CreateDisplayRules() // We'll figure item displays out... some day...
        {
            return new ItemDisplayRuleDict();
        }

        public static void AddTokens(float HopooEgg_JumpModifier, float HopooEgg_AirControlModifier)
        {
            float HopooEgg_JumpModifier_Readable = HopooEgg_JumpModifier * 100f;
            float HopooEgg_AirControlModifier_Readable = HopooEgg_AirControlModifier * 100f;

            LanguageAPI.Add("H3_" + upperName + "_NAME", "Hopoo Egg");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Jump higher and with more control.");
            LanguageAPI.Add("H3_" + upperName + "_DESC", "Jump <style=cIsUtility>" + HopooEgg_JumpModifier_Readable + "%</style> higher <style=cStack>(+" + HopooEgg_JumpModifier_Readable + "% per stack)</style>. While in the air, you can control your movement <style=cIsUtility>" + HopooEgg_AirControlModifier_Readable + "%</style> more <style=cStack>(+" + HopooEgg_AirControlModifier_Readable + "% per stack)</style>.");
            LanguageAPI.Add("H3_" + upperName + "_LORE", "\"...The hopoo's chicks are independent, being the only Europan nesting birds that hunt for themselves from birth. One is able to leave its nest as a newborn thanks to the low gravity environment, and it is born nimble enough to navigate down any rough terrain that lies between it and its food. This creates a unique problem for many newborn hopoos, however, as they often find themselves unable to go back up. The hopoo, out of reach of its nest, usually becomes nomadic and lives on its lonesome, having forgotten about its home by the time it has grown into an adult. This is where they get their common nickname, the 'hobo bird'.\"\n\n- Europan Wildlife Guide");
        }

        public static void AddHooks(ItemDef itemDefToHooks, float HopooEgg_JumpModifier, float HopooEgg_AirControlModifier) // Insert hooks here
        {
            float jumpModifier = HopooEgg_JumpModifier;
            float airControlModifier = HopooEgg_AirControlModifier;

            void H3_JumpVelocity(GenericCharacterMain.orig_ApplyJumpVelocity orig, CharacterMotor motor, CharacterBody body, float horiz, float vert, bool vault)
            {
                if ((body != null) && (body.inventory != null))
                {
                    int itemCount = body.inventory.GetItemCount(itemDefToHooks);
                    if (itemCount > 0)
                    {
                        vert *= 1f + jumpModifier * (float)itemCount;
                        motor.airControl = 0.25f + (airControlModifier * (float)itemCount);
                    }
                }
                orig.Invoke(motor, body, horiz, vert, vault);
            }

            GenericCharacterMain.ApplyJumpVelocity += new GenericCharacterMain.hook_ApplyJumpVelocity(H3_JumpVelocity);
        }

        public static void Initiate(float HopooEgg_JumpModifier, float HopooEgg_AirControlModifier) // Finally, initiate the item and all of its features
        {
            CreateItem();
            ItemAPI.Add(new CustomItem(itemDefinition, CreateDisplayRules()));
            AddTokens(HopooEgg_JumpModifier, HopooEgg_AirControlModifier);
            AddHooks(itemDefinition, HopooEgg_JumpModifier, HopooEgg_AirControlModifier);
        }
    }
}
