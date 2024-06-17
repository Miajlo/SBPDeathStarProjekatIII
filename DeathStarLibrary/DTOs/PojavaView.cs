namespace DeathStarLibrary.DTOs;

public class PojavaView
{
    public int PojavaID { get; set; }
    public string? Tip { get; set; }
    public string? Naziv { get; set; }
    public char Opasna { get; set; }
    public double RastojanjeOP { get; set; }
    public PlanetaView? Planeta { get; set; }

    public PojavaView() { }

    internal PojavaView(Pojava? p)
    {
        if (p != null)
        {
            PojavaID = p.PojavaID;
            Tip = p.Tip;
            Naziv = p.Naziv;
            Opasna = p.Opasna;
            RastojanjeOP = p.RastojanjeOP;
        }
    }
}
