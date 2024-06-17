namespace DeathStarLibrary.DataProviders;

public static class RasaDP
{
    public async static Task<Result<List<RasaView>, ErrorMessage>> ReturnAllRasa()
    {
        ISession? s = null;

        List<RasaView> rase = new();

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            rase = (await s.QueryOver<Rasa>().ListAsync())
                                    .Select(p => new RasaView(p)).ToList();
        }
        catch (Exception)
        {
            return "Nemoguće vratiti sve prodavnice.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return rase;
    }

    public async static Task<Result<int, ErrorMessage>> AddRasaAsync(RasaView p)
    {
        ISession? s = null;
        int rasaID = 0;
        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            Rasa o = new()
            {
                Naziv = p.Naziv,
            };

            await s.SaveOrUpdateAsync(o);
            rasaID = o.RasaID;
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return GetError("Nemoguće dodati rasu.", 404);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return rasaID;
    }

    public async static Task<Result<RasaView, ErrorMessage>> UpdateRasaAsync(RasaView p)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            Rasa sv = s.Load<Rasa>(p.RasaID);
            sv.Naziv = p.Naziv;
            await s.UpdateAsync(sv);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće ažurirati rasu.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return p;
    }

    public async static Task<Result<RasaView, ErrorMessage>> ReturnRasaAsync(int id)
    {
        ISession? s = null;

        RasaView RasaView = default!;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            Rasa o = await s.LoadAsync<Rasa>(id);
            RasaView = new RasaView(o);
        }
        catch (Exception)
        {
            return "Nemoguće vratiti rasu sa zadatim ID-jem.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return RasaView;
    }

    public async static Task<Result<bool, ErrorMessage>> DeleteRasaAsync(int id)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            Rasa o = await s.LoadAsync<Rasa>(id);

            await s.DeleteAsync(o);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće obrisati rasu.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
}
