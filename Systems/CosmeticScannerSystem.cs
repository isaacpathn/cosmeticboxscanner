using Cosmetics_Box_Scanner.Models;
using UnityEngine;

namespace Cosmetics_Box_Scanner.Systems
{
    public static class CosmeticScannerSystem
    {
        public static CosmeticStats Stats =
            new CosmeticStats();

        public static void Scan()
        {
            var cosmetics =
                Object.FindObjectsOfType<CosmeticWorldObject>();

            Stats.Common = 0;
            Stats.Uncommon = 0;
            Stats.Rare = 0;
            Stats.UltraRare = 0;

            foreach (var cosmetic in cosmetics)
            {
                switch (cosmetic.rarity)
                {
                    case SemiFunc.Rarity.Common:
                        Stats.Common++;
                        break;

                    case SemiFunc.Rarity.Uncommon:
                        Stats.Uncommon++;
                        break;

                    case SemiFunc.Rarity.Rare:
                        Stats.Rare++;
                        break;

                    case SemiFunc.Rarity.UltraRare:
                        Stats.UltraRare++;
                        break;
                }
            }
        }
    }
}