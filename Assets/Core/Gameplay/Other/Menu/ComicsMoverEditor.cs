#if UNITY_EDITOR
using TMPro.Examples;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ComicsMover))]
public class ComicsMoverEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ComicsMover comicsMover = (ComicsMover)target;

        if (GUILayout.Button("Добавить текущий Transform"))
        {
            Transform currentTransform = Selection.activeTransform;
            if (currentTransform != null)
            {
                comicsMover.CreateTransformPointForEditor();
            }
        }

        DrawDefaultInspector();
    }
}

#endif