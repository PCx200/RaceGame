using UnityEngine;

[CreateAssetMenu(fileName = "Road", menuName = "ScriptableObjects/Roads", order = 2)]
public class RoadScriptableObject :ScriptableObject
{
    public GameObject votedRoad;
    public GameObject roadToSpawn;
}
