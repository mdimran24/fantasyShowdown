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

 private void OnEnable() {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClientEventReceived;
    }

private void OnDisable() {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClientEventReceived;
    }

private void NetworkingClientEventReceived(EventData obj){
    if (obj.Code == PASSHAND){
        Card[] datas = (Card[]) obj.CustomData;

        List<Card> temp = new List<Card>();
        for (int i =0; i<6; i++){
            Card addon = datas[i];
            temp.Add(addon);
        }

       GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in players){
            if (!p.GetComponent<PhotonView>().IsMine){
                p.GetComponent<Player>().handOfCards = temp;
            }
        }
    }
}
    public void Dealbtn(){
       if (PhotonNetwork.IsMasterClient){
          DealCards();
       }
          // base.photonView.RPC("DealCards", RpcTarget.All);
         
       
   }


    public void DealCards(){
      
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
       // Debug.Log(players.Length);

        foreach (GameObject p in players){
            if (p.GetComponent<PhotonView>().IsMine && PhotonNetwork.IsMasterClient){
                p.GetComponent<Player>().handOfCards = playerDeck.GiveHand(6);
            }
           List<Card> otherplayer = playerDeck.GiveHand(6);
       // Object[] datas = otherplayer.ToArray();
       // PhotonNetwork.RaiseEvent(PASSHAND, datas, null, SendOptions.SendReliable);
        }
        
    }
  
}
    
