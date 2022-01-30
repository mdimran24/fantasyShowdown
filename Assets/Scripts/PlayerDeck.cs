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
   [SerializeField]
    private int i;
  
    public static int deckSize;

    
    public int cardDatabaseSize;

    void Awake()
    { 
     FillDeck();
     Shuffle();
        i = 0;
    }

    public void FillDeck()
    {
        db.CreatePack();
      
        deckSize = 20;
        cardDatabaseSize = CardDatabase.cardList.Count;
        

        for (int i = 0; i < deckSize; i++)
        {
           
            deck.Add(CardDatabase.cardList[i]);
            //shuffleDeck.Add(CardDatabase.cardList[i]);
        }
    
    }

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

    public void EmptyDeck()
    {
        deck = new List<Card>();
        shuffleDeck = new List<Card>();
    }

  
}
