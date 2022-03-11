using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class PlayerListing : MonoBehaviour
{
   [SerializeField]
   private Text playerName;
  
  public Photon.Realtime.Player PhotonPlayer { get; private set;}

   public void SetPlayerInfo(Photon.Realtime.Player player){
       PhotonPlayer = player;
        playerName.text = player.NickName;
   }
}
