using System;
using System.Collections.Generic;
using System.Text;
using BepInEx;
using BepInEx.Configuration;
using R2API;
using R2API.Utils;
using RoR2;
using RoR2.Orbs;
using UnityEngine;
using Hex3Mod;
using Hex3Mod.Logging;

namespace Hex3Mod.HelperClasses
{
    public class helpers
    {
        public static GameObject PrepareItemDisplayModel(GameObject itemDisplayModel)
        {
            // I snatched this from Mystics Items, I'd prefer to make my own but I'm unity inept
            ItemDisplay itemDisplay = itemDisplayModel.AddComponent<ItemDisplay>();
            List<CharacterModel.RendererInfo> rendererInfos = new List<CharacterModel.RendererInfo>();
            foreach (Renderer renderer in itemDisplayModel.GetComponentsInChildren<Renderer>())
            {
                CharacterModel.RendererInfo rendererInfo = new CharacterModel.RendererInfo
                {
                    renderer = renderer,
                    defaultMaterial = renderer.material
                };
                rendererInfos.Add(rendererInfo);
            }
            itemDisplay.rendererInfos = rendererInfos.ToArray();

            return itemDisplayModel;
        }

    }
    public static class MysticsCompatibility
    {
        private static bool? _enabled;
        public static bool enabled
        {
            get
            {
                if (_enabled == null)
                {
                    _enabled = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.themysticsword.mysticsitems");
                }
                return (bool)_enabled;
            }
        }
        public static ItemDef ShopTerminalCard
        {
            get
            {
                if (_enabled == true)
                {
                    return MysticsItems.MysticsItemsContent.Items.MysticsItems_KeepShopTerminalOpen;
                }
                else
                {
                    return new ItemDef();
                }
            }
        }
        public static ItemDef ShopTerminalCardConsumed
        {
            get
            {
                if (_enabled == true)
                {
                    return MysticsItems.MysticsItemsContent.Items.MysticsItems_KeepShopTerminalOpenConsumed;
                }
                else
                {
                    return new ItemDef();
                }
            }
        }
    }
    public static class TinkersCompatibility
    {
        private static bool? _enabled;
        public static bool enabled
        {
            get
            {
                if (_enabled == null)
                {
                    _enabled = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.ThinkInvisible.TinkersSatchel");
                }
                return (bool)_enabled;
            }
        }
    }
}
