namespace DeathStarLibrary.Entiteti;

internal class Savez
{
    #region Properties
    internal protected virtual int SavezID { get; set; }
    internal protected virtual string? Naziv { get; set; }
    internal protected virtual DateTime DatumFormiranja { get; set; }
    internal protected virtual Savez? NadSavez { get; set; }
    internal protected virtual IList<Igrac>? IgraciSaveza { get; set; }
    internal protected virtual IList<Brod>? BrodoviSaveza { get; set; }
    internal protected virtual IList<SirenjePoseda>? SirenjePoseda { get; set; }
    #endregion

    internal Savez()
    {
        IgraciSaveza = new List<Igrac>();
        BrodoviSaveza = new List<Brod>();
        SirenjePoseda = new List<SirenjePoseda>();
    }
}
