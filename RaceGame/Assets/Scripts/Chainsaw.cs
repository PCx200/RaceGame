using UnityEngine;

public class Chainsaw : MonoBehaviour
{
    public float moveAmplitude = 2f; 
    public float moveSpeed = 2f;    
    public float spinSpeed = 90f;    

    private float posOffset;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        posOffset = Mathf.Sin(Time.time * moveSpeed) * moveAmplitude;

        transform.position = startPos + new Vector3(posOffset, 0f, 0f);

        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Write what is happening to the player, when they hit the chainsaw.");
        }

    }
}
