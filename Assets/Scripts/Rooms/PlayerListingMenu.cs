using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Rooms
{
    public class PlayerListingMenu : MonoBehaviourPunCallbacks
    {
        private RoomCanvases _roomCanvases;

        [SerializeField]
        private Transform _content;

        [SerializeField] private GameObject _playerListingPrefab;

        [SerializeField] private List<PlayerListing> listings;

        public void FirstInitialize(RoomCanvases roomCanvases)
        {
            _roomCanvases = roomCanvases;
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            GameObject listingObject = Instantiate(_playerListingPrefab, _content);
            var listing = listingObject.GetComponent<PlayerListing>();
            if (listing != null)
            {
                listing.SetPlayerInfo(newPlayer);
                listings.Add(listing);

            }
            else
            {
                Debug.LogError("There is not RoomListing Component in the gameobject "+listingObject.name);
            }
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
        
    }
}
