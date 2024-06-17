namespace DeathStarLibrary.DTOs;
public class SavezView
{
    public int SavezID { get; set; }
    public string? Naziv { get; set; }
    public DateTime DatumFormiranja { get; set; }
    public virtual SavezView? NadSavez { get; set; }
    public virtual IList<IgracView>? IgraciSaveza { get; set; }
    public virtual IList<BrodView>? BrodoviSaveza { get; set; }
    public virtual IList<SirenjePosedaView>? SirenjePoseda { get; set; }

    public SavezView()
    {
        IgraciSaveza = new List<IgracView>();
        BrodoviSaveza = new List<BrodView>();
        SirenjePoseda = new List<SirenjePosedaView>();
    }

    internal SavezView(Savez savez):this()
    {
        if(savez != null)
        {
            SavezID = savez.SavezID;
            Naziv = savez.Naziv;
            DatumFormiranja = savez.DatumFormiranja;
        }
    }
}
