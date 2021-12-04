using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script builds a 20 card deck using cards from the database, for now it just uses random cards however in the future we can make a deck builder interface, for the MVP this will do

public class PlayerDeck : MonoBehaviour
{

    public List<Card> deck;
    //where the cards are gonna be taken from in game, the main deck should be untouched.
    //static so that several scripts can reference it while only one deck is define throughout.
    public static List<Card> shuffleDeck = new List<Card>();

    public int x;
    public static int deckSize;
    public int cardDatabaseSize;

    // Start is called before the first frame update
    // fills the main deck with cards from the database.
    void Awake()
    {
        x = 0;
        deckSize = 20;
        cardDatabaseSize = CardDatabase.cardList.Count;

        for(int i=0; i<deckSize; i++)
        {
            x = Random.Range(1, cardDatabaseSize);
           deck.Add(CardDatabase.cardList[i]);
        }

        
    }

    // shuffles the deck
    //fills the shuffled deck in question.

    public void Shuffle()
    {
      
            if (shuffleDeck.Count == 0)
            {
            for (int i = 0; i < deckSize; i++)
            {
                shuffleDeck.Add(deck[Random.Range(1, deckSize)]);
            }
            } else
            {
                shuffleDeck = new List<Card>();
            for (int i = 0; i < deckSize; i++)
            {
                shuffleDeck.Add(deck[Random.Range(1, deckSize)]);
            }
                Debug.Log("Shuffled deck size after reset " + PlayerDeck.shuffleDeck.Count);
            }
        
    }

    //returns a card to be put in the player' hand from the shuffled deck.
    // removes said card from the shuffled deck to avoid duplication
   
    public static Card RemovefromShuffle(int i)
    {
        Card shufpicked = shuffleDeck[i];
        shuffleDeck.Remove(shufpicked);
        return shufpicked;
    }

  
}
