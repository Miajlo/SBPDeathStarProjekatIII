namespace DeathStarLibrary.DTOs;

public class GalaksijaView
{
    public int GalaksijaID { get; set; }
    public string? Naziv { get; set; }
    public long BrojZvezda { get; set; }
    public long BrojPlaneta { get; set; }
    public RasaView? DominantnaRasa { get; set; }
    public IList<PlanetaView>? Planete { get; set; }
    public IList<KvadrantView>? Kvadranti { get; set; }


    internal GalaksijaView(Galaksija g) : this()
    {
        if (g != null)
        {
            GalaksijaID = g.GalaksijaID;
            Naziv = g.Naziv;
            BrojZvezda = g.BrojZvezda;
            BrojPlaneta = g.BrojPlaneta;
        }
    }

    public GalaksijaView()
    {
        Planete = new List<PlanetaView>();
        Kvadranti = new List<KvadrantView>();
    }
}
