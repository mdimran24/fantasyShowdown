using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using ExitGames.Client.Photon;

//parts of the game in question
//CRINA: I HAVE EXTRA PARTS IN ANOTHER BRANCH, HANG ON.
public enum RoundStates { START, PLAYERTURN, ENEMYTURN, WON, LOST, DRAW }

public class MultiCardManager : MonoBehaviour
{
    //Represents the area that holds the player's cards, at the bottom of the screen
    public GameObject bottom;

    //HUD messenger text
    public Text Messenger;


    protected void Start()
    {
    
        Messenger.text = "Game Started";

    }
 
    IEnumerator SetupBattle()
    {
        Debug.Log("Game started.");
       // yield return new WaitForSeconds(1f);
       // state = RoundState.PLAYERTURN;
       // StartCoroutine(PlayerTurn());
        yield break;
    }


  //This button provides cards for the player who clicks it
  public void Draw(){
      //look for players
     GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
    foreach (GameObject p in players){
        //if this player is you, instantiate your cards and place them as needed.
        if(p.GetComponent<PhotonView>().IsMine){
            p.GetComponent<Player>().InstantiateCards();
            for(int i = 0; i<p.GetComponent<Player>().physicalCards.Count; i++){
                p.GetComponent<Player>().physicalCards[i].transform.SetParent(bottom.transform, false);
            }
        }
    }
    
     }
      
  }

 