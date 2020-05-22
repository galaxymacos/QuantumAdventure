using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
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
    
    public void Spawn()
    {
        print("Spawn enemy");
        PhotonNetwork.Instantiate(enemyToSpawn.name, spawnTransform.position, Quaternion.identity);
    }
}




