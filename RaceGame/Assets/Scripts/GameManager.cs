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
    [NonSerialized] public bool isSplited;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject boarderPanel;

    [SerializeField] GameObject[] playerPrefabs;

    [SerializeField] GameObject mainCamera;

    public List<PlayerInput> ActivePlayers = new List<PlayerInput>();

    public List<UnityEvent> events;

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
            if (PlayerInputManager.instance.playerCount == 2)
            {
                if (boarderPanel != null && mainCamera != null)
                {
                    yield return new WaitForSeconds(5);
                    boarderPanel.SetActive(true);
                    mainCamera.SetActive(false);
                    isSplited = true;
                }
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

    public void PickCar(int index)
    {
        Debug.Log($"Index: {index}");
        PlayerInputManager.instance.playerPrefab = GetPlayerPrefab(index);
    }

    public void InvokeEvent(int eventId)
    {
        events[eventId].Invoke();
    }

    public void UpdateActivePlayers()
    { 
        ActivePlayers.Clear();
        ActivePlayers.AddRange(FindObjectsByType<PlayerInput>(FindObjectsSortMode.None));  
    }
   
    public void UpdateRound()
    { 
        // players join and start playing
        // when the active players hit the finish collider the round ends, UI is being desplayed 
        // camera for the chosing of the roads is activated, players choose a road and get returned to their spawn
        // camera for traps is activated, players place traps, when they both have placed their traps -> return them to their spawn points and immediatelly disable them
        // enable car cameras and spawn the cars

    
    }
}
