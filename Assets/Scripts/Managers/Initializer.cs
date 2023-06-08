using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Retro.Managers
{
    public class Initializer : MonoBehaviour
    {
        [SerializeField] private AssetReference gameplayManagers;
        void Start()
        {
            var loadSceneHandle = Addressables.LoadSceneAsync(gameplayManagers, LoadSceneMode.Additive);
            loadSceneHandle.Completed += (handle) => SceneManager.UnloadSceneAsync(gameObject.scene);
        }
    }
}