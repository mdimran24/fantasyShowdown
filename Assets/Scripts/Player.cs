using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


    [Serializable]

public class Player : MonoBehaviour, IPunObservable
{

    //PLayer identification
    public int playerId;
    public string playerName;
    public int score;

     public List<GameObject> physicalCards = new List<GameObject>();
    public GameObject playerCard;
    public bool isDrawn = false;

     public int selectedVal;
    private int valtoberemoved;
    public ThisCard thisCard;
    public ThisCard selectedCard;

  // [SerializeField]
   // private Button getcardsbtn;
   // [SerializeField]
   // private PlayerDeck shareddeck;
private int numOfCards = 3;
   
    int incrementer = 1;

    public List<Card> handOfCards = new List<Card>();

       public static bool HasBeenConfirmed = false;

    public PhotonView view;

   //public Button DrawButtonPlayer = MultiCardManager.DrawBtn;

    public void Start(){
      playerId = GetComponent<PhotonView>().ViewID;
        playerName = "player" + incrementer;
        incrementer++;
        score = 0;
     // getcardsbtn = GameObject.FindGameObjectWithTag("Startup").GetComponent<Button>();
     // shareddeck = GameObject.FindGameObjectWithTag("Deck").GetComponent<PlayerDeck>();
     //  getcardsbtn.onClick.AddListener(FillHand);
    }

 

    

   
  
    public void Removefromcardid(int cardid)
    {
        for (int i = 0; i < 6; i++)
        {
            if (handOfCards[i].id == cardid)
            {
                handOfCards.RemoveAt(i);
            }
        }
    }

    public void EmptyHand()
    {
        handOfCards.Clear();
    }

    public void InstantiateCards()
    {
          for (int i = 0; i< numOfCards; i++){
              playerCard = Instantiate(this.playerCard, new Vector3(0, 0, 0), Quaternion.identity);
        
          playerCard.GetComponent<ThisCard>().thisId = handOfCards[i].id;
         physicalCards.Add(playerCard);

          }
    }

   

     public void RemoveUsedCard()
    {
      //  Destroy(selectGO.GetComponent<RectTransform>().GetChild(0).gameObject);
        
    }

    public void emptyPhysical()
    {
        physicalCards.Clear();
      //  if (physicalCards.Count == 0) { }
      
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) {
            Debug.Log("Sending number of cards for " + this.playerId + ": " + handOfCards.Count);
            stream.SendNext(handOfCards.Count);
            foreach (Card c in handOfCards) {
                Debug.Log($"sending card ID for {this.playerId}: {c.id}");
                stream.SendNext(c.id);
            }
        } else {
            int nCards = (int) stream.ReceiveNext();
            Debug.Log("Received number of cards for " + this.playerId + ": " + nCards);
            if (nCards != handOfCards.Count) {
                handOfCards.Clear();
                for (int i = 0; i < nCards; i++) {
                    handOfCards.Add(new Card(0, "dummy", 0, 0, 0, 0, 0, 0, null));
                }
            }

            for (int i = 0; i< nCards; i++) {
                int cardId = (int) stream.ReceiveNext();
                Debug.Log("Received card ID for " + this.playerId + ": " + cardId);
                if (handOfCards[i].id != cardId) {
                    handOfCards[i].id = cardId;
                    // update anything else in the card...
                }
            }
        }
    }
}


