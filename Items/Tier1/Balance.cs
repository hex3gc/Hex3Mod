using R2API;
using RoR2;
using UnityEngine;
using Hex3Mod.HelperClasses;
using UnityEngine.AddressableAssets;

namespace Hex3Mod.Items
{
    /*
    Balance provides dodge chance the slower you are.
    This balances it out for regular combat, while making it very useful while interacting with menus, being frozen/slowed, or using turrets-- all times when you're most vulnerable.
    */
    public class Balance
    {
        static string itemName = "Balance";
        static string upperName = itemName.ToUpper();
        static ItemDef itemDefinition = CreateItem();
        public static GameObject LoadPrefab()
        {
            GameObject pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/BalancePrefab.prefab");
            return pickupModelPrefab;
        }
        public static Sprite LoadSprite()
        {
            Sprite pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/Balance.png");
            return pickupIconSprite;
        }
        public static GameObject dodgeFX = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/SprintOutOfCombat/SprintActivate.prefab").WaitForCompletion();
        public static ItemDef CreateItem()
        {
            ItemDef item = ScriptableObject.CreateInstance<ItemDef>();

            item.name = itemName;
            item.nameToken = "H3_" + upperName + "_NAME";
            item.pickupToken = "H3_" + upperName + "_PICKUP";
            item.descriptionToken = "H3_" + upperName + "_DESC";
            item.loreToken = "H3_" + upperName + "_LORE";

            item.tags = new ItemTag[]{ ItemTag.Utility };
            item.deprecatedTier = ItemTier.Tier1;
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

        public static void AddTokens(float Balance_MaxDodge)
        {
            LanguageAPI.Add("H3_" + upperName + "_NAME", "Balance");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Dodge more attacks the slower you're moving.");
            LanguageAPI.Add("H3_" + upperName + "_DESC", string.Format("Gain a maximum <style=cWorldEvent>{0}% chance to dodge attacks</style> <style=cStack>(+{0}% per stack, hyperbolic)</style> <style=cWorldEvent>the slower you're moving:</style> <style=cIsUtility>Full chance</style> while not moving, <style=cIsUtility>half chance</style> while walking or receiving a speed debuff, and <style=cIsUtility>no chance</style> while freely sprinting.", Balance_MaxDodge));
            LanguageAPI.Add("H3_" + upperName + "_LORE", "\"I think one of them stung me.\"" +
            "\n\n\"The beetles? Really?\"" +
            "\n\n\"What?\"" +
            "\n\n\"Just move faster!\"" +
            "\n\n\"I'm not trying to blow out my hip, you know? I understand that we've got places to be, but can't we just slow down a little?\"" +
            "\n\n\"That's not really an option...\"" +
            "\n\n\"Because 'something's chasing us', right? I still think you're seeing things. The life forms on this planet are all about as intelligent as they look, so you can quit being paranoid about it at your own pace.\"" +
            "\n\n\"You've trusted my judgement until now, Captain.\"" +
            "\n\n\"Yeah, yeah... We should get some rest.\"" +
            "\n\nThe two survivors- only one of them having set aside their anxiety- had made a camp for the night in a small cave. Hitomi rarely let fear get to her, so it was uncharacteristic of her to be nervous on such a routine mission-- and yet, no reasoning could calm the twitching in her muscles or the chills running around her skin. She'd have chalked it up to a fever if her instincts weren't telling her otherwise." +
            "\n\n\"You too. If we're crossing the forest tomorrow, both of us will need the sleep. Alright?\"" +
            "\n\n\"Mhm...\"" +
            "\n\nHitomi took a few deep breaths-- a pattern for which she'd practiced the timing since childhood. Whenever the nerves got to her she'd begin to control her breathing, as in her line of work, nerves were not an option. And she really did need some rest." +
            "\n\nFor a moment, she felt close to drifting off, as her thoughts finally wandered away from the threat of danger. That was until a hand wrapped around the entrance of the cave. Like a skeleton covered in flesh, wrapped with pulsating muscle and vein, and adorned with crimson-spattered spikes." +
            "\n\n\"Captain-\"");
        }

        private static void AddHooks(ItemDef itemDef, float Balance_MaxDodge)
        {
            void TakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo damageInfo)
            {
                if (self.body && damageInfo.damage > 0 && !damageInfo.rejected && self.body.inventory && self.body.inventory.GetItemCount(itemDef) > 0)
                {
                    CharacterBody body = self.body;
                    int itemCount = body.inventory.GetItemCount(itemDef);
                    bool chanceAlreadySet = false;
                    float chance = 0f;
                    int slowDebuffCount = body.GetBuffCount(BuffCatalog.FindBuffIndex("bdBeetleJuice")) +
                        body.GetBuffCount(BuffCatalog.FindBuffIndex("bdCripple")) +
                        body.GetBuffCount(BuffCatalog.FindBuffIndex("bdSlow50")) +
                        body.GetBuffCount(BuffCatalog.FindBuffIndex("bdSlow60")) +
                        body.GetBuffCount(BuffCatalog.FindBuffIndex("bdSlow80")) +
                        body.GetBuffCount(BuffCatalog.FindBuffIndex("bdWeak")) +
                        body.GetBuffCount(BuffCatalog.FindBuffIndex("bdSlow30")) +
                        body.GetBuffCount(BuffCatalog.FindBuffIndex("bdClayGoo")) +
                        body.GetBuffCount(BuffCatalog.FindBuffIndex("bdJailerSlow"));
                    int stopDebuffCount = body.GetBuffCount(BuffCatalog.FindBuffIndex("bdEntangle")) +
                        body.GetBuffCount(BuffCatalog.FindBuffIndex("bdLunarSecondaryRoot")) +
                        body.GetBuffCount(BuffCatalog.FindBuffIndex("bdNullified")) +
                        body.GetBuffCount(BuffCatalog.FindBuffIndex("bdJailerTether"));

                    if (body.notMovingStopwatch > 0f || stopDebuffCount > 0 || self.isInFrozenState) // Not moving at all (100%)
                    {
                        chance = 1f;
                        chanceAlreadySet = true;
                    }
                    if (!chanceAlreadySet && body.isSprinting && slowDebuffCount > 0) // Sprinting with debuffs (50%)
                    {
                        chance = 0.5f;
                        chanceAlreadySet = true;
                    }
                    if (!chanceAlreadySet && body.isSprinting && slowDebuffCount <= 0) // Sprinting without debuffs (0%)
                    {
                        chance = 0f;
                        chanceAlreadySet = true;
                    }
                    if (!chanceAlreadySet && slowDebuffCount > 0) // Walking while debuffed (100%)
                    {
                        chance = 1f;
                        chanceAlreadySet = true;
                    }
                    if (!chanceAlreadySet) // Default (50%)
                    {
                        chance = 0.5f;
                        chanceAlreadySet = true;
                    }

                    if (Util.CheckRoll(Util.ConvertAmplificationPercentageIntoReductionPercentage((Balance_MaxDodge * chance) * (float)itemCount), 0f, null))
                    {
                        EffectData effectData = new EffectData
                        {
                            origin = damageInfo.position,
                            rotation = Util.QuaternionSafeLookRotation((damageInfo.force != Vector3.zero) ? damageInfo.force : UnityEngine.Random.onUnitSphere)
                        };
                        EffectManager.SpawnEffect(dodgeFX, effectData, true);
                        damageInfo.rejected = true;
                    }
                }
                orig(self, damageInfo);
            }

            On.RoR2.HealthComponent.TakeDamage += TakeDamage;
        }

        public static void Initiate(float Balance_MaxDodge)
        {
            ItemAPI.Add(new CustomItem(itemDefinition, CreateDisplayRules()));
            AddTokens(Balance_MaxDodge);
            AddHooks(itemDefinition, Balance_MaxDodge);
        }
    }
}
