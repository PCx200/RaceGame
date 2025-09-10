using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float boostStrength = 20f;
    public float upwardStrength = 10f;
    public float boostDuration = 1f; //large number = more time to get to the destination

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            VelocityChanger boost = rb.GetComponent<VelocityChanger>();
            if (boost != null)
            {
                Vector3 dir = -Vector3.forward * boostStrength + Vector3.up * upwardStrength; //the calculation of the boost
                boost.ApplyBoost(dir, boostStrength, boostDuration);
            }
            Debug.Log(other.CompareTag("Player"));
        }
    }
}
