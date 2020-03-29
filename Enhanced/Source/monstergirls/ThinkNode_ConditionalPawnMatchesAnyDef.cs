using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using Verse.AI;

namespace monstergirls
{
    public class ThinkNode_ConditionalPawnMatchesAnyDef : ThinkNode_Conditional
    {
        public List<ThingDef> defs;

        public override ThinkNode DeepCopy(bool resolve = true)
        {
            ThinkNode_ConditionalPawnMatchesAnyDef thinkNode_ConditionalPawnMatchesAnyDef = (ThinkNode_ConditionalPawnMatchesAnyDef)base.DeepCopy(resolve);
            thinkNode_ConditionalPawnMatchesAnyDef.defs = new List<ThingDef>(defs);
            return thinkNode_ConditionalPawnMatchesAnyDef;
        }

        protected override bool Satisfied(Pawn pawn)
        {
            //if (defs != null && pawn != null && pawn.def != null
            if (defs != null)
                return defs.Contains(pawn.def);

            return false;
        }
    }
}
