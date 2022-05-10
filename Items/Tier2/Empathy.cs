using BepInEx;
using BepInEx.Configuration;
using R2API;
using R2API.Utils;
using RoR2;
using RoR2.Orbs;
using System;
using UnityEngine;
using Hex3Mod;
using Hex3Mod.Logging;

namespace Hex3Mod.Items
{
    /*
    Empathy is a unique healing item that heals you whenever you or an ally takes damage.
    5 hp per hit sustained is a baseline value, I need to test this because with Drones it may be way too powerful (looking at you, Tinker's Bulwark drone)
    Also, I hope to change Planula so it doesn't get invalidated by stacking Empathy
    */
    public class Empathy
    {
        // Create functions here for defining the ITEM, TOKENS, HOOKS and CONFIG. This is simpler than doing it in Main
        static string itemName = "Empathy"; // Change this name when making a new item
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

            item.tags = new ItemTag[]{ ItemTag.Healing }; // Also change these when making a new item
            item.deprecatedTier = ItemTier.Tier2;
            item.canRemove = true;
            item.hidden = false;

            item.pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/EmpathyPrefab.prefab");
            item.pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Textures/Icons/Empathy.png");

            return item;
        }

        public static ItemDisplayRuleDict CreateDisplayRules() // We'll figure item displays out... some day...
        {
            return new ItemDisplayRuleDict();
        }

        public static void AddTokens(float Empathy_HealFor)
        {
            LanguageAPI.Add("H3_" + upperName + "_NAME", "Empathy");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Heal whenever you or an ally are hit.");
            LanguageAPI.Add("H3_" + upperName + "_DESC", "Heal for <style=cIsHealing>" + Empathy_HealFor + " HP</style> <style=cStack>(+" + Empathy_HealFor + " per stack)</style> whenever you or an ally takes damage.");
            LanguageAPI.Add("H3_" + upperName + "_LORE", "<style=cEvent>//--AUTO-TRANSCRIPTION FROM UES [Redacted] --//</style>\n\n\"Oh yeah? How does this one work?\"\n\n\"Nanomachines. In response to physical trauma to the body, they get to work immediately and start patching up the wound. They're so tiny you don't even feel it happening.\"\n\n\"Is that... safe?\"\n\n\"What?\"\n\n\"A bunch of little robots in your bloodstream? There's no way that's never caused a problem.\"\n\n\"Well, maybe twenty years ago. It's 2055, technology has come far.\"\n\n\"Huh.\"\n\n\"...Although,\"\n\n\"See, I'm not putting that in my body.\"\n\n\"No, it's no big deal! But- these bots have been known to 'overcorrect'. They operate on a shared network, meaning that if- you and I, for example- both use the same group, then when -you- get hurt, the bots will think I'm hurt too!\"\n\n\"Meaning?\"\n\n\"It's unpredictable, but fascinating. If you get hurt and my body is healthy, they'll still try to 'fix' me, so they'll begin to look for inefficiencies and redundancies. They'll begin removing unneeded vestiges and replacing them with something useful, and when they're done with that, they'll begin creating something new. It's been known to happen-- they'll grow fresh organs that deal with the function of your heart or your liver but using ten times less energy and ten times less space. They'll begin re-organizing everything in your body, and they'll make your skeleton stronger while they're at it. So, really, they're quite helpful.\"\n\n\"And we only have one of these between us.\"\n\n\"Yes.\"\n\n\"...\"\n\n\"...\"\n\n\"I think I'll do the mission alone.\"");
        }

        public static void AddHooks(ItemDef itemDefToHooks, float Empathy_HealFor) // Insert hooks here
        {
            float healFor = Empathy_HealFor;

            void H3_OnHpLost2(HealthComponent healthComponent)
            {
                if (healthComponent.body) // Another stack of if statements, on the rocks.
                {
                    if (healthComponent.body.master) // This might be redundant?
                    {
                        CharacterBody body = healthComponent.body;
                        if (body.teamComponent)
                        {
                            TeamIndex teamIndex = body.teamComponent.teamIndex;
                            int teamIndexInt = Convert.ToInt32(teamIndex);

                            if ((teamIndexInt >= 0) && (teamIndexInt <= 4))
                            {
                                var allies = TeamComponent.GetTeamMembers(teamIndex); // Get all members of the damage receiver's team...

                                foreach (var ally in allies)
                                {
                                    if (ally.body.inventory) // For every team member who has this item, heal them
                                    {
                                        int itemCount = ally.body.inventory.GetItemCount(itemDefToHooks);
                                        if (itemCount > 0)
                                        {
                                            float healAmount = Empathy_HealFor * itemCount;
                                            ally.body.healthComponent.Heal(healAmount, default, true);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            On.RoR2.HealthComponent.TakeDamage += (orig, self, damageInfo) =>
            {
                orig(self, damageInfo);
                if (self.body && !self.body.name.StartsWith("ShopkeeperBody"))
                {
                    H3_OnHpLost2(self);
                }
            };
        }

        public static void Initiate(float Empathy_HealFor) // Finally, initiate the item and all of its features
        {
            CreateItem();
            ItemAPI.Add(new CustomItem(itemDefinition, CreateDisplayRules()));
            AddTokens(Empathy_HealFor);
            AddHooks(itemDefinition, Empathy_HealFor);
        }
    }
}
