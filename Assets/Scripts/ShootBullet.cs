using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ShootBullet : MonoBehaviourPun
{

    #region Serialized Field

    public GameObject projectilePrefab;
    public Transform projectileSpawnTransform;

    #endregion

    #region Property



    #endregion

    #region Private Field



    #endregion

    #region MonoBehavior Callback

    private void Awake()
    {
        if (photonView.IsMine || !PhotonNetwork.IsConnected)
        {
            UserInput.onLeftMouseButtonPressed += SpawnBullet;
        }
    }


    private void OnDestroy()
    {
        if (photonView.IsMine || !PhotonNetwork.IsConnected)
        {
            UserInput.onLeftMouseButtonPressed -= SpawnBullet;
        }

    }

    #endregion

    #region Public Methods



    #endregion

    #region Private Methods

    private void SpawnBullet()
    {
        print("spawn bullet");
        Ray ray = Camera.main.ViewportPointToRay (new Vector3(0.5f,0.5f));
        ray.GetPoint(400);
        var protectile = PhotonNetwork.Instantiate(projectilePrefab.name, projectileSpawnTransform.position,
            Quaternion.identity);
        Vector3 bulletDirection = Vector3.zero;
        if (Physics.Raycast(ray,out RaycastHit hitinfo,400, GameManager.Instance.whatIsGround))
        {
            print("spawn projectile");
            
            bulletDirection = hitinfo.point - projectileSpawnTransform.position;
        }
        else
        {
            var point = ray.GetPoint(400);
            bulletDirection = point - projectileSpawnTransform.position;

        }
        protectile.SendMessage("Setup", new ProjectileArgs {flyDirection = bulletDirection, flySpeed = 2, firingRange = 400, damage = 10});

    }

    #endregion


}


