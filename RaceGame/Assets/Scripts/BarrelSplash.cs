using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrelSplash : MonoBehaviour
{
    [Tooltip("For the splashs images, create RawImages in the hierarchy, add the images to the RawImages and those to the list.")]

    public List<RawImage> barrelSplash = new List<RawImage>();
    public float fadeOutTimer = 1f;
    private bool splashIsShown = false;

    private int index;

    public PlaySoundEffect playSoundEffect;

    private void Awake()
    {
        playSoundEffect = GetComponent<PlaySoundEffect>();
    }

    private void FixedUpdate()
    {
        if(barrelSplash.Count == 0)
        {
            Debug.LogWarning("Splash images are missing.");
        }

        float t = 1 / fadeOutTimer;

        Color color = barrelSplash[index].color;

        if (splashIsShown)
        {
            color.a -= t * Time.deltaTime;
        }

        if (color.a <= 0f)
        {
            splashIsShown = false;
            barrelSplash[index].gameObject.SetActive(false);
            color.a = 1f;
        }

        barrelSplash[index].color = color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !splashIsShown)
        {
            index = Random.Range(0, barrelSplash.Count);
            barrelSplash[index].gameObject.SetActive(true);
            splashIsShown = true;
            playSoundEffect.PlaySound();
        }
    }
}
