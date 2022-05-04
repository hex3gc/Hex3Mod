using BepInEx;
using BepInEx.Configuration;
using R2API;
using R2API.Utils;
using RoR2;
using System;
using UnityEngine;
using Hex3Mod;
using Hex3Mod.Logging;

namespace Hex3Mod.Items
{
    /*
    ATG Prototype is a simple port of the ATG to the Common tier, but with a different proc condition.
    It should serve a different purpose than the normal ATG (Consistent, small damage rather than luck-based, large damage) and also give ICBM a good Common item to synergize with
    */
    public class AtgPrototype
    {
        // Create functions here for defining the ITEM, TOKENS, HOOKS and CONFIG. This is simpler than doing it in Main
        static string itemName = "AtgPrototype"; // Change this name when making a new item
        static string upperName = itemName.ToUpper();
        public static ItemDef itemDefinition = CreateItem(); // Must be public for ScatteredReflection to read
        public static ItemDef CreateItem()
        {
            ItemDef item = ScriptableObject.CreateInstance<ItemDef>();

            item.name = itemName;
            item.nameToken = "H3_" + upperName + "_NAME";
            item.pickupToken = "H3_" + upperName + "_PICKUP";
            item.descriptionToken = "H3_" + upperName + "_DESC";
            item.loreToken = "H3_" + upperName + "_LORE";

            item.tags = new ItemTag[]{ ItemTag.Damage }; // Also change these when making a new item
            item.deprecatedTier = ItemTier.Tier1;
            item.canRemove = true;
            item.hidden = false;

            item.pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/AtgPrototypePrefab.prefab");
            item.pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Textures/Icons/AtgPrototype.png");

            return item;
        }

        public static ItemDisplayRuleDict CreateDisplayRules() // We'll figure item displays out... when our models get better
        {
            return new ItemDisplayRuleDict();
        }

        public static void AddTokens(float atgDamageStack, int hitRequirement)
        {
            float atgDamageStack_Readable = atgDamageStack * 100;

            LanguageAPI.Add("H3_" + upperName + "_NAME", "AtG Prototype");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Every ten hits, fire a missile.");
            LanguageAPI.Add("H3_" + upperName + "_DESC", "After inflicting <style=cIsUtility>" + hitRequirement + "</style> hits, fire a missile that deals <style=cIsDamage>" + atgDamageStack_Readable + "%</style> <style=cStack>(+" + atgDamageStack_Readable + "% per stack)</style> damage.");
            LanguageAPI.Add("H3_" + upperName + "_LORE", "Order: AtG Missile Launcher Prototype\nTracking Number: 11******\nEstimated Delivery: 08/15/2056\nShipping Method: Priority\nShipping Address: Cargo Bay 1-A, Terminal 2-A, UES Port\nShipping Details:\n\nThe AtG Missile Launcher MK1 is a staple for our military operations, but like many wrist rockets and shoulder-mounted launchers, it suffers a safety issue with its targeting scheme. As the missiles are heat-seeking, it can often be thrown off by variance in local temperatures, which is a common issue at our deployment locations. This also renders the missiles ineffective against targets who can manipulate cold and ice, which was admittedly unprecedented.\n\nAdditionally, while the ATG has safeguards against seeking its holder, these fall apart when the launcher sustains heavy damage or even a temporary power outage. This has resulted in a number of unfortunate accidents. We'll need to have a look at the AtG's predecessor models, because clearly something was [REDACTED] up along the way.");
        }

        public static void AddHooks(ItemDef itemDefToHooks, float atgDamageStack, int hitRequirement) // Insert hooks here
        {
            void H3_OnHitEnemy(On.RoR2.GlobalEventManager.orig_OnHitEnemy orig, GlobalEventManager self, DamageInfo damageInfo, GameObject victim)
            {
                orig(self, damageInfo, victim);

                if (damageInfo.procChainMask.HasProc(ProcType.Missile)) return; // Must only activate on proc hits and non-missile hits to avoid cascading too hard
                if (victim)
                {
                    if (damageInfo.attacker != null && damageInfo.attacker.GetComponent<CharacterBody>() != null && damageInfo.procCoefficient > 0f)
                    {
                        CharacterBody attackerBody = damageInfo.attacker.GetComponent<CharacterBody>();

                        if (attackerBody.inventory)
                        {
                            int itemCount = attackerBody.inventory.GetItemCount(itemDefToHooks);

                            if (itemCount > 0)
                            {
                                float missileDamage = (attackerBody.baseDamage * atgDamageStack) * itemCount;

                                if (attackerBody.GetBuffCount(atgCounter) == (hitRequirement - 1) || attackerBody.GetBuffCount(atgCounter) > (hitRequirement - 1))
                                {
                                    // Fire the missile on the 10th attack, then reset the counter

                                    MissileUtils.FireMissile(
                                        attackerBody.corePosition,
                                        attackerBody,
                                        default,
                                        victim,
                                        missileDamage,
                                        damageInfo.crit,
                                        GlobalEventManager.CommonAssets.missilePrefab,
                                        DamageColorIndex.Item,
                                        true
                                    );

                                    attackerBody.SetBuffCount(atgCounter.buffIndex, 0);
                                }
                                else
                                {
                                    attackerBody.SetBuffCount(atgCounter.buffIndex, attackerBody.GetBuffCount(atgCounter) + 1);
                                }
                            }
                        }
                    }
                }
            }

            On.RoR2.GlobalEventManager.OnHitEnemy += H3_OnHitEnemy;
        }

        public static BuffDef atgCounter { get; private set; }
        public static void AddBuffs()
        {
            atgCounter = ScriptableObject.CreateInstance<BuffDef>();
            atgCounter.buffColor = new Color(0.3f, 0.9f, 0.3f);
            atgCounter.canStack = true;
            atgCounter.isDebuff = false;
            atgCounter.name = "ATGCounter";
            atgCounter.isHidden = true;
            atgCounter.iconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Materials/Icons/ATGIcon.png"); // Change this
            ContentAddition.AddBuffDef(atgCounter);
        }

        public static void Initiate(float atgDamageStack, int hitRequirement) // Finally, initiate the item and all of its features
        {
            CreateItem();
            ItemAPI.Add(new CustomItem(itemDefinition, CreateDisplayRules()));
            AddTokens(atgDamageStack, hitRequirement);
            AddBuffs();
            AddHooks(itemDefinition, atgDamageStack, hitRequirement);
        }
    }
}
