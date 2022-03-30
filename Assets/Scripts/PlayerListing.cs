using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;

public class PlayerListing : MonoBehaviour
{
   [SerializeField]
   private Text playerName;
 
   public Image master;
 
   public Image readyicon;


   private DependencyStatus dependencyStatus;
   private FirebaseAuth auth;
   private FirebaseUser user;
   private DatabaseReference DBref;
   public string username;
   private FirebaseManager firebase;
  
  public Photon.Realtime.Player PhotonPlayer { get; private set;}
  public bool Ready = false;

   // pulls the username for the current user from the database
  /*public IEnumerator GetUsername()
  {
     user = FirebaseManager.Singleton.User;

     DatabaseReference DBref = FirebaseManager.Singleton.DBref;

     var DBtask = DBref.Child("users").Child(user.UserId).GetValueAsync();

     yield return new WaitUntil(predicate: () => DBtask.IsCompleted); 
     {
        if(DBtask.Exception != null)
        {
           Debug.LogWarning(message: $"Failed to register task with {DBtask.Exception}");
        }
        else if(DBtask.Result.Value == null)
        {
           username = "";
        }
        else
        {
           DataSnapshot snapshot = DBtask.Result;
           username = snapshot.Child("username").Value.ToString();
        }
     }
   } */

   public void SetPlayerInfo(Photon.Realtime.Player player)
   {
      /*StartCoroutine(GetUsername());
       
      if(user !=null)
      {
         playerName.text = username;
      }
      else 
      {
         playerName.text = player.NickName;
      }*/
      PhotonPlayer = player;

      playerName.text = player.NickName;

      if (PhotonNetwork.CurrentRoom.MasterClientId == player.ActorNumber)
      {
           master.gameObject.SetActive(true);
           Debug.Log("You're the room owner");
      }
   
   }
}
