using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script builds a 20 card deck using cards from the database, for now it just uses random cards however in the future we can make a deck builder interface, for the MVP this will do

public class PlayerDeck : MonoBehaviour
{

    public List<Card> deck;
    public static List<Card> shuffleDeck = new List<Card>();

    public int x;
    public static int deckSize;
    public int cardDatabaseSize;

    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        
    }

    // shuffles the deck

    public void Shuffle()
    {
        for(int i = 0; i < deckSize; i++)
        {
          //  shuffleDeck[0] = deck[i];
          //  int randomIndex = Random.Range(i, deckSize);
         //   deck[i] = deck[randomIndex];
         //   deck[randomIndex] = shuffleDeck[0];
         shuffleDeck.Add(deck[Random.Range(1, deckSize)]);
        }
    }

    public Card RemoveRnd()
    {
        Card picked = deck[Random.Range(0, deckSize)];
        deck.Remove(picked);
        return picked;
    }

    public static Card RemovefromShuffle(int i)
    {
        Card shufpicked = shuffleDeck[i];
        shuffleDeck.Remove(shufpicked);
        return shufpicked;
    }

  
}
