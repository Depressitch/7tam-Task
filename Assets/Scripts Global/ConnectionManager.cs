using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class ConnectionManager : MonoBehaviourPunCallbacks
    {
        public static UnityEvent JoinedServerLobby { get; private set; } = new();
        public static UnityEvent JoinedRoom { get; private set; } = new();

        private void Start()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }
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
            Debug.Log("Joined lobby");
            JoinedServerLobby.Invoke();
        }

        public void CreateRoom(TMPro.TMP_InputField roomInputField)
        {
            PhotonNetwork.CreateRoom(roomInputField.text);
        }

        public static void JoinRoom(TMPro.TMP_InputField roomInputField)
        {
            PhotonNetwork.JoinRoom(roomInputField.text);
        }

        public override void OnJoinedRoom()
        {
            JoinedRoom.Invoke();
            Debug.Log("Joined room" + PhotonNetwork.CurrentRoom.Name);
        }
        //public override void OnDisconnected(DisconnectCause cause)
        //{
        //    base.OnDisconnected(cause);
        //}
    }
}