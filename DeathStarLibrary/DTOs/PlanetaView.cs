namespace DeathStarLibrary.DTOs;

public class PlanetaView
{
    public int PlanetaID { get; set; }
    public string? Naziv { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
    public long BrojStanovnika { get; set; }
    public string? DrustvenoUredjenje { get; set; }
    public double PlutonijumKol { get; set; }
    public double BerilijumKol { get; set; }
    public double TrilijumKol { get; set; }
    public char JeMaticna { get; set; }
    public GradView? GlavniGrad { get; set; }
    public GalaksijaView? Galaksija { get; set; }
    public IgracView? Vlasnik { get; set; }
    public RasaView? DominantnaRasa { get; set; }
    public IList<PojavaView>? Pojave { get; set; }
    public IList<GradView>? Gradovi { get; set; }
    public IList<SatelitView>? Sateliti { get; set; }
    public IList<SvemirskaStanicaView>? SvemirskeStanice { get; set; }
    public IList<BrodView>? Brodovi { get; set; }
    public IList<ZvezdaView>? Zvezde { get; set; }
    public IList<ZvezdaniSistemView>? Sistem { get; set; }

    public PlanetaView()
    {
        Pojave = new List<PojavaView>();
        Gradovi = new List<GradView>();
        Sateliti = new List<SatelitView>();
        SvemirskeStanice = new List<SvemirskaStanicaView>();
        Brodovi = new List<BrodView>();
        Sistem = new List<ZvezdaniSistemView>();
        Zvezde = new List<ZvezdaView>();
    }

    internal PlanetaView(Planeta? p): this()
    {
        if (p != null)
        {
            PlanetaID = p.PlanetaID;
            Naziv = p.Naziv;
            X = p.X;
            Y = p.Y;
            Z = p.Z;
            BrojStanovnika = p.BrojStanovnika;
            DrustvenoUredjenje = p.DrustvenoUredjenje;
            PlutonijumKol = p.PlutonijumKol;
            BerilijumKol = p.BerilijumKol;
            TrilijumKol = p.TrilijumKol;
            JeMaticna = p.JeMaticna;
        }  
    }
}
