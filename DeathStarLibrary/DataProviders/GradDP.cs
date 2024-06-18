namespace DeathStarLibrary.DataProviders;

public static class GradDP
{
    public async static Task<Result<List<GradView>, ErrorMessage>> ReturnAllGrad()
    {
        ISession? s = null;

        List<GradView> gradovi = new();

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);


            gradovi = (await s.QueryOver<Grad>().ListAsync())
                      .Select(p => new GradView(p)).ToList();
        }
        catch (Exception)
        {
            return "Nemoguće vratiti sve gradove.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return gradovi;
    }

    public async static Task<Result<int, ErrorMessage>> AddGradAsync(GradView p)
    {
        ISession? s = null;
        int gradID = 0;
        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            Planeta? planeta = null;

            if (p.Planeta != null && p.Planeta.PlanetaID > 0)
                planeta = await s.GetAsync<Planeta>(p!.Planeta!.PlanetaID);

            Grad o = new()
            {
                Naziv = p.Naziv,
                Planeta=planeta
            };

            await s.SaveOrUpdateAsync(o);
            gradID = o.GradID;
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return GetError("Nemoguće dodati grad.", 404);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return gradID;
    }

    public async static Task<Result<GradView, ErrorMessage>> UpdateGradAsync(GradView p)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

                Grad grad = s.Load<Grad>(p.GradID);
                if (grad == null)
                    return "Grad sa datim ID-om ne postoji.".ToError(404);
                grad.Naziv = p.Naziv;
                await s.UpdateAsync(grad);
                await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće ažurirati grad.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return p;
    }

    public async static Task<Result<GradView, ErrorMessage>> ReturnGradAsync(int id)
    {
        ISession? s = null;

        GradView gradView = default!;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            Grad o = await s.LoadAsync<Grad>(id);
            gradView = new GradView(o);
        }
        catch (Exception)
        {
            return "Nemoguće vratiti grad sa zadatim ID-jem.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return gradView;
    }

    public async static Task<Result<bool, ErrorMessage>> DeleteGradAsync(int id)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            Grad o = await s.LoadAsync<Grad>(id);

            await s.DeleteAsync(o);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće obrisati grad.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
}
