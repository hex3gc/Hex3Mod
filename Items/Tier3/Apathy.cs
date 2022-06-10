using BepInEx;
using BepInEx.Configuration;
using R2API;
using R2API.Utils;
using RoR2;
using RoR2.Orbs;
using RoR2.Items;
using System;
using UnityEngine;
using Hex3Mod;
using Hex3Mod.Logging;
using Hex3Mod.HelperClasses;

namespace Hex3Mod.Items
{
    /*
    Apathy is another item that is intended to buff allies, but this time more directly by granting barrier for every hit sustained.
    Additionally, the Apathy holder gets damage reduction from all hits on their barrier
    This item is meant to synergize with other barrier items by increasing their effectiveness, with the passive barrier gain allowing more barrier sustain
    */
    public class Apathy
    {
        // Create functions here for defining the ITEM, TOKENS, HOOKS and CONFIG. This is simpler than doing it in Main
        static string itemName = "Apathy"; // Change this name when making a new item
        static string upperName = itemName.ToUpper();
        static ItemDef itemDefinition = CreateItem();
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

            item.tags = new ItemTag[]{ ItemTag.Healing, ItemTag.Utility, ItemTag.AIBlacklist, ItemTag.BrotherBlacklist }; // Also change these when making a new item
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

        public static void AddTokens(float Apathy_Barrier, float Apathy_BarrierStack, float Apathy_Reduction, float Apathy_ReductionStack)
        {
            float Apathy_Barrier_Readable = Apathy_Barrier * 100f;
            float Apathy_BarrierStack_Readable = Apathy_BarrierStack * 100f;
            float Apathy_Reduction_Readable = Apathy_Reduction * 100f;
            float Apathy_ReductionStack_Readable = Apathy_ReductionStack * 100f;

            LanguageAPI.Add("H3_" + upperName + "_NAME", "Apathy");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Your barrier is more resistant to damage. Grants barrier to your team when you or your allies are hit.");
            LanguageAPI.Add("H3_" + upperName + "_DESC", "Reduce ALL incoming damage by <style=cIsUtility>" + Apathy_Reduction_Readable + "%</style> <style=cStack>(+" + Apathy_ReductionStack_Readable + "% per stack)</style> when you have <style=cIsHealing>barrier</style>. When you or your allies are hit, grant <style=cIsUtility>" + Apathy_Barrier_Readable + "%</style> <style=cStack>(+" + Apathy_BarrierStack_Readable + "% per stack)</style> <style=cIsHealing>barrier</style> to your whole team.");
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

        private static void AddHooks(ItemDef itemDefToHooks, float Apathy_Barrier, float Apathy_BarrierStack, float Apathy_Reduction, float Apathy_ReductionStack) // Insert hooks here
        {
            void H3_OnHpLost3(DamageInfo damageInfo, HealthComponent healthComponent)
            {
                if (healthComponent.body) // Another stack of if statements, please. Wait, you're all out? I'll come back on Saturday
                {
                    if (healthComponent.body.master)
                    {
                        CharacterBody body = healthComponent.body;

                        if (body.inventory) // For every team member who has this item, heal them
                        {
                            int itemCount = body.inventory.GetItemCount(itemDefToHooks);
                            if (itemCount > 0)
                            {
                                if (healthComponent.barrier > (body.maxBarrier * 0.1f))
                                {
                                    float finalDamageReduction = Apathy_Reduction; // Only reduce damage at >10% barrier to prevent constant barrier tics permanently reducing dmg
                                    for (int i = 1; i < itemCount; i++) // For loop so that each item stack reduces damage a bit less
                                    {
                                        finalDamageReduction += Apathy_ReductionStack * (1f - finalDamageReduction);
                                    }
                                    if (finalDamageReduction > 0.9f) // Cap it at 90% reduction to prevent invincibility (Item still stacks barrier, so it's ok)
                                    {
                                        finalDamageReduction = 0.9f;
                                    }

                                    damageInfo.damage -= (damageInfo.damage * finalDamageReduction); // Reduce the hit's damage if barrier is present
                                }
                            }
                        }

                        if (body.teamComponent)
                        {
                            TeamIndex teamIndex = body.teamComponent.teamIndex;
                            int teamIndexInt = Convert.ToInt32(teamIndex);

                            if ((teamIndexInt >= 0) && (teamIndexInt <= 4))
                            {
                                var allies = TeamComponent.GetTeamMembers(teamIndex); // Get all members of the damage receiver's team...
                                float aggregateBarrier = 0f;

                                foreach (var ally in allies) // For each team member who has the item off cooldown, add 5% (plus stacks) barrier
                                {
                                    if (!ally.body) continue;
                                    if (ally.body.inventory && ally.body.inventory.GetItemCount(itemDefToHooks) > 0 && ally.body.GetBuffCount(apathyCooldown) < 1)
                                    {
                                        aggregateBarrier += Apathy_Barrier + (Apathy_BarrierStack * (ally.body.inventory.GetItemCount(itemDefToHooks) - 1));
                                        ally.body.AddTimedBuff(apathyCooldown, 0.5f);
                                    }
                                    if (ally.body.healthComponent)
                                        ally.body.healthComponent.AddBarrier(ally.body.maxBarrier * aggregateBarrier); // And grant it to all team members
                                }
                            }
                        }
                    }
                }
            }

            On.RoR2.HealthComponent.TakeDamage += (orig, self, damageInfo) =>
            {
                if (self.body && !self.body.name.StartsWith("ShopkeeperBody"))
                {
                    bool isVoidDamage = false;
                    if (!damageInfo.attacker && !damageInfo.inflictor && damageInfo.damageColorIndex == DamageColorIndex.Void && damageInfo.damageType == (DamageType.BypassArmor | DamageType.BypassBlock))
                    {
                        isVoidDamage = true;
                    }
                    if (isVoidDamage == false)
                    {
                        H3_OnHpLost3(damageInfo, self);
                    }
                }
                orig(self, damageInfo);
            };
        }

        public static BuffDef apathyCooldown { get; private set; }
        public static void AddBuffs()
        {
            apathyCooldown = ScriptableObject.CreateInstance<BuffDef>();
            apathyCooldown.buffColor = new Color(1f, 1f, 1f);
            apathyCooldown.canStack = true;
            apathyCooldown.isDebuff = false;
            apathyCooldown.name = "Apathy Barrier Cooldown";
            apathyCooldown.isHidden = false;
            apathyCooldown.isCooldown = true;
            apathyCooldown.iconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/Buff_Apathy.png"); // Change this
            ContentAddition.AddBuffDef(apathyCooldown);
        }

        public static void Initiate(float Apathy_Barrier, float Apathy_BarrierStack, float Apathy_Reduction, float Apathy_ReductionStack) // Finally, initiate the item and all of its features
        {
            CreateItem();
            ItemAPI.Add(new CustomItem(itemDefinition, CreateDisplayRules()));
            AddTokens(Apathy_Barrier, Apathy_BarrierStack, Apathy_Reduction, Apathy_ReductionStack);
            AddBuffs();
            AddHooks(itemDefinition, Apathy_Barrier, Apathy_BarrierStack, Apathy_Reduction, Apathy_ReductionStack);
        }
    }
}
