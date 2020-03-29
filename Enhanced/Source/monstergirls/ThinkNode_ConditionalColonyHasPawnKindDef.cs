using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using Verse.AI;

namespace monstergirls
{
    public class ThinkNode_ConditionalColonyHasPawnKindDef : ThinkNode_Conditional
    {
        public List<PawnKindDef> pawnKindDefs;

        public override ThinkNode DeepCopy(bool resolve = true)
        {
            ThinkNode_ConditionalColonyHasPawnKindDef thinkNode_ConditionalPawnMatchesAnyDef = (ThinkNode_ConditionalColonyHasPawnKindDef)base.DeepCopy(resolve);
            thinkNode_ConditionalPawnMatchesAnyDef.pawnKindDefs = new List<PawnKindDef>(pawnKindDefs);
            return thinkNode_ConditionalPawnMatchesAnyDef;
        }

        protected override bool Satisfied(Pawn pawn)
        {
            if (pawnKindDefs != null && pawn != null && pawn.Map != null && pawn.Map.mapPawns != null)
            {
                if (pawn.Map.mapPawns.PawnsInFaction(pawn.Faction).Where(p => pawnKindDefs.Contains(p.kindDef)).Count() > 0)
                    return true;
            }

            return false;
        }
    }
}
