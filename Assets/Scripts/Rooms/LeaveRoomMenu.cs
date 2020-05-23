using Photon.Pun;
using UnityEngine;

namespace Rooms
{
    public class LeaveRoomMenu : MonoBehaviour
    {
        #region Property
        

        #endregion

        #region Private Field
        
        private RoomCanvases _roomCanvases;

            #endregion

        #region MonoBehavior Callback

    

        #endregion

        #region Public Method

        public void FirstInitialize(RoomCanvases roomCanvases)
        {
            _roomCanvases = roomCanvases;
        }

        public void OnClick_LeaveRoom()
        {
            PhotonNetwork.LeaveRoom(true);
            _roomCanvases.currentRoomCanvas.Hide();
        }

        #endregion

        #region Private Method

        #endregion
    }
}
