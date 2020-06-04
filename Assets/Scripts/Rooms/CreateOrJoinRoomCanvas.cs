using UnityEngine;
using UnityEngine.Serialization;

namespace Rooms
{
    public class CreateOrJoinRoomCanvas : MonoBehaviour
    {
        #region Private field

        // Ref to parent
        private RoomCanvases _roomCanavases;

        // Ref to children
        [FormerlySerializedAs("_createRoomMenu")] [SerializeField] private CreateRoomMenu createRoomMenu;
        [FormerlySerializedAs("_roomListingsMenu")] [SerializeField] private RoomListingsMenu roomListingsMenu;

        #endregion


        #region Public method

        public void FirstInitialize(RoomCanvases roomCanvases)
        {
            _roomCanavases = roomCanvases;
            createRoomMenu.FirstInitialize(roomCanvases);
            roomListingsMenu.FirstInitialize(roomCanvases);
        }

        #endregion
    }
}
