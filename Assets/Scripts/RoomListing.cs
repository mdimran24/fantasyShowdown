using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class RoomListing : MonoBehaviour
{
   [SerializeField]
   private Text roomName;
   public RoomInfo roomInfo1 { get; private set;}

   public void SetRoomInfo(RoomInfo roomInfo){
       roomInfo1 = roomInfo;
       roomName.text = roomInfo.Name + " - " + roomInfo.MaxPlayers + " max players";
   }

   public void OnClickBtn(){
       PhotonNetwork.JoinRoom(roomInfo1.Name);
   }
}
