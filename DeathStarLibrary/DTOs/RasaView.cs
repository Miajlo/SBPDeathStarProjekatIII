namespace DeathStarLibrary.DTOs;

public class RasaView
{
    public int RasaID { get; set; }
    public string? Naziv { get; set; }
    public IList<PlanetaView>? Planete { get; set; }
    public IList<GalaksijaView>? Galaksije { get; set; }

    public RasaView()
    {
        Planete = new List<PlanetaView>();
        Galaksije = new List<GalaksijaView>();
    }

    internal RasaView(Rasa? r) : this()
    {
        if (r != null)
        {
            RasaID = r.RasaID;
            Naziv = r.Naziv;
        }
    }
}
