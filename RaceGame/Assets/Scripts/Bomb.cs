using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosion;

    public PlaySoundEffect playSoundEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Trap")
        {
            explosion = Instantiate(explosion,transform.position,Quaternion.identity);
            playSoundEffect = explosion.GetComponent<PlaySoundEffect>();
            playSoundEffect.PlaySound();
            Object.Destroy(gameObject);
            Object.Destroy(other.gameObject);
            Destroy(explosion, playSoundEffect.soundEffect.length);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            explosion = Instantiate(explosion, transform.position, Quaternion.identity);
            playSoundEffect = explosion.GetComponent<PlaySoundEffect>();
            playSoundEffect.PlaySound();
            CarController carController = collision.gameObject.GetComponent<CarController>();
            carController.OnDeath();
            Object.Destroy(gameObject);
            Object.Destroy(collision.gameObject);
            Destroy(explosion, playSoundEffect.soundEffect.length);
        }
    }
}
