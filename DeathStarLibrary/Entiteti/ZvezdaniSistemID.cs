namespace DeathStarLibrary.Entiteti;

internal class ZvezdaniSistemID
{
    #region Properties

    internal protected virtual Planeta? PlanetaSistema { get; set; }
    internal protected virtual Zvezda? ZvezdaSistema { get; set; }
    #endregion

    public override bool Equals(object? obj)
    {
        if (Object.ReferenceEquals(this, obj))
            return true;

        if (obj!.GetType() != typeof(ZvezdaniSistemID))
            return false;

        ZvezdaniSistemID recievedObject = (ZvezdaniSistemID)obj;

        if ((ZvezdaSistema!.ZvezdaID == recievedObject!.ZvezdaSistema!.ZvezdaID) &&
            (PlanetaSistema!.PlanetaID == recievedObject!.PlanetaSistema!.PlanetaID))
        {
            return true;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
