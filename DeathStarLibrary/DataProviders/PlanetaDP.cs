using DeathStarLibrary.Entiteti;

namespace DeathStarLibrary.DataProviders;

public static class PlanetaDP
{
    public async static Task<Result<List<PlanetaView>, ErrorMessage>> ReturnAllPlaneta()
    {
        ISession? s = null;

        List<PlanetaView> planete = new();

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            planete = (await s.QueryOver<Planeta>().ListAsync())
                        .Select(p => new PlanetaView(p)).ToList();
        }
        catch (Exception)
        {
            return "Nemoguće vratiti sve planete.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return planete;
    }

    public async static Task<Result<int, ErrorMessage>> AddPlanetaAsync(PlanetaView p)
    {
        ISession? s = null;
        int planetaID = 0;
        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            Igrac? rasa = null;
            Galaksija? galaksija = null;
            Rasa? domRasa = null;
            Grad? glavniGrad = null;
            
            if (p.Vlasnik != null)
                rasa = await s.GetAsync<Igrac>(p!.Vlasnik!.IgracID!);
            
            if (p!.Galaksija! != null)
                galaksija = await s.GetAsync<Galaksija>(p!.Galaksija!.GalaksijaID);
            
            if (p!.DominantnaRasa! != null)
                domRasa = await s.GetAsync<Rasa>(p!.DominantnaRasa!.RasaID);
            
            if (p!.GlavniGrad! != null)
                glavniGrad = await s.GetAsync<Grad>(p!.GlavniGrad!.GradID);


            Planeta o = new()
            {
                Naziv = p.Naziv,
                X = p.X,
                Y = p.Y,
                Z = p.Z,
                BrojStanovnika = p.BrojStanovnika,
                DrustvenoUredjenje = p.DrustvenoUredjenje,
                PlutonijumKol = p.PlutonijumKol,
                BerilijumKol = p.BerilijumKol,
                TrilijumKol = p.TrilijumKol,
                JeMaticna = p.JeMaticna,
                Vlasnik = rasa,
                DominantnaRasa = domRasa,
                Galaksija = galaksija,
                GlavniGrad = glavniGrad,
            };

            await s.SaveOrUpdateAsync(o);
            planetaID = o.PlanetaID;
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return GetError("Nemoguće dodati planetu.", 404);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return planetaID;
    }

    public async static Task<Result<PlanetaView, ErrorMessage>> UpdatePlanetaAsync(PlanetaView p)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            Planeta planeta = s.Load<Planeta>(p.PlanetaID);

            if (planeta == null)
                return "Ne postoji trazena planeta".ToError(404);

            if (p!.Vlasnik!= null && p!.Vlasnik!.IgracID! != planeta!.Vlasnik!.IgracID)
            {
                var igrac = await s.GetAsync<Igrac>(p!.Vlasnik!.IgracID);
                if (igrac == null)
                    return "Ne postoji trazen igrac".ToError(400);
                planeta.Vlasnik = igrac;
            }

            if (p!.Galaksija!= null && p!.Galaksija!.GalaksijaID! != planeta!.Galaksija!.GalaksijaID)
            {
                var galaksija = await s.GetAsync<Galaksija>(p!.Galaksija!.GalaksijaID);
                if (galaksija == null)
                    return "Ne postoji trazena galaksija".ToError(400);
                planeta.Galaksija = galaksija;
            }

            if (p!.DominantnaRasa!= null 
                && p!.DominantnaRasa!.RasaID! != planeta!.DominantnaRasa!.RasaID)
            {
                var rasa = await s.GetAsync<Rasa>(p!.DominantnaRasa!.RasaID);
                if (rasa == null)
                    return "Ne postoji trazena rasa".ToError(400);
                planeta.DominantnaRasa = rasa;
            }

            if(p!.GlavniGrad != null && p!.GlavniGrad!.GradID != planeta!.GlavniGrad!.GradID)
            {
                var grad = await s.GetAsync<Grad>(p!.GlavniGrad!.GradID);
                if (grad == null)
                    return "Ne postoji trazeni grad".ToError(404);
                planeta.GlavniGrad = grad;
            }
            planeta.Naziv = p.Naziv;
            planeta.X = p.X;
            planeta.Y = p.Y;
            planeta.Z = p.Z;
            planeta.BrojStanovnika = p.BrojStanovnika;
            planeta.DrustvenoUredjenje = p.DrustvenoUredjenje;
            planeta.PlutonijumKol = p.PlutonijumKol;
            planeta.BerilijumKol = p.BerilijumKol;
            planeta.TrilijumKol = p.TrilijumKol;
            planeta.JeMaticna = p.JeMaticna;
            await s.UpdateAsync(planeta);
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

        return p;
    }

    public async static Task<Result<PlanetaView, ErrorMessage>> ReturnPlanetaAsync(int id)
    {
        ISession? s = null;

        PlanetaView planetaView = default!;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            Planeta o = await s.LoadAsync<Planeta>(id);
            planetaView = new PlanetaView(o);
        }
        catch (Exception)
        {
            return "Nemoguće vratiti planetu sa zadatim ID-jem.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return planetaView;
    }

    public async static Task<Result<bool, ErrorMessage>> DeletePlanetaAsync(int id)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
                return "Nemoguće otvoriti sesiju.".ToError(403);

            Planeta o = await s.LoadAsync<Planeta>(id);

            await s.DeleteAsync(o);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće obrisati planetu.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
}
