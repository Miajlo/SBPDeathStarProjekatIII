using DeathStarLibrary.Entiteti;

namespace DeathStarLibrary.DataProviders;

public static class SavezDP
{
    public async static Task<Result<List<SavezView>, ErrorMessage>> ReturnAllSavez()
    {
        ISession? s = null;

        List<SavezView> savezi = new();

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            savezi = (await s.QueryOver<Savez>().ListAsync())
                        .Select(p => new SavezView(p)).ToList();
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

        return savezi;
    }

    public async static Task<Result<int, ErrorMessage>> AddSavezAsync(SavezView p)
    {
        ISession? s = null;
        int savezID = 0;
        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            Savez o = new()
            {
                Naziv = p.Naziv,
                DatumFormiranja = p.DatumFormiranja,
                SavezID = p.SavezID,
            };

           
            await s.SaveAsync(o);
            savezID = o.SavezID;
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return GetError("Nemoguće dodati savez.", 404);
        }
        finally
        {
            s?.Close();
            s?.Dispose();           
        }
        return savezID;
    }

    public async static Task<Result<SavezView, ErrorMessage>> UpdateSavezAsync(SavezView p)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            Savez sv = s.Load<Savez>(p.SavezID);
            if (sv == null)
                return "Savez sa datim ID-om ne postoji.".ToError(404);

            sv.Naziv = p.Naziv;
            sv.DatumFormiranja = p.DatumFormiranja;
            await s.UpdateAsync(sv);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće ažurirati savez.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return p;
    }

    public async static Task<Result<SavezView, ErrorMessage>> ReturnSavezAsync(int id)
    {
        ISession? s = null;

        SavezView SavezView = default!;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            Savez o = await s.LoadAsync<Savez>(id);
            SavezView = new SavezView(o);
        }
        catch (Exception)
        {
            return "Nemoguće vratiti savez sa zadatim ID-jem.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return SavezView;
    }

    public async static Task<Result<bool, ErrorMessage>> DeleteSavezAsync(int id)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            Savez o = await s.LoadAsync<Savez>(id);

            await s.DeleteAsync(o);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće obrisati savez.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
}
