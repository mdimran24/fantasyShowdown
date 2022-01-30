using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Dealer : MonoBehaviourPun
{
 
[SerializeField]
    private PlayerDeck playerDeck;
    public GameObject deck;

    private void Awake() {
        playerDeck = deck.GetComponent<PlayerDeck>();
    }

  

   public void Dealbtn(){
         // DealCards();
          base.photonView.RPC("DealCards", RpcTarget.All);
         
       
   }

[PunRPC]
    public void DealCards(){
      
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
       // Debug.Log(players.Length);

        foreach (GameObject p in players){
            if (p.GetComponent<PhotonView>().IsMine){
           
                p.GetComponent<Player>().handOfCards = playerDeck.giveHand();
        
            }
        }
        
    }
  
}
    
