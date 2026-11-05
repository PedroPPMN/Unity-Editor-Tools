using UnityEngine;

namespace Portfolio.EditorTools
{
    [CreateAssetMenu(menuName = "Portfolio/Catalog Item", fileName = "CatalogItem")]
    public sealed class CatalogItem : ScriptableObject
    {
        public string Id;
        public string DisplayName;
        public GameObject Prefab;
        public string[] Tags;
    }
}