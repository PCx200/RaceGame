using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int playerCount;
    [NonSerialized] public bool isSplited;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject boarderPanel;

    [SerializeField] GameObject[] playerPrefabs;

    public GameObject MainCamera;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        GetSpawnPoint(0);
        PickCar(0);
    }

    private void Update()
    {
       StartCoroutine(SplitScreen());
    }

    IEnumerator SplitScreen()
    {
        if (!isSplited)
        {           
            if (GetPlayerCount() == 2)
            {
                yield return new WaitForSeconds(5);
                boarderPanel.SetActive(true);
                //foreach (PlayerInput player in FindObjectsByType<PlayerInput>(sortMode: FindObjectsSortMode.None))
                //{
                //    //player.camera.enabled = true;
                //    //mainCamera.SetActive(false);
                //}
                isSplited = true;
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
        
    }
    public int GetPlayerCount()
    {
        return playerCount;
    }
    public void IncreasePlayerCount()
    {
        playerCount++;
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
}
