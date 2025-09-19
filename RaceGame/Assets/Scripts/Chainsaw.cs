using UnityEngine;

public class Chainsaw : MonoBehaviour
{
    public Vector3 spinAxis = new Vector3 (0f, 0f, 1f);

    public float moveAmplitude = 2f; 
    public float moveSpeed = 2f;    
    public float spinSpeed = 90f;    

    private float posOffset;

    private Vector3 currentPos;

    void Start()
    {
        currentPos = transform.localPosition;
    }

    void Update()
    {
        posOffset = Mathf.Sin(Time.time * moveSpeed) * moveAmplitude;

        transform.localPosition = currentPos + new Vector3(posOffset, 0f, 0f);

        transform.Rotate(spinAxis, spinSpeed * Time.deltaTime, Space.Self);
    }
}
