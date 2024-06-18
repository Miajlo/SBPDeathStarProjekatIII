using DeathStarLibrary.Entiteti;

namespace DeathStarLibrary.DataProviders;

public static class SirenjePosedaDP
{
    public async static Task<Result<List<SirenjePosedaView>, ErrorMessage>> ReturnAllSirenjePoseda()
    {
        ISession? s = null;

        List<SirenjePosedaView> posedi = new();

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);


            posedi = (await s.QueryOver<SirenjePoseda>().ListAsync())
                       .Select(p => new SirenjePosedaView(p)).ToList();
        }
        catch (Exception)
        {
            return "Nemoguće vratiti sve posede.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return posedi;
    }

    public async static Task<Result<int, ErrorMessage>> AddSirenjePosedaAsync(SirenjePosedaView p)
    {
        ISession? s = null;
        int sPID = 0;
        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            Igrac? igrac = null;
            Savez? savez = null;

            if (p.PrethodniVlasnik != null && p.PrethodniVlasnik.IgracID > 0)
                igrac = await s.GetAsync<Igrac>(p!.PrethodniVlasnik!.IgracID);

            if (p.Savez != null && p.Savez.SavezID > 0)
                savez = await s.GetAsync<Savez>(p!.Savez!.SavezID);

            SirenjePoseda o = new()
            {
                Tip = p.Tip,
                Datum = p.Datum,
                PrethodniVlasnik=igrac,
                Savez=savez
            };

            await s.SaveOrUpdateAsync(o);
            sPID = o.SPID;
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return GetError("Nemoguće dodati posed.", 404);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return sPID;
    }

    public async static Task<Result<SirenjePosedaView, ErrorMessage>> UpdateSirenjePosedaAsync(SirenjePosedaView p)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

                SirenjePoseda posed = s.Load<SirenjePoseda>(p.SPID);
                if (posed == null)
                    return "Posed sa datim ID-om ne postoji.".ToError(404);
                posed.Tip = p.Tip;
                posed.Datum = p.Datum;
                await s.UpdateAsync(posed);
                await s.FlushAsync();
            }
            catch (Exception)
            {
                return "Nemoguće ažurirati posed.".ToError(400);
            }
            finally
            {
                s?.Close();
                s?.Dispose();
            }

        return p;
    }

    public async static Task<Result<SirenjePosedaView, ErrorMessage>> ReturnSirenjePosedaAsync(int id)
    {
        ISession? s = null;

        SirenjePosedaView sirenjePosedaView = default!;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            SirenjePoseda o = await s.LoadAsync<SirenjePoseda>(id);
            sirenjePosedaView = new SirenjePosedaView(o);
        }
        catch (Exception)
        {
            return "Nemoguće vratiti posed sa zadatim ID-jem.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return sirenjePosedaView;
    }

    public async static Task<Result<bool, ErrorMessage>> DeleteSirenjePosedaAsync(int id)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            SirenjePoseda o = await s.LoadAsync<SirenjePoseda>(id);

            await s.DeleteAsync(o);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće obrisati posed.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
}
