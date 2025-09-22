using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public RoadManager1 roadManager;
    public int voteCounts;
    public int totalPlayers = 2;
    [SerializeField] private List<GameObject> roadToVotePositions = new List<GameObject>();
    private List<RoadScriptableObject> roadsToChoose = new List<RoadScriptableObject>();
    private List<VoteRoad1> roadsToVote = new List<VoteRoad1>();
    private List<GameObject> roadsInTheScene = new List<GameObject>();
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        roadManager = FindFirstObjectByType<RoadManager1>();
        for (int i = 0; i < 3; i++) 
        {
            RoadScriptableObject tempRoad = roadManager.roads[Random.Range(0, roadManager.roads.Count)];

            while (roadsToChoose.Contains(tempRoad) && roadsToChoose.Count < roadManager.roads.Count)
            {
                tempRoad = roadManager.roads[Random.Range(0, roadManager.roads.Count)];
            }
            roadsToChoose.Add(tempRoad);
            Transform transform = roadToVotePositions[i].transform;
            GameObject go = Instantiate(tempRoad.votedRoad.gameObject, roadToVotePositions[i].transform.position, Quaternion.identity);
            roadsInTheScene.Add(go);
            Destroy(roadToVotePositions[i]);
            
        }
    }

    public void RegisterVoteRoad(VoteRoad1 voteRoad)
    {
        if (!roadsToVote.Contains(voteRoad))
            roadsToVote.Add(voteRoad);
    }

    public void UpdateVotes()
    {
        int currentTotalVotes = 0;
        foreach (var road in roadsToVote)
        {
            currentTotalVotes += road.voteCounter;
        }

        if (currentTotalVotes >= totalPlayers)
        {
            DecideWinner();
        }
    }

    private void DecideWinner()
    {
        int maxVotes = -1;
        foreach (var road in roadsToVote)
        {
            if (road.voteCounter > maxVotes)
                maxVotes = road.voteCounter;
        }

        List<VoteRoad1> topCandidates = new List<VoteRoad1>();
        foreach (var road in roadsToVote)
        {
            if (road.voteCounter == maxVotes)
                topCandidates.Add(road);
        }

        VoteRoad1 winner = null;
        if (topCandidates.Count == 1)
        {
            winner = topCandidates[0];
        }
        else if (topCandidates.Count > 1)
        {
            int r = Random.Range(0, topCandidates.Count);
            winner = topCandidates[r];
            //Debug.Log("Tie detected, random winner chosen!");
        }

        if (winner != null)
        {
            //Debug.Log("Winner: " + winner.name + " with " + maxVotes + " votes");

            foreach (var road in roadsInTheScene)
            {
                Destroy(road);
            }
            int index = roadsToVote.IndexOf(winner);
            if (index >= 0 && index < roadsToChoose.Count)
            {
                RoadScriptableObject winnerSO = roadsToChoose[index];
                roadManager.SpawnRoad(winnerSO);
                //Debug.Log("Winner SO: " + winnerSO.name);
            }
        }
    }





}
