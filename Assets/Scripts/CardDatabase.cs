using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using SWNetwork;


//Generates a database of cards, use this formula to add a new card to the database: cardList.Add(new Card(int id, string cardName, int str, int dex, int con, int intel, int wis, int cha,Resources.Load <Sprite>("Card Art/Card") ));));
public class CardDatabase : MonoBehaviour
{

    public static List<Card> cardList = new List<Card>();

    public void CreatePack()
    {
        cardList.Add(new Card(0, "Hydra", 75, 100, 90, 40, 20, 15, Resources.Load<Sprite>("Card Art/Hydra"), "This multiheaded monster has the ability to grow two more heads every time one of its heads is damaged or removed."));
        cardList.Add(new Card(1, "Knight", 75, 25, 75, 35, 30, 60, Resources.Load<Sprite>("Card Art/Knight"), "Equipped in heavy armour and armed with a shield and longsword this valiant defender of justice fights to protect the realms."));
        cardList.Add(new Card(2, "Warlock", 25, 35, 50, 60, 40, 75, Resources.Load<Sprite>("Card Art/Warlock"), "This spellcaster made a dark pact with the forces of darkness in order to gain more power."));
        cardList.Add(new Card(3, "Wizard", 20, 30, 15, 100, 75, 65, Resources.Load<Sprite>("Card Art/Wizard"), " A seeker of knowledge, this spellcaster has spent many years of study in order to perfect his craft."));
        cardList.Add(new Card(4, "Shaman", 30, 50, 60, 40, 100, 50, Resources.Load<Sprite>("Card Art/Shaman"), "Often seen as a spiritual guide for many of the nomadic tribes, this spellcaster uses their powers to heal and help those in need."));
        cardList.Add(new Card(5, "Barbarian", 85, 55, 65, 15, 30, 40, Resources.Load<Sprite>("Card Art/Barbarian"), "This powerful warrior defends his tripe with nothing but honour and his huge battleaxe."));
        cardList.Add(new Card(6, "Battlemage", 70, 45, 75, 75, 30, 20, Resources.Load<Sprite>("Card Art/Battlemage"), "This mage has lived through the darkness of war and has learnt that sometimes a sword can be just as useful as a spell."));
        cardList.Add(new Card(7, "Bard", 40, 85, 45, 35, 25, 100, Resources.Load<Sprite>("Card Art/Bard"), "This entertainer is proficient in the art of persuasion and seduction. Never to be seen without his lute he will charm his way into any situation."));
        cardList.Add(new Card(8, "Rogue", 35, 100, 40, 55, 40, 65, Resources.Load<Sprite>("Card Art/Rogue"), "Learning his craft in the streets of the big cities this man is not worried about honour all that matters is the prize and getting out alive."));
        cardList.Add(new Card(9, "Ranger", 30, 85, 55, 50, 75, 15, Resources.Load<Sprite>("Card Art/Ranger"), "This skilled archer has perfected her craft, she’s a master tracker and is well in tune with nature."));
        cardList.Add(new Card(10, "Fighter", 70, 45, 70, 65, 50, 45, Resources.Load<Sprite>("Card Art/Fighter"), "A warrior of honour, he is a master of any weapon and can easily outsmart many opponents."));
        cardList.Add(new Card(11, "Dragon", 100, 75, 85, 60, 45, 35, Resources.Load<Sprite>("Card Art/Dragon"), "The most dominant creature in all the fantasy realms, this fire breathing monster strikes fear into anyone who’s seen it and lived to tell the tale."));
        cardList.Add(new Card(12, "Goblin", 40, 65, 40, 75, 40, 25, Resources.Load<Sprite>("Card Art/Goblin"), "A common creature in the fantasy realm, whilst not too smart or strong they’re quick on their feet and can be deadly in large numbers."));
        cardList.Add(new Card(13, "Orc", 90, 20, 100, 15, 20, 30, Resources.Load<Sprite>("Card Art/Orc"), "These large humanoids have no regard for anything other than strength and endurance, however despite their insane strength they can be easily outsmarted."));
        cardList.Add(new Card(14, "Dire Wolf", 75, 85, 75, 15, 10, 20, Resources.Load<Sprite>("Card Art/Dire Wolf"), "These fiercesome beasts are twice the size of a regular wolf and twice as dangerous."));
        cardList.Add(new Card(15, "Kraken", 100, 85, 75, 10, 40, 30, Resources.Load<Sprite>("Card Art/Kraken"), "Ruler of the seas any ship that has the misfortune of encountering this monstrous creature of the depths is certain to sink."));
        cardList.Add(new Card(16, "Skeleton", 70, 75, 55, 30, 35, 30, Resources.Load<Sprite>("Card Art/Skeleton"), "Risen from their eternal rest by an evil wizard this monster is about to make it everyone else problem."));
        cardList.Add(new Card(17, "Zombie", 60, 60, 70, 5, 20, 35, Resources.Load<Sprite>("Card Art/Zombie"), "Not long ago this monster was just a regular human, now after having their death interrupted all they want is to eat some brains."));
        cardList.Add(new Card(18, "Lich", 20, 35, 75, 100, 85, 20, Resources.Load<Sprite>("Card Art/Lich"), "This once powerful wizard tried to delay his own death by making herself into an undead creature, the process however turned her into an evil monster."));
        cardList.Add(new Card(19, "Vampire", 50, 75, 40, 35, 35, 100, Resources.Load<Sprite>("Card Art/Vampire"), "This blood sucking monster was once human, until he made the mistake of inviting his rather pale friend into his home."));
        cardList.Add(new Card(20, "Werewolf", 80, 55, 85, 20, 40, 35, Resources.Load<Sprite>("Card Art/Werewolf"), "Cursed after getting bitten by a wolf this man goes hunting every full moon."));
        cardList.Add(new Card(21, "Wyvern", 80, 70, 45, 40, 20, 15, Resources.Load<Sprite>("Card Art/Wyvern"), "A close relative of the Dragon, although not as strong it is a fiercesome opponent nonetheless."));
        cardList.Add(new Card(22, "Griffon", 75, 85, 50, 60, 40, 45, Resources.Load<Sprite>("Card Art/Griffon"), "A half bird half Lion hybrid, this terrifying creature can rip people apart with its dangerous beak and claws."));
        cardList.Add(new Card(23, "Demon", 100, 55, 75, 30, 30, 30, Resources.Load<Sprite>("Card Art/Demon"), "This diabolical creature from the nine hells has come to the fantasy realm to wreak havoc."));
        cardList.Add(new Card(24, "Imp", 10, 20, 15, 90, 85, 75, Resources.Load<Sprite>("Card Art/Imp"), "This small demon loves mischief."));
        cardList.Add(new Card(25, "Dire Bear", 85, 75, 85, 15, 20, 30, Resources.Load<Sprite>("Card Art/Dire Bear"), "This giant bear is both terrifying and lethal."));
        cardList.Add(new Card(26, "Giant Spider", 35, 75, 60, 55, 20, 5, Resources.Load<Sprite>("Card Art/Giant Spider"), "These giant spiders are the same size as dogs and have venom that can instantly kill a grown human."));
        cardList.Add(new Card(27, "Kobold", 15, 25, 35, 55, 100, 65, Resources.Load<Sprite>("Card Art/Kobold"), "These small dragonkin are extremely weak on their own but very dangerous in large numbers, and are experts at setting traps."));
        cardList.Add(new Card(28, "Slime", 5, 10, 85, 45, 60, 35, Resources.Load<Sprite>("Card Art/Slime"), "The weakest creature in the realm, whilst weak in strength it is almost impossible to damage this creature."));
        cardList.Add(new Card(29, "Ogre", 95, 25, 90, 5, 15, 35, Resources.Load<Sprite>("Card Art/Ogre"), "This brainless brute is a walking mass of raw strength."));
        cardList.Add(new Card(30, "Troll", 70, 55, 100, 10, 20, 25, Resources.Load<Sprite>("Card Art/Troll"), "Able to heal from injuries this monster can prove to be a dangerous foe if you don’t know they’re weak to fire."));
    }

}