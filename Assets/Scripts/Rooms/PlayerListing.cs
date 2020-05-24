using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace Rooms
{
    public class PlayerListing: MonoBehaviourPunCallbacks
    {
        public TextMeshProUGUI _text;

        private Player _player;
        public bool Ready;
        

        public Player Player => _player;

        public void SetPlayerInfo(Player player)
        {
            _player = player;
            SetPlayerText(player);

        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            if (targetPlayer != null && Equals(targetPlayer, Player))
            {
                if (changedProps.ContainsKey("RandomNumber"))
                {
                    SetPlayerText(targetPlayer);
                    
                }
            }
        }

        private void SetPlayerText(Player player)
        {
            string result = "";
            if (player.CustomProperties.ContainsKey("RandomNumber"))
            {
                result = (string)player.CustomProperties["RandomNumber"];
            }
            _text.text = result+", "+player.NickName;
        }
    }
}