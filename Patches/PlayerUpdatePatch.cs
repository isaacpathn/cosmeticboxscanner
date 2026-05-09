using Cosmetics_Box_Scanner.Models;
using Cosmetics_Box_Scanner.Systems;
using HarmonyLib;
using UnityEngine;

namespace Cosmetics_Box_Scanner.Patches
{
    [HarmonyPatch(typeof(PlayerAvatar))]
    [HarmonyPatch("Update")]
    public class PlayerUpdatePatch
    {
        private static float scanTimer;
        private static bool debugMode;

        public static void ResetTimer()
        {
            scanTimer = 0f;
        }

        private static void Postfix()
        {
            Plugin.Instance.scannerUI?.Tick();

            if (Input.GetKeyDown(KeyCode.F6))
            {
                Plugin.Instance.ToggleGUI();
            }

            if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && Input.GetKeyDown(KeyCode.F7))
            {
                debugMode = !debugMode;

                if (debugMode)
                {
                    Plugin.Instance.scannerUI.Refresh(
                        CosmeticStats.CreateDebugStats(),
                        true
                    );
                }
                else
                {
                    CosmeticScannerSystem.Scan();

                    Plugin.Instance.scannerUI.Refresh(
                        CosmeticScannerSystem.Stats,
                        Plugin.ShowGUI
                    );
                }
            }

            if (!Plugin.ShowGUI)
                return;

            if (!debugMode)
            {
                scanTimer += Time.deltaTime;

                if (scanTimer >= 2f)
                {
                    scanTimer = 0f;

                    CosmeticScannerSystem.Scan();

                    Plugin.Instance.scannerUI.Refresh(
                        CosmeticScannerSystem.Stats,
                        true
                    );
                }
            }
        }
    }
}