using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using static Unity.Cinemachine.CinemachineSplineRoll;
using System.Collections;
using UnityEngine.UIElements;
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
    [SerializeField] private int lastRoadType;
    [SerializeField]private Transform lastEndPoint;
    [SerializeField] Transform finishPoint;

    GameObject tempSpawner;
    int yRotation;

    private void Start()
    {
        placedRoads.Add((0, startingRoad));
        lastRoad = placedRoads[placedRoads.Count - 1].Item2.transform;
        lastRoadType = placedRoads[placedRoads.Count - 1].Item1;
        finishPoint.position = lastRoad.position;
        SpawnSpawnerInTheCorrectDirection();
    }
    

    public void SpawnSpawnerInTheCorrectDirection()
    {
        Transform newRoadTransform = lastRoad;

        if (lastRoadType == 0)
        {
            Vector3 spawnPos = new Vector3(lastEndPoint.position.x, lastEndPoint.position.y, lastEndPoint.position.z + 20);

            tempSpawner = Instantiate(roadsToVoteSpawner, spawnPos, new Quaternion(0,0,0,0));
        }
        else if (lastRoadType == 1)
        {
            Vector3 spawnPos = new Vector3(lastEndPoint.position.x - 20, lastEndPoint.position.y, lastEndPoint.position.z);
            Quaternion rotation = new Quaternion(0, lastEndPoint.localEulerAngles.y - 90, 0, 0);
            tempSpawner = Instantiate(roadsToVoteSpawner, spawnPos, rotation);
        }
        else
        {
            Vector3 spawnPos = new Vector3(lastEndPoint.position.x + 20, lastEndPoint.position.y, lastEndPoint.position.z);
            Quaternion rotation = new Quaternion(0, lastEndPoint.localEulerAngles.y + 90, 0, 0);
            tempSpawner = Instantiate(roadsToVoteSpawner, spawnPos, rotation);
        }
    }
    public void SpawnRoad(RoadScriptableObject roadData)
    {
        GameObject newRoad = Instantiate(roadData.roadToSpawn);
        placedRoads.Add((roadData.roadType, newRoad));

        lastRoad = placedRoads[placedRoads.Count - 1].Item2.transform;
        lastRoadType = placedRoads[placedRoads.Count - 1].Item1;

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
        finishPoint.rotation = lastEndPoint.rotation;

        Destroy(tempSpawner);
        StartCoroutine(PutNextRoad());
    }

    public IEnumerator PutNextRoad()
    {
        yield return new WaitForSeconds(2);
        GameManager.Instance.InvokeEvent(0);
    }
    
}
