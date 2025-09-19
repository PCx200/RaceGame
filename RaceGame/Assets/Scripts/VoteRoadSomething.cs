using UnityEngine;

public class VoteRoad1: MonoBehaviour
{
    public int voteCounter;
    private void OnTriggerEnter(Collider other)
    {
        voteCounter++;
    }

    private void OnTriggerExit(Collider other)
    {
        voteCounter--;
    }
}
