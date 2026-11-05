using UnityEditor;
using UnityEngine;

namespace Portfolio.EditorTools
{
    public sealed class CatalogSetupWindow : EditorWindow
    {
        private string _id = "item-001";
        private string _displayName = "New Item";
        private GameObject _prefab;
        private string _folder = "Assets/Catalog";

        [MenuItem("Portfolio/Catalog Setup")]
        public static void Open() => GetWindow<CatalogSetupWindow>("Catalog Setup");

        private void OnGUI()
        {
            _id = EditorGUILayout.TextField("Id", _id);
            _displayName = EditorGUILayout.TextField("Display Name", _displayName);
            _prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", _prefab, typeof(GameObject), false);
            _folder = EditorGUILayout.TextField("Folder", _folder);

            if (GUILayout.Button("Create Catalog Item"))
            {
                if (!AssetDatabase.IsValidFolder(_folder))
                {
                    System.IO.Directory.CreateDirectory(_folder);
                    AssetDatabase.Refresh();
                }

                var asset = CreateInstance<CatalogItem>();
                asset.Id = _id;
                asset.DisplayName = _displayName;
                asset.Prefab = _prefab;
                var path = $"{_folder}/{_id}.asset";
                AssetDatabase.CreateAsset(asset, path);
                AssetDatabase.SaveAssets();
                EditorGUIUtility.PingObject(asset);
            }
        }
    }
}