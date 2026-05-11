using HarmonyLib;
using UnityEngine;

namespace Cosmetics_Box_Scanner.Patches
{
    public class ForceUltraRarePatch
    {
        // 用來記錄當前關卡是否已經有紫箱了
        public static bool ultraRareSpawnedThisLevel = false;

        // 攔截 CosmeticWorldObject 的 Awake 方法（在它設定顏色和掉落物之前）
        [HarmonyPatch(typeof(CosmeticWorldObject), "Awake")]
        public class CosmeticWorldObjectAwakePatch
        {
            [HarmonyPrefix]
            public static void Prefix(CosmeticWorldObject __instance)
            {
                // 如果在主選單，就不執行
                if (SemiFunc.MenuLevel()) return;

                // 如果這場遊戲還沒有生成過紫箱，就把這個箱子強制改成紫色
                if (!ultraRareSpawnedThisLevel)
                {
                    __instance.rarity = SemiFunc.Rarity.UltraRare;
                    ultraRareSpawnedThisLevel = true;
                    Plugin.Log.LogInfo("已將一個 Cosmetic 箱子強制修改為 UltraRare (紫色)！");
                }
                // 如果系統自己生成了紫箱，也記錄下來，避免重複強制修改
                else if (__instance.rarity == SemiFunc.Rarity.UltraRare)
                {
                    ultraRareSpawnedThisLevel = true;
                }
            }
        }

        // 攔截關卡生成器，每次載入新關卡時，重置紫箱標記
        [HarmonyPatch(typeof(LevelGenerator), "Awake")] 
        public class LevelGeneratorAwakePatch
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                ultraRareSpawnedThisLevel = false;
                Plugin.Log.LogInfo("新關卡載入，重置紫箱強制生成狀態。");
            }
        }
    }
}
