using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeControlScheme : MonoBehaviour
{
    public PlayerInput playerInput;
    private void Awake()
    {
        playerInput.SwitchCurrentControlScheme("Gamepad", Gamepad.current);
    }
}
