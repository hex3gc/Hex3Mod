using R2API;
using RoR2;
using RoR2.ExpansionManagement;
using UnityEngine;
using Hex3Mod.HelperClasses;
using VoidItemAPI;
using Hex3Mod.Utils;
using static Hex3Mod.Main;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using System.ComponentModel;

namespace Hex3Mod.Items
{
    /*
    Captain's Favor provides an alternative to the consumable, high-reward nature of Tickets, instead giving players a constant generalized reward of more items
    */
    public static class CaptainsFavor
    {
        static string itemName = "CaptainsFavor";
        static string upperName = itemName.ToUpper();
        public static ItemDef itemDef;
        public static GameObject LoadPrefab()
        {
            GameObject pickupModelPrefab = MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/CaptainsFavorPrefab.prefab");
            if (debugMode)
            {
                pickupModelPrefab.GetComponentInChildren<Renderer>().gameObject.AddComponent<MaterialControllerComponents.HGControllerFinder>();
            }
            return pickupModelPrefab;
        }
        public static Sprite LoadSprite()
        {
            return MainAssets.LoadAsset<Sprite>("Assets/Icons/CaptainsFavor.png");
        }
        public static Sprite LoadBuffSprite()
        {
            return MainAssets.LoadAsset<Sprite>("Assets/VFXPASS3/Icons/Buff_CaptainsFavor.png");
        }
        static GameObject potentialPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/OptionPickup/OptionPickup.prefab").WaitForCompletion();
        static GameObject chestKillPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/TreasureCacheVoid/VoidCacheOpenExplosion.prefab").WaitForCompletion();

        public static ItemDef CreateItem()
        {
            ItemDef item = ScriptableObject.CreateInstance<ItemDef>();

            item.name = itemName;
            item.nameToken = "H3_" + upperName + "_NAME";
            item.pickupToken = "H3_" + upperName + "_PICKUP";
            item.descriptionToken = "H3_" + upperName + "_DESC";
            item.loreToken = "H3_" + upperName + "_LORE";

            item.tags = new ItemTag[]{ ItemTag.Utility, ItemTag.AIBlacklist, ItemTag.BrotherBlacklist };
            item.deprecatedTier = ItemTier.VoidTier1;
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
                        childName = "Pelvis",
                        localPos = new Vector3(0.19271F, -0.00003F, 0.04374F),
                        localAngles = new Vector3(288.0233F, 244.3802F, 114.5137F),
                        localScale = new Vector3(0.21322F, 0.21322F, 0.21322F)
                    }
                }
            );
            rules.Add("mdlHuntress", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "HandL",
                        localPos = new Vector3(-0.01511F, 0.05672F, -0.04692F),
                        localAngles = new Vector3(58.18119F, 86.20083F, 88.43928F),
                        localScale = new Vector3(0.17556F, 0.17556F, 0.17556F)
                    }
                }
            );
            rules.Add("mdlToolbot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(0.39249F, 1.42169F, -1.99632F),
                        localAngles = new Vector3(8.81671F, 341.1347F, 294.7228F),
                        localScale = new Vector3(1.37533F, 1.37533F, 1.37533F)
                    }
                }
            );
            rules.Add("mdlEngi", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(-0.21986F, -0.00005F, 0.11161F),
                        localAngles = new Vector3(286.1519F, 112.8171F, 315.7423F),
                        localScale = new Vector3(0.22062F, 0.22062F, 0.22062F)
                    }
                }
            );
            rules.Add("mdlMage", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(-0.20292F, 0.04521F, -0.22428F),
                        localAngles = new Vector3(9.10053F, 253.2964F, 18.96548F),
                        localScale = new Vector3(0.18893F, 0.18893F, 0.18893F)
                    }
                }
            );
            rules.Add("mdlMerc", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "ThighL",
                        localPos = new Vector3(0.07843F, -0.02536F, -0.0621F),
                        localAngles = new Vector3(281.2404F, 1.5235F, 246.6278F),
                        localScale = new Vector3(0.22681F, 0.22681F, 0.22681F)
                    }
                }
            );
            rules.Add("mdlTreebot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "WeaponPlatform",
                        localPos = new Vector3(0.17098F, 0.01482F, 0.22337F),
                        localAngles = new Vector3(72.32838F, 289.0612F, 239.6884F),
                        localScale = new Vector3(0.45311F, 0.45311F, 0.45311F)
                    }
                }
            );
            rules.Add("mdlLoader", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "ThighL",
                        localPos = new Vector3(0.07165F, 0.04776F, 0.15439F),
                        localAngles = new Vector3(330.304F, 65.97124F, 91.43171F),
                        localScale = new Vector3(0.2498F, 0.2498F, 0.2498F)
                    }
                }
            );
            rules.Add("mdlCroco", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "SpineChest1",
                        localPos = new Vector3(0.70543F, 0.22722F, 3.51065F),
                        localAngles = new Vector3(301.1924F, 39.61919F, 259.5851F),
                        localScale = new Vector3(2.09619F, 2.09619F, 2.10439F)
                    }
                }
            );
            rules.Add("mdlCaptain", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "HandR",
                        localPos = new Vector3(0.0234F, 0.20076F, -0.02223F),
                        localAngles = new Vector3(353.9582F, 3.41899F, 12.58068F),
                        localScale = new Vector3(0.29051F, 0.29051F, 0.29051F)
                    }
                }
            );
            rules.Add("mdlBandit2", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Hat",
                        localPos = new Vector3(0.09874F, 0.05098F, -0.02968F),
                        localAngles = new Vector3(73.3124F, 346.5022F, 10.95594F),
                        localScale = new Vector3(0.16652F, 0.16652F, 0.16652F)
                    }
                }
            );
            rules.Add("EngiTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(0F, 0.74932F, -1.58013F),
                        localAngles = new Vector3(0F, 346.9402F, 300.2751F),
                        localScale = new Vector3(0.88916F, 0.88916F, 0.88916F)
                    }
                }
            );
            rules.Add("EngiWalkerTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(0.48721F, 1.54461F, -0.48716F),
                        localAngles = new Vector3(283.4064F, 21.09174F, 6.65518F),
                        localScale = new Vector3(0.9191F, 0.9191F, 0.9191F)
                    }
                }
            );
            rules.Add("mdlScav", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Backpack",
                        localPos = new Vector3(8.15228F, 6.10287F, -0.07035F),
                        localAngles = new Vector3(339.7366F, 14.63561F, 231.4966F),
                        localScale = new Vector3(2.95015F, 2.95015F, 2.95015F)
                    }
                }
            );
            rules.Add("mdlRailGunner", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "CalfR",
                        localPos = new Vector3(-0.05798F, -0.03074F, -0.1312F),
                        localAngles = new Vector3(317.8916F, 346.8395F, 348.7143F),
                        localScale = new Vector3(0.35865F, 0.35865F, 0.35865F)
                    }
                }
            );
            rules.Add("mdlVoidSurvivor", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(0.15158F, 0.16286F, -0.01573F),
                        localAngles = new Vector3(302.3738F, 134.1664F, 64.0929F),
                        localScale = new Vector3(0.16103F, 0.16103F, 0.1838F)
                    }
                }
            );

            return rules;
        }

        public static void AddTokens()
        {
            LanguageAPI.Add("H3_" + upperName + "_NAME", "Captain's Favor");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "The first chests you open each stage will be replaced by a Void Potential. <style=cIsVoid>Corrupts all 400 Tickets.</style>");
            LanguageAPI.Add("H3_" + upperName + "_LORE", "Don't spend it all in one place...\n\nOr, better yet, don't spend it. That's MY card!");
        }
        public static void UpdateItemStatus(Run run)
        {
            if (!CaptainsFavor_Enable.Value)
            {
                LanguageAPI.AddOverlay("H3_" + upperName + "_NAME", "Captain's Favor" + " <style=cDeath>[DISABLED]</style>");
                if (run && run.availableItems.Contains(itemDef.itemIndex)) { run.availableItems.Remove(itemDef.itemIndex); }
            }
            else
            {
                LanguageAPI.AddOverlay("H3_" + upperName + "_NAME", "Captain's Favor");
                if (run && !run.availableItems.Contains(itemDef.itemIndex) && run.IsExpansionEnabled(Hex3ModExpansion)) { run.availableItems.Add(itemDef.itemIndex); }
            }
            LanguageAPI.AddOverlay("H3_" + upperName + "_DESC", "The first <style=cStack>(+1 per stack)</style> chest you open each stage will be replaced by a <style=cIsVoid>Void Potential</style>, inheriting the chest's item tiers. <style=cIsVoid>Corrupts all 400 Tickets.</style>");
        }

        private static void AddHooks()
        {
            CostTypeIndex[] blacklistedCostTypes = new CostTypeIndex[9]
            {
                CostTypeIndex.LunarItemOrEquipment,
                CostTypeIndex.WhiteItem,
                CostTypeIndex.GreenItem,
                CostTypeIndex.RedItem,
                CostTypeIndex.BossItem,
                CostTypeIndex.ArtifactShellKillerItem,
                CostTypeIndex.VolatileBattery,
                CostTypeIndex.TreasureCacheVoidItem,
                CostTypeIndex.TreasureCacheItem
            };

            // Void transformation
            VoidTransformation.CreateTransformation(itemDef, "FourHundredTickets");

            // Give item holders an ItemBehavior
            void CharacterBody_OnInventoryChanged(On.RoR2.CharacterBody.orig_OnInventoryChanged orig, CharacterBody self)
            {
                orig(self);
                if (!self.inventory) { return; }
                int itemCount = self.inventory.GetItemCount(itemDef);
                if (itemCount > 0 && self.master && self.master.playerCharacterMasterController)
                {
                    VoidTicketsBehavior component;
                    component = self.GetComponent<VoidTicketsBehavior>();
                    if (!component)
                    {
                        component = self.AddItemBehavior<VoidTicketsBehavior>(1);
                    }
                    component.stack = itemCount;
                    component.usesLeft = itemCount - component.timesUsedThisStage;
                    self.SetBuffCount(captainsFavorBuff.buffIndex, component.usesLeft);
                }
                else
                {
                    if (self.GetComponent<VoidTicketsBehavior>())
                    {
                        UnityEngine.Object.Destroy(self.GetComponent<VoidTicketsBehavior>());
                        self.SetBuffCount(captainsFavorBuff.buffIndex, 0);
                    }
                }
            }

            void PurchaseInteraction_OnInteractionBegin(On.RoR2.PurchaseInteraction.orig_OnInteractionBegin orig, PurchaseInteraction self, Interactor activator)
            {
                if (self.isShrine || self.isGoldShrine || blacklistedCostTypes.Contains(self.costType))
                {
                    orig(self, activator);
                    return;
                }
                CharacterBody body = activator.GetComponent<CharacterBody>();
                if (body)
                {
                    VoidTicketsBehavior component = body.GetComponent<VoidTicketsBehavior>();
                    if (component)
                    {
                        component.interactions.Add(self);
                    }
                }
                orig(self, activator);
            }

            void ChestBehavior_ItemDrop(On.RoR2.ChestBehavior.orig_ItemDrop orig, ChestBehavior self)
            {
                if (self.gameObject.GetComponent<PurchaseInteraction>())
                {
                    foreach (VoidTicketsBehavior behavior in Object.FindObjectsOfType<VoidTicketsBehavior>())
                    {
                        if (behavior && behavior.interactions.Contains(self.gameObject.GetComponent<PurchaseInteraction>()) && behavior.usesLeft > 0)
                        {
                            ItemTier firstDropTier;
                            PickupIndex[] generatedDrops = self.dropTable.GenerateUniqueDrops(3, self.rng);
                            if (generatedDrops[0].itemIndex != ItemIndex.None && ItemCatalog.GetItemDef(generatedDrops[0].itemIndex).tier != ItemTier.NoTier)
                            {
                                firstDropTier = ItemCatalog.GetItemDef(generatedDrops[0].itemIndex).tier;
                            }
                            else
                            {
                                firstDropTier = ItemTier.Tier1;
                            }

                            PickupDropletController.CreatePickupDroplet(new GenericPickupController.CreatePickupInfo
                            {
                                pickerOptions = PickupPickerController.GenerateOptionsFromArray(generatedDrops),
                                prefabOverride = potentialPrefab,
                                position = self.dropTransform.position + Vector3.up * 1.5f,
                                rotation = Quaternion.identity,
                                pickupIndex = PickupCatalog.FindPickupIndex(firstDropTier)
                            }, 
                            self.dropTransform.position, Vector3.up * self.dropUpVelocityStrength + self.dropTransform.forward * self.dropForwardVelocityStrength);
                            behavior.interactions.Remove(self.gameObject.GetComponent<PurchaseInteraction>());
                            if (behavior.interactions.Count > 500) { behavior.interactions.Clear(); }
                            behavior.usesLeft--;
                            behavior.timesUsedThisStage++;
                            behavior.body.SetBuffCount(captainsFavorBuff.buffIndex, behavior.usesLeft);

                            EffectData effectData = new EffectData
                            {
                                origin = self.transform.position
                            };
                            EffectManager.SpawnEffect(chestKillPrefab, effectData, false);
                            Object.Destroy(self.gameObject);

                            return;
                        }
                    }
                }
                orig(self);
            }

            void SceneDirector_PopulateScene(On.RoR2.SceneDirector.orig_PopulateScene orig, SceneDirector self)
            {
                orig(self);
                foreach (PlayerCharacterMasterController playerCharacterMasterController in PlayerCharacterMasterController.instances)
                {
                    if (playerCharacterMasterController.master && playerCharacterMasterController.master.GetBody())
                    {
                        VoidTicketsBehavior component = playerCharacterMasterController.master.GetBody().GetComponent<VoidTicketsBehavior>();
                        if (component)
                        {
                            component.timesUsedThisStage = 0;
                            component.usesLeft = component.stack;
                            component.body.SetBuffCount(captainsFavorBuff.buffIndex, component.usesLeft);
                        }
                    }
                }
            }

            On.RoR2.CharacterBody.OnInventoryChanged += CharacterBody_OnInventoryChanged;
            On.RoR2.PurchaseInteraction.OnInteractionBegin += PurchaseInteraction_OnInteractionBegin;
            On.RoR2.ChestBehavior.ItemDrop += ChestBehavior_ItemDrop;
            On.RoR2.SceneDirector.PopulateScene += SceneDirector_PopulateScene;
        }

        public class VoidTicketsBehavior : CharacterBody.ItemBehavior
        {
            public List<PurchaseInteraction> interactions = new List<PurchaseInteraction>();
            public int usesLeft = 0;
            public int timesUsedThisStage = 0;
        }

        public static BuffDef captainsFavorBuff { get; private set; }
        public static void AddBuffs() // Visual indicator of Apathy stacks
        {
            captainsFavorBuff = ScriptableObject.CreateInstance<BuffDef>();
            captainsFavorBuff.buffColor = new Color(1f, 0.6f, 0.94f);
            captainsFavorBuff.canStack = true;
            captainsFavorBuff.isDebuff = false;
            captainsFavorBuff.name = "Captain's Favor Potentials Left";
            captainsFavorBuff.isHidden = false;
            captainsFavorBuff.isCooldown = false;
            captainsFavorBuff.iconSprite = LoadBuffSprite();
            ContentAddition.AddBuffDef(captainsFavorBuff);
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
