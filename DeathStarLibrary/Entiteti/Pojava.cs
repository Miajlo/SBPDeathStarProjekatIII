namespace DeathStarLibrary.Entiteti;

internal class Pojava
{
    #region Properties
    internal protected virtual int PojavaID { get; set; }
    internal protected virtual string? Tip { get; set; }
    internal protected virtual string? Naziv { get; set; }
    internal protected virtual char Opasna { get; set; }
    internal protected virtual double RastojanjeOP { get; set; }
    internal protected virtual Planeta? Planeta { get; set; }
    #endregion
}
