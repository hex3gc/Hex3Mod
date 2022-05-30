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
using Hex3Mod.HelperClasses;
using VoidItemAPI;

namespace Hex3Mod.Items
{
    /*
    The void item counterpart for Scattered Reflection. Meant to be paired with Drop Of Necrosis for a blight build, allowing damage scaling in a fully void build
    Replaces your DOTs with blight, but makes Blight stronger.
    */
    public class SpatteredCollection
    {
        // Create functions here for defining the ITEM, TOKENS, HOOKS and CONFIG. This is simpler than doing it in Main
        static string itemName = "SpatteredCollection"; // Change this name when making a new item
        static string upperName = itemName.ToUpper();
        public static ItemDef itemDefinition = CreateItem();
        public static GameObject LoadPrefab()
        {
            GameObject pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/SpatteredCollectionPrefab.prefab");
            return pickupModelPrefab;
        }
        public static Sprite LoadSprite()
        {
            Sprite pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/SpatteredCollection.png");
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

            item.tags = new ItemTag[]{ ItemTag.Damage, ItemTag.AIBlacklist };
            item.deprecatedTier = ItemTier.VoidTier2;
            item.canRemove = true;
            item.hidden = false;
            item.requiredExpansion = ExpansionCatalog.expansionDefs.FirstOrDefault(x => x.nameToken == "DLC1_NAME");

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
                            localPos = new Vector3(0.1406F, 0.20362F, 0.16122F),
                            localAngles = new Vector3(0F, 317.8871F, 0F),
                            localScale = new Vector3(0.09072F, 0.09072F, 0.09072F)
                        }
                    }
            );
            rules.Add("mdlHuntress", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplayPrefab,
                            childName = "ThighR",
                            localPos = new Vector3(-0.0478F, 0.14598F, 0.05861F),
                            localAngles = new Vector3(0F, 299.3364F, 180F),
                            localScale = new Vector3(0.13552F, 0.13552F, 0.13552F)
                        }
                    }
            );
            rules.Add("mdlToolbot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplayPrefab,
                            childName = "Chest",
                            localPos = new Vector3(1.84889F, 2.48941F, -0.17016F),
                            localAngles = new Vector3(0F, 114.488F, 0F),
                            localScale = new Vector3(0.95584F, 0.95584F, 0.95584F)
                        }
                    }
            );
            rules.Add("mdlEngi", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplayPrefab,
                            childName = "CannonHeadR",
                            localPos = new Vector3(0F, 0.31162F, 0.19408F),
                            localAngles = new Vector3(90F, 0F, 0F),
                            localScale = new Vector3(0.1497F, 0.1497F, 0.1497F)
                        }
                    }
            );
            rules.Add("mdlMage", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplayPrefab,
                            childName = "Chest",
                            localPos = new Vector3(-0.09075F, 0.33386F, -0.19281F),
                            localAngles = new Vector3(348.8875F, 172.4447F, 359.4133F),
                            localScale = new Vector3(0.09625F, 0.09625F, 0.09625F)
                        }
                    }
            );
            rules.Add("mdlMerc", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplayPrefab,
                            childName = "ThighR",
                            localPos = new Vector3(-0.09941F, 0.19489F, -0.01716F),
                            localAngles = new Vector3(0F, 121.1547F, 180F),
                            localScale = new Vector3(0.14587F, 0.14587F, 0.14587F)
                        }
                    }
            );
            rules.Add("mdlTreebot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplayPrefab,
                            childName = "PlatformBase",
                            localPos = new Vector3(-0.01306F, 0.74383F, -0.54007F),
                            localAngles = new Vector3(0F, 20.26201F, 0F),
                            localScale = new Vector3(0.32589F, 0.32589F, 0.32589F)
                        }
                    }
            );
            rules.Add("mdlLoader", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplayPrefab,
                            childName = "Chest",
                            localPos = new Vector3(0F, 0.49343F, -0.25102F),
                            localAngles = new Vector3(0F, 0F, 0F),
                            localScale = new Vector3(0.13602F, 0.13602F, 0.13602F)
                        }
                    }
            );
            rules.Add("mdlCroco", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplayPrefab,
                            childName = "Neck",
                            localPos = new Vector3(-0.28827F, 3.40373F, -0.23129F),
                            localAngles = new Vector3(7.96254F, 0F, 0F),
                            localScale = new Vector3(1.1376F, 1.1376F, 1.14205F)
                        }
                    }
            );
            rules.Add("mdlCaptain", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplayPrefab,
                            childName = "Chest",
                            localPos = new Vector3(0.10312F, 0.32193F, -0.09578F),
                            localAngles = new Vector3(0F, 112.2311F, 0F),
                            localScale = new Vector3(0.15129F, 0.15129F, 0.15129F)
                        }
                    }
            );
            rules.Add("mdlBandit2", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplayPrefab,
                            childName = "Pelvis",
                            localPos = new Vector3(0.13183F, -0.01922F, -0.01681F),
                            localAngles = new Vector3(341.423F, 180F, 180F),
                            localScale = new Vector3(0.10984F, 0.10984F, 0.10984F)
                        }
                    }
            );
            rules.Add("EngiTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplayPrefab,
                            childName = "Head",
                            localPos = new Vector3(0.70618F, 0.69575F, -0.50425F),
                            localAngles = new Vector3(359.6133F, 179.1287F, 0.86641F),
                            localScale = new Vector3(0.35501F, 0.35501F, 0.35501F)
                        }
                    }
            );
            rules.Add("EngiWalkerTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplayPrefab,
                            childName = "Head",
                            localPos = new Vector3(0.55367F, 1.29359F, -1.13702F),
                            localAngles = new Vector3(0F, 138.7365F, 0F),
                            localScale = new Vector3(0.37941F, 0.37941F, 0.37941F)
                        }
                    }
            );
            rules.Add("mdlScav", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplayPrefab,
                            childName = "Backpack",
                            localPos = new Vector3(7.06659F, 4.30474F, 2.12134F),
                            localAngles = new Vector3(354.2775F, 239.2192F, 358.8981F),
                            localScale = new Vector3(4.12256F, 4.12256F, 4.12256F)
                        }
                    }
            );
            rules.Add("mdlRailGunner", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplayPrefab,
                            childName = "GunRoot",
                            localPos = new Vector3(0.10203F, -0.11071F, 0.03383F),
                            localAngles = new Vector3(1.85989F, 271.3306F, 270.8604F),
                            localScale = new Vector3(0.13455F, 0.13455F, 0.13455F)
                        }
                    }
            );
            rules.Add("mdlVoidSurvivor", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = ItemDisplayPrefab,
                            childName = "UpperArmR",
                            localPos = new Vector3(0.15095F, 0.07747F, 0.01464F),
                            localAngles = new Vector3(359.7774F, 180F, 180F),
                            localScale = new Vector3(0.16238F, 0.16238F, 0.16238F)
                        }
                    }
            );

            return rules;
        }

        public static void AddTokens(float SpatteredCollection_IntervalReduction)
        {
            LanguageAPI.Add("H3_" + upperName + "_NAME", "Spattered Collection");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "All of your <style=cIsDamage>damage over time</style> effects are converted into a stronger stacking <style=cIsDamage>Blight</style>. <style=cIsVoid>Corrupts all Scattered Reflections.</style>");
            LanguageAPI.Add("H3_" + upperName + "_DESC", "All of your <style=cIsDamage>damage over time</style> effects are converted to stacking <style=cIsDamage>Blight</style>, which ticks <style=cIsDamage>" + ((1f - SpatteredCollection_IntervalReduction) * 100f) + "%</style> faster for each stack of this item owned by your team. <style=cIsVoid>Corrupts all Scattered Reflections.</style>");
            LanguageAPI.Add("H3_" + upperName + "_LORE", "\"I don't remember my name... I don't, not at all. That's all gone.\"" +
            "\n\n<style=cStack>(Several minutes of silence, broken up by what sounds like shuffles and clattering of glass)</style>" +
            "\n\n\"My collection. It's wonderful, isn't it? I want you to see it. Please, come see my collection... Bubbling, stinging poisons, corrosive to the touch... If you listen closely it's bubbling, here...\"" +
            "\n\n<style=cStack>(Bubbling sounds, mixed with heavy breathing. The audio recording ends shortly after.)</style>");
        }

        private static void AddHooks(ItemDef itemDefToHooks, float SpatteredCollection_IntervalReduction, float SpatteredCollection_DotChance) // Insert hooks here
        {
            // Void transformation
            VoidTransformation.CreateTransformation(itemDefToHooks, "ScatteredReflection");

            On.RoR2.DotController.AddDot += (orig, self, attackerObject, duration, dotIndex, damageMultiplier, maxStacksFromAttacker, totalDamage, preUpgradeDotIndex) =>
            {
                if (attackerObject && attackerObject.GetComponent<CharacterBody> != null && attackerObject.GetComponent<CharacterBody>().inventory && attackerObject.GetComponent<CharacterBody>().teamComponent)
                {
                    int itemCount = attackerObject.GetComponent<CharacterBody>().inventory.GetItemCount(itemDefToHooks);
                    if (itemCount > 0)
                    {
                        int totalItemCount = 0; // To make this easy for me, Spattered Collection stacking will be tracked for the whole team

                        foreach (TeamComponent member in TeamComponent.GetTeamMembers(TeamIndex.Player))
                        {
                            if (member.body && member.body.inventory && member.body.inventory.GetItemCount(itemDefToHooks) > 0)
                            {
                                totalItemCount += member.body.inventory.GetItemCount(itemDefToHooks);
                            }
                        }

                        float newInterval = 0.333f;
                        for (int i = 0; i < totalItemCount; i++)
                        {
                            newInterval = newInterval * SpatteredCollection_IntervalReduction;
                        }
                        DotController.dotDefs[5].interval = newInterval;
                        orig(self, attackerObject, 5f, DotController.DotIndex.Blight, 1f, maxStacksFromAttacker, null, preUpgradeDotIndex);
                    }
                    else
                    {
                        orig(self, attackerObject, duration, dotIndex, damageMultiplier, maxStacksFromAttacker, totalDamage, preUpgradeDotIndex);
                    }
                }
                else
                {
                    orig(self, attackerObject, duration, dotIndex, damageMultiplier, maxStacksFromAttacker, totalDamage, preUpgradeDotIndex);
                }
            };

            On.RoR2.GlobalEventManager.OnHitEnemy += (orig, self, damageInfo, victim) =>
            {
                if (damageInfo.attacker && damageInfo.attacker.GetComponent<CharacterBody> != null && damageInfo.attacker.GetComponent<CharacterBody>().inventory && damageInfo.attacker.GetComponent<CharacterBody>().inventory.GetItemCount(itemDefToHooks) > 0)
                {
                    if (damageInfo.attacker.GetComponent<CharacterBody>().master && damageInfo.dotIndex != DotController.DotIndex.Blight && damageInfo.attacker != victim && damageInfo.damage > 0f)
                    {
                        if (damageInfo.damageType != DamageType.DoT && damageInfo.damageType != DamageType.FallDamage)
                        {
                            if (Util.CheckRoll(SpatteredCollection_DotChance, damageInfo.attacker.GetComponent<CharacterBody>().master.luck) == true)
                            {
                                InflictDotInfo inflictDotInfo = new InflictDotInfo
                                {
                                    attackerObject = damageInfo.attacker,
                                    victimObject = victim,
                                    dotIndex = DotController.DotIndex.Blight,
                                    duration = 5f,
                                    damageMultiplier = 1f
                                };
                                DotController.InflictDot(ref inflictDotInfo);
                            }
                        }
                    }
                }
                orig(self, damageInfo, victim);
            };
        }

        public static void Initiate(float SpatteredCollection_IntervalReduction, float SpatteredCollection_DotChance) // Finally, initiate the item and all of its features
        {
            CreateItem();
            ItemAPI.Add(new CustomItem(itemDefinition, CreateDisplayRules()));
            AddTokens(SpatteredCollection_IntervalReduction);
            AddHooks(itemDefinition, SpatteredCollection_IntervalReduction, SpatteredCollection_DotChance);
        }
    }
}
