namespace DeathStarLibrary.DataProviders;
public static class SpisakOruzjaDP
{
    public async static Task<Result<List<SpisakOruzjaView>, ErrorMessage>> ReturnAllSpisakOruzja()
    {
        ISession? s = null;

        List<SpisakOruzjaView> spisakOruzja = new();

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);


            spisakOruzja = (await s.QueryOver<SpisakOruzja>().ListAsync())
                      .Select(p => new SpisakOruzjaView(p)).ToList();
        }
        catch (Exception)
        {
            return "Nemoguće vratiti sve spiskove oruzja.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return spisakOruzja;
    }

    public async static Task<Result<int, ErrorMessage>> AddSpisakOruzjaAsync(SpisakOruzjaView p)
    {
        ISession? s = null;
        int sOID = 0;
        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            SpisakOruzja o = new()
            {
                Oruzje = p.Oruzje
            };

            await s.SaveOrUpdateAsync(o);
            sOID = o.SOID;
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return GetError("Nemoguće dodati spisak oruzja.", 404);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return sOID;
    }

    public async static Task<Result<SpisakOruzjaView, ErrorMessage>> UpdateSpisakOruzjaAsync(SpisakOruzjaView p)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

                SpisakOruzja spisakOruzja = s.Load<SpisakOruzja>(p.SOID);
                if (spisakOruzja == null)
                    return "Spisak oruzja sa datim ID-om ne postoji.".ToError(404);
                spisakOruzja.Oruzje = p.Oruzje;
                await s.UpdateAsync(spisakOruzja);
                await s.FlushAsync();
            }
            catch (Exception)
            {
                return "Nemoguće ažurirati spisak oruzja.".ToError(400);
            }
            finally
            {
                s?.Close();
                s?.Dispose();
            }

        return p;
    }

    public async static Task<Result<SpisakOruzjaView, ErrorMessage>> ReturnSpisakOruzjaAsync(int id)
    {
        ISession? s = null;

        SpisakOruzjaView spisakOruzjaView = default!;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            SpisakOruzja o = await s.LoadAsync<SpisakOruzja>(id);
            spisakOruzjaView = new SpisakOruzjaView(o);
        }
        catch (Exception)
        {
            return "Nemoguće vratiti spisak oruzja sa zadatim ID-jem.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return spisakOruzjaView;
    }

    public async static Task<Result<bool, ErrorMessage>> DeleteSpisakOruzjaAsync(int id)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            SpisakOruzja o = await s.LoadAsync<SpisakOruzja>(id);

            await s.DeleteAsync(o);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće obrisati spisak oruzja.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
}
