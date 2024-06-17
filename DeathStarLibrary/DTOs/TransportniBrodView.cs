namespace DeathStarLibrary.DTOs;

public class TransportniBrodView : BrodView
{
    public double Nosivost { get; set; }
    public char Zastita { get; set; }

    public TransportniBrodView() { }

    internal TransportniBrodView(TransportniBrod? t): base(t)
    {
        if (t != null)
        {
            Nosivost = t.Nosivost;
            Zastita = t.Zastita;
        }
    }
}
