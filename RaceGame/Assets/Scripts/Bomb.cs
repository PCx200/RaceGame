using UnityEngine;

public class Bomb : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Trap")
        {
            Object.Destroy(gameObject);
            //play sound
            //play effect
            Object.Destroy(other.gameObject);
        }
    }
}
