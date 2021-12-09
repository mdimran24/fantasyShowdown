using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWNetwork;

public class MultiplayerGameManager : CardManager
    // extends the card manager script, you have everything that is in there, but to be modified for multiplayer use.
{

     protected new void Start()
    {
        base.Start();

        NetworkClient.Lobby.GetPlayersInRoom((successful, reply, error) =>
        {
            if (successful)
            {
                foreach (SWPlayer swPlayer in reply.players)
                {
                    string playerName = swPlayer.GetCustomDataString();
                    string playerId = swPlayer.id;

                    if (playerId.Equals(NetworkClient.Instance.PlayerId))
                    {
                        player.playerId = playerId;
                        player.playerName = playerName;
                    }
                    else
                    {
                        enemy.playerId = playerId;
                        enemy.playerName = playerName;
                    }
                }

              //  gameDataManager = new GameDataManager(localPlayer, remotePlayer, NetworkClient.Lobby.RoomId);
              //  netCode.EnableRoomPropertyAgent();
            }
            else
            {
                Debug.Log("Failed to get players in room.");
            }

        });
    }
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
