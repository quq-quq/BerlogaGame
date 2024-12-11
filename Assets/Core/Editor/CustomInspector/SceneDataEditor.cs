using System.Linq;
using Core.Editor.Extensions;
using Core.Gameplay.SceneManagement;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Core.Editor.CustomInspector
{
    [CustomEditor(typeof(SceneData))]
    public class SceneDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            SceneData data = (SceneData)target;
            var hande = Addressables.LoadResourceLocationsAsync(data.SceneName).WaitForCompletion();
            var resource = hande.FirstOrDefault(x => x.ResourceType == typeof(SceneInstance));
            if(resource != null)
            {
                EditorGUILayout.HelpBox("Scene found successful", MessageType.None);
                data.MakeAddressable("sd_"+data.SceneName, labels: new []{"SceneData"});
            }
            else
            {
                EditorGUILayout.HelpBox("Scene NOT found", MessageType.Warning);
            }
            
            base.OnInspectorGUI();
        }

        public static void InitSceneData(SceneData data)
        {
            var hande = Addressables.LoadResourceLocationsAsync(data.SceneName).WaitForCompletion();
            var resource = hande.FirstOrDefault(x => x.ResourceType == typeof(SceneInstance));
            if(resource != null)
            {
                data.MakeAddressable("sd_"+data.SceneName, labels: new []{"SceneData"});
            }
            else
            {
                Debug.LogWarning("Scene NOT found");
            }
        }
    }
}