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
    public Player player;
    public Player enemy;

   

    public RoundState state;

    private void Awake()
    {
       

    }
    void Start()
    {
        state = RoundState.START;
        playerDeck.Shuffle();
        player.FillHand(5);

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
        if (player.controller.isDrawn)
        {
            StartCoroutine(SetupBattle());
           
        }
        
    }
   public void OnClickDraw()
    {
        
        if (!isDrawn)
        {
           
            for (int j = 0; j < player.handOfCards.Count; j++)
            {

                player.controller.InstantiateCards(j);
            }
        }
         
        isDrawn = true;
        StartCoroutine(SetupBattle());

        
    }

    IEnumerator PlayerTurn()
    {
        Debug.Log("Pick your stat bro");
        player.controller.statSel.gameObject.SetActive(true);
      
yield return new WaitForSeconds(1f);
    }

    IEnumerator EnemyTurn()
    {
     
        yield return new WaitForSeconds(1f);
        // EnemyMove();
        enemy.controller.InstantiateCards(1);

        player.controller.compareB.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);

      
    }

    IEnumerator Compare()
    {
        yield return new WaitForSeconds(1f); yield return new WaitForSeconds(1f);
        if (player.controller.selectedVal > enemy.controller.selectedVal)
        {
            state = RoundState.WON;
            EndRound();
        }
        else if (enemy.controller.selectedVal > player.controller.selectedVal)
        {
            state = RoundState.LOST;
            EndRound();
        } else if (enemy.controller.selectedVal == player.controller.selectedVal)
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
        
        player.controller.fetchStat(i);
        player.controller.statSel.gameObject.SetActive(false);
        player.controller.buttons = i;
       // ..playerController.selectedVal = playerController.selectedVal;
        Debug.Log("Button pressed was " + player.controller.buttons);
        Debug.Log(player.controller.selectedVal.ToString());
        state = RoundState.ENEMYTURN;
        StartCoroutine(EnemyTurn());

    }
  
    public void EnemyMove()
    {
     
        
          enemy.controller.fetchStat(player.controller.buttons);
       
        Debug.Log(enemy.controller.selectedVal);
        player.controller.compareB.gameObject.SetActive(false);
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
