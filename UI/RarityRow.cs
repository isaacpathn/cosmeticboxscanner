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
            string spriteName,
            out TextMeshProUGUI textComponent,
            int index,
            int rowSpacing = 65
        )
        {
            GameObject row = new GameObject(spriteName);

            row.transform.SetParent(parent, false);

            RectTransform rowRect =
                row.AddComponent<RectTransform>();

            rowRect.anchorMin = new Vector2(0, 1);
            rowRect.anchorMax = new Vector2(1, 1);

            rowRect.pivot = new Vector2(0.5f, 1);

            rowRect.anchoredPosition =
                new Vector2(0, -15 - (index * rowSpacing));

            rowRect.sizeDelta = new Vector2(0, 36);

            //
            // ICON
            //

            GameObject iconObj = new GameObject("Icon");

            iconObj.transform.SetParent(row.transform, false);

            Image icon = iconObj.AddComponent<Image>();

            icon.sprite = AssetLoader.Load(spriteName);

            RectTransform iconRect =
                icon.GetComponent<RectTransform>();

            iconRect.anchorMin = new Vector2(0, 0.5f);
            iconRect.anchorMax = new Vector2(0, 0.5f);

            iconRect.pivot = new Vector2(0, 0.5f);

            iconRect.anchoredPosition = new Vector2(15, 0);

            iconRect.sizeDelta = new Vector2(55, 55);

            //
            // TEXT
            //

            GameObject textObj = new GameObject("Count");

            textObj.transform.SetParent(row.transform, false);

            textComponent =
                textObj.AddComponent<TextMeshProUGUI>();

            textComponent.fontSize = 24;

            textComponent.color = Color.white;

            textComponent.enableWordWrapping = false;

            RectTransform textRect =
                textComponent.GetComponent<RectTransform>();

            textRect.anchorMin = new Vector2(0, 0);
            textRect.anchorMax = new Vector2(1, 1);

            textRect.offsetMin = new Vector2(85, 0);

            textRect.offsetMax = Vector2.zero;

            return row;
        }
    }
}