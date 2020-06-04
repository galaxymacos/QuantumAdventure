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

    public event EventHandler<ColliderHitEventArgs> onHit;

    public bool IsHitBoxActive => _isHitBoxActive;

    #endregion

    #region Private Field

    private bool _isHitBoxActive;
    private bool _hasSent;

    #endregion

    #region MonoBehavior Callback

    private void OnTriggerStay(Collider other)
    {
        
        if (_isHitBoxActive)
        {
            if (!_hasSent)
            {
                if (other.transform.root.gameObject != transform.root.gameObject)
                {
                    print("Hit "+other.name);
                    onHit?.Invoke(this, new ColliderHitEventArgs(other));
                    _hasSent = true;
                }
                
            }
        }
    }

    #endregion

    #region Public Methods

    public void ActiveHitbox()
    {
        _isHitBoxActive = true;
        print("activate sword hitbox");
    }

    public void DeactivateHitbox()
    {
        _isHitBoxActive = false;
        _hasSent = false;
        print("Deactivate sword hitbox");
    }

    #endregion

    #region Private Methods



    #endregion


}

public class ColliderHitEventArgs
{
    public Collider HitCollider;

    public ColliderHitEventArgs(Collider hitCollider)
    {
        this.HitCollider = hitCollider;
    }
}