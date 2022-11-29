using R2API;
using RoR2;
using UnityEngine;
using Hex3Mod.HelperClasses;
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
using HG;

namespace Hex3Mod.Items
{
    /*
    An item I added later because I liked the idea. Ever felt like holdout zones were too small? Want your allies to be in bungus range? This is the item for you.
    */
    public class OverkillOverdrive
    {
        static string itemName = "OverkillOverdrive";
        static string upperName = itemName.ToUpper();
        static ItemDef itemDefinition = CreateItem();
        public static GameObject LoadPrefab()
        {
            GameObject pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/TheUnforgivablePrefab.prefab");
            return pickupModelPrefab;
        }
        public static Sprite LoadSprite()
        {
            Sprite pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/TheUnforgivable.png");
            return pickupIconSprite;
        }
        static GameObject focusCrystalPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/NearbyDamageBonus/NearbyDamageBonusIndicator.prefab").WaitForCompletion();
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

        public static void AddTokens(float OverkillOverdrive_ZoneIncrease, bool OverkillOverdrive_AltMode)
        {
            LanguageAPI.Add("H3_" + upperName + "_NAME", "Overkill Overdrive");
            LanguageAPI.Add("H3_" + upperName + "_LORE", "\"I want everyone to hear it, even if it doesn't sound good.\"\n\n- Written on a sticky note, attached to the back of the amp.");
            if (OverkillOverdrive_AltMode)
            {
                LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Amplify the range of area buffs.");
                LanguageAPI.Add("H3_" + upperName + "_DESC", string.Format("Amplify the radius of <style=cIsUtility>area buffs</style> by <style=cWorldEvent>{0}%</style> <style=cStack>(+{0}% per stack)</style>", OverkillOverdrive_ZoneIncrease * 1.5));
            }
            else
            {
                LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Amplify the range of area buffs and holdout zones.");
                LanguageAPI.Add("H3_" + upperName + "_DESC", string.Format("Amplify the radius of <style=cIsUtility>area buffs</style> and <style=cWorldEvent>holdout zones</style> by <style=cWorldEvent>{0}%</style> <style=cStack>(+{0}% per stack)</style>", OverkillOverdrive_ZoneIncrease));
            }
        }

        private static void AddHooks(ItemDef itemDef, float OverkillOverdrive_ZoneIncrease, bool OverkillOverdrive_AltMode)
        {
            float FindTotalMultiplier() // Gets the appropriate radius multiplier based on all player inventories
            {
                int totalItemAmount = 0;
                foreach (TeamComponent ally in TeamComponent.GetTeamMembers(TeamIndex.Player))
                {
                    if (ally.body && ally.body.inventory)
                    {
                        totalItemAmount += ally.body.inventory.GetItemCount(itemDef);
                    }
                }
                return totalItemAmount * (OverkillOverdrive_ZoneIncrease / 100f);
            }

            // General holdout zones
            void HoldoutZoneController_OnEnable(On.RoR2.HoldoutZoneController.orig_OnEnable orig, HoldoutZoneController self)
            {
                orig(self);
                if (!OverkillOverdrive_AltMode)
                {
                    self.baseRadius += self.baseRadius * FindTotalMultiplier();
                }
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
                if (self.TryGetComponent(out NearbyDamageBonusBodyBehavior behavior) == true && behavior.isActiveAndEnabled)
                {
                    float scaledSize = 1 + (1 * FindTotalMultiplier());
                    behavior.nearbyDamageBonusIndicator.transform.localScale = new Vector3(scaledSize, scaledSize, scaledSize);
                }
            }
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

            // Buff Wards
            void BuffWard_Start(On.RoR2.BuffWard.orig_Start orig, BuffWard self)
            {
                self.radius += (self.radius * FindTotalMultiplier());
                orig(self);
            }

            // Bustling Fungus
            IL.RoR2.Items.MushroomBodyBehavior.FixedUpdate += (il) =>
            {
                ILCursor ilcursor = new(il);
                if (ilcursor.TryGotoNext(MoveType.After,
                    x => x.MatchAdd(),
                    x => x.MatchLdcR4(1.5f)))
                {
                    ilcursor.Emit(OpCodes.Add);
                    ilcursor.EmitDelegate<Func<float>>(() =>
                    {
                        return (0.5f + 1.5f + 1.5f) * FindTotalMultiplier();
                    });
                }
            };

            // Interstellar Desk Plant
            void DeskPlantController_Awake(On.RoR2.DeskPlantController.orig_Awake orig, DeskPlantController self)
            {
                self.healingRadius = 10f + (10f * FindTotalMultiplier());
                self.radiusIncreasePerStack = 5f + (5f * FindTotalMultiplier());
                orig(self);
            }

            // Void Fields Cells?

            // Captain Beacons?

            // Find an efficient way to add Sharp Anchor?

            On.RoR2.HoldoutZoneController.OnEnable += HoldoutZoneController_OnEnable;
            On.RoR2.ShrineHealingBehavior.SetWardEnabled += ShrineHealingBehavior_SetWardEnabled;
            On.RoR2.CharacterBody.OnInventoryChanged += CharacterBody_OnInventoryChanged;
            On.RoR2.BuffWard.Start += BuffWard_Start;
            On.RoR2.DeskPlantController.Awake += DeskPlantController_Awake;
        }

        public static void Initiate(float OverkillOverdrive_ZoneIncrease, bool OverkillOverdrive_AltMode)
        {
            ItemAPI.Add(new CustomItem(itemDefinition, CreateDisplayRules()));
            AddTokens(OverkillOverdrive_ZoneIncrease, OverkillOverdrive_AltMode);
            AddHooks(itemDefinition, OverkillOverdrive_ZoneIncrease, OverkillOverdrive_AltMode);
        }
    }
}
