namespace DeathStarLibrary.Mapiranja;

internal class GradMapiranja : ClassMap<Grad>
{
    public GradMapiranja()
    {
        Table("GRAD");

        Id(x => x.GradID).Column("GRAD_ID").GeneratedBy.TriggerIdentity();

        Map(x => x.Naziv).Column("NAZIV");

        References(x => x.Planeta).Column("PLANETA_ID").LazyLoad();
    }
}
