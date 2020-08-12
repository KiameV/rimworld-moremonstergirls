using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace monstergirlsbase
{
    public class SettingsController : Mod
    {
        private const float NEWLINE = 30;
        private const float NEW_SETTING = NEWLINE + 20;

        private bool initialized = false;
        private Settings settings;
        private Vector2 scroll = Vector2.zero;
        private float y = 0;

        public SettingsController(ModContentPack content) : base(content)
        {
            settings = base.GetSettings<Settings>();
        }

        public override string SettingsCategory()
        {
            return "MoreMonsterGirls".Translate();
        }

        public override void DoSettingsWindowContents(Rect rect)
        {
            Widgets.BeginScrollView(new Rect(0, 40, rect.width, rect.height - 50), ref scroll, new Rect(0, 0, rect.width - 16, y));
            y = 0;

            if (!this.initialized)
            {
                settings.SetUnsetProductions();
                this.initialized = true;
            }

            Widgets.Label(new Rect(0, y, 150, 22), "MMG.Centaur".Translate());
            y += NEWLINE;
            Widgets.Label(new Rect(20, y, 150, 22), "MMG.Milk".Translate());
            if (Widgets.ButtonText(new Rect(200, y, 100, 22), "Reset".Translate()))
            {
                settings.DefaultCentaurMilk();
                SetCompProps(GetMilkCompProps("Centaur"), settings.CentaurMilk);
            }
            y += NEWLINE;
            if (DrawProductionInput(ref y, settings.CentaurMilk))
            {
                SetCompProps(GetMilkCompProps("Centaur"), settings.CentaurMilk);
            }
            y += NEWLINE;
            Widgets.Label(new Rect(20, y, 150, 22), "MMG.Hair".Translate());
            if (Widgets.ButtonText(new Rect(200, y, 100, 22), "Reset".Translate()))
            {
                settings.DefaultCentaurHair();
                SetCompProps(GetShearableCompProps("Centaur"), settings.CentaurHair);
            }
            y += NEWLINE;
            if (DrawProductionInput(ref y, settings.CentaurHair))
            {
                SetCompProps(GetShearableCompProps("Centaur"), settings.CentaurHair);
            }
            y += NEW_SETTING;

            Widgets.Label(new Rect(0, y, 150, 22), "MMG.Cowgirl".Translate());
            y += NEWLINE;
            Widgets.Label(new Rect(20, y, 150, 22), "MMG.Milk".Translate());
            if (Widgets.ButtonText(new Rect(200, y, 100, 22), "Reset".Translate()))
            {
                settings.DefaultCowgirl();
                SetCompProps(GetMilkCompProps("Cowgirl"), settings.CowgirlMilk);
            }
            y += NEWLINE;
            if (DrawProductionInput(ref y, settings.CowgirlMilk))
            {
                SetCompProps(GetMilkCompProps("Cowgirl"), settings.CowgirlMilk);
            }
            y += NEW_SETTING;

            Widgets.Label(new Rect(0, y, 150, 22), "MMG.Dragongirl".Translate());
            y += NEWLINE;
            Widgets.Label(new Rect(20, y, 150, 22), "MMG.Milk".Translate());
            if (Widgets.ButtonText(new Rect(200, y, 100, 22), "Reset".Translate()))
            {
                settings.DefaultDragongirlMilk();
                SetCompProps(GetMilkCompProps("Dragongirl"), settings.DragongirlMilk);
            }
            y += NEWLINE;
            if (DrawProductionInput(ref y, settings.DragongirlMilk))
            {
                SetCompProps(GetMilkCompProps("Dragongirl"), settings.DragongirlMilk);
            }
            y += NEWLINE;
            Widgets.Label(new Rect(20, y, 150, 22), "MMG.Scales".Translate());
            if (Widgets.ButtonText(new Rect(200, y, 100, 22), "Reset".Translate()))
            {
                settings.DefaultDragongirlScales();
                SetCompProps(GetShearableCompProps("Dragongirl"), settings.DragongirlScales);
            }
            y += NEWLINE;
            if (DrawProductionInput(ref y, settings.DragongirlScales))
            {
                SetCompProps(GetShearableCompProps("Dragongirl"), settings.DragongirlScales);
            }
            y += NEW_SETTING;

            Widgets.Label(new Rect(0, y, 150, 22), "MMG.Dryad".Translate());
            y += NEWLINE;
            Widgets.Label(new Rect(20, y, 150, 22), "MMG.Milk".Translate());
            if (Widgets.ButtonText(new Rect(200, y, 100, 22), "Reset".Translate()))
            {
                settings.DefaultDryadMilk();
                SetCompProps(GetMilkCompProps("Dryad"), settings.DryadMilk);
            }
            y += NEWLINE;
            if (DrawProductionInput(ref y, settings.DryadMilk))
            {
                SetCompProps(GetMilkCompProps("Dryad"), settings.DryadWool);
            }
            y += NEWLINE;
            Widgets.Label(new Rect(20, y, 150, 22), "MMG.Wool".Translate());
            if (Widgets.ButtonText(new Rect(200, y, 100, 22), "Reset".Translate()))
            {
                settings.DefaultDryadWool();
                SetCompProps(GetShearableCompProps("Dryad"), settings.DryadWool);
            }
            y += NEWLINE;
            if (DrawProductionInput(ref y, settings.DryadWool))
            {
                SetCompProps(GetShearableCompProps("Dryad"), settings.DryadWool);
            }
            y += NEW_SETTING;

            Widgets.Label(new Rect(0, y, 150, 22), "MMG.FairyForest".Translate());
            y += NEWLINE;
            Widgets.Label(new Rect(20, y, 150, 22), "MMG.FairyDust".Translate());
            if (Widgets.ButtonText(new Rect(200, y, 100, 22), "Reset".Translate()))
            {
                settings.DefaultFairyForest();
                SetCompProps(GetMilkCompProps("FairyForest"), settings.ForestFairyDust);
            }
            y += NEWLINE;
            if (DrawProductionInput(ref y, settings.ForestFairyDust))
            {
                SetCompProps(GetMilkCompProps("FairyForest"), settings.ForestFairyDust);
            }
            y += NEW_SETTING;

            Widgets.Label(new Rect(0, y, 150, 22), "MMG.FairyIce".Translate());
            y += NEWLINE;
            Widgets.Label(new Rect(20, y, 150, 22), "MMG.FairyDust".Translate());
            if (Widgets.ButtonText(new Rect(200, y, 100, 22), "Reset".Translate()))
            {
                settings.DefaultFairyIce();
                SetCompProps(GetMilkCompProps("FairyIce"), settings.IceFairyDust);
            }
            y += NEWLINE;
            if (DrawProductionInput(ref y, settings.IceFairyDust))
            {
                SetCompProps(GetMilkCompProps("FairyIce"), settings.IceFairyDust);
            }
            y += NEW_SETTING;

            Widgets.Label(new Rect(0, y, 150, 22), "MMG.Foxgirl".Translate());
            y += NEWLINE;
            Widgets.Label(new Rect(20, y, 150, 22), "MMG.Milk".Translate());
            if (Widgets.ButtonText(new Rect(200, y, 100, 22), "Reset".Translate()))
            {
                settings.DefaultFoxgirl();
                SetCompProps(GetMilkCompProps("Foxgirl"), settings.FoxgirlMilk);
            }
            y += NEWLINE;
            if (DrawProductionInput(ref y, settings.FoxgirlMilk))
            {
                SetCompProps(GetMilkCompProps("Foxgirl"), settings.FoxgirlMilk);
            }
            y += NEW_SETTING;

            Widgets.Label(new Rect(0, y, 150, 22), "MMG.Harpy".Translate());
            y += NEWLINE;
            Widgets.Label(new Rect(20, y, 150, 22), "MMG.Eggs".Translate());
            if (Widgets.ButtonText(new Rect(200, y, 100, 22), "Reset".Translate()))
            {
                settings.DefaultHarpyEggs();
                SetCompProps(GetEggLayerCompProps("Harpy"), settings.HarpyEggs);
            }
            y += NEWLINE;
            if (DrawProductionInput(ref y, settings.HarpyEggs))
            {
                SetCompProps(GetEggLayerCompProps("Harpy"), settings.HarpyEggs);
            }
            y += NEWLINE;
            Widgets.Label(new Rect(20, y, 150, 22), "MMG.Feathers".Translate());
            if (Widgets.ButtonText(new Rect(200, y, 100, 22), "Reset".Translate()))
            {
                settings.DefaultHarpyFeathers();
                SetCompProps(GetShearableCompProps("Harpy"), settings.HarpyFeathers);
            }
            y += NEWLINE;
            if (DrawProductionInput(ref y, settings.HarpyFeathers))
            {
                SetCompProps(GetShearableCompProps("Harpy"), settings.HarpyFeathers);
            }
            y += NEW_SETTING;

            Widgets.Label(new Rect(0, y, 150, 22), "MMG.BlackHarpy".Translate());
            y += NEWLINE;
            Widgets.Label(new Rect(20, y, 150, 22), "MMG.Eggs".Translate());
            if (Widgets.ButtonText(new Rect(200, y, 100, 22), "Reset".Translate()))
            {
                settings.DefaultBlackHarpyEggs();
                SetCompProps(GetEggLayerCompProps("BlackHarpy"), settings.BlackHarpyEggs);
            }
            y += NEWLINE;
            if (DrawProductionInput(ref y, settings.BlackHarpyEggs))
            {
                SetCompProps(GetEggLayerCompProps("BlackHarpy"), settings.BlackHarpyEggs);
            }
            y += NEWLINE;
            Widgets.Label(new Rect(20, y, 150, 22), "MMG.Feathers".Translate());
            if (Widgets.ButtonText(new Rect(200, y, 100, 22), "Reset".Translate()))
            {
                settings.DefaultBlackHarpyFeathers();
                SetCompProps(GetShearableCompProps("BlackHarpy"), settings.BlackHarpyFeathers);
            }
            y += NEWLINE;
            if (DrawProductionInput(ref y, settings.BlackHarpyFeathers))
            {
                SetCompProps(GetShearableCompProps("BlackHarpy"), settings.BlackHarpyFeathers);
            }
            y += NEW_SETTING;

            Widgets.Label(new Rect(0, y, 150, 22), "MMG.Impmother".Translate());
            y += NEWLINE;
            Widgets.Label(new Rect(20, y, 150, 22), "MMG.Milk".Translate());
            if (Widgets.ButtonText(new Rect(200, y, 100, 22), "Reset".Translate()))
            {
                settings.DefaultImpMother();
                SetCompProps(GetMilkCompProps("Impmother"), settings.ImpMotherMilk);
            }
            y += NEWLINE;
            if (DrawProductionInput(ref y, settings.ImpMotherMilk))
            {
                SetCompProps(GetMilkCompProps("Impmother"), settings.ImpMotherMilk);
            }
            y += NEW_SETTING;

            Widgets.Label(new Rect(0, y, 150, 22), "MMG.Slime".Translate());
            y += NEWLINE;
            Widgets.Label(new Rect(20, y, 150, 22), "MMG.RedGoo".Translate());
            if (Widgets.ButtonText(new Rect(200, y, 100, 22), "Reset".Translate()))
            {
                settings.DefaultSlimegirlSlime();
                SetCompProps(GetMilkCompProps("Slime"), settings.SlimegirlSlime);
            }
            y += NEWLINE;
            if (DrawProductionInput(ref y, settings.SlimegirlSlime))
            {
                SetCompProps(GetMilkCompProps("Slime"), settings.SlimegirlSlime);
            }
            y += NEW_SETTING;

            Log.Warning("thrumb");
            Widgets.Label(new Rect(0, y, 150, 22), "MMG.Thrumbomorph".Translate());
            y += NEWLINE;
            Widgets.Label(new Rect(20, y, 150, 22), "MMG.Milk".Translate());
            if (Widgets.ButtonText(new Rect(200, y, 100, 22), "Reset".Translate()))
            {
                settings.DefaultThrumbomorphMilk();
                SetCompProps(GetMilkCompProps("Thrumbomorph"), settings.ThumbromorphMilk);
            }
            y += NEWLINE;
            if (DrawProductionInput(ref y, settings.ThumbromorphMilk))
            {
                SetCompProps(GetMilkCompProps("Thrumbomorph"), settings.ThumbromorphMilk);
            }
            y += NEWLINE;
            Widgets.Label(new Rect(20, y, 150, 22), "MMG.Wool".Translate());
            if (Widgets.ButtonText(new Rect(200, y, 100, 22), "Reset".Translate()))
            {
                settings.DefaultThrumbomorphWool();
                SetCompProps(GetShearableCompProps("Thrumbomorph"), settings.ThumbromorphWool);
            }
            y += NEWLINE;
            if (DrawProductionInput(ref y, settings.ThumbromorphWool))
            {
                SetCompProps(GetShearableCompProps("Thrumbomorph"), settings.ThumbromorphWool);
            }

            Widgets.EndScrollView();
        }

        private CompProperties_Milkable GetMilkCompProps(string defName)
        {
            var c = DefDatabase<ThingDef>.GetNamed(defName).GetCompProperties<CompProperties_Milkable>();
            if (c == null)
            {
                Log.ErrorOnce("unable to find monster girl " + defName, defName.GetHashCode());
                return null;
            }
            return c;
        }

        private CompProperties_Shearable GetShearableCompProps(string defName)
        {
            var c = DefDatabase<ThingDef>.GetNamed(defName).GetCompProperties<CompProperties_Shearable>();
            if (c == null)
            {
                Log.ErrorOnce("unable to find monster girl " + defName, defName.GetHashCode());
                return null;
            }
            return c;
        }

        private CompProperties_EggLayer GetEggLayerCompProps(string defName)
        {
            var c = DefDatabase<ThingDef>.GetNamed(defName).GetCompProperties<CompProperties_EggLayer>();
            if (c == null)
            {
                Log.ErrorOnce("unable to find monster girl " + defName, defName.GetHashCode()); 
                return null;
            }
            return c;
        }

        private void SetCompProps(CompProperties_Milkable compProps, Production p)
        {
            if (compProps != null)
            {
                compProps.milkAmount = p.Amount;
                compProps.milkIntervalDays = p.IntervalDays;
            }
        }

        private void SetCompProps(CompProperties_Shearable compProps, Production p)
        {
            if (compProps != null)
            {
                compProps.woolAmount = p.Amount;
                compProps.shearIntervalDays = p.IntervalDays;
            }
        }

        private void SetCompProps(CompProperties_EggLayer compProps, EggProduction p)
        {
            if (compProps != null)
            {
                compProps.eggCountRange.min = p.CountMin;
                compProps.eggCountRange.max = p.CountMax;
                compProps.eggFertilizationCountMax = p.FertalizedCountMax;
                compProps.eggLayIntervalDays = p.IntervalDays;
            }
        }

        private bool DrawProductionInput(ref float y, Production p)
        {
            Widgets.Label(new Rect(40, y, 150, 22), "MMG.IntervalInDays".Translate());
            string value = Widgets.TextField(new Rect(200, y, 100, 22), p.IntervalDays.ToString());
            if (int.TryParse(value, out int i) && i != p.IntervalDays && i >= 0)
            {
                p.IntervalDays = i;
                return true;
            }
            y += NEWLINE;
            Widgets.Label(new Rect(40, y, 150, 22), "MMG.Amount".Translate());
            value = Widgets.TextField(new Rect(200, y, 100, 22), p.Amount.ToString());
            if (int.TryParse(value, out i) && i != p.Amount && i >= 0)
            {
                p.Amount = i;
                return true;
            }
            y += NEWLINE;
            return false;
        }

        private bool DrawProductionInput(ref float y, EggProduction p)
        {
            Widgets.Label(new Rect(40, y, 150, 22), "MMG.IntervalInDays".Translate());
            string value = Widgets.TextField(new Rect(200, y, 100, 22), p.IntervalDays.ToString());
            if (int.TryParse(value, out int i) && i != p.IntervalDays && i >= 0)
            {
                p.IntervalDays = i;
                return true;
            }
            y += NEWLINE;
            Widgets.Label(new Rect(40, y, 150, 22), "MMG.CountRangeMinMax".Translate());
            value = Widgets.TextField(new Rect(200, y, 100, 22), p.CountMin.ToString());
            if (int.TryParse(value, out i) && i != p.CountMin && i >= 0)
            {
                p.CountMin = i;
                return true;
            }
            value = Widgets.TextField(new Rect(310, y, 100, 22), p.CountMax.ToString());
            if (int.TryParse(value, out i) && i != p.CountMax && i >= 0)
            {
                p.CountMax = i;
                return true;
            }
            y += NEWLINE;
            Widgets.Label(new Rect(40, y, 150, 22), "MMG.FertalizedCountMax".Translate());
            value = Widgets.TextField(new Rect(200, y, 100, 22), p.FertalizedCountMax.ToString());
            if (int.TryParse(value, out i) && i != p.FertalizedCountMax && i >= 0)
            {
                p.FertalizedCountMax = i;
                return true;
            }
            y += NEWLINE;
            return false;
        }
    }

    class Settings : ModSettings
    {
        public Production CentaurMilk;
        public Production CentaurHair;

        public Production CowgirlMilk;

        public Production DragongirlMilk;
        public Production DragongirlScales;

        public Production DryadMilk;
        public Production DryadWool;

        public Production ForestFairyDust;
        public Production IceFairyDust;

        public Production FoxgirlMilk;

        public EggProduction HarpyEggs;
        public Production HarpyFeathers;
        public EggProduction BlackHarpyEggs;
        public Production BlackHarpyFeathers;

        public Production ImpMotherMilk;

        public Production SlimegirlSlime;

        public Production ThumbromorphMilk;
        public Production ThumbromorphWool;


        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Deep.Look(ref this.CentaurMilk, "CentaurMilk", null);
            Scribe_Deep.Look(ref this.CentaurHair, "CentaurHair", null);
            Scribe_Deep.Look(ref this.CowgirlMilk, "CowgirlMilk", null);
            Scribe_Deep.Look(ref this.DragongirlMilk, "DragongirlMilk", null);
            Scribe_Deep.Look(ref this.DragongirlScales, "DragongirlScales", null);
            Scribe_Deep.Look(ref this.DryadMilk, "DryadMilk", null);
            Scribe_Deep.Look(ref this.DryadWool, "DryadWool", null);
            Scribe_Deep.Look(ref this.ForestFairyDust, "ForestFairyDust", null);
            Scribe_Deep.Look(ref this.IceFairyDust, "IceFairyDust", null);
            Scribe_Deep.Look(ref this.FoxgirlMilk, "FoxgirlMilk", null);
            Scribe_Deep.Look(ref this.HarpyEggs, "HarpyEggs", null);
            Scribe_Deep.Look(ref this.HarpyFeathers, "HarpyFeathers", null);
            Scribe_Deep.Look(ref this.BlackHarpyEggs, "BlackHarpyEggs", null);
            Scribe_Deep.Look(ref this.BlackHarpyFeathers, "BlackHarpyFeathers", null);
            Scribe_Deep.Look(ref this.ImpMotherMilk, "ImpMotherMilk", null);
            Scribe_Deep.Look(ref this.SlimegirlSlime, "SlimegirlSlime", null);
            Scribe_Deep.Look(ref this.ThumbromorphMilk, "ThumbromorphMilk", null);
            Scribe_Deep.Look(ref this.ThumbromorphWool, "ThumbromorphWool", null);

            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                this.SetUnsetProductions();
            }
        }

        public void SetUnsetProductions()
        {
            if (this.CentaurMilk == null)
                this.DefaultCentaurMilk();
            if (this.CentaurHair == null)
                this.DefaultCentaurHair();

            if (this.CowgirlMilk == null)
                this.DefaultCowgirl();

            if (this.DragongirlMilk == null)
                this.DefaultDragongirlMilk();
            if (this.DragongirlScales == null)
                this.DefaultDragongirlScales();

            if (this.DryadMilk == null)
                this.DefaultDryadMilk();
            if (this.DryadWool == null)
                this.DefaultDryadWool();

            if (this.ForestFairyDust == null)
                this.DefaultFairyForest();

            if (this.IceFairyDust == null)
                this.DefaultFairyIce();

            if (this.FoxgirlMilk == null)
                this.DefaultFoxgirl();

            if (this.HarpyEggs == null)
                this.DefaultHarpyEggs();
            if (this.HarpyFeathers == null)
                this.DefaultHarpyFeathers();

            if (this.BlackHarpyEggs == null)
                this.DefaultBlackHarpyEggs();
            if (this.BlackHarpyFeathers == null)
                this.DefaultBlackHarpyFeathers();

            if (this.ImpMotherMilk == null)
                this.DefaultImpMother();

            if (this.SlimegirlSlime == null)
                this.DefaultSlimegirlSlime();

            if (this.ThumbromorphMilk == null)
                this.DefaultThrumbomorphMilk();
            if (this.ThumbromorphWool == null)
                this.DefaultThrumbomorphWool();
        }

        public void DefaultCentaurMilk()
        {
            this.CentaurMilk = new Production()
            {
                IntervalDays = 1,
                Amount = 25,
            };
        }

        public void DefaultCentaurHair()
        {
            this.CentaurHair = new Production()
            {
                IntervalDays = 25,
                Amount = 100,
            };
        }


        public void DefaultCowgirl()
        {
            this.CowgirlMilk = new Production()
            {
                IntervalDays = 1,
                Amount = 25,
            };
        }


        public void DefaultDragongirlMilk()
        {
            this.DragongirlMilk = new Production()
            {
                IntervalDays = 2,
                Amount = 8,
            };
        }
        public void DefaultDragongirlScales()
        {
            this.DragongirlScales = new Production()
            {
                IntervalDays = 30,
                Amount = 120,
            };
        }

        public void DefaultDryadMilk()
        {
            this.DryadMilk = new Production()
            {
                IntervalDays = 1,
                Amount = 25,
            };
        }

        public void DefaultDryadWool()
        {
            this.DryadWool = new Production()
            {
                IntervalDays = 15,
                Amount = 100,
            };
        }

        public void DefaultFairyForest()
        {
            this.ForestFairyDust = new Production()
            {
                IntervalDays = 1,
                Amount = 10,
            };
        }

        public void DefaultFairyIce()
        {
            this.IceFairyDust = new Production()
            {
                IntervalDays = 1,
                Amount = 10,
            };
        }

        public void DefaultFoxgirl()
        {
            this.FoxgirlMilk = new Production()
            {
                IntervalDays = 1,
                Amount = 10,
            };
        }

        public void DefaultBlackHarpyEggs()
        {
            this.BlackHarpyEggs = new EggProduction()
            {
                IntervalDays = 10,
                CountMin = 1,
                CountMax = 1,
                FertalizedCountMax = 1,
            };
        }

        public void DefaultBlackHarpyFeathers()
        {
            this.BlackHarpyFeathers = new Production()
            {
                IntervalDays = 10,
                Amount = 100,
            };
        }

        public void DefaultHarpyEggs()
        {
            this.HarpyEggs = new EggProduction()
            {
                IntervalDays = 10,
                CountMin = 1,
                CountMax = 1,
                FertalizedCountMax = 1,
            };
        }

        public void DefaultHarpyFeathers()
        {
            this.HarpyFeathers = new Production()
            {
                IntervalDays = 10,
                Amount = 100,
            };
        }

        public void DefaultImpMother()
        {
            this.ImpMotherMilk = new Production()
            {
                IntervalDays = 1,
                Amount = 25,
            };
        }

        public void DefaultSlimegirlSlime()
        {
            this.SlimegirlSlime = new Production()
            {
                IntervalDays = 1,
                Amount = 5,
            };
        }

        public void DefaultThrumbomorphMilk()
        {
            this.ThumbromorphMilk = new Production()
            {
                IntervalDays = 1,
                Amount = 10,
            };
        }

        public void DefaultThrumbomorphWool()
        {
            this.ThumbromorphWool = new Production()
            {
                IntervalDays = 15,
                Amount = 100,
            };
        }
    }

    class Production : IExposable
    {
        public int IntervalDays;
        public int Amount;

        public Production() : base() { }

        public void ExposeData()
        {
            Scribe_Values.Look(ref this.IntervalDays, "intervalDays", 1, false);
            Scribe_Values.Look(ref this.Amount, "amount", 1, false);
        }
    }

    class EggProduction : IExposable
    {
        public int IntervalDays;
        public int CountMin;
        public int CountMax;
        public int FertalizedCountMax;

        public EggProduction() : base() { }

        public void ExposeData()
        {
            Scribe_Values.Look(ref this.IntervalDays, "intervalDays", 1, false);
            Scribe_Values.Look(ref this.CountMin, "countMin", 1, false);
            Scribe_Values.Look(ref this.CountMax, "countMax", 1, false);
            Scribe_Values.Look(ref this.FertalizedCountMax, "fertalizedCountMax", 1, false);
        }
    }
}