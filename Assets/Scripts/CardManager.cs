using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//parts of the game in question
public enum RoundState { START, PLAYERTURN, ENEMYTURN, WON, LOST, DRAW }

public class CardManager : MonoBehaviour
{
    //Has the player drawn their cards (offline)
    private bool isDrawn = false;
    // deck where the players gonna draw the cards from
    public PlayerDeck playerDeck;
    // players in question
    public Player player;
    public Player enemy;

    public string[] statuses;
    public int chosenstat;


    //defines what part of the game we in
    public RoundState state;


    //Claims the game as started, shuffles cards, fills the player's hand with cards (should be for the enemy as well next time)
    void Start()
    {
        state = RoundState.START;
        playerDeck.Shuffle();
        player.FillHand(6);
        enemy.FillHand(1);
        Debug.Log(PlayerDeck.shuffleDeck.Count);

        player.controller.strT.text = player.handOfCards[0].cardName;
        player.controller.dexT.text = player.handOfCards[1].cardName;
        player.controller.conT.text = player.handOfCards[2].cardName;
        player.controller.intlT.text = player.handOfCards[3].cardName;
        player.controller.wisT.text = player.handOfCards[4].cardName;
        player.controller.chaT.text = player.handOfCards[5].cardName;
    }


    // Method that *legitiately* starts the game. IEnumerators are kinda separate from void methods, it's a bit weird but it works
    // this one is called when you click "draw".
    // As you can see each state has a respective IEnumerator and a void method.
    // Rn I'm mainly using IEnumerators to change between states and call the respective methods
    //void methods do actual stuff in game.
    //Not consistent, feel free to change.
    IEnumerator SetupBattle()
    {
        Debug.Log("Game started.");
        yield return new WaitForSeconds(1f);
        state = RoundState.PLAYERTURN;
        StartCoroutine(PlayerTurn());
    }


    // Runs when the Draw button is clicked.
    //isDrawn bool disables the player from drawing cards to infinity
    //j is the number of cards in the player's hand,
    //may also represent the index of the cards in the shuffled deck where it's drawing cards from.
    public void OnClickDraw()
    {

        if (!isDrawn)
        {

            for (int j = 0; j < player.handSize; j++)
            {

                player.controller.InstantiateCards(j);
                player.controller.thisCard.thisId = player.handOfCards[j].id;
               
            }
        }

        isDrawn = true;
        StartCoroutine(SetupBattle());


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

    }

//it's the player's turn now, pops up a window letting them choose a stat to pick.
IEnumerator PlayerTurn()
    {
      SelectCardPrep();
        Debug.Log("Pick your card bro");
        player.controller.statSel.gameObject.SetActive(true);
        //IEnumerators NEED to return something, you can let do this.
        yield return new WaitForSeconds(1f);
    }

    //Happens when a stat is clicked, index represents the button/stat in question (check inspector for the buttons)
    //once done, it hides the window, and stores the following:
    //the stat selected to be compared against the enemy
    //and the index in question to be used for the enemy, to make sure the same stat is chosen
    //index goes 0-5 for the stats repectively.
    //then it switched to the enemy's turn and calls the method for it.

    //now it takes the value from the selected card.
    public void onStatClick(int i)
    {

        player.controller.fetchCard(i);
        player.controller.statSel.gameObject.SetActive(false);
        switch (statuses[chosenstat])
        {
            case "strength": player.controller.selectedVal = player.controller.selectedCard.strength; break;
            case "dexterity": player.controller.selectedVal = player.controller.selectedCard.dexterity; break;
            case "constitution": player.controller.selectedVal = player.controller.selectedCard.constitution; break;
            case "intelligence": player.controller.selectedVal = player.controller.selectedCard.intelligence; break;
            case "wisdom": player.controller.selectedVal = player.controller.selectedCard.wisdom; break;
            case "charisma": player.controller.selectedVal = player.controller.selectedCard.charisma; break;
            default: player.controller.selectedVal = 0;  break;
        }

       
        Debug.Log("Card picked was" + player.controller.selectedCard.cardName);
        Debug.Log(player.controller.selectedVal.ToString());
        state = RoundState.ENEMYTURN;
        StartCoroutine(EnemyTurn());

    }

    //instantiates a card in the enemy's area, implies the enemy  has chosen a card.
    //maybe it should be "hidden" at the start, with the back showing.
    //a "compare" button will appear for the player allowing the player to carry out the comparison and conclude the game.
    IEnumerator EnemyTurn()
    {

        enemy.controller.InstantiateCards(1);
        enemy.controller.thisCard.thisId = enemy.handOfCards[0].id;
        //enemy.controller.fetchCard(0);

        player.controller.compareB.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);


    }

    //Called when pressing the compare button
    // the aforementioned stat is now fetched form the enemy's card using the variables stored from the player's move.
    //once that is done, the comparison is carried out
    public void EnemyMove()
    {
        enemy.controller.fetchCard(0);
        switch (statuses[chosenstat])
        {
            case "strength": enemy.controller.selectedVal = enemy.controller.thisCard.strength; break;
            case "dexterity": enemy.controller.selectedVal = enemy.controller.thisCard.dexterity; break;
            case "constitution": enemy.controller.selectedVal = enemy.controller.thisCard.constitution; break;
            case "intelligence": enemy.controller.selectedVal = enemy.controller.thisCard.intelligence; break;
            case "wisdom": enemy.controller.selectedVal = enemy.controller.thisCard.wisdom; break;
            case "charisma": enemy.controller.selectedVal = enemy.controller.thisCard.charisma; break;
            default: enemy.controller.selectedVal = 0; break;
        }


        Debug.Log(enemy.controller.selectedVal);
        player.controller.compareB.gameObject.SetActive(false);
        StartCoroutine(Compare());
    }

    // Carries out the comparison to conclude the game, switched the game state respectively.
    IEnumerator Compare()
    {
        yield return new WaitForSeconds(1f);
        if (player.controller.selectedVal > enemy.controller.selectedVal)
        {
            player.score++;
            NextRound();
           
        }
        else if (enemy.controller.selectedVal > player.controller.selectedVal)
        {
            enemy.score++;
            NextRound();
            
        }
        else if (enemy.controller.selectedVal == player.controller.selectedVal)
        {
            state = RoundState.DRAW;
            NextRound();
            
        }

        if (player.score == 5)
        {
            state = RoundState.WON;
        } else if (enemy.score == 5)
        {
            state = RoundState.LOST;
        }
    }

    public void NextRound()
    {
        Debug.Log("Going to next round");
       
    }

    // Happens depending on the current game state. Sorta self explanatory
    public void EndRound()
    {
        player.controller.RemoveUsedCard();
        if (state == RoundState.WON)
        {
            Debug.Log("You won. Commencing victory cry.");
            new WaitForSeconds(3f);
            // Application.OpenURL("https://www.youtube.com/watch?v=sAXZbfLzJUg");
        }
        else if (state == RoundState.LOST)
        {
            Debug.Log("You lost.");
        }
        else if (state == RoundState.DRAW)
        {
            Debug.Log("It's a draw!");
        }

    }

}