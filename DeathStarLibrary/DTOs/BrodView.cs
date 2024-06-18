namespace DeathStarLibrary.DTOs;

public class BrodView
{
    public int BrodID { get; set; }
    public string? Naziv { get; set; }
    public double MaxBrzina { get; set; }
    public SavezView? Savez { get; set; }
    public PlanetaView? Planeta { get; set; }

    public BrodView()
    {

    }

    internal BrodView(Brod? brod)
    {
        if(brod!=null)
        {
            BrodID = brod.BrodID;
            Naziv = brod.Naziv;
            MaxBrzina = brod.MaxBrzina;
            if (brod!.Savez! != null)
                Savez = new(brod.Savez);
            if (brod!.Planeta! != null)
                Planeta = new(brod.Planeta);
        }
    }
}
