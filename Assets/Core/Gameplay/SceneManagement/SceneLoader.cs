using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Core.Gameplay.SceneManagement
{
    public class SceneLoader
    {
        private List<SceneData> _scenes;
        private SceneData _lastScene;

        private SceneLoader()
        {
            _scenes = Addressables.LoadAssetsAsync<SceneData>("SceneData",
                sd => Debug.Log($"SceneData ({sd.SceneName}) Loaded")).WaitForCompletion().ToList();
            _lastScene = GetSceneData(SceneManager.GetActiveScene().name);
        }
        
        public Task<SceneInstance> LoadScene(SceneData data, LoadSceneMode mode = LoadSceneMode.Single)
        {
            _lastScene = data;
            var handle = Addressables.LoadSceneAsync(data.SceneName, mode);
            return handle.Task;
        }

        public Task<SceneInstance> LoadScene(string nameScene,
            LoadSceneMode mode = LoadSceneMode.Single) => LoadScene(GetSceneData(nameScene), mode);

        public SceneData GetSceneData(string sceneName)
        {
            var res = _scenes.FirstOrDefault(x => x.SceneName == sceneName);
            if (res == null) throw new ArgumentException($"Can't find SceneData with name {sceneName}");
            return res;
        }

        public SceneData GetCurrentScene() => _lastScene;

    }
}