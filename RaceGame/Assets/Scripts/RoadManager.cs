using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using static Unity.Cinemachine.CinemachineSplineRoll;
using System.Collections;
using UnityEngine.UIElements;
using Unity.Cinemachine;
public class RoadManager : MonoBehaviour
{
    public int totalPlayers = 2; 
    
    public List<RoadScriptableObject> roads = new List<RoadScriptableObject>();
    public GameObject roadsToVoteSpawner;
    [SerializeField] Transform spawnerTransform;
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
    [SerializeField] public float yRotation = 180f;

    private void Start()
    {
        GameManager.Instance.UpdateActivePlayers();

        placedRoads.Add((0, startingRoad));
        lastEndPoint = placedRoads[placedRoads.Count - 1].Item2.transform.GetChild(0).GetChild(3);
        lastRoad = placedRoads[placedRoads.Count - 1].Item2.transform;
        lastRoadType = placedRoads[placedRoads.Count - 1].Item1;
        finishPoint.position = lastRoad.position;
    }

    private void UpdateYRotation()
    {
        if (yRotation < -1f)
        {
            yRotation = 270f;
        }
        else if (yRotation > 359f)
        {
            yRotation = 0f;
        }
    }

    public void SpawnSpawnerInTheCorrectDirection()
    {
        Transform newRoadTransform = lastRoad;
        Debug.Log(placedRoads[0]);
        lastEndPoint = placedRoads[placedRoads.Count - 1].Item2.transform.GetChild(0).GetChild(3);
        Vector3 spawnPoint = spawnerTransform.position;

        if (lastRoadType == 0)
        {

            tempSpawner = Instantiate(roadsToVoteSpawner, spawnPoint, Quaternion.Euler(new Vector3(0,0,0)));
        }
        else if (lastRoadType == 1)
        {
            yRotation -= 90f;
            tempSpawner = Instantiate(roadsToVoteSpawner, spawnPoint, Quaternion.Euler(new Vector3(0, 0, 0)));
        }
        else
        {
            yRotation += 90f;
            tempSpawner = Instantiate(roadsToVoteSpawner, spawnPoint, Quaternion.Euler(new Vector3(0,0,0)));
        }


    }
    public void SpawnRoad(RoadScriptableObject roadData, Transform winnerTransform)
    {
        GameObject newRoad = Instantiate(roadData.roadToSpawn);
        placedRoads.Add((roadData.roadType, newRoad));
       
        lastRoad = placedRoads[placedRoads.Count - 1].Item2.transform;
        lastRoadType = placedRoads[placedRoads.Count - 1].Item1;

        // Get spawn markers
        Transform startPoint = newRoad.transform.Find("StartPoint");
        Transform endPoint = newRoad.transform.GetChild(0).GetChild(3);

        if (lastEndPoint == null)
        {
            // First road: just place it at origin
            newRoad.transform.position = Vector3.zero;
            newRoad.transform.rotation = Quaternion.identity;
        }
        else
        {
            if (placedRoads.Count >=2)
            {
                Transform previousLast = placedRoads[placedRoads.Count - 2].Item2.transform;

                newRoad.transform.position = previousLast.GetChild(0).GetChild(3).position;

                newRoad.transform.rotation = Quaternion.Euler(0,winnerTransform.rotation.y + yRotation,0) ;
            }
        }

        // Update tracker
        lastEndPoint = endPoint;
        finishPoint.position = lastEndPoint.position;
        finishPoint.rotation = lastEndPoint.rotation;

        finishPoint.SetParent(placedRoads[placedRoads.Count - 1].Item2.transform);

        Destroy(tempSpawner);
        //SpawnSpawnerInTheCorrectDirection();
        GameManager.Instance.TeleportToSpawnPoint();
        GameManager.Instance.InvokeEvent(1);
    }

    public IEnumerator PutNextRoad()
    {
        yield return new WaitForSeconds(2);
        GameManager.Instance.InvokeEvent(0);
    }
    
}
