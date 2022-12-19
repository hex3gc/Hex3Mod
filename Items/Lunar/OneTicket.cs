using R2API;
using RoR2;
using RoR2.Achievements;
using UnityEngine;
using Hex3Mod.HelperClasses;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Diagnostics;
using System.ComponentModel;
using EntityStates.AffixVoid;
using Hex3Mod.Utils;

namespace Hex3Mod.Items
{
    /*
    Four Hundred Tickets' cooler older cousin
    Making a Tickets variant that granted extra items was too OP in many situations, so this iteration forces you to deal with a heavy downside before a big reward
    */
    public class OneTicket
    {
        static string itemName = "OneTicket";
        static string upperName = itemName.ToUpper();
        static ItemDef itemDefinition = CreateItem();
        public static ItemDef consumedItemDefinition = CreateConsumedItem();
        public static ItemDef hiddenItemDefinition = CreateHiddenItem();
        public static GameObject LoadPrefab()
        {
            GameObject pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/OneTicketPrefab.prefab");
            if (Main.debugMode == true)
            {
                pickupModelPrefab.GetComponentInChildren<Renderer>().gameObject.AddComponent<MaterialControllerComponents.HGControllerFinder>();
            }
            return pickupModelPrefab;
        }
        public static Sprite LoadSprite()
        {
            Sprite pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/OneTicket.png");
            return pickupIconSprite;
        }
        public static Sprite LoadBuffSprite()
        {
            Sprite pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/Buff_OneTicket.png");
            return pickupIconSprite;
        }
        public static Sprite LoadAchievementSprite()
        {
            Sprite achievementIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/OneTicketAchievement.png");
            return achievementIconSprite;
        }
        public static ItemDef CreateItem()
        {
            ItemDef item = ScriptableObject.CreateInstance<ItemDef>();
            UnlockableDef oneTicketUnlock = ScriptableObject.CreateInstance<UnlockableDef>();

            oneTicketUnlock.cachedName = "OneTicketUnlock";
            oneTicketUnlock.nameToken = upperName + "_UNLOCK_NAME";
            oneTicketUnlock.achievementIcon = LoadAchievementSprite();

            item.name = itemName;
            item.nameToken = "H3_" + upperName + "_NAME";
            item.pickupToken = "H3_" + upperName + "_PICKUP";
            item.descriptionToken = "H3_" + upperName + "_DESC";
            item.loreToken = "H3_" + upperName + "_LORE";

            item.tags = new ItemTag[]{ ItemTag.Utility, ItemTag.BrotherBlacklist, ItemTag.AIBlacklist }; // Useless on monsters
            item.deprecatedTier = ItemTier.Lunar;
            item.canRemove = true;
            item.hidden = false;

            item.pickupModelPrefab = LoadPrefab();
            item.pickupIconSprite = LoadSprite();

            ContentAddition.AddUnlockableDef(oneTicketUnlock);
            item.unlockableDef = oneTicketUnlock;
            return item;
        }
        public static ItemDef CreateConsumedItem()
        {
            ItemDef item = ScriptableObject.CreateInstance<ItemDef>();

            item.name = itemName + "Consumed";
            item.nameToken = "H3_" + upperName + "CONSUMED_NAME";
            item.pickupToken = "H3_" + upperName + "CONSUMED_PICKUP";
            item.descriptionToken = "H3_" + upperName + "CONSUMED_DESC";

            item.tags = new ItemTag[] { ItemTag.CannotCopy, ItemTag.CannotDuplicate, ItemTag.AIBlacklist, ItemTag.BrotherBlacklist };
            item.deprecatedTier = ItemTier.NoTier;
            item.canRemove = false;
            item.hidden = false;

            item.pickupModelPrefab = LoadPrefab();
            item.pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/OneTicket_Consumed.png");

            return item;
        }
        public static ItemDef CreateHiddenItem()
        {
            ItemDef item = ScriptableObject.CreateInstance<ItemDef>();

            item.name = itemName + "Hidden";
            item.nameToken = "H3_" + upperName + "HIDDEN_NAME";
            item.pickupToken = "H3_" + upperName + "HIDDEN_NAME";
            item.descriptionToken = "H3_" + upperName + "HIDDEN_NAME";

            item.tags = new ItemTag[] { ItemTag.CannotCopy, ItemTag.CannotDuplicate, ItemTag.AIBlacklist, ItemTag.BrotherBlacklist }; // Need to make sure the item can't be given or cloned
            item.deprecatedTier = ItemTier.NoTier;
            item.canRemove = false;
            item.hidden = true;

            return item;
        }

        public static ItemDisplayRuleDict CreateDisplayRules()
        {
            GameObject ItemDisplayPrefab = helpers.PrepareItemDisplayModel(PrefabAPI.InstantiateClone(LoadPrefab(), LoadPrefab().name + "Display", false));

            ItemDisplayRuleDict rules = new ItemDisplayRuleDict();
            rules.Add("mdlCommandoDualies", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Base",
                        localPos = new Vector3(-0.38684F, 0F, -0.00001F),
                        localAngles = new Vector3(25.14539F, 57.38155F, 221.496F),
                        localScale = new Vector3(0.19258F, 0.19258F, 0.19258F)
                    }
                }
            );
            rules.Add("mdlHuntress", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Base",
                        localPos = new Vector3(0.35564F, -0.21171F, -0.00001F),
                        localAngles = new Vector3(0.00003F, 347.2937F, 289.0848F),
                        localScale = new Vector3(0.14591F, 0.14591F, 0.14591F)
                    }
                }
            );
            rules.Add("mdlToolbot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Base",
                        localPos = new Vector3(-5.51267F, 2.50042F, -0.81135F),
                        localAngles = new Vector3(339.3259F, 152.71F, 11.07411F),
                        localScale = new Vector3(1.63014F, 1.63014F, 1.63014F)
                    }
                }
            );
            rules.Add("mdlEngi", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Base",
                        localPos = new Vector3(0.54471F, -0.14013F, -0.02633F),
                        localAngles = new Vector3(348.1776F, 342.9403F, 311.9007F),
                        localScale = new Vector3(0.18951F, 0.17094F, 0.17094F)
                    }
                }
            );
            rules.Add("mdlMage", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Base",
                        localPos = new Vector3(-0.30597F, -0.48563F, -0.00003F),
                        localAngles = new Vector3(2.69413F, 130.1902F, 272.2738F),
                        localScale = new Vector3(0.13215F, 0.13215F, 0.13215F)
                    }
                }
            );
            rules.Add("mdlMerc", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Base",
                        localPos = new Vector3(-0.30884F, -0.20682F, -0.00002F),
                        localAngles = new Vector3(20.82046F, 92.46006F, 250.6549F),
                        localScale = new Vector3(0.15085F, 0.15085F, 0.15085F)
                    }
                }
            );
            rules.Add("mdlTreebot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "PlatformBase",
                        localPos = new Vector3(-1.51006F, -0.00002F, -0.14861F),
                        localAngles = new Vector3(0.0001F, 141.6828F, -0.00001F),
                        localScale = new Vector3(0.34822F, 0.34822F, 0.34822F)
                    }
                }
            );
            rules.Add("mdlLoader", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Base",
                        localPos = new Vector3(-0.51045F, -0.4641F, 0F),
                        localAngles = new Vector3(9.0467F, 100.8493F, 271.7261F),
                        localScale = new Vector3(0.14454F, 0.14454F, 0.14454F)
                    }
                }
            );
            rules.Add("mdlCroco", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "SpineChest1",
                        localPos = new Vector3(4.56075F, 0.56272F, 3.08516F),
                        localAngles = new Vector3(10.45312F, 344.764F, 9.91819F),
                        localScale = new Vector3(1.35486F, 1.35486F, 1.36016F)
                    }
                }
            );
            rules.Add("mdlCaptain", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Base",
                        localPos = new Vector3(0.7441F, 0F, 0.00002F),
                        localAngles = new Vector3(0.02976F, 0.16507F, 339.595F),
                        localScale = new Vector3(0.25523F, 0.21078F, 0.21078F)
                    }
                }
            );
            rules.Add("mdlBandit2", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Pelvis",
                        localPos = new Vector3(0.24297F, -0.04822F, -0.38465F),
                        localAngles = new Vector3(339.9618F, 29.80922F, 333.0994F),
                        localScale = new Vector3(0.15308F, 0.15308F, 0.15308F)
                    }
                }
            );
            rules.Add("EngiTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Base",
                        localPos = new Vector3(1.37689F, 1.66679F, 0.25339F),
                        localAngles = new Vector3(32.48184F, 317.041F, 320.2357F),
                        localScale = new Vector3(0.67567F, 0.67567F, 0.67567F)
                    }
                }
            );
            rules.Add("EngiWalkerTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Base",
                        localPos = new Vector3(2.12827F, 1.25267F, 0.36514F),
                        localAngles = new Vector3(1.3384F, 316.5583F, 333.2706F),
                        localScale = new Vector3(0.78879F, 0.78879F, 0.78879F)
                    }
                }
            );
            rules.Add("mdlScav", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "HandL",
                        localPos = new Vector3(8.95256F, -3.9939F, -18.84292F),
                        localAngles = new Vector3(68.08115F, 20.07728F, 313.8858F),
                        localScale = new Vector3(4.3872F, 4.3872F, 4.3872F)
                    }
                }
            );
            rules.Add("mdlRailGunner", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Base",
                        localPos = new Vector3(0.49771F, 0.3535F, -0.03917F),
                        localAngles = new Vector3(299.1913F, 287.1414F, 66.45328F),
                        localScale = new Vector3(0.23906F, 0.23906F, 0.23906F)
                    }
                }
            );
            rules.Add("mdlVoidSurvivor", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "Base",
                        localPos = new Vector3(-0.5479F, 0.40061F, 0.04804F),
                        localAngles = new Vector3(353.8923F, 134.0595F, 356.0528F),
                        localScale = new Vector3(0.20457F, 0.20457F, 0.20457F)
                    }
                }
            );

            return rules;
        }

        public static ItemDisplayRuleDict CreateHiddenDisplayRules()
        {
            return new ItemDisplayRuleDict();
        }

        public static void AddTokens()
        {
            LanguageAPI.Add("H3_" + upperName + "_NAME", "One Ticket");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "Clearing stages increases this ticket's value, which can be exchanged at a <style=cShrine>cleansing pool</style> for <style=cDeath>legendary items</style>... <style=cDeath>but ALL enemies become Perfected Elites while you're holding it.</style>");
            LanguageAPI.Add("H3_" + upperName + "_DESC", "This ticket grants <style=cDeath>legendary items</style> when deposited into a <style=cShrine>cleansing pool</style>: One <style=cStack>(+1 per stack)</style> for each stage cleared while holding it. <style=cDeath>ALL enemies spawn as Perfected Elites while it is in your inventory, and become stronger with additional stacks.</style>");
            LanguageAPI.Add("H3_" + upperName + "_LORE", "\"A REAL Moon-Casino ticket!\"" +
            "\n\n\"You know how rare these things are? I could sell this for thousands- no- MILLIONS online... maybe more...\"" +
            "\n\n\"Wait- you? No, I found it first.\"" +
            "\n\n\"I saw it before you did.\"" +
            "\n\n\"No no, I picked it up. That gives me ownership.\"" +
            "\n\n\"Come on, we're partners- at least split it with me...\"" +
            "\n\n\"Get your hands off!\"" +
            "\n\n\"Mother<style=cStack>[REDACTED]</style>er!\"" +
            "\n\nThe remaining contents of the log are deemed too graphic to show to the Board. Both employees were found dead days later, with the 'ticket' in question missing. So, now that we have a lead: How shall we proceed with the reclamation?");

            LanguageAPI.Add("H3_" + upperName + "CONSUMED_NAME", "Cleansed Ticket");
            LanguageAPI.Add("H3_" + upperName + "CONSUMED_PICKUP", "Cleansed of all value.");
            LanguageAPI.Add("H3_" + upperName + "CONSUMED_DESC", "Cleansed of all value.");

            LanguageAPI.Add("ACHIEVEMENT_" + upperName + "_NAME", "It's A Feature");
            LanguageAPI.Add("ACHIEVEMENT_" + upperName + "_DESCRIPTION", "Use 400 Tickets to duplicate the contents of a Scavenger's bag.");
            LanguageAPI.Add(upperName + "_UNLOCK_NAME", "It's A Feature");
        }

        private static void AddHooks(ItemDef itemDef, ItemDef consumedItemDef, ItemDef hiddenItemDef)
        {
            // Give a hidden item every stage to ensure the player has been holding the ticket since the stage began
            void CharacterMaster_OnServerStageBegin(On.RoR2.CharacterMaster.orig_OnServerStageBegin orig, CharacterMaster self, Stage stage)
            {
                orig(self, stage);
                if (stage.sceneDef && stage.sceneDef.sceneType == SceneType.Stage && self.inventory && self.inventory.GetItemCount(itemDef) > 0)
                {
                    self.inventory.GiveItem(hiddenItemDef, self.inventory.GetItemCount(itemDef));
                }
            }

            // Set One Ticket buffs and buff enemy stats
            void GetStatCoefficients(CharacterBody body, RecalculateStatsAPI.StatHookEventArgs args)
            {
                if (body.inventory && body.inventory.GetItemCount(itemDef) > 0)
                {
                    if (body.inventory.GetItemCount(hiddenItemDef) > 0)
                    {
                        body.SetBuffCount(ticketStacks.buffIndex, body.inventory.GetItemCount(hiddenItemDef));
                    }
                }
                if (body.inventory && body.inventory.GetItemCount(itemDef) <= 0 && body.GetBuffCount(ticketStacks.buffIndex) > 0)
                {
                    body.SetBuffCount(ticketStacks.buffIndex, 0);
                }

                int ticketsInExistence = Util.GetItemCountGlobal(itemDef.itemIndex, true);
                if (ticketsInExistence > 0 && body.inventory && body.teamComponent && (body.teamComponent.teamIndex == TeamIndex.Monster | body.teamComponent.teamIndex == TeamIndex.Lunar | body.teamComponent.teamIndex == TeamIndex.Void))
                {
                    args.damageMultAdd += 1f + (1f * (ticketsInExistence - 1));
                    args.healthMultAdd += 2f + (2f * (ticketsInExistence - 1));
                }
            }

            // If One Ticket is present while activating with Cleansing Pool, run a special method
            void PurchaseInteraction_OnInteractionBegin(On.RoR2.PurchaseInteraction.orig_OnInteractionBegin orig, PurchaseInteraction self, Interactor activator)
            {
                if (self.costType == CostTypeIndex.LunarItemOrEquipment && activator.TryGetComponent(out CharacterBody body) && body.inventory && body.inventory.GetItemCount(itemDef) > 0 && body.master)
                {
                    Inventory inventory = body.inventory;
                    if (inventory.GetItemCount(hiddenItemDef) <= 0)
                    {
                        orig(self, activator);
                    }
                    else
                    {
                        Chat.AddMessage("<style=cIsUtility>You are rewarded for the risks you've taken.</style>");

                        Xoroshiro128Plus rng = new Xoroshiro128Plus(Run.instance.stageRng.nextUlong);
                        List<PickupIndex> listOfLegendaries = new List<PickupIndex>();
                        List<PickupIndex> listOfDrops = new List<PickupIndex>();
                        listOfLegendaries = Run.instance.availableTier3DropList;

                        // Get a list of legendaries to drop
                        for (int i = 0; i < body.inventory.GetItemCount(hiddenItemDef) * body.inventory.GetItemCount(itemDef); i++)
                        {
                            rng.Next();
                            Util.ShuffleList(listOfLegendaries, rng);
                            listOfDrops.Add(listOfLegendaries.First());
                        }

                        // Drop legendaries around the pool
                        Transform transform = self.transform;
                        float angle = 360f / (float)listOfDrops.Count;
                        Vector3 vector = Vector3.up * 20f + transform.forward * 2f;
                        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
                        for (int i = 0; i < listOfDrops.Count; i++)
                        {
                            PickupDropletController.CreatePickupDroplet(listOfDrops[i], transform.position + Vector3.up * 1.5f, vector);
                            vector = rotation * vector;
                        }

                        // Remove all ticket-related items
                        inventory.RemoveItem(itemDef, inventory.GetItemCount(itemDef));
                        inventory.RemoveItem(hiddenItemDef, inventory.GetItemCount(hiddenItemDef));
                        CharacterMasterNotificationQueue.SendTransformNotification(body.master, itemDef.itemIndex, consumedItemDef.itemIndex, CharacterMasterNotificationQueue.TransformationType.Default);
                        PurchaseInteraction.CreateItemTakenOrb(body.corePosition, self.gameObject, itemDef.itemIndex);
                        inventory.GiveItem(consumedItemDef);
                        body.SetBuffCount(ticketStacks.buffIndex, 0);
                    }
                }
                else
                {
                    orig(self, activator);
                }
            }

            // Spawn enemies as Perfected elites
            void CharacterBody_Start(On.RoR2.CharacterBody.orig_Start orig, CharacterBody self)
            {
                orig(self);
                int ticketsInExistence = Util.GetItemCountGlobal(itemDef.itemIndex, true);
                if (ticketsInExistence > 0 && self.inventory && self.teamComponent && (self.teamComponent.teamIndex == TeamIndex.Monster || self.teamComponent.teamIndex == TeamIndex.Lunar || self.teamComponent.teamIndex == TeamIndex.Void))
                {
                    self.inventory.SetEquipmentIndex(RoR2Content.Equipment.AffixLunar.equipmentIndex);
                    self.isElite = true;
                }
            }

            // Prevents Cripple while holding
            void CharacterBody_OnBuffFirstStackGained(On.RoR2.CharacterBody.orig_OnBuffFirstStackGained orig, CharacterBody self, BuffDef buffDef)
            {
                orig(self, buffDef);
                if (self.inventory && self.inventory.GetItemCount(itemDef) > 0 && buffDef.name == "bdCripple")
                {
                    self.RemoveBuff(buffDef);
                }
            }

            On.RoR2.CharacterMaster.OnServerStageBegin += CharacterMaster_OnServerStageBegin;
            RecalculateStatsAPI.GetStatCoefficients += GetStatCoefficients;
            On.RoR2.PurchaseInteraction.OnInteractionBegin += PurchaseInteraction_OnInteractionBegin;
            On.RoR2.CharacterBody.Start += CharacterBody_Start;
            On.RoR2.CharacterBody.OnBuffFirstStackGained += CharacterBody_OnBuffFirstStackGained;
        }

        public static BuffDef ticketStacks { get; private set; }
        public static void AddBuffs() // Shows how many uses are left in a pack
        {
            ticketStacks = ScriptableObject.CreateInstance<BuffDef>();
            ticketStacks.buffColor = new Color(1f, 1f, 1f);
            ticketStacks.canStack = true;
            ticketStacks.isDebuff = false;
            ticketStacks.name = "One Ticket Value";
            ticketStacks.isHidden = false;
            ticketStacks.isCooldown = false;
            ticketStacks.iconSprite = LoadBuffSprite();
            ContentAddition.AddBuffDef(ticketStacks);
        }

        [RegisterAchievement("OneTicket", "OneTicketUnlock", null, typeof(OneTicketAchievement))]
        public class OneTicketAchievement : BaseAchievement
        {
            public override void OnInstall()
            {
                base.OnInstall();
                On.RoR2.ChestBehavior.ItemDrop += ChestBehavior_ItemDrop;
            }

            public override void OnUninstall()
            {
                On.RoR2.ChestBehavior.ItemDrop -= ChestBehavior_ItemDrop;
                base.OnUninstall();
            }

            private void ChestBehavior_ItemDrop(On.RoR2.ChestBehavior.orig_ItemDrop orig, ChestBehavior self)
            {
                if (localUser != null && self.dropCount > 1 && self.name.Contains("Scav"))
                {
                    Grant();
                }
                orig(self);
            }
        }

        public static void Initiate()
        {
            ItemAPI.Add(new CustomItem(itemDefinition, CreateDisplayRules()));
            ItemAPI.Add(new CustomItem(consumedItemDefinition, CreateHiddenDisplayRules()));
            ItemAPI.Add(new CustomItem(hiddenItemDefinition, CreateHiddenDisplayRules()));
            AddBuffs();
            AddTokens();
            AddHooks(itemDefinition, consumedItemDefinition, hiddenItemDefinition);
        }
    }
}
