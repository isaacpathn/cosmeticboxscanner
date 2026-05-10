using Cosmetics_Box_Scanner.Systems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cosmetics_Box_Scanner.UI
{
    class RarityRowFactory
    {
        public static GameObject CreateRarityRow(
            Transform parent,
            SemiFunc.Rarity rarity,
            out TextMeshProUGUI textComponent,
            int index
        )
        {
            //
            // ROW ROOT
            //

            GameObject row =
                new GameObject(
                    rarity.ToString() + "Row"
                );

            row.transform.SetParent(
                parent,
                false
            );

            RectTransform rowRect =
                row.AddComponent<RectTransform>();

            rowRect.anchorMin =
                new Vector2(0f, 1f);

            rowRect.anchorMax =
                new Vector2(1f, 1f);

            rowRect.pivot =
                new Vector2(0.5f, 1f);

            rowRect.sizeDelta =
                new Vector2(0f, 40f);

            //
            // ICON
            //

            CosmeticTokenUIElement prefab =
                GameAssets.GetTokenPrefab();

            if (prefab != null)
            {
                GameObject iconObj =
                    Object.Instantiate(
                        prefab.gameObject,
                        row.transform
                    );

                iconObj.name = "Icon";

                //
                // Disable ALL behaviours
                //

                CosmeticTokenUIElement element = iconObj.GetComponent<CosmeticTokenUIElement>();

                if (element != null)
                {
                    element.enabled = false;
                }

                //
                // Position
                //

                RectTransform iconRect =
                    iconObj.GetComponent<
                        RectTransform>();

                iconRect.anchorMin =
                    new Vector2(0f, 0.5f);

                iconRect.anchorMax =
                    new Vector2(0f, 0.5f);

                iconRect.pivot =
                    new Vector2(0f, 0.5f);

                iconRect.anchoredPosition =
                    new Vector2(18f, 0f);

                //
                // Scale down
                //

                iconRect.localScale =
                    Vector3.one * 0.75f;

                //
                // Colorize
                //

                RawImage rawImage =
                    iconObj.GetComponentInChildren<
                        RawImage>(true);

                if (rawImage != null)
                {
                    rawImage.color =
                        GameAssets.GetRarityColor(
                            rarity
                        );
                }

                //
                // Hide unnecessary visuals
                //

                Graphic[] graphics =
                    iconObj.GetComponentsInChildren<
                        Graphic>(true);

                foreach (Graphic graphic in graphics)
                {
                    if (
                        graphic.name.Contains("Blink") ||
                        graphic.name.Contains("Sheen")
                    )
                    {
                        graphic.gameObject.SetActive(
                            false
                        );
                    }
                }
            }

            //
            // TEXT
            //

            GameObject textObj =
                new GameObject("Count");

            textObj.transform.SetParent(
                row.transform,
                false
            );

            textComponent =
                textObj.AddComponent<
                    TextMeshProUGUI>();

            textComponent.fontSize = 18;

            textComponent.color =
                Color.white;

            textComponent.alignment =
                TextAlignmentOptions.Left;

            textComponent.enableWordWrapping =
                false;

            textComponent.text = "0";

            RectTransform textRect =
                textComponent.GetComponent<
                    RectTransform>();

            textRect.anchorMin =
                new Vector2(0f, 0f);

            textRect.anchorMax =
                new Vector2(1f, 1f);

            textRect.offsetMin =
                new Vector2(68f, 0f);

            textRect.offsetMax =
                Vector2.zero;

            return row;
        }
    }
}