namespace DeathStarLibrary.Mapiranja;

internal class SatelitMapiranja : ClassMap<Satelit>
{
    public SatelitMapiranja()
    {
        Table("SATELIT");

        Id(x => x.SatelitID).Column("SATELIT_ID").GeneratedBy.TriggerIdentity();

        Map(x => x.Naziv).Column("NAZIV");
        Map(x => x.Precnik).Column("PRECNIK");
        Map(x => x.RastojanjeOP).Column("RASTOJANJE_OP");
        Map(x => x.Naseobine).Column("NASEOBINE");

        References(x => x.Planeta).Column("PLANETA_ID").LazyLoad();

    }
}
