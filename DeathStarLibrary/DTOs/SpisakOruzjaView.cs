namespace DeathStarLibrary.DTOs;

public class SpisakOruzjaView
{
    public int SOID { get; set; }
    public SvemirskaStanicaView? SvemirskaStanica { get; set; }
    public string? Oruzje { get; set; }

    public SpisakOruzjaView() { }

    internal SpisakOruzjaView(SpisakOruzja? s)
    {
        if (s != null)
        {
            SOID = s.SOID;
            Oruzje = s.Oruzje;
        }
    }
}
