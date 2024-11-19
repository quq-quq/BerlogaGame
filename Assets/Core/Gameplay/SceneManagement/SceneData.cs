using System.Linq;
using Core.Extensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Core.Gameplay.SceneManagement
{
    [CreateAssetMenu(fileName = "NewSceneData", menuName = "SceneData")]
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
                data.MakeAddressable("sd_"+data.SceneName, labels: new []{"SceneData"});
            }
            else
            {
                EditorGUILayout.HelpBox("Scene NOT found", MessageType.Warning);
            }
            
            base.OnInspectorGUI();
        }
        
    }
#endif
}
