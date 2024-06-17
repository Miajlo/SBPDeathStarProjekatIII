namespace DeathStarLibrary.DataProviders;
public static class TransportniBrodDP
{
    public async static Task<Result<List<TransportniBrodView>, ErrorMessage>> ReturnAllTBrod()
    {
        ISession? s = null;

        List<TransportniBrodView> tbrodovi = new();

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);


            tbrodovi = (await s.QueryOver<TransportniBrod>().ListAsync())
                      .Select(p => new TransportniBrodView(p)).ToList();
        }
        catch (Exception)
        {
            return "Nemoguće vratiti sve transportne brodove.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return tbrodovi;
    }

    public async static Task<Result<int, ErrorMessage>> AddTBrodAsync(TransportniBrodView p)
    {
        ISession? s = null;
        int tBrodID = 0;
        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            TransportniBrodView o = new()
            {
                Naziv = p.Naziv,
                MaxBrzina = p.MaxBrzina,
                Nosivost = p.Nosivost,
                Zastita = p.Zastita
            };

            await s.SaveOrUpdateAsync(o);
            tBrodID = o.BrodID;
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return GetError("Nemoguće dodati transportni brod.", 404);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return tBrodID;
    }

    public async static Task<Result<TransportniBrodView, ErrorMessage>> UpdateTBrodAsync(TransportniBrodView p)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

                TransportniBrod brod = s.Load<TransportniBrod>(p.BrodID);
                if (brod == null)
                    return "Transportni brod sa datim ID-om ne postoji.".ToError(404);
                brod.Naziv = p.Naziv;
                brod.MaxBrzina = p.MaxBrzina;
                brod.Zastita = p.Zastita;
                brod.Nosivost = p.Nosivost;
                await s.UpdateAsync(brod);
                await s.FlushAsync();
            }
            catch (Exception)
            {
                return "Nemoguće ažurirati transportni brod.".ToError(400);
            }
            finally
            {
                s?.Close();
                s?.Dispose();
            }

        return p;
    }

    public async static Task<Result<TransportniBrodView, ErrorMessage>> ReturnTBrodAsync(int id)
    {
        ISession? s = null;

        TransportniBrodView tbrodView = default!;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            TransportniBrod o = await s.LoadAsync<TransportniBrod>(id);
            tbrodView = new TransportniBrodView(o);
        }
        catch (Exception)
        {
            return "Nemoguće vratiti transportni brod sa zadatim ID-jem.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return tbrodView;
    }

    public async static Task<Result<bool, ErrorMessage>> DeleteTBrodAsync(int id)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            TransportniBrod o = await s.LoadAsync<TransportniBrod>(id);

            await s.DeleteAsync(o);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće obrisati transportni brod.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
}

