using UnityEngine;

namespace Rooms
{
    public class CreateOrJoinRoomCanvas : MonoBehaviour
    {
        #region Private field

        // Ref to parent
        private RoomCanvases roomCanavases;

        // Ref to children
        [SerializeField] private CreateRoomMenu _createRoomMenu;
        [SerializeField] private RoomListingsMenu _roomListingsMenu;

        #endregion


        #region Public method

        public void FirstInitialize(RoomCanvases roomCanvases)
        {
            roomCanavases = roomCanvases;
            _createRoomMenu.FirstInitialize(roomCanvases);
            _roomListingsMenu.FirstInitialize(roomCanvases);
        }

        #endregion
    }
}
