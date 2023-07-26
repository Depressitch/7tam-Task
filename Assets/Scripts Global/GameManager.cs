using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private void Start()
        {
            SceneManager.LoadScene("LoadingScene", LoadSceneMode.Additive); //we start the game with the loading scene
            ConnectionManager.JoinedServerLobby.AddListener(() => StartCoroutine(LoadLobbyScene())); //when we successfully join a server lobby, we start a coroutine to load a lobby scene
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
        private IEnumerator LoadLobbyScene()
        {
            //TODO yield return SceneManager.UnloadSceneAsync("LoadingScene");
            AsyncOperation unloadingTask = SceneManager.UnloadSceneAsync("LoadingScene");
            while(!unloadingTask.isDone)
            {
                yield return new WaitForSeconds(1); //if task isn't done, we wait for 1 second and check it again
            }
            SceneManager.LoadSceneAsync("LobbyScene", LoadSceneMode.Additive); //once loading scene is unloaded, we start loading the lobby scene
        }
    }
}