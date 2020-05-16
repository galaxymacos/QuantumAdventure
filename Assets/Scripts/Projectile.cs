using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    #region Serialized Field



    #endregion

    #region Property



    #endregion

    #region Private Field

    private float flySpeed;
    private Vector3 flyDirection;
    private float firingRange;
    private bool hasSetup;
    private float damage;

    private GameObject owner;

    #endregion

    #region MonoBehavior Callback

    private void Update()
    {
        if (hasSetup)
        {
            transform.Translate(flyDirection * (flySpeed * Time.deltaTime));
        }
    }

    #endregion

    #region Public Methods

    public void Setup(ProjectileArgs args)
    {
        flySpeed = args.flySpeed;
        flyDirection = args.flyDirection;
        damage = args.damage;
        firingRange = args.firingRange;
        owner = args.owner;

        hasSetup = true;
    }
    
    #endregion

    #region Private Methods

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == owner) return;
        var damagableObjects = other.GetComponents<ITakeDamage>();
        foreach (var t in damagableObjects)
        {
            t.TakeDamage(new DamageArgs(damage));
        }
    }

    #endregion

    
}

public class ProjectileArgs
{
    public GameObject owner;
    public Vector3 flyDirection;
    public float flySpeed;
    public float firingRange;
    public float damage;

}


