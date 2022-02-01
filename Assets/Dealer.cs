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
        PhotonPeer.RegisterType(typeof(Card), (byte)'W' ,Card.Serialize, Card.Deserialize);
        Debug.Log ( PhotonPeer.RegisterType(typeof(Card), (byte)'W' ,Card.Serialize, Card.Deserialize));
    
    }

 private void OnEnable() {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClientEventReceived;
    }

private void OnDisable() {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClientEventReceived;
    }

private void NetworkingClientEventReceived(EventData obj){
    if (obj.Code == PASSHAND){
        object[] datas = (object[]) obj.CustomData;

        List<Card> temp = new List<Card>();
        for (int i =0; i<6; i++){
            Card addon = (Card) datas[i];
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
            if (base.photonView.IsMine){
                p.GetComponent<Player>().handOfCards = playerDeck.GiveHand(6);
            }
           List<Card> otherplayer = playerDeck.GiveHand(6);
        object[] datas = new object[] { otherplayer[0], otherplayer[1], otherplayer[2], otherplayer[3], otherplayer[4], otherplayer[5]};
        PhotonNetwork.RaiseEvent(PASSHAND, datas, null, SendOptions.SendUnreliable);
        }
        
    }
  
}
    
