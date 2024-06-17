using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathStarLibrary.Entiteti;

internal class Grad
{
    #region Properties
    internal protected virtual int GradID { get; set; }
    internal protected virtual string? Naziv { get; set; }
    internal protected virtual Planeta? Planeta { get; set; }
    #endregion
}

