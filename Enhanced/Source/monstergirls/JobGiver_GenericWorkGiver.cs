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