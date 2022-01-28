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
        if (PhotonNetwork.IsMasterClient){
            deck = PhotonNetwork.Instantiate("Deck", new Vector3(0, 0, 0), Quaternion.identity); 
        }
     base.photonView.RPC("setup", RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void setup(){
        // deck = PhotonNetwork.Instantiate("Deck", new Vector3(0, 0, 0), Quaternion.identity);
        playerDeck = deck.GetComponent<PlayerDeck>(); 
    }

   public void Dealbtn(){
          //DealCards();
           base.photonView.RPC("DealCards", RpcTarget.All);
         
       
   }

[PunRPC]
    public void DealCards(){
      
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
       // Debug.Log(players.Length);

        foreach (GameObject p in players){
            //if (p.GetComponent<PhotonView>().IsMine){
            for (int i = 0; i< 6; i++){
                Card dealt = playerDeck.GrabShuffledCard();
                p.GetComponent<Player>().handOfCards.Add(dealt);
            }
           // }
        }
        
    }
  
}
    
