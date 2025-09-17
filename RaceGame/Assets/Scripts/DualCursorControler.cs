using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class DualCursorController : MonoBehaviour
{
    public PlayerInput player0;
    public PlayerInput player1;
    public RectTransform cursor0;
    public RectTransform cursor1;
    public float speed = 10f;

    void Update()
    {
        if (player0 != null)
        {
            var gamepad0 = player0.GetDevice<Gamepad>();
            if (gamepad0 != null)
            {
                Vector2 move = gamepad0.leftStick.ReadValue() * speed;
                MoveMouse(VirtualMouseManager.virtualMouse0, move);
                cursor0.position = VirtualMouseManager.virtualMouse0.position.ReadValue();
            }
        }

        if (player1 != null)
        {
            var gamepad1 = player1.GetDevice<Gamepad>();
            if (gamepad1 != null)
            {
                Vector2 move = gamepad1.leftStick.ReadValue() * speed;
                MoveMouse(VirtualMouseManager.virtualMouse1, move);
                cursor1.position = VirtualMouseManager.virtualMouse1.position.ReadValue();
            }
        }
    }

    void MoveMouse(Mouse mouse, Vector2 delta)
    {
        if (mouse == null) return;

        Vector2 current = mouse.position.ReadValue();
        Vector2 newPos = new Vector2(
            Mathf.Clamp(current.x + delta.x, 0, Screen.width),
            Mathf.Clamp(current.y + delta.y, 0, Screen.height)
        );

        InputState.Change(mouse.position, newPos);
        InputState.Change(mouse.delta, delta);
    }
}
