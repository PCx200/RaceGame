using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public static PlayerInput player1;
    public static PlayerInput player2;

    private void OnEnable()
    {
        if (player1 != null && player2 != null)
        { 
            PlayerInputManager.instance.onPlayerJoined += HandlePlayerJoined;
        }
    }

    private void OnDisable()
    {
        if (player1 != null && player2 != null)
        {
            PlayerInputManager.instance.onPlayerJoined -= HandlePlayerJoined;
        }
    }

    private void HandlePlayerJoined(PlayerInput playerInput)
    {
        if (player1 == null)
        {
            player1 = playerInput;
            Debug.Log("Player 0 joined: " + player1.gameObject.name);
        }
        else if (player2 == null)
        {
            player2 = playerInput;
            Debug.Log("Player 1 joined: " + player2.gameObject.name);
        }
        else
        {
            Debug.Log("Extra player joined, ignoring: " + playerInput.gameObject.name);
        }
    }
}
