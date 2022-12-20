using R2API;
using RoR2;
using UnityEngine;
using Hex3Mod.HelperClasses;
using Hex3Mod.Utils;
using UnityEngine.PlayerLoop;
using UnityEngine.Networking;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;
using RoR2.Projectile;
using RoR2.Items;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using System.Linq;
using EntityStates.Missions.Arena.NullWard;
using static Hex3Mod.Main;
using Hex3Mod.Logging;

namespace Hex3Mod.Items
{
    /*
    An item I added later because I liked the idea. Ever felt like holdout zones were too small? Want your allies to be in bungus range? This is the item for you.
    */
    public class OverkillOverdrive
    {
        static string itemName = "OverkillOverdrive";
        static string upperName = itemName.ToUpper();
        static ItemDef itemDef;
        public static GameObject LoadPrefab()
        {
            GameObject pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/OverkillOverdrivePrefab.prefab");
            if (Main.debugMode == true)
            {
                pickupModelPrefab.GetComponentInChildren<Renderer>().gameObject.AddComponent<MaterialControllerComponents.HGControllerFinder>();
            }
            return pickupModelPrefab;
        }
        public static Sprite LoadSprite()
        {
            Sprite pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/OverkillOverdrive.png");
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

            item.tags = new ItemTag[]{ ItemTag.Utility, ItemTag.HoldoutZoneRelated, ItemTag.AIBlacklist, ItemTag.BrotherBlacklist };
            item.deprecatedTier = ItemTier.Tier2;
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
                        localPos = new Vector3(0.02932F, 0.15739F, 0.17492F),
                        localAngles = new Vector3(40.37937F, 56.99835F, 47.6669F),
                        localScale = new Vector3(0.51137F, 0.51137F, 0.51137F)
                    }
                }
            );
            rules.Add("mdlHuntress", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "UpperArmL",
                        localPos = new Vector3(0.07187F, 0.12714F, -0.00512F),
                        localAngles = new Vector3(324.8661F, 61.10993F, 224.6511F),
                        localScale = new Vector3(0.29754F, 0.29754F, 0.29754F)
                    }
                }
            );
            rules.Add("mdlToolbot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(0.16963F, 1.28033F, 3.28989F),
                        localAngles = new Vector3(32.9379F, 50.76556F, 35.19836F),
                        localScale = new Vector3(3.28279F, 3.28279F, 3.28279F)
                    }
                }
            );
            rules.Add("mdlEngi", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.01875F, -0.05477F, -0.23042F),
                        localAngles = new Vector3(329.951F, 131.4691F, 209.9838F),
                        localScale = new Vector3(0.37683F, 0.37683F, 0.37683F)
                    }
                }
            );
            rules.Add("mdlMage", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Stomach",
                        localPos = new Vector3(-0.12576F, 0.1397F, 0.10673F),
                        localAngles = new Vector3(41.19534F, 347.6386F, 30.502F),
                        localScale = new Vector3(0.30644F, 0.30644F, 0.30644F)
                    }
                }
            );
            rules.Add("mdlMerc", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "UpperArmL",
                        localPos = new Vector3(0.09703F, 0.31073F, 0.03949F),
                        localAngles = new Vector3(340.1984F, 44.75924F, 206.5631F),
                        localScale = new Vector3(0.35621F, 0.35621F, 0.35621F)
                    }
                }
            );
            rules.Add("mdlTreebot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "PlatformBase",
                        localPos = new Vector3(-0.56661F, -0.00001F, 0F),
                        localAngles = new Vector3(32.41188F, 315.6955F, 32.89065F),
                        localScale = new Vector3(0.90093F, 0.90093F, 0.90093F)
                    }
                }
            );
            rules.Add("mdlLoader", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(0.15835F, 0.13885F, 0.23601F),
                        localAngles = new Vector3(37.26324F, 56.39846F, 42.37847F),
                        localScale = new Vector3(0.30961F, 0.30961F, 0.30961F)
                    }
                }
            );
            rules.Add("mdlCroco", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(1.39583F, 1.26191F, -1.94266F),
                        localAngles = new Vector3(31.73183F, 181.9521F, 10.41118F),
                        localScale = new Vector3(2.90436F, 2.90436F, 2.91573F)
                    }
                }
            );
            rules.Add("mdlCaptain", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "MuzzleGun",
                        localPos = new Vector3(-0.07456F, 0.01484F, -0.18739F),
                        localAngles = new Vector3(323.0191F, 311.0693F, 93.4054F),
                        localScale = new Vector3(0.33308F, 0.33308F, 0.33308F)
                    }
                }
            );
            rules.Add("mdlBandit2", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(-0.15341F, -0.02131F, -0.09669F),
                        localAngles = new Vector3(324.8036F, 204.0864F, 195.2549F),
                        localScale = new Vector3(0.25355F, 0.25355F, 0.25355F)
                    }
                }
            );
            rules.Add("EngiTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Base",
                        localPos = new Vector3(0.18428F, 1.32453F, 0.00293F),
                        localAngles = new Vector3(32.46166F, 117.6807F, 32.9138F),
                        localScale = new Vector3(1.2481F, 1.2481F, 1.22919F)
                    }
                }
            );
            rules.Add("EngiWalkerTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(-0.81344F, 0.80558F, -0.49835F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(1.08189F, 1.08189F, 1.08189F)
                    }
                }
            );
            rules.Add("mdlScav", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "MuzzleEnergyCannon",
                        localPos = new Vector3(-3.00158F, -2.37988F, -18.43572F),
                        localAngles = new Vector3(59.47304F, 186.4973F, 217.8052F),
                        localScale = new Vector3(7.09703F, 7.09703F, 7.09703F)
                    }
                }
            );
            rules.Add("mdlRailGunner", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Backpack",
                        localPos = new Vector3(0.12763F, 0.30365F, -0.10679F),
                        localAngles = new Vector3(27.44786F, 220.3247F, 28.54817F),
                        localScale = new Vector3(0.39887F, 0.39887F, 0.39887F)
                    }
                }
            );
            rules.Add("mdlVoidSurvivor", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "UpperArmL",
                        localPos = new Vector3(0.04071F, 0.25528F, -0.0247F),
                        localAngles = new Vector3(318.3246F, 78.82507F, 235.4018F),
                        localScale = new Vector3(0.40362F, 0.40362F, 0.40362F)
                    }
                }
            );

            return rules;
        }

        public static void AddTokens()
        {
            LanguageAPI.Add("H3_" + upperName + "_NAME", "Overkill Overdrive");
            LanguageAPI.Add("H3_" + upperName + "_LORE", "\"I want everyone to hear it, even if it doesn't sound good.\"\n\n- Written on a sticky note, attached to the package the item arrived in.");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Amplify the range of area buffs and holdout zones.");
        }
        public static void UpdateItemStatus()
        {
            if (!OverkillOverdrive_Enable.Value)
            {
                LanguageAPI.AddOverlay("H3_" + upperName + "_NAME", "Overkill Overdrive" + " <style=cDeath>[DISABLED]</style>");
            }
            else
            {
                LanguageAPI.AddOverlay("H3_" + upperName + "_NAME", "Overkill Overdrive");
            }
            LanguageAPI.AddOverlay("H3_" + upperName + "_DESC", string.Format("Amplify the radius of <style=cIsUtility>area buffs</style> and <style=cWorldEvent>holdout zones</style> by <style=cWorldEvent>{0}%</style> <style=cStack>(+{0}% per stack)</style>", OverkillOverdrive_ZoneIncrease.Value));
        }

        private static void AddHooks()
        {
            float FindTotalMultiplier() // Gets the appropriate radius multiplier based on all player inventories
            {
                int totalItemAmount = 0;
                foreach (TeamComponent ally in TeamComponent.GetTeamMembers(TeamIndex.Player))
                {
                    if (ally.body && OverkillOverdrive_TurretBlacklist.Value == true)
                    {
                        string trimmedName = ally.body.name.Replace("(Clone)", "").Trim();
                        if (trimmedName == "EngiTurretBody" || trimmedName == "EngiWalkerTurretBody")
                        {
                            continue;
                        }
                    }
                    if (ally.body && ally.body.inventory)
                    {
                        totalItemAmount += ally.body.inventory.GetItemCount(itemDef);
                    }
                }
                return totalItemAmount * (OverkillOverdrive_ZoneIncrease.Value / 100f);
            }

            // General holdout zones
            void HoldoutZoneController_OnEnable(On.RoR2.HoldoutZoneController.orig_OnEnable orig, HoldoutZoneController self)
            {
                orig(self);
                self.baseRadius += self.baseRadius * FindTotalMultiplier();
            }
            void Inventory_GiveItem_ItemIndex_int(On.RoR2.Inventory.orig_GiveItem_ItemIndex_int orig, Inventory self, ItemIndex itemIndex, int count)
            {
                orig(self, itemIndex, count);
                if (itemIndex == itemDef.itemIndex)
                {
                    foreach (HoldoutZoneController holdoutZone in GameObject.FindObjectsOfType<HoldoutZoneController>())
                    {
                        holdoutZone.baseRadius += (holdoutZone.currentRadius * OverkillOverdrive_ZoneIncrease.Value / 100f) * count;
                        holdoutZone.currentRadius += (holdoutZone.currentRadius * OverkillOverdrive_ZoneIncrease.Value / 100f) * count;
                    }
                }

                // Void fields cells
                NullWardBaseState.wardRadiusOff = 0.2f + (0.2f * FindTotalMultiplier());
                NullWardBaseState.wardRadiusOn = 15f + (15f * FindTotalMultiplier());
                NullWardBaseState.wardWaitingRadius = 5f + (5f * FindTotalMultiplier());
            }

            // Shrine of the Forest
            void ShrineHealingBehavior_SetWardEnabled(On.RoR2.ShrineHealingBehavior.orig_SetWardEnabled orig, ShrineHealingBehavior self, bool enableWard)
            {
                if (enableWard)
                {
                    self.baseRadius += self.baseRadius * FindTotalMultiplier();
                }
                orig(self, enableWard);
            }

            // Focus Crystal
            void CharacterBody_OnInventoryChanged(On.RoR2.CharacterBody.orig_OnInventoryChanged orig, CharacterBody self)
            {
                orig(self);
                if (self.TryGetComponent(out NearbyDamageBonusBodyBehavior behavior) == true && behavior.isActiveAndEnabled && !UltimateCustomRunCompatibility.enabled && !VanillaRebalanceCompatibility.enabled)
                {
                    float scaledSize = 1 + (1 * FindTotalMultiplier());
                    behavior.nearbyDamageBonusIndicator.transform.localScale = new Vector3(scaledSize, scaledSize, scaledSize);
                }
            }
            if (Overkilloverdrive_EnableFocusCrystal.Value)
            {
                if (!UltimateCustomRunCompatibility.enabled && !VanillaRebalanceCompatibility.enabled)
                {
                    IL.RoR2.HealthComponent.TakeDamage += (il) =>
                    {
                        ILCursor ilcursor = new(il);
                        if (ilcursor.TryGotoNext(MoveType.Before,
                            x => x.MatchLdcR4(169f)))
                        {
                            ilcursor.Remove();
                            ilcursor.EmitDelegate<Func<float>>(() =>
                            {
                                return (float)Math.Pow(13f + (13f * FindTotalMultiplier()), 2);
                            });
                        }
                    };
                }
                else
                {
                    Log.LogWarning("Ultimate Custom Run or VanillaRebalance are installed. Overkill Overdrive will not function on Focus Crystal!");
                }
            }

            // Buff Wards
            void BuffWard_Start(On.RoR2.BuffWard.orig_Start orig, BuffWard self)
            {
                self.radius += (self.radius * FindTotalMultiplier());
                orig(self);
            }

            // Bustling Fungus
            if (Overkilloverdrive_EnableBungus.Value)
            {
                if (!UltimateCustomRunCompatibility.enabled && !VanillaRebalanceCompatibility.enabled)
                {
                    IL.RoR2.Items.MushroomBodyBehavior.FixedUpdate += (il) =>
                    {
                        ILCursor ilcursor = new(il);
                        if (ilcursor.TryGotoNext(MoveType.After,
                            x => x.MatchLdloc(0),
                            x => x.MatchConvR4(),
                            x => x.MatchMul(),
                            x => x.MatchAdd()))
                        {
                            ilcursor.Emit(OpCodes.Dup);
                            ilcursor.EmitDelegate<Func<float>>(() =>
                            {
                                return FindTotalMultiplier();
                            });
                            ilcursor.Emit(OpCodes.Mul);
                            ilcursor.Emit(OpCodes.Add);
                        }
                    };
                }
                else
                {
                    Log.LogWarning("Ultimate Custom Run or VanillaRebalance are installed. Overkill Overdrive will not function on Bustling Fungus!");
                }
            }

            // Interstellar Desk Plant
            void DeskPlantController_Awake(On.RoR2.DeskPlantController.orig_Awake orig, DeskPlantController self)
            {
                self.healingRadius = 10f + (10f * FindTotalMultiplier());
                self.radiusIncreasePerStack = 5f + (5f * FindTotalMultiplier());
                orig(self);
            }

            // Captain Beacons?

            if (Overkilloverdrive_EnableHoldouts.Value) { On.RoR2.HoldoutZoneController.OnEnable += HoldoutZoneController_OnEnable; On.RoR2.Inventory.GiveItem_ItemIndex_int += Inventory_GiveItem_ItemIndex_int; }
            if (Overkilloverdrive_EnableShrineWoods.Value) { On.RoR2.ShrineHealingBehavior.SetWardEnabled += ShrineHealingBehavior_SetWardEnabled; }
            if (Overkilloverdrive_EnableFocusCrystal.Value) { On.RoR2.CharacterBody.OnInventoryChanged += CharacterBody_OnInventoryChanged; }
            if (Overkilloverdrive_EnableBuffWards.Value) { On.RoR2.BuffWard.Start += BuffWard_Start; }
            if (Overkilloverdrive_EnableDeskPlant.Value) { On.RoR2.DeskPlantController.Awake += DeskPlantController_Awake; }
        }

        public static void Initiate()
        {
            itemDef = CreateItem();
            ItemAPI.Add(new CustomItem(itemDef, CreateDisplayRules()));
            AddTokens();
            UpdateItemStatus();
            AddHooks();
        }
    }
}
