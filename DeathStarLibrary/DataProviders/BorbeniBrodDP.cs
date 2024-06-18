namespace DeathStarLibrary.DataProviders;

public static class BorbeniBrodDP
{
    public async static Task<Result<List<BorbeniBrodView>, ErrorMessage>> ReturnAllBBrod()
    {
        ISession? s = null;

        List<BorbeniBrodView> bbrodovi = new();

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);


            bbrodovi = (await s.QueryOver<BorbeniBrod>().ListAsync())
                      .Select(p => new BorbeniBrodView(p)).ToList();

            foreach (var bb in bbrodovi)
            {
                var brod = await s.GetAsync<Brod>(bb.BrodID);
                if(brod != null)
                {
                    bb.Naziv = brod.Naziv;
                    bb.Savez = new(brod!.Savez!);
                    bb.Planeta = new(brod!.Planeta!);
                    bb.MaxBrzina = brod.MaxBrzina;
                }
            }
        }
        catch (Exception)
        {
            return "Nemoguće vratiti sve borbene brodove.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return bbrodovi;
    }

    public async static Task<Result<int, ErrorMessage>> AddBBrodAsync(BorbeniBrodView p)
    {
        ISession? s = null;
        int bBrodID = 0;
        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            Brod brod = new()
            {
                Naziv = p.Naziv,
                MaxBrzina = p.MaxBrzina,
                Planeta = await s.GetAsync<Planeta>(p!.Planeta!.PlanetaID),
                Savez = await s.GetAsync<Savez>(p!.Savez!.SavezID),
            };

            int brodId = (int)await s.SaveAsync(brod);

            BorbeniBrod o = new()
            {
                BrodID = brodId,
                Naziv = p.Naziv,
                MaxBrzina = p.MaxBrzina,
                BrojTopova = p.BrojTopova,
                PosedujeFotTorp = p.PosedujeFotTorp,
                Tip = p.Tip
            };

            await s.SaveOrUpdateAsync(o);
            bBrodID = o.BrodID;
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

        return bBrodID;
    }

    public async static Task<Result<BorbeniBrodView, ErrorMessage>> UpdateBBrodAsync(BorbeniBrodView p)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

                BorbeniBrod brod = s.Load<BorbeniBrod>(p.BrodID);

                if (brod == null)
                    return "Borbeni brod sa datim ID-om ne postoji.".ToError(404); 
                brod.Naziv = p.Naziv;
                brod.MaxBrzina = p.MaxBrzina;
                brod.BrojTopova = p.BrojTopova;
                brod.PosedujeFotTorp = p.PosedujeFotTorp;
                brod.Tip = p.Tip;
                await s.UpdateAsync(brod);
                await s.FlushAsync();
            }
            catch (Exception)
            {
                return "Nemoguće ažurirati borbeni brod.".ToError(400);
            }
            finally
            {
                s?.Close();
                s?.Dispose();
            }

        return p;
    }

    public async static Task<Result<BorbeniBrodView, ErrorMessage>> ReturnBBrodAsync(int id)
    {
        ISession? s = null;

        BorbeniBrodView bbrodView = default!;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            BorbeniBrod o = await s.LoadAsync<BorbeniBrod>(id);
            bbrodView = new BorbeniBrodView(o);
            var brod = await s.GetAsync<Brod>(bbrodView.BrodID);
            bbrodView.Savez = new(brod!.Savez!);
            bbrodView.Planeta = new(brod!.Planeta!);
        }
        catch (Exception)
        {
            return "Nemoguće vratiti borbeni brod sa zadatim ID-jem.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return bbrodView;
    }

    public async static Task<Result<bool, ErrorMessage>> DeleteBBrodAsync(int id)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            Brod brod = await s.LoadAsync<Brod>(id);
            await s.DeleteAsync(brod);

            BorbeniBrod o = await s.LoadAsync<BorbeniBrod>(id);

            await s.DeleteAsync(o);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće obrisati borbeni brod.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
}

