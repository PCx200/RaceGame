using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SplitScreen : MonoBehaviour
{
    Camera cam;
    int index;

    public PlayerInput playerInput;

    private void Awake()
    {
        PlayerInputManager.instance.onPlayerJoined += OnPlayerJoined;
    }

    private void Update()
    {

    }

    private void OnPlayerJoined(PlayerInput input)
    {
        GameManager.Instance.UpdatePlayerCount();
        GameManager.Instance.UpdateActivePlayers();

        GameManager.Instance.PickCar(1);

        Debug.Log(input);

        Transform spawnPoint = GameManager.Instance.GetSpawnPoint(input.playerIndex);
        Debug.Log(input.playerIndex);
        input.transform.position = spawnPoint.position;
        input.transform.rotation = spawnPoint.rotation;
    }

    void Start()
    {
        index = GetComponentInParent<PlayerInput>().playerIndex;
    }

    public PlayerInput SetPlayerInput(List<PlayerInput> inputs, PlayerInput input)
    {
        for (int i = 0; i < inputs.Count; i++)
        {
            input = inputs[i];
        }
        return input;    
    }
}
