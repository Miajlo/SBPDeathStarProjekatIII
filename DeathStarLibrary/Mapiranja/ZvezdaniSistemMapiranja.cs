using FluentNHibernate.Mapping;
using DeathStarLibrary.Entiteti;

namespace SBPDeathStarProjekat.Mapiranja;

internal class ZvezdaniSistemMapiranja : ClassMap<ZvezdaniSistem>
{
    public ZvezdaniSistemMapiranja()
    {
        Table("ZVEZDANI_SISTEM");

        Id(x => x.ZvezdaniSistemID).Column("ZVEZDANI_SISTEM_ID").GeneratedBy.TriggerIdentity();

        CompositeId(x => x.ID)
           .KeyReference(x => x.ZvezdaSistema, "ZVEZDA_ID")
           .KeyReference(x => x.PlanetaSistema, "PLANETA_ID");
    }
}
