using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollisionTest : MonoBehaviour
{

    #region Serialized Field



    #endregion

    #region Property



    #endregion

    #region Private Field



    #endregion

    #region MonoBehavior Callback

    private void Start()
    {
        print("Activate sword collision test");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            print("Trigger collide with "+other.name);
            other.SendMessage("TakeDamage", new DamageArgs(70));
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            print($"Collide with {other.gameObject.name}");
        }
    }

    #endregion

    #region Public Methods



    #endregion

    #region Private Methods



    #endregion

    
}
