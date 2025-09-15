using UnityEngine;
using UnityEngine.InputSystem;

public class DualCursors : MonoBehaviour
{
    public RectTransform cursorP1;
    public RectTransform cursorP2;
    public float speed = 1000f;

    private Vector2 posP1;
    private Vector2 posP2;

    private Vector2 moveP1;
    private Vector2 moveP2;

    void Start()
    {
        posP1 = new Vector2(Screen.width / 3f, Screen.height / 2f);
        posP2 = new Vector2(2f * Screen.width / 3f, Screen.height / 2f);
    }

    void Update()
    {
        posP1 += moveP1 * speed * Time.deltaTime;
        posP1.x = Mathf.Clamp(posP1.x, 0, Screen.width);
        posP1.y = Mathf.Clamp(posP1.y, 0, Screen.height);
        cursorP1.position = posP1;

        posP2 += moveP2 * speed * Time.deltaTime;
        posP2.x = Mathf.Clamp(posP2.x, 0, Screen.width);
        posP2.y = Mathf.Clamp(posP2.y, 0, Screen.height);
        cursorP2.position = posP2;
    }

    // Called by your InputAction "Move"
    public void OnMove(InputAction.CallbackContext ctx)
    {
        // Detect which device triggered this input
        var device = ctx.control.device;

        if (device == Gamepad.all[0]) // First gamepad
        {
            moveP1 = ctx.ReadValue<Vector2>();
        }
        else if (device == Gamepad.all[1]) // Second gamepad
        {
            moveP2 = ctx.ReadValue<Vector2>();
        }
    }
}
