namespace DeathStarLibrary.Mapiranja;

internal class KvadrantMapiranja : ClassMap<Kvadrant>
{   
    public KvadrantMapiranja()
    {
        Table("KVADRANT");

        Id(x => x.KvadrantID).Column("KVADRANT_ID").GeneratedBy.TriggerIdentity();

        Map(x => x.RedniBroj).Column("REDNI_BROJ");
        Map(x => x.ProcenjenPrecnik).Column("PROCENJENI_PRECNIK");

        References(x => x.Galaksija).Column("GALAKSIJA_ID").LazyLoad();
    }
}
