using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

public class Card
{
    public int id;
    public string cardName;
    public int strength;
    public int dexterity;
    public int constitution;
    public int intelligence;
    public int wisdom;
    public int charisma;
    public Sprite cardArt;

    // constructor for the card class, made another script that creates a List of cards instead of a database, this saves us from hosting a database which is fine for a small number of cards
    public Card(int id, string cardName, int str, int dex, int con, int intel, int wis, int cha, Sprite art)
    {
        this.id = id;
        this.cardName = cardName;
        this.strength = str;
        this.dexterity = dex;
        this.intelligence = intel;
        this.charisma = cha;
        this.wisdom = wis;
        this.constitution = con;
        this.cardArt = art;
    }

     public byte Id { get; set; }

  public static object Deserialize(byte[] data)
  {
    var result = new Card(0, null, 0, 0, 0, 0, 0, 0, null);
    result.Id = data[0];
    return result;
  }

  public static byte[] Serialize(object customType)
  {
    var c = (Card)customType;
    return new byte[] { c.Id };
  }

}

