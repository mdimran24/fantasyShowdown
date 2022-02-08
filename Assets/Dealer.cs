using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;

public class Dealer : MonoBehaviourPun
{
 
[SerializeField]
    private PlayerDeck playerDeck;
    // Deck of cards that will be drawn from. the playerDeck above is just the component taken from it
    public GameObject deck;

    //RaiseEvent codes, identifies the method in question
    private const byte PASSDECK = 0;
    private const byte PASSHAND = 1;

    
    private void Awake() {
        if (PhotonNetwork.IsMasterClient){
            //Creates the deck only for the master, the cards are shared across the network
            deck = PhotonNetwork.Instantiate("Deck", new Vector3(0, 0, 0), Quaternion.identity); 
            playerDeck = deck.GetComponent<PlayerDeck>(); 
         
        }
    }

// Event is received if the object listens for it. Otherwise when OnDisable.
 private void OnEnable() {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClientEventReceived;
    }

private void OnDisable() {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClientEventReceived;
    }


//Identifies what code is received and acts accordingly
private void NetworkingClientEventReceived(EventData obj){
    //If the code is PASSHAND (Passes hard to other player) AND it's not the master client listening for them
    if (obj.Code == PASSHAND && !PhotonNetwork.IsMasterClient){
        Debug.Log("Received event: " + obj);
        //extract the data and put it into an array
        object[] datas = (object[]) obj.CustomData;
        //create temporary hand of cards to pass over to the player.
        List<Card> temp = new List<Card>();
        for (int i =0; i<3; i++){
            //fill the hand with new blank cards that take in the datas[] elements for IDs
            //turns into actual cards once instantiated.
            //rest of the fields are irellevant
            Card addon = new Card((int) datas[i], "test", (int) datas[i], 0, 0, 0, 0, 0, null);
            temp.Add(addon);
        }
        //find players currently in game
       GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in players){
            PhotonView playerView = p.GetComponent<PhotonView>();
            //if that player is you
            if (playerView.IsMine){
                Debug.Log("Received hand from master client: " + temp);
                //turn the temp hand of cards into YOUR hand of cards
                p.GetComponent<Player>().handOfCards = temp;
            }
        }
    }
}
    public void Dealbtn(){
        //Only the master client can press this. 
        //pressing it as a non-master client will do nothing.
       if (PhotonNetwork.IsMasterClient){
          DealCards();
       }
   }


    public void DealCards(){
        //base.photonView.IsMine refers to the master!!!!
        if (!base.photonView.IsMine){
            return;
        }
      
        // look for all the players currently in the scene
        //as you can see, Player objects have a "player" tag
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
     
        //temporary array of integers to put the IDs to be passed in
        //IDs represent cards to be sent to the other player
        int[] numstopasstest = new int[6];
        foreach (GameObject p in players){
            PhotonView v = p.GetComponent<PhotonView>();
            //if this is you out of the players
            if (v.IsMine) {
                //you as ther master have access to the deck, fill hand as normal
                p.GetComponent<Player>().handOfCards = playerDeck.GiveHand(6);
            } else {
                //if this is not you out of the players
                //create a temporary list of cards
                 List<Card> otherplayer = playerDeck.GiveHand(6);
                 for (int i=0; i<6; i++){
                     //take the IDs of those cards and put them into the array
                     numstopasstest[i] = otherplayer[i].id;
                 } 
                 //put the elements of said array into a datas array to be specifically sent across the network
                 object[] datas = new object[] {numstopasstest[0], numstopasstest[1], numstopasstest[2], numstopasstest[3], numstopasstest[4], numstopasstest[5]};
                 Debug.Log("raising event with datas " + datas);
                 //Actually send it to the other player.
                 PhotonNetwork.RaiseEvent(PASSHAND, datas, null, SendOptions.SendReliable);
            }
        }

        }

    }
  


    
