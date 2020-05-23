using UnityEngine;

namespace Rooms
{
    public class CurrentRoomCanvas : MonoBehaviour
    {
        #region Private method

        private RoomCanvases _roomCanavases;
        [SerializeField] private PlayerListingsMenu playerListingsMenu;
        [SerializeField] private LeaveRoomMenu _leaveRoomMenu;


        #endregion

        #region Property

        // parent ref
        public RoomCanvases RoomCanavases => _roomCanavases;
        
        // child ref
        public LeaveRoomMenu LeaveRoomMenu => _leaveRoomMenu;
        public PlayerListingsMenu PlayerListingsMenu => playerListingsMenu;

        #endregion


        #region Public method

        public void FirstInitialize(RoomCanvases roomCanvases)
        {
            _roomCanavases = roomCanvases;
            playerListingsMenu.FirstInitialize(roomCanvases);
            _leaveRoomMenu.FirstInitialize(roomCanvases);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        #endregion
    }
}
