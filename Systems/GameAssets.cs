using UnityEngine;
using UnityEngine.UI;

namespace Cosmetics_Box_Scanner.Systems
{
    public static class GameAssets
    {
        public static CosmeticTokenUIElement GetTokenPrefab()
        {
            CosmeticTokenUI ui =
                Object.FindObjectOfType<CosmeticTokenUI>(true);

            if (ui == null)
            {
                Plugin.Log.LogError(
                    "TOKEN UI NOT FOUND"
                );

                return null;
            }

            return ui.tokenPrefab
                .GetComponent<CosmeticTokenUIElement>();
        }

        public static Color GetRarityColor(
            SemiFunc.Rarity rarity)
        {
            switch (rarity)
            {
                case SemiFunc.Rarity.Common:
                    return new Color32(
                        0, 255, 2, 255
                    );

                case SemiFunc.Rarity.Uncommon:
                    return new Color32(
                        0, 195, 255, 255
                    );

                case SemiFunc.Rarity.Rare:
                    return new Color32(
                        255, 110, 244, 255
                    );

                case SemiFunc.Rarity.UltraRare:
                    return new Color32(
                        255, 199, 0, 255
                    );

                default:
                    return Color.white;
            }
        }
    }
}