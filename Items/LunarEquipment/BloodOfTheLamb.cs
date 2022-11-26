using R2API;
using RoR2;
using UnityEngine;
using Hex3Mod.HelperClasses;
using System.Collections.Generic;
using System.Linq;
using RoR2;

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
        static EquipmentDef equipDefinition = CreateEquip();
        public static GameObject LoadPrefab()
        {
            GameObject pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/ShardOfGlassPrefab.prefab");
            return pickupModelPrefab;
        }
        public static Sprite LoadSprite()
        {
            Sprite pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/ShardOfGlass.png");
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
                        localPos = new Vector3(0.21708F, 0.49989F, -0.0198F),
                        localAngles = new Vector3(300.1077F, 191.7878F, 31.30767F),
                        localScale = new Vector3(0.10416F, 0.10416F, 0.10416F)
                    }
                }
            );
            rules.Add("mdlHuntress", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "UpperArmR",
                        localPos = new Vector3(-0.04204F, 0.3909F, -0.08947F),
                        localAngles = new Vector3(305.158F, 128.5434F, 241.4366F),
                        localScale = new Vector3(0.0859F, 0.0859F, 0.0859F)
                    }
                }
            );
            rules.Add("mdlToolbot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(-1.98627F, 2.50041F, -0.81139F),
                        localAngles = new Vector3(6.48878F, 50.8491F, 42.82001F),
                        localScale = new Vector3(0.78954F, 0.78954F, 0.78954F)
                    }
                }
            );
            rules.Add("mdlEngi", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "HandL",
                        localPos = new Vector3(-0.06379F, 0.15714F, -0.02627F),
                        localAngles = new Vector3(70.9774F, 52.81272F, 261.3786F),
                        localScale = new Vector3(0.14769F, 0.14769F, 0.14769F)
                    }
                }
            );
            rules.Add("mdlMage", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Stomach",
                        localPos = new Vector3(-0.12536F, 0.14136F, 0.10847F),
                        localAngles = new Vector3(310.494F, 40.69888F, 318.9477F),
                        localScale = new Vector3(0.09897F, 0.09897F, 0.09897F)
                    }
                }
            );
            rules.Add("mdlMerc", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "FootR",
                        localPos = new Vector3(0.00465F, 0.10424F, -0.15497F),
                        localAngles = new Vector3(348.0789F, 137.4101F, 190.6102F),
                        localScale = new Vector3(0.088F, 0.088F, 0.088F)
                    }
                }
            );
            rules.Add("mdlTreebot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "PlatformBase",
                        localPos = new Vector3(-0.40513F, 0.74024F, 0.42998F),
                        localAngles = new Vector3(9.43343F, 55.30598F, 41.14965F),
                        localScale = new Vector3(0.20768F, 0.20768F, 0.20768F)
                    }
                }
            );
            rules.Add("mdlLoader", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(-0.00024F, 0.12501F, 0.25337F),
                        localAngles = new Vector3(25.9752F, 47.40374F, 25.39513F),
                        localScale = new Vector3(0.11278F, 0.11278F, 0.11278F)
                    }
                }
            );
            rules.Add("mdlCroco", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "SpineChest1",
                        localPos = new Vector3(1.40569F, 1.67903F, 1.83F),
                        localAngles = new Vector3(335.2653F, 21.50072F, 285.631F),
                        localScale = new Vector3(0.8967F, 0.8967F, 0.90021F)
                    }
                }
            );
            rules.Add("mdlCaptain", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "CalfL",
                        localPos = new Vector3(-0.21595F, 0.37524F, 0.0112F),
                        localAngles = new Vector3(327.5428F, 347.9001F, 28.43982F),
                        localScale = new Vector3(0.17073F, 0.17073F, 0.17073F)
                    }
                }
            );
            rules.Add("mdlBandit2", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(-0.2345F, -0.1407F, 0.05543F),
                        localAngles = new Vector3(53.97828F, 36.91263F, 81.21328F),
                        localScale = new Vector3(0.10738F, 0.10738F, 0.10738F)
                    }
                }
            );
            rules.Add("EngiTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Base",
                        localPos = new Vector3(0.29738F, 1.32457F, 0.25339F),
                        localAngles = new Vector3(2.91952F, 16.66828F, 290.7995F),
                        localScale = new Vector3(0.36967F, 0.36967F, 0.36967F)
                    }
                }
            );
            rules.Add("EngiWalkerTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Base",
                        localPos = new Vector3(-0.17481F, 1.25268F, 0.36514F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.36967F, 0.36967F, 0.36967F)
                    }
                }
            );
            rules.Add("mdlScav", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "HandL",
                        localPos = new Vector3(0.34979F, 2.53159F, -0.53631F),
                        localAngles = new Vector3(68.08115F, 20.07728F, 313.8858F),
                        localScale = new Vector3(1.69716F, 1.69716F, 1.69716F)
                    }
                }
            );
            rules.Add("mdlRailGunner", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "CalfL",
                        localPos = new Vector3(0.04085F, 0.3535F, -0.03918F),
                        localAngles = new Vector3(339.1311F, 105.2301F, 215.211F),
                        localScale = new Vector3(0.17307F, 0.17307F, 0.17307F)
                    }
                }
            );
            rules.Add("mdlVoidSurvivor", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(0.17212F, 0.19905F, 0.12029F),
                        localAngles = new Vector3(353.8923F, 134.0595F, 356.0528F),
                        localScale = new Vector3(0.10439F, 0.10439F, 0.10439F)
                    }
                }
            );

            return rules;
        }

        public static void AddTokens(int BloodOfTheLamb_ItemsTaken)
        {
            LanguageAPI.Add("H3_" + upperName + "_NAME", "Blood Of The Lamb");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "<style=cDeath>Purge " + BloodOfTheLamb_ItemsTaken + " of your items</style> for a <style=cShrine>random boss item.</style>");
            LanguageAPI.Add("H3_" + upperName + "_DESC", "<style=cDeath>Purge " + BloodOfTheLamb_ItemsTaken + " of your items</style> for a <style=cShrine>random boss item.</style> Any item except <style=cIsUtility>Lunar</style>, <style=cStack>Tierless</style>, or <style=cShrine>Boss</style> items may be purged.");
            LanguageAPI.Add("H3_" + upperName + "_LORE", "");
        }

        private static void AddHooks(EquipmentDef equipmentDef, int BloodOfTheLamb_ItemsTaken)
        {
            bool PerformEquipmentAction(On.RoR2.EquipmentSlot.orig_PerformEquipmentAction orig, EquipmentSlot self, EquipmentDef heldEquipmentDef)
            {
                if (equipmentDef == heldEquipmentDef)
                {
                    if (self.characterBody && self.characterBody.inventory && self.characterBody.master)
                    {
                        int totalApplicableItemCount = self.characterBody.inventory.GetTotalItemCountOfTier(ItemTier.Tier1) + self.characterBody.inventory.GetTotalItemCountOfTier(ItemTier.Tier2) + self.characterBody.inventory.GetTotalItemCountOfTier(ItemTier.Tier3) + self.characterBody.inventory.GetTotalItemCountOfTier(ItemTier.VoidTier1) + self.characterBody.inventory.GetTotalItemCountOfTier(ItemTier.VoidTier2) + self.characterBody.inventory.GetTotalItemCountOfTier(ItemTier.VoidTier3);
                        if (totalApplicableItemCount >= BloodOfTheLamb_ItemsTaken)
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
                            for (int i = 0; i < BloodOfTheLamb_ItemsTaken; i++) // Takes random item each loop
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

        public static void Initiate(int BloodOfTheLamb_ItemsTaken)
        {
            ItemAPI.Add(new CustomEquipment(equipDefinition, CreateDisplayRules()));
            AddTokens(BloodOfTheLamb_ItemsTaken);
            AddHooks(equipDefinition, BloodOfTheLamb_ItemsTaken);
        }
    }
}
