using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private string currentMainScene = nameof(Scenes.LoadingScene);
        public bool IsLoading => currentMainScene == nameof(Scenes.LoadingScene);


        private void Awake()
        {
            //transition to LobbyScene when we successfully join server lobby
            ConnectionManager.JoinedServerLobby.AddListener(() => StartCoroutine(TransitionToScene(nameof(Scenes.LobbyScene))));
            //transition to GameScene when we successfully join a room from LobbyScene
            ConnectionManager.JoinedRoom.AddListener(() => StartCoroutine(TransitionToScene(nameof(Scenes.GameScene))));
        }
        private void Start()
        {
            SceneManager.LoadScene(currentMainScene, LoadSceneMode.Additive); //start the game with the loading screen
            StartCoroutine(ConnectToServerLobby()); //start trying to connect to a server lobby
        }
        private IEnumerator ConnectToServerLobby()
        {
            while(!ConnectionManager.TryToConnect()) //attempting to reconnect every time connection attempt failed
            {
                Debug.LogError("Couldn't connect to Photon, trying again.");
                yield return null;
            }
        }
        private IEnumerator TransitionToScene(string targetSceneName)
        {
            if(targetSceneName == nameof(Scenes.LoadingScene))
            {
                Debug.LogError("Don't change to LoadingScene! It's reserved for transitions between active scenes.");
                yield break;
            }
            
            float oldTimeScale = Time.timeScale;
            Time.timeScale = 0; //pause the game entirely

            if(!IsLoading)
                SceneManager.LoadScene(nameof(Scenes.LoadingScene), LoadSceneMode.Additive); //load loading screen


            AsyncOperation loadingTask = SceneManager.LoadSceneAsync(targetSceneName, LoadSceneMode.Additive);
            while(!loadingTask.isDone) //wait until the target scene is loaded
                yield return null;

            if (!IsLoading)
            {
                AsyncOperation unloadingTask = SceneManager.UnloadSceneAsync(currentMainScene);
                while(!unloadingTask.isDone) //wait until the previous scene is unloaded
                    yield return null;
            }
            
            currentMainScene = targetSceneName;
            SceneManager.UnloadSceneAsync(nameof(Scenes.LoadingScene)); //unload loading scene when all is finished

            Time.timeScale = oldTimeScale; //return to the previous timeScale to resume the game
        }

        private enum Scenes
        {
            LoadingScene,
            LobbyScene,
            GameScene,
        }
    }
}