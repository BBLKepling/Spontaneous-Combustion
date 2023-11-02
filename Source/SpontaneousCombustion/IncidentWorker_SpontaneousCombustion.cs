using RimWorld;
using System.Linq;
using Verse;

namespace Spontaneous_Combustion
{
    public class IncidentWorker_SpontaneousCombustion : IncidentWorker
    {
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            return SpontaneousCombustionUtility.GetCombustables((Map)parms.target).Any();
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            if (!SpontaneousCombustionUtility.GetCombustables((Map)parms.target).TryRandomElement(out var result))
            {
                return false;
            }
            return SpontaneousCombustionUtility.TryCombust(result);
        }
    }
}
