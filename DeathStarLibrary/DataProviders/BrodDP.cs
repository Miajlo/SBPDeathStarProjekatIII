
namespace DeathStarLibrary.DataProviders;

public static class BrodDP
{
    public async static Task<Result<List<BrodView>, ErrorMessage>> ReturnAllBrod()
    {
        ISession? s = null;

        List<BrodView> brodovi = new();

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);


            brodovi = (await s.QueryOver<Brod>().ListAsync())
                        .Select(p => new BrodView(p)).ToList();
        }
        catch (Exception)
        {
            return "Nemoguće vratiti sve brodove.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return brodovi;
    }

    public async static Task<Result<int, ErrorMessage>> AddBrodAsync(BrodView p)
    {
        ISession? s = null;
        int brodID = 0;
        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            Brod o = new()
            {
                Naziv = p.Naziv,
                MaxBrzina = p.MaxBrzina
            };

            await s.SaveOrUpdateAsync(o);
            brodID = o.BrodID;
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return GetError("Nemoguće dodati brod.", 404);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return brodID;
    }

    public async static Task<Result<BrodView, ErrorMessage>> UpdateBrodAsync(BrodView p)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

                Brod brod = s.Load<Brod>(p.BrodID);

                if (brod == null)
                    return "Brod sa datim ID-om ne postoji.".ToError(404);

                brod.Naziv = p.Naziv;
                brod.MaxBrzina = p.MaxBrzina;
                await s.UpdateAsync(brod);
                await s.FlushAsync();
            }
            catch (Exception)
            {
                return "Nemoguće ažurirati brod.".ToError(400);
            }
            finally
            {
                s?.Close();
                s?.Dispose();
            }

        return p;
    }

    public async static Task<Result<BrodView, ErrorMessage>> ReturnBrodAsync(int id)
    {
        ISession? s = null;

        BrodView brodView = default!;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            Brod o = await s.LoadAsync<Brod>(id);
            brodView = new BrodView(o);
        }
        catch (Exception)
        {
            return "Nemoguće vratiti brod sa zadatim ID-jem.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return brodView;
    }

    public async static Task<Result<bool, ErrorMessage>> DeleteBrodAsync(int id)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            Brod o = await s.LoadAsync<Brod>(id);

            await s.DeleteAsync(o);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće obrisati brod.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
}
