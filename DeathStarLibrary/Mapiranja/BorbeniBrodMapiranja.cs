namespace DeathStarLibrary.Mapiranja;

internal class BorbeniBrodMapiranja : ClassMap<BorbeniBrod>
{
    public BorbeniBrodMapiranja()
    {
        Table("BORBENI_BROD");

        Id(x => x.BrodID).Column("BROD_ID").GeneratedBy.Assigned();

        Map(x => x.BrojTopova).Column("BROJ_TOPOVA");
        Map(x => x.PosedujeFotTorp).Column("POSEDUJU_FOT_TORP");
        Map(x => x.Tip).Column("TIP");
    }
}
