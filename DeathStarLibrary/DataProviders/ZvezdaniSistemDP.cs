using DeathStarLibrary.Entiteti;
using NHibernate.Mapping.ByCode;

namespace DeathStarLibrary.DataProviders;

public static class ZvezdaniSistemDP
{
    public async static Task<Result<List<ZvezdaniSistemView>, ErrorMessage>> ReturnAllZvezdaniSistem()
    {
        ISession? s = null;

        List<ZvezdaniSistemView> zvezdaniSistemi = new();

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            zvezdaniSistemi = (await s.QueryOver<ZvezdaniSistem>().ListAsync())
                        .Select(p => new ZvezdaniSistemView(p)).ToList();
        }
        catch (Exception)
        {
            return "Nemoguće vratiti sve zvezdaniSistemi.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return zvezdaniSistemi;
    }

    public async static Task<Result<List<ZvezdaniSistemView>,ErrorMessage>> ReturnSystemByZvezdaId(int zvezdaID)
    {
        ISession? s = null;

        List<ZvezdaniSistemView> zvezdaniSistemi = new List<ZvezdaniSistemView>();
        try
        {
             s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            
            var sistemi = await s.Query<ZvezdaniSistem>()
                                 .Where(zs => zs.ID.ZvezdaSistema!.ZvezdaID == zvezdaID)
                                 .ToListAsync();

            var uniquePlanete = sistemi.Select(zs => zs.ID.PlanetaSistema).Distinct().ToList();

            foreach (var planeta in uniquePlanete)
            {
                var zvezde = await s.Query<Zvezda>()
                                    .Where(z => z.Planete!.Any(p => p.PlanetaID == planeta!.PlanetaID))
                                    .ToListAsync();

                foreach (var zvezda in zvezde)
                {
                    zvezdaniSistemi.Add(new ZvezdaniSistemView
                    {
                        ZvezdaSistema = new(zvezda),
                        PlanetaSistema = new(planeta)
                    });
                }
            }
        }
        catch (Exception ec)
        {
            Console.WriteLine(ec.Message);
        }

        return zvezdaniSistemi;
    }

    public async static Task<Result<List<ZvezdaniSistemView>, ErrorMessage>> ReturnSystemByPlanetaId(int planetaID)
    {
        ISession? s = null;

        List<ZvezdaniSistemView> zvezdaniSistemi = new List<ZvezdaniSistemView>();
        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);


            var sistemi = await s.Query<ZvezdaniSistem>()
                                 .Where(zs => zs.ID.PlanetaSistema!.PlanetaID == planetaID)
                                 .ToListAsync();

            var uniqueZvezde = sistemi.Select(zs => zs.ID.ZvezdaSistema).Distinct().ToList();

            foreach (var zvezda in uniqueZvezde)
            {
                var planete = await s.Query<Planeta>()
                                    .Where(z => z.Zvezde!.Any(p => p.ZvezdaID== zvezda!.ZvezdaID))
                                    .ToListAsync();

                foreach (var planeta in planete)
                {
                    zvezdaniSistemi.Add(new ZvezdaniSistemView
                    {
                        ZvezdaSistema = new(zvezda),
                        PlanetaSistema = new(planeta)
                    });
                }
            }
        }
        catch (Exception ec)
        {
            Console.WriteLine(ec.Message);
        }

        return zvezdaniSistemi;
    }

    public async static Task<Result<bool, ErrorMessage>> AddZvezdaniSistemAsync(ZvezdaniSistemView p)
    {
        ISession? s = null;
        
        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            if (p.ZvezdaSistema == null)
                return "Nema zvezde za dodavanje".ToError(400);

            if (p.PlanetaSistema == null)
                return "Nema planete za dodavanje".ToError(404);


            var zvezda = await s.GetAsync<Zvezda>(p!.ZvezdaSistema!.ZvezdaID);

            if (zvezda == null)
                return "Ne postoji trazena zvezda".ToError(404);

            var planeta = await s.GetAsync<Planeta>(p!.PlanetaSistema!.PlanetaID);

            if (planeta == null)
                return "Ne postoji trazena planeta".ToError(404);

            var zvezdaVeze = await s.Query<ZvezdaniSistem>()
                                    .Where(zs => zs!.ID!.ZvezdaSistema!.ZvezdaID
                                              == p.ZvezdaSistema.ZvezdaID)
                                    .ToListAsync();

            var planetaVeze = await s.Query<ZvezdaniSistem>()
                                     .Where(zs => zs!.ID!.PlanetaSistema!.PlanetaID
                                               == p!.PlanetaSistema!.PlanetaID)
                                     .ToListAsync();

            if ((planetaVeze.Count > 0 && zvezdaVeze.Count > 0))
                return "Nije moguce kreiranje date veze: planeta i zvezda su vec u sistemu"
                       .ToError(400);

            if(planetaVeze.Count == 0 && zvezdaVeze.Count > 0)
            {
                var helper = zvezdaVeze[0].ID!.PlanetaSistema!.PlanetaID;
                var zvezde = await s.Query<ZvezdaniSistem>()
                                    .Where(zs => zs!.ID!.PlanetaSistema!.PlanetaID == helper)
                                    .ToListAsync();

                foreach(var z in zvezde)
                {
                    ZvezdaniSistem zs = new()
                    {
                        ID = new()
                        {
                            ZvezdaSistema = z.ID.ZvezdaSistema,
                            PlanetaSistema = planeta
                        }
                    };
                    await s.SaveAsync(zs);
                }
            }

            else if (zvezdaVeze.Count == 0 && planetaVeze.Count > 0)
            {
                var helper = planetaVeze[0].ID!.ZvezdaSistema!.ZvezdaID;

                var planete = await s.Query<ZvezdaniSistem>()
                                    .Where(zs => zs!.ID!.ZvezdaSistema!.ZvezdaID == helper)
                                    .ToListAsync();

                foreach (var z in planete)
                {
                    ZvezdaniSistem zs = new()
                    {
                        ID = new()
                        {
                            ZvezdaSistema = zvezda,
                            PlanetaSistema = z.ID.PlanetaSistema
                        }
                    };
                    await s.SaveAsync(zs);
                }
            }
            else
            {
                ZvezdaniSistem o = new()
                {
                    ID = new()
                    {
                        ZvezdaSistema = zvezda,
                        PlanetaSistema = planeta
                    }
                };


                await s.SaveOrUpdateAsync(o);
                await s.FlushAsync();
            }
            
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return GetError("Nemoguće dodati zvezdani sistem.", 404);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }

    public async static Task<Result<ZvezdaniSistemView, ErrorMessage>> UpdateZvezdaniSistemAsync(ZvezdaniSistemView p)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            ZvezdaniSistem ZvezdaniSistem = s.Load<ZvezdaniSistem>(p.ZvezdaniSistemID);

            await s.UpdateAsync(ZvezdaniSistem);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće ažurirati zvezdani sistem.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return p;
    }

    public async static Task<Result<ZvezdaniSistemView, ErrorMessage>> ReturnZvezdaniSistemAsync(int id)
    {
        ISession? s = null;

        ZvezdaniSistemView ZvezdaniSistemView = default!;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            ZvezdaniSistem o = await s.LoadAsync<ZvezdaniSistem>(id);
            ZvezdaniSistemView = new ZvezdaniSistemView(o);
        }
        catch (Exception)
        {
            return "Nemoguće vratiti pojavu sa zadatim ID-jem.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return ZvezdaniSistemView;
    }

    public async static Task<Result<bool, ErrorMessage>> DeleteZvezdaniSistemAsync(int id)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            ZvezdaniSistem o = await s.LoadAsync<ZvezdaniSistem>(id);

            await s.DeleteAsync(o);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće obrisati zvezdani sistem.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
}
