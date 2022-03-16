using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerListingMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private PlayerListing playerListing;
    [SerializeField]
    private Transform content;

    [SerializeField]
    private Text readyOrNotText;
    private RoomsCanvases _roomsCanvases;

    private List<PlayerListing> listings = new List<PlayerListing>();
    private bool ready = false;


    public override void OnEnable()
    {
        base.OnEnable();
        Debug.Log("OnEnable has been called");
        GetCurrentRoomPlayers();
        SetReadyUp(false);


    }

    public override void OnDisable()
    {
        for (int i = 0; i < listings.Count; i++)
        {
            Destroy(listings[i].gameObject);
            listings.Clear();
        }
    }

    public void FirstInitialize(RoomsCanvases canvases)
    {
        _roomsCanvases = canvases;
    }

    //  public override void OnLeftRoom()
    //{
    // content.DestroyChildren();
    //  }

    private void SetReadyUp(bool state)
    {
        ready = state;
        if (ready)
        {
            readyOrNotText.text = "Ready";
        }
        else
        {
            readyOrNotText.text = "Not r"; 
        }
    }

    private void GetCurrentRoomPlayers()
    {
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }
        if (PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null)
        {
            return;
        }
        foreach (KeyValuePair<int, Photon.Realtime.Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log("Getting curret room players...");
            AddPlayerListing(playerInfo.Value);
        }
    }

    private void AddPlayerListing(Photon.Realtime.Player pl)
    {
        int index = listings.FindIndex(x => x.PhotonPlayer == pl);
        if (index != -1)
        {
            listings[index].SetPlayerInfo(pl);
        }
        else
        {


            PlayerListing listing = Instantiate(playerListing, content);
            if (listing != null)
            {
                listing.SetPlayerInfo(pl);
                listings.Add(listing);
            }
        }
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.Log("A new player entered!");
        AddPlayerListing(newPlayer);
    }

    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
      _roomsCanvases.currentRoomCanvas.LeaveRoomMenu.OnClick_LeaveRoom();
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        int index = listings.FindIndex(x => x.PhotonPlayer == otherPlayer);
        if (index != -1)
        {
            Debug.Log("the player left");
            Destroy(listings[index].gameObject);
            listings.RemoveAt(index);
        }
    }

    public async void OnClickStartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i< listings.Count; i++){
                if (listings[i].PhotonPlayer != PhotonNetwork.LocalPlayer){
                    if (!listings[i].Ready){
                        return;
                    }
                }
            }
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LoadLevel("MultiplayerGame");
        }
    }

    public void OnClickReadyUp()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            SetReadyUp(!ready);
            
            base.photonView.RPC("RPC_ChangeReadyState", RpcTarget.MasterClient,PhotonNetwork.LocalPlayer, ready);
        }
    }

    [PunRPC]
    private void RPC_ChangeReadyState(Photon.Realtime.Player player,bool ready)
    {
         int index = listings.FindIndex(x => x.PhotonPlayer == player);
        if (index != -1)
        {
            listings[index].Ready = ready;
           listings[index].readyicon.gameObject.SetActive(ready);
           
        }
    }
}
