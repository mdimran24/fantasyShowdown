using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomListings : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private RoomListing roomListing;
    [SerializeField]
    private Transform content;

      private List<RoomListing> listings = new List<RoomListing>();

      private RoomsCanvases _roomsCanvases;

    public void FirstInitialize(RoomsCanvases canvases){
         _roomsCanvases = canvases;
     }
    public override void OnJoinedRoom()
    {
        _roomsCanvases.currentRoomCanvas.Show();
        content.DestroyChildren();
        listings.Clear();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            if (info.RemovedFromList)
            {
                int index = listings.FindIndex(x => x.roomInfo1.Name == info.Name);
               if (index != -1){
                  Destroy(listings[index].gameObject); 
                  listings.RemoveAt(index);
               } 
            }
            else
            {
                
                int index = listings.FindIndex(x => x.roomInfo1.Name == info.Name);
                if (index == -1){
                     RoomListing listing = Instantiate(roomListing, content);
                    if (listing != null)
                    {
                    listing.SetRoomInfo(info);
                    listings.Add(listing);
                     }
                } 
               
            }

        }
    }
}
