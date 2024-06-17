namespace DeathStarLibrary.Mapiranja;

internal class SirenjePosedaMapiranja : ClassMap<SirenjePoseda>
{
    public SirenjePosedaMapiranja()
    {
        Table("SIRENJE_POSEDA");

        Id(x => x.SPID).Column("SPID").GeneratedBy.TriggerIdentity();

        Map(x => x.Tip).Column("TIP");
        Map(x => x.Datum).Column("DATUM");

        References(x => x.Savez).Column("SAVEZ_ID").LazyLoad();
        References(x => x.PrethodniVlasnik).Column("PRETHODNI_VLASNIK").LazyLoad();
    }
}
