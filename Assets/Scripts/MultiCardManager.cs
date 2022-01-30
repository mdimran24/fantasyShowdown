using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using ExitGames.Client.Photon;

//parts of the game in question
public enum RoundStates { START, PLAYERTURN, ENEMYTURN, WON, LOST, DRAW }

public class MultiCardManager : MonoBehaviour
{
 
    private bool isDrawn;
    
   
    public GameObject bottom;
    public GameObject opponentCards;
   // public GameObject Selector;

    public Player player;
    public Dealer dealer;
   

    bool HasBeenConfirmed;

 public Button ResetSelected;
    public Button ConfirmSelected;
    public static Button DrawBtn;

     public static string[] statuses;
    public static int chosenstat;



     protected void Awake()
    {
       
       
       
    }

    protected void Start()
    {
   
       Debug.Log( "Game Started");

    }
 
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


  
  public void Draw(){
      
 GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
         foreach (GameObject p in players){
             if (p.GetComponent<PhotonView>().IsMine){
                 player = p.GetComponent<Player>();
             }
         }
dealer.DealCards();
        player.GetComponent<Player>().InstantiateCards();
        for(int i = 0; i<player.GetComponent<Player>().physicalCards.Count; i++){
        player.GetComponent<Player>().physicalCards[i].transform.SetParent(bottom.transform, false);    
    }
    PlayerTurn();
    
    

     }

    private void PlayerTurn()
    {
      SelectCardPrep();
        Debug.Log("Pick your card bro");
       
    }

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

     public void OnResetPressed()
    {
        SelectedItemSlot.hasBeenSelected = false;
        GameObject cardTemp = player.selectGO.GetComponent<RectTransform>().GetChild(0).gameObject;
       cardTemp.GetComponent<CanvasGroup>().blocksRaycasts = true;
        cardTemp.transform.SetParent(bottom.transform, false);
      
    }

    public void OnConfirmPressed()
    {
         HasBeenConfirmed = true;
         ResetSelected.gameObject.SetActive(false);
         ConfirmSelected.gameObject.SetActive(false);

        if (player.selectGO.GetComponent<RectTransform>().childCount == 0)
        {
            Debug.Log("There is no card to confirm");
        }
        else
        {
            player.selectedCard = player.selectGO.GetComponent<RectTransform>().GetChild(0).GetComponent<ThisCard>();
            Debug.Log(player.selectedCard.cardName);
            onStatClick();
        }
    }

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

 