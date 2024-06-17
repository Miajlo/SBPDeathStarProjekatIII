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
        }
        
    }
}
