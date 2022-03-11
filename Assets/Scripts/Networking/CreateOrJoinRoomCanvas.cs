using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CreateOrJoinRoomCanvas : MonoBehaviour
{
    [SerializeField]
    private CreateAndJoinRooms createAndJoinRooms;
    private RoomsCanvases roomsCanvases;
   public void FirstInitialize(RoomsCanvases canvases){
       roomsCanvases = canvases;
       createAndJoinRooms.FirstInitialize(canvases);
   }
}
