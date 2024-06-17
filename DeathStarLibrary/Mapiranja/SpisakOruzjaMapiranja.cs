namespace DeathStarLibrary.Mapiranja;

internal class SpisakOruzjaMapiranja : ClassMap<SpisakOruzja>
{
    public SpisakOruzjaMapiranja()
    {
        Table("SPISAK_ORUZJA");

        Id(x => x.SOID).Column("SOID").GeneratedBy.TriggerIdentity();

        Map(x => x.Oruzje).Column("ORUZJE");

        References(x => x.SvemirskaStanica).Column("SSID").LazyLoad();       
    }
}
