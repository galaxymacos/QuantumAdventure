using System;
using System.Collections.Generic;
using Helpers;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Rooms
{
    public class PlayerListingsMenu : MonoBehaviourPunCallbacks
    {
        private RoomCanvases _roomCanvases;

        [SerializeField]
        private Transform _content;

        [SerializeField] private GameObject _playerListingPrefab;

        [SerializeField] private List<PlayerListing> listings;

        #region Callbacks

        public override void OnEnable()
        {
            GetCurrentRoomPlayers();
        }
        
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            AddPlayerListing(newPlayer);
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            int indexToRemove = listings.FindIndex(x => x.Player == otherPlayer);
            if (indexToRemove != -1)
            {
                Destroy(listings[indexToRemove].gameObject);
                listings.RemoveAt(indexToRemove);
            }
        }

        #endregion

        #region Public method

        public void FirstInitialize(RoomCanvases roomCanvases)
        {
            _roomCanvases = roomCanvases;
        }


        

        public void GetCurrentRoomPlayers()
        {
            foreach (KeyValuePair<int,Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
            {
                AddPlayerListing(playerInfo.Value);
            }
        }

        public void AddPlayerListing(Player player)
        {
            int index = listings.FindIndex(x => Equals(x.Player, player));
            if (index != -1)
            {
                listings[index].SetPlayerInfo(player);
            }
            else
            {
                GameObject listingObject = Instantiate(_playerListingPrefab, _content);
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

        #endregion
        

        
        
    }
}
