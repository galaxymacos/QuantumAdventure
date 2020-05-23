using System.Collections.Generic;
using Helpers;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Rooms
{
    public class RoomListingsMenu: MonoBehaviourPunCallbacks
    {
        [SerializeField] private Transform _content;
        public GameObject roomListingPrefab;

        public RoomCanvases _roomCanvases;
        
        public List<RoomListing> _listings = new List<RoomListing>();

        public void FirstInitialize(RoomCanvases roomCanvases)
        {
            _roomCanvases = roomCanvases;
        }
        
        public override void OnJoinedRoom()
        {
            _roomCanvases.currentRoomCanvas.Show();
            _content.DestroyChildren();
            _listings.Clear();
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            foreach (RoomInfo info in roomList)
            {
                // Remove from room list
                if (info.RemovedFromList)
                {
                    int indexToRemove = _listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                    if (indexToRemove != -1)
                    {
                        Destroy(_listings[indexToRemove].gameObject);
                        _listings.RemoveAt(indexToRemove);
                    }
                }
                else // Add to room list
                {
                    int index = _listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                    if (index == -1)
                    {
                        GameObject listingObject = Instantiate(roomListingPrefab, _content);
                        var listing = listingObject.GetComponent<RoomListing>();
                        if (listing != null)
                        {
                            listing.SetRoomInfo(info);
                            _listings.Add(listing);

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
    }
}