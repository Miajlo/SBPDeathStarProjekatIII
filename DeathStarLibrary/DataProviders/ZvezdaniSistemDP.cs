using NHibernate.Linq;

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

    public async static Task<Result<int, ErrorMessage>> AddZvezdaniSistemAsync(ZvezdaniSistemView p)
    {
        ISession? s = null;
        int zSID = 0;
        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            ZvezdaniSistem o = new()
            { 

            };

            
            await s.SaveOrUpdateAsync(o);
            zSID = o.ZvezdaniSistemID;
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return GetError("Nemoguće dodati pojavu.", 404);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return zSID;
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
            return "Nemoguće ažurirati pojavu.".ToError(400);
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
            return "Nemoguće obrisati pojavu.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
}
