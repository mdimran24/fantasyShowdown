using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.IO;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public Text create;

       private RoomsCanvases roomsCanvases;
       public CheckForProfanities checker;
   public void FirstInitialize(RoomsCanvases canvases){
       roomsCanvases = canvases;
       checker = GetComponent<CheckForProfanities>();
   }

    public void OnClickCreateRoom()
    {
        if (!PhotonNetwork.IsConnected){
            return;
           
        } 
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;
        if (checker.CheckforProfanities()){
            Debug.Log("You cannot use profanities in your room name!");
            StartCoroutine(checker.Popup());
            return;
        } else {
             PhotonNetwork.JoinOrCreateRoom(create.text, options, TypedLobby.Default);
        }
      
      
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
