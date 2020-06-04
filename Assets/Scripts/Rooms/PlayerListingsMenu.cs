using System;
using System.Collections.Generic;
using Helpers;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Rooms
{
    public class PlayerListingsMenu : MonoBehaviourPunCallbacks
    {
        #region Private field

        private RoomCanvases _roomCanvases;
        [FormerlySerializedAs("_content")] [SerializeField] private Transform content;
        [FormerlySerializedAs("_playerListingPrefab")] [SerializeField] private GameObject playerListingPrefab;
        [FormerlySerializedAs("_listings")] [SerializeField] private List<PlayerListing> listings;
        [SerializeField] private TextMeshProUGUI readyText;

        private bool _ready = false;

        #endregion

        #region Property

        public RoomCanvases RoomCanvases => _roomCanvases;

        #endregion

        #region Callback
        

        public override void OnEnable()
        {
            base.OnEnable();
            SetReadyUp(false);
            GetCurrentRoomPlayers();

        }

        public override void OnDisable()
        {
            base.OnDisable();
            foreach (var playerListing in listings)
            {
                Destroy(playerListing.gameObject);
            }
            listings.Clear();
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            AddPlayerListing(newPlayer);
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            int indexToRemove = listings.FindIndex(x => Equals(x.Player, otherPlayer));
            if (indexToRemove != -1)
            {
                Destroy(listings[indexToRemove].gameObject);
                listings.RemoveAt(indexToRemove);
            }
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            _roomCanvases.currentRoomCanvas.LeaveRoomMenu.OnClick_LeaveRoom();
        }

        #endregion

        #region Public method

        public void FirstInitialize(RoomCanvases roomCanvases)
        {
            _roomCanvases = roomCanvases;
        }

        public void SetReadyUp(bool state)
        {
            _ready = state;

            if (_ready)
            {
                readyText.text = "R";
            }
            else
            {
                readyText.text = "N";
            }
        }

        public void OnClick_StartGame()
        {
            if (CheckPlayerSelectDifferentCharacter()) return;

            if (PhotonNetwork.IsMasterClient)
            {
                foreach (var playerListing in listings)
                {
                    if (!Equals(playerListing.Player, PhotonNetwork.LocalPlayer))
                    {
                        if (!playerListing.ready)
                        {
                            return;
                        }
                    }
                }
                PhotonNetwork.CurrentRoom.IsOpen = false;
                PhotonNetwork.CurrentRoom.IsVisible = false;
                PhotonNetwork.LoadLevel("Room for "+PhotonNetwork.CurrentRoom.PlayerCount);
            }
        }

        public void OnClick_ReadyUp()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                SetReadyUp(!_ready);
                photonView.RPC("RPC_ChangeReadyState", RpcTarget.MasterClient,PhotonNetwork.LocalPlayer, _ready);
            }
        }

        #endregion

        #region Private method

        private void GetCurrentRoomPlayers()
        {
            if (!PhotonNetwork.IsConnected)
            {
                return;
            }

            if (PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null)
            {
                return;
            }

            foreach (KeyValuePair<int,Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
            {
                AddPlayerListing(playerInfo.Value);
            }
        }

        private void AddPlayerListing(Player player)
        {
            int index = listings.FindIndex(x => Equals(x.Player, player));
            if (index != -1)
            {
                listings[index].SetPlayerInfo(player);
            }
            else
            {
                GameObject listingObject = Instantiate(playerListingPrefab, content);
                var listing = listingObject.GetComponent<PlayerListing>();
                if (listing != null)
                {
                    listing.SetPlayerInfo(player);
                    listings.Add(listing);

                }
                else
                {
                    Debug.LogError("There is not RoomListing Component in the gameobject "+listingObject.name);
                }
            }
            

        }

        private bool CheckPlayerSelectDifferentCharacter()
        {
            HashSet<string> redundantCharacterList = new HashSet<string>();
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                string playerCharacterDecision = (string) player.CustomProperties["RandomNumber"];
                if (redundantCharacterList.Contains(playerCharacterDecision))
                {
                    Debug.Log("You need to choose a different character than your teammate to continue");
                    return true;
                }

                redundantCharacterList.Add(playerCharacterDecision);
            }

            return false;
        }

        #endregion

        #region RPC

        [PunRPC]
        private void RPC_ChangeReadyState(Player player, bool ready)
        {
            int index = listings.FindIndex(x => Equals(x.Player, player));
            if (index != -1)
            {
                listings[index].ready = ready;
            }
        }

        #endregion
        
        
    }
}
