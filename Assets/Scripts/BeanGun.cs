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

    #region Property



    #endregion

    #region Private Field



    #endregion

    #region MonoBehavior Callback

    private void Awake()
    {
        if (photonView.IsMine || !PhotonNetwork.IsConnected)
        {
            UserInput.onLeftMouseButtonPressed += Shoot;
        }
    }


    private void OnDestroy()
    {
        if (photonView.IsMine || !PhotonNetwork.IsConnected)
        {
            UserInput.onLeftMouseButtonPressed -= Shoot;
        }

    }

    #endregion

    #region Public Methods



    #endregion

    #region Private Methods

    public override void Shoot()
    {
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
            protectile.SendMessage("Setup", new ProjectileArgs {flyDirection = bulletDirection, flySpeed = 50, firingRange = 400, damage = 10, owner = gameObject}, SendMessageOptions.RequireReceiver);
        }
        
    }

    #endregion


}

public class HitscanGun: GunPart
{
    public float damage;
    public float firingRange = 400f;
    public override void Shoot()
    {
        if (Camera.main != null)
        {
            Ray ray = Camera.main.ViewportPointToRay (new Vector3(0.5f,0.5f));
            if (Physics.Raycast(ray,out RaycastHit hitinfo,firingRange))
            {
                if (hitinfo.collider.GetComponent<ITakeDamage>() != null)
                {
                    NetworkEventFirer.DealDamage(damage,hitinfo.collider.GetComponent<PhotonView>().ViewID);
                }
            }
        }
    }
}