namespace DeathStarLibrary.Entiteti;

internal class SvemirskaStanica
{
    #region Properties
    internal protected virtual int SSID { get; set; }
    internal protected virtual string? Naziv { get; set; }
    internal protected virtual int BrojLjudi { get; set; }
    internal protected virtual double Velicina { get; set; }
    internal protected virtual double RastojanjeOP { get; set; }
    internal protected virtual string? Tip { get; set; }
    internal protected virtual string? Namena { get; set; }
    internal protected virtual Planeta? Planeta { get; set; }
    #endregion
}
