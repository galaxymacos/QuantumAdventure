using System;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace Rooms
{
    public class CreateRoomMenu : MonoBehaviourPunCallbacks
    {

        #region Private field
        
        [SerializeField] private TextMeshProUGUI _roomName;
        private RoomCanvases roomCanvases;

        #endregion

        #region Property

        public RoomCanvases RoomCanvases => roomCanvases;

        #endregion

        #region Public method

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

            RoomOptions options = new RoomOptions
            {
                MaxPlayers = 2,
                BroadcastPropsChangeToAll = true, 
                PublishUserId = true
            };

            PhotonNetwork.JoinOrCreateRoom(_roomName.text, options, TypedLobby.Default);
        }

        #endregion

        #region Callback

        public override void OnCreatedRoom()
        {
            print("Create room successfully");
            roomCanvases.currentRoomCanvas.Show();
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            print("Room creation failed");
        }

        public override void OnConnectedToMaster()
        {
            
        }

        #endregion
    }
}
