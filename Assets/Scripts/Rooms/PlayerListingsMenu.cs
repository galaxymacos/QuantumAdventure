using System;
using System.Collections.Generic;
using Helpers;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace Rooms
{
    public class PlayerListingsMenu : MonoBehaviourPunCallbacks
    {
        #region Private field

        private RoomCanvases _roomCanvases;
        [SerializeField] private Transform _content;
        [SerializeField] private GameObject _playerListingPrefab;
        [SerializeField] private List<PlayerListing> _listings;
        [SerializeField] private TextMeshProUGUI readyText;

        private bool _ready = false;

        #endregion

        #region Property

        public RoomCanvases RoomCanvases => _roomCanvases;

        #endregion

        #region Callback

        private void Awake()
        {
            GetCurrentRoomPlayers();
            
        }

        public override void OnEnable()
        {
            base.OnEnable();
            SetReadyUp(false);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            foreach (var playerListing in _listings)
            {
                Destroy(playerListing.gameObject);
            }
            _listings.Clear();
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            AddPlayerListing(newPlayer);
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            int indexToRemove = _listings.FindIndex(x => Equals(x.Player, otherPlayer));
            if (indexToRemove != -1)
            {
                Destroy(_listings[indexToRemove].gameObject);
                _listings.RemoveAt(indexToRemove);
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
        
        public void GetCurrentRoomPlayers()
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

        public void AddPlayerListing(Player player)
        {
            int index = _listings.FindIndex(x => Equals(x.Player, player));
            if (index != -1)
            {
                _listings[index].SetPlayerInfo(player);
            }
            else
            {
                GameObject listingObject = Instantiate(_playerListingPrefab, _content);
                var listing = listingObject.GetComponent<PlayerListing>();
                if (listing != null)
                {
                    listing.SetPlayerInfo(player);
                    _listings.Add(listing);

                }
                else
                {
                    Debug.LogError("There is not RoomListing Component in the gameobject "+listingObject.name);
                }
            }
            

        }

        public void OnClick_StartGame()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                foreach (var playerListing in _listings)
                {
                    if (!Equals(playerListing.Player, PhotonNetwork.LocalPlayer))
                    {
                        if (!playerListing.Ready)
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
                base.photonView.RPC("RPC_ChangeReadyState", RpcTarget.MasterClient,PhotonNetwork.LocalPlayer, _ready);
            }
        }

        #endregion

        [PunRPC]
        private void RPC_ChangeReadyState(Player player, bool ready)
        {
            int index = _listings.FindIndex(x => Equals(x.Player, player));
            if (index != -1)
            {
                _listings[index].Ready = ready;
            }
        }

        #region RPC
        
        

        #endregion
        
        
    }
}
