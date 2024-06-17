namespace DeathStarLibrary.DataProviders;

public static class ZvezdaDP
{
    public async static Task<Result<List<ZvezdaView>, ErrorMessage>> ReturnAllZvezda()
    {
        ISession? s = null;

        List<ZvezdaView> zvezde = new();

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            zvezde = (await s.QueryOver<Zvezda>().ListAsync())
                          .Select(p => new ZvezdaView(p)).ToList();

        }
        catch (Exception)
        {
            return "Nemoguće vratiti sve zvezde.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return zvezde;
    }

    public async static Task<Result<int, ErrorMessage>> AddZvezdaAsync(ZvezdaView p)
    {
        ISession? s = null;
        int zvezdaID = 0;
        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            Zvezda o = new()
            {
                Naziv = p.Naziv,
                Tip = p.Tip
            };

            await s.SaveOrUpdateAsync(o);
            zvezdaID = o.ZvezdaID;
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return GetError("Nemoguće dodati zvezdu.", 404);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return zvezdaID;
    }

    public async static Task<Result<ZvezdaView, ErrorMessage>> UpdateZvezdaAsync(ZvezdaView p)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            Zvezda zvezda = s.Load<Zvezda>(p.ZvezdaID);

            if (zvezda == null)
                return "Zvezda sa datim ID-om ne postoji.".ToError(404);

            zvezda.Naziv = p.Naziv;
            zvezda.Tip = p.Tip;
            await s.UpdateAsync(zvezda);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće ažurirati zvezdu.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return p;
    }

    public async static Task<Result<ZvezdaView, ErrorMessage>> ReturnZvezdaAsync(int id)
    {
        ISession? s = null;

        ZvezdaView zvezdaView = default!;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            Zvezda o = await s.LoadAsync<Zvezda>(id);
            zvezdaView = new ZvezdaView(o);
        }
        catch (Exception)
        {
            return "Nemoguće vratiti zvezdu sa zadatim ID-jem.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return zvezdaView;
    }

    public async static Task<Result<bool, ErrorMessage>> DeleteZvezdaAsync(int id)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            Zvezda o = await s.LoadAsync<Zvezda>(id);

            await s.DeleteAsync(o);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće obrisati zvezdu.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
}
