namespace DeathStarLibrary.Mapiranja;

internal class IgracMapiranja: ClassMap<Igrac>
{
    public IgracMapiranja()
    {
        Table("IGRAC");

        Id(x => x.IgracID).Column("IGRAC_ID").GeneratedBy.TriggerIdentity();

        Map(x => x.KorisnickoIme).Column("KORISNICKO_IME");
        Map(x => x.DatumOtvaranjaNaloga).Column("DATUM_OTVARANJA_NALOGA");
        Map(x => x.LIme).Column("LIME");
        Map(x => x.SSlovo).Column("SSLOVO");
        Map(x => x.Prezime).Column("PREZIME");
        Map(x => x.Email).Column("EMAIL");
        Map(x => x.Pol).Column("POL");
        Map(x => x.DatumRodjenja).Column("DATUM_RODJENJA");
        Map(x => x.Drzava).Column("DRZAVA");
        Map(x => x.Opis).Column("OPIS");
        Map(x => x.Slika).Column("SLIKA");

        References(x => x.Savez).Column("SAVEZ_ID").LazyLoad();
        HasMany(x => x.Planete).KeyColumn("IGRAC_ID").LazyLoad().Cascade.All().Inverse();
    }
}

