namespace DeathStarLibrary.DataProviders;

public static class IgracDP
{
    public async static Task<Result<List<IgracView>, ErrorMessage>> ReturnAllIgrac()
    {
        ISession? s = null;

        List<IgracView> igraci = new();

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            igraci = (await s.QueryOver<Igrac>().ListAsync())
                         .Select(p => new IgracView(p)).ToList();
        }
        catch (Exception)
        {
            return "Nemoguće vratiti sve igrace.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return igraci;
    }

    public async static Task<Result<int, ErrorMessage>> AddIgracAsync(IgracView p)
    {
        ISession? s = null;
        int igracID = 0;
        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            Savez savez = new()
            {
                Naziv=$"{p.LIme}Savez",
                DatumFormiranja = DateTime.Now,
            };

            await s.SaveAsync(savez);
            await s.FlushAsync();
            Igrac o = new()
            {
                KorisnickoIme = p.KorisnickoIme,
                DatumOtvaranjaNaloga = p.DatumOtvaranjaNaloga,
                LIme = p.LIme,
                Prezime = p.Prezime,
                SSlovo = p.SSlovo,
                Email = p.Email,
                Pol = p.Pol,
                DatumRodjenja = p.DatumRodjenja,
                Drzava = p.Drzava,
                Opis = p.Opis,
                Slika = p.Slika,
                Savez = savez,
            };

            await s.SaveOrUpdateAsync(o);
            igracID = o.IgracID;
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return GetError("Nemoguće dodati igraca.", 404);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return igracID;
    }

    public async static Task<Result<IgracView, ErrorMessage>> UpdateIgracAsync(IgracView p)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            Igrac igrac = s.Load<Igrac>(p.IgracID);
            if (igrac == null)
                return "Ne postoji trazeni igrac".ToError(404);

            if(p!.Savez != null && p!.Savez!.SavezID != igrac!.Savez!.SavezID)
            {
                var savez = await s.GetAsync<Savez>(p!.Savez!.SavezID);
                if (savez == null)
                    return "Ne postoji trazeni savez".ToError(404);
                igrac.Savez = savez;
            }

            igrac.KorisnickoIme = p.KorisnickoIme;
            igrac.DatumOtvaranjaNaloga = p.DatumOtvaranjaNaloga;
            igrac.LIme = p.LIme;
            igrac.Prezime = p.Prezime;
            igrac.SSlovo = p.SSlovo;
            igrac.Email = p.Email;
            igrac.Pol = p.Pol;
            igrac.DatumRodjenja = p.DatumRodjenja;
            igrac.Drzava = p.Drzava;
            igrac.Opis = p.Opis;
            igrac.Slika = p.Slika;
            await s.UpdateAsync(igrac);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće ažurirati igraca.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return p;
    }

    public async static Task<Result<IgracView, ErrorMessage>> ReturnIgracAsync(int id)
    {
        ISession? s = null;

        IgracView igracView = default!;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            Igrac o = await s.LoadAsync<Igrac>(id);
            igracView = new IgracView(o);
        }
        catch (Exception)
        {
            return "Nemoguće vratiti igraca sa zadatim ID-jem.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return igracView;
    }

    public async static Task<Result<bool, ErrorMessage>> DeleteIgracAsync(int id)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            Igrac o = await s.LoadAsync<Igrac>(id);

            await s.DeleteAsync(o);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće obrisati igraca.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
}
