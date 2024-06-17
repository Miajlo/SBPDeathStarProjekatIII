namespace DeathStarLibrary.DataProviders;

public static class PojavaDP
{
    public async static Task<Result<List<PojavaView>, ErrorMessage>> ReturnAllPojava()
    {
        ISession? s = null;

        List<PojavaView> pojave = new();

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            pojave = (await s.QueryOver<Pojava>().ListAsync())
                        .Select(p => new PojavaView(p)).ToList();
        }
        catch (Exception)
        {
            return "Nemoguće vratiti sve pojave.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return pojave;
    }

    public async static Task<Result<int, ErrorMessage>> AddPojavaAsync(PojavaView p)
    {
        ISession? s = null;
        int pojavaID = 0;
        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            Planeta? planeta = null;
            if (p!.Planeta! != null)
                planeta = await s.GetAsync<Planeta>(p!.Planeta!.PlanetaID);

            Pojava o = new()
            {
                Naziv = p.Naziv,
                Opasna = p.Opasna,
                Tip = p.Tip,
                RastojanjeOP = p.RastojanjeOP,
                Planeta = planeta
            };

            await s.SaveOrUpdateAsync(o);
            pojavaID = o.PojavaID;
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

        return pojavaID;
    }

    public async static Task<Result<PojavaView, ErrorMessage>> UpdatePojavaAsync(PojavaView p)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            Pojava pojava = s.Load<Pojava>(p.PojavaID);

            if (pojava == null)
                return "Ne postoji trazena pojava".ToError(404);

            if (p!.Planeta != null && p!.Planeta!.PlanetaID! != pojava!.Planeta!.PlanetaID)
            {
                var plan = await s.GetAsync<Planeta>(p!.Planeta!.PlanetaID);
                if (plan == null)
                    return "Ne postoji trazena planeta".ToError(400);
                pojava.Planeta = plan;
            }
            pojava.Naziv = p.Naziv;
            pojava.Tip = p.Tip;
            pojava.Opasna = p.Opasna;
            pojava.RastojanjeOP = p.RastojanjeOP;
            await s.UpdateAsync(pojava);
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

    public async static Task<Result<PojavaView, ErrorMessage>> ReturnPojavaAsync(int id)
    {
        ISession? s = null;

        PojavaView pojavaView = default!;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            Pojava o = await s.LoadAsync<Pojava>(id);
            pojavaView = new PojavaView(o);
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

        return pojavaView;
    }

    public async static Task<Result<int, ErrorMessage>> DeletePojavaAsync(int id)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            Pojava o = await s.LoadAsync<Pojava>(id);

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

        return id;
    }
}
