namespace DeathStarLibrary.Entiteti;

internal class Brod
{
    #region Properties
    internal protected virtual int BrodID { get; set; }
    internal protected virtual string? Naziv { get; set; }
    internal protected virtual double MaxBrzina { get; set; }
    internal protected virtual Savez? Savez { get; set; }
    internal protected virtual Planeta? Planeta { get; set; }
    #endregion
}
