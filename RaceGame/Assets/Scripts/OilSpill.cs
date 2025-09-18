using UnityEngine;

public class OilSpill : MonoBehaviour
{
    public VelocityChanger player;
    public Vector3 spinAxis = new Vector3(0f, 1f, 0f);
    public float spinSpeed = 90f;
    public float spinDuration = 1f;
    private bool spinInAction = false;

    private bool isAligning = false;
    public float alignSpeed = 5f;

    private float t;

    private void Update()
    {
        if (spinInAction)
        {
            t += Time.deltaTime;

            player.transform.Rotate(spinAxis, spinSpeed * Time.deltaTime);

            if (t > spinDuration)
            {
                isAligning = true;
                t = 0;
                spinInAction = false;

                //Restore the tracktion of the player 
            }
        }

        if (isAligning)
        {
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, Vector3.up); //the z axis for the final rotation of the player 

            player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, targetRotation, alignSpeed * Time.deltaTime * 100f); //smoth transition between the current and the final rotation

            if (Quaternion.Angle(player.transform.rotation, targetRotation) < 0.1f)
            {
                player.transform.rotation = targetRotation;
                isAligning = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spinInAction = true;
            Debug.Log("Write what is happening to the player, when they go throught the oil spill aka lack of traction.");
        }
    }
}
