using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;
using UnityEngine.UI;

public class CharacterManager : NetworkBehaviour
{
    public static CharacterManager instanse;

    private Player NeedSpawnPlayer;
    public Dictionary<int, Player> PlayersinGame = new Dictionary<int, Player>();
    public Text T1;

    private void Awake()
    {
        instanse = this;
    }

    public void OnSpawn(Player CurrentPlayer)
    {
        CurrentPlayer.GetHealth.health.Value = 2;
        CurrentPlayer.gameObject.SetActive(true);
    }
    public Player OnStatus(Player CurrentPlayer)
    {
        if (!CurrentPlayer.gameObject.activeSelf)
            return CurrentPlayer;
        return null;
    }

    public void AddingPlayer(Player currPlayer)
    {
        PlayersinGame.Add((int)currPlayer.OwnerClientId, currPlayer);
        if (PlayersinGame.Count > 0)
        {
            foreach (var item in PlayersinGame)
            {
                T1.text = "P:" + item.Value;
            }
        }
    }
    public Player GettingPlayer(int id)
    {
        foreach (KeyValuePair<int , Player> item in PlayersinGame)
        {
            if (id == item.Key)
            {
                return item.Value;
            }
        }
        return null;
    }
    //if (!OnStatus(currentPlayer)) StartCoroutine(TimeLine(2));

    //if (Input.GetKeyDown(KeyCode.X))
    //{
    //    print("C");
    //    TestClientRpc(new ClientRpcParams
    //    {
    //        Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 1 } }
    //    });

    //}
    //if (Input.GetKeyDown(KeyCode.Z))
    //{
    //    print("S");
    //    TestServerRpc(new ServerRpcParams());
    //}


    private void Start()
    {
        StartCoroutine(TimeLineForSpawn(2f));
    }
    IEnumerator TimeLineForSpawn(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            if (PlayersinGame.Count > 0)
            {
                foreach (Player plyr in PlayersinGame.Values)
                {
                    NeedSpawnPlayer = OnStatus(plyr);
                }
                if (NeedSpawnPlayer)
                    OnSpawn(NeedSpawnPlayer);
            }
        }
    }
}