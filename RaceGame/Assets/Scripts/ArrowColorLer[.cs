using UnityEngine;

public class ArrowColorLerp : MonoBehaviour
{
    public enum Direction { forwards, backwards };

    public Direction direction;

    //public Color colorA = Color.yellow;
    //public Color colorB = Color.red;
    public float lerpSpeed = 2f;

    private MeshRenderer rend;

    void Start()
    {
        rend = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        float scroll = Time.time * 0.5f;
        //float t = (Mathf.Sin(Time.time * lerpSpeed) + 1f) / 2f; 
        //rend.material.color = Color.Lerp(colorA, colorB, t);
       // rend.material.mainTextureOffset = new Vector2(0f, -scroll);

        if (direction == Direction.forwards)
        {
            rend.material.mainTextureOffset = new Vector2(2.315f, -scroll);
        }

        else if (direction == Direction.backwards)
        {
            rend.material.mainTextureOffset = new Vector2(1.645f, -scroll);
        }
    }
}