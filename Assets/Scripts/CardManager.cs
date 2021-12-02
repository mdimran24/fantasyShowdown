using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum RoundState { START, PLAYERTURN, ENEMYTURN, WON, LOST, DRAW }

public class CardManager : MonoBehaviour
{

   // public Card[] pack;

 //   public GameObject playerCard;
  //  public GameObject playerHand;
  //  public GameObject selectGO;
    private bool isDrawn = false;
    public PlayerDeck playerDeck;
    public PlayerController playerController;

    public GameObject statSel;
    public Button strB;
    public Button dexB;
    public Button conB;
    public Button intlB;
    public Button wisB;
    public Button chaB;
    private int buttons;
    public Button compareB;

    private int selectedVal;
    private int enemyVal;

    public GameObject enemyCard;
    public GameObject enemyArea;

   // private ThisCard thisCard;
    private ThisCard enemyDisplayer;

    public RoundState state;

    void Start()
    {
        state = RoundState.START;
      
    }

    

    IEnumerator SetupBattle()
    {
        Debug.Log("Game started.");
        yield return new WaitForSeconds(1f);
        state = RoundState.PLAYERTURN;
        StartCoroutine(PlayerTurn());
    }

   public void CheckForDrawn()
    {
        if (playerController.isDrawn)
        {
            StartCoroutine(SetupBattle());
           
        }
        
    }
   public void OnClickDraw()
    {
       // for (var i = 0; i < 4; i++)
        //{
        if (!isDrawn)
        {
            
          //  playerController.playerCard = Instantiate(playerController.playerCard, new Vector3(0, 0, 0), Quaternion.identity);
          //  playerController.playerCard.transform.SetParent(playerController.playerHand.transform, false);
          //  playerController.thisCard = playerController.playerCard.GetComponent<ThisCard>();
          //  playerController.thisCard.thisId = Random.Range(0, playerDeck.deckSize);
               // thisCard. = playerDeck.deck[i];
               playerController.InstantiateCards();
         
     //   }
         }
        isDrawn = true;
        StartCoroutine(SetupBattle());

        
    }

    IEnumerator PlayerTurn()
    {
        Debug.Log("Pick your stat bro");
        statSel.gameObject.SetActive(true);
      
yield return new WaitForSeconds(1f);
    }

    IEnumerator EnemyTurn()
    {
     
        yield return new WaitForSeconds(1f);
        // EnemyMove();
        enemyCard = Instantiate(enemyCard, new Vector3(0, 0, 0), Quaternion.identity);
        enemyCard.transform.SetParent(enemyArea.transform, false);
         enemyDisplayer = enemyCard.GetComponent<ThisCard>();
        enemyDisplayer.thisId = Random.Range(0, playerDeck.deckSize);

        compareB.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);

      
    }

    IEnumerator Compare()
    {
        yield return new WaitForSeconds(1f); yield return new WaitForSeconds(1f);
        if (selectedVal > enemyVal)
        {
            state = RoundState.WON;
            EndRound();
        }
        else if (enemyVal > selectedVal)
        {
            state = RoundState.LOST;
            EndRound();
        } else if (enemyVal == selectedVal)
        {
            state = RoundState.DRAW;
            EndRound();
        }

    }

    public void Fetchstats()
    {
       
    }

   public void onStatClick(int i)
    {
        
        playerController.fetchStat(i);
        statSel.gameObject.SetActive(false);
        buttons = i;
        selectedVal = playerController.selectedVal;
        Debug.Log("Button pressed was " + buttons);
        Debug.Log(selectedVal.ToString());
        state = RoundState.ENEMYTURN;
        StartCoroutine(EnemyTurn());

    }
  
    public void EnemyMove()
    {
     
          switch (buttons)
           {
               case 0: enemyVal = enemyDisplayer.strength; break;
               case 1: enemyVal = enemyDisplayer.dexterity; break;
               case 2: enemyVal = enemyDisplayer.constitution; break;
               case 3: enemyVal = enemyDisplayer.intelligence; break;
               case 4: enemyVal = enemyDisplayer.wisdom; break;
               case 5: enemyVal = enemyDisplayer.charisma; break;
               default: enemyVal = 0; break;
           } 
       
        Debug.Log(enemyVal);
        compareB.gameObject.SetActive(false);
        StartCoroutine(Compare());
    }

    public void EndRound()
    {
        if (state == RoundState.WON)
        {
            Debug.Log("You won. Commencing victory cry.");
           new WaitForSeconds(3f);
          // Application.OpenURL("https://www.youtube.com/watch?v=sAXZbfLzJUg");
        }
        else if (state == RoundState.LOST)
        {
            Debug.Log("You lost.");
        } else if (state == RoundState.DRAW)
        {
            Debug.Log("It's a draw!");
        }

    }

}
