using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


    [Serializable]

public class Player : MonoBehaviour, IPunObservable
{

    //Payer identification
    public int playerId;

    //player's name and score, will be relevant later on.
    public string playerName;
    public int score;

    //hand of cards but represented as card gameobjects
     public List<GameObject> physicalCards = new List<GameObject>();

    // acts as temporary card for several methods. There's perhaps a better place to put it.
    public GameObject playerCard;

    //WILL BE USED, determined whether the player has drawn their cards or not
    //prevents them from instantiating cards to infinity.
    public bool isDrawn = false;

    public int winnerstreak;

    //WILL BE USED
    //Value of the selected status of the selected card
     public int selectedVal;
     //UNSURE
    private int valtoberemoved;

  /* Represents the top left side of the game screen, where you drop the card you want to select 
   Is here instead to avoid any sort of interference.*/
    public GameObject selectGO;

    //Also a temp variable
    public ThisCard thisCard;
    //Represents details of the card that has been selected
    public ThisCard selectedCard;

//number of cards that each player will have in their hand. Should be 6
//also look into it to avoid magic numbers
public int numOfCards = 6;
   
    // Players hand of cards
    public List<Card> handOfCards = new List<Card>();

    //If the player has already selected the card to play with.
       public bool HasBeenConfirmed = false;

    //Network identity of object
    public PhotonView view;

    //player's ID is the same as the ID of the Photon view
    public void Start(){
      playerId = GetComponent<PhotonView>().ViewID;
        playerName = "player";  
        score = 0;
           /* the aforementioned area for selected cards is tagged with "select". 
    This is a really handy way to retrieve objects without assiging them in the inspector
    and even without having them in the scene right away. */
        selectGO = GameObject.FindGameObjectWithTag("Select");
    }

    //Removes a specific card by finding its ID (Now sure where it's useable??)
    public void Removefromcardid(int cardid)
    {
        for (int i = 0; i < 6; i++)
        {
            if (handOfCards[i].id == cardid)
            {
                handOfCards.RemoveAt(i);
            }
        }
    }

    //empty the player's hand
    public void EmptyHand()
    {
        handOfCards.Clear();
    }

    //turn the player's hand of cards into tangible form
    public void InstantiateCards()
    {
          for (int i = 0; i< numOfCards; i++){
              playerCard = Instantiate(this.playerCard, new Vector3(0, 0, 0), Quaternion.identity);
        
        playerCard.GetComponent<ThisCard>().thisId = handOfCards[i].id;
         physicalCards.Add(playerCard);
          }
    }

   
    //WILL BE USED LATER
    //Once a round has ended, this command is used to discard the card that has already been played
     public void RemoveUsedCard()
    {
        int index = handOfCards.FindIndex(x => x.id == selectedCard.id);
        handOfCards.RemoveAt(index);
        selectedCard = null;
        if (view.IsMine){
        Destroy(selectGO.GetComponent<Transform>().GetChild(0).gameObject);
        }
       

        
    }

    //WILL BE USED LATER
    //Makes the physical cards disappear, but the actual cards are still there.
    public void emptyPhysical()
    {
        physicalCards.Clear();
      //  if (physicalCards.Count == 0) { }
      
    }

    //IMPORTANT
    //MAKES HAND SYNCHRONISATION POSSIBLE
    //Essentially makes it possible for Photon to identify players to send events to
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) {
           // Debug.Log("Sending number of cards for " + this.playerId + ": " + handOfCards.Count);
            stream.SendNext(handOfCards.Count);
            foreach (Card c in handOfCards) {
               // Debug.Log($"sending card ID for {this.playerId}: {c.id}");
                stream.SendNext(c.id);
            }

            stream.SendNext(HasBeenConfirmed);
            stream.SendNext(selectedVal);
            stream.SendNext(winnerstreak);
           //  Debug.LogError("Sending stat for" + MultiCardManager.chosenstat + ": " + selectedVal);
        
        } else {
            int nCards = (int) stream.ReceiveNext();
           // Debug.Log("Received number of cards for " + this.playerId + ": " + nCards);
            if (nCards != handOfCards.Count) {
                handOfCards.Clear();
                for (int i = 0; i < nCards; i++) {
                    handOfCards.Add(new Card(0, "dummy", 0, 0, 0, 0, 0, 0, null));
                }
            }


                 
           
            for (int i = 0; i< nCards; i++) {
                int cardId = (int) stream.ReceiveNext();
              //  Debug.Log("Received card ID for " + this.playerId + ": " + cardId);
                if (handOfCards[i].id != cardId) {
                    handOfCards[i].id = cardId;
                    // update anything else in the card...
                }
            }
             HasBeenConfirmed = (bool) stream.ReceiveNext();
            
                 selectedVal = (int) stream.ReceiveNext();
            winnerstreak = (int) stream.ReceiveNext();
           //  Debug.LogError("Received stat for" + MultiCardManager.chosenstat + ": " + selectedVal);
            
        }
    }
}


