namespace DeathStarLibrary.Mapiranja;

internal class PojavaMapiranja : ClassMap<Pojava>
{
    public PojavaMapiranja()
    {
        Table("POJAVA");

        Id(x => x.PojavaID).Column("POJAVA_ID").GeneratedBy.TriggerIdentity();

        Map(x => x.Naziv).Column("NAZIV");
        Map(x => x.Tip).Column("TIP");
        Map(x => x.Opasna).Column("OPASNA");
        Map(x => x.RastojanjeOP).Column("RASTOJANJE_OP");

        References(x => x.Planeta).Column("PLANETA_ID").LazyLoad();
    }
}
