using R2API;
using RoR2;
using UnityEngine;
using Hex3Mod.HelperClasses;

namespace Hex3Mod.Items
{
    /*
    Mint Condition is an attempt at making a mobility red item. This is good for both Benthic Bloom's playability and even more mobility options in a movement-focused game
    Ben's Raincoat used to be a powerful item for its debuff blocking power, but possibly too powerful. Restricting this item to only some debuffs should make it a more focused effect
    */
    public class MintCondition
    {
        // Create functions here for defining the ITEM, TOKENS, HOOKS and CONFIG. This is simpler than doing it in Main
        static string itemName = "MintCondition"; // Change this name when making a new item
        static string upperName = itemName.ToUpper();
        static ItemDef itemDefinition = CreateItem();
        public static GameObject LoadPrefab()
        {
            GameObject pickupModelPrefab = Main.MainAssets.LoadAsset<GameObject>("Assets/Models/Prefabs/MintConditionPrefab.prefab");
            return pickupModelPrefab;
        }
        public static Sprite LoadSprite()
        {
            Sprite pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Icons/MintCondition.png");
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

            item.tags = new ItemTag[]{ItemTag.Utility}; // Also change these when making a new item
            item.deprecatedTier = ItemTier.Tier3;
            item.canRemove = true;
            item.hidden = false;

            item.pickupModelPrefab = LoadPrefab();
            item.pickupIconSprite = LoadSprite();

            return item;
        }

        public static ItemDisplayRuleDict CreateDisplayRules() // We've figured item displays out!
        {
            GameObject ItemDisplayPrefab = helpers.PrepareItemDisplayModel(PrefabAPI.InstantiateClone(LoadPrefab(), LoadPrefab().name + "Display", false));

            ItemDisplayRuleDict rules = new ItemDisplayRuleDict();
            rules.Add("mdlCommandoDualies", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "FootL",
                        localPos = new Vector3(0.01132F, 0.12471F, 0.03123F),
                        localAngles = new Vector3(85.29122F, 89.97788F, 268.0291F),
                        localScale = new Vector3(-1.13461F, 1.0467F, 1.0632F)
                    }
                }
            );
            rules.Add("mdlHuntress", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "FootL",
                        localPos = new Vector3(0.00556F, 0.08883F, 0.00324F),
                        localAngles = new Vector3(81.64972F, 39.93797F, 221.9832F),
                        localScale = new Vector3(-1.19428F, 1.19259F, 1.19259F)
                    }
                }
            );
            rules.Add("mdlToolbot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "CalfL",
                        localPos = new Vector3(-0.10672F, 3.18186F, -0.73824F),
                        localAngles = new Vector3(281.0628F, 56.6582F, 304.5649F),
                        localScale = new Vector3(10.45477F, 10.45477F, 10.45477F)
                    }
                }
            );
            rules.Add("mdlEngi", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "FootL",
                        localPos = new Vector3(-0.00658F, 0.15874F, 0.02936F),
                        localAngles = new Vector3(81.19193F, 90.81255F, 270.6108F),
                        localScale = new Vector3(-2.03043F, 2.01183F, 1.6818F)
                    }
                }
            );
            rules.Add("mdlMage", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "FootL",
                        localPos = new Vector3(-0.0012F, 0.13523F, 0.02474F),
                        localAngles = new Vector3(50.60272F, 14.16508F, 190.7144F),
                        localScale = new Vector3(-1.06108F, 1.03622F, 0.87889F)
                    }
                }
            );
            rules.Add("mdlMerc", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "FootL",
                        localPos = new Vector3(-0.00396F, 0.17447F, -0.01644F),
                        localAngles = new Vector3(49.75164F, 1.50993F, 181.2459F),
                        localScale = new Vector3(-1.31043F, 1.42759F, 1.21673F)
                    }
                }
            );
            rules.Add("mdlTreebot", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "FootFrontLEnd",
                        localPos = new Vector3(0.13854F, -0.01981F, -0.14856F),
                        localAngles = new Vector3(357.6425F, 312.001F, 184.4179F),
                        localScale = new Vector3(-1.88872F, 2.10885F, 2.10885F)
                    }
                }
            );
            rules.Add("mdlLoader", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "FootL",
                        localPos = new Vector3(0.00488F, 0.22272F, -0.03361F),
                        localAngles = new Vector3(40.20655F, 355.8039F, 180.5376F),
                        localScale = new Vector3(1.73732F, 1.73732F, 1.73732F)
                    }
                }
            );
            rules.Add("mdlCroco", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "FootL",
                        localPos = new Vector3(-0.2068F, 1.41351F, -1.81195F),
                        localAngles = new Vector3(7.74748F, 5.96484F, 180.0678F),
                        localScale = new Vector3(19.34271F, 19.34271F, 19.34271F)
                    }
                }
            );
            rules.Add("mdlCaptain", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "FootL",
                        localPos = new Vector3(0.00975F, 0.20311F, -0.02508F),
                        localAngles = new Vector3(37.14312F, 342.1239F, 179.6053F),
                        localScale = new Vector3(-1.61046F, 1.5221F, 1.5221F)
                    }
                }
            );
            rules.Add("mdlBandit2", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "FootL",
                        localPos = new Vector3(0.00122F, 0.19279F, -0.04388F),
                        localAngles = new Vector3(44.38901F, 13.30392F, 189.6347F),
                        localScale = new Vector3(-1.48986F, 1.6513F, 1.42553F)
                    }
                }
            );
            rules.Add("EngiTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "LegBar1",
                        localPos = new Vector3(0F, 1.78346F, -0.58545F),
                        localAngles = new Vector3(61.16201F, 180F, 180F),
                        localScale = new Vector3(4.67824F, 4.67824F, 4.67824F)
                    }
                }
            );
            rules.Add("EngiWalkerTurretBody", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "FootF",
                        localPos = new Vector3(0.01399F, 0.32434F, -0.36431F),
                        localAngles = new Vector3(88.63456F, 180F, 180F),
                        localScale = new Vector3(3.54235F, 3.54235F, 3.54235F)
                    }
                }
            );
            rules.Add("mdlScav", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "FootL",
                        localPos = new Vector3(-0.04414F, 0.07749F, -2.31831F),
                        localAngles = new Vector3(16.44856F, 178.8749F, 355.4724F),
                        localScale = new Vector3(-16.14141F, -20.94848F, -16.14141F)
                    }
                }
            );
            rules.Add("mdlRailGunner", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "ToeL",
                        localPos = new Vector3(-0.06672F, 0.11091F, 0.00429F),
                        localAngles = new Vector3(85.14207F, 40.24481F, 310.826F),
                        localScale = new Vector3(-2.08739F, 2.14087F, 1.77939F)
                    }
                }
            );
            rules.Add("mdlVoidSurvivor", new RoR2.ItemDisplayRule[]{new RoR2.ItemDisplayRule{
                        ruleType = ItemDisplayRuleType.ParentedPrefab,
                        followerPrefab = ItemDisplayPrefab,
                        childName = "FootL",
                        localPos = new Vector3(0.0013F, 0.16675F, -0.01337F),
                        localAngles = new Vector3(54.3084F, 274.4107F, 185.6223F),
                        localScale = new Vector3(-1.52044F, 1.50761F, 1.50761F)
                    }
                }
            );

            return rules;
        }

        public static void AddTokens(float MintCondition_MoveSpeed, float MintCondition_MoveSpeedStack, int MintCondition_AddJumps, int MintCondition_AddJumpsStack)
        {
            float MintCondition_MoveSpeed_Readable = MintCondition_MoveSpeed * 100f;
            float MintCondition_MoveSpeedStack_Readable = MintCondition_MoveSpeedStack * 100f;

            LanguageAPI.Add("H3_" + upperName + "_NAME", "Mint Condition");
            LanguageAPI.Add("H3_" + upperName + "_PICKUP", "You are immune to movement restricting status effects. Gain movement speed and extra jumps.");
            LanguageAPI.Add("H3_" + upperName + "_DESC", "Provides immunity to all movement restricting status effects. Gain <style=cIsUtility>" + MintCondition_MoveSpeed_Readable + "%</style> <style=cStack>(+" + MintCondition_MoveSpeedStack_Readable + "% per stack)</style> movement speed and <style=cIsUtility>" + MintCondition_AddJumps + "</style> <style=cStack>(+" + MintCondition_AddJumpsStack + " per stack)</style> extra jumps.");
            LanguageAPI.Add("H3_" + upperName + "_LORE", 
                "\nOrder: Eclipse 380 Boosts(Mint Condition)" +
                "\nTracking Number: 240******" +
                "\nEstimated Delivery: 08/29/2056" +
                "\nShipping Method: Priority" +
                "\nShipping Address: 70 Marshall Road, Chicago, Earth" +
                "\nShipping Details:" +
                "\n\nHandle carefully. Do not turn upside-down and keep shoes in vacuum seal. If nobody answers the door for the delivery, send it to the post office for safe-keeping, don't just leave it on the doorstep." +
                "\n\nEdit by Customer 240****** (02/22/2056):" +
                "\nHello my son recently used your site to buy some of your shoes. Are you kidding me? $4350 for shoes? I can assure you this purchase was NOT cleared by me (the home owner) and will be immediately cancelled. Your support team has not answered my emails and calls but still has the money for the shoes. If you do not CANCEL and REFUND this order immediately I will have to take this up with the company, and if they dont answer me either you'll be sued" +
                "\n\nEdit by Customer 240****** (03/02/2056)" +
                "\n\"Lost in transit\" are you joking??? you STILL HAVE the money it has not been refunded. I will sue your [REDACTED] [REDACTED] to mars and back if you keep turning down my calls, believe it you [REDACTED]. You don't know what kind of [REDACTED] youve gotten yourselves into now");
        }

        private static void AddHooks(ItemDef itemDefToHooks, float MintCondition_MoveSpeed, float MintCondition_MoveSpeedStack, int MintCondition_AddJumps, int MintCondition_AddJumpsStack) // Insert hooks here
        {
            void H3_MobilityIncreaseRoR(CharacterBody body) // I don't know how to effectively add base move speed, so doing it separately through r2api is the safe option
            {
                if (body && body.inventory)
                {
                    int itemCount = body.inventory.GetItemCount(itemDefToHooks);
                    if (itemCount > 0)
                    {
                        body.maxJumpCount += MintCondition_AddJumps + (MintCondition_AddJumpsStack * (itemCount - 1));
                    }
                }
            }

            void H3_MobilityIncreaseR2API(CharacterBody body, RecalculateStatsAPI.StatHookEventArgs args)
            {
                if (body && body.inventory)
                {
                    int itemCount = body.inventory.GetItemCount(itemDefToHooks);
                    if (itemCount > 0)
                    {
                        args.baseMoveSpeedAdd += MintCondition_MoveSpeed + (MintCondition_MoveSpeedStack * (itemCount - 1));
                    }
                }
            }

            void H3_PreventFreeze(DamageReport damageReport) // Make sure that no received damage can be stunning or freezing
            {
                if (damageReport.victim && damageReport.victim.body && damageReport.victim.body.inventory)
                {
                    int itemCount = damageReport.victim.body.inventory.GetItemCount(itemDefToHooks);
                    if (itemCount > 0 && damageReport.damageInfo != null)
                    {
                        SetStateOnHurt component = damageReport.victim.body.GetComponent<SetStateOnHurt>();
                        if (component)
                        {
                            component.canBeFrozen = false;
                            component.canBeStunned = false;
                        }
                    }
                }
            }

            void H3_RemoveMovementBuff(CharacterBody body, BuffDef receivedBuff)
            {
                if (body && body.master && body.inventory)
                {
                    int itemCount = body.inventory.GetItemCount(itemDefToHooks); // Now, for a looong list of all the movement-restricting buffs I could find...
                    if (itemCount > 0)
                    {
                        if (receivedBuff.name == "bdBeetleJuice" || receivedBuff.name == "bdClayGoo" || receivedBuff.name == "bdCripple" || receivedBuff.name == "bdNullified" || receivedBuff.name == "bdNullifyStack" || receivedBuff.name == "bdSlow50" || receivedBuff.name == "bdSlow60" || receivedBuff.name == "bdSlow80" || receivedBuff.name == "bdWeak" || receivedBuff.name == "bdLunarSecondaryRoot" || receivedBuff.name == "bdEntangle" || receivedBuff.name == "bdJailerSlow" || receivedBuff.name == "bdJailerTether" || receivedBuff.name == "bdSlow30")
                        {
                            body.RemoveBuff(receivedBuff); // Remove the buff if it matches the bill
                        }
                    }
                }
            }

            On.RoR2.CharacterBody.RecalculateStats += (orig, self) => // Finally, add the hooks... surely none could go wrong
            {
                orig(self);
                H3_MobilityIncreaseRoR(self);
            };
            On.RoR2.CharacterBody.OnBuffFirstStackGained += (orig, self, receivedBuff) =>
            {
                orig(self, receivedBuff);
                H3_RemoveMovementBuff(self, receivedBuff);
            };
            On.RoR2.SetStateOnHurt.OnTakeDamageServer += (orig, self, damageReport) =>
            {
                H3_PreventFreeze(damageReport);
                orig(self, damageReport);
            };
            RecalculateStatsAPI.GetStatCoefficients += H3_MobilityIncreaseR2API;
        }

        public static void Initiate(float MintCondition_MoveSpeed, float MintCondition_MoveSpeedStack, int MintCondition_AddJumps, int MintCondition_AddJumpsStack) // Finally, initiate the item and all of its features
        {
            CreateItem();
            ItemAPI.Add(new CustomItem(itemDefinition, CreateDisplayRules()));
            AddTokens(MintCondition_MoveSpeed, MintCondition_MoveSpeedStack, MintCondition_AddJumps, MintCondition_AddJumpsStack);
            AddHooks(itemDefinition, MintCondition_MoveSpeed, MintCondition_MoveSpeedStack, MintCondition_AddJumps, MintCondition_AddJumpsStack);
        }
    }
}
