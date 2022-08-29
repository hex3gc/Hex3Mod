using R2API;
using RoR2;
using RoR2.Orbs;
using UnityEngine;
using Hex3Mod.HelperClasses;

namespace Hex3Mod.Items
{
    /*
    Scattered Reflection provides a unique item synergy for the simple Shard Of Glass
    It also provides damage reduction and retaliation in one, making it ideal for players who want to tank some damage
    */
    public class ScatteredReflection
    {
        static string itemName = "ScatteredReflection";
        static string upperName = itemName.ToUpper();
        static ItemDef itemDefinition = CreateItem();
        public static GameObject LoadPrefab()
        {
            GameObject pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/ScatteredReflectionPrefab.prefab");
            return pickupModelPrefab;
        }
        public static Sprite LoadSprite()
        {
            Sprite pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/ScatteredReflection.png");
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

            item.tags = new ItemTag[]{ ItemTag.Damage, ItemTag.Utility, ItemTag.AIBlacklist, ItemTag.BrotherBlacklist };
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
            /*
            GameObject ItemDisplayPrefab = PrefabAPI.InstantiateClone(new GameObject("H3_ScatteredReflectionFollower"), "H3_ScatteredReflectionFollower", false);
            ItemFollower itemFollower = ItemDisplayPrefab.AddComponent<ItemFollower>();
            itemFollower.targetObject = ItemDisplayPrefab;
            itemFollower.followerPrefab = LoadPrefab();
            itemFollower.distanceDampTime = 0.1f;
            itemFollower.distanceMaxSpeed = 200f;

            Just implement follower behavior in a later update
            */

            ItemDisplayRuleDict rules = new ItemDisplayRuleDict();
            rules.Add("mdlCommandoDualies", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(-0.4304F, -0.81567F, 0.12051F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.07054F, 0.07054F, 0.07054F)
                    }
                }
            );
            rules.Add("mdlHuntress", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(-0.4304F, -0.81567F, 0.12051F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.07054F, 0.07054F, 0.07054F)
                    }
                }
            );
            rules.Add("mdlToolbot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Hip",
                        localPos = new Vector3(-3.67421F, -1.17204F, -5.27553F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.46104F, 0.46104F, 0.46104F)
                    }
                }
            );
            rules.Add("mdlEngi", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(-0.58279F, -0.3402F, 0.63118F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.07054F, 0.07054F, 0.07054F)
                    }
                }
            );
            rules.Add("mdlMage", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(-0.4304F, -0.81567F, 0.12051F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.07054F, 0.07054F, 0.07054F)
                    }
                }
            );
            rules.Add("mdlMerc", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(-0.4304F, -0.81567F, 0.12051F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.07054F, 0.07054F, 0.07054F)
                    }
                }
            );
            rules.Add("mdlTreebot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "PlatformBase",
                        localPos = new Vector3(-0.67259F, 0.96418F, -1.43672F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.1357F, 0.1357F, 0.1357F)
                    }
                }
            );
            rules.Add("mdlLoader", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(-0.58465F, -0.69641F, 0.35263F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.07054F, 0.07054F, 0.07054F)
                    }
                }
            );
            rules.Add("mdlCroco", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Hip",
                        localPos = new Vector3(-4.59637F, -1.1522F, 3.66788F),
                        localAngles = new Vector3(319.2833F, 0F, 0F),
                        localScale = new Vector3(0.49983F, 0.49983F, 0.49983F)
                    }
                }
            );
            rules.Add("mdlCaptain", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(-0.54088F, -0.86142F, 0.10506F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.07054F, 0.07054F, 0.07054F)
                    }
                }
            );
            rules.Add("mdlBandit2", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(-0.4304F, -0.81567F, 0.12051F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.07054F, 0.07054F, 0.07054F)
                    }
                }
            );
            rules.Add("EngiTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Base",
                        localPos = new Vector3(-1.95867F, 1.47275F, 0.22363F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.31116F, 0.31116F, 0.31116F)
                    }
                }
            );
            rules.Add("EngiWalkerTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Base",
                        localPos = new Vector3(-2.20764F, 1.68749F, 0.22363F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.31494F, 0.31494F, 0.31494F)
                    }
                }
            );
            rules.Add("mdlScav", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(-15.17687F, -0.5384F, 1.92231F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(1.66772F, 1.66772F, 1.66772F)
                    }
                }
            );
            rules.Add("mdlRailGunner", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(-0.65262F, -0.5386F, 0.1665F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.07054F, 0.07054F, 0.07054F)
                    }
                }
            );
            rules.Add("mdlVoidSurvivor", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(-0.5793F, -0.49399F, -0.44001F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.07054F, 0.07054F, 0.07054F)
                    }
                }
            );

            return rules;
        }

        public static void AddTokens(float ScatteredReflection_DamageReflectPercent, float ScatteredReflection_DamageReflectShardStack, float ScatteredReflection_DamageReflectBonus)
        {
            float ScatteredReflection_DamageReflectPercent_Readable = ScatteredReflection_DamageReflectPercent * 100f;
            float ScatteredReflection_DamageReflectShardStack_Readable = ScatteredReflection_DamageReflectShardStack * 100f;
            float ScatteredReflection_DamageReflectBonus_Readable = ScatteredReflection_DamageReflectBonus * 100f;

            LanguageAPI.Add("H3_" + upperName + "_NAME", "Scattered Reflection");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Reflect some of the damage you take back to attackers. Reflect more with each Shard Of Glass you own.");
            LanguageAPI.Add("H3_" + upperName + "_DESC", "<style=cWorldEvent>Prevent</style> <style=cIsUtility>" + ScatteredReflection_DamageReflectPercent_Readable + "%</style> of all received damage and reflect it back to your attacker, adding an additional <style=cIsDamage>" + ScatteredReflection_DamageReflectBonus_Readable + "%</style> <style=cStack>(+" + ScatteredReflection_DamageReflectBonus_Readable + "% per stack)</style> damage bonus. For every <style=cWorldEvent>Shard Of Glass</style> in your inventory, prevent <style=cIsUtility>" + ScatteredReflection_DamageReflectShardStack_Readable + "%</style> more damage.");
            LanguageAPI.Add("H3_" + upperName + "_LORE", "An aggregate of shattered souls\n\nLost to the wind and to time\n\nThey form a ward to protect you\n\nThe only one they can follow home");
        }

        private static void AddHooks(ItemDef itemDef, float ScatteredReflection_DamageReflectPercent, float ScatteredReflection_DamageReflectShardStack, float ScatteredReflection_DamageReflectBonus) // Insert hooks here
        {
            ItemDef ShardOfGlassDef = ShardOfGlass.CreateItem(); // Might be a better way to do this than to call CreateItem again, but no errors are caused

            float damageReflectPercent = ScatteredReflection_DamageReflectPercent;
            float damageReflectShardStack = ScatteredReflection_DamageReflectShardStack;
            float damageReflectBonus = ScatteredReflection_DamageReflectBonus;

            void H3_OnHpLost(DamageInfo damageInfo, HealthComponent healthComponent)
            {
                if (healthComponent.body && healthComponent.body.master && healthComponent.body.teamComponent) // Those darned pots...
                {
                    CharacterBody body = healthComponent.body;
                    if (body.teamComponent.teamIndex >= 0 && body.gameObject)
                    {
                        if (body.inventory && damageInfo.attacker) // And also make sure that there's an attacker in the first place...
                        {
                            GameObject enemyGameObject = damageInfo.attacker;

                            if (enemyGameObject.GetComponent<CharacterBody>() && enemyGameObject.GetComponent<CharacterBody>().teamComponent) // AND that they have a body/teamindex...
                            {
                                CharacterBody enemy = enemyGameObject.GetComponent<CharacterBody>();
                                Inventory inventory = body.inventory;
                                TeamIndex teamIndex = body.teamComponent.teamIndex;
                                TeamIndex teamIndexEnemy = enemy.teamComponent.teamIndex;
                                int itemCount = inventory.GetItemCount(itemDef);
                                int shardCount = inventory.GetItemCount(ShardOfGlassDef);

                                if (itemCount > 0)
                                {
                                    float percentWithShardBonus = damageReflectPercent + (damageReflectShardStack * shardCount);
                                    if (percentWithShardBonus > 0.8f) // First, we should cap damage reduction at 80% to prevent total invincibility
                                    {
                                        percentWithShardBonus = 0.8f;
                                    }

                                    // Get the damage we need to do back, and then the damage we must prevent
                                    float damageValue = ((damageInfo.damage + (damageInfo.damage * percentWithShardBonus)) + (damageInfo.damage * (damageReflectBonus * itemCount)));
                                    float damageReduction = (damageInfo.damage * percentWithShardBonus);

                                    // Deal the reflected damage if these conditions are met
                                    if (enemy != body && teamIndex != teamIndexEnemy && damageInfo.damageType != DamageType.OutOfBounds)
                                    {
                                        LightningOrb lightningOrb = new LightningOrb();
                                        lightningOrb.attacker = body.gameObject;
                                        lightningOrb.bouncedObjects = null;
                                        lightningOrb.bouncesRemaining = 0;
                                        lightningOrb.damageCoefficientPerBounce = 1f;
                                        lightningOrb.damageColorIndex = DamageColorIndex.Item;
                                        lightningOrb.damageValue = damageValue;
                                        lightningOrb.damageType = DamageType.OutOfBounds;
                                        lightningOrb.isCrit = false;
                                        lightningOrb.lightningType = LightningOrb.LightningType.RazorWire;
                                        lightningOrb.origin = body.corePosition;
                                        lightningOrb.procChainMask = default(ProcChainMask);
                                        lightningOrb.procCoefficient = 1f;
                                        lightningOrb.range = 0f;
                                        lightningOrb.teamIndex = teamIndex;
                                        lightningOrb.target = enemy.mainHurtBox;
                                        OrbManager.instance.AddOrb(lightningOrb);
                                        Util.PlaySound(EntityStates.BrotherMonster.Weapon.FireLunarShards.fireSound, body.gameObject);
                                    }
                                    // Finally, reduce the damage dealt to the item holder
                                    damageInfo.damage -= damageReduction;
                                }
                            }
                        }
                    }
                }
            }
            On.RoR2.HealthComponent.TakeDamage += (orig, self, damageInfo) =>
            {
                orig(self, damageInfo);
                H3_OnHpLost(damageInfo, self); // Hook into the damage report that occurs whenever damage is dealt to a body
            };
        }

        public static void Initiate(float ScatteredReflection_DamageReflectPercent, float ScatteredReflection_DamageReflectShardStack, float ScatteredReflection_DamageReflectBonus)
        {
            ItemAPI.Add(new CustomItem(itemDefinition, CreateDisplayRules()));
            AddTokens(ScatteredReflection_DamageReflectPercent, ScatteredReflection_DamageReflectShardStack, ScatteredReflection_DamageReflectBonus);
            AddHooks(itemDefinition, ScatteredReflection_DamageReflectPercent, ScatteredReflection_DamageReflectShardStack, ScatteredReflection_DamageReflectBonus);
        }
    }
}
