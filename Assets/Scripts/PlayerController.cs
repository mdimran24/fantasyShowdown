using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Physical manifestation of the game on the player's end
    public List<GameObject> physicalCards = new List<GameObject>();
    public GameObject playerCard;
    public GameObject playerHand;
    public GameObject selectGO;
    public bool isDrawn = false;
 
    // Physical manfestation of the stats to be chosen --- SHOULD NO LONGER BE IN THE CONTROL OF THE PLAYER !!!
      public GameObject statSel;
      public Button strB;
    public Text strT;
     public Button dexB;
    public Text dexT;
      public Button conB;
    public Text conT;
      public Button intlB;
    public Text intlT;
      public Button wisB;
    public Text wisT;
      public Button chaB;
    public Text chaT;
    
     public Button compareB;

    // Stores the details of the cards to be drawn / selected
     public int selectedVal;
    public ThisCard thisCard;
    public ThisCard selectedCard;

   

    // Mathod that instantiates the cards one by one and assigns them details
    public void InstantiateCards(int onebyone)
    {
            playerCard = Instantiate(playerCard, new Vector3(0, 0, 0), Quaternion.identity);
         physicalCards.Add(playerCard);
            playerCard.transform.SetParent(playerHand.transform, false);
            thisCard = playerCard.GetComponent<ThisCard>();
      //  strB.GetComponent<Text>().text = physicalCards[0].GetComponent<ThisCard>().cardName;
            
           
        }

   
  

    //Enables the player to select the stat -- MAKE IT RANDOMISED !!!
    public void fetchCard(int i)
    {
        /*   switch (i)
           {
               case 0: selectedVal = thisCard.strength; buttons = i; break;
               case 1: selectedVal = thisCard.dexterity; buttons = i; break;
               case 2: selectedVal = thisCard.constitution; buttons = i; break;
               case 3: selectedVal = thisCard.intelligence; buttons = i; break;
               case 4: selectedVal = thisCard.wisdom; buttons = i; break;
               case 5: selectedVal = thisCard.charisma; buttons = i; break;
               default: selectedVal = 0; buttons = i; break;
           }*/
        //this now fetches which card in the hand.
          switch (i)
        {
            case 0:selectedCard = physicalCards[i].GetComponent<ThisCard>();  break;
            case 1: selectedCard = physicalCards[i].GetComponent<ThisCard>(); break;
            case 2: selectedCard = physicalCards[i].GetComponent<ThisCard>(); break;
            case 3: selectedCard = physicalCards[i].GetComponent<ThisCard>();  break;
            case 4: selectedCard = physicalCards[i].GetComponent<ThisCard>();  break;
            case 5: selectedCard = physicalCards[i].GetComponent<ThisCard>(); break;
            default: selectedCard = physicalCards[i].GetComponent<ThisCard>(); break;
        }
       // Debug.Log()
       
    }

    // if you're not the one selecting the stats, on the enemy's behalf // MODIFY
    public void FetchCardIfNotLead()
    {

       //   selectedCard = playerCard.GetComponent<ThisCard>();
       // selectedCard.thisId = 

          
        
    }

}

  


