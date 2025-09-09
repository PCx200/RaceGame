using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    Vector2 movementInput;

    bool isBraking;
    [SerializeField] float brakingPower;

    void Update()
    {
        if (GameManager.Instance.isSplited)
        { 
            Move();
        }
    }

    public void OnMove(InputValue input)
    {
        movementInput = input.Get<Vector2>();
    }

    private void Move()
    {
        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);

        float currentSpeed = movementSpeed;
        if (isBraking)
        {
            currentSpeed *= brakingPower;
        }
        else
        {
            currentSpeed = movementSpeed;
        }


        transform.Translate(move * currentSpeed * Time.deltaTime, Space.World);
    }
    public void OnBreak(InputValue input)
    {
        isBraking = input.isPressed;

        if (isBraking)
        {
            Debug.Log("Breaking");
        }
        else { Debug.Log("Not Breaking"); }
    }
}
