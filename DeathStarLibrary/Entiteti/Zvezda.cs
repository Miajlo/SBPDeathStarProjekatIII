namespace DeathStarLibrary.Entiteti;

internal class Zvezda
{
    #region Properties
    internal protected virtual int ZvezdaID { get; set; }
    internal protected virtual string? Naziv { get; set; }
    internal protected virtual string? Tip { get; set; }
    internal protected virtual IList<Planeta>? Planete { get; set; }
    internal protected virtual IList<ZvezdaniSistem>? ZvezdaniSistem { get; set; }
    #endregion

    internal Zvezda()
    {
        ZvezdaniSistem = new List<ZvezdaniSistem>();
        Planete = new List<Planeta>();
    }
}

