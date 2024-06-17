namespace DeathStarLibrary.Mapiranja;

internal class SavezMapiranja : ClassMap<Savez>
{
    public SavezMapiranja()
    {
        Table("SAVEZ");

        Id(x => x.SavezID).Column("SAVEZ_ID").GeneratedBy.TriggerIdentity();

        Map(x => x.Naziv).Column("NAZIV");
        Map(x => x.DatumFormiranja).Column("DATUM_FORMIRANJA");

        References(x => x.NadSavez).Column("NAD_SAVEZ").LazyLoad();
        HasMany(x => x.SirenjePoseda).KeyColumn("SAVEZ_ID").LazyLoad().Cascade.All().Inverse();
        HasMany(x => x.BrodoviSaveza).KeyColumn("SAVEZ_ID").LazyLoad().Cascade.All().Inverse();
        HasMany(x => x.IgraciSaveza).KeyColumn("SAVEZ_ID").LazyLoad().Cascade.All().Inverse();
    }
}
