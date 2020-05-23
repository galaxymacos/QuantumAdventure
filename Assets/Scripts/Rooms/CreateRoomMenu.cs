using System;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace Rooms
{
    public class CreateRoomMenu : MonoBehaviourPunCallbacks
    {
        public TextMeshProUGUI _roomName;

        public RoomCanvases roomCanvases;

        public void FirstInitialize(RoomCanvases _roomCanvases)
        {
            roomCanvases = _roomCanvases;
        }
        
        public void OnClick_CreateRoom()
        {
            if (!PhotonNetwork.IsConnected)
            {
                return;
            }
            // Create Room
            // Join or create room
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = 2;
            PhotonNetwork.JoinOrCreateRoom(_roomName.text, options, TypedLobby.Default);
        }

        public override void OnCreatedRoom()
        {
            print("Create room successfully");
            roomCanvases.currentRoomCanvas.Show();
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            print("Room creation failed");
        }
    }
}
