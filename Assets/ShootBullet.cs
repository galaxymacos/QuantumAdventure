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
            UserInput.LeftMouseButtonJustPressed += SpawnBullet;
        }
    }

    private void Update()
    {
        Debug.DrawRay(projectileSpawnTransform.position,projectileSpawnTransform.forward,Color.yellow, 2f);
    }


    private void OnDestroy()
    {
        if (photonView.IsMine || !PhotonNetwork.IsConnected)
        {
            UserInput.LeftMouseButtonJustPressed -= SpawnBullet;
        }

    }

    #endregion

    #region Public Methods



    #endregion

    #region Private Methods

    private void SpawnBullet()
    {
        Ray ray = Camera.main.ViewportPointToRay (new Vector3(0.5f,0.5f));
        
        if (Physics.Raycast(ray,out RaycastHit hitinfo,500, GameManager.Instance.whatIsGround))
        {
            print("spawn projectile");
            var protectile = PhotonNetwork.Instantiate(projectilePrefab.name, projectileSpawnTransform.position,
                Quaternion.identity);
            Vector3 direction = hitinfo.point - projectileSpawnTransform.position;
            protectile.SendMessage("Setup", new ProjectileArgs(direction, 1));

        }
    }

    #endregion


}
