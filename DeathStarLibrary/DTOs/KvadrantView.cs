namespace DeathStarLibrary.DTOs;

public class KvadrantView
{
    public int KvadrantID { get; set; }
    public int RedniBroj { get; set; }
    public double ProcenjenPrecnik { get; set; }
    public GalaksijaView? Galaksija { get; set; }

    public KvadrantView() { }

    internal KvadrantView(Kvadrant? k) : this()
    {
        if (k != null)
        {
            KvadrantID = k.KvadrantID;
            RedniBroj = k.RedniBroj;
            ProcenjenPrecnik = k.ProcenjenPrecnik;
            Galaksija = new(k!.Galaksija!);
        }
        
    }
}
