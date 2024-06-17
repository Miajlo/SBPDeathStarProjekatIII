using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathStarLibrary.Entiteti;

internal class Galaksija
{
    #region Properties
    public virtual int GalaksijaID { get; set; }
    public virtual string? Naziv { get; set; }
    public virtual long BrojZvezda { get; set; }
    public virtual long BrojPlaneta { get; set; }
    public virtual Rasa? DominantnaRasa { get; set; }
    public virtual IList<Planeta>? Planete { get; protected set; }
    public virtual IList<Kvadrant>? Kvadranti { get; protected set; }

    #endregion
    public Galaksija()
    {
        Planete = new List<Planeta>();
        Kvadranti = new List<Kvadrant>();
    }
}
