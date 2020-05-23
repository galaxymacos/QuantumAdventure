using UnityEngine;

namespace Rooms
{
    public class CreateOrJoinRoomCanvas : MonoBehaviour
    {
        public RoomCanvases roomCanavases;

        [SerializeField] private CreateRoomMenu _createRoomMenu;
        [SerializeField] private RoomListingsMenu roomListingsMenu;

        public void FirstInitialize(RoomCanvases roomCanvases)
        {
            roomCanavases = roomCanvases;
            _createRoomMenu.FirstInitialize(roomCanvases);
            roomListingsMenu.FirstInitialize(roomCanvases);
        }
    }
}
