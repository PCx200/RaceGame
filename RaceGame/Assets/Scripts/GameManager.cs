using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int playerCount;
    bool isSplited;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject boarderPanel;

    [SerializeField] GameObject[] PlayerPrefabs;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        SplitScreen();
    }

    void SplitScreen()
    {
        if (!isSplited)
        {
            if (GetPlayerCount() == 2)
            {
                boarderPanel.SetActive(true);
                foreach (PlayerInput player in FindObjectsByType<PlayerInput>(sortMode: FindObjectsSortMode.None))
                {
                    player.camera.enabled = true;
                }
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
        return PlayerPrefabs[index];
    }
}
