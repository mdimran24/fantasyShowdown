using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public Text create;

       private RoomsCanvases roomsCanvases;
   public void FirstInitialize(RoomsCanvases canvases){
       roomsCanvases = canvases;
   }

    public void OnClickCreateRoom()
    {
        if (!PhotonNetwork.IsConnected){
            return;
           
        } 
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom(create.text, options, TypedLobby.Default);
      
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("created room successfully", this);
        roomsCanvases.currentRoomCanvas.Show();

      
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
          Debug.Log("room creation failed" + message , this);
        
    }
}
