using UnityEngine;

public class SpeedPad : MonoBehaviour
{
    public enum SpeedDirection { Forward, Backwards }

    public SpeedDirection speedDirection;

    public float boostStrength = 20f;
    public float boostDuration = 1f; //large number = more time to get to the destination

    private PlaySoundEffect playSoundEffect;

    private void Awake()
    {
        playSoundEffect = GetComponent<PlaySoundEffect>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            VelocityChanger boost = rb.GetComponent<VelocityChanger>();

            if (boost != null)
            {
                if (speedDirection == SpeedDirection.Forward)
                {
                    boost.ApplyBoost(transform.forward, boostStrength, boostDuration); //the calculation of the boost
                }

                else if (speedDirection == SpeedDirection.Backwards)
                {
                    boost.ApplyBoost(-transform.forward, boostStrength, boostDuration); //the calculation of the boost
                }

                playSoundEffect.PlaySound();
            }
        }
    }
}
