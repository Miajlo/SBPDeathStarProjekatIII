namespace DeathStarLibrary.DTOs;

public class ZvezdaView
{
    public int ZvezdaID { get; set; }
    public string? Naziv { get; set; }
    public string? Tip { get; set; }

    public virtual IList<PlanetaView>? Planete { get; set; }
    public virtual IList<ZvezdaniSistemView>? ZvezdaniSistem { get; set; }

    public ZvezdaView()
    {
        ZvezdaniSistem = new List<ZvezdaniSistemView>();
        Planete = new List<PlanetaView>();
    }

    internal ZvezdaView(Zvezda? z)
    {
        if (z != null)
        {
            ZvezdaID = z.ZvezdaID;
            Naziv = z.Naziv;
            Tip = z.Tip;
        }
    }
}
