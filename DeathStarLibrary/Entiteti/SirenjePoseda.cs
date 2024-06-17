namespace DeathStarLibrary.Entiteti
{
    internal class SirenjePoseda
    {
        #region Properties
        internal protected virtual int SPID { get; set; }
        internal protected virtual string? Tip { get; set; }
        internal protected virtual Savez? Savez { get; set; }
        internal protected virtual DateTime Datum { get; set; }
        internal protected virtual Igrac? PrethodniVlasnik { get; set; }
        #endregion
    }
}
