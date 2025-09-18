using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public GameObject explosion;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            explosion = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(explosion, 2f);
            Debug.Log("Write what is happening to the player, when they hit the a trap.");
        }
    }
}
