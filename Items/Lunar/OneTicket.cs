using R2API;
using RoR2;
using UnityEngine;
using Hex3Mod.HelperClasses;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Diagnostics;
using System.ComponentModel;

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
            return pickupModelPrefab;
        }
        public static Sprite LoadSprite()
        {
            Sprite pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/OneTicket.png");
            return pickupIconSprite;
        }
        public static Sprite LoadBuffSprite()
        {
            Sprite pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/OneTicket.png");
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

            item.tags = new ItemTag[]{ ItemTag.Utility, ItemTag.BrotherBlacklist, ItemTag.AIBlacklist }; // Useless on monsters
            item.deprecatedTier = ItemTier.Lunar;
            item.canRemove = true;
            item.hidden = false;

            item.pickupModelPrefab = LoadPrefab();
            item.pickupIconSprite = LoadSprite();

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
            item.pickupIconSprite = LoadSprite();

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
                    else
                    {
                        body.RemoveBuff(ticketStacks.buffIndex);
                    }
                }
                else
                {
                    body.RemoveBuff(ticketStacks.buffIndex);
                }

                int ticketsInExistence = Util.GetItemCountGlobal(itemDef.itemIndex, true);
                if (ticketsInExistence > 0 && body.inventory && body.teamComponent && (body.teamComponent.teamIndex == TeamIndex.Monster | body.teamComponent.teamIndex == TeamIndex.Lunar | body.teamComponent.teamIndex == TeamIndex.Void))
                {
                    args.damageMultAdd += 2f + (2f * (ticketsInExistence - 1));
                    args.healthMultAdd += 4f + (4f * (ticketsInExistence - 1));
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
                        Chat.AddMessage("<style=cIsUtility>You are rewarded for risks taken.</style>");

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
                        body.RemoveBuff(ticketStacks.buffIndex);
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
                int ticketsInExistence = Util.GetItemCountGlobal(itemDef.itemIndex, true);
                if (ticketsInExistence > 0 && self.inventory && self.teamComponent && (self.teamComponent.teamIndex == TeamIndex.Monster | self.teamComponent.teamIndex == TeamIndex.Lunar | self.teamComponent.teamIndex == TeamIndex.Void))
                {
                    self.inventory.SetEquipmentIndex(RoR2Content.Equipment.AffixLunar.equipmentIndex);
                }
                orig(self);
            }

            On.RoR2.CharacterMaster.OnServerStageBegin += CharacterMaster_OnServerStageBegin;
            RecalculateStatsAPI.GetStatCoefficients += GetStatCoefficients;
            On.RoR2.PurchaseInteraction.OnInteractionBegin += PurchaseInteraction_OnInteractionBegin;
            On.RoR2.CharacterBody.Start += CharacterBody_Start;
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
