namespace SBPDeathStarProjekat.Mapiranja;

internal class SvemirskaStanicaMapiranja : ClassMap<SvemirskaStanica>
{
    public SvemirskaStanicaMapiranja()
    {
        Table("SVEMIRSKA_STANICA");

        Id(x => x.SSID).Column("SSID").GeneratedBy.TriggerIdentity();

        Map(x => x.Naziv).Column("NAZIV");
        Map(x => x.BrojLjudi).Column("BROJ_LJUDI");
        Map(x => x.Velicina).Column("VELICINA");
        Map(x => x.RastojanjeOP).Column("RASTOJANJE_OP");
        Map(x => x.Tip).Column("TIP");
        Map(x => x.Namena).Column("NAMENA");

        References(x => x.Planeta).Column("PLANETA_ID").LazyLoad();

    }
}
