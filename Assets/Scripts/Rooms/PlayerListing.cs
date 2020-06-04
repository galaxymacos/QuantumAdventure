using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Rooms
{
    public class PlayerListing: MonoBehaviourPunCallbacks
    {
        [FormerlySerializedAs("_text")] public TextMeshProUGUI text;

        private Player _player;
        [FormerlySerializedAs("Ready")] public bool ready;
        

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
            text.text = result+", "+player.NickName;
        }
    }
}