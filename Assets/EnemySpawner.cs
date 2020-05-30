using System;
using System.Collections;
using Photon.Pun;
using Rooms;
using UnityEngine;

public class EnemySpawner : MonoBehaviourPun
{
    public static EnemySpawner instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Spawn(string enemyType, string dungeonName)
    {
        DungeonInfo targetDungeonInfo = MapInfo.instance.GetDungeonInfo(dungeonName);
        MasterManager.NetworkInstantiate(EnemyFetcher.instance.Fetch(enemyType),
            targetDungeonInfo.GetRandomSpawnLocations().position, Quaternion.identity);
    }
}