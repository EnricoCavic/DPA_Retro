using Retro.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Retro.Managers
{
    public class SceneManager : Singleton<SceneManager>
    {
        [SerializeField] private AssetReference firstScene;

        private SceneInstance loadedScene;

        private void Awake()
        {
            if (!InstanceSetup(this)) return;

            var handle = Addressables.LoadSceneAsync(firstScene, LoadSceneMode.Additive);
            handle.Completed += (operation) =>
            {
                loadedScene = new();
                loadedScene = operation.Result;
            };
        }

        public AsyncOperationHandle<SceneInstance> LoadScene(object _assetKey)
        {
            Addressables.UnloadSceneAsync(loadedScene);
            
            var handle = Addressables.LoadSceneAsync(_assetKey, LoadSceneMode.Additive);
            handle.Completed += (operation) =>
            {
                loadedScene = new();
                loadedScene = operation.Result;
            };

            return handle;
        }
        
    }
}