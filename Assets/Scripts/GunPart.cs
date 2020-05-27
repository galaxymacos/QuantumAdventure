using System;
using Photon.Pun;
using UnityEngine;

public abstract class GunPart: MonoBehaviourPun
{
    #region Property

    public int catridge;
    public int bulletLeft;
    public int bulletTotal;
    public bool holdingTrigger;
    public float firingRate;
    public bool isFullyAuto;
    public float firingRange;
    public string gunName;
    public Sprite icon;

    #endregion

    #region MonoBehaviour Callback

    private void Awake()
    {
        bulletLeft = catridge;
    }

    public virtual void OnUpdate()
    {
    }

    #endregion

    #region Public method

    public abstract void Fire();

    public virtual void Reload()
    {
        if (bulletLeft == catridge || bulletTotal == 0)    // There is no need to fill the catridge
        {
            return;
        }
        // the gun has enough bullet to fill the catridge
        if (bulletTotal - (catridge - bulletLeft) >= 0)
        {
            int difference = catridge - bulletLeft;
            bulletTotal -= difference;
            bulletLeft = catridge;
        }
        else if(bulletTotal>0)// the gun doesn't have enough bullet to fill the catridge
        {
            bulletLeft += bulletTotal;
            bulletTotal = 0;
        }
    }

    #endregion
}