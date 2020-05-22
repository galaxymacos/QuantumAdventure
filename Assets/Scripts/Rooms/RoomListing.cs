using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace Rooms
{
    public class RoomListing: MonoBehaviour
    {
        public TextMeshProUGUI listText;

        public RoomInfo RoomInfo { get; private set; }
        
        public void SetRoomInfo(RoomInfo _roomInfo)
        {
            RoomInfo = _roomInfo;
            listText.text = $"{_roomInfo.MaxPlayers}, {_roomInfo.Name}";
        }
    }
}