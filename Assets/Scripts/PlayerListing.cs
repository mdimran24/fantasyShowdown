using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class PlayerListing : MonoBehaviour
{
   [SerializeField]
   private Text playerName;
 
   public Image master;
 
   public Image readyicon;
  
  public Photon.Realtime.Player PhotonPlayer { get; private set;}
  public bool Ready = false;

   public void SetPlayerInfo(Photon.Realtime.Player player){
       PhotonPlayer = player;
        playerName.text = player.NickName;

        if (PhotonNetwork.CurrentRoom.MasterClientId == player.ActorNumber){
           master.gameObject.SetActive(true);
           Debug.Log("You're the room owner");
        }

   }
}
