using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Rooms
{
    public class RoomListingMenu: MonoBehaviourPunCallbacks
    {
        public Transform content;
        public GameObject roomListingPrefab;

        public List<RoomListing> _listings = new List<RoomListing>();
        
        
        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            foreach (RoomInfo info in roomList)
            {
                if (info.RemovedFromList)
                {
                    int indexToRemove = _listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                    if (indexToRemove != -1)
                    {
                        Destroy(_listings[indexToRemove].gameObject);
                        _listings.RemoveAt(indexToRemove);
                    }
                }
                else
                {
                    print("add room listing");
                    GameObject listingObject = Instantiate(roomListingPrefab, content);
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
                
                
            }
        }
    }
}