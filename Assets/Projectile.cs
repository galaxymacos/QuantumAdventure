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
    private bool hasSetup;

    #endregion

    #region MonoBehavior Callback

    private void Update()
    {
        if (hasSetup)
        {
            transform.Translate(flySpeed * flyDirection*Time.deltaTime);
        }
    }

    #endregion

    #region Public Methods

    public void Setup(ProjectileArgs args)
    {
        flySpeed = args.flySpeed;
        flyDirection = args.flyDirection;
        
        hasSetup = true;
    }
    
    #endregion

    #region Private Methods
    
    

    #endregion

    
}

public class ProjectileArgs
{
    public Vector3 flyDirection;
    public float flySpeed;

    public ProjectileArgs(Vector3 flyDirection, float flySpeed)
    {
        this.flyDirection = flyDirection;
        this.flySpeed = flySpeed;
        
    }
}


