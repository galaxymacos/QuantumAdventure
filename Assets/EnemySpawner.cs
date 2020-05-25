using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Rooms;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public Transform spawnTransform;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Spawn();
            
        }
    }

    private void Spawn()
    {
        MasterManager.NetworkInstantiate(enemyToSpawn, spawnTransform.position, Quaternion.identity);
    }
}




