namespace DeathStarLibrary.Entiteti;

internal class Igrac
{
    #region Properties
    internal protected virtual int IgracID { get; set; }
    internal protected virtual string? KorisnickoIme { get; set; }
    internal protected virtual DateTime DatumOtvaranjaNaloga { get; set; }
    internal protected virtual string? LIme { get; set; }
    internal protected virtual string? Prezime { get; set; }
    internal protected virtual char SSlovo { get; set; }
    internal protected virtual string? Email { get; set; }
    internal protected virtual char Pol { get; set; }
    internal protected virtual DateTime DatumRodjenja { get; set; }
    internal protected virtual string? Drzava { get; set; }
    internal protected virtual string? Opis { get; set; }
    internal protected virtual string? Slika { get; set; }
    internal protected virtual Savez? Savez { get; set; }
    internal protected virtual IList<Planeta> Planete { get; set; }
    #endregion

    internal Igrac()
    {
        Planete = new List<Planeta>();
    }
}
