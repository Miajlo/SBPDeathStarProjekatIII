namespace DeathStarLibrary.DTOs;

public class SvemirskaStanicaView
{
    public int SSID { get; set; }
    public string? Naziv { get; set; }
    public int BrojLjudi { get; set; }
    public double Velicina { get; set; }
    public double RastojanjeOP { get; set; }
    public string? Tip { get; set; }
    public string? Namena { get; set; }
    public PlanetaView? Planeta { get; set; }

    public SvemirskaStanicaView() { }

    internal SvemirskaStanicaView(SvemirskaStanica? s):this()
    {
        if (s != null)
        {
            SSID = s.SSID;
            Naziv = s.Naziv;
            BrojLjudi = s.BrojLjudi;
            Velicina = s.Velicina;
            RastojanjeOP = s.RastojanjeOP;
            Tip = s.Tip;
            Namena = s.Namena;
            Planeta = new(s.Planeta);
        }    
    }
}
