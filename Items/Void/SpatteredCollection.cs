using R2API;
using RoR2;
using RoR2.ExpansionManagement;
using System.Linq;
using UnityEngine;
using Hex3Mod.HelperClasses;
using VoidItemAPI;
using Hex3Mod.Utils;
using static Hex3Mod.Main;
using System;

namespace Hex3Mod.Items
{
    /*
    The void item counterpart for Scattered Reflection. Meant to be paired with Drop Of Necrosis for a blight build, allowing damage scaling in a fully void build
    Replaces your bleeds with blight, but makes Blight stronger.
    */
    public static class SpatteredCollection
    {
        static string itemName = "SpatteredCollection";
        static string upperName = itemName.ToUpper();
        public static ItemDef itemDef;
        public static GameObject LoadPrefab()
        {
            GameObject pickupModelPrefab = MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/SpatteredCollectionPrefab.prefab");
            if (debugMode)
            {
                pickupModelPrefab.GetComponentInChildren<Renderer>().gameObject.AddComponent<MaterialControllerComponents.HGControllerFinder>();
            }
            return pickupModelPrefab;
        }
        public static Sprite LoadSprite()
        {
            return MainAssets.LoadAsset<Sprite>("Assets/Icons/SpatteredCollection.png");
        }
        public static ItemDef CreateItem()
        {
            ItemDef item = ScriptableObject.CreateInstance<ItemDef>();

            item.name = itemName;
            item.nameToken = "H3_" + upperName + "_NAME";
            item.pickupToken = "H3_" + upperName + "_PICKUP";
            item.descriptionToken = "H3_" + upperName + "_DESC";
            item.loreToken = "H3_" + upperName + "_LORE";

            item.tags = new ItemTag[]{ ItemTag.Damage };
            item.deprecatedTier = ItemTier.VoidTier2;
            item.canRemove = true;
            item.hidden = false;
            item.requiredExpansion = Hex3ModExpansion;

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

        public static void AddTokens()
        {
            LanguageAPI.Add("H3_" + upperName + "_NAME", "Spattered Collection");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Your attacks may inflict a potent <style=cIsDamage>Blight</style> which melts through enemies' armor. <style=cIsVoid>Corrupts all Scattered Reflections.</style>");
            LanguageAPI.Add("H3_" + upperName + "_LORE", "\"I don't remember my name... I don't, not at all. That's all gone.\"" +
            "\n\n<style=cStack>(Several minutes of silence, broken up by what sounds like shuffles and clattering of glass)</style>" +
            "\n\n\"My collection. It's wonderful, isn't it? I want you to see it. Please, come see my collection... Bubbling, stinging poisons, corrosive to the touch... If you listen closely it's bubbling, here...\"" +
            "\n\n<style=cStack>(Bubbling sounds, mixed with heavy breathing. The audio recording ends shortly after.)</style>");
        }
        public static void UpdateItemStatus(Run run)
        {
            if (!SpatteredCollection_Enable.Value)
            {
                LanguageAPI.AddOverlay("H3_" + upperName + "_NAME", "Spattered Collection" + " <style=cDeath>[DISABLED]</style>");
                if (run && run.availableItems.Contains(itemDef.itemIndex)) { run.availableItems.Remove(itemDef.itemIndex); }
            }
            else
            {
                LanguageAPI.AddOverlay("H3_" + upperName + "_NAME", "Spattered Collection");
                if (run && !run.availableItems.Contains(itemDef.itemIndex) && run.IsExpansionEnabled(Hex3ModExpansion)) { run.availableItems.Add(itemDef.itemIndex); }
            }
            LanguageAPI.AddOverlay("H3_" + upperName + "_DESC", string.Format("Your attacks have a <style=cIsDamage>{1}%</style> chance to inflict <style=cIsDamage>Blight</style>, which now <style=cIsDamage>reduces enemies' armor by {0}</style> <style=cStack>(+2 per stack)</style> for each stack. <style=cIsVoid>Corrupts all Scattered Reflections.</style>", SpatteredCollection_ArmorReduction.Value, SpatteredCollection_DotChance.Value));
        }

        private static void AddHooks() // Insert hooks here
        {
            // Void transformation
            VoidTransformation.CreateTransformation(itemDef, "ScatteredReflection");

            void GetStatCoefficients(CharacterBody body, RecalculateStatsAPI.StatHookEventArgs args) // Reduce armor based on Blight stacks
            {
                if (body.teamComponent && (body.teamComponent.teamIndex == TeamIndex.Monster || body.teamComponent.teamIndex == TeamIndex.Lunar || body.teamComponent.teamIndex == TeamIndex.Void) && Util.GetItemCountForTeam(TeamIndex.Player, itemDef.itemIndex, true) > 0)
                {
                    if (body.GetBuffCount(RoR2Content.Buffs.Blight) > 0)
                    {
                        args.armorAdd -= (SpatteredCollection_ArmorReduction.Value * Util.GetItemCountForTeam(TeamIndex.Player, itemDef.itemIndex, true)) * body.GetBuffCount(RoR2Content.Buffs.Blight);
                    }
                }
                if (body.teamComponent && body.teamComponent.teamIndex == TeamIndex.Player && Util.GetItemCountForTeam(TeamIndex.Monster, itemDef.itemIndex, true) + Util.GetItemCountForTeam(TeamIndex.Lunar, itemDef.itemIndex, true) + Util.GetItemCountForTeam(TeamIndex.Void, itemDef.itemIndex, true) > 0)
                {
                    if (body.GetBuffCount(RoR2Content.Buffs.Blight) > 0)
                    {
                        args.armorAdd -= (SpatteredCollection_ArmorReduction.Value * (Util.GetItemCountForTeam(TeamIndex.Monster, itemDef.itemIndex, true) + Util.GetItemCountForTeam(TeamIndex.Lunar, itemDef.itemIndex, true) + Util.GetItemCountForTeam(TeamIndex.Void, itemDef.itemIndex, true))) * body.GetBuffCount(RoR2Content.Buffs.Blight);
                    }
                }
            }

            RecalculateStatsAPI.GetStatCoefficients += GetStatCoefficients;

            On.RoR2.GlobalEventManager.OnHitEnemy += (orig, self, damageInfo, victim) =>
            {
                if (damageInfo.attacker && damageInfo.attacker.TryGetComponent(out CharacterBody body1) && body1.inventory && body1.inventory.GetItemCount(itemDef) > 0)
                {
                    if (body1.master && damageInfo.dotIndex != DotController.DotIndex.Blight && damageInfo.attacker != victim)
                    {
                        if (damageInfo.damageType != DamageType.DoT && damageInfo.damageType != DamageType.FallDamage && damageInfo.damage > 0f)
                        {
                            if (Util.CheckRoll((SpatteredCollection_DotChance.Value * body1.inventory.GetItemCount(itemDef)) * damageInfo.procCoefficient, body1.master.luck))
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

        public static void Initiate()
        {
            itemDef = CreateItem();
            ItemAPI.Add(new CustomItem(itemDef, CreateDisplayRules()));
            AddTokens();
            UpdateItemStatus(null);
            AddHooks();
        }
    }
}
