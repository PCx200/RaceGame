using UnityEngine;

public class Barrier : MonoBehaviour
{
    public GameObject wood;

    public GameObject planks;

    private PlaySoundEffect playSoundEffect;

    private void Awake()
    {
        playSoundEffect = GetComponent<PlaySoundEffect>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            wood = Instantiate(wood, transform.position, Quaternion.identity);
            playSoundEffect.PlaySound();
            Destroy(planks);
            Destroy(wood, 1f);
            Destroy(gameObject, 2f);

            //lower the players speed
        }
    }
}
