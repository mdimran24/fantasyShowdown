using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    [Serializable]

public class Player : IEquatable<Player>
{

    //PLayer identification
    public string playerId;
    public string playerName;

    // where the cards from the deck go
    public static List<Card> handOfCards = new List<Card>();
    public int handSize;
    //Enables the player to do stuff with the cards
    public PlayerController controller;
   

    // takes cards from the deck and puts them in the player's hand.
    //debug log is to make sure the cards intantiates are the same as the ones that fill the hand.
    //sometimes it only logs 4 cards instead of 5 due to duplicates. Maybe there's a way around it.
    public void FillHand(int number)
    {
        for (int i = 0; i < number; i++)
        {
            handOfCards.Add(PlayerDeck.RemovefromShuffle(i));
            Debug.Log(handOfCards[i].cardName);
        }
        handSize = handOfCards.Count;
    }

  //Turns, TBC
    public bool Equals(Player other)
    {
         if (playerId.Equals(other.playerId))
          {
              return true;
          }
          else
          {
        return false;
          }
    }
}


