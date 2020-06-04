using System;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Rooms
{
    public class CreateRoomMenu : MonoBehaviourPunCallbacks
    {

        #region Private field
        
        [FormerlySerializedAs("_roomName")] [SerializeField] private TextMeshProUGUI roomName;
        private RoomCanvases _roomCanvases;

        #endregion

        #region Property

        public RoomCanvases RoomCanvases => _roomCanvases;

        #endregion

        #region Public method

        public void FirstInitialize(RoomCanvases roomCanvases)
        {
            this._roomCanvases = roomCanvases;
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

            PhotonNetwork.JoinOrCreateRoom(roomName.text, options, TypedLobby.Default);
        }

        #endregion

        #region Callback

        public override void OnCreatedRoom()
        {
            print("Create room successfully");
            _roomCanvases.currentRoomCanvas.Show();
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
