namespace DeathStarLibrary.DTOs;

public class IgracView
{
    public int IgracID { get; set; }
    public string? KorisnickoIme { get; set; }
    public DateTime DatumOtvaranjaNaloga { get; set; }
    public string? LIme { get; set; }
    public string? Prezime { get; set; }
    public char SSlovo { get; set; }
    public string? Email { get; set; }
    public char Pol { get; set; }
    public DateTime DatumRodjenja { get; set; }
    public string? Drzava { get; set; }
    public string? Opis { get; set; }
    public string? Slika { get; set; }
    public virtual SavezView? Savez { get; set; }
    public virtual IList<PlanetaView> Planete { get; set; }

    public IgracView()
    {
        Planete = new List<PlanetaView>();
    }

    internal IgracView(Igrac? i) : this()
    {
        if (i != null)
        {
            IgracID = i.IgracID;
            KorisnickoIme = i.KorisnickoIme;
            DatumOtvaranjaNaloga = i.DatumOtvaranjaNaloga;
            LIme = i.LIme;
            Prezime = i.Prezime;
            SSlovo = i.SSlovo;
            Email = i.Email;
            Pol = i.Pol;
            DatumRodjenja = i.DatumRodjenja;
            Drzava = i.Drzava;
            Opis = i.Opis;
            Slika = i.Slika;
        }
    }
}
