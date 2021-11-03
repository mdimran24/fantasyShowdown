using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ConnectToServer : MonoBehaviour
{

   private void Start(){
   PhotonNetwork.ConnectUsingSettings();
   }
}
