using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Header("Car Settings")]
    [SerializeField] float motorForce = 100f;
    [SerializeField] float breakForce = 1000f;
    [SerializeField] float maxSteerAngle = 30f;

    [Header("Wheels")]
    [SerializeField] WheelCollider frontLeftWheelCollider;
    [SerializeField] WheelCollider frontRightWheelCollider;
    [SerializeField] WheelCollider backLeftWheelCollider;
    [SerializeField] WheelCollider backRightWheelCollider;

    [SerializeField] Transform frontLeftWheelTransform;
    [SerializeField] Transform frontRightWheelTransform;
    [SerializeField] Transform backLeftWheelTransform;
    [SerializeField] Transform backRightWheelTransform;

    //[SerializeField] float movementSpeed;
    Vector2 movementInput;

    bool isBraking;

    private float currentSteerAngle;
    private float currentBreakForce;

    //[SerializeField] float brakingPower;

    void Update()
    {
        if (GameManager.Instance.isSplited)
        {
            HandleMotor();
            HandleSteering();
            UpdateWheels();
        }
    }

    public void OnMove(InputValue input)
    {
        movementInput = input.Get<Vector2>();
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

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = movementInput.y * motorForce;
        frontRightWheelCollider.motorTorque = movementInput.y * motorForce;

        backLeftWheelCollider.motorTorque = movementInput.y * motorForce;
        backRightWheelCollider.motorTorque = movementInput.y * motorForce;

        //currentBreakForce = isBraking ? breakForce : 0f;
        //ApplyBraking();
    }

    private void ApplyBraking()
    {
        float brake = isBraking ? currentBreakForce : 0f;

        frontLeftWheelCollider.brakeTorque = brake;
        frontRightWheelCollider.brakeTorque = brake;
        backLeftWheelCollider.brakeTorque = brake;
        backRightWheelCollider.brakeTorque = brake;
       
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * movementInput.x;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateSingleWheel(WheelCollider wheelColider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelColider.GetWorldPose(out pos, out rot);
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(backLeftWheelCollider, backLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(backRightWheelCollider, backRightWheelTransform);
    }
    //private void Move()
    //{
    //    Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);

    //    float currentSpeed = motorForce;
    //    if (isBraking)
    //    {
    //        currentSpeed *= brakingPower;
    //    }
    //    else
    //    {
    //        currentSpeed = movementSpeed;
    //    }


    //    transform.Translate(move * currentSpeed * Time.deltaTime, Space.World);
    //}
}
