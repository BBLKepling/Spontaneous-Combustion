using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Spontaneous_Combustion
{
    public static class SpontaneousCombustionUtility
    {
        public static IEnumerable<Thing> GetCombustables(Map map)
        {
            if (!SpontaneousCombustionSettings.combustFire && !SpontaneousCombustionSettings.combustExplode)
            {
                yield break;
            }
            if (SpontaneousCombustionSettings.includePlants)
            {
                List<Thing> combustables = map.listerThings.AllThings;
                for (int i = 0; i < combustables.Count; i++)
                {
                    if (!combustables[i].Destroyed && combustables[i].FlammableNow)
                    {
                        yield return combustables[i];
                    }
                }
            }
            else
            {
                List<Thing> combustables = map.listerThings.AllThings;
                for (int i = 0; i < combustables.Count; i++)
                {
                    if (FireUtility.CanEverAttachFire(combustables[i]))
                    {
                        yield return combustables[i];
                    }
                }
            }
        }
        public static bool TryCombust(Thing culprit)
        {
            if (SpontaneousCombustionSettings.combustFire && SpontaneousCombustionSettings.combustExplode)
            {
                GenExplosion.DoExplosion(
                    radius: SpontaneousCombustionSettings.explosionRadius,
                    center: culprit.Position,
                    map: culprit.Map,
                    damType: DamageDefOf.Flame,
                    instigator: culprit);
                if (SpontaneousCombustionSettings.letterSend)
                {
                    Find.LetterStack.ReceiveLetter(
                        "BBLK_SpontaneousCombustion_LetterLabel".Translate(),
                        "BBLK_SpontaneousCombustion_LetterText".Translate(culprit.Label,
                        culprit.Named("CULPRIT")),
                        LetterDefOf.NegativeEvent,
                        new TargetInfo(culprit.Position, culprit.Map));
                }
                return true;
            }
            if (SpontaneousCombustionSettings.combustFire)
            {
                if (culprit.CanEverAttachFire())
                {
                    culprit.TryAttachFire(Rand.Range(0.5f, 1f), culprit);
                }
                else
                {
                    FireUtility.TryStartFireIn(culprit.Position, culprit.Map, Rand.Range(0.5f, 1f), culprit);
                }
                if (SpontaneousCombustionSettings.letterSend)
                {
                    Find.LetterStack.ReceiveLetter(
                        "BBLK_SpontaneousCombustion_LetterLabel".Translate(),
                        "BBLK_SpontaneousCombustion_LetterText".Translate(culprit.Label,
                        culprit.Named("CULPRIT")),
                        LetterDefOf.NegativeEvent,
                        new TargetInfo(culprit.Position, culprit.Map));
                }
                return true;
            }
            if (SpontaneousCombustionSettings.combustExplode)
            {
                GenExplosion.DoExplosion(
                    radius: SpontaneousCombustionSettings.explosionRadius,
                    center: culprit.Position,
                    map: culprit.Map,
                    damType: DamageDefOf.Bomb,
                    instigator: culprit);
                if (SpontaneousCombustionSettings.letterSend)
                {
                    Find.LetterStack.ReceiveLetter(
                        "BBLK_SpontaneousCombustion_LetterLabel".Translate(),
                        "BBLK_SpontaneousCombustion_LetterText".Translate(culprit.Label,
                        culprit.Named("CULPRIT")),
                        LetterDefOf.NegativeEvent,
                        new TargetInfo(culprit.Position, culprit.Map));
                }
                return true;
            }
            return false;
        }
    }
}
