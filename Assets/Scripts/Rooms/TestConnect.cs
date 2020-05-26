using System;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

namespace Rooms
{
    public class TestConnect : MonoBehaviourPunCallbacks
    {
        public TextMeshProUGUI nickName;
        private void Start()
        {
            OnClick_Connect();
        }

        public void OnClick_Connect()
        {
            PhotonNetwork.NickName = nickName.text;
            AuthenticationValues authValues = new AuthenticationValues("1");
            PhotonNetwork.AuthValues = authValues;
            PhotonNetwork.SendRate = 20;
            PhotonNetwork.SerializationRate = 10;
            PhotonNetwork.GameVersion = "0.1";
            PhotonNetwork.AutomaticallySyncScene = true;

            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            print($"{PhotonNetwork.LocalPlayer.NickName} connect to Photon.");

            if (!PhotonNetwork.InLobby)
            {
                PhotonNetwork.JoinLobby();
            }

        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            print($"Failed to connect to photon: {cause}");
        }
        
        

        public override void OnJoinedLobby()
        {
            base.OnJoinedLobby();
            
        }
    }
}