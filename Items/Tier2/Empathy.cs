using R2API;
using RoR2;
using UnityEngine;
using Hex3Mod.HelperClasses;
using UnityEngine.PlayerLoop;
using static Hex3Mod.Items.ScavengersPack;

namespace Hex3Mod.Items
{
    /*
    Empathy's purpose has changed slightly to make it scale better: You heal for a percent of the damage your allies take
    */
    public class Empathy
    {
        static string itemName = "Empathy";
        static string upperName = itemName.ToUpper();
        static ItemDef itemDefinition = CreateItem();
        public static GameObject LoadPrefab()
        {
            GameObject pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/EmpathyPrefab.prefab");
            return pickupModelPrefab;
        }
        public static Sprite LoadSprite()
        {
            Sprite pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/Empathy.png");
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

            item.tags = new ItemTag[]{ ItemTag.Healing, ItemTag.AIBlacklist, ItemTag.BrotherBlacklist }; // AI Blacklist for performance sake
            item.deprecatedTier = ItemTier.Tier2;
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
                        localPos = new Vector3(0.46303F, -0.05688F, 0.22363F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.04992F, 0.04992F, 0.04992F)
                    }
                }
            );
            rules.Add("mdlHuntress", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.46303F, -0.06435F, 0.21564F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.04496F, 0.04496F, 0.04496F)
                    }
                }
            );
            rules.Add("mdlToolbot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Hip",
                        localPos = new Vector3(-3.67415F, -5.35767F, 3.49506F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.37776F, 0.37776F, 0.37776F)
                    }
                }
            );
            rules.Add("mdlEngi", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.70594F, -0.32829F, 0.46413F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.04899F, 0.04899F, 0.04899F)
                    }
                }
            );
            rules.Add("mdlMage", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.46303F, -0.44582F, 0.16459F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.04752F, 0.04752F, 0.04752F)
                    }
                }
            );
            rules.Add("mdlMerc", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.46303F, -0.45782F, 0.25012F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.0447F, 0.0447F, 0.0447F)
                    }
                }
            );
            rules.Add("mdlTreebot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "PlatformBase",
                        localPos = new Vector3(1.49471F, 1.49075F, -1.24891F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.11992F, 0.11992F, 0.11992F)
                    }
                }
            );
            rules.Add("mdlLoader", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.73526F, -0.48415F, 0.3866F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.04441F, 0.04441F, 0.04441F)
                    }
                }
            );
            rules.Add("mdlCroco", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Hip",
                        localPos = new Vector3(5.36278F, -1.64505F, 3.96713F),
                        localAngles = new Vector3(319.2833F, 0F, 0F),
                        localScale = new Vector3(0.32944F, 0.32944F, 0.33073F)
                    }
                }
            );
            rules.Add("mdlCaptain", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.55942F, -0.62916F, 0.15856F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.04415F, 0.04415F, 0.04415F)
                    }
                }
            );
            rules.Add("mdlBandit2", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.48541F, -0.15744F, -0.15702F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.0428F, 0.0428F, 0.0428F)
                    }
                }
            );
            rules.Add("EngiTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Base",
                        localPos = new Vector3(1.59118F, 2.13328F, 0.22363F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.19256F, 0.19256F, 0.19256F)
                    }
                }
            );
            rules.Add("EngiWalkerTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Base",
                        localPos = new Vector3(1.55029F, 2.32694F, 0.22363F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.18841F, 0.18841F, 0.18841F)
                    }
                }
            );
            rules.Add("mdlScav", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(14.30769F, -3.86581F, 1.33207F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(1.14461F, 1.14461F, 1.14461F)
                    }
                }
            );
            rules.Add("mdlRailGunner", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.46303F, -0.10003F, 0.24969F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.04316F, 0.04316F, 0.04316F)
                    }
                }
            );
            rules.Add("mdlVoidSurvivor", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(-0.53176F, -0.30046F, 0.2652F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.04685F, 0.04685F, 0.04685F)
                    }
                }
            );

            return rules;
        }

        public static void AddTokens(float Empathy_HealingFactor)
        {
            LanguageAPI.Add("H3_" + upperName + "_NAME", "Empathy");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Heal for a fraction of the damage your allies take.");
            LanguageAPI.Add("H3_" + upperName + "_DESC", "When an ally takes damage, <style=cIsHealing>heal</style> for <style=cIsHealing>" + Empathy_HealingFactor * 100f + "%</style> <style=cStack>(+" + Empathy_HealingFactor * 100f + "% per stack)</style> of that damage.");
            LanguageAPI.Add("H3_" + upperName + "_LORE", "<style=cEvent>//--AUTO-TRANSCRIPTION FROM UES [Redacted] --//</style>\n\n\"Oh yeah? How does this one work?\"\n\n\"Nanomachines. In response to physical trauma to the body, they get to work immediately and start patching up the wound. They're so tiny you don't even feel it happening.\"\n\n\"Is that... safe?\"\n\n\"What?\"\n\n\"A bunch of little robots in your bloodstream? There's no way that's never caused a problem.\"\n\n\"Well, maybe twenty years ago. It's 2055, technology has come far.\"\n\n\"Huh.\"\n\n\"...Although,\"\n\n\"See, I'm not putting that in my body.\"\n\n\"No, it's no big deal! But- these bots have been known to 'overcorrect'. They operate on a shared network, meaning that if- you and I, for example- both use the same group, then when -you- get hurt, the bots will think I'm hurt too!\"\n\n\"Meaning?\"\n\n\"It's unpredictable, but fascinating. If you get hurt and my body is healthy, they'll still try to 'fix' me, so they'll begin to look for inefficiencies and redundancies. They'll begin removing unneeded vestiges and replacing them with something useful, and when they're done with that, they'll begin creating something new. It's been known to happen-- they'll grow fresh organs that deal with the function of your heart or your liver but using ten times less energy and ten times less space. They'll begin re-organizing everything in your body, and they'll make your skeleton stronger while they're at it. So, really, they're quite helpful.\"\n\n\"And we only have one of these between us.\"\n\n\"Yes.\"\n\n\"...\"\n\n\"...\"\n\n\"I think I'll do the mission alone.\"");
        }

        private static void AddHooks(ItemDef itemDef, float Empathy_HealingFactor)
        {
            // Heal for a fraction of the damage that your allies take
            On.RoR2.HealthComponent.TakeDamage += (orig, self, damageInfo) =>
            {
                orig(self, damageInfo);
                if (!damageInfo.rejected && damageInfo.damage > 0 && self.body && self.body.teamComponent && self.body.teamComponent.teamIndex == TeamIndex.Player)
                {
                    foreach (var ally in TeamComponent.GetTeamMembers(TeamIndex.Player))
                    {
                        if (ally.body && ally.body.healthComponent && ally.body.inventory && ally.body.inventory.GetItemCount(itemDef) > 0)
                        {
                            float stackHealFactor = (Empathy_HealingFactor) * (ally.body.inventory.GetItemCount(itemDef) - 1);
                            ally.body.healthComponent.Heal(damageInfo.damage * (Empathy_HealingFactor + stackHealFactor), new ProcChainMask());
                        }
                    }
                }
            };
        }

        public static void Initiate(float Empathy_HealingFactor)
        {
            ItemAPI.Add(new CustomItem(itemDefinition, CreateDisplayRules()));
            AddTokens(Empathy_HealingFactor);
            AddHooks(itemDefinition, Empathy_HealingFactor);
        }
    }
}
