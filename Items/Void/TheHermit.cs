using R2API;
using RoR2;
using RoR2.ExpansionManagement;
using System.Linq;
using System;
using UnityEngine;
using VoidItemAPI;
using Hex3Mod.HelperClasses;
using Hex3Mod.Utils;
using static Hex3Mod.Main;

namespace Hex3Mod.Items
{
    /*
    The Hermit seeks to provide some extra survivability in a void build, and provide an alternative to Scorpion for builds that don't benefit much from it
    Its purpose has changed, and now it should provide a very sizable damage reduction against Damage-Over-Time and sustained hits
    */
    public class TheHermit
    {
        static string itemName = "TheHermit";
        static string upperName = itemName.ToUpper();
        public static ItemDef itemDef;
        public static GameObject LoadPrefab()
        {
            GameObject pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/TheHermitPrefab.prefab");
            if (Main.debugMode == true)
            {
                pickupModelPrefab.GetComponentInChildren<Renderer>().gameObject.AddComponent<MaterialControllerComponents.HGControllerFinder>();
            }
            return pickupModelPrefab;
        }
        public static Sprite LoadSprite()
        {
            Sprite pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/TheHermit.png");
            return pickupIconSprite;
        }
        public static Sprite LoadBuffSprite()
        {
            Sprite pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/Buff_TheHermit.png");
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

            item.tags = new ItemTag[]{ ItemTag.Healing };
            item.deprecatedTier = ItemTier.VoidTier3;
            item.canRemove = true;
            item.hidden = false;
            item.requiredExpansion = Hex3ModExpansion;

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
                        childName = "Chest",
                        localPos = new Vector3(-0.01248F, 0.42417F, -0.17057F),
                        localAngles = new Vector3(352.275F, 282.2963F, 350.3966F),
                        localScale = new Vector3(0.23097F, 0.21308F, 0.21643F)
                    }
                }
            );
            rules.Add("mdlHuntress", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "HeadCenter",
                        localPos = new Vector3(-0.02213F, -0.09881F, 0.10617F),
                        localAngles = new Vector3(21.97226F, 76.78506F, 31.59275F),
                        localScale = new Vector3(0.18335F, 0.18335F, 0.18335F)
                    }
                }
            );
            rules.Add("mdlToolbot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(-0.45113F, 1.71135F, -0.5302F),
                        localAngles = new Vector3(329.3982F, 336.148F, 339.0386F),
                        localScale = new Vector3(2.94697F, 2.94697F, 2.94697F)
                    }
                }
            );
            rules.Add("mdlEngi", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "CannonHeadR",
                        localPos = new Vector3(-0.17948F, 0.16459F, 0.00262F),
                        localAngles = new Vector3(54.38401F, 131.6052F, 138.6951F),
                        localScale = new Vector3(0.25445F, 0.25445F, 0.25445F)
                    }
                }
            );
            rules.Add("mdlMage", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(-0.00119F, 0.04999F, -0.28631F),
                        localAngles = new Vector3(356.5753F, 276.9439F, 22.30641F),
                        localScale = new Vector3(0.2105F, 0.2105F, 0.2105F)
                    }
                }
            );
            rules.Add("mdlMerc", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(0.00918F, 0.08362F, -0.27662F),
                        localAngles = new Vector3(7.01334F, 272.4854F, 38.51432F),
                        localScale = new Vector3(0.32255F, 0.32255F, 0.32255F)
                    }
                }
            );
            rules.Add("mdlTreebot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "PlatformBase",
                        localPos = new Vector3(0.56956F, 0.221F, -0.35119F),
                        localAngles = new Vector3(51.06863F, 340.1037F, 277.2233F),
                        localScale = new Vector3(0.7225F, 0.7225F, 0.7225F)
                    }
                }
            );
            rules.Add("mdlLoader", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "ThighR",
                        localPos = new Vector3(-0.12666F, 0.13209F, 0.02458F),
                        localAngles = new Vector3(324.0293F, 305.8312F, 70.87708F),
                        localScale = new Vector3(0.20347F, 0.20347F, 0.20347F)
                    }
                }
            );
            rules.Add("mdlCroco", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "HeadCenter",
                        localPos = new Vector3(0.62093F, 3.56951F, 0.62145F),
                        localAngles = new Vector3(323.7129F, 179.6431F, 325.5504F),
                        localScale = new Vector3(2.29979F, 2.29979F, 2.29979F)
                    }
                }
            );
            rules.Add("mdlCaptain", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Neck",
                        localPos = new Vector3(0.02166F, 0.11679F, 0.0974F),
                        localAngles = new Vector3(11.35254F, 290.6996F, 245.9148F),
                        localScale = new Vector3(0.1795F, 0.1815F, 0.1795F)
                    }
                }
            );
            rules.Add("mdlBandit2", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(0.19331F, 0.21284F, -0.15525F),
                        localAngles = new Vector3(335.8373F, 229.7396F, 38.50159F),
                        localScale = new Vector3(0.17084F, 0.17084F, 0.17084F)
                    }
                }
            );
            rules.Add("EngiTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Base",
                        localPos = new Vector3(-0.3055F, 0.74397F, 0.20357F),
                        localAngles = new Vector3(16.53893F, 14.99345F, 25.73735F),
                        localScale = new Vector3(0.84315F, 0.84315F, 0.84315F)
                    }
                }
            );
            rules.Add("EngiWalkerTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Base",
                        localPos = new Vector3(-0.36004F, 0.91609F, -0.19541F),
                        localAngles = new Vector3(38.83065F, 267.0875F, 327.8494F),
                        localScale = new Vector3(0.66535F, 0.66535F, 0.66535F)
                    }
                }
            );
            rules.Add("mdlScav", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(-4.33622F, 3.70776F, -0.94777F),
                        localAngles = new Vector3(21.97188F, 232.2604F, 325.6177F),
                        localScale = new Vector3(3.81427F, 3.81427F, 3.81427F)
                    }
                }
            );
            rules.Add("mdlRailGunner", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Backpack",
                        localPos = new Vector3(0.26499F, 0.07962F, -0.02356F),
                        localAngles = new Vector3(323.4973F, 5.6934F, 234.7006F),
                        localScale = new Vector3(0.17824F, 0.17824F, 0.17824F)
                    }
                }
            );
            rules.Add("mdlVoidSurvivor", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "CalfL",
                        localPos = new Vector3(0.10176F, 0.46033F, -0.02201F),
                        localAngles = new Vector3(5.2009F, 27.75262F, 149.8759F),
                        localScale = new Vector3(0.42825F, 0.42825F, 0.42825F)
                    }
                }
            );

            return rules;
        }

        public static void AddTokens()
        {
            LanguageAPI.Add("H3_" + upperName + "_NAME", "The Hermit");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Getting hit builds up damage resistance. <style=cIsVoid>Corrupts all Symbiotic Scorpions.</style>");
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
        public static void UpdateItemStatus(Run run)
        {
            if (!TheHermit_Enable.Value)
            {
                LanguageAPI.AddOverlay("H3_" + upperName + "_NAME", "The Hermit" + " <style=cDeath>[DISABLED]</style>");
                if (run && run.availableItems.Contains(itemDef.itemIndex)) { run.availableItems.Remove(itemDef.itemIndex); }
            }
            else
            {
                LanguageAPI.AddOverlay("H3_" + upperName + "_NAME", "The Hermit");
                if (run && !run.availableItems.Contains(itemDef.itemIndex) && run.IsExpansionEnabled(Hex3ModExpansion)) { run.availableItems.Add(itemDef.itemIndex); }
            }
            LanguageAPI.AddOverlay("H3_" + upperName + "_DESC", string.Format("Taking damage grants a <style=cIsHealing>stacking damage resistance</style> of <style=cIsHealing>{1}%</style> that lasts for <style=cIsHealing>{0}</style> seconds <style=cStack>(+{0} per stack)</style>. <style=cIsVoid>Corrupts all Symbiotic Scorpions.</style>", TheHermit_BuffDuration.Value, TheHermit_DamageReduction.Value));
        }

        private static void AddHooks() // Insert hooks here
        {
            // Void transformation
            VoidTransformation.CreateTransformation(itemDef, "PermanentDebuffOnHit");

            void HealthComponent_TakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo damageInfo)
            {
                orig(self, damageInfo);
                if (self.body && self.body.inventory && damageInfo.damage > 0f && !damageInfo.rejected)
                {
                    if (self.body.inventory.GetItemCount(itemDef) > 0)
                    {
                        self.body.AddTimedBuff(hermitBuff, TheHermit_BuffDuration.Value * self.body.inventory.GetItemCount(itemDef));
                    }
                    if (self.body.GetBuffCount(hermitBuff) > 0)
                    {
                        damageInfo.damage *= Math.Abs(Util.ConvertAmplificationPercentageIntoReductionPercentage(TheHermit_DamageReduction.Value * self.body.GetBuffCount(hermitBuff)) / 100f);
                    }
                }
            }

            On.RoR2.HealthComponent.TakeDamage += HealthComponent_TakeDamage;
        }

        public static BuffDef hermitBuff { get; private set; }
        public static void AddBuffs()
        {
            hermitBuff = ScriptableObject.CreateInstance<BuffDef>();
            hermitBuff.buffColor = new Color(1f, 1f, 1f);
            hermitBuff.canStack = true;
            hermitBuff.isDebuff = true;
            hermitBuff.name = "The Hermit's Protection";
            hermitBuff.isHidden = false;
            hermitBuff.isCooldown = false;
            hermitBuff.iconSprite = LoadBuffSprite();
            ContentAddition.AddBuffDef(hermitBuff);
        }

        public static void Initiate()
        {
            itemDef = CreateItem();
            ItemAPI.Add(new CustomItem(itemDef, CreateDisplayRules()));
            AddTokens();
            UpdateItemStatus(null);
            AddBuffs();
            AddHooks();
        }
    }
}
