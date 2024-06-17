namespace DeathStarLibrary.DataProviders;

public static class SvemirskaStanicaDP
{
    public async static Task<Result<List<SvemirskaStanicaView>, ErrorMessage>> ReturnAllSvemirskaStanica()
    {
        ISession? s = null;

        List<SvemirskaStanicaView> sstanice = new();

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);


            sstanice = (await s.QueryOver<SvemirskaStanica>().ListAsync())
                      .Select(p => new SvemirskaStanicaView(p)).ToList();
        }
        catch (Exception)
        {
            return "Nemoguće vratiti sve svemirske stanice.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return sstanice;
    }

    public async static Task<Result<int, ErrorMessage>> AddSStanicaAsync(SvemirskaStanicaView ss)
    {
        ISession? s = null;
        int sSID = 0;
        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            SvemirskaStanica o = new()
            {
                Naziv = ss.Naziv,
                BrojLjudi = ss.BrojLjudi,
                Velicina = ss.Velicina,
                RastojanjeOP = ss.RastojanjeOP,
                Tip = ss.Tip,
                Namena = ss.Namena
            };

            await s.SaveOrUpdateAsync(o);
            sSID = o.SSID;
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return GetError("Nemoguće dodati svemirsku stanicu.", 404);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return sSID;
    }

    public async static Task<Result<SvemirskaStanicaView, ErrorMessage>> UpdateSvemirskaStanicaAsync(SvemirskaStanicaView ss)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

                SvemirskaStanica sstanica = s.Load<SvemirskaStanica>(ss.SSID);
                if (sstanica == null)
                    return "Svemirska stanica sa datim ID-om ne postoji.".ToError(404);
                sstanica.Naziv = ss.Naziv;
                sstanica.BrojLjudi = ss.BrojLjudi;
                sstanica.Velicina = ss.Velicina;
                sstanica.RastojanjeOP = ss.RastojanjeOP;
                sstanica.Tip = ss.Tip;
                sstanica.Namena = ss.Namena;
                await s.UpdateAsync(sstanica);
                await s.FlushAsync();
            }
            catch (Exception)
            {
                return "Nemoguće ažurirati planetu.".ToError(400);
            }
            finally
            {
                s?.Close();
                s?.Dispose();
            }

        return ss;
    }

    public async static Task<Result<SvemirskaStanicaView, ErrorMessage>> ReturnSvemirskaStanicaAsync(int id)
    {
        ISession? s = null;

        SvemirskaStanicaView sstanicaView = default!;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            SvemirskaStanica o = await s.LoadAsync<SvemirskaStanica>(id);
            sstanicaView = new SvemirskaStanicaView(o);
        }
        catch (Exception)
        {
            return "Nemoguće vratiti svemirsku stanicu sa zadatim ID-jem.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return sstanicaView;
    }

    public async static Task<Result<bool, ErrorMessage>> DeleteSvemirskaStanicaAsync(int id)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            SvemirskaStanica o = await s.LoadAsync<SvemirskaStanica>(id);

            await s.DeleteAsync(o);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće obrisati svemirsku stanicu.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
}
