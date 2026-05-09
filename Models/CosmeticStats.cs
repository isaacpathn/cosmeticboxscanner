namespace Cosmetics_Box_Scanner.Models
{
    public class CosmeticStats
    {
        public int Common;
        public int Uncommon;
        public int Rare;
        public int UltraRare;

        public static CosmeticStats CreateDebugStats()
        {
            return new CosmeticStats
            {
                Common = 3,
                Uncommon = 2,
                Rare = 3,
                UltraRare = 1
            };
        }
    }
}