using RimWorld;
using UnityEngine;
using Verse;

namespace Spontaneous_Combustion
{
    public class SpontaneousCombustionSettings : ModSettings
    {
        public static bool letterSend = true;
        public static bool includePlants = true;
        public static bool combustFire = true;
        public static bool combustExplode = false;
        public static float explosionRadius = 3.9f;
        public override void ExposeData()
        {
            Scribe_Values.Look(ref letterSend, "letterSend");
            Scribe_Values.Look(ref includePlants, "includePlants");
            Scribe_Values.Look(ref combustFire, "combustFire");
            Scribe_Values.Look(ref combustExplode, "combustExplode");
            Scribe_Values.Look(ref explosionRadius, "explosionRadius", 3.9f);
            base.ExposeData();
        }
    }
    public class SpontaneousCombustionMod : Mod
    {
        public SpontaneousCombustionMod(ModContentPack content) : base(content)
        {
            GetSettings<SpontaneousCombustionSettings>();
        }
        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            listingStandard.CheckboxLabeled("BBLK_SpontaneousCombustionLabelLetterSend".Translate(), ref SpontaneousCombustionSettings.letterSend, "BBLK_SpontaneousCombustionLetterToolTipSend".Translate());
            listingStandard.CheckboxLabeled("BBLK_SpontaneousCombustionLabelIncludePlants".Translate(), ref SpontaneousCombustionSettings.includePlants, "BBLK_SpontaneousCombustionIncludeToolTipPlants".Translate());
            listingStandard.Label("BBLK_SpontaneousCombustion_Info".Translate());
            listingStandard.CheckboxLabeled("BBLK_SpontaneousCombustionLabelCombustFire".Translate(), ref SpontaneousCombustionSettings.combustFire, "BBLK_SpontaneousCombustionCombustToolTipFire".Translate());
            listingStandard.CheckboxLabeled("BBLK_SpontaneousCombustionLabelCombustExplode".Translate(), ref SpontaneousCombustionSettings.combustExplode, "BBLK_SpontaneousCombustionCombustToolTipExplode".Translate());
            SpontaneousCombustionSettings.explosionRadius = Mathf.RoundToInt(listingStandard.SliderLabeled("BBLK_SpontaneousCombustionLabelExplosionRadius".Translate() + " " + SpontaneousCombustionSettings.explosionRadius, 10f * SpontaneousCombustionSettings.explosionRadius, 5f, 100f, tooltip: "BBLK_SpontaneousCombustionToolTipExplosionRadius".Translate())) / 10f;
            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }
        public override string SettingsCategory() => "BBLK_SpontaneousCombustion_Settings".Translate();
    }
}
