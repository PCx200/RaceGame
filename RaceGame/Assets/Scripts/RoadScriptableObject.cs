using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Road", menuName = "ScriptableObjects/Roads", order = 2)]
public class RoadScriptableObject :ScriptableObject
{
    public VoteRoad1 votedRoad;
    public GameObject roadToSpawn;
    public int roadType;
}
