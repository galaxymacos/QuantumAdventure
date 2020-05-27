using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace Rooms
{
    public class TestConnect : MonoBehaviourPunCallbacks
    {
        public TextMeshProUGUI nickName;
        private void Start()
        {
            OnClick_Connect();

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public void OnClick_Connect()
        {
            PhotonNetwork.NickName = nickName.text;
            // AuthenticationValues authValues = new AuthenticationValues("0");
            // PhotonNetwork.AuthValues = authValues;
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
            print($"joined lobby");

            // PhotonNetwork.FindFriends(new[] {"1"});
            
        }

        public override void OnFriendListUpdate(List<FriendInfo> friendList)
        {
            foreach (FriendInfo friendInfo in friendList)
            {
                Debug.Log($"Friend info received " + friendInfo.UserId + " is online? " + friendInfo.IsOnline);
            }
        }
    }
}