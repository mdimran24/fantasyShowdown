using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using ExitGames.Client.Photon;

//parts of the game in question
public enum RoundStates { START, PLAYING, CONCLUSION, NEWROUND, END }

public class MultiCardManager : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField]
    private RoundStates currState;
    //Represents the area that holds the player's cards, at the bottom of the screen
    public GameObject bottom;

    //Upper RHS of screen where it represents all enemy cards
    public GameObject opponentCards;

    /*the player in question. Now note that both ends of the game basically have individual copies of this script
 and only the photon-based code actually deals with sharing info across the network. Therefore there is only one player
 variable as on this end, it represents you and on their end, it represents them. */
    public Player player;

    //The reset button, meaning that their selected card goes back onto their hand.
    public UnityEngine.UI.Button ResetSelected;
    //the confirm button meaning that the player has decided on which card to play
    public UnityEngine.UI.Button ConfirmSelected;

    //represents that the player has selected their card
    bool HasBeenConfirmed;

    //HUD messenger text
    public Text Messenger;
    [SerializeField]
    private Text HUDRounds;
    [SerializeField]
    private Text Winnerstreak;

    //list of statuses that will be selected from
    public static string[] statuses;
    //the status that has been picked
    public static int chosenstat;

    public GameObject enemyscard;

    //Dealer that hands the cards to the players
    public Dealer dealer;

    //Photon raiseevent stuff
    public const byte PASS_STAT = 2;
    public const byte PASS_CHOSEN = 3;
    public const byte RECEIVESTATE = 4;
    public const byte CURRENTROUND = 5;

    //Have both players got their cards instantiated?
    [SerializeField]
    private bool bothplayerscards = false;

    //is the comparison done?
    [SerializeField]
    private bool gotresults = false;
    [SerializeField]
    private bool isadraw = false;

    //self explanatory
    [SerializeField]
    private int maxRounds = 5;
    [SerializeField]
    private int currentRound = 1;



    protected void Start()
    {
        currState = RoundStates.START;
        Messenger.text = "Game Started";
        HUDRounds.text = "Round " + currentRound.ToString() + " of 5";
        Winnerstreak.text = "";
        GameFlow();


    }

    private void GameFlow()
    {
        switch (currState)
        {
            case (RoundStates.START): StartCoroutine(checkforboth()); break;
            case (RoundStates.PLAYING): StartCoroutine(starter()); break;
            case (RoundStates.CONCLUSION): StartCoroutine(StartConcluding()); break;
            case (RoundStates.NEWROUND): incrementAsMaster(); StartCoroutine(AdvanceRound()); break;
            case (RoundStates.END): determineWinner(); break;
        }
    }

    //Proceeds to next round, otherwise this would end up happening twice.
    private void incrementAsMaster()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (!isadraw)
            {
                currentRound++;
            }
            object[] rdatas = new object[] { currentRound };
            PhotonNetwork.RaiseEvent(CURRENTROUND, rdatas, Photon.Realtime.RaiseEventOptions.Default, SendOptions.SendReliable);
        }
        HUDRounds.text = "Round " + currentRound.ToString() + " of 5";
    }


    //Photon stuff
    public override void OnEnable()
    {
        // player.HasBeenConfirmed = false;
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClientEventReceived;
    }

    public override void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClientEventReceived;
    }

    private void NetworkingClientEventReceived(EventData obj)
    {
        //raise event making it possible for the deck to be shared.
        if (obj.Code == PASS_STAT)
        {
            Debug.Log("Received event: " + obj);
            //extract the data and put it into an array
            object[] datas = (object[])obj.CustomData;

            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject p in players)
            {
                PhotonView playerView = p.GetComponent<PhotonView>();
                //if that player is you
                if (!playerView.IsMine)
                {
                    Card enemyscardp = new Card((int)datas[0], "test", 0, 0, 0, 0, 0, 0, null, "test");
                    enemyscard = PhotonNetwork.Instantiate("Card", new Vector3(0, 0, 0), Quaternion.identity);

                    enemyscard.transform.Find("Frame").gameObject.SetActive(false);
                    enemyscard.transform.Find("BackOfCard").gameObject.SetActive(true);
                    enemyscard.GetComponent<ThisCard>().thisId = enemyscardp.id;
                    enemyscard.transform.SetParent(opponentCards.transform, false);

                }
            }
        }

        //raiseevent that shares the chosen start as well as the HUD message
        if (obj.Code == PASS_CHOSEN)
        {
            object[] datas = (object[])obj.CustomData;
            chosenstat = (int)datas[0];
            Messenger.text = (string)datas[1];
        }

        //shares the current game state, keeps whole thing on track.
        if (obj.Code == RECEIVESTATE)
        {
            Debug.Log("Rased event to receive state");
            object[] datas = (object[])obj.CustomData;
            currState = (RoundStates)datas[0];
            //these are random but the game breaks without them.
            if (currState == RoundStates.PLAYING && currentRound > 1)
            {
                return;
            }

            if (currState != RoundStates.NEWROUND || currState == RoundStates.CONCLUSION || currState == RoundStates.END)
            {
                GameFlow();
            }
        }

        //shares count of rounds.
        if (obj.Code == CURRENTROUND)
        {
            object[] datas = (object[])obj.CustomData;
            currentRound = (int)datas[0];
            HUDRounds.text = "Round " + currentRound.ToString() + " of 5";
        }

    }

    // Event is received if the object listens for it. Otherwise when OnDisable.

    /* Makes sure the card that has been dragged onto the select area can no longer be clickable
    And also that the reset and confirm buttons don't appear when a card is not selected yet or 
    when a card has already been played. */
    private void Update()
    {
        if (SelectedItemSlot.hasBeenSelected && !HasBeenConfirmed)
        {
            ResetSelected.gameObject.SetActive(true);
            ConfirmSelected.gameObject.SetActive(true);
        }
        else
        {
            ResetSelected.gameObject.SetActive(false);
            ConfirmSelected.gameObject.SetActive(false);
        }
    }

    //determines the the game is good to go to start comparing cards.
    private bool bothplayersdrawn()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in players)
        {
            if (p.GetComponent<Player>().HasBeenConfirmed != true)
            {
                return false;
            }
        }
        return true;
    }


    //roll call for both players before proceeding.
    public IEnumerator checkforboth()
    {
        yield return new WaitForSeconds(1);
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            currState = RoundStates.PLAYING;
            object[] datas = new object[] { currState };
            PhotonNetwork.RaiseEvent(RECEIVESTATE, datas, Photon.Realtime.RaiseEventOptions.Default, SendOptions.SendReliable);
            yield return new WaitForSeconds(1);
        }

    }

    //actually starts the game
    public IEnumerator starter()
    {
        if (currentRound == 1 && !isadraw)
        {
            dealer.Dealbtn();

            Debug.LogError("Starter");
            yield return new WaitForSeconds(1);
            Draw();
        }

        //if the game is already in progress, don't draw another hand of cards.
        else
        {
            PlayerTurn();
        }
        isadraw = false;

    }

    //This button provides cards for the player who clicks it
    public void Draw()
    {
        //look for players
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in players)
        {
            //if this player is you, instantiate your cards and place them as needed.
            if (p.GetComponent<PhotonView>().IsMine)
            {
                player = p.GetComponent<Player>();

                p.GetComponent<Player>().InstantiateCards();
                for (int i = 0; i < p.GetComponent<Player>().physicalCards.Count; i++)
                {
                    p.GetComponent<Player>().physicalCards[i].transform.SetParent(bottom.transform, false);
                }
            }
        }
        bothplayerscards = true;
        PlayerTurn();

    }


    private void PlayerTurn()
    {
        SelectCardPrep();
    }


    //creates the stat array and picks one at random, then lets you know about it.
    public void SelectCardPrep()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            statuses = new string[6];
            statuses[0] = "Strength";
            statuses[1] = "Dexterity";
            statuses[2] = "Constitution";
            statuses[3] = "Intelligence";
            statuses[4] = "Wisdom";
            statuses[5] = "Charisma";
            chosenstat = Random.Range(0, statuses.Length);
            Debug.Log("Status is:" + statuses[chosenstat]);
            Messenger.text = "Status is: " + statuses[chosenstat].ToString();
            object[] datas = new object[] { chosenstat, Messenger.text };
            PhotonNetwork.RaiseEvent(PASS_CHOSEN, datas, null, SendOptions.SendReliable);
        }

    }

    // removes the card from the selected area and puts it back onto your hand.
    public void OnResetPressed()
    {
        SelectedItemSlot.hasBeenSelected = false;
        GameObject cardTemp = player.selectGO.GetComponent<RectTransform>().GetChild(0).gameObject;
        cardTemp.GetComponent<CanvasGroup>().blocksRaycasts = true;
        cardTemp.transform.SetParent(bottom.transform, false);

    }

    // the card disappears and is now stored to be played against your component
    // YOU CANNOT UNDO THIS.
    public void OnConfirmPressed()
    {
        //confirms you have indeed drawn your card. Should move this to the player class.
        HasBeenConfirmed = true;
        //makes aforementioned buttons disappear.
        ResetSelected.gameObject.SetActive(false);
        ConfirmSelected.gameObject.SetActive(false);

        //if your pressed the confirm button while there was no card inside.
        if (player.selectGO.GetComponent<RectTransform>().childCount == 0)
        {
            Debug.LogError("There is no card to confirm");
        }
        else
        {
            //marks the card inside the area as the one to be played.
            player.selectedCard = player.selectGO.GetComponent<RectTransform>().GetChild(0).GetComponent<ThisCard>();
            Debug.Log(player.selectedCard.cardName);
            //advances the game
            onStatClick();
        }
    }

    /*picks the stat that has been picked from the card you have selected,
 then lets you know of it.
 Suggestion: create a method that returns a ThisCard object that contains the
 same info as the selected card in order to be used in a RaiseEvent method.
 Additional: A method that intantiated this card to the opponent, with the face down and all. */
    public void onStatClick()
    {
        if (player.selectedCard != null)
        {
            // Debug.Log(statuses[chosenstat].ToString());
            switch (chosenstat)
            {
                case 0: player.selectedVal = player.selectedCard.strength; break;
                case 1: player.selectedVal = player.selectedCard.dexterity; break;
                case 2: player.selectedVal = player.selectedCard.constitution; break;
                case 3: player.selectedVal = player.selectedCard.intelligence; break;
                case 4: player.selectedVal = player.selectedCard.wisdom; break;
                case 5: player.selectedVal = player.selectedCard.charisma; break;
                default: player.selectedVal = 0; Debug.Log("Something went wrong with value selection."); break;
            }
        }
        Debug.Log("Card picked was " + player.selectedCard.cardName);
        Debug.Log(player.selectedVal.ToString());
        object[] stat = new object[] { player.selectedCard.id };
        Debug.Log("raising event with datas " + stat);
        //Actually send it to the other player.
        PhotonNetwork.RaiseEvent(PASS_STAT, stat, null, SendOptions.SendReliable);

        player.HasBeenConfirmed = true;

        //now that both players confirmed their cards, proceed further.
        if (bothplayersdrawn() && !gotresults)
        {
            currState = RoundStates.CONCLUSION;
            object[] datas = new object[] { currState };
            PhotonNetwork.RaiseEvent(RECEIVESTATE, datas, Photon.Realtime.RaiseEventOptions.Default, SendOptions.SendReliable);
            Debug.LogError("Changed state to conclusion");
            GameFlow();
        }

    }

    //calls for comparison
    private IEnumerator StartConcluding()
    {
        yield return new WaitForSeconds(1);
        Comparer();
        currState = RoundStates.NEWROUND;
        object[] datas = new object[] { currState };
        PhotonNetwork.RaiseEvent(RECEIVESTATE, datas, Photon.Realtime.RaiseEventOptions.Default, SendOptions.SendReliable);
        Debug.LogError("Starting new round...");
        yield return new WaitForSeconds(1);
        GameFlow();
    }

    //this one tells you if you won or not.
    private void Comparer()
    {
        //look for players, take the selected value from each of them and put them in a list.
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        List<int> values = new List<int>();
        foreach (GameObject p in players)
        {
            values.Add(p.GetComponent<Player>().selectedVal);
        }
        //sort this list so that the first value will always be the smallest.
             values.Sort();
            //if that value is yours, you lost. if not, you won.
         if (values[0] == values[1])
        {
            Messenger.text = "It's a draw!";
            isadraw = true;
        }
        else
        {
            if (values[0] == player.selectedVal)
            {
                Messenger.text = "You lost!";
                Winnerstreak.text += "X";
            }
            else
            {
                Messenger.text = "You won!";
                player.score++;
                player.customProperties["PlayerScore"] = player.score;
                PhotonNetwork.LocalPlayer.CustomProperties = player.customProperties;
               

                Winnerstreak.text += "V";
            }
        }


        enemyscard.transform.Find("Frame").gameObject.SetActive(true);
        enemyscard.transform.Find("BackOfCard").gameObject.SetActive(false);
    }

    //determined whether the game is over or not.
    private IEnumerator AdvanceRound()
    {
        yield return new WaitForSeconds(1);
        //if all 5 rounds are played, end the game.
        if (currentRound > maxRounds)
        {
            currState = RoundStates.END;
            object[] datas = new object[] { currState };
            PhotonNetwork.RaiseEvent(RECEIVESTATE, datas, Photon.Realtime.RaiseEventOptions.Default, SendOptions.SendReliable);
            Debug.Log("Game Over");
            GameFlow();
        }
        else
        {
            //else, partially reset the game and loop it over.
            if (isadraw)
            {
                OnResetPressed();

            }
            else
            {
                player.RemoveUsedCard();

            }
            Destroy(opponentCards.GetComponent<Transform>().GetChild(0).gameObject);
            HasBeenConfirmed = false;
            SelectedItemSlot.hasBeenSelected = false;
            player.HasBeenConfirmed = false;
            gotresults = false;

            currState = RoundStates.PLAYING;
            object[] datas = new object[] { currState };
            PhotonNetwork.RaiseEvent(RECEIVESTATE, datas, Photon.Realtime.RaiseEventOptions.Default, SendOptions.SendReliable);
        }
        yield return new WaitForSeconds(1);
        GameFlow();
    }

    private void determineWinner()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        List<int> scores = new List<int>();
        foreach (GameObject p in players)
        {
            scores.Add(p.GetComponent<Player>().score);
        }
        //sort this list so that the first value will always be the smallest.
        scores.Sort();
        if (scores[0] == player.score)
        {
            Messenger.text = "You lost this game. Better luck next time.";

        }
        else
        {
            player.isWinner = true;
            Messenger.text = "You won this game! Congrats!";


        }
        DontDestroyOnLoad(player);
        PhotonNetwork.LoadLevel("GameOver");
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(currState);
        }
        else
        {
            currState = (RoundStates)stream.ReceiveNext();
        }
    }
}

