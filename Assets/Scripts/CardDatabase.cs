using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Generates a database of cards, use this formula to add a new card to the database: cardList.Add(new Card(int id, string cardName, int str, int dex, int con, int intel, int wis, int cha,Resources.Load <Sprite>("Card Art/Card") ));));
public class CardDatabase : MonoBehaviour
{

    public static List<Card> cardList = new List<Card>();

    void Awake()
    {
        cardList.Add(new Card(0, "Example", 0, 0, 0, 0, 0, 0, Resources.Load <Sprite>("Card Art/1") ));
        cardList.Add(new Card(1, "Knight", 75, 25, 75, 35, 30, 60, Resources.Load<Sprite>("Card Art/Knight")));
        cardList.Add(new Card(2, "Warlock", 25, 35, 50, 60, 40, 75, Resources.Load <Sprite>("Card Art/Warlcok") ));
        cardList.Add(new Card(3, "Wizard", 20, 30, 15, 100, 75, 65, Resources.Load <Sprite>("Card Art/Wizard") ));
        cardList.Add(new Card(4, "Shaman", 30, 50, 60, 40, 100, 50, Resources.Load <Sprite>("Card Art/Shaman") ));
        cardList.Add(new Card(5, "Barbarian", 85, 55, 65, 15, 30, 40, Resources.Load <Sprite>("Card Art/Barbarian") ));
        cardList.Add(new Card(6, "Battlemage", 70, 45, 75, 75, 30, 20, Resources.Load <Sprite>("Card Art/Battlemage") ));
        cardList.Add(new Card(7, "Bard", 40, 85, 45, 35, 25, 100, Resources.Load <Sprite>("Card Art/Bard") ));
        cardList.Add(new Card(8, "Rogue", 35, 100, 40, 55, 40, 65, Resources.Load <Sprite>("Card Art/Rogue") ));
        cardList.Add(new Card(9, "Ranger", 30, 85, 55, 50, 75, 15, Resources.Load <Sprite>("Card Art/Ranger") ));
        cardList.Add(new Card(10, "Fighter", 70, 45, 70, 65, 50, 45, Resources.Load <Sprite>("Card Art/Fighter") ));
        cardList.Add(new Card(11, "Dragon", 100, 75, 85, 60, 45, 35, Resources.Load <Sprite>("Card Art/Dragon") ));
        cardList.Add(new Card(12, "Goblin", 40, 65, 40, 75, 40, 25, Resources.Load <Sprite>("Card Art/Goblin") ));
        cardList.Add(new Card(13, "Orc", 90, 20, 100, 15, 20, 30, Resources.Load <Sprite>("Card Art/Orc") ));
        cardList.Add(new Card(14, "Dire Wolf", 75, 85, 75, 15, 10, 20, Resources.Load <Sprite>("Card Art/Dire Wolf") ));
        cardList.Add(new Card(15, "Kraken", 100, 85, 75, 10, 40, 30, Resources.Load <Sprite>("Card Art/Kraken") ));
        cardList.Add(new Card(16, "Skeleton", 70, 75, 55, 30, 35, 30, Resources.Load <Sprite>("Card Art/Skeleton") ));
        cardList.Add(new Card(17, "Zombie", 60, 60, 70, 5, 20, 35, Resources.Load <Sprite>("Card Art/Zombie") ));
        cardList.Add(new Card(18, "Lich", 20, 35, 75, 100, 85, 20, Resources.Load <Sprite>("Card Art/Lich") ));
        cardList.Add(new Card(19, "Vampire", 50, 75, 40, 35, 35, 100, Resources.Load <Sprite>("Card Art/Vampire") ));
        cardList.Add(new Card(20, "Werewolf", 80, 55, 85, 20, 40, 35, Resources.Load <Sprite>("Card Art/Werewolf") ));
        cardList.Add(new Card(21, "Wyvern", 80, 70, 45, 40, 20, 15, Resources.Load <Sprite>("Card Art/Wyvern") ));
        cardList.Add(new Card(22, "Griffon", 75, 85, 50, 60, 40, 45, Resources.Load <Sprite>("Card Art/Griffon") ));
        cardList.Add(new Card(23, "Demon", 100, 55, 75, 30, 30, 30, Resources.Load <Sprite>("Card Art/Demon") ));
        cardList.Add(new Card(24, "Imp", 10, 20, 15, 90, 85, 75, Resources.Load <Sprite>("Card Art/Imp") ));
        cardList.Add(new Card(25, "Dire Bear", 85, 75, 85, 15, 20, 30, Resources.Load <Sprite>("Card Art/Dire Bear") ));
        cardList.Add(new Card(26, "Giant Spider", 35, 75, 60, 55, 20, 5, Resources.Load <Sprite>("Card Art/Giant Spider") ));
        cardList.Add(new Card(27, "Kobold", 15, 25, 35, 55, 100, 65, Resources.Load <Sprite>("Card Art/Kobold") ));
        cardList.Add(new Card(28, "Slime", 5, 10, 85, 45, 60, 35, Resources.Load <Sprite>("Card Art/Slime") ));
        cardList.Add(new Card(29, "Ogre", 95, 25, 90, 5, 15, 35, Resources.Load <Sprite>("Card Art/Ogre") ));
        cardList.Add(new Card(30, "Troll", 70, 55, 100, 10, 20, 25, Resources.Load <Sprite>("Card Art/Troll") ));
        cardList.Add(new Card(31, "Hydra", 75, 100, 90, 40, 20, 15, Resources.Load <Sprite>("Card Art/Hydra") ));
    }

}