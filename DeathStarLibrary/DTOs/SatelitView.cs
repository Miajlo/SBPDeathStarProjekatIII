namespace DeathStarLibrary.DTOs;

public class SatelitView
{
    public int SatelitID { get; set; }
    public string? Naziv { get; set; }
    public double Precnik { get; set; }
    public double RastojanjeOP { get; set; }
    public char Naseobine { get; set; }
    public PlanetaView? Planeta { get; set; }

    public SatelitView() { }

    internal SatelitView(Satelit? s) : this()
    {
        if (s != null)
        {
            SatelitID = s.SatelitID;
            Naziv = s.Naziv;
            Precnik = s.Precnik;
            RastojanjeOP = s.RastojanjeOP;
            Naseobine = s.Naseobine;
            if (s!.Planeta != null)
                Planeta = new(s!.Planeta!);
        }
    }
}
