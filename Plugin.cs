using BepInEx;
using HarmonyLib;
using Cosmetics_Box_Scanner.Patches;
using Cosmetics_Box_Scanner.Systems;
using Cosmetics_Box_Scanner.UI;

namespace Cosmetics_Box_Scanner
{
    [BepInPlugin(
        "cosmetic.scanner",
        "Cosmetic Scanner",
        "1.0.3"
    )]
    public class Plugin : BaseUnityPlugin
    {
        internal static Plugin Instance;
        internal static BepInEx.Logging.ManualLogSource Log;
        public static bool ShowGUI;
        private Harmony harmony;
        public ScannerUI scannerUI;

        private void Awake()
        {
            Instance = this;

            Log = Logger;

            harmony = new Harmony(
                "cosmetic.scanner"
            );

            harmony.PatchAll();

            Log.LogInfo(
                "Cosmetic Scanner Loaded"
            );
        }

        public void ToggleGUI()
        {
            Plugin.Log.LogInfo(
                "F6 PRESSED"
            );
            if (scannerUI == null)
            {
                scannerUI = new ScannerUI();
                scannerUI.Create();

                Log.LogInfo("UI CREATED");
            }

            ShowGUI = !ShowGUI;

            scannerUI.SetVisible(ShowGUI);

            PlayerUpdatePatch.ResetTimer();

            if (ShowGUI)
            {
                CosmeticScannerSystem.Scan();

                scannerUI.Refresh(
                    CosmeticScannerSystem.Stats,
                    true
                );

                scannerUI.ShowStatus(
                    "Cosmetic Overlay Enabled"
                );
            }
            else
            {
                scannerUI.Refresh(
                    CosmeticScannerSystem.Stats,
                    false
                );

                scannerUI.ShowStatus(
                    "Cosmetic Overlay Disabled"
                );
            }
        }
    }
}