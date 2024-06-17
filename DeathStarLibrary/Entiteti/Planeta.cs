namespace DeathStarLibrary.Entiteti;

internal class Planeta
{
    #region Properties
    internal protected virtual int PlanetaID { get; set; }
    internal protected virtual string? Naziv { get; set; }
    internal protected virtual double X { get; set; }
    internal protected virtual double Y { get; set; }
    internal protected virtual double Z { get; set; }
    internal protected virtual long BrojStanovnika { get; set; }
    internal protected virtual string? DrustvenoUredjenje { get; set; }
    internal protected virtual double PlutonijumKol { get; set; }
    internal protected virtual double BerilijumKol { get; set; }
    internal protected virtual double TrilijumKol { get; set; }
    internal protected virtual char JeMaticna { get; set; }
    internal protected virtual Grad? GlavniGrad { get; set; }
    internal protected virtual Galaksija? Galaksija { get; set; }
    internal protected virtual Igrac? Vlasnik { get; set; }
    internal protected virtual Rasa? DominantnaRasa { get; set; }
    internal protected virtual IList<Pojava>? Pojave { get; set; }
    internal protected virtual IList<Grad>? Gradovi { get; set; }
    internal protected virtual IList<Satelit>? Sateliti { get; set; }
    internal protected virtual IList<SvemirskaStanica>? SvemirskeStanice { get; set; }
    internal protected virtual IList<Brod>? Brodovi { get; set; }
    internal protected virtual IList<Zvezda> Zvezde { get; set; }
    internal protected virtual IList<ZvezdaniSistem> Sistem { get; set; }
    #endregion

    internal Planeta()
    {
        Pojave = new List<Pojava>();
        Gradovi = new List<Grad>();
        Sateliti = new List<Satelit>();
        SvemirskeStanice = new List<SvemirskaStanica>();
        Brodovi = new List<Brod>();
        Sistem = new List<ZvezdaniSistem>();
        Zvezde = new List<Zvezda>();
    }
}

