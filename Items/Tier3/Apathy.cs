using R2API;
using RoR2;
using System;
using UnityEngine;
using Hex3Mod.HelperClasses;

namespace Hex3Mod.Items
{
    /*
    Apathy was a weird item, and its mechanics were shaky. Thematically, I think it works better as a "send in your allies to do the work" item,
    synergizing with Empathy with an incentive to keep buying those drones that die all the time. Let me know how it feels to play with this one.
    */
    public class Apathy
    {
        static string itemName = "Apathy";
        static string upperName = itemName.ToUpper();
        static ItemDef itemDefinition = CreateItem();
        public static ItemDef hiddenItemDefinition = CreateHiddenItem();
        public static GameObject LoadPrefab()
        {
            GameObject pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/ApathyPrefab.prefab");
            return pickupModelPrefab;
        }
        public static Sprite LoadSprite()
        {
            Sprite pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/Apathy.png");
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

            item.tags = new ItemTag[]{ ItemTag.Healing, ItemTag.Damage, ItemTag.AIBlacklist, ItemTag.BrotherBlacklist };
            item.deprecatedTier = ItemTier.Tier3;
            item.canRemove = true;
            item.hidden = false;

            item.pickupModelPrefab = LoadPrefab();
            item.pickupIconSprite = LoadSprite();

            return item;
        }

        public static ItemDisplayRuleDict CreateDisplayRules()
        {
            GameObject ItemDisplayPrefab = helpers.PrepareItemDisplayModel(PrefabAPI.InstantiateClone(LoadPrefab(), LoadPrefab().name + "Display", false));

            ItemDisplayRuleDict rules = new ItemDisplayRuleDict();
            rules.Add("mdlCommandoDualies", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.46303F, -0.23741F, 0.22363F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.07054F, 0.07054F, 0.07054F)
                    }
                }
            );
            rules.Add("mdlHuntress", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.46303F, -0.23741F, 0.22363F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.07054F, 0.07054F, 0.07054F)
                    }
                }
            );
            rules.Add("mdlToolbot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Hip",
                        localPos = new Vector3(-3.67415F, -3.53406F, 3.49507F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.61885F, 0.61885F, 0.61885F)
                    }
                }
            );
            rules.Add("mdlEngi", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.70594F, -0.52649F, 0.46413F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.07054F, 0.07054F, 0.07054F)
                    }
                }
            );
            rules.Add("mdlMage", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.46303F, -0.23741F, 0.22363F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.07054F, 0.07054F, 0.07054F)
                    }
                }
            );
            rules.Add("mdlMerc", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.46303F, -0.23741F, 0.22363F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.07054F, 0.07054F, 0.07054F)
                    }
                }
            );
            rules.Add("mdlTreebot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "PlatformBase",
                        localPos = new Vector3(1.49471F, 1.14408F, -1.24895F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.19447F, 0.19447F, 0.19447F)
                    }
                }
            );
            rules.Add("mdlLoader", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.73526F, -0.26901F, 0.42105F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.07054F, 0.07054F, 0.07054F)
                    }
                }
            );
            rules.Add("mdlCroco", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Hip",
                        localPos = new Vector3(5.36278F, -2.56467F, 4.70967F),
                        localAngles = new Vector3(319.2833F, 0F, 0F),
                        localScale = new Vector3(0.49983F, 0.49983F, 0.49983F)
                    }
                }
            );
            rules.Add("mdlCaptain", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.55865F, -0.77158F, 0.13444F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.07054F, 0.07054F, 0.07054F)
                    }
                }
            );
            rules.Add("mdlBandit2", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.46303F, -0.23741F, 0.22363F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.07054F, 0.07054F, 0.07054F)
                    }
                }
            );
            rules.Add("EngiTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Base",
                        localPos = new Vector3(1.59118F, 1.47278F, 0.22363F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.31116F, 0.31116F, 0.31116F)
                    }
                }
            );
            rules.Add("EngiWalkerTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Base",
                        localPos = new Vector3(1.55029F, 1.6875F, 0.22363F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.31494F, 0.31494F, 0.31494F)
                    }
                }
            );
            rules.Add("mdlScav", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(14.30765F, -0.53849F, 1.92231F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(1.66772F, 1.66772F, 1.66772F)
                    }
                }
            );
            rules.Add("mdlRailGunner", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.46303F, -0.23741F, 0.22363F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.07054F, 0.07054F, 0.07054F)
                    }
                }
            );
            rules.Add("mdlVoidSurvivor", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(-0.49633F, -0.15624F, 0.2652F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.07054F, 0.07054F, 0.07054F)
                    }
                }
            );

            return rules;
        }

        // Hidden items should not display at all
        public static ItemDisplayRuleDict CreateHiddenDisplayRules()
        {
            return new ItemDisplayRuleDict();
        }

        public static void AddTokens(float Apathy_HealthIncrease, float Apathy_DamageIncrease)
        {
            LanguageAPI.Add("H3_" + upperName + "_NAME", "Apathy");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Gain a permanent max health and damage buff when an ally is killed, as well as full barrier.");
            LanguageAPI.Add("H3_" + upperName + "_DESC", String.Format("When an ally is killed, gain full <style=cIsHealing>barrier</style> and receive a permanent <style=cIsHealing>{0}%</style> <style=cStack>(+{0}% per stack)</style> <style=cIsHealing>max health</style> increase and a <style=cIsDamage>{1}%</style> <style=cStack>(+{1}% per stack)</style> <style=cIsDamage>damage buff</style>.", Apathy_HealthIncrease * 100f, Apathy_DamageIncrease * 100f));
            LanguageAPI.Add("H3_" + upperName + "_LORE",
                "\"I can't [REDACTED] believe I'm doing this...\"" +
                "\n\n\"We'll share the network. We have to.\"" +
                "\n\n\"FINE! Just do it fast-\"" +
                "\n\nFrom out of the container came a silvery, goopy substance, like viscous mercury. At first it laid still on the table, but it didn't take long for it to wake up. It seemed to notice its two potential hosts, only giving a moment of hesistation before splitting into two and floating towards their faces." +
                "\n\n\"What's it gonna do!?\"" +
                "\n\n\"Open your mouth.\"" +
                "\n\n\"NO!\"" +
                "\n\nAs it drew near enough to the crewmates' faces, it suddenly gained speed and latched onto their noses and mouths, choking the one who wasn't prepared. The calmer of the crew raised a hand to try and ease the other down to his level, but he had already doubled over, trying to wrench the substance from his face. Neither could speak." +
                "\n\nIt took less time for the substance to enter and integrate with its first host, who began to yell and command his crewmate to keep calm and hold his breath." +
                "\n\nThe struggling crewmate was purple in the face and teary-eyed. He felt like he was about to die. The goop then sucked itself into his system and propogated its way through, leaving him coughing and wretching." +
                "\n\n\"Oh my god... oh my god...\" He croaked out the words" +
                "\n\n\"Look- we need to leave, now. That Titan could show up at-\"" +
                "\n\nTheir ship was then crushed in on one side. Stone demolished metal for one deafening moment and then stopped. The Stone Titan only had to kick the craft once in order to destroy it beyond repair. One of its occupants, however, managed to get to his feet and reorient himself." +
                "\n\n\"Burke ? You alright ? Where...\"" +
                "\n\nBurke's body lay beneath the remains of the cabin wall It had been pulverized. After his trouble with the nanobots, he could barely think, let alone dodge the wreckage of his ship as it came crashing down." +
                "\n\nJones began to feel something shift inside of him. The network had sustained critical damage and needed to begin repair routines. Then he felt everything shift inside of him..." +
                "\n\n..." +
                "\n\nThe Stone Titan spotted an unknown figure leaving the ship. Its red eye flared with heat, and without a moment to wait, it focused its laser directly onto the intruder." +
                "\n\nIt expected to see a pile of ash left behind, but it once again saw the figure. It wasn't a creature that it knew of. The Titan had seen humans before, but this was not one. Its eyes leaked dark red as if they were ripped out and replaced with black orbs. It no longer had a face, but a mangling of bone and fatty tissue, with hard spines leading from its forehead all the way down to its lower back. Its torso pumped hard, in and out like a giant beating heart. Its skin split open from the pressure, but then wove itself back together again. All of its limbs were lithe and muscular, with nails hardened into claws and bones that poked out to shield the body." +
                "\n\nThe Titan had never felt fear before."
                );
        }

        private static void AddHooks(ItemDef itemDef, ItemDef hiddenItemDef, float Apathy_HealthIncrease, float Apathy_DamageIncrease)
        {
            // Apply stat changes
            void GetStatCoefficients(CharacterBody body, RecalculateStatsAPI.StatHookEventArgs args)
            {
                if (body.inventory && body.healthComponent && body.inventory.GetItemCount(hiddenItemDef) > 0)
                {
                    int hiddenItemCount = body.inventory.GetItemCount(hiddenItemDef);
                    if (body.inventory.GetItemCount(itemDef) > 0)
                    {
                        args.damageMultAdd += Apathy_DamageIncrease * hiddenItemCount; // Apply buffs and update buff count
                        args.healthMultAdd += Apathy_HealthIncrease * hiddenItemCount;
                        body.SetBuffCount(apathyStacks.buffIndex, hiddenItemCount);
                    }
                    else
                    {
                        body.inventory.RemoveItem(hiddenItemDef, 9999); // If the player doesn't have Apathy, remove all stacks of the hidden item
                        body.SetBuffCount(apathyStacks.buffIndex, 0);
                    }
                }
            }

            // Give temporary barrier and hidden item on ally death
            // AI blacklisted for performance's sake
            On.RoR2.GlobalEventManager.OnCharacterDeath += (orig, self, damageReport) =>
            {
                if (damageReport.attacker && damageReport.victim && damageReport.victim.body && damageReport.victim.body.teamComponent && damageReport.victim.body.teamComponent.teamIndex == TeamIndex.Player)
                {
                    foreach (var ally in TeamComponent.GetTeamMembers(TeamIndex.Player))
                    {
                        if (ally.body && ally.body.healthComponent && ally.body.inventory && ally.body.inventory.GetItemCount(itemDef) > 0)
                        {
                            ally.body.healthComponent.AddBarrier(ally.body.healthComponent.fullBarrier);
                            ally.body.inventory.GiveItem(hiddenItemDef);
                            Util.PlaySound(EntityStates.ImpBossMonster.GroundPound.initialAttackSoundString, ally.gameObject);
                            EffectData effectData = new EffectData
                            {
                                origin = ally.body.corePosition
                            };
                            EffectManager.SpawnEffect(EntityStates.VoidMegaCrab.BackWeapon.FireVoidMissiles.muzzleEffectPrefab, effectData, false);
                        }
                    }
                }
                orig(self, damageReport);
            };

            RecalculateStatsAPI.GetStatCoefficients += GetStatCoefficients;
        }

        public static ItemDef CreateHiddenItem() // Keeps track of Apathy stacks
        {
            ItemDef item = ScriptableObject.CreateInstance<ItemDef>();

            item.name = "ApathyHidden";
            item.nameToken = "H3_" + upperName + "_NAME";
            item.pickupToken = "H3_" + upperName + "_PICKUP";
            item.descriptionToken = "H3_" + upperName + "_DESC";
            item.loreToken = "H3_" + upperName + "_LORE";

            item.tags = new ItemTag[] { ItemTag.CannotCopy, ItemTag.CannotSteal, ItemTag.CannotDuplicate };
            item.deprecatedTier = ItemTier.NoTier;
            item.canRemove = false;
            item.hidden = true;

            item.pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/ApathyPrefab.prefab");
            item.pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Textures/Icons/Apathy.png");

            return item;
        }

        public static BuffDef apathyStacks { get; private set; }
        public static void AddBuffs() // Visual indicator of Apathy stacks
        {
            apathyStacks = ScriptableObject.CreateInstance<BuffDef>();
            apathyStacks.buffColor = new Color(1f, 1f, 1f);
            apathyStacks.canStack = true;
            apathyStacks.isDebuff = false;
            apathyStacks.name = "Apathy Stacks";
            apathyStacks.isHidden = false;
            apathyStacks.isCooldown = false;
            apathyStacks.iconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/Buff_Apathy.png");
            ContentAddition.AddBuffDef(apathyStacks);
        }

        public static void Initiate(float Apathy_HealthIncrease, float Apathy_DamageIncrease)
        {
            ItemAPI.Add(new CustomItem(itemDefinition, CreateDisplayRules()));
            ItemAPI.Add(new CustomItem(hiddenItemDefinition, CreateHiddenDisplayRules()));
            AddTokens(Apathy_HealthIncrease, Apathy_DamageIncrease);
            AddBuffs();
            AddHooks(itemDefinition, hiddenItemDefinition, Apathy_HealthIncrease, Apathy_DamageIncrease);
        }
    }
}
