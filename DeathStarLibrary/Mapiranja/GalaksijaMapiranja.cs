namespace DeathStarLibrary.Mapiranja;

internal class GalaksijaMapiranja : ClassMap<Galaksija>
{
    public GalaksijaMapiranja()
    {
        Table("GALAKSIJA");

        Id(x => x.GalaksijaID).Column("GALAKSIJA_ID").GeneratedBy.TriggerIdentity();

        Map(x => x.Naziv).Column("NAZIV");
        Map(x => x.BrojZvezda).Column("BROJ_ZVEZDA");
        Map(x => x.BrojPlaneta).Column("BROJ_PLANETA");

        References(x => x.DominantnaRasa).Column("RASA_ID").LazyLoad();

        HasMany(x => x.Planete).KeyColumn("GALAKSIJA_ID").LazyLoad().Cascade.All().Inverse();
        HasMany(x => x.Kvadranti).KeyColumn("GALAKSIJA_ID").LazyLoad().Cascade.All().Inverse();
    }
}
