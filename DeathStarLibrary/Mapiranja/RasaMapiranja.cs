namespace DeathStarLibrary.Mapiranja;

internal class RasaMapiranja : ClassMap<Rasa>
{
    public RasaMapiranja()
    {
        Table("RASA");

        Id(x => x.RasaID).Column("RASA_ID").GeneratedBy.TriggerIdentity();

        Map(x => x.Naziv).Column("NAZIV");

        HasMany(x => x.Planete).KeyColumn("RASA_ID").LazyLoad().Cascade.All().Inverse();
        HasMany(x => x.Galaksije).KeyColumn("RASA_ID").LazyLoad().Cascade.All().Inverse();
    }
}
