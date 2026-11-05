using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Portfolio.EditorTools
{
    public sealed class AssetBundleManifestWindow : EditorWindow
    {
        private DefaultAsset _folder;
        private string _baseUrl = "https://cdn.example.com/bundles";

        [MenuItem("Portfolio/AssetBundle Manifest")]
        public static void Open() => GetWindow<AssetBundleManifestWindow>("Bundle Manifest");

        private void OnGUI()
        {
            _folder = (DefaultAsset)EditorGUILayout.ObjectField("Bundles Folder", _folder, typeof(DefaultAsset), false);
            _baseUrl = EditorGUILayout.TextField("Base URL", _baseUrl);

            if (GUILayout.Button("Generate JSON Manifest") && _folder != null)
            {
                var path = AssetDatabase.GetAssetPath(_folder);
                var files = Directory.GetFiles(path);
                var sb = new StringBuilder();
                sb.AppendLine("{");
                sb.AppendLine("  \"bundles\": [");
                var first = true;
                foreach (var file in files)
                {
                    if (file.EndsWith(".meta")) continue;
                    var name = Path.GetFileName(file);
                    if (!first) sb.AppendLine(",");
                    sb.AppendLine("    {");
                    sb.AppendLine($"      \"name\": \"{Path.GetFileNameWithoutExtension(name)}\",");
                    sb.AppendLine($"      \"url\": \"{_baseUrl.TrimEnd('/')}/{name}\",");
                    sb.AppendLine("      \"version\": \"1\",");
                    sb.AppendLine("      \"dependencies\": []");
                    sb.Append("    }");
                    first = false;
                }
                sb.AppendLine();
                sb.AppendLine("  ]");
                sb.AppendLine("}");

                var outPath = Path.Combine(path, "manifest.json");
                File.WriteAllText(outPath, sb.ToString());
                AssetDatabase.Refresh();
                Debug.Log($"Wrote {outPath}");
            }
        }
    }
}