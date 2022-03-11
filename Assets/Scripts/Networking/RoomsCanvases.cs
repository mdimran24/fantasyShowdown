using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsCanvases : MonoBehaviour
{
   [SerializeField]
   private CreateOrJoinRoomCanvas createOrJoinCanvas;
   public CreateOrJoinRoomCanvas createOrJoinRoomCanvas { get { return createOrJoinCanvas; } }

[SerializeField]
   private CurrentRoomCanvas currentRoom;
   public CurrentRoomCanvas currentRoomCanvas { get {return currentRoom;}}

   private void Awake(){
FirstInitialize();
   }

   private void FirstInitialize(){
       createOrJoinCanvas.FirstInitialize(this);
       currentRoom.FirstInitialize(this);
   }
}
