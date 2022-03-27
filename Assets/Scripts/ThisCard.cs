using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


// This script allows me to pull cards from the database using just their card ID and add them on to a playable game object
public class ThisCard : MonoBehaviour
{

    public List<Card> thisCard = new List<Card>();
    public int thisId;

    public int id;
    public string cardName;
    public int strength;
    public int dexterity;
    public int constitution;
    public int intelligence;
    public int wisdom;
    public int charisma;
    public string lore;

    public Text nameText;
    public Text strengthText;
    public Text dexterityText;
    public Text constitutionText;
    public Text intelligenceText;
    public Text wisdomText;
    public Text charismaText;
    public Text loreText;

    public Sprite thisCardArt;
    public Image thisArt;


    // Start is called before the first frame update
    // Assign the details ont othe card gameobjects
    void Start()
    {

        thisCard[0] = CardDatabase.cardList[thisId];

        id = thisCard[0].id;
        cardName = thisCard[0].cardName;

        strength = thisCard[0].strength;
        dexterity = thisCard[0].dexterity;
        constitution = thisCard[0].constitution;
        intelligence = thisCard[0].intelligence;
        wisdom = thisCard[0].wisdom;
        charisma = thisCard[0].charisma;
        lore = thisCard[0].lore;


        thisCardArt = thisCard[0].cardArt;

        nameText.text = "" + cardName;
        strengthText.text = "Strength - " + strength;
        dexterityText.text = "Dexterity - " + dexterity;
        constitutionText.text = "Constitution - " + constitution;
        intelligenceText.text = "Intelligence - " + intelligence;
        wisdomText.text = "Wisdom - " + wisdom;
        charismaText.text = "Charisma - " + charisma;
        loreText.text = "" + lore;

        thisArt.sprite = thisCardArt;
    }


    // Update is called once per frame
    void Update()
    {
    }



}
