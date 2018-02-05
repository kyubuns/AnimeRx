using System.IO;
using UnityEditor;
using UnityEngine;

namespace Development
{
    public static class PackageExporter
    {
        [MenuItem("Dev/Export Package")]
        public static void Export()
        {
            var directories = new[]
            {
                "Assets/Plugins/AnimeRx",
            };
            var outputPath = Path.Combine(Path.GetDirectoryName(Application.dataPath), "AnimeRx.unitypackage");

            AssetDatabase.ExportPackage(directories, outputPath);

            Debug.LogFormat("ExportPackage {0}", outputPath);
        }
    }
}
