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
}
