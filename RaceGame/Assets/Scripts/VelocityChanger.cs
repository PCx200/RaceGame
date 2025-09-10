using UnityEngine;

public class VelocityChanger : MonoBehaviour
{
    [Tooltip("This script should be placed on the player or being integrated in the movement scrpt.")]

    private Rigidbody rb;
    private Vector3 baseVelocity; //velocity before boost
    private Vector3 startVelocity; //velocity after boost
    private bool boosting = false;
    float duration; //the duration of the jump / speed change
    float elapsedTime; //timer for slowing down the jump / speed change

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ApplyBoost(Vector3 direction, float strength, float dur)
    {
        duration = Mathf.Max(0.0001f, dur);
        baseVelocity = rb.linearVelocity; //saves the current velocity
        startVelocity = baseVelocity + direction.normalized * strength; 
        rb.linearVelocity = startVelocity;

        elapsedTime = 0f;

        boosting = true;
    }

    void FixedUpdate()
    {
        if (!boosting) return;

        elapsedTime += Time.fixedDeltaTime;
        float t = Mathf.Clamp01(elapsedTime / duration);

        rb.linearVelocity = Vector3.Lerp(startVelocity, baseVelocity, t);

        if (t >= 1f)
        {
            rb.linearVelocity = baseVelocity;
            boosting = false;
        }
    }
}