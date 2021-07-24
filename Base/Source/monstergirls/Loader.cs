using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using Verse;

namespace monstergirlsbase
{
    [DefOf]
    public class MilkDefOf
    {
        public static ThingDef Milk;
        public static ThingDef CentaurMilk;
        public static ThingDef CowgirlMilk;
        public static ThingDef DryadMilk;
        public static ThingDef ThrumbogirlMilk;
        public static ThingDef ImpmotherMilk;
        public static ThingDef DragonMilk;
        public static ThingDef FoxgirlMilk;
    }

    [StaticConstructorOnStartup]
    public static class Loader
    {
        static Loader()
        {
            LongEventHandler.QueueLongEvent(new Action(Init), "LibraryStartup", false, null);
        }

        private static void Init()
        {
            if (Settings.UseMonsterGirlMilk)
            {
                SetMilk();
                DisableMilk();
            }
        }

        public static void SetMilk()
        {
            if (!Settings.DisableMilk && Settings.UseMonsterGirlMilk)
            {
                DefDatabase<ThingDef>.GetNamed("Centaur").GetCompProperties<CompProperties_Milkable>().milkDef = MilkDefOf.CentaurMilk;
                DefDatabase<ThingDef>.GetNamed("Cowgirl").GetCompProperties<CompProperties_Milkable>().milkDef = MilkDefOf.CowgirlMilk;
                DefDatabase<ThingDef>.GetNamed("Impmother").GetCompProperties<CompProperties_Milkable>().milkDef = MilkDefOf.ImpmotherMilk;
                DefDatabase<ThingDef>.GetNamed("Dragongirl").GetCompProperties<CompProperties_Milkable>().milkDef = MilkDefOf.DragonMilk;
                DefDatabase<ThingDef>.GetNamed("Dryad").GetCompProperties<CompProperties_Milkable>().milkDef = MilkDefOf.DryadMilk;
                DefDatabase<ThingDef>.GetNamed("Foxgirl").GetCompProperties<CompProperties_Milkable>().milkDef = MilkDefOf.FoxgirlMilk;
            }
            else
            {
                DefDatabase<ThingDef>.GetNamed("Centaur").GetCompProperties<CompProperties_Milkable>().milkDef = MilkDefOf.Milk;
                DefDatabase<ThingDef>.GetNamed("Cowgirl").GetCompProperties<CompProperties_Milkable>().milkDef = MilkDefOf.Milk;
                DefDatabase<ThingDef>.GetNamed("Impmother").GetCompProperties<CompProperties_Milkable>().milkDef = MilkDefOf.Milk;
                DefDatabase<ThingDef>.GetNamed("Dragongirl").GetCompProperties<CompProperties_Milkable>().milkDef = MilkDefOf.Milk;
                DefDatabase<ThingDef>.GetNamed("Dryad").GetCompProperties<CompProperties_Milkable>().milkDef = MilkDefOf.Milk;
                DefDatabase<ThingDef>.GetNamed("Foxgirl").GetCompProperties<CompProperties_Milkable>().milkDef = MilkDefOf.Milk;
            }
        }
        public static void DisableMilk()
        {
            if (!Settings.DisableMilk)
            {
                if (DefDatabase<ThingDef>.GetNamed("CentaurMilk", false) == null)
                {
                    DefDatabase<ThingDef>.Add(MilkDefOf.CentaurMilk);
                    DefDatabase<ThingDef>.Add(MilkDefOf.CowgirlMilk);
                    DefDatabase<ThingDef>.Add(MilkDefOf.DryadMilk);
                    DefDatabase<ThingDef>.Add(MilkDefOf.ThrumbogirlMilk);
                    DefDatabase<ThingDef>.Add(MilkDefOf.ImpmotherMilk);
                    DefDatabase<ThingDef>.Add(MilkDefOf.DragonMilk);
                    DefDatabase<ThingDef>.Add(MilkDefOf.FoxgirlMilk);
                }
            }
            else
            {
                if (DefDatabase<ThingDef>.GetNamed("CentaurMilk", false) != null)
                {
                    if (Current.Game != null)
                        Log.Warning("monster girl milk can only be disabled if the game is not currently running");
                    else
                    {
                        var mi = typeof(DefDatabase<ThingDef>).GetMethod("Remove", BindingFlags.Static | BindingFlags.NonPublic);
                        mi.Invoke(null, new object[] { MilkDefOf.CentaurMilk });
                        mi.Invoke(null, new object[] { MilkDefOf.CowgirlMilk });
                        mi.Invoke(null, new object[] { MilkDefOf.DryadMilk });
                        mi.Invoke(null, new object[] { MilkDefOf.ThrumbogirlMilk });
                        mi.Invoke(null, new object[] { MilkDefOf.ImpmotherMilk });
                        mi.Invoke(null, new object[] { MilkDefOf.DragonMilk });
                        mi.Invoke(null, new object[] { MilkDefOf.FoxgirlMilk });
                    }
                }
            }
        }
    }
}
