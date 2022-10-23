using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using UnityEngine;
using Verse;
using HarmonyLib;

namespace LupineWitch.ConfigurableShelfCapacity
{
    [StaticConstructorOnStartup]
    public static class ConfigurableShelfCapacity
    {
        static ConfigurableShelfCapacity()
        {
            ConfigurableShelfCapacitySettings.ShelfDef =  DefDatabase<ThingDef>.GetNamed("Shelf");
            ConfigurableShelfCapacitySettings.ShelfSmallDef =  DefDatabase<ThingDef>.GetNamed("ShelfSmall");
            ConfigurableShelfCapacitySettings.ApplySettings();

            Harmony.DEBUG = true;
            var harmony = new Harmony("patch.shelfutils.lupinewitch.mods");
            harmony.PatchAll();
        }
    }
    
    public class ConfigurableShelfCapacityMod : Mod
    {
        public ConfigurableShelfCapacitySettings Settings;

        public ConfigurableShelfCapacityMod(ModContentPack content) : base(content)
        {
            Settings = GetSettings<ConfigurableShelfCapacitySettings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);

            listingStandard.Label(string.Format("Items should tower up to {0} stacks per cell (minimum 2)", ConfigurableShelfCapacitySettings.SplitVisualStackCount));
            string towerThresholdBuffer = ConfigurableShelfCapacitySettings.SplitVisualStackCount.ToString();
            listingStandard.IntEntry(ref ConfigurableShelfCapacitySettings.SplitVisualStackCount, ref towerThresholdBuffer);

            listingStandard.Label(string.Format("Minimum capacity is {0}, maximum capacity is {1}", ConfigurableShelfCapacitySettings.MIN_SHELF_CAPACITY, ConfigurableShelfCapacitySettings.MAX_SHELF_CAPACITY));
            listingStandard.Label("Regular Shelf Max Per Cell Capacity");
            string shelfBuffer = ConfigurableShelfCapacitySettings.RegularShelfPerCellCapacity.ToString();
            listingStandard.IntEntry(ref ConfigurableShelfCapacitySettings.RegularShelfPerCellCapacity, ref shelfBuffer);
            
            listingStandard.Label("Small Shelf Max Per Cell Capacity");
            string smallShelfBuffer = ConfigurableShelfCapacitySettings.SmallShelfPerCellCapacity.ToString();
            listingStandard.IntEntry(ref ConfigurableShelfCapacitySettings.SmallShelfPerCellCapacity, ref smallShelfBuffer);
            listingStandard.End();

            ConfigurableShelfCapacitySettings.SplitVisualStackCount = ConfigurableShelfCapacitySettings.SplitVisualStackCount.Clamp(2, int.MaxValue);
            ConfigurableShelfCapacitySettings.SmallShelfPerCellCapacity = ConfigurableShelfCapacitySettings.SmallShelfPerCellCapacity.Clamp(ConfigurableShelfCapacitySettings.MIN_SHELF_CAPACITY, ConfigurableShelfCapacitySettings.MAX_SHELF_CAPACITY);
            ConfigurableShelfCapacitySettings.RegularShelfPerCellCapacity = ConfigurableShelfCapacitySettings.RegularShelfPerCellCapacity.Clamp(ConfigurableShelfCapacitySettings.MIN_SHELF_CAPACITY, ConfigurableShelfCapacitySettings.MAX_SHELF_CAPACITY);

            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "Configurable Shelf Capacity";
        }

        public override void WriteSettings()
        {
            ConfigurableShelfCapacitySettings.ApplySettings();
            base.WriteSettings();
        }
    }
}
