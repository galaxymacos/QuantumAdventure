using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace Rooms
{
    public class RoomListing: MonoBehaviour
    {
        public TextMeshProUGUI listText;
        

        public RoomInfo RoomInfo { get; private set; }

        #region Public method

        public void SetRoomInfo(RoomInfo roomInfo)
        {
            RoomInfo = roomInfo;
            listText.text = $"{roomInfo.MaxPlayers}, {roomInfo.Name}";
        }

        public void OnClick_Button()
        {
            PhotonNetwork.JoinRoom(RoomInfo.Name);
        }

        #endregion
    }
}