using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using ExitGames.Client.Photon;

//parts of the game in question
public enum RoundStates { START, PLAYERTURN, ENEMYTURN, WON, LOST, DRAW }

public class MultiCardManager : MonoBehaviour
{
 
    private bool isDrawn;
    
   
    public GameObject bottom;
    public GameObject Selector;


    bool HasBeenConfirmed;

    public static Button DrawBtn;
    public Text Messenger;


     protected void Awake()
    {
        Messenger.text = "";
       
       
    }

 

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


  
  public void Draw(){
     GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
    //Player player;
    foreach (GameObject p in players){
        if(p.GetComponent<PhotonView>().IsMine){
            p.GetComponent<Player>().InstantiateCards();
            for(int i = 0; i<p.GetComponent<Player>().physicalCards.Count; i++){
                p.GetComponent<Player>().physicalCards[i].transform.SetParent(bottom.transform, false);
            }
        }
    }
    
     }
      
  }

 