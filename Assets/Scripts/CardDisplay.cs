using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CardDisplay : MonoBehaviour
{
    public Card card;

       public Text cardName;
    public Text description;
    public Image art;
    public Text strength;
    public Text dexterity;
    public Text intelligence;
    public Text charisma;
    public Text wisdom;
    public Text constitution;
    // Start is called before the first frame update
    void Start()
    {
        cardName.text = card.cardName;
        description.text = card.description;
        art.sprite = card.art;

        strength.text = card.strength.ToString();
        dexterity.text = card.dexterity.ToString();
        intelligence.text = card.intelligence.ToString();
        charisma.text = card.charisma.ToString();
        wisdom.text = card.wisdom.ToString();
        constitution.text = card.constitution.ToString();

    }
}
