using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Card", menuName = "Fantasy Showdown/Card", order = 0)]


//[CreateAssetMenu(fileName = "Card", menuName = "Fantasy Showdown/Card", order = 0)]
public class Card : ScriptableObject {
    public string cardName;
    public string description;
    public Sprite art;
    public int strength;
    public int dexterity;
    public int intelligence;
    public int charisma;
    public int wisdom;
    public int constitution;


}

