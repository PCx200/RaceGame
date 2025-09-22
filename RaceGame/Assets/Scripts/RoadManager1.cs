using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using static Unity.Cinemachine.CinemachineSplineRoll;
public class RoadManager1 : MonoBehaviour
{
    public int totalPlayers = 2; 
    
    public List<RoadScriptableObject> roads = new List<RoadScriptableObject>();
    public GameObject roadsToVoteSpawner;
    [SerializeField] public List<(int,GameObject)> placedRoads = new List<(int, GameObject)>();
    public GameObject startingRoad;
    private enum RoadContinue {Forward, Backward,Right,Left };

    private RoadContinue roadContinue = RoadContinue.Forward;
    
    private bool votingComplete = false;
    [SerializeField] Transform lastRoad;
    private int lastRoadType;
    [SerializeField]private Transform lastEndPoint;
    [SerializeField] Transform finishPoint;

    private void Start()
    {
        placedRoads.Add((0, startingRoad));
        lastRoad = placedRoads[placedRoads.Count - 1].Item2.transform;
        lastRoadType = placedRoads[placedRoads.Count - 1].Item1;
        finishPoint.position = lastRoad.position;
        SpawnSpawnerInTheCorrectDirection();
    }
    

    private void SpawnSpawnerInTheCorrectDirection()
    {
        lastRoad = placedRoads[placedRoads.Count - 1].Item2.transform;
        lastRoadType = placedRoads[placedRoads.Count - 1].Item1;
        Transform newRoadTransform = lastRoad;
        Vector3 pos = newRoadTransform.position;

        if (roadContinue == RoadContinue.Forward)
        {     
            pos.z += 30;
            Instantiate(roadsToVoteSpawner,pos,Quaternion.identity);
        }
        else if (roadContinue == RoadContinue.Backward)
        {   
            pos.z -= 30;
            newRoadTransform.position = pos;    
            Instantiate(roadsToVoteSpawner, pos, Quaternion.identity);
        }
        else if (roadContinue == RoadContinue.Right)
        {    
            pos.x += 30;
            newRoadTransform.position = pos;
            Instantiate(roadsToVoteSpawner, pos, Quaternion.identity);
        }
        else
        {
            pos.x -= 30;
            newRoadTransform.position = pos;
            Instantiate(roadsToVoteSpawner, pos, Quaternion.identity);
        }
    }
    public void SpawnRoad(RoadScriptableObject roadData)
    {
        GameObject newRoad = Instantiate(roadData.roadToSpawn);

        // Get spawn markers
        Transform startPoint = newRoad.transform.Find("StartPoint");
        Transform endPoint = newRoad.transform.Find("EndPoint");

        if (lastEndPoint == null)
        {
            // First road: just place it at origin
            newRoad.transform.position = Vector3.zero;
            newRoad.transform.rotation = Quaternion.identity;
        }
        else
        {
            // Align startPoint of new road with lastEndPoint
            Vector3 positionOffset = newRoad.transform.position - startPoint.position;
            newRoad.transform.position = lastEndPoint.position + positionOffset;

            // Match rotation too
            Quaternion rotationOffset = Quaternion.Inverse(startPoint.rotation) * newRoad.transform.rotation;
            newRoad.transform.rotation = lastEndPoint.rotation * rotationOffset;
        }

        // Update tracker
        lastEndPoint = endPoint;
        finishPoint.position = lastEndPoint.position;

    }
    
}
