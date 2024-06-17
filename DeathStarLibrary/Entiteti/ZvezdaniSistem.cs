namespace DeathStarLibrary.Entiteti;

internal class ZvezdaniSistem
{
    #region Properties
    internal protected virtual int ZvezdaniSistemID { get; set; }
    internal protected virtual ZvezdaniSistemID ID { get; set; }
    #endregion

    internal ZvezdaniSistem()
    {
        ID = new ZvezdaniSistemID();
    }
}

