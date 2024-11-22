using System;
using System.Linq;
#if UnityEditor
using Core.Extensions;
#endif
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Core.Gameplay.SceneManagement
{
    [CreateAssetMenu(fileName = "NewSceneData", menuName = "SceneData"), Serializable]
    public class SceneData : ScriptableObject
    {
        [SerializeField] private string _sceneName;
        [SerializeField] private AudioClip _musicBackground;
        
        public string SceneName { get => _sceneName; set => _sceneName = value; }
    }
    
#if UNITY_EDITOR
    [CustomEditor(typeof(SceneData))]
    public class SceneDataEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            SceneData data = (SceneData)target;
            var hande = Addressables.LoadResourceLocationsAsync(data.SceneName).WaitForCompletion();
            var resource = hande.FirstOrDefault(x => x.ResourceType == typeof(SceneInstance));
            if(resource != null)
            {
                EditorGUILayout.HelpBox("Scene found successful", MessageType.None);
#if UnityEditor
                data.MakeAddressable("sd_"+data.SceneName, labels: new []{"SceneData"});
#endif
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
#if UnityEditor
                data.MakeAddressable("sd_"+data.SceneName, labels: new []{"SceneData"});
#endif
            }
            else
            {
                Debug.LogWarning("Scene NOT found");
            }
        }
    }
#endif
            }
