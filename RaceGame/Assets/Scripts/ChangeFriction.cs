using UnityEngine;

public class ChangeFriction : MonoBehaviour
{
    public WheelCollider wheel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wheel = GetComponent<WheelCollider>();
        WheelFrictionCurve wheelFrictionCurve = wheel.sidewaysFriction;
        wheelFrictionCurve.stiffness = 0.0f;
        wheel.sidewaysFriction = wheelFrictionCurve;
    }
    
   
}
