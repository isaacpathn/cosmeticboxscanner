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

            //
            // CANVAS
            //

            canvasObject =
                new GameObject(
                    "CosmeticScannerCanvas"
                );

            Object.DontDestroyOnLoad(
                canvasObject
            );

            Canvas canvas =
                canvasObject.AddComponent<
                    Canvas>();

            canvas.renderMode =
                RenderMode.ScreenSpaceOverlay;

            canvas.sortingOrder = 9999;

            CanvasScaler scaler =
                canvasObject.AddComponent<
                    CanvasScaler>();

            scaler.uiScaleMode =
                CanvasScaler.ScaleMode
                    .ScaleWithScreenSize;

            scaler.referenceResolution =
                new Vector2(1920, 1080);

            canvasObject.AddComponent<
                GraphicRaycaster>();

            //
            // PANEL CONTAINER
            //

            panelObject =
                new GameObject("Panel");

            panelObject.transform.SetParent(
                canvasObject.transform,
                false
            );

            RectTransform panelRect =
                panelObject.AddComponent<
                    RectTransform>();

            panelRect.anchorMin =
                new Vector2(1f, 0f);

            panelRect.anchorMax =
                new Vector2(1f, 0f);

            panelRect.pivot =
                new Vector2(1f, 0f);

            panelRect.anchoredPosition =
                new Vector2(-35f, 35f);

            //
            // ROWS
            //

            commonRow =
                RarityRowFactory.CreateRarityRow(
                    panelObject.transform,
                    SemiFunc.Rarity.Common,
                    out commonText,
                    0
                );

            uncommonRow =
                RarityRowFactory.CreateRarityRow(
                    panelObject.transform,
                    SemiFunc.Rarity.Uncommon,
                    out uncommonText,
                    1
                );

            rareRow =
                RarityRowFactory.CreateRarityRow(
                    panelObject.transform,
                    SemiFunc.Rarity.Rare,
                    out rareText,
                    2
                );

            ultraRow =
                RarityRowFactory.CreateRarityRow(
                    panelObject.transform,
                    SemiFunc.Rarity.UltraRare,
                    out ultraText,
                    3
                );

            //
            // STATUS TEXT
            //

            GameObject statusObj =
                new GameObject("StatusText");

            statusObj.transform.SetParent(
                canvasObject.transform,
                false
            );

            statusText =
                statusObj.AddComponent<
                    TextMeshProUGUI>();

            statusText.fontSize = 28;

            statusText.color =
                Color.white;

            statusText.alignment =
                TextAlignmentOptions.Center;

            RectTransform statusRect =
                statusText.GetComponent<
                    RectTransform>();

            statusRect.anchorMin =
                new Vector2(0.5f, 0.1f);

            statusRect.anchorMax =
                new Vector2(0.5f, 0.1f);

            statusRect.pivot =
                new Vector2(0.5f, 0.5f);

            statusRect.sizeDelta =
                new Vector2(600f, 80f);

            statusText.gameObject.SetActive(
                false
            );

            //
            // INITIAL STATE
            //

            panelObject.SetActive(false);
        }

        public void Refresh(
            CosmeticStats stats,
            bool visible
        )
        {
            if (commonText == null)
                return;

            commonText.text =
                stats.Common.ToString();

            uncommonText.text =
                stats.Uncommon.ToString();

            rareText.text =
                stats.Rare.ToString();

            ultraText.text =
                stats.UltraRare.ToString();

            RebuildLayout(stats);

            bool hasAny =
                stats.Common > 0 ||
                stats.Uncommon > 0 ||
                stats.Rare > 0 ||
                stats.UltraRare > 0;

            panelObject.SetActive(
                visible && hasAny
            );
        }

        public void ShowStatus(string message)
        {
            if (statusText == null)
                return;

            statusText.text = message;

            statusText.gameObject.SetActive(
                true
            );

            statusTimer = 2f;
        }

        public void Tick()
        {
            if (statusTimer <= 0f)
                return;

            statusTimer -= Time.deltaTime;

            if (statusTimer <= 0f)
            {
                statusText.gameObject.SetActive(
                    false
                );
            }
        }

        public void SetVisible(bool visible)
        {
            if (panelObject != null)
            {
                panelObject.SetActive(
                    visible
                );
            }
        }

        private void RebuildLayout(
            CosmeticStats stats
        )
        {
            int visibleIndex = 0;

            visibleIndex = PositionRow(
                commonRow,
                stats.Common > 0,
                visibleIndex
            );

            visibleIndex = PositionRow(
                uncommonRow,
                stats.Uncommon > 0,
                visibleIndex
            );

            visibleIndex = PositionRow(
                rareRow,
                stats.Rare > 0,
                visibleIndex
            );

            visibleIndex = PositionRow(
                ultraRow,
                stats.UltraRare > 0,
                visibleIndex
            );
        }

        private int PositionRow(
            GameObject row,
            bool active,
            int visibleIndex
        )
        {
            row.SetActive(active);

            if (!active)
                return visibleIndex;

            RectTransform rect =
                row.GetComponent<
                    RectTransform>();

            rect.anchoredPosition =
                new Vector2(
                    0f,
                    -15f - (visibleIndex * 30f)
                );

            return visibleIndex + 1;
        }
    }
}