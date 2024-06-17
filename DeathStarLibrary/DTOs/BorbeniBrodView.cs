
namespace DeathStarLibrary.DTOs
{
    public class BorbeniBrodView : BrodView
    {
        public int BrojTopova { get; set; }
        public char PosedujeFotTorp { get; set; }
        public string? Tip { get; set; }

        public BorbeniBrodView() { }

        internal BorbeniBrodView(BorbeniBrod brod):base(brod)
        {
            if (brod != null)
            {
                BrojTopova = brod.BrojTopova;
                PosedujeFotTorp = brod.PosedujeFotTorp;
                Tip = brod.Tip;
            }
        }
    }
}
