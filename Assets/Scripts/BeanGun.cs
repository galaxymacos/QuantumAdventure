using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Rooms;
using UnityEngine;

public class BeanGun : GunPart
{

    #region Serialized Field

    public GameObject projectilePrefab;
    public Transform projectileSpawnTransform;

    #endregion


    #region Private Methods

    public override void Fire()
    {
        if (bulletLeft == 0)
        {
            print("no bullet in catridge");
            return;
        }

        bulletLeft--;
        
        print("Bean gun shoot");
        if (Camera.main != null)
        {
            Ray ray = Camera.main.ViewportPointToRay (new Vector3(0.5f,0.5f));
            GameObject protectile = MasterManager.NetworkInstantiate(projectilePrefab, projectileSpawnTransform.position,
                Quaternion.identity);
            Vector3 bulletDirection = Vector3.zero;
            if (Physics.Raycast(ray,out RaycastHit hitinfo,400))
            {
                bulletDirection = (hitinfo.point - projectileSpawnTransform.position).normalized;
            }
            else
            {
                var point = ray.GetPoint(400);
                bulletDirection = (point - projectileSpawnTransform.position).normalized;

            }
            protectile.SendMessage("Setup", new ProjectileArgs {FlyDirection = bulletDirection, FlySpeed = 50, FiringRange = 400, Damage = 10, Owner = gameObject}, SendMessageOptions.RequireReceiver);
        }
        
    }

    #endregion


}