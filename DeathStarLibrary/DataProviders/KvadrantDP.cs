namespace DeathStarLibrary.DataProviders;
public static class KvadrantDP
{
    public async static Task<Result<List<KvadrantView>, ErrorMessage>> ReturnAllKvadrant()
    {
        ISession? s = null;

        List<KvadrantView> kvadranti = new();

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);


            kvadranti = (await s.QueryOver<Kvadrant>().ListAsync())
                       .Select(p => new KvadrantView(p)).ToList();
        }
        catch (Exception)
        {
            return "Nemoguće vratiti sve kvadrante.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return kvadranti;
    }

    public async static Task<Result<int, ErrorMessage>> AddKvadrantAsync(KvadrantView p)
    {
        ISession? s = null;
        int kvadrantID = 0;
        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            Galaksija? galaksija = null;

            if (p.Galaksija != null && p.Galaksija.GalaksijaID > 0)
                galaksija = await s.GetAsync<Galaksija>(p!.Galaksija!.GalaksijaID);

            Kvadrant o = new()
            {
                RedniBroj = p.RedniBroj,
                ProcenjenPrecnik = p.ProcenjenPrecnik,
                Galaksija=galaksija
            };

            await s.SaveOrUpdateAsync(o);
            kvadrantID = o.KvadrantID;
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return GetError("Nemoguće dodati kvadrant.", 404);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return kvadrantID;
    }

    public async static Task<Result<KvadrantView, ErrorMessage>> UpdateKvadrantAsync(KvadrantView p)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

                Kvadrant kvadrant = s.Load<Kvadrant>(p.KvadrantID);

                if (kvadrant == null)
                    return "Kvadrant sa datim ID-om ne postoji.".ToError(404);

                kvadrant.RedniBroj = p.RedniBroj;
                kvadrant.ProcenjenPrecnik = p.ProcenjenPrecnik;
                await s.UpdateAsync(kvadrant);
                await s.FlushAsync();
            }
            catch (Exception)
            {
                return "Nemoguće ažurirati kvadrant.".ToError(400);
            }
            finally
            {
                s?.Close();
                s?.Dispose();
            }

        return p;
    }

    public async static Task<Result<KvadrantView, ErrorMessage>> ReturnKvadrantAsync(int id)
    {
        ISession? s = null;

        KvadrantView kvadrantView = default!;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            Kvadrant o = await s.LoadAsync<Kvadrant>(id);
            kvadrantView = new KvadrantView(o);
        }
        catch (Exception)
        {
            return "Nemoguće vratiti kvadrant sa zadatim ID-jem.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return kvadrantView;
    }

    public async static Task<Result<bool, ErrorMessage>> DeleteKvadrantAsync(int id)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            Kvadrant o = await s.LoadAsync<Kvadrant>(id);

            await s.DeleteAsync(o);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće obrisati kvadrant.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
}
