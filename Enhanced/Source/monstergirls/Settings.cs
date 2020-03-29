using UnityEngine;
using Verse;

namespace monstergirls
{
    public class SettingsController : Mod
    {
        public SettingsController(ModContentPack content) : base(content)
        {
            base.GetSettings<Settings>();
        }

        public override string SettingsCategory()
        {
            return "MoreMonsterGirlsEnhanced".Translate();
        }

        public override void DoSettingsWindowContents(Rect rect)
        {
            float y = 60f;
            Widgets.CheckboxLabeled(new Rect(0, y, 250, 22), "monstergirls.MinedItemsAreDisabled".Translate(), ref Settings.MinedItemsAreDisabled);
        }
    }

    class Settings : ModSettings
    {
        public static bool MinedItemsAreDisabled = false;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look<bool>(ref MinedItemsAreDisabled, "MinedItemsAreDisabled", true, false);

        }
    }
}