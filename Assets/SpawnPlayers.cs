using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject PlayerEntered;
    

    // Start is called before the first frame update
    //Code is run everytime someone joins a game
    //therefore for each player joined, this object is instantiated across the network.
    void Start()
    {
    PhotonNetwork.Instantiate(PlayerEntered.name, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
