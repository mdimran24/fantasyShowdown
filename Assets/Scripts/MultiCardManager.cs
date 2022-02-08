using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using ExitGames.Client.Photon;

//parts of the game in question
//CRINA: I HAVE EXTRA PARTS IN ANOTHER BRANCH, HANG ON.
public enum RoundStates { START, PLAYERTURN, ENEMYTURN, WON, LOST, DRAW }

public class MultiCardManager : MonoBehaviour
{
    //Represents the area that holds the player's cards, at the bottom of the screen
    public GameObject bottom;

    //Upper RHS of screen where it represents all enemy cards
    public GameObject opponentCards;

       /*the player in question. Now note that both ends of the game basically have individual copies of this script
    and only the photon-based code actually deals with sharing info across the network. Therefore there is only one player
    variable as on this end, it represents you and on their end, it represents them. */
    public Player player;

    //The reset button, meaning that their selected card goes back onto their hand.
 public Button ResetSelected;
 //the confirm button meaning that the player has decided on which card to play
    public Button ConfirmSelected;

      //represents that the player has selected their card
    bool HasBeenConfirmed;


    //HUD messenger text
    public Text Messenger;

       //list of statuses that will be selected from
     public static string[] statuses;
     //the status that has been picked
    public static int chosenstat;


    protected void Start()
    {
    
        Messenger.text = "Game Started";

    }
 
 /* Makes sure the card that has been dragged onto the select area can no longer be clickable
 And also that the reset and confirm buttons don't appear when a card is not selected yet or 
 when a card has already been played. */
  private void Update()
    {
        if (SelectedItemSlot.hasBeenSelected && !HasBeenConfirmed)
        {
            ResetSelected.gameObject.SetActive(true);
            ConfirmSelected.gameObject.SetActive(true);


        } else
        {
            ResetSelected.gameObject.SetActive(false);
            ConfirmSelected.gameObject.SetActive(false);
            
        }
     
    }

    IEnumerator SetupBattle()
    {
        Debug.Log("Game started.");
       // yield return new WaitForSeconds(1f);
       // state = RoundState.PLAYERTURN;
       // StartCoroutine(PlayerTurn());
        yield break;
    }


  //This button provides cards for the player who clicks it
  public void Draw(){
      //look for players
     GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
    foreach (GameObject p in players){
        //if this player is you, instantiate your cards and place them as needed.
        if(p.GetComponent<PhotonView>().IsMine){
              player = p.GetComponent<Player>();
            p.GetComponent<Player>().InstantiateCards();
            for(int i = 0; i<p.GetComponent<Player>().physicalCards.Count; i++){
                p.GetComponent<Player>().physicalCards[i].transform.SetParent(bottom.transform, false);
            }
        }
    }
    PlayerTurn();
    
     }

     
    private void PlayerTurn()
    {
      SelectCardPrep();
        Debug.Log("Pick your card bro");
       
    }

    
    //creates the stat array and picks one at random, then lets you know about it.
     public void SelectCardPrep()
    {
       statuses = new string[6];
        statuses[0] = "strength";
        statuses[1] = "dexterity";
        statuses[2] = "constitution";
        statuses[3] = "intelligence";
        statuses[4] = "wisdom";
        statuses[5] = "charisma";
        chosenstat = Random.Range(0, statuses.Length);
        Debug.Log("Status is:" + statuses[chosenstat]);
        //Messenger.text = "Status is: " + statuses[chosenstat].ToString();

    }
      
  // removes the card from the selected area and puts it back onto your hand.
     public void OnResetPressed()
    {
        SelectedItemSlot.hasBeenSelected = false;
        GameObject cardTemp = player.selectGO.GetComponent<RectTransform>().GetChild(0).gameObject;
       cardTemp.GetComponent<CanvasGroup>().blocksRaycasts = true;
        cardTemp.transform.SetParent(bottom.transform, false);
      
    }

    // the card disappears and is now stored to be played against your component
    // YOU CANNOT UNDO THIS.
    public void OnConfirmPressed()
    {
        //confirms you have indeed drawn your card. Should move this to the player class.
         HasBeenConfirmed = true;
         //makes aforementioned buttons disappear.
         ResetSelected.gameObject.SetActive(false);
         ConfirmSelected.gameObject.SetActive(false);

        //if your pressed the confirm button while there was no card inside.
        if (player.selectGO.GetComponent<RectTransform>().childCount == 0)
        {
            Debug.LogError("There is no card to confirm");
        }
        else
        {
            //marks the card inside the area as the one to be played.
            player.selectedCard = player.selectGO.GetComponent<RectTransform>().GetChild(0).GetComponent<ThisCard>();
            Debug.Log(player.selectedCard.cardName);
            //advances the game
            onStatClick();
        }
    }

       /*picks the stat that has been picked from the card you have selected,
    then lets you know of it.
    Suggestion: create a method that returns a ThisCard object that contains the
    same info as the selected card in order to be used in a RaiseEvent method.
    Additional: A method that intantiated this card to the opponent, with the face down and all. */
      public void onStatClick()
    {
      
        if (player.selectedCard != null)
        {
            switch (statuses[chosenstat])
            {
                case "strength": player.selectedVal = player.selectedCard.strength; break;
                case "dexterity": player.selectedVal = player.selectedCard.dexterity; break;
                case "constitution": player.selectedVal = player.selectedCard.constitution; break;
                case "intelligence": player.selectedVal = player.selectedCard.intelligence; break;
                case "wisdom": player.selectedVal = player.selectedCard.wisdom; break;
                case "charisma": player.selectedVal = player.selectedCard.charisma; break;
                default: player.selectedVal = 0; break;
            }
        }
       
        Debug.Log("Card picked was " + player.selectedCard.cardName);
        Debug.Log(player.selectedVal.ToString());

    
       // GameObject testcard = PhotonNetwork.Instantiate("Card", new Vector3(0, 0, 0), Quaternion.identity);
       // testcard.transform.SetParent(opponentCards.transform, false);
        //testcard.GetComponent<ThisCard>().thisId = player.selectedCard.thisId;
    
    }

  }

 