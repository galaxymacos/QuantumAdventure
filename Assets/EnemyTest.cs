using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using UnityEngine;

public class EnemyTest : MonoBehaviour,ITakeDamage
{
    #region Serialized Field

    

    #endregion

    #region Property
    
    

    #endregion

    #region Private Field

    [SerializeField] private float healthPoint = 100f;

    #endregion

    #region MonoBehavior Callback



    #endregion

    #region Public Methods



    #endregion

    #region Private Methods



    #endregion


    public void TakeDamage(DamageArgs args)
    {
        healthPoint -= args.damageAmount;
        if (healthPoint <= 0)
        {
            Destroy(gameObject);
        }
    }
}
