namespace DeathStarLibrary.Entiteti;

internal class Satelit
{
    #region Properties
    internal protected virtual int SatelitID { get; set; }
    internal protected virtual string? Naziv { get; set; }
    internal protected virtual double Precnik { get; set; }
    internal protected virtual double RastojanjeOP { get; set; }
    internal protected virtual char Naseobine { get; set; }
    internal protected virtual Planeta? Planeta { get; set; }
    #endregion
}
