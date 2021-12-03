using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    [Serializable]

public class Player : IEquatable<Player>
{

    public string playerId;
    public string playerName;
    public List<Card> handOfCards = new List<Card>();
    public PlayerController controller;
   // public PlayerDeck deck;
  
    public void FillHand(int number)
    {
        for (int i = 0; i < number; i++)
        {
            handOfCards.Add(PlayerDeck.RemovefromShuffle(i));
        }
    }

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


