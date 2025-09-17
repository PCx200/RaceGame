using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosion;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Trap")
        {
            //play sound
            explosion = Instantiate(explosion,transform.position,Quaternion.identity);
            Object.Destroy(gameObject);
            Object.Destroy(other.gameObject);
            Destroy(explosion, 1f);
        }
    }
}
