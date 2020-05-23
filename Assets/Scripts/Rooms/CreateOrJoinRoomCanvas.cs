using UnityEngine;

namespace Rooms
{
    public class CreateOrJoinRoomCanvas : MonoBehaviour
    {
        public RoomCanvases roomCanavases;

        public CreateRoomMenu createRoomMenu;

        public void FirstInitialize(RoomCanvases _roomCanvases)
        {
            roomCanavases = _roomCanvases;
            createRoomMenu.FirstInitialize(_roomCanvases);
        }
    }
}
