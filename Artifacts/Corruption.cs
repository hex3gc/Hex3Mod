using BepInEx;
using BepInEx.Configuration;
using R2API;
using R2API.Utils;
using RoR2;
using RoR2.ExpansionManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Hex3Mod;
using Hex3Mod.Logging;
namespace Hex3Mod.Artifacts
{
    public class ArtifactOfCorruption
    {
        static string artifactName = "ArtifactOfCorruption";
        static string upperName = artifactName.ToUpper();
        static ArtifactDef artifactDefinition = CreateArtifact(); // Basically the same as making an item
        public static bool artifactEnabled => RunArtifactManager.instance.IsArtifactEnabled(artifactDefinition);


        public static ArtifactDef CreateArtifact()
        {
            ArtifactDef artifact = ScriptableObject.CreateInstance<ArtifactDef>();

            artifact.cachedName = artifactName;
            artifact.nameToken = "H3_" + upperName + "_NAME";
            artifact.descriptionToken = "H3_" + upperName + "_DESC";

            artifact.smallIconSelectedSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Textures/Icons/ArtifactOfCorruption_On.png");
            artifact.smallIconDeselectedSprite = Main.MainAssets.LoadAsset<Sprite>("Assets/Textures/Icons/ArtifactOfCorruption_Off.png");

            return artifact;
        }
        public static void AddTokens()
        {
            LanguageAPI.Add("H3_" + upperName + "_NAME", "Artifact of Corruption");
            LanguageAPI.Add("H3_" + upperName + "_DESC", "Gives you one <style=cIsVoid>Corrupting Parasite</style> each stage.");
        }

        public static void AddHooks(ArtifactDef artifactDefToHooks) // Insert hooks here
        {
            void giveParasite(CharacterMaster master)
            {
                if (artifactEnabled == true && master.inventory && ItemCatalog.FindItemIndex("CorruptingParasite") != ItemIndex.None)
                {
                    master.inventory.GiveItemString("CorruptingParasite"); // Simple, easy, effective
                }
            }

            On.RoR2.CharacterMaster.OnServerStageBegin += (orig, self, stage) =>
            {
                orig(self, stage);
                giveParasite(self);
            };
        }
        public static void Initiate()
        {
            CreateArtifact();
            ContentAddition.AddArtifactDef(artifactDefinition);
            AddTokens();
            AddHooks(artifactDefinition);
        }
    }
}
