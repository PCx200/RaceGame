using UnityEngine;
using UnityEngine.InputSystem;

public class TrapSpawner : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed = 5f;

    private Vector2 movementInput;
 

    public void OnMove(InputValue input)
    {
        movementInput = input.Get<Vector2>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 move = new Vector3(movementInput.y, 0f, movementInput.x);
        transform.Translate(move * movementSpeed * Time.deltaTime, Space.World);
    }
}
