﻿using Photon.Pun;
using UnityEngine;

public class HitscanGun : GunPart
{
    public float damage;
    public int takeDownValuePerShoot;
    public float nextFiringTime;

    public override void Fire()
    {
        print($"Hitscan gun tries to shoot");
        if (Camera.main != null)
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            if (Physics.Raycast(ray, out RaycastHit hitinfo, firingRange))
            {
                if (hitinfo.collider.GetComponent<ITakeDamage>() != null)
                {
                    print($"Hitscan gun deals damage to {hitinfo.collider.gameObject}");
                    NetworkEventFirer.DealDamage(damage, hitinfo.collider.GetComponent<PhotonView>().ViewID, takeDownValuePerShoot);
                }
            }
        }
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (!isFullyAuto)
        {
            return;
        }


        if (holdingTrigger)
        {
            GetComponent<SoapMovement>().SetAnimationBool("Gunplay", bulletLeft > 0);
            if (Time.time >= nextFiringTime)
            {
                nextFiringTime = Time.time + 1 / firingRate;
                Fire();

            }
        }
        else
        {
            GetComponent<SoapMovement>().SetAnimationBool("Gunplay",false);

            nextFiringTime = Time.time + 1 / firingRate;
        }
    }

    //
    // public override void Reload()
    // {
    //     
    // }
}