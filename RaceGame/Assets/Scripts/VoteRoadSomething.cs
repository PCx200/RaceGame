using UnityEngine;

public class VoteRoad1: MonoBehaviour
{
    public int voteCounter;
    private RoadSpawner roadSpawner;
    private void Start()
    {
        roadSpawner = FindFirstObjectByType<RoadSpawner>();
        roadSpawner.RegisterVoteRoad(this);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.root.CompareTag("TrapSpawner"))
        {
            //print("Vote registered");
            voteCounter++;
            roadSpawner.UpdateVotes();
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.root.CompareTag("TrapSpawner"))
        {
            //print("Vote deregistered");
            voteCounter--;
            roadSpawner.UpdateVotes();
        }
        
    }
}
