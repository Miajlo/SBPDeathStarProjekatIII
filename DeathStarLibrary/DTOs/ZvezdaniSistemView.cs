namespace DeathStarLibrary.DTOs;

public class ZvezdaniSistemView
{
    public int ZvezdaniSistemID { get; set; }
    public PlanetaView? PlanetaSistema { get; set; }
    public ZvezdaView? ZvezdaSistema { get; set; }

    public ZvezdaniSistemView()
    {
        PlanetaSistema = new PlanetaView();
        ZvezdaSistema = new ZvezdaView();
    }

    internal ZvezdaniSistemView(ZvezdaniSistem? z) : this()
    {
        if(z!=null)
        {
            ZvezdaniSistemID = z.ZvezdaniSistemID;
            ZvezdaSistema = new(z.ID.ZvezdaSistema);
            PlanetaSistema = new(z.ID.PlanetaSistema);
        }
    }
}
