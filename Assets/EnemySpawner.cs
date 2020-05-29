using System;
using System.Collections;
using Photon.Pun;
using Rooms;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
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

    private void Spawn(string enemyType, string dungeonName)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            DungeonInfo targetDungeonInfo = MapInfo.instance.GetDungeonInfo(dungeonName);
            MasterManager.NetworkInstantiate(EnemyFetcher.instance.Fetch(enemyType), targetDungeonInfo.GetRandomSpawnLocations().position, Quaternion.identity);
        }
    }
}