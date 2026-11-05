using UnityEditor;
using UnityEngine;

namespace Portfolio.EditorTools
{
    /// <summary>
    /// Generic seating helper: snaps selected transforms onto a grid for classroom/lobby layouts.
    /// </summary>
    public static class SelectableSeatingEditor
    {
        [MenuItem("Portfolio/Seating/Snap Selection To Grid")]
        public static void SnapToGrid()
        {
            const float spacing = 1.5f;
            var index = 0;
            foreach (var transform in Selection.transforms)
            {
                Undo.RecordObject(transform, "Snap seating");
                var row = index / 4;
                var col = index % 4;
                transform.position = new Vector3(col * spacing, 0f, row * spacing);
                index++;
            }
        }
    }
}