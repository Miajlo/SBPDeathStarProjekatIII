namespace DeathStarLibrary.Entiteti;

internal class Kvadrant
{
    #region Properties
    internal protected virtual int KvadrantID { get; set; }
    internal protected virtual int RedniBroj { get; set; }
    internal protected virtual double ProcenjenPrecnik { get; set; }
    internal protected virtual Galaksija? Galaksija { get; set; }
    #endregion
}
