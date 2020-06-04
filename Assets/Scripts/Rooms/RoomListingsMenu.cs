using System.Collections.Generic;
using Helpers;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Serialization;

namespace Rooms
{
    public class RoomListingsMenu: MonoBehaviourPunCallbacks
    {
        #region Private field

        [FormerlySerializedAs("_content")] [SerializeField] private Transform content;
        [SerializeField] private GameObject roomListingPrefab;
        private RoomCanvases _roomCanvases;

        #endregion

        #region Property

        public RoomCanvases RoomCanvases => _roomCanvases;
        [FormerlySerializedAs("_listings")] public List<RoomListing> listings = new List<RoomListing>();

        #endregion

        #region Public method

        public void FirstInitialize(RoomCanvases roomCanvases)
        {
            _roomCanvases = roomCanvases;
        }

        #endregion

        #region Callback

        public override void OnJoinedRoom()
        {
            _roomCanvases.currentRoomCanvas.Show();
            content.DestroyChildren();
            listings.Clear();
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            foreach (RoomInfo info in roomList)
            {
                // Remove from room list
                if (info.RemovedFromList)
                {
                    int indexToRemove = listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                    if (indexToRemove != -1)
                    {
                        Destroy(listings[indexToRemove].gameObject);
                        listings.RemoveAt(indexToRemove);
                    }
                }
                else // Add to room list
                {
                    int index = listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                    if (index == -1)
                    {
                        GameObject listingObject = Instantiate(roomListingPrefab, content);
                        var listing = listingObject.GetComponent<RoomListing>();
                        if (listing != null)
                        {
                            listing.SetRoomInfo(info);
                            listings.Add(listing);

                        }
                        else
                        {
                            Debug.LogError("There is not RoomListing Component in the gameobject "+listingObject.name);
                        }
                    }
                    else
                    {
                        // Modify Listing here
                    }
                    
                }
                
                
            }
        }

        #endregion
    }
}