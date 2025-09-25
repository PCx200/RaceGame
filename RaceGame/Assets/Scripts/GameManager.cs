using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private int playerCount;
    [SerializeField] public bool isSplited;
    public bool areCars = true;
    public bool isReadyToDrive = false;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject boarderPanel;

    [SerializeField] GameObject[] playerPrefabs;

    [SerializeField] GameObject mainCamera;

    public List<PlayerInput> ActivePlayers = new List<PlayerInput>();
    public List<PlayerInput> FinishedPlayers = new List<PlayerInput>();
    [SerializeField] int finishedPlayersCount;

    [SerializeField] int playersPlacedTrapsCount;

    public List<UnityEvent> Events;

    public GameObject halfScreenP1;
    public GameObject halfScreenP2;
    

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        //GetSpawnPoint(0);
        //PickCar(0);
    }

    private void Update()
    {
        StartCoroutine(SplitScreen());
    }

    IEnumerator SplitScreen()
    {
        if (!isSplited)
        {
            if (PlayerInputManager.instance.playerCount == 2 && areCars)
            {
                if (boarderPanel != null && mainCamera != null)
                {
                    isSplited = true;
                    yield return new WaitForSeconds(3);
                    boarderPanel.SetActive(true);
                    mainCamera.SetActive(false);
                    isReadyToDrive = true;

                }
            }
            else
            {
                isSplited = false;
                boarderPanel.SetActive(false);
                mainCamera.SetActive(true);
                isReadyToDrive = false;
            }
        }
        

    }
    public Transform GetSpawnPoint(int index)
    {
        if (index < spawnPoints.Length)
        {
            return spawnPoints[index];
        }
        return null;
    }

    public void TeleportToSpawnPoint()
    {
        for (int i = 0; i < ActivePlayers.Count; i++)
        {
            PlayerInput player = ActivePlayers[i];
            player.transform.position = spawnPoints[i].position;
        }
    }
    public int GetPlayerCount()
    {
        return playerCount;
    }
    public void UpdatePlayerCount()
    {
        playerCount = PlayerInputManager.instance.playerCount;  
    }

    public GameObject GetPlayerPrefab(int index)
    {
        return playerPrefabs[index];
    }

    public int GetPlayersPlacedTrapsCount()
    { 
        return playersPlacedTrapsCount;
    }
    public void IncreasePlayersPlacedTrapsCount()
    { 
        playersPlacedTrapsCount++;
    }

    public void ZerofyPlayerPlacedTrapCount()
    {
        playersPlacedTrapsCount = 0;
    }

    public void PickCar(int index)
    {
        Debug.Log($"Index: {index}");
        PlayerInputManager.instance.playerPrefab = GetPlayerPrefab(index);
    }

    public void InvokeEvent(int eventId)
    {
        Events[eventId].Invoke();
    }

    public void UpdateActivePlayers()
    { 
        ActivePlayers.Clear();
        ActivePlayers.AddRange(FindObjectsByType<PlayerInput>(FindObjectsSortMode.None));  
    }

    public void PlayerFinished(PlayerInput finishedPlayer)
    {
        FinishedPlayers.Add(finishedPlayer);
        finishedPlayersCount++; 
    }

    public void AreCars()
    {
        areCars = !areCars;
    }
}
