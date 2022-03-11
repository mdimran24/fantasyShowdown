using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class PlayerListingMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private PlayerListing playerListing;
    [SerializeField]
    private Transform content;
    private RoomsCanvases _roomsCanvases;

    private List<PlayerListing> listings = new List<PlayerListing>();


    public override void OnEnable()
    {
        GetCurrentRoomPlayers();
    }

    public override async void OnDisable()
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

    private void GetCurrentRoomPlayers()
    {
        foreach (KeyValuePair<int, Photon.Realtime.Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
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
        AddPlayerListing(newPlayer);
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        int index = listings.FindIndex(x => x.PhotonPlayer == otherPlayer);
        if (index != -1)
        {
            Destroy(listings[index].gameObject);
            listings.RemoveAt(index);
        }
    }
}
