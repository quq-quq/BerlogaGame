using System;
using UnityEngine;

namespace Core.Gameplay.SceneManagement
{
    [CreateAssetMenu(fileName = "NewSceneData", menuName = "SceneData"), Serializable]
    public class SceneData : ScriptableObject
    {
        [SerializeField] private string _sceneName;
        [SerializeField] private AudioClip _backgroundMusic;
        
        public string SceneName { get => _sceneName; set => _sceneName = value; }
        public AudioClip BackgroundMusic { get => _backgroundMusic; }
    }
}
