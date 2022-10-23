using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace LupineWitch.ConfigurableShelfCapacity
{
    public class ConfigurableShelfCapacitySettings : ModSettings
    {
        public const int MAX_SHELF_CAPACITY = 80;
        public const int MIN_SHELF_CAPACITY = 1;

        public static int SmallShelfPerCellCapacity = 3;
        public static int RegularShelfPerCellCapacity = 3;
        public static int SplitVisualStackCount = 3;

        public static ThingDef ShelfDef;
        public static ThingDef ShelfSmallDef;


        public override void ExposeData()
        {
            Scribe_Values.Look(ref SmallShelfPerCellCapacity, "SmallShelfPerCellCapacity",3);
            Scribe_Values.Look(ref RegularShelfPerCellCapacity, "RegularShelfPerCellCapacity",3);
            Scribe_Values.Look(ref SplitVisualStackCount, "SplitVisualStackCount", 3);
            base.ExposeData();
        }

        public static void ApplySettings()
        {
            ShelfDef.building.maxItemsInCell = RegularShelfPerCellCapacity;
            ShelfSmallDef.building.maxItemsInCell = SmallShelfPerCellCapacity;
        }
    }
}
