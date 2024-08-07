using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GameManager : NetworkBehaviour
{
    public NetworkVariable<float> gameTimer = new NetworkVariable<float>();

    [SerializeField] private UIManager uiManager;
    [SerializeField] private Transform[] spawnPoints;
    public static GameManager Singleton {  get; private set; }

    [SerializeField] private List<PlayerInput> _connectedPlayers;
    private bool isCounting;
    private void Awake()
    {
        Singleton = this;
    }
    private void Update()
    {
        if (isCounting && NetworkManager.IsServer) 
        {
            gameTimer.Value -= Time.deltaTime;
            if(gameTimer.Value <= 0)
            {
                EndGameRpc();
                isCounting = false;
            }
        }
    }

    public void StartMatch()
    {
        if(NetworkManager.IsServer)
        {
            gameTimer.Value = 300f;
            isCounting = true;
            foreach(PlayerInput player in _connectedPlayers)
            {
                player.killCount.Value = 0;
            }
        }
    }

    private void EndGameRpc()
    {
        foreach(PlayerInput player in _connectedPlayers)
        {
            player.GetComponent<PlayerMovement>().enabled = false;
        }
    }

    public void OnLocalPlayerJoined(PlayerInput player)
    {
        player.playerNickname.Value = uiManager.GetLocalNickname();
        OnPlayerJoined(player);
    }

    public void OnPlayerJoined(PlayerInput playerObject)
    {
        _connectedPlayers.Add(playerObject);
        SpawnPlayer(playerObject.NetworkObject);
        playerObject.killCount.OnValueChanged += ScoreboardUpdate;
    }
    private void ScoreboardUpdate(int oldValue, int newValue)
    {
        string tempScoreboard = "";
        foreach (PlayerInput connectedPlayer in _connectedPlayers)
        {
            tempScoreboard += connectedPlayer.playerNickname.Value.ToString() + ": " + connectedPlayer.killCount.Value.ToString() + "\n";
        }

        uiManager.SetScoreboardText(tempScoreboard);
    }

    public void OnPlayerDied(PlayerHealth playerHealth)
    {
        playerHealth.health.Value = 3;
        SpawnPlayer(playerHealth.NetworkObject);
    }
    private void SpawnPlayer(NetworkObject playerObject)
    {
        if (NetworkManager.IsServer)
        {
            Transform randomSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
            playerObject.transform.position = randomSpawn.position;
            playerObject.transform.rotation = randomSpawn.rotation;
        }
    }

    
}
