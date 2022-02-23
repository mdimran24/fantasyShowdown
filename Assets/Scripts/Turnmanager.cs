using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using ExitGames.Client.Photon;

//This has been written in gitpod and is yet to be tested
public class Turnmanager : MonoBehaviour
{
    /* Theory: Store 2 cards that represent the players' selected cards. Can also be null.

        Update funtion that enables the visibility of enabled cards
        Theory is the cards are always there, except they are disabled/invisible.
        Number of enemy cards is determined by player count, just one for now, assuming it's not you.

        Uses RPCs to manage turns i.e. 
            if this player's selected card is no longer null, pass control to other player
            once both are no longer null, perform comparison.
            when a new round starts (both selected cards are null), game master starts first
                set a variable so that winnin player gets first turn in round

        Perhaps keeps track of the number of rounds and concludes the game.

        Suggestion: Make use of the spawnplayers game, make a script that counts the players in the scene and picks whoever is the master/who is you
                    prevents constantly copy-pasting code.
    */

    [SerializeField]
    //private GameObject enemyCard; //card to be displayed. Might also be a list but this is for simplicity's sake
    //ALTERNATIVELY we pick cards directly from the players below. The card above might be just for display purposes.
    private Player player1;
    [SerializeField]
    private Player player2; 

    public void Start(){
        //Go through all the players and figure out who is who
       
     
    }
    

    



    public void Update(){
        //if (you.selectedCard != null){
            //make card gameobject visible and pass the details onto it. make it so that it's visible on the corresponding end.
        //}
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
    foreach (GameObject p in players){
        //if this player is you, instantiate your cards and place them as needed.
        if(PhotonNetwork.IsMasterClient){
              player1 = p.GetComponent<Player>();
            } else {
                player2 = p.GetComponent<Player>();
            }
        }
    }

    public void Compare(){
        //PSEUDOCODE!!! NOT WORKING!!!

        /*statone = me.card.stat;
        stattwo = you.card.stat;
        if (me > you){
            Conclude(you won);
        } else if (you > me){
            Conclude (You lost)
        } else {
            Conclude it's a draw
        }

        NOTE: We need to parse this in some way so that each player gets a corresponding message.
        Suggestion: A method in the Player class that determines whether the player has control over their cards atm.
        */
    }

    public void Passturn(){
        //Has this player made their turn?
        //Has the turn been passed but the player in question already done their move?
        //How do you move on?
    }
}
