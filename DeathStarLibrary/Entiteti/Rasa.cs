namespace DeathStarLibrary.Entiteti;

internal class Rasa
{
    #region Properties
    internal protected virtual int RasaID { get; set; }
    internal protected virtual string? Naziv { get; set; }
    internal protected virtual IList<Planeta>? Planete { get; set; }
    internal protected virtual IList<Galaksija>? Galaksije { get; set; }
    #endregion

    internal Rasa()
    {
        Planete = new List<Planeta>();
        Galaksije = new List<Galaksija>();
    }
}
