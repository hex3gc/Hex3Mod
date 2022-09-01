using R2API;
using RoR2;
using System.Collections.Generic;
using UnityEngine;
using Hex3Mod.HelperClasses;

namespace Hex3Mod.Items
{
    /*
    A rework to the Elder Mutagen as I felt its purpose was too convoluted and its mechanics too janky. 
    I'd rather make it a simple item that supports certain builds neatly.
    */
    public class ElderMutagen
    {
        static string itemName = "ElderMutagen";
        static string upperName = itemName.ToUpper();
        static ItemDef itemDefinition = CreateItem();
        public static GameObject LoadPrefab()
        {
            GameObject pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/ElderMutagenPrefab.prefab");
            return pickupModelPrefab;
        }
        public static Sprite LoadSprite()
        {
            Sprite pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/ElderMutagen.png");
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

            item.tags = new ItemTag[]{ ItemTag.Utility, ItemTag.Damage, ItemTag.AIBlacklist, ItemTag.BrotherBlacklist}; // AI blacklist for simplicity and performance. May change later
            item.deprecatedTier = ItemTier.Tier3;
            item.canRemove = true;
            item.hidden = false;

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
                        childName = "Head",
                        localPos = new Vector3(0.13142F, 0.1467F, 0.08031F),
                        localAngles = new Vector3(297.3017F, 14.59079F, 48.55595F),
                        localScale = new Vector3(0.06707F, 0.13088F, 0.06707F)
                    }
                }
            );
            rules.Add("mdlHuntress", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(-0.01099F, 0.04634F, -0.01131F),
                        localAngles = new Vector3(4.94392F, 0.02119F, -0.00515F),
                        localScale = new Vector3(0.10607F, 0.10607F, 0.10607F)
                    }
                }
            );
            rules.Add("mdlToolbot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "UpperArmL",
                        localPos = new Vector3(-0.21565F, 3.81528F, -1.77649F),
                        localAngles = new Vector3(13.28516F, 342.2303F, 346.2823F),
                        localScale = new Vector3(0.66386F, 0.66386F, 0.66386F)
                    }
                }
            );
            rules.Add("mdlEngi", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "ThighR",
                        localPos = new Vector3(-0.07713F, 0.19162F, -0.10058F),
                        localAngles = new Vector3(74.02536F, 297.283F, 256.941F),
                        localScale = new Vector3(0.09095F, 0.09095F, 0.09095F)
                    }
                }
            );
            rules.Add("mdlMage", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "FootL",
                        localPos = new Vector3(0.00348F, 0.06752F, 0.08452F),
                        localAngles = new Vector3(53.72561F, 15.16084F, 192.0224F),
                        localScale = new Vector3(-0.04578F, 0.04536F, 0.03792F)
                    }
                }
            );
            rules.Add("mdlMerc", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "ThighL",
                        localPos = new Vector3(0.06946F, 0.29215F, -0.10201F),
                        localAngles = new Vector3(84.85436F, 229.5896F, 265.6841F),
                        localScale = new Vector3(0.08755F, 0.08755F, 0.08755F)
                    }
                }
            );
            rules.Add("mdlTreebot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "PlatformBase",
                        localPos = new Vector3(0F, 0F, 0F),
                        localAngles = new Vector3(0.00012F, 74.58047F, -0.00001F),
                        localScale = new Vector3(0.49053F, 0.49053F, 0.49053F)
                    }
                }
            );
            rules.Add("mdlLoader", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "MechLowerArmL",
                        localPos = new Vector3(0.0653F, 0.22464F, 0.04163F),
                        localAngles = new Vector3(84.2281F, 267.1171F, 268.1745F),
                        localScale = new Vector3(0.13033F, 0.13033F, 0.13033F)
                    }
                }
            );
            rules.Add("mdlCroco", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Stomach",
                        localPos = new Vector3(1.40725F, 0.29892F, 1.91719F),
                        localAngles = new Vector3(307.247F, 344.692F, 52.17693F),
                        localScale = new Vector3(0.86716F, 0.86716F, 0.86716F)
                    }
                }
            );
            rules.Add("mdlCaptain", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "ClavicleR",
                        localPos = new Vector3(0.04858F, 0.13547F, -0.12345F),
                        localAngles = new Vector3(72.32738F, 243.9777F, 85.64837F),
                        localScale = new Vector3(0.09552F, 0.09552F, 0.09552F)
                    }
                }
            );
            rules.Add("mdlBandit2", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "ThighL",
                        localPos = new Vector3(0.05238F, 0.36679F, 0.07032F),
                        localAngles = new Vector3(69.67028F, 312.9913F, 272.3289F),
                        localScale = new Vector3(0.05981F, 0.05981F, 0.05981F)
                    }
                }
            );
            rules.Add("EngiTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Base",
                        localPos = new Vector3(0F, 0.86834F, 0F),
                        localAngles = new Vector3(0F, 0F, 0F),
                        localScale = new Vector3(0.42677F, 0.42677F, 0.42677F)
                    }
                }
            );
            rules.Add("EngiWalkerTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Base",
                        localPos = new Vector3(0F, 0.87072F, 0F),
                        localAngles = new Vector3(0F, 0F, 0F),
                        localScale = new Vector3(0.44815F, 0.44815F, 0.44815F)
                    }
                }
            );
            rules.Add("mdlScav", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(-5.16409F, 3.66659F, -0.02804F),
                        localAngles = new Vector3(337.9631F, 359.9089F, 24.0611F),
                        localScale = new Vector3(-1.41672F, -3.30552F, -1.41672F)
                    }
                }
            ); 
            rules.Add("mdlRailGunner", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Backpack",
                        localPos = new Vector3(-0.26249F, 0.2179F, 0.00295F),
                        localAngles = new Vector3(304.3797F, 175.19F, 274.0369F),
                        localScale = new Vector3(0.11333F, 0.1129F, 0.1129F)
                    }
                }
            );
            rules.Add("mdlVoidSurvivor", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "ForeArmR",
                        localPos = new Vector3(0.11695F, 0.14536F, -0.20407F),
                        localAngles = new Vector3(79.43037F, 354.2926F, 354.4371F),
                        localScale = new Vector3(0.12869F, 0.12869F, 0.12869F)
                    }
                }
            );

            return rules;
        }

        public static void AddTokens(float ElderMutagen_AddDuration, float ElderMutagen_CooldownReduction)
        {
            LanguageAPI.Add("H3_" + upperName + "_NAME", "Elder Mutagen");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Buffs you receive and debuffs you inflict last longer.");
            LanguageAPI.Add("H3_" + upperName + "_DESC", "All <style=cIsUtility>buffs</style> you receive and <style=cIsDamage>debuff / damage-over-time</style> effects you inflict last <style=cIsUtility>" + ElderMutagen_AddDuration + "</style> seconds longer <style=cStack>(+" + ElderMutagen_AddDuration + " per stack)</style>. <style=cIsUtility>Cooldown buffs</style> are <style=cIsUtility>" + ElderMutagen_CooldownReduction + "</style> seconds shorter.");
            LanguageAPI.Add("H3_" + upperName + "_LORE", "<style=cMono>Lab Dissection Analysis File</style> " +
            "\n\nSubject: Strange Mutagen" +
            "\nTechnician: Alex [REDACTED]" +
            "\nTable Spec: Table" +
            "\nNotes:" +
            "\n\n> I don't know how to use this" +
            "\n> Oh that's the button" +
            "\n> We found something strange. We aren't scientists but we need to make sure it's safe to transport." +
            "\n> Blob of living jelly, air around it changes from cold to hot" +
            "\n> Poking it" +
            "\n> Blob tried to latch onto tweezers, had to give them up" +
            "\n> Tweezers growing spiky growths, twisting into spirals?" +
            "\n> Throwing on a spider" +
            "\n> Blob let the spider go" +
            "\n> Spider uncontrollable" +
            "\n> we killed it" +
            "\n> Blob grabbed onto Jolene's arm" +
            "\n> Somethings wrong" +
            "\n> Timestamping for break");
        }

        private static void AddHooks(ItemDef itemDef, float ElderMutagen_AddDuration, float ElderMutagen_CooldownReduction)
        {
            // Known Quirks:
            // - When you pick up a mutagen, any already-existing monsters will not receive longer debuffs (DOTs work). Changing this would be a hit to performance, so I kept it as-is

            // Apply mutagen behavior to all enemies who spawn, based on the highest amount of mutagens owned by a player
            On.RoR2.TeamComponent.Start += (orig, self) =>
            {
                if (self.teamIndex != TeamIndex.Player)
                {
                    int highestMutagenStack = 0;
                    foreach (var player in TeamComponent.GetTeamMembers(TeamIndex.Player))
                    {
                        if (player.body && player.body.inventory && player.body.inventory.GetItemCount(itemDef) > highestMutagenStack)
                        {
                            highestMutagenStack = player.body.inventory.GetItemCount(itemDef);
                        }
                    }
                    if (highestMutagenStack > 0)
                    {
                        self.body.AddItemBehavior<MutagenItemBehavior>(1);
                        self.body.GetComponent<MutagenItemBehavior>().buffAddTime = highestMutagenStack * ElderMutagen_AddDuration;
                    }
                }
            };

            On.RoR2.CharacterBody.AddTimedBuff_BuffDef_float += (orig, self, buffDef, duration) =>
            {
                // When a debuff is added to a body with the Mutagen itembehavior, increase its length
                if (self.GetComponent<MutagenItemBehavior>() && buffDef.isDebuff && !buffDef.isCooldown)
                {
                    duration += self.GetComponent<MutagenItemBehavior>().buffAddTime;
                }

                // When a buff is added to a body with the Mutagen item, increase its length
                if (self.inventory && self.inventory.GetItemCount(itemDef) > 0 && !buffDef.isDebuff && !buffDef.isCooldown)
                {
                    duration += self.inventory.GetItemCount(itemDef) * ElderMutagen_AddDuration;
                }

                // Reduce cooldown time for item holders
                if (self.inventory && self.inventory.GetItemCount(itemDef) > 0 && buffDef.isCooldown)
                {
                    duration -= ElderMutagen_CooldownReduction;
                }

                orig(self, buffDef, duration);
            };

            // If the attacker has a Mutagen, increase DOT length
            On.RoR2.DotController.AddDot += (orig, self, attackerObject, duration, dotIndex, damageMultiplier, maxStacksFromAttacker, totalDamage, preUpgradeDotIndex) =>
            {
                if (attackerObject.GetComponent<CharacterBody>())
                {
                    CharacterBody attacker = attackerObject.GetComponent<CharacterBody>();
                    if (attacker.inventory && attacker.inventory.GetItemCount(itemDef) > 0)
                    {
                        duration += attacker.inventory.GetItemCount(itemDef) * ElderMutagen_AddDuration;
                    }
                }
                orig(self, attackerObject, duration, dotIndex, damageMultiplier, maxStacksFromAttacker, totalDamage, preUpgradeDotIndex);
            };
        }

        // Upon hitting an enemy, give them an itembehavior that tracks how much longer debuffs should last on them
        public class MutagenItemBehavior : CharacterBody.ItemBehavior
        {
            public float buffAddTime = 0;
        }

        public static void Initiate(float ElderMutagen_AddDuration, float ElderMutagen_CooldownReduction)
        {
            ItemAPI.Add(new CustomItem(itemDefinition, CreateDisplayRules()));
            AddTokens(ElderMutagen_AddDuration, ElderMutagen_CooldownReduction);
            AddHooks(itemDefinition, ElderMutagen_AddDuration, ElderMutagen_CooldownReduction);
        }
    }
}
