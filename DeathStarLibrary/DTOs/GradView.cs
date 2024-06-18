namespace DeathStarLibrary.DTOs;

public class GradView
{
    public int GradID { get; set; }
    public string? Naziv { get; set; }
    public PlanetaView? Planeta { get; set; }

    public GradView() { }

    internal GradView(Grad? g) : this()
    {
        if(g!=null)
        {
            GradID = g.GradID;
            Naziv = g.Naziv;
            if (g!.Planeta != null)
                Planeta = new(g!.Planeta!);
        }
    }
}
