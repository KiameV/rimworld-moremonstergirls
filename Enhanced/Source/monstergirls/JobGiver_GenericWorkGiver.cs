using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using Verse.AI;


namespace monstergirls
{
    public class JobGiver_GenericWorkGiver : ThinkNode_JobGiver
    {
        public bool emergency;
        public WorkGiverDef workGiverDef;

        public override ThinkNode DeepCopy(bool resolve = true)
        {
            JobGiver_GenericWorkGiver jobGiver_GenericWorkGiver = (JobGiver_GenericWorkGiver)base.DeepCopy(resolve);
            jobGiver_GenericWorkGiver.emergency = this.emergency;
            jobGiver_GenericWorkGiver.workGiverDef = this.workGiverDef;
            return jobGiver_GenericWorkGiver;
        }

        public override float GetPriority(Pawn pawn)
        {
            return 9f;
        }

        protected override Job TryGiveJob(Pawn pawn)
        {
            if (workGiverDef != null)
            {
                int num = -999;
                TargetInfo targetInfo = TargetInfo.Invalid;
                WorkGiver_Scanner workGiver_Scanner = null;

                WorkGiver workGiver = workGiverDef.Worker;
                if (workGiver != null)
                {
                    if (workGiver.def.priorityInType != num && targetInfo.IsValid)
                    {
                        return null;
                    }
                    if (this.PawnCanUseWorkGiver(pawn, workGiver))
                    {
                        try
                        {
                            Job job2 = workGiver.NonScanJob(pawn);
                            if (job2 != null)
                            {
                                return job2;
                            }
                            WorkGiver_Scanner scanner = workGiver as WorkGiver_Scanner;
                            if (scanner != null)
                            {
                                if (workGiver.def.scanThings)
                                {
                                    Predicate<Thing> predicate = (Thing t) => !t.IsForbidden(pawn) && scanner.HasJobOnThing(pawn, t);
                                    IEnumerable<Thing> enumerable = scanner.PotentialWorkThingsGlobal(pawn);
                                    Thing thing;
                                    if (scanner.Prioritized)
                                    {
                                        IEnumerable<Thing> enumerable2 = enumerable;
                                        if (enumerable2 == null)
                                        {
                                            enumerable2 = pawn.Map.listerThings.ThingsMatching(scanner.PotentialWorkThingRequest);
                                        }
                                        Predicate<Thing> validator = predicate;
                                        thing = GenClosest.ClosestThing_Global_Reachable(pawn.Position, pawn.Map, enumerable2, scanner.PathEndMode, TraverseParms.For(pawn, Danger.Deadly, TraverseMode.ByPawn, false), 9999f, validator, (Thing x) => scanner.GetPriority(pawn, x));
                                    }
                                    else
                                    {
                                        Predicate<Thing> validator = predicate;
                                        thing = GenClosest.ClosestThingReachable(pawn.Position, pawn.Map, scanner.PotentialWorkThingRequest, scanner.PathEndMode, TraverseParms.For(pawn, Danger.Deadly, TraverseMode.ByPawn, false), 9999f, validator, enumerable, scanner.MaxRegionsToScanBeforeGlobalSearch);
                                    }
                                    if (thing != null)
                                    {
                                        targetInfo = thing;
                                        workGiver_Scanner = scanner;
                                    }
                                }
                                if (workGiver.def.scanCells)
                                {
                                    IntVec3 position = pawn.Position;
                                    float num2 = 99999f;
                                    float num3 = -3.40282347E+38f;
                                    bool prioritized = scanner.Prioritized;
                                    foreach (IntVec3 current in scanner.PotentialWorkCellsGlobal(pawn))
                                    {
                                        bool flag = false;
                                        float lengthHorizontalSquared = (current - position).LengthHorizontalSquared;
                                        if (prioritized)
                                        {
                                            if (!current.IsForbidden(pawn) && scanner.HasJobOnCell(pawn, current))
                                            {
                                                float priority = scanner.GetPriority(pawn, current);
                                                if (priority > num3 || (priority == num3 && lengthHorizontalSquared < num2))
                                                {
                                                    flag = true;
                                                    num3 = priority;
                                                }
                                            }
                                        }
                                        else if (lengthHorizontalSquared < num2 && !current.IsForbidden(pawn) && scanner.HasJobOnCell(pawn, current))
                                        {
                                            flag = true;
                                        }
                                        if (flag)
                                        {
                                            targetInfo = new TargetInfo(current, pawn.Map, false);
                                            workGiver_Scanner = scanner;
                                            num2 = lengthHorizontalSquared;
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Error($"{pawn} threw exception in WorkGiver {workGiver.def.defName}: {ex.ToString()}");
                        }
                        finally
                        {
                        }
                        if (targetInfo.IsValid)
                        {
                            //pawn.mindState. = workGiver.def.workType;
                            Job job3;
                            if (targetInfo.HasThing)
                            {
                                job3 = workGiver_Scanner.JobOnThing(pawn, targetInfo.Thing);
                            }
                            else
                            {
                                job3 = workGiver_Scanner.JobOnCell(pawn, targetInfo.Cell);
                            }
                            if (job3 != null)
                            {
                                return job3;
                            }
                            Log.ErrorOnce($"{workGiver_Scanner} provided target {targetInfo} but yielded no actual job for pawn {pawn}. The CanGiveJob and JobOnX methods may not be synchronized.", 6112651);
                        }
                        num = workGiver.def.priorityInType;
                    }
                }
            }

            return null;
        }

        private bool PawnCanUseWorkGiver(Pawn pawn, WorkGiver giver)
        {
            return giver.MissingRequiredCapacity(pawn) == null && !giver.ShouldSkip(pawn);
        }
    }
}
/*
            if (workGiverDef != null)
            {
                int num = -999;
                TargetInfo val2 = TargetInfo.Invalid;
                WorkGiver_Scanner val3 = null;
                if (workGiverDef != null)
                {
                    WorkGiver worker2 = workGiverDef.Worker;
                    if (worker2.def.priorityInType != num && val2.IsValid)
                    {
                        return null;
                    }
                    if (PawnCanUseWorkGiver(pawn, worker2))
                    {
                        try
                        {
                            Job val4 = worker2.NonScanJob(pawn);
                            if (val4 != null)
                            {
                                return val4;
                            }
                            WorkGiver_Scanner scanner = worker2 as WorkGiver_Scanner;
                            if (scanner != null)
                            {
                                if (worker2.def.scanThings)
                                {
                                    Predicate<Thing> predicate = (Thing t) => !ForbidUtility.IsForbidden(t, pawn) && scanner.HasJobOnThing(pawn, t, false);
                                    IEnumerable<Thing> enumerable = scanner.PotentialWorkThingsGlobal(pawn);
                                    Thing val5;
                                    if (scanner.Prioritized)
                                    {
                                        IEnumerable<Thing> enumerable2 = enumerable;
                                        if (enumerable2 == null)
                                        {
                                            enumerable2 = ((Thing)pawn).Map.listerThings.ThingsMatching(scanner.PotentialWorkThingRequest);
                                        }
                                        Predicate<Thing> predicate2 = predicate;
                                        val5 = GenClosest.ClosestThing_Global_Reachable(((Thing)pawn).Position, ((Thing)pawn).Map, enumerable2, scanner.PathEndMode, TraverseParms.For(pawn, (Danger)3, (TraverseMode)0, false), 9999f, predicate2, (Func<Thing, float>)((Thing x) => scanner.GetPriority(pawn, x)));
                                    }
                                    else
                                    {
                                        Predicate<Thing> predicate3 = predicate;
                                        bool flag = enumerable != null;
                                        val5 = GenClosest.ClosestThingReachable(((Thing)pawn).Position, ((Thing)pawn).Map, scanner.PotentialWorkThingRequest, scanner.PathEndMode, TraverseParms.For(pawn, (Danger)3, (TraverseMode)0, false), 9999f, predicate3, enumerable, 0, 0, flag, (RegionType)6, false);
                                    }
                                    if (val5 != null)
                                    {
                                        val2 = val5;
                                        val3 = scanner;
                                    }
                                }
                                if (worker2.def.scanCells)
                                {
                                    IntVec3 position = ((Thing)pawn).Position;
                                    float num2 = 99999f;
                                    float num3 = float.MinValue;
                                    bool prioritized = scanner.Prioritized;
                                    foreach (IntVec3 item in scanner.PotentialWorkCellsGlobal(pawn))
                                    {
                                        bool flag2 = false;
                                        IntVec3 val6 = item - position;
                                        float num4 = val6.LengthHorizontalSquared;
                                        if (prioritized)
                                        {
                                            if (!ForbidUtility.IsForbidden(item, pawn) && scanner.HasJobOnCell(pawn, item, false))
                                            {
                                                float priority = scanner.GetPriority(pawn, item);
                                                if (priority > num3 || (priority == num3 && num4 < num2))
                                                {
                                                    flag2 = true;
                                                    num3 = priority;
                                                }
                                            }
                                        }
                                        else if (num4 < num2 && !ForbidUtility.IsForbidden(item, pawn) && scanner.HasJobOnCell(pawn, item, false))
                                        {
                                            flag2 = true;
                                        }
                                        if (flag2)
                                        {
                                            val2 = new TargetInfo(item, pawn.Map, false);
                                            val3 = scanner;
                                            num2 = num4;
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Message(pawn + " threw exception DONT WANT LAH in WorkGiver " + ((Def)worker2.def).defName + ": " + ex.ToString(), false);
                        }
                        finally
                        {
                        }
                        if (val2.IsValid)
                        {
                            //pawn.mindState.lastGivenWorkType = worker2.def.workType;
                            Job val7 = (!val2.HasThing) ? val3.JobOnCell(pawn, val2.Cell, false) : val3.JobOnThing(pawn, val2.Thing, false);
                            if (val7 != null)
                            {
                                return val7;
                            }
                            Log.ErrorOnce(val3 + " provided target " + val2 + " but yielded no actual job for pawn " + pawn + ". The CanGiveJob and JobOnX methods may not be synchronized.", 6112651, false);
                        }
                        num = worker2.def.priorityInType;
                    }
                }
            }
            return null;
 */
