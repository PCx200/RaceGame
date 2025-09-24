using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    float currentSpeed;


    [Header("Car Settings")]
    [SerializeField] float motorForce = 100f;
    [SerializeField] float breakForce = 1000f;
    [SerializeField] float maxSteerAngle = 30f;

    [Header("Wheels")]
    [SerializeField] public WheelCollider frontLeftWheelCollider;
    [SerializeField] public WheelCollider frontRightWheelCollider;
    [SerializeField] public WheelCollider backLeftWheelCollider;
    [SerializeField] public WheelCollider backRightWheelCollider;

    [SerializeField] Transform frontLeftWheelTransform;
    [SerializeField] Transform frontRightWheelTransform;
    [SerializeField] Transform backLeftWheelTransform;
    [SerializeField] Transform backRightWheelTransform;

    [Header("Explosion")]
    [SerializeField] private GameObject particleEffect;
    [SerializeField] private GameObject explodedScreen;
    [SerializeField] private GameObject passedFinishlineScreen;

    //[SerializeField] float movementSpeed;
    Vector2 movementInput;

    bool isBraking;

    private float currentSteerAngle;
    private float currentBreakForce;

    //[SerializeField] float brakingPower;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (GameManager.Instance.isSplited)
        {
            HandleMotor();
            HandleSteering();
            UpdateWheels();

            DisplaySpeed();

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

        currentBreakForce = isBraking ? breakForce : 0f;
        ApplyBraking();
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
        //ChangeFriction(frontLeftWheelCollider);
       // ChangeFriction(frontRightWheelCollider);
        //ChangeFriction(backRightWheelCollider);
        //ChangeFriction(backLeftWheelCollider);
       
    }

    private void ChangeFriction(WheelCollider wheel)
    {
        WheelFrictionCurve wheelFrictionCurve = wheel.sidewaysFriction;
        if (currentSteerAngle > 20)
        {
            wheelFrictionCurve.stiffness = 2f;
            wheel.sidewaysFriction = wheelFrictionCurve;
        }
        else
        {
            wheelFrictionCurve.stiffness = 2.5f;
            wheel.sidewaysFriction = wheelFrictionCurve;
        }
        
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

    private void DisplaySpeed()
    {
        float speedKmh = rb.linearVelocity.magnitude * 3.6f;
        currentSpeed = speedKmh;
        if (speedKmh > 200f)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * (200f / 3.6f);
        }

        //Debug.Log("Car Speed: " + speedKmh.ToString("F1") + " km/h");
    }

    public void OnDeath()
    {
        GameObject explosion = Instantiate(particleEffect, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(explosion, 5);
        //GameObject explosionHalfScreen =
    }

    //private void Move()
    //{
    //    Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);

    //    float currentSpeed = movementSpeed;
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
