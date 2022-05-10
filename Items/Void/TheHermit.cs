using BepInEx;
using BepInEx.Configuration;
using R2API;
using R2API.Utils;
using RoR2;
using RoR2.ExpansionManagement;
using System;
using System.Runtime;
using System.Linq;
using UnityEngine;
using Hex3Mod;
using Hex3Mod.Logging;
using VoidItemAPI;

namespace Hex3Mod.Items
{
    /*
    The Hermit seeks to provide some extra survivability in a void build, taking advantage of the boost to primary potential w/ Shrimp and future void items.
    This should be done carefully so that having too much attack speed doesn't cause invincibility.
    It should be quite weak to begin with, but when the player has a lot of attack speed, it should ramp up.
    */
    public class TheHermit
    {
        // Create functions here for defining the ITEM, TOKENS, HOOKS and CONFIG. This is simpler than doing it in Main
        static string itemName = "TheHermit"; // Change this name when making a new item
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

            item.tags = new ItemTag[]{ ItemTag.Utility};
            item.deprecatedTier = ItemTier.VoidTier3;
            item.canRemove = true;
            item.hidden = false;
            item.requiredExpansion = ExpansionCatalog.expansionDefs.FirstOrDefault(x => x.nameToken == "DLC1_NAME");

            item.pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/TheHermitPrefab.prefab");
            item.pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Textures/Icons/TheHermit.png");

            return item;
        }

        public static ItemDisplayRuleDict CreateDisplayRules() // We'll figure item displays out... when our models get better
        {
            return new ItemDisplayRuleDict();
        }

        public static void AddTokens(float TheHermit_BuffDuration, float TheHermit_DamageReduction)
        {
            float TheHermit_DamageReductionReadable = TheHermit_DamageReduction * 100f;

            LanguageAPI.Add("H3_" + upperName + "_NAME", "The Hermit");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Hitting an enemy grants you stacking damage reduction.");
            LanguageAPI.Add("H3_" + upperName + "_DESC", "On hit, grant yourself a <style=cIsUtility>stacking buff</style> that <style=cIsHealing>reduces</style> all incoming damage by <style=cIsHealing>" + TheHermit_DamageReductionReadable + "%</style> for " + TheHermit_BuffDuration + " <style=cStack>(+" + TheHermit_BuffDuration + " per stack)</style> seconds. <style=cIsVoid>Corrupts all Symbiotic Scorpions.</style>");
            LanguageAPI.Add("H3_" + upperName + "_LORE", "\"When do you think we'll get outta here?\"" +
        "\n\n\"I don't think we will.\"" +
        "\n\n\"Really? With this again?\"" +
        "\n\nThe Railgunner had let her weapon down long ago, waiting for it to be claimed by the shifting sands around them. The environment they were in had felt familiar, and it wasn't until she spotted a few landmarks that they realized: This was an exact replica of their landing zone. The differences were superficial, namely everything being purple and the air feeling more like liquid oxygen. The Commando tried to swim up through it a couple of times, but the physics didn't work out in his favor." +
        "\n\nAlex nodded. \"Yes. I think we'll die here.\"" +
        "\n\n\"What about the distress beacon?\"" +
        "\n\n\"You seem to forget how we got here. The whole ship went down.\"" +
        "\n\n\"A rescue vessel will pick that up.\"" +
        "\n\n\"I don't think UES can ship rescue vessels to different dimensions...\"" +
        "\n\nLance paced around, growing frustrated with the pessimism. He didn't like the situation they'd been put in either, but he wasn't wallowing in it. It had been what felt like weeks, but still he was searching for an exit, an anomaly, or anything that could lead them somewhere different. It wasn't so easy while preparing for another attack by monsters, but he occasionally had some time off to ponder an escape. \"You're saying that is if we know where the hell we are. Maybe we'd find out if you ever helped me.\"" +
        "\n\n\"There is nothing out there. Simple.\" Alex was resting up in anticipation of another wave. She turned over onto her side, away from her crewmate." +
        "\n\nLance spotted a creature in the corner of his eye, and like all the others they'd met, he was to destroy it on sight. He stomped his foot down upon it, raised it back up, and the tiny crab scampered away unharmed." +
        "\n\n\"Tch. You really are the spitting image of your generation, getting depressed and giving up when something doesn't go your way...\"" +
        "\n\n\"Stop talking to me.\"" +
        "\n\n\"What else am I supposed to do here?\"" +
        "\n\nThe world shifted and groaned." +
        "\n\n\"-Wha-\"" +
        "\n\nIt was like the dimension had gone out of focus for a moment, and was trying to force itself back into place. The sands shook violently, pinhole lights permeated the skies and an unimaginably loud rumbling overtook their conversation. Lance tried to speak but nothing could be heard. All they could do was watch as the event took place." +
        "\n\nThe purple sands began to leak green, acidic bile." +
        "\n\nLance gestured frantically for Alex to get up. In hopes of finding an exit, he led them away from the now putrid ground and onto a hill leading to the cliffside. It was where he discovered the edge of their pocket dimension, and where he wasted far too many bullets trying to drill through the invisible wall holding him in." +
        "\n\nThe lights that shone through the sky got brighter and spread out. When their eyes adjusted, they could see what was revealed: Their landing site." +
        "\n\n\"GO!!\"" +
        "\n\nAlex felt a push at her back, and she fell on solid ground. The purple hue on everything had vanished, and so had the rumbling. She looked back towards Lance, and behind him, the jaws of a giant claw ready to consume him." +
        "\n\nHe had found a way out, but it was too late to take it.");
        }

        public static void AddHooks(ItemDef itemDefToHooks, float TheHermit_BuffDuration, float TheHermit_DamageReduction) // Insert hooks here
        {
            // Void transformation
            VoidTransformation.CreateTransformation(itemDefToHooks, "PermanentDebuffOnHit");

            void H3_HermitStack(DamageInfo damageInfo) // Add the stacks of buffs
            {
                if (damageInfo.attacker && damageInfo.attacker.GetComponent<CharacterBody>() != null)
                {
                    CharacterBody body = damageInfo.attacker.GetComponent<CharacterBody>();
                    if (body.inventory && body.inventory.GetItemCount(itemDefToHooks) > 0)
                    {
                        body.AddTimedBuff(hermitBuff, TheHermit_BuffDuration * body.inventory.GetItemCount(itemDefToHooks));
                    }
                }
            }

            void H3_HermitReduction(DamageInfo damageInfo, HealthComponent healthComponent)
            {
                if (healthComponent.body)
                {
                    int buffCount = healthComponent.body.GetBuffCount(hermitBuff);
                    if (buffCount > 0)
                    {
                        float finalDamageReduction = 0f; // Copied from Apathy, ez damage reduction
                        for (int i = 0; i < buffCount; i++) // For loop so that each item stack reduces damage a bit less
                        {
                            finalDamageReduction += TheHermit_DamageReduction * (1f - finalDamageReduction);
                        }
                        if (finalDamageReduction > 0.9f) // Cap it at 90% reduction to prevent invincibility
                        {
                            finalDamageReduction = 0.9f;
                        }

                        damageInfo.damage -= (damageInfo.damage * finalDamageReduction);
                    }
                }
            }

            On.RoR2.HealthComponent.TakeDamage += (orig, self, damageInfo) =>
            {
                if (self.body && !self.body.name.StartsWith("ShopkeeperBody"))
                {
                    H3_HermitStack(damageInfo);
                    H3_HermitReduction(damageInfo, self);
                }
                orig(self, damageInfo);
            };
        }

        public static BuffDef hermitBuff { get; private set; }
        public static void AddBuffs()
        {
            hermitBuff = ScriptableObject.CreateInstance<BuffDef>();
            hermitBuff.buffColor = new Color(0.4f, 0.4f, 0.4f);
            hermitBuff.canStack = true;
            hermitBuff.isDebuff = false;
            hermitBuff.name = "The Hermit's Protection";
            hermitBuff.isHidden = false;
            hermitBuff.isCooldown = false;
            hermitBuff.iconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Textures/Icons/TheHermit.png"); // Rework this later
            ContentAddition.AddBuffDef(hermitBuff);
        }

        public static void Initiate(float TheHermit_BuffDuration, float TheHermit_DamageReduction) // Finally, initiate the item and all of its features
        {
            CreateItem();
            ItemAPI.Add(new CustomItem(itemDefinition, CreateDisplayRules()));
            AddTokens(TheHermit_BuffDuration, TheHermit_DamageReduction);
            AddBuffs();
            AddHooks(itemDefinition, TheHermit_BuffDuration, TheHermit_DamageReduction);
        }
    }
}
