namespace DeathStarLibrary.Mapiranja;

internal class PlanetaMapiranja: ClassMap<Planeta>
{
    public PlanetaMapiranja() 
    {
        Table("PLANETA");

        Id(x => x.PlanetaID).Column("PLANETA_ID").GeneratedBy.TriggerIdentity();

        Map(x => x.Naziv).Column("NAZIV");
        Map(x => x.X).Column("X");
        Map(x => x.Y).Column("Y");
        Map(x => x.Z).Column("Z");
        Map(x => x.BrojStanovnika).Column("BROJ_STANOVNIKA");
        Map(x => x.DrustvenoUredjenje).Column("DRUSTVENO_UREDJENJE");
        Map(x => x.PlutonijumKol).Column("PLUTONIJUM_KOL");
        Map(x => x.BerilijumKol).Column("BERILIJUM_KOL");
        Map(x => x.TrilijumKol).Column("TRILIJUM_KOL");
        Map(x => x.JeMaticna).Column("JE_MATICNA");

        References(x => x.GlavniGrad).Column("GLAVNI_GRAD").LazyLoad();
        References(x => x.DominantnaRasa).Column("RASA_ID").LazyLoad();
        References(x => x.Galaksija).Column("GALAKSIJA_ID").LazyLoad();
        References(x => x.Vlasnik).Column("IGRAC_ID").LazyLoad();

        HasMany(x => x.Pojave).KeyColumn("PLANETA_ID").LazyLoad().Cascade.All().Inverse();
        HasMany(p => p.Gradovi).KeyColumn("PLANETA_ID").LazyLoad().Cascade.All().Inverse();
        HasMany(x => x.Sateliti).KeyColumn("PLANETA_ID").LazyLoad().Cascade.All().Inverse();
        HasMany(p => p.SvemirskeStanice).KeyColumn("PLANETA_ID").LazyLoad().Cascade.All().Inverse();
        HasMany(x => x.Brodovi).KeyColumn("PLANETA_ID").LazyLoad().Cascade.All().Inverse();

       
        HasManyToMany(x => x.Zvezde).Table("ZVEZDANI_SISTEM")
                                    .ParentKeyColumn("PLANETA_ID")
                                    .ChildKeyColumn("ZVEZDA_ID")
                                    .Inverse()
                                    .Cascade.All();
    }
}
