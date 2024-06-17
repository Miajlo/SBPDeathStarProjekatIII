namespace DeathStarLibrary.DataProviders;

public static class GalaksijaDP
{
    public async static Task<Result<List<GalaksijaView>, ErrorMessage>> ReturnAllGalaksija()
    {
        ISession? s = null;

        List<GalaksijaView> galaksije = new();

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            galaksije = (await s.QueryOver<Galaksija>().ListAsync())
                          .Select(p => new GalaksijaView(p)).ToList();
        }
        catch (Exception)
        {
            return "Nemoguće vratiti sve galaksije.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return galaksije;
    }

    public async static Task<Result<int, ErrorMessage>> AddGalaksijaAsync(GalaksijaView p)
    {
        ISession? s = null;
        int galaksijaID = 0;
        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            Galaksija o = new()
            {
                Naziv = p.Naziv,
                BrojPlaneta = p.BrojPlaneta,
                BrojZvezda = p.BrojZvezda,
            };

            await s.SaveOrUpdateAsync(o);
            galaksijaID = o.GalaksijaID;
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return GetError("Nemoguće dodati galaksiju.", 404);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return galaksijaID;
    }

    public async static Task<Result<GalaksijaView, ErrorMessage>> UpdateGalaksijaAsync(GalaksijaView p)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            Galaksija galaksija = s.Load<Galaksija>(p.GalaksijaID);
            galaksija.Naziv = p.Naziv;
            galaksija.BrojPlaneta = p.BrojPlaneta;
            galaksija.BrojZvezda = p.BrojZvezda;
            await s.UpdateAsync(galaksija);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće ažurirati galaksiju.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return p;
    }

    public async static Task<Result<GalaksijaView, ErrorMessage>> ReturnGalaksijaAsync(int id)
    {
        ISession? s = null;

        GalaksijaView GalaksijaView = default!;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            Galaksija o = await s.LoadAsync<Galaksija>(id);
            GalaksijaView = new GalaksijaView(o);
        }
        catch (Exception)
        {
            return "Nemoguće vratiti galaksiju sa zadatim ID-jem.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return GalaksijaView;
    }

    public async static Task<Result<bool, ErrorMessage>> DeleteGalaksijaAsync(int id)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            Galaksija o = await s.LoadAsync<Galaksija>(id);

            await s.DeleteAsync(o);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće obrisati galaksiju.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
}
