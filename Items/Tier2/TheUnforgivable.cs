using R2API;
using RoR2;
using UnityEngine;
using Hex3Mod.HelperClasses;
using UnityEngine.PlayerLoop;
using UnityEngine.Networking;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;
using RoR2.Projectile;
using System;

namespace Hex3Mod.Items
{
    /*
    The Unforgiven provides a neat way to utilize on-kill items without needing to kill anyone, making them viable for bossfights
    Synergizes well with Forgive Me Please, both in theme and practice
    */
    public class TheUnforgivable
    {
        static string itemName = "TheUnforgivable";
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
        static GameObject fmpPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/DeathProjectile/DeathProjectile.prefab").WaitForCompletion();
        static GameObject fmpEffectPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/DeathProjectile/DeathProjectileTickEffect.prefab").WaitForCompletion();
        public static ItemDef CreateItem()
        {
            ItemDef item = ScriptableObject.CreateInstance<ItemDef>();

            item.name = itemName;
            item.nameToken = "H3_" + upperName + "_NAME";
            item.pickupToken = "H3_" + upperName + "_PICKUP";
            item.descriptionToken = "H3_" + upperName + "_DESC";
            item.loreToken = "H3_" + upperName + "_LORE";

            item.tags = new ItemTag[]{ ItemTag.Utility, ItemTag.OnKillEffect, ItemTag.Damage };
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
                        localPos = new Vector3(0.16323F, 0.27924F, -0.19534F),
                        localAngles = new Vector3(302.5952F, 232.2738F, 27.85937F),
                        localScale = new Vector3(0.10416F, 0.10416F, 0.10416F)
                    }
                }
            );
            rules.Add("mdlHuntress", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "BowHinge1L",
                        localPos = new Vector3(-0.02723F, 0.70702F, -0.15321F),
                        localAngles = new Vector3(344.0466F, 199.6963F, 136.7387F),
                        localScale = new Vector3(0.0859F, 0.0859F, 0.0859F)
                    }
                }
            );
            rules.Add("mdlToolbot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(-1.14487F, 1.62085F, 0.76236F),
                        localAngles = new Vector3(20.37697F, 73.13673F, 54.94458F),
                        localScale = new Vector3(0.98322F, 0.98322F, 0.98322F)
                    }
                }
            );
            rules.Add("mdlEngi", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "MuzzleRight",
                        localPos = new Vector3(-0.19759F, -0.12779F, -0.20736F),
                        localAngles = new Vector3(333.5917F, 241.627F, 101.1087F),
                        localScale = new Vector3(0.14993F, 0.15907F, 0.13943F)
                    }
                }
            );
            rules.Add("mdlMage", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "LowerArmR",
                        localPos = new Vector3(-0.00429F, 0.05059F, 0.05075F),
                        localAngles = new Vector3(28.22801F, 255.0296F, 283.6907F),
                        localScale = new Vector3(0.09897F, 0.09897F, 0.09897F)
                    }
                }
            );
            rules.Add("mdlMerc", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "ThighR",
                        localPos = new Vector3(-0.0296F, 0.21508F, 0.15369F),
                        localAngles = new Vector3(82.56138F, 127.7051F, 154.7617F),
                        localScale = new Vector3(0.11547F, 0.11547F, 0.11547F)
                    }
                }
            );
            rules.Add("mdlTreebot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "PlatformBase",
                        localPos = new Vector3(0.19059F, 1.25539F, 0.45615F),
                        localAngles = new Vector3(357.8243F, 344.6883F, 356.3381F),
                        localScale = new Vector3(0.26154F, 0.26154F, 0.26154F)
                    }
                }
            );
            rules.Add("mdlLoader", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "ThighL",
                        localPos = new Vector3(0.12829F, 0.12041F, 0.02403F),
                        localAngles = new Vector3(39.52324F, 2.26535F, 242.2232F),
                        localScale = new Vector3(0.13353F, 0.13353F, 0.13353F)
                    }
                }
            );
            rules.Add("mdlCroco", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "MuzzleHandL",
                        localPos = new Vector3(0.25224F, 0.26869F, 0.63436F),
                        localAngles = new Vector3(273.0432F, 53.96208F, 305.2365F),
                        localScale = new Vector3(1.06985F, 1.06985F, 1.07404F)
                    }
                }
            );
            rules.Add("mdlCaptain", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Chest",
                        localPos = new Vector3(-0.12415F, 0.20583F, 0.19383F),
                        localAngles = new Vector3(65.74024F, 5.56896F, 3.69536F),
                        localScale = new Vector3(0.17073F, 0.17073F, 0.17073F)
                    }
                }
            );
            rules.Add("mdlBandit2", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.12433F, -0.02797F, 0.11857F),
                        localAngles = new Vector3(289.2777F, 37.17081F, 172.3479F),
                        localScale = new Vector3(0.12074F, 0.12074F, 0.12074F)
                    }
                }
            );
            rules.Add("EngiTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Head",
                        localPos = new Vector3(-0.73664F, 0.74299F, 0.09672F),
                        localAngles = new Vector3(358.9678F, 9.08454F, 358.573F),
                        localScale = new Vector3(0.48142F, 0.48142F, 0.48142F)
                    }
                }
            );
            rules.Add("EngiWalkerTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "FootF",
                        localPos = new Vector3(0F, 0.14568F, -0.36895F),
                        localAngles = new Vector3(273.2368F, 218.6912F, 321.2642F),
                        localScale = new Vector3(0.58456F, 0.58456F, 0.58456F)
                    }
                }
            );
            rules.Add("mdlScav", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "FootR",
                        localPos = new Vector3(-0.11567F, 0.19908F, -1.42512F),
                        localAngles = new Vector3(14.50238F, 183.138F, 193.1178F),
                        localScale = new Vector3(2.41253F, 2.41253F, 2.41253F)
                    }
                }
            );
            rules.Add("mdlRailGunner", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "ThighR",
                        localPos = new Vector3(-0.07472F, -0.02756F, 0.01684F),
                        localAngles = new Vector3(57.57196F, 277.1546F, 10.47075F),
                        localScale = new Vector3(0.17307F, 0.17307F, 0.17307F)
                    }
                }
            );
            rules.Add("mdlVoidSurvivor", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "RingFinger",
                        localPos = new Vector3(-0.07172F, 0.03721F, 0.00721F),
                        localAngles = new Vector3(278.1604F, 145.2473F, 85.95165F),
                        localScale = new Vector3(0.12077F, 0.12077F, 0.12077F)
                    }
                }
            );

            return rules;
        }

        public static void AddTokens(float TheUnforgivable_Interval)
        {
            LanguageAPI.Add("H3_" + upperName + "_NAME", "The Unforgivable");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Periodically activate on-kill effects at your location.");
            LanguageAPI.Add("H3_" + upperName + "_DESC", string.Format("Activate your <style=cIsDamage>on-kill effects</style> at your location <style=cIsDamage>once</style> <style=cStack>(+1 per stack)</style> every <style=cIsDamage>{0}</style> seconds.", TheUnforgivable_Interval));
            LanguageAPI.Add("H3_" + upperName + "_LORE", "I left her for twenty minutes." +
            "\n\nShe couldn't have gone anywhere in twenty minutes. She can barely even walk yet." +
            "\n\nYou know something." +
            "\n\nI see the way you look at her. The way you look at me, too. You're scared. You should be." +
            "\n\nWhat did you do?" +
            "\n\nI don't know where you took her or what you did, but for your own sake, you need to bring her back. Then you can start running." +
            "\n\nI found her shoe beside the bed." +
            "\n\nWhat the hell did you do...?");
        }

        private static void AddHooks(ItemDef itemDef, float TheUnforgivable_Interval)
        {
            void AddRemoveBehavior(On.RoR2.CharacterBody.orig_OnInventoryChanged orig, CharacterBody self)
            {
                // Adds, destroys or updates stack of itembehavior if applicable
                if (self.inventory && self.inventory.GetItemCount(itemDef) > 0)
                {
                    if (self.TryGetComponent(out UnforgivableBehavior behavior) == false)
                    {
                        self.AddItemBehavior<UnforgivableBehavior>(self.inventory.GetItemCount(itemDef));
                        self.GetComponent<UnforgivableBehavior>().killInterval = TheUnforgivable_Interval;
                    }
                    else
                    {
                        self.GetComponent<UnforgivableBehavior>().stack = self.inventory.GetItemCount(itemDef);
                    }
                }
                else
                {
                    if (self.TryGetComponent(out UnforgivableBehavior behavior) == true)
                    {
                        UnityEngine.Object.Destroy(behavior);
                    }
                }
                orig(self);
            }

            On.RoR2.CharacterBody.OnInventoryChanged += AddRemoveBehavior;
        }

        public static void Initiate(float TheUnforgivable_Interval)
        {
            ItemAPI.Add(new CustomItem(itemDefinition, CreateDisplayRules()));
            AddTokens(TheUnforgivable_Interval);
            AddHooks(itemDefinition, TheUnforgivable_Interval);
        }

        private class UnforgivableBehavior : CharacterBody.ItemBehavior
        {
            private float killTimer = 0f;
            public float killInterval;
            GameObject ghostFMP;

            private void Awake()
            {
                fmpEffectPrefab.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            }

            private void OnDestroy()
            {
                UnityEngine.Object.Destroy(ghostFMP);
            }

            private void FixedUpdate()
            {
                killTimer += Time.fixedDeltaTime;

                if (!ghostFMP)
                {
                    ghostFMP = Instantiate<GameObject>(fmpPrefab, /*new Vector3(0f, -200f, 0f)*/body.footPosition, Quaternion.identity);
                    ghostFMP.transform.localScale = new Vector3(0f, 0f, 0f);
                    UnityEngine.Object.Destroy(ghostFMP.GetComponent<DestroyOnTimer>());
                    UnityEngine.Object.Destroy(ghostFMP.GetComponent<DeathProjectile>());
                    UnityEngine.Object.Destroy(ghostFMP.GetComponent<ApplyTorqueOnStart>());
                    UnityEngine.Object.Destroy(ghostFMP.GetComponent<ProjectileDeployToOwner>());
                    UnityEngine.Object.Destroy(ghostFMP.GetComponent<Deployable>());
                    UnityEngine.Object.Destroy(ghostFMP.GetComponent<ProjectileStickOnImpact>());
                    UnityEngine.Object.Destroy(ghostFMP.GetComponent<ProjectileController>());
                }

                ghostFMP.transform.position = body.footPosition;

                if (killTimer >= (killInterval / stack)) // Spaces kills evenly along 8 seconds
                {
                    EffectData effectData = new EffectData
                    {
                        origin = body.corePosition,
                        rotation = Quaternion.identity
                    };
                    EffectManager.SpawnEffect(fmpEffectPrefab, effectData, false);
                    DamageInfo damageInfo = new DamageInfo
                    {
                        attacker = body.gameObject,
                        crit = body.RollCrit(),
                        damage = body.baseDamage,
                        position = body.footPosition,
                        procCoefficient = 0f,
                        damageType = DamageType.Generic,
                        damageColorIndex = DamageColorIndex.Item
                    };
                    DamageReport damageReport = new DamageReport(damageInfo, ghostFMP.GetComponent<HealthComponent>(), damageInfo.damage, ghostFMP.GetComponent<HealthComponent>().combinedHealth);
                    GlobalEventManager.instance.OnCharacterDeath(damageReport);

                    killTimer = 0f;
                }
            }
        }
    }
}
