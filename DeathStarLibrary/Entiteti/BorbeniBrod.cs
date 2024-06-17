using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathStarLibrary.Entiteti;

internal class BorbeniBrod : Brod
{
    #region Properties
    internal protected virtual int BrojTopova { get; set; }
    internal protected virtual char PosedujeFotTorp { get; set; }
    internal protected virtual string? Tip { get; set; }
    #endregion
}
