namespace DeathStarLibrary.Mapiranja;

internal class BrodMapiranja : ClassMap<Brod>
{
    public BrodMapiranja()
    {
        Table("BROD");

        Id(x => x.BrodID).Column("BROD_ID").GeneratedBy.TriggerIdentity();

        Map(x => x.Naziv).Column("NAZIV");
        Map(x => x.MaxBrzina).Column("MAX_BRZINA");

        References(x => x.Savez).Column("SAVEZ_ID").LazyLoad();
        References(x => x.Planeta).Column("PLANETA_ID").LazyLoad();

    }
}
