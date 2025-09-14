using UnityEngine;
using UnityEngine.UI;

public class BarrelSplash : MonoBehaviour
{
    public RawImage barrelSplash;
    public float fadeOutTimer = 1f;

    public bool splashIsShown = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            barrelSplash.gameObject.SetActive(true);
            splashIsShown = true;
        }
    }

    private void FixedUpdate()
    {
        float t = 1 / fadeOutTimer;

        Color color = barrelSplash.color;

        if (splashIsShown)
        {
            color.a -= t * Time.deltaTime;
        }

        if (color.a <= 0f)
        {
            splashIsShown = false;
            barrelSplash.gameObject.SetActive(false);
            color.a = 1f;
        }

        barrelSplash.color = color;
    }
}
