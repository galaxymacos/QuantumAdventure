using UnityEngine;

namespace Rooms
{
    public class CurrentRoomCanvas : MonoBehaviour
    {
        public RoomCanvases roomCanavases;
        
        public PlayerListingsMenu playerListingsMenu;

        [SerializeField] private LeaveRoomMenu _leaveRoomMenu;

        public void FirstInitialize(RoomCanvases roomCanvases)
        {
            roomCanavases = roomCanvases;
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
    }
}
