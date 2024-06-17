using DeathStarLibrary.Entiteti;

namespace DeathStarLibrary.DataProviders;

public static class SatelitDP
{
    public async static Task<Result<List<SatelitView>, ErrorMessage>> ReturnAllSatelit()
    {
        ISession? s = null;

        List<SatelitView> sateliti = new();

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);


            sateliti = (await s.QueryOver<Satelit>().ListAsync())
                      .Select(p => new SatelitView(p)).ToList();
        }
        catch (Exception)
        {
            return "Nemoguće vratiti sve satelite.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return sateliti;
    }

    public async static Task<Result<int, ErrorMessage>> AddSatelitAsync(SatelitView p)
    {
        ISession? s = null;
        int satelitID = 0;
        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            Satelit o = new()
            {
                Naziv = p.Naziv,
                Precnik = p.Precnik,
                RastojanjeOP = p.RastojanjeOP,
                Naseobine = p.Naseobine,
            };

            await s.SaveAsync(o);
            satelitID = o.SatelitID;
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return GetError("Nemoguće dodati satelit.", 404);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return satelitID;
    }

    public async static Task<Result<SatelitView, ErrorMessage>> UpdateSatelitAsync(SatelitView p)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

                Satelit satelit = s.Load<Satelit>(p.SatelitID);

                if (satelit == null)
                    return "Satelit sa datim ID-om ne postoji.".ToError(404);

                satelit.Naziv = p.Naziv;
                satelit.Precnik = p.Precnik;
                satelit.RastojanjeOP = p.RastojanjeOP;
                satelit.Naseobine = p.Naseobine;
                await s.UpdateAsync(satelit);
                await s.FlushAsync();
            }
            catch (Exception)
            {
                return "Nemoguće ažurirati satelit.".ToError(400);
            }
            finally
            {
                s?.Close();
                s?.Dispose();
            }

        return p;
    }

    public async static Task<Result<SatelitView, ErrorMessage>> ReturnSatelitAsync(int id)
    {
        ISession? s = null;

        SatelitView satelitView = default!;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            Satelit o = await s.LoadAsync<Satelit>(id);
            satelitView = new SatelitView(o);
        }
        catch (Exception)
        {
            return "Nemoguće vratiti satelit sa zadatim ID-jem.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return satelitView;
    }

    public async static Task<Result<bool, ErrorMessage>> DeleteSatelitAsync(int id)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            Satelit o = await s.LoadAsync<Satelit>(id);

            await s.DeleteAsync(o);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće obrisati satelit.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
}
