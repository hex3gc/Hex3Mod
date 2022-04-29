using BepInEx;
using BepInEx.Configuration;
using R2API;
using R2API.Utils;
using RoR2;
using RoR2.Orbs;
using System;
using UnityEngine;
using Hex3Mod;
using Hex3Mod.Logging;

namespace Hex3Mod.Items
{
    /*
    Shard Of Glass is a very simple stat increase, an item you'd never be unhappy to find
    Its secondary purpose, however, is to synergize with Shattered Reflection, forming a unique common-uncommon synergy and giving this simple item more utility
    */
    public class ScatteredReflection
    {
        // Create functions here for defining the ITEM, TOKENS, HOOKS and CONFIG. This is simpler than doing it in Main
        static string itemName = "ScatteredReflection"; // Change this name when making a new item
        static string upperName = itemName.ToUpper();
        static ItemDef itemDefinition = CreateItem();
        public static ItemDef CreateItem()
        {
            ItemDef item = ScriptableObject.CreateInstance<ItemDef>();

            item.name = itemName;
            item.nameToken = "H3_" + upperName + "_NAME";
            item.pickupToken = "H3_" + upperName + "_PICKUP";
            item.descriptionToken = "H3_" + upperName + "_DESC";
            item.loreToken = "H3_" + upperName + "_LORE";

            item.tags = new ItemTag[]{ ItemTag.Damage, ItemTag.Utility, ItemTag.AIBlacklist, ItemTag.BrotherBlacklist }; // Also change these when making a new item
            item.deprecatedTier = ItemTier.Tier2;
            item.canRemove = true;
            item.hidden = false;

            item.pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/Items/shatter.prefab");
            item.pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Materials/Icons/postShatter.png");

            return item;
        }

        public static ItemDisplayRuleDict CreateDisplayRules() // We'll figure item displays out... some day...
        {
            return new ItemDisplayRuleDict();
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

        public static void AddHooks(ItemDef itemDefToHooks, float ScatteredReflection_DamageReflectPercent, float ScatteredReflection_DamageReflectShardStack, float ScatteredReflection_DamageReflectBonus) // Insert hooks here
        {
            ItemDef ShardOfGlassDef = ShardOfGlass.CreateItem(); // Might be a better way to do this than to call CreateItem again, but no errors are caused

            float damageReflectPercent = ScatteredReflection_DamageReflectPercent;
            float damageReflectShardStack = ScatteredReflection_DamageReflectShardStack;
            float damageReflectBonus = ScatteredReflection_DamageReflectBonus;

            void OnHpLost(DamageInfo damageInfo, HealthComponent healthComponent)
            {
                if (healthComponent.body) // Those darned pots...
                {
                    if (healthComponent.body.master) // Make sure to qualify hooks with these if statements to save us from errors
                    {
                        CharacterBody body = healthComponent.body;
                        if (body.teamComponent.teamIndex >= 0)
                        {
                            if (body.inventory)
                            {
                                if (damageInfo.attacker) // And also make sure that there's an attacker in the first place...
                                {
                                    GameObject enemyGameObject = damageInfo.attacker;
                                    CharacterBody enemy = enemyGameObject.GetComponent<CharacterBody>();
                                    Inventory inventory = body.inventory;
                                    TeamIndex teamIndex = body.teamComponent.teamIndex;
                                    int itemCount = inventory.GetItemCount(itemDefToHooks);
                                    int shardCount = inventory.GetItemCount(ShardOfGlassDef);

                                    if (itemCount > 0)
                                    {
                                        float percentWithShardBonus = damageReflectPercent + (damageReflectShardStack * shardCount);
                                        if (percentWithShardBonus > 0.9f) // First, we should cap damage reduction at 90% to prevent total invincibility
                                        {
                                            percentWithShardBonus = 0.9f;
                                        }

                                        // Get the damage we need to do back, and then the damage we must prevent
                                        float damageValue = ((damageInfo.damage + (damageInfo.damage * percentWithShardBonus)) + (damageInfo.damage * (damageReflectBonus * itemCount)));
                                        float damageReduction = (damageInfo.damage * percentWithShardBonus);

                                        // Deal the reflected damage

                                        if (enemy != body)
                                        {
                                            LightningOrb lightningOrb = new LightningOrb();
                                            lightningOrb.attacker = body.gameObject;
                                            lightningOrb.bouncedObjects = null;
                                            lightningOrb.bouncesRemaining = 0;
                                            lightningOrb.damageCoefficientPerBounce = 1f;
                                            lightningOrb.damageColorIndex = DamageColorIndex.Item;
                                            lightningOrb.damageValue = damageValue;
                                            lightningOrb.isCrit = false;
                                            lightningOrb.lightningType = LightningOrb.LightningType.RazorWire;
                                            lightningOrb.origin = body.corePosition;
                                            lightningOrb.procChainMask = default(ProcChainMask);
                                            lightningOrb.procCoefficient = 0f;
                                            lightningOrb.range = 0f;
                                            lightningOrb.teamIndex = teamIndex;
                                            lightningOrb.target = enemy.mainHurtBox;
                                            OrbManager.instance.AddOrb(lightningOrb);

                                        }
                                        // Finally, reduce the damage dealt to the item holder
                                        damageInfo.damage -= damageReduction;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            On.RoR2.HealthComponent.TakeDamage += (orig, self, damageInfo) =>
            {
                orig(self, damageInfo);
                OnHpLost(damageInfo, self); // Hook into the damage report that occurs whenever damage is dealt to a body
            };
        }

        public static void Initiate(float ScatteredReflection_DamageReflectPercent, float ScatteredReflection_DamageReflectShardStack, float ScatteredReflection_DamageReflectBonus) // Finally, initiate the item and all of its features
        {
            CreateItem();
            ItemAPI.Add(new CustomItem(itemDefinition, CreateDisplayRules()));
            AddTokens(ScatteredReflection_DamageReflectPercent, ScatteredReflection_DamageReflectShardStack, ScatteredReflection_DamageReflectBonus);
            AddHooks(itemDefinition, ScatteredReflection_DamageReflectPercent, ScatteredReflection_DamageReflectShardStack, ScatteredReflection_DamageReflectBonus);
        }
    }
}
