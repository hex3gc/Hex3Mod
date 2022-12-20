using R2API;
using RoR2;
using System;
using UnityEngine;
using Hex3Mod.HelperClasses;
using UnityEngine.AddressableAssets;
using Hex3Mod.Utils;
using static Hex3Mod.Main;

namespace Hex3Mod.Items
{
    /*
    Final (I hope) version of Apathy. Now synergizes with close-range options and on-kill boosters like The Unforgivable
    */
    public class Apathy
    {
        static string itemName = "Apathy";
        static string upperName = itemName.ToUpper();
        static ItemDef itemDef;
        public static GameObject LoadPrefab()
        {
            GameObject pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/ApathyPrefab.prefab");
            if (Main.debugMode == true)
            {
                pickupModelPrefab.GetComponentInChildren<Renderer>().gameObject.AddComponent<MaterialControllerComponents.HGControllerFinder>();
            }
            return pickupModelPrefab;
        }
        public static Sprite LoadSprite()
        {
            Sprite pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/Apathy.png");
            return pickupIconSprite;
        }
        public static Sprite LoadBuffSprite()
        {
            Sprite pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/Buff_Apathy.png");
            return pickupIconSprite;
        }
        public static Sprite LoadBuffSprite2()
        {
            Sprite pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/Buff_ApathyStacks.png");
            return pickupIconSprite;
        }
        static GameObject FocusCrystalPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/NearbyDamageBonus/NearbyDamageBonusIndicator.prefab").WaitForCompletion();
        static Material ApathyMaterial = Addressables.LoadAssetAsync<Material>("RoR2/Base/ArmorReductionOnHit/matPulverizedOverlay.mat").WaitForCompletion();

        public static ItemDef CreateItem()
        {
            ItemDef item = ScriptableObject.CreateInstance<ItemDef>();

            item.name = itemName;
            item.nameToken = "H3_" + upperName + "_NAME";
            item.pickupToken = "H3_" + upperName + "_PICKUP";
            item.descriptionToken = "H3_" + upperName + "_DESC";
            item.loreToken = "H3_" + upperName + "_LORE";

            item.tags = new ItemTag[]{ ItemTag.Healing, ItemTag.Damage, ItemTag.AIBlacklist, ItemTag.BrotherBlacklist };
            item.deprecatedTier = ItemTier.Tier3;
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
                        localPos = new Vector3(0.46303F, -0.23741F, 0.22363F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.07054F, 0.07054F, 0.07054F)
                    }
                }
            );
            rules.Add("mdlHuntress", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.46303F, -0.23741F, 0.22363F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.07054F, 0.07054F, 0.07054F)
                    }
                }
            );
            rules.Add("mdlToolbot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Hip",
                        localPos = new Vector3(-3.67415F, -3.53406F, 3.49507F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.61885F, 0.61885F, 0.61885F)
                    }
                }
            );
            rules.Add("mdlEngi", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.70594F, -0.52649F, 0.46413F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.07054F, 0.07054F, 0.07054F)
                    }
                }
            );
            rules.Add("mdlMage", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.46303F, -0.23741F, 0.22363F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.07054F, 0.07054F, 0.07054F)
                    }
                }
            );
            rules.Add("mdlMerc", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.46303F, -0.23741F, 0.22363F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.07054F, 0.07054F, 0.07054F)
                    }
                }
            );
            rules.Add("mdlTreebot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "PlatformBase",
                        localPos = new Vector3(1.49471F, 1.14408F, -1.24895F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.19447F, 0.19447F, 0.19447F)
                    }
                }
            );
            rules.Add("mdlLoader", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.73526F, -0.26901F, 0.42105F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.07054F, 0.07054F, 0.07054F)
                    }
                }
            );
            rules.Add("mdlCroco", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Hip",
                        localPos = new Vector3(5.36278F, -2.56467F, 4.70967F),
                        localAngles = new Vector3(319.2833F, 0F, 0F),
                        localScale = new Vector3(0.49983F, 0.49983F, 0.49983F)
                    }
                }
            );
            rules.Add("mdlCaptain", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.55865F, -0.77158F, 0.13444F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.07054F, 0.07054F, 0.07054F)
                    }
                }
            );
            rules.Add("mdlBandit2", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.46303F, -0.23741F, 0.22363F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.07054F, 0.07054F, 0.07054F)
                    }
                }
            );
            rules.Add("EngiTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Base",
                        localPos = new Vector3(1.59118F, 1.47278F, 0.22363F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.31116F, 0.31116F, 0.31116F)
                    }
                }
            );
            rules.Add("EngiWalkerTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Base",
                        localPos = new Vector3(1.55029F, 1.6875F, 0.22363F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.31494F, 0.31494F, 0.31494F)
                    }
                }
            );
            rules.Add("mdlScav", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(14.30765F, -0.53849F, 1.92231F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(1.66772F, 1.66772F, 1.66772F)
                    }
                }
            );
            rules.Add("mdlRailGunner", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.46303F, -0.23741F, 0.22363F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.07054F, 0.07054F, 0.07054F)
                    }
                }
            );
            rules.Add("mdlVoidSurvivor", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(-0.49633F, -0.15624F, 0.2652F),
                        localAngles = new Vector3(0.00001F, 41.26042F, 0F),
                        localScale = new Vector3(0.07054F, 0.07054F, 0.07054F)
                    }
                }
            );

            return rules;
        }

        public static void AddTokens()
        {
            LanguageAPI.Add("H3_" + upperName + "_NAME", "Apathy");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Power up after witnessing enough violence.");
            LanguageAPI.Add("H3_" + upperName + "_LORE",
                "\"I can't [REDACTED] believe I'm doing this...\"" +
                "\n\n\"We'll share the network. We have to.\"" +
                "\n\n\"FINE! Just do it fast-\"" +
                "\n\nFrom out of the container came a silvery, goopy substance, like viscous mercury. At first it laid still on the table, but it didn't take long for it to wake up. It seemed to notice its two potential hosts, only giving a moment of hesistation before splitting into two and floating towards their faces." +
                "\n\n\"What's it gonna do!?\"" +
                "\n\n\"Open your mouth.\"" +
                "\n\n\"NO!\"" +
                "\n\nAs it drew near enough to the crewmates' faces, it suddenly gained speed and latched onto their noses and mouths, choking the one who wasn't prepared. The calmer of the crew raised a hand to try and ease the other down to his level, but he had already doubled over, trying to wrench the substance from his face. Neither could speak." +
                "\n\nIt took less time for the substance to enter and integrate with its first host, who began to yell and command his crewmate to keep calm and hold his breath." +
                "\n\nThe struggling crewmate was purple in the face and teary-eyed. He felt like he was about to die. The goop then sucked itself into his system and propogated its way through, leaving him coughing and wretching." +
                "\n\n\"Oh my god... oh my god...\" He croaked out the words." +
                "\n\n\"Look- we need to leave, now. That Titan could show up at-\"" +
                "\n\nTheir ship was then crushed in on one side. Stone demolished metal for one deafening moment and then stopped. The Stone Titan only had to kick the craft once in order to destroy it beyond repair. One of its occupants, however, managed to get to his feet and reorient himself." +
                "\n\n\"Burke ? You alright ? Where...\"" +
                "\n\nBurke's body lay beneath the remains of the cabin wall It had been pulverized. After his trouble with the nanobots, he could barely think, let alone dodge the wreckage of his ship as it came crashing down." +
                "\n\nJones began to feel something shift inside of him. The network had sustained critical damage and needed to begin repair routines. Then he felt everything shift inside of him..." +
                "\n\n..." +
                "\n\nThe Stone Titan spotted an unknown figure leaving the ship. Its red eye flared with heat, and without a moment to wait, it focused its laser directly onto the intruder." +
                "\n\nIt expected to see a pile of ash left behind, but it once again saw the figure. It wasn't a creature that it knew of. The Titan had seen humans before, but this was not one. Its eyes leaked dark red as if they were ripped out and replaced with black orbs. It no longer had a face, but a mangling of bone and fatty tissue, with hard spines leading from its forehead all the way down to its lower back. Its torso pumped hard, in and out like a giant beating heart. Its skin split open from the pressure, but then wove itself back together again. All of its limbs were lithe and muscular, with nails hardened into claws and bones that poked out to shield the body." +
                "\n\nThe Titan had never felt fear before."
                );
        }
        public static void UpdateItemStatus()
        {
            if (!Apathy_Enable.Value)
            {
                LanguageAPI.AddOverlay("H3_" + upperName + "_NAME", "Apathy" + " <style=cDeath>[DISABLED]</style>");
            }
            else
            {
                LanguageAPI.AddOverlay("H3_" + upperName + "_NAME", "Apathy");
            }
            LanguageAPI.AddOverlay("H3_" + upperName + "_DESC", String.Format("When an enemy dies within <style=cDeath>{0}m</style> of you, gain a stack of <style=cDeath>Apathy</style>. After reaching <style=cIsDamage>{5}</style> stacks, <style=cDeath>enter a frenzy</style> which grants you <style=cIsDamage>+{1}% movement speed, +{2}% attack speed and {3} hp/s of regeneration for {4} seconds</style> <style=cStack>(+{4}s per stack)</style>", Apathy_Radius.Value, Apathy_MoveSpeedAdd.Value * 100f, Apathy_AttackSpeedAdd.Value * 100f, Apathy_RegenAdd.Value, Apathy_Duration.Value, Apathy_RequiredKills.Value));
        }

        private static void AddHooks()
        {
            // Add/remove radius indicator on inventory change
            void CharacterBody_OnInventoryChanged(On.RoR2.CharacterBody.orig_OnInventoryChanged orig, CharacterBody self)
            {
                orig(self);
                if (self.inventory && self.inventory.GetItemCount(itemDef) > 0)
                {
                    if (!self.GetComponent<ApathyBehavior>())
                    {
                        self.AddItemBehavior<ApathyBehavior>(1);
                    }
                    int numberOfOverdrives = 0;
                    if (ItemCatalog.FindItemIndex("OverkillOverdrive") != ItemIndex.None)
                    {
                        numberOfOverdrives = self.inventory.GetItemCount(ItemCatalog.FindItemIndex("OverkillOverdrive"));
                    }

                    ApathyBehavior behavior = self.GetComponent<ApathyBehavior>();
                    behavior.radius = Apathy_Radius.Value + (Apathy_Radius.Value * ((OverkillOverdrive_ZoneIncrease.Value / 100f) * numberOfOverdrives));
                    behavior.sizeAddMultiplier = (OverkillOverdrive_ZoneIncrease.Value / 100f) * numberOfOverdrives;
                    behavior.focusCrystalSizeDivisor = Apathy_Radius.Value / 13f;
                    behavior.enable = true;
                }
                if (self.inventory && self.inventory.GetItemCount(itemDef) <= 0 && self.GetComponent<ApathyBehavior>())
                {
                    UnityEngine.Object.Destroy(self.GetComponent<ApathyBehavior>());
                    self.RemoveBuff(apathyStacks);
                }
            }

            void GlobalEventManager_OnCharacterDeath(On.RoR2.GlobalEventManager.orig_OnCharacterDeath orig, GlobalEventManager self, DamageReport damageReport)
            {
                orig(self, damageReport);
                foreach (ApathyBehavior apathyOwner in GameObject.FindObjectsOfType<ApathyBehavior>())
                {
                    int numberOfOverdrives = 0;
                    Vector3 deathDistanceVector = Vector3.zero;
                    deathDistanceVector = apathyOwner.body.corePosition - damageReport.damageInfo.position;

                    if (ItemCatalog.FindItemIndex("OverkillOverdrive") != ItemIndex.None)
                    {
                        numberOfOverdrives = apathyOwner.body.inventory.GetItemCount(ItemCatalog.FindItemIndex("OverkillOverdrive"));
                    }
                    if (deathDistanceVector.sqrMagnitude <= (float)Math.Pow(Apathy_Radius.Value + (Apathy_Radius.Value * ((OverkillOverdrive_ZoneIncrease.Value / 100f) * numberOfOverdrives)), 2) && apathyOwner.body.GetBuffCount(apathyBuff) <= 0)
                    {
                        apathyOwner.body.AddBuff(apathyStacks);
                        if (apathyOwner.body.GetBuffCount(apathyStacks) >= Apathy_RequiredKills.Value)
                        {
                            Util.PlaySound(EntityStates.ImpBossMonster.GroundPound.initialAttackSoundString, apathyOwner.body.gameObject);
                            apathyOwner.body.AddTimedBuff(apathyBuff, Apathy_Duration.Value + (Apathy_Duration.Value * (apathyOwner.body.inventory.GetItemCount(itemDef) - 1)));
                            apathyOwner.body.SetBuffCount(apathyStacks.buffIndex, 0);
                            apathyOwner.currentBuffDuration = Apathy_Duration.Value + (Apathy_Duration.Value * (apathyOwner.body.inventory.GetItemCount(itemDef) - 1));
                            apathyOwner.body.RecalculateStats();
                        }
                    }
                }
            }

            void GetStatCoefficients(CharacterBody body, RecalculateStatsAPI.StatHookEventArgs args)
            {
                if (body.GetBuffCount(apathyBuff) > 0)
                {
                    args.moveSpeedMultAdd += Apathy_MoveSpeedAdd.Value;
                    args.attackSpeedMultAdd += Apathy_MoveSpeedAdd.Value;
                    args.baseRegenAdd += Apathy_RegenAdd.Value;
                }
            }

            On.RoR2.CharacterBody.OnInventoryChanged += CharacterBody_OnInventoryChanged;
            On.RoR2.GlobalEventManager.OnCharacterDeath += GlobalEventManager_OnCharacterDeath;
            RecalculateStatsAPI.GetStatCoefficients += GetStatCoefficients;
        }

        public class ApathyBehavior : CharacterBody.ItemBehavior
        {
            public float radius = 0f;
            public float sizeAddMultiplier;
            public float focusCrystalSizeDivisor;
            public float apathyEffectTimer = 0.8f;
            public float currentBuffDuration = 0f;
            public bool enable = false;
            public bool setupDone = false;
            public bool firstBeatFlag = false;
            public bool hasOverlay = false;
            Vector3 defaultScale = new Vector3(0f, 0f, 0f);
            GameObject radiusIndicator;
            GameObject apathyEffect;
            void FixedUpdate()
            {
                if (enable)
                {
                    if (!setupDone)
                    {
                        radiusIndicator = PrefabAPI.InstantiateClone(FocusCrystalPrefab, "ApathyRange");
                        apathyEffect = PrefabAPI.InstantiateClone(HealthComponent.AssetReferences.diamondDamageBonusImpactEffectPrefab, "ApathyPulse");
                        apathyEffect.transform.localScale *= 0.1f;
                        radiusIndicator.GetComponent<NetworkedBodyAttachment>().AttachToGameObjectAndSpawn(base.gameObject, null);
                        radiusIndicator.transform.localScale = defaultScale;
                        defaultScale.x = focusCrystalSizeDivisor;
                        defaultScale.y = focusCrystalSizeDivisor;
                        defaultScale.z = focusCrystalSizeDivisor;
                        setupDone = true;
                    }
                    else
                    {
                        radiusIndicator.transform.localScale = defaultScale + (defaultScale * sizeAddMultiplier);
                        if (body.HasBuff(apathyBuff))
                        {
                            if (hasOverlay == false)
                            {
                                TemporaryOverlay temporaryOverlay = body.gameObject.AddComponent<TemporaryOverlay>();
                                temporaryOverlay.duration = currentBuffDuration;
                                temporaryOverlay.animateShaderAlpha = true;
                                temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
                                temporaryOverlay.destroyComponentOnEnd = true;
                                temporaryOverlay.originalMaterial = ApathyMaterial;
                                temporaryOverlay.AddToCharacerModel(body.gameObject.GetComponent<ModelLocator>().modelTransform.GetComponentInParent<CharacterModel>());
                                hasOverlay = true;
                            }
                            apathyEffectTimer += Time.fixedDeltaTime;
                            if (apathyEffectTimer >= 0.8f && !firstBeatFlag)
                            {
                                EffectManager.SimpleImpactEffect(apathyEffect, body.corePosition, body.corePosition, true);
                                firstBeatFlag = true;
                            }
                            if (apathyEffectTimer >= 1f)
                            {
                                EffectManager.SimpleImpactEffect(apathyEffect, body.corePosition, body.corePosition, true);
                                apathyEffectTimer = 0f;
                                firstBeatFlag = false;
                            }
                        }
                        else
                        {
                            apathyEffectTimer = 0.8f;
                            hasOverlay = false;
                        }
                    }
                }
            }

            private void OnDestroy()
            {
                UnityEngine.Object.Destroy(radiusIndicator);
            }
        }

        public static BuffDef apathyStacks { get; private set; }
        public static BuffDef apathyBuff { get; private set; }
        public static void AddBuffs()
        {
            apathyStacks = ScriptableObject.CreateInstance<BuffDef>();
            apathyStacks.buffColor = new Color(1f, 1f, 1f);
            apathyStacks.canStack = true;
            apathyStacks.isDebuff = false;
            apathyStacks.name = "Apathy";
            apathyStacks.isHidden = false;
            apathyStacks.isCooldown = false;
            apathyStacks.iconSprite = LoadBuffSprite2();
            ContentAddition.AddBuffDef(apathyStacks);

            apathyBuff = ScriptableObject.CreateInstance<BuffDef>();
            apathyBuff.buffColor = new Color(1f, 1f, 1f);
            apathyBuff.canStack = true;
            apathyBuff.isDebuff = false;
            apathyBuff.name = "Apathy Buff";
            apathyBuff.isHidden = false;
            apathyBuff.isCooldown = false;
            apathyBuff.iconSprite = LoadBuffSprite();
            ContentAddition.AddBuffDef(apathyBuff);
        }

        public static void Initiate()
        {
            itemDef = CreateItem();
            ItemAPI.Add(new CustomItem(itemDef, CreateDisplayRules()));
            AddTokens();
            UpdateItemStatus();
            AddBuffs();
            AddHooks();
        }
    }
}
