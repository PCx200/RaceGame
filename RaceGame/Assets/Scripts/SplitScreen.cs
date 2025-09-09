using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class SplitScreen : MonoBehaviour
{
    Camera cam;
    int index;

    private void Awake()
    {
        PlayerInputManager.instance.onPlayerJoined += OnPlayerJoined;
    }

    private void OnPlayerJoined(PlayerInput input)
    {
        PlayerInputManager.instance.playerPrefab = GameManager.Instance.GetPlayerPrefab(1);
        Transform spawnPoint = GameManager.Instance.GetSpawnPoint(input.playerIndex);
        Debug.Log(input.playerIndex);
        input.transform.position = spawnPoint.position;
        input.transform.rotation = spawnPoint.rotation;

        GameManager.Instance.IncreasePlayerCount();

        SetupCamera();
    }

    private void SetupCamera()
    {
        if (GameManager.Instance.GetPlayerCount() == 2)
        {
            cam.rect = new Rect(index == 0 ? 0 : 0.5f, 0, 0.5f, 1);
        }
    }

    void Start()
    {
        index = GetComponentInParent<PlayerInput>().playerIndex;
        cam = GetComponent<Camera>();
        cam.depth = index;

        if (index == 0)
        {
            OnPlayerJoined(GetComponentInParent<PlayerInput>());
        }

        SetupCamera();
    }
}
