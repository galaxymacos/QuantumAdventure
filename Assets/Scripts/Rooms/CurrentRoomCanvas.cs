using UnityEngine;

namespace Rooms
{
    public class CurrentRoomCanvas : MonoBehaviour
    {
        public RoomCanvases roomCanavases;
        
        public PlayerListingMenu playerListingMenu;

        public void FirstInitialize(RoomCanvases roomCanvases)
        {
            roomCanavases = roomCanvases;
            playerListingMenu.FirstInitialize(roomCanvases);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
