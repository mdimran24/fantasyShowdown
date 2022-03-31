using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = MasterManager.GameSettings.ScreenName;
        PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion;
        if (!PhotonNetwork.IsConnected){
        PhotonNetwork.ConnectUsingSettings();
        } else {
            OnJoinedLobby();
        }
    }

    public override void OnConnectedToMaster()
    {
        if (!PhotonNetwork.InLobby){
        PhotonNetwork.JoinLobby();
        }
        print(PhotonNetwork.LocalPlayer.NickName);
    }

    public override void OnJoinedLobby()
    {
      SceneManager.LoadScene("Lobbyphoton");
    }

  
}
