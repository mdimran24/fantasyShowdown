using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject playerCard;
    public GameObject playerHand;
    public GameObject selectGO;
    public bool isDrawn = false;
 

      public GameObject statSel;
      public Button strB;
     public Button dexB;
      public Button conB;
      public Button intlB;
      public Button wisB;
      public Button chaB;
     public int buttons;
     public Button compareB;

     public int selectedVal;
    public ThisCard thisCard;
    public ThisCard selectedCard;

   


    public void InstantiateCards(int onebyone)
    {
      
            playerCard = Instantiate(playerCard, new Vector3(0, 0, 0), Quaternion.identity);
         
            playerCard.transform.SetParent(playerHand.transform, false);
            thisCard = playerCard.GetComponent<ThisCard>();
          thisCard.thisId = Random.Range(0, PlayerDeck.deckSize);
           
        }

    public void OnSelected()
    {
       Image cardgraphic= playerCard.GetComponent<Image>();
        cardgraphic.color = Color.red;
    }
    public void fetchStat(int i)
    {
        switch (i)
        {
            case 0: selectedVal = thisCard.strength; buttons = i; break;
            case 1: selectedVal = thisCard.dexterity; buttons = i; break;
            case 2: selectedVal = thisCard.constitution; buttons = i; break;
            case 3: selectedVal = thisCard.intelligence; buttons = i; break;
            case 4: selectedVal = thisCard.wisdom; buttons = i; break;
            case 5: selectedVal = thisCard.charisma; buttons = i; break;
            default: selectedVal = 0; buttons = i; break;
        }
        // statSel.gameObject.SetActive(false);
        // buttons = i;
        // Debug.Log("Button pressed was " + buttons);
        //  Debug.Log(selectedVal.ToString());
        //  state = RoundState.ENEMYTURN;
        //  StartCoroutine(EnemyTurn());

    }

    public void FetchStatIfNotLead()
    {

            switch (buttons)
            {
            case 0: selectedVal = thisCard.strength; break;
            case 1: selectedVal = thisCard.dexterity; break;
            case 2: selectedVal = thisCard.constitution; break;
            case 3: selectedVal = thisCard.intelligence;  break;
            case 4: selectedVal = thisCard.wisdom; break;
            case 5: selectedVal = thisCard.charisma; break;
            default:
                selectedVal = 0; break;
            }

          
        
    }

}

  


