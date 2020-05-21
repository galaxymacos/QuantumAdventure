using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HitBoxPart : MonoBehaviour
{

    #region Serialized Field

    #endregion

    #region Property

    public event EventHandler<HitEventArgs> onHit;

    public bool IsHitBoxActive => isHitBoxActive;

    #endregion

    #region Private Field

    private bool isHitBoxActive;
    private bool hasSent;

    #endregion

    #region MonoBehavior Callback

    private void OnTriggerStay(Collider other)
    {
        
        if (isHitBoxActive)
        {
            if (!hasSent)
            {
                if (other.transform.root.gameObject != transform.root.gameObject)
                {
                    print("Hit "+other.name);
                    onHit?.Invoke(this, new HitEventArgs(other));
                    hasSent = true;
                }
                
            }
        }
    }

    #endregion

    #region Public Methods

    public void ActiveHitbox()
    {
        isHitBoxActive = true;
        print("activate sword hitbox");
    }

    public void DeactivateHitbox()
    {
        isHitBoxActive = false;
        hasSent = false;
        print("Deactivate sword hitbox");
    }

    #endregion

    #region Private Methods



    #endregion


}

public class HitEventArgs
{
    public Collider hitCollider;

    public HitEventArgs(Collider hitCollider)
    {
        this.hitCollider = hitCollider;
    }
}