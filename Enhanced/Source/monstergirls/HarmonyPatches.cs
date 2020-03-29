using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Verse;

namespace monstergirls
{

    [StaticConstructorOnStartup]
    internal static class Main
    {
        static Main()
        {
            var harmony = new Harmony("com.rimworld.mod.moremonstergirls");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }


    [HarmonyPatch(typeof(Mineable), "TrySpawnYield")]
    static class Patch_Mineable_TrySpawnYield
    {
        private static readonly FieldInfo yieldPctFI = typeof(Mineable).GetField("yieldPct", BindingFlags.NonPublic | BindingFlags.Instance);
        static bool Prefix(Mineable __instance, Map map, float yieldChance, bool moteOnWaste, Pawn pawn)
        {
            if (!Settings.MinedItemsAreDisabled)
            {
                var def = __instance.def;
                if (def.building.mineableThing != null && !(Rand.Value > def.building.mineableDropChance))
                {
                    int num = Mathf.Max(1, def.building.mineableYield);
                    if (def.building.mineableYieldWasteable)
                    {
                        num = Mathf.Max(1, GenMath.RoundRandom((float)num * (float)yieldPctFI.GetValue(__instance)));
                    }
                    Thing thing = ThingMaker.MakeThing(def.building.mineableThing);
                    thing.stackCount = num;
                    GenSpawn.Spawn(thing, __instance.Position, map);
                    if ((pawn == null || !pawn.Faction.IsPlayer) && thing.def.EverHaulable && !thing.def.designateHaulable)
                    {
                        thing.SetForbidden(value: true);
                    }
                }
                return false;
            }

            return true;
        }
    }
}