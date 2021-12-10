using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerController : MonoBehaviourPun
{
    //Physical manifestation of the game on the player's end
    public List<GameObject> physicalCards = new List<GameObject>();
    public GameObject playerCard;
    public GameObject playerHand;
    public GameObject selectGO;
    public bool isDrawn = false;

    public Button compareB;
    

    // Stores the details of the cards to be drawn / selected
    public int selectedVal;
    private int valtoberemoved;
    public ThisCard thisCard;
    public ThisCard selectedCard;

    public static bool HasBeenConfirmed = false;

    public PhotonView view;

   

    // Mathod that instantiates the cards one by one and assigns them details
    public void InstantiateCards(int onebyone)
    {
        if (view.IsMine){
        playerCard = Instantiate(playerCard, new Vector3(0, 0, 0), Quaternion.identity);
        physicalCards.Add(playerCard);
        playerCard.transform.SetParent(playerHand.transform, false);
        thisCard = playerCard.GetComponent<ThisCard>();
       } else{
           Debug.Log("You messed something up with the ismine stuff");
        }
       


    }




    //Enables the player to select the stat -- MAKE IT RANDOMISED !!!
    public void fetchCard(int i)
    {
        valtoberemoved = i;
        //this now fetches which card in the hand.
        switch (i)
        {
            case 0: selectedCard = physicalCards[i].GetComponent<ThisCard>(); break;
            case 1: selectedCard = physicalCards[i].GetComponent<ThisCard>(); break;
            case 2: selectedCard = physicalCards[i].GetComponent<ThisCard>(); break;
            case 3: selectedCard = physicalCards[i].GetComponent<ThisCard>(); break;
            case 4: selectedCard = physicalCards[i].GetComponent<ThisCard>(); break;
            case 5: selectedCard = physicalCards[i].GetComponent<ThisCard>(); break;
            default: selectedCard = physicalCards[i].GetComponent<ThisCard>(); break;
        }
      

    }

    public void RemoveUsedCard()
    {
        Destroy(selectGO.GetComponent<RectTransform>().GetChild(0).gameObject);
        
    }

   

 

    public void onStatClick()
    {

        //  player.controller.fetchCard(i);
        //  player.controller.statSel.gameObject.SetActive(false);
        if (selectedCard != null)
        {
            switch (CardManager.statuses[CardManager.chosenstat])
            {
                case "strength": selectedVal = selectedCard.strength; break;
                case "dexterity": selectedVal = selectedCard.dexterity; break;
                case "constitution": selectedVal = selectedCard.constitution; break;
                case "intelligence": selectedVal = selectedCard.intelligence; break;
                case "wisdom": selectedVal = selectedCard.wisdom; break;
                case "charisma": selectedVal = selectedCard.charisma; break;
                default: selectedVal = 0; break;
            }
        }

    }
}

  


