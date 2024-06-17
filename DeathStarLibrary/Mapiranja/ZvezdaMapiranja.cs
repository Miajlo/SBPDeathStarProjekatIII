namespace DeathStarLibrary.Mapiranja;

internal class ZvezdaMapiranja : ClassMap<Zvezda>
{
    public ZvezdaMapiranja()
    {
        Table("ZVEZDA");

        Id(x => x.ZvezdaID).Column("ZVEZDA_ID").GeneratedBy.TriggerIdentity();

        Map(x => x.Naziv).Column("NAZIV");
        Map(x => x.Tip).Column("TIP");

        HasManyToMany(x => x.Planete).Table("ZVEZDANI_SISTEM")
                                    .ParentKeyColumn("ZVEZDA_ID")
                                    .ChildKeyColumn("PLANETA_ID")
                                    .Cascade.All();

    }
}
