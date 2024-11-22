using System.IO;
#if UnityEditor
using Core.Extensions;
#endif
using Core.Gameplay.SceneManagement;
using UnityEditor;
using UnityEngine;

namespace Core.Editor.ContextMenu
{
    public static class SceneContextMenu
    {
        
        [MenuItem("Assets/Actions/Create SceneData", true)]
        private static bool SceneValidate()
        {
            if (Selection.activeObject is not SceneAsset) return false;
            var sceneAsset = Selection.activeObject as SceneAsset;
            var path = Path.Combine(PathContainer.SceneDataPath, sceneAsset.name.Replace(" ", "") + ".asset");
            if (File.Exists(path))
            {
                var sceneData = AssetDatabase.LoadAssetAtPath<SceneData>(path);
                return sceneData.SceneName != sceneAsset.name;
            }
            return true;
        }

        [MenuItem("Assets/Actions/Create SceneData", false, 5)]
        private static void CreateSceneData()
        {
            var sceneAsset = Selection.activeObject as SceneAsset;
            if (sceneAsset == null) return;
#if UnityEditor
            sceneAsset.MakeAddressable(sceneAsset.name);
#endif

            var path = Path.Combine(PathContainer.SceneDataPath, sceneAsset.name.Replace(" ", "") + ".asset");
            if (File.Exists(path))
            {
                var sceneData = AssetDatabase.LoadAssetAtPath<SceneData>(path);
                sceneData.SceneName = sceneAsset.name;
                Debug.Log("Scene data updated");
                return;
            }
            var instance = ScriptableObject.CreateInstance<SceneData>();
            instance.SceneName = sceneAsset.name;
            Directory.CreateDirectory(PathContainer.SceneDataPath);
            AssetDatabase.CreateAsset(instance, path);
            AssetDatabase.SaveAssets();
            SceneDataEditor.InitSceneData(instance);
        }
    
    }
}
