using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;

public class Dealer : MonoBehaviourPun
{
 
[SerializeField]
    private PlayerDeck playerDeck;
    public GameObject deck;

    private const byte PASSDECK = 0;
    private const byte PASSHAND = 1;

    private void Awake() {
        if (PhotonNetwork.IsMasterClient){
            deck = PhotonNetwork.Instantiate("Deck", new Vector3(0, 0, 0), Quaternion.identity); 
             playerDeck = deck.GetComponent<PlayerDeck>(); 
           // object[] deckdata = new object[] {playerDeck};
           //  PhotonNetwork.RaiseEvent(PASSDECK, deckdata, null, SendOptions.SendUnreliable);
        }
    
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
    
