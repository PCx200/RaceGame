using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class VirtualMouseManager : MonoBehaviour
{
    public static Mouse virtualMouse0;
    public static Mouse virtualMouse1;

    private void Awake()
    {
        // Create two different virtual mouse devices
        virtualMouse0 = (Mouse)InputSystem.AddDevice("VirtualMouse0");
        virtualMouse1 = (Mouse)InputSystem.AddDevice("VirtualMouse1");

        // Initialize positions
        InputState.Change(virtualMouse0.position, new Vector2(Screen.width / 4, Screen.height / 2));
        InputState.Change(virtualMouse1.position, new Vector2(Screen.width * 3 / 4, Screen.height / 2));
    }
}
