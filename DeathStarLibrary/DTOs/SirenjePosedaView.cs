namespace DeathStarLibrary.DTOs;

public class SirenjePosedaView
{
    public int SPID { get; set; }
    public string? Tip { get; set; }
    public SavezView? Savez { get; set; }
    public DateTime Datum { get; set; }
    public IgracView? PrethodniVlasnik { get; set; }

    public SirenjePosedaView() { }

    internal SirenjePosedaView(SirenjePoseda? s) : this()
    {
        if (s != null)
        {
            SPID = s.SPID;
            Tip = s.Tip;
            Datum = s.Datum;
            if(s.PrethodniVlasnik!=null)
                PrethodniVlasnik = new(s!.PrethodniVlasnik!);
            if (s.Savez != null)
                Savez = new(s.Savez);
        }
        
    }
}
