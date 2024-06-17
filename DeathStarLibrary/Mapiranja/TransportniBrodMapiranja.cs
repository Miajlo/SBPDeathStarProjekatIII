namespace DeathStarLibrary.Mapiranja;

internal class TransportniBrodMapiranja : ClassMap<TransportniBrod>
{
    public TransportniBrodMapiranja()
    {
        Table("TRANSPORTNI_BROD");

        Id(x => x.BrodID).Column("BROD_ID").GeneratedBy.Assigned();

        Map(x => x.Nosivost).Column("NOSIVOST");
        Map(x => x.Zastita).Column("ZASTITA");

    }
}
