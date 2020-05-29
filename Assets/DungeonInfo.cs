using System.Collections.Generic;
using UnityEngine;

public class DungeonInfo: MonoBehaviour
{
    [SerializeField] private string dungeonName;
    [SerializeField] private List<GameObject> enemyWaypoints;
    [SerializeField] private List<Transform> enemySpawnlocations;

    public Transform GetRandomSpawnLocations()
    {
        if (enemySpawnlocations.Count == 0)
        {
            Debug.LogError("The Array enemyspawnlocation is empty");
            return null;
        }
        return enemySpawnlocations[Random.Range(0, enemySpawnlocations.Count)];
    }
}