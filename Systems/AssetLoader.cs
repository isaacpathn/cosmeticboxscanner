using UnityEngine;
using System.IO;

namespace Cosmetics_Box_Scanner.Systems
{
    public static class AssetLoader
    {
        public static Sprite Load(string fileName)
        {
            string asset_path = Path.Combine(
                Path.GetDirectoryName(Plugin.Instance.Info.Location),
                "Assets",
                fileName
            );

            if (!File.Exists(asset_path))
            {
                Plugin.Log.LogError($"SPRITE NOT FOUND: {asset_path}");
                return null;
            }

            byte[] bytes = File.ReadAllBytes(asset_path);

            Texture2D texture = new Texture2D(2, 2);

            texture.LoadImage(bytes);

            texture.filterMode = FilterMode.Point;

            return Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f)
            );
        }
    }
}