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
    if (obj.Code == PASSHAND && !PhotonNetwork.IsMasterClient){
        Debug.Log("Received event: " + obj);
        object[] datas = (object[]) obj.CustomData;

        List<Card> temp = new List<Card>();
        for (int i =0; i<3; i++){
            Card addon = new Card((int) datas[i], "test", (int) datas[i], 0, 0, 0, 0, 0, null);
            temp.Add(addon);
        }

       GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in players){
            PhotonView playerView = p.GetComponent<PhotonView>();
            if (playerView.IsMine){
                Debug.Log("Received hand from master client: " + temp);
                p.GetComponent<Player>().handOfCards = temp;
            }
        }
    }
}
    public void Dealbtn(){
       if (PhotonNetwork.IsMasterClient){
          DealCards();
       }
   }


    public void DealCards(){
        if (!base.photonView.IsMine){
            return;
        }
      
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
       // Debug.Log(players.Length);
        int[] numstopasstest = new int[3];
        foreach (GameObject p in players){
            PhotonView v = p.GetComponent<PhotonView>();
            if (v.IsMine) {
                p.GetComponent<Player>().handOfCards = playerDeck.GiveHand(3);
            } else {
                 List<Card> otherplayer = playerDeck.GiveHand(3);
                 for (int i=0; i<3; i++){
                     numstopasstest[i] = otherplayer[i].id;
                 } 
                 object[] datas = new object[] {numstopasstest[0], numstopasstest[1], numstopasstest[2]};
                 Debug.Log("raising event with datas " + datas);
                 PhotonNetwork.RaiseEvent(PASSHAND, datas, null, SendOptions.SendReliable);
            }
        }

        }

    }
  


    
