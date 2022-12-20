using R2API;
using RoR2;
using UnityEngine;
using Hex3Mod.HelperClasses;
using System.Collections.Generic;
using System.Linq;
using Hex3Mod.Utils;
using static Hex3Mod.Main;
using System;

namespace Hex3Mod.Items
{
    /*
    Blood Of The Lamb gives you an opportunity to gain boss items that may come rarely in normal games
    The risk of using it is that sacrificed items are random, so in return for a legendary you may get a titanic knurl...
    Another neat thing about it is that it can purge unwanted void items.
    */
    public class BloodOfTheLamb
    {
        static string equipName = "BloodOfTheLamb";
        static string upperName = equipName.ToUpper();
        static EquipmentDef equipmentDef;
        public static GameObject LoadPrefab()
        {
            GameObject pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/BloodOfTheLambPrefab.prefab");
            if (Main.debugMode == true)
            {
                pickupModelPrefab.GetComponentInChildren<Renderer>().gameObject.AddComponent<MaterialControllerComponents.HGControllerFinder>();
            }
            return pickupModelPrefab;
        }
        public static Sprite LoadSprite()
        {
            Sprite pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/BloodOfTheLamb.png");
            return pickupIconSprite;
        }
        public static EquipmentDef CreateEquip()
        {
            EquipmentDef equipment = ScriptableObject.CreateInstance<EquipmentDef>();

            equipment.name = equipName;
            equipment.nameToken = "H3_" + upperName + "_NAME";
            equipment.pickupToken = "H3_" + upperName + "_PICKUP";
            equipment.descriptionToken = "H3_" + upperName + "_DESC";
            equipment.loreToken = "H3_" + upperName + "_LORE";

            equipment.cooldown = 120f;
            equipment.canDrop = true;
            equipment.enigmaCompatible = false;
            equipment.canBeRandomlyTriggered = false;
            equipment.isBoss = false;
            equipment.isLunar = true;
            equipment.requiredExpansion = Hex3ModExpansion;

            equipment.pickupModelPrefab = LoadPrefab();
            equipment.pickupIconSprite = LoadSprite();

            return equipment;
        }

        public static ItemDisplayRuleDict CreateDisplayRules()
        {
            GameObject ItemDisplayPrefab = helpers.PrepareItemDisplayModel(PrefabAPI.InstantiateClone(LoadPrefab(), LoadPrefab().name + "Display", false));

            ItemDisplayRuleDict rules = new ItemDisplayRuleDict();
            rules.Add("mdlCommandoDualies", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(-0.18288F, 0.48508F, -0.02452F),
                        localAngles = new Vector3(315.3539F, 190.3651F, 2.05826F),
                        localScale = new Vector3(0.25057F, 0.25057F, 0.25057F)
                    }
                }
            );
            rules.Add("mdlHuntress", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "UpperArmR",
                        localPos = new Vector3(-0.15584F, 0.02475F, -0.51502F),
                        localAngles = new Vector3(312.8542F, 164.091F, 251.0403F),
                        localScale = new Vector3(0.46502F, 0.46502F, 0.46502F)
                    }
                }
            );
            rules.Add("mdlToolbot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "UpperArmL",
                        localPos = new Vector3(-0.75196F, 0.23989F, 0.1797F),
                        localAngles = new Vector3(5.12917F, 0.96534F, 304.8264F),
                        localScale = new Vector3(2.92298F, 2.92298F, 2.92298F)
                    }
                }
            );
            rules.Add("mdlEngi", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(-0.6306F, 0.15716F, -0.02998F),
                        localAngles = new Vector3(58.87991F, 230.906F, 182.7502F),
                        localScale = new Vector3(0.47396F, 0.47396F, 0.47396F)
                    }
                }
            );
            rules.Add("mdlMage", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "HandL",
                        localPos = new Vector3(-0.05974F, -0.07437F, 0.18598F),
                        localAngles = new Vector3(352.552F, 339.6735F, 311.3987F),
                        localScale = new Vector3(0.41609F, 0.41609F, 0.41609F)
                    }
                }
            );
            rules.Add("mdlMerc", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "HandR",
                        localPos = new Vector3(0.1683F, 0.19383F, 0.03806F),
                        localAngles = new Vector3(306.1627F, 338.5605F, 233.6912F),
                        localScale = new Vector3(0.53493F, 0.53493F, 0.53493F)
                    }
                }
            );
            rules.Add("mdlTreebot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "PlatformBase",
                        localPos = new Vector3(1.26817F, 0.45629F, 0.1303F),
                        localAngles = new Vector3(9.43352F, 91.78703F, 41.14963F),
                        localScale = new Vector3(0.7384F, 0.7384F, 0.7384F)
                    }
                }
            );
            rules.Add("mdlLoader", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Base",
                        localPos = new Vector3(0.50006F, 0.12501F, -0.75347F),
                        localAngles = new Vector3(25.9752F, 47.40374F, 25.39513F),
                        localScale = new Vector3(0.34672F, 0.34672F, 0.34672F)
                    }
                }
            );
            rules.Add("mdlCroco", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "ThighL",
                        localPos = new Vector3(1.6677F, -1.13498F, -0.79392F),
                        localAngles = new Vector3(356.2261F, 13.94416F, 13.58796F),
                        localScale = new Vector3(2.69721F, 2.69721F, 2.70777F)
                    }
                }
            );
            rules.Add("mdlCaptain", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "LowerArmL",
                        localPos = new Vector3(-0.12553F, 0.25293F, -0.06317F),
                        localAngles = new Vector3(8.33136F, 89.70241F, 313.1871F),
                        localScale = new Vector3(0.34368F, 0.30762F, 0.34368F)
                    }
                }
            );
            rules.Add("mdlBandit2", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(-0.40512F, -0.14133F, 0.05839F),
                        localAngles = new Vector3(9.83305F, 357.5694F, 358.4125F),
                        localScale = new Vector3(0.27847F, 0.27847F, 0.27847F)
                    }
                }
            );
            rules.Add("EngiTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Base",
                        localPos = new Vector3(-1.0518F, 1.32456F, 0.25339F),
                        localAngles = new Vector3(71.16631F, 98.50111F, 48.69374F),
                        localScale = new Vector3(1.41534F, 1.41534F, 1.41534F)
                    }
                }
            );
            rules.Add("EngiWalkerTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Base",
                        localPos = new Vector3(-1.31098F, 1.52273F, 0F),
                        localAngles = new Vector3(78.6899F, 224.2303F, 179.9999F),
                        localScale = new Vector3(1.09362F, 1.09362F, 1.09362F)
                    }
                }
            );
            rules.Add("mdlScav", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(0.63858F, 4.34641F, -8.47121F),
                        localAngles = new Vector3(53.68382F, 191.9732F, 147.5359F),
                        localScale = new Vector3(6.20223F, 6.20223F, 6.20223F)
                    }
                }
            );
            rules.Add("mdlRailGunner", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "MuzzleSniper",
                        localPos = new Vector3(0.04086F, 0.3535F, -0.32963F),
                        localAngles = new Vector3(339.9185F, 262.4123F, 222.8827F),
                        localScale = new Vector3(0.3483F, 0.3483F, 0.3483F)
                    }
                }
            );
            rules.Add("mdlVoidSurvivor", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(-0.01238F, 0.12163F, 0.28027F),
                        localAngles = new Vector3(296.2607F, 228.937F, 95.92348F),
                        localScale = new Vector3(0.30837F, 0.30837F, 0.30837F)
                    }
                }
            );

            return rules;
        }

        public static void AddTokens()
        {
            LanguageAPI.Add("H3_" + upperName + "_NAME", "Blood Of The Lamb");
            LanguageAPI.Add("H3_" + upperName + "_LORE", "\nOrder: 0" +
            "\nTracking Number: <style=cIsUtility>0000000000000</style>" +
            "\nEstimated Delivery: <style=cIsUtility>00/00/0000</style>" +
            "\nShipping Method: Priority" +
            "\nShipping Address: <style=cIsUtility>0</style>" +
            "\nShipping Details:" +
            "\n\nThe dagger was reportedly found lodged edge-first into the surface of Earth's moon, stained in extremely old flakings of blood. When I say old, I mean ancient-- analysis dated it at least 5000 years into the past. What was also discovered was the creature that the blood originally belonged to: A sheep." +
            "\n\nAs we know, sheep never reached the moon, and neither had humans circa 3000 B.C... The most likely explanation is that it was placed there recently for some reason, but no traces of tampering or disturbance were found in the surrounding rock. Additionally, even after three thousand years, the dagger has remained sharp enough to pierce one of our teams' spacesuit and cause an emergency." +
            "\n\nThere's no avoiding it: This item has unusual, possibly even supernatural properties. We're sending it to <style=cIsUtility>0</style> for a prompt and in-depth analysis, with which we can start to piece together this object's" +
            "\n\n<style=cIsUtility>000000</style>" +
            "\n\n<style=cIsUtility>ORIGIN</style>");
        }
        public static void UpdateItemStatus()
        {
            if (!BloodOfTheLamb_Enable.Value)
            {
                LanguageAPI.AddOverlay("H3_" + upperName + "_NAME", "Blood Of The Lamb" + " <style=cDeath>[DISABLED]</style>");
            }
            else
            {
                LanguageAPI.AddOverlay("H3_" + upperName + "_NAME", "Blood Of The Lamb");
            }
            LanguageAPI.AddOverlay("H3_" + upperName + "_PICKUP", "<style=cDeath>Purge " + BloodOfTheLamb_ItemsTaken.Value + " of your items</style> for a <style=cShrine>random boss item.</style>");
            LanguageAPI.AddOverlay("H3_" + upperName + "_DESC", "<style=cDeath>Purge " + BloodOfTheLamb_ItemsTaken.Value + " of your items</style> for a <style=cShrine>random boss item.</style> Any item except <style=cIsUtility>Lunar</style>, <style=cStack>Tierless</style>, or <style=cShrine>Boss</style> items may be purged.");
        }

        private static void AddHooks()
        {
            bool PerformEquipmentAction(On.RoR2.EquipmentSlot.orig_PerformEquipmentAction orig, EquipmentSlot self, EquipmentDef heldEquipmentDef)
            {
                if (equipmentDef == heldEquipmentDef)
                {
                    if (self.characterBody && self.characterBody.inventory && self.characterBody.master)
                    {
                        int totalApplicableItemCount = self.characterBody.inventory.GetTotalItemCountOfTier(ItemTier.Tier1) + self.characterBody.inventory.GetTotalItemCountOfTier(ItemTier.Tier2) + self.characterBody.inventory.GetTotalItemCountOfTier(ItemTier.Tier3) + self.characterBody.inventory.GetTotalItemCountOfTier(ItemTier.VoidTier1) + self.characterBody.inventory.GetTotalItemCountOfTier(ItemTier.VoidTier2) + self.characterBody.inventory.GetTotalItemCountOfTier(ItemTier.VoidTier3);
                        if (totalApplicableItemCount >= BloodOfTheLamb_ItemsTaken.Value)
                        {
                            Util.PlaySound(EntityStates.ImpBossMonster.GroundPound.initialAttackSoundString, self.gameObject);
                            EffectData effectData = new EffectData
                            {
                                origin = self.characterBody.corePosition
                            };
                            EffectManager.SpawnEffect(EntityStates.VoidMegaCrab.BackWeapon.FireVoidMissiles.muzzleEffectPrefab, effectData, false);

                            Inventory inventory = self.characterBody.inventory;
                            List<ItemIndex> itemList = new List<ItemIndex>();
                            List<ItemDef> allItems = new List<ItemDef>(ItemCatalog.allItemDefs);
                            Xoroshiro128Plus rng = new Xoroshiro128Plus(Run.instance.stageRng.nextUlong);
                            ItemIndex givenItem = new ItemIndex();

                            Util.ShuffleList(allItems, rng);
                            rng.Next(); // RNG shuffle

                            foreach (ItemDef def in allItems) // Finds a random boss item to give (No aspects, scrap, artifact keys, Relic of Energy or Essence of Tar)
                            {
                                if (def.tier == ItemTier.Boss && def.name != RoR2Content.Items.ArtifactKey.name && !def.name.Contains("Aspect") && !def.name.Contains("Scrap") && !def.name.Contains("EssenceOfTar") && !def.name.Contains("RelicOfEnergy"))
                                {
                                    inventory.GiveItem(def);
                                    givenItem = def.itemIndex;
                                    break;
                                }
                            }
                            for (int i = 0; i < BloodOfTheLamb_ItemsTaken.Value; i++) // Takes random item each loop
                            {
                                itemList = inventory.itemAcquisitionOrder;
                                Util.ShuffleList(itemList, rng);
                                rng.Next(); // RNG shuffle
                                foreach (ItemIndex index in itemList) // Find first applicable item from shuffled list
                                {
                                    ItemDef def = ItemCatalog.GetItemDef(index);
                                    if (def.tier == ItemTier.Tier1 || def.tier == ItemTier.Tier2 || def.tier == ItemTier.Tier3 || def.tier == ItemTier.VoidTier1 || def.tier == ItemTier.VoidTier2 || def.tier == ItemTier.VoidTier3)
                                    {
                                        inventory.RemoveItem(def);
                                        CharacterMasterNotificationQueue.PushItemTransformNotification(self.characterBody.master, index, givenItem, CharacterMasterNotificationQueue.TransformationType.Default);
                                        break;
                                    }
                                }
                            }
                            return true;
                        }
                        Chat.AddMessage("<style=cStack>You aren't carrying enough for a proper sacrifice.</style>");
                        return true;
                    }
                    return orig(self, heldEquipmentDef);
                }
                else
                {
                    return orig(self, heldEquipmentDef);
                }
            }

            On.RoR2.EquipmentSlot.PerformEquipmentAction += PerformEquipmentAction;
        }

        public static void Initiate()
        {
            equipmentDef = CreateEquip();
            ItemAPI.Add(new CustomEquipment(equipmentDef, CreateDisplayRules()));
            AddTokens();
            UpdateItemStatus();
            AddHooks();
        }
    }
}
