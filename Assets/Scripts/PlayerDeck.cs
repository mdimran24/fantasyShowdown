using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//this script builds a 20 card deck using cards from the database, for now it just uses random cards however in the future we can make a deck builder interface, for the MVP this will do

public class PlayerDeck : MonoBehaviourPun
{
    //original deck of cards
    public List<Card> deck;

    //shuffled deck of cards
    public List<Card> shuffleDeck;

    //database of cards.
   CardDatabase db = new CardDatabase();
 
 //size of the deck
    public static int deckSize;

  //size of the database  
    public int cardDatabaseSize;

    void Awake()
    { 
     FillDeck();
     Shuffle();
      
    }

//crabs card from the database and puts them into the original deck.
    public void FillDeck()
    {
        db.CreatePack();
      
        deckSize = 20;
        cardDatabaseSize = CardDatabase.cardList.Count;
        

        for (int i = 0; i < deckSize; i++)
        {
           
            deck.Add(CardDatabase.cardList[i]);
           
        }
    
    }

//Creates the deck to be shuffled and, well, shuffles its cards.
     public void Shuffle()
    {
        shuffleDeck = new List<Card>();
        for (int i = 0; i < deckSize; i++)
        {
           
            shuffleDeck.Add(CardDatabase.cardList[i]);
          
        }
           
                
            for (int i = 0; i < deckSize; i++)
            {  
                
                 int j = Random.Range(0, i + 1);
                 Card tmp = shuffleDeck[j];
            shuffleDeck[j] = shuffleDeck[i];
            shuffleDeck[i] = tmp;
                
            }     
        
    }

  
//provides a list of cards that will represent the player's hand of cards.
    public List<Card> giveHand()
    {
        List<Card> given = new List<Card>();
        for (int i = 0; i<6; i++){
        Card shufpicked = shuffleDeck[i];
        given.Add(shufpicked);
        shuffleDeck.Remove(shufpicked);
        }
        return given;
    }

//Empties both original and shuffled decks
    public void EmptyDeck()
    {
        deck = new List<Card>();
        shuffleDeck = new List<Card>();
    }

  
}
