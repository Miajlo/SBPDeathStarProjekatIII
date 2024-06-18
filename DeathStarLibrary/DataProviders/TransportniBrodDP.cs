using DeathStarLibrary.DTOs;

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

            foreach (var tb in tbrodovi)
            {
                var brod = await s.GetAsync<Brod>(tb.BrodID);
                if (brod != null)
                {
                    tb.Naziv = brod.Naziv;
                    tb.Savez = new(brod!.Savez!);
                    tb.Planeta = new(brod!.Planeta!);
                    tb.MaxBrzina = brod.MaxBrzina;
                }
            }

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
            Brod brod = new()
            {
                Naziv = p!.Naziv,
                MaxBrzina = p.MaxBrzina,
                Planeta = await s.GetAsync<Planeta>(p!.Planeta!.PlanetaID),
                Savez = await s.GetAsync<Savez>(p!.Savez!.SavezID),
               
            };

            int tbID = (int)await s.SaveAsync(brod);           

            TransportniBrod o = new()
            {
                BrodID = tbID,
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
            Brod b = await s.LoadAsync<Brod>(p.BrodID);

            if (b == null)
                return "Trazeni brod ne postoji!".ToError(404);

            TransportniBrod brod = s.Load<TransportniBrod>(p.BrodID);

            if (brod == null)
                return "Borbeni brod sa datim ID-om ne postoji.".ToError(404);


            if (b.Savez != null && b!.Savez!.SavezID != p!.Savez!.SavezID)
                b.Savez = await s.GetAsync<Savez>(p!.Savez!.SavezID);

            if (b.Planeta != null && b!.Planeta!.PlanetaID != p!.Planeta!.PlanetaID)
                b.Planeta = await s.GetAsync<Planeta>(p!.Planeta!.PlanetaID);

            await s.SaveAsync(b);
           
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

            var o = await s.LoadAsync<TransportniBrod>(id);
            tbrodView = new TransportniBrodView(o);
            var brod = await s.GetAsync<Brod>(tbrodView.BrodID);
            tbrodView.Naziv = brod.Naziv;
            tbrodView.MaxBrzina = brod.MaxBrzina;
            tbrodView.Savez = new(brod!.Savez!);
            tbrodView.Planeta = new(brod!.Planeta!);
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

            Brod brod = await s.LoadAsync<Brod>(id);

            if (brod == null)
                return "Ne postoji trazeni brod".ToError(404);

            await s.DeleteAsync(brod);


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

