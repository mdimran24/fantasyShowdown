using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//this script builds a 20 card deck using cards from the database, for now it just uses random cards however in the future we can make a deck builder interface, for the MVP this will do

public class PlayerDeck : MonoBehaviourPun
{

    public List<Card> deck;
    public List<Card> shuffleDeck;
   CardDatabase db = new CardDatabase();
  
    public static int deckSize;

    
    public int cardDatabaseSize;

    void Awake()
    { 
     FillDeck();
     Shuffle();
    }

    public void FillDeck()
    {
        //create pack from the database
        db.CreatePack();
        //setup a number of cards that will actually be playable.
        deckSize = 20;
        cardDatabaseSize = CardDatabase.cardList.Count;
        

        for (int i = 0; i < deckSize; i++)
        {
           //add said cards into the deck
            deck.Add(CardDatabase.cardList[i]);
        }
    
    }

     public void Shuffle()
    {
        shuffleDeck = new List<Card>();
        for (int i = 0; i < deckSize; i++)
        {
           //first make the shuffledeck basically a duplicate of the original deck
           //this is done by just using the same procedure
            shuffleDeck.Add(CardDatabase.cardList[i]);
          
        }
           
                
            for (int i = 0; i < deckSize; i++)
            {  
                //shuffle cards around without complications
                 int j = Random.Range(0, i + 1);
                 Card tmp = shuffleDeck[j];
            shuffleDeck[j] = shuffleDeck[i];
            shuffleDeck[i] = tmp;
                
            }     
        
    }

  
    //Give a list of cards to act as the hand of each player.
    public List<Card> GiveHand(int numofthem)
    {
        //list of cards that will be returned
        List<Card> given = new List<Card>();
        for (int i =0; i< numofthem; i++){
            //fill the hand
        Card shufpicked = shuffleDeck[i];
       given.Add(shufpicked);
        shuffleDeck.Remove(shufpicked);
        }
        return given;
    }

    //clears both decks.
    public void EmptyDeck()
    {
        deck = new List<Card>();
        shuffleDeck = new List<Card>();
    }

  
}
