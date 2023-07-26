using Photon.Pun;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class ConnectionManager : MonoBehaviourPunCallbacks
    {
        public static UnityEvent JoinedServerLobby { get; private set; } = new();


        public static bool TryToConnect()
        {
            return PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            JoinedServerLobby.Invoke();
        }

        //public override void OnDisconnected(DisconnectCause cause)
        //{
        //    base.OnDisconnected(cause);
        //}
    }
}