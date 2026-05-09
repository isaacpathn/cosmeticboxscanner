using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cosmetics_Box_Scanner.Models;

namespace Cosmetics_Box_Scanner.UI
{
    public class ScannerUI
    {
        private GameObject canvasObject;
        private GameObject panelObject;
        private GameObject commonRow;
        private GameObject uncommonRow;
        private GameObject rareRow;
        private GameObject ultraRow;
        private TextMeshProUGUI commonText;
        private TextMeshProUGUI uncommonText;
        private TextMeshProUGUI rareText;
        private TextMeshProUGUI ultraText;
        private TextMeshProUGUI statusText;
        private float statusTimer;


        public void Create()
        {
            if (canvasObject != null)
                return;

            canvasObject = new GameObject("CosmeticScannerCanvas");

            Object.DontDestroyOnLoad(canvasObject);

            Canvas canvas = canvasObject.AddComponent<Canvas>();

            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 9999;

            canvasObject.AddComponent<CanvasScaler>();
            canvasObject.AddComponent<GraphicRaycaster>();

            //
            // PANEL
            //

            panelObject = new GameObject("Panel");

            panelObject.transform.SetParent(canvasObject.transform, false);

            Image panelImage = panelObject.AddComponent<Image>();

            panelImage.color = new Color(0f, 0f, 0f, 0.45f);

            RectTransform panelRect =
                panelObject.GetComponent<RectTransform>();

            panelRect.anchorMin = new Vector2(1, 0);
            panelRect.anchorMax = new Vector2(1, 0);

            panelRect.pivot = new Vector2(1, 0);

            panelRect.anchoredPosition = new Vector2(-20, 20);

            panelRect.sizeDelta = new Vector2(240, 320);

            //
            // RARITY ROWS
            //

            commonRow = RarityRowFactory.CreateRarityRow(
                panelObject.transform,
                "transparent_bg_common.png",
                out commonText,
                0
            );

            uncommonRow = RarityRowFactory.CreateRarityRow(
                panelObject.transform,
                "transparent_bg_uncommon.png",
                out uncommonText,
                1
            );

            rareRow = RarityRowFactory.CreateRarityRow(
                panelObject.transform,
                "transparent_bg_rare.png",
                out rareText,
                2
            );

            ultraRow = RarityRowFactory.CreateRarityRow(
                panelObject.transform,
                "transparent_bg_ultrarare.png",
                out ultraText,
                3
            );

            //
            // STATUS TEXT
            //

            GameObject statusObj = new GameObject("StatusText");

            statusObj.transform.SetParent(canvasObject.transform, false);

            statusText = statusObj.AddComponent<TextMeshProUGUI>();

            statusText.fontSize = 28;
            statusText.color = Color.white;
            statusText.alignment = TextAlignmentOptions.Center;

            RectTransform statusRect =
                statusText.GetComponent<RectTransform>();

            statusRect.anchorMin = new Vector2(0.5f, 0.1f);
            statusRect.anchorMax = new Vector2(0.5f, 0.1f);

            statusRect.pivot = new Vector2(0.5f, 0.5f);

            statusRect.sizeDelta = new Vector2(600, 80);

            statusText.gameObject.SetActive(false);
            canvasObject.SetActive(true);
            panelObject.SetActive(false);
        }
        public void Refresh(CosmeticStats stats, bool visible)
        {
            if (commonText == null)
                return;

            //
            // ROW VISIBILITY
            //

            commonRow.SetActive(stats.Common > 0);

            uncommonRow.SetActive(stats.Uncommon > 0);

            rareRow.SetActive(stats.Rare > 0);

            ultraRow.SetActive(stats.UltraRare > 0);

            //
            // TEXT UPDATE
            //

            commonText.text = stats.Common.ToString();

            uncommonText.text = stats.Uncommon.ToString();

            rareText.text = stats.Rare.ToString();

            ultraText.text = stats.UltraRare.ToString();

            //
            // PANEL VISIBILITY
            //

            bool hasAny =
                stats.Common > 0 ||
                stats.Uncommon > 0 ||
                stats.Rare > 0 ||
                stats.UltraRare > 0;

            if (panelObject != null)
            {
                panelObject.SetActive(visible && hasAny);
            }
        }
        public void ShowStatus(string message)
        {
            if (statusText == null)
                return;

            statusText.text = message;

            statusText.gameObject.SetActive(true);

            statusTimer = 2f;
        }
        public void Tick()
        {
            if (statusTimer > 0f)
            {
                statusTimer -= Time.deltaTime;

                if (statusTimer <= 0f)
                {
                    statusText.gameObject.SetActive(false);
                }
            }
        }
        public void SetVisible(bool visible)
        {
            if (panelObject != null)
            {
                panelObject.SetActive(visible);
            }

            if (!visible && statusText != null)
            {
                statusText.gameObject.SetActive(false);
            }
        }
    }
}