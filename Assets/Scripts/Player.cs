using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


    [Serializable]

public class Player : MonoBehaviour
{
    //ID of player, for future reference when using logins maybe.
    //same premise for the name.
    public int playerId;
    public string playerName;
    //TBA
    public int score;

//Cards in the form of gameobjects so that they can appear on screen.
     public List<GameObject> physicalCards = new List<GameObject>();

     //sorta temporary variable that makes it possible to add the cards on the list above.
     //Might be better if it was local.
    public GameObject playerCard;

    /*Not implemented yet. Determines if the player has pressed the Draw button. Should prevent them from
    drawing cards to infinity */
    public bool isDrawn = false;

    //The stat of the card that was selected.
     public int selectedVal;
     
    //The card that was selected, or rather the info in it.
      public ThisCard selectedCard;

      //??? Possibly refers to having to discard cards at the end of a turn.
    private int valtoberemoved;
    
   /* Represents the top left side of the game screen, where you drop the card you want to select 
   Is here instead to avoid any sort of interference.*/
    public GameObject selectGO;

  //number of cards in the player's hand.
private int numOfCards = 6;
   
  
    public List<Card> handOfCards = new List<Card>();

       public static bool HasBeenConfirmed = false;

    //photon view of the player.
    public PhotonView view;

    public void Start(){
        //currently the player's ID is just the ID of the Photon View.
      playerId = GetComponent<PhotonView>().ViewID;
      //Placeholders
        playerName = "player";
        score = 0;
    /* the aforementioned area for selected cards is tagged with "select". 
    This is a really handy way to retrieve objects without assiging them in the inspector
    and even without having them in the scene right away. */
        selectGO = GameObject.FindGameObjectWithTag("Select");

    }

 
 /*Remove a specified card from the player's hand.
 Looks through all the cards, sees if it matches, and if so, removes it. */
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

//Empties the player's hand.
    public void EmptyHand()
    {
        handOfCards.Clear();
    }

//Makes it that all the cards can appear on screen.
    public void InstantiateCards()
    {
          for (int i = 0; i< numOfCards; i++){
              playerCard = Instantiate(this.playerCard, new Vector3(0, 0, 0), Quaternion.identity);
        
          playerCard.GetComponent<ThisCard>().thisId = handOfCards[i].id;
         physicalCards.Add(playerCard);

          }
    }

   
/* Command that removes the card that has been selected once it's been confirmed. Its info is stored, but 
its physical manifestation is destroyed.*/
     public void RemoveUsedCard()
    {
      Destroy(selectGO.GetComponent<RectTransform>().GetChild(0).gameObject);
        
    }

//Makes the cards disappear on the screen, but the actual hand of cards is still there.
    public void emptyPhysical()
    {
        physicalCards.Clear();
      //  if (physicalCards.Count == 0) { }
      
    }
   
   
}


