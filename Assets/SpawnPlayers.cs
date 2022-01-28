using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject PlayerEntered;
    

    // Start is called before the first frame update
    void Start()
    {
    PhotonNetwork.Instantiate(PlayerEntered.name, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
