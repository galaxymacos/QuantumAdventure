using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace Rooms
{
    public class PlayerListing: MonoBehaviour
    {
        public TextMeshProUGUI _text;

        private Player _player;

        public Player Player => _player;

        public void SetPlayerInfo(Player player)
        {
            _player = player;
            _text.text = player.NickName;
        }
        
    }
}