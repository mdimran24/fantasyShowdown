using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Dealer : MonoBehaviourPun
{
 
 //deck of cards that will be drawn from, playerDeck is the component we need.
 //might be handy to reorganise these vars.
[SerializeField]
    private PlayerDeck playerDeck;
    public GameObject deck;

    private void Awake() {
        playerDeck = deck.GetComponent<PlayerDeck>();
    }

  


//fills the hands of the players.
    public void DealCards(){
      
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in players){
            //Making sure it's you.
            if (p.GetComponent<PhotonView>().IsMine){
           
                p.GetComponent<Player>().handOfCards = playerDeck.giveHand();
        
            }
        }
        
    }
  
}
    
