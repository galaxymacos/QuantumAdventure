using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class HitBoxDealDamage : SerializedMonoBehaviour
{
    public Dictionary<string, HitBoxPart> hitboxs = new Dictionary<string, HitBoxPart>();
    public string currentSkill;
    public SkillDamage skillDamage;

    #region Serialized Field



    #endregion

    #region Property
    
    

    #endregion

    #region Private Field



    #endregion

    #region MonoBehavior Callback

    private void Awake()
    {
        foreach (var element in hitboxs.Values)
        {
            element.onHit += DealDamage;
        }
    }

    private void OnDestroy()
    {
        foreach (var element in hitboxs.Values)
        {
            element.onHit -= DealDamage;
        }
    }

    

    #endregion

    #region Public Methods

    public void TurnOnHitBox(string hitBoxName)
    {
        if (!hitboxs.ContainsKey(hitBoxName))
        {
            Debug.LogError($"Can't find hit box with name {hitBoxName} under {gameObject.name}");
            return;
        }
        hitboxs[hitBoxName].ActiveHitbox();
    }

    public void TurnOffHitBox(string hitBoxName)
    {
        if (!hitboxs.ContainsKey(hitBoxName))
        {
            Debug.LogError($"Can't find hit box with name {hitBoxName} under {gameObject.name}");
        }
        hitboxs[hitBoxName].DeactivateHitbox();

    }

    #endregion

    #region Private Methods

    private void DealDamage(object sender, HitEventArgs e)
    {
        Debug.Log("try to deal damage");
        var takeDamageParts = e.hitCollider.GetComponents<ITakeDamage>();
        if (takeDamageParts != null && takeDamageParts.Length > 0)
        {
            foreach (var takeDamageComponent in takeDamageParts)
            {
                takeDamageComponent.TakeDamage(new DamageArgs(skillDamage.GetSkillDamage(currentSkill)));

            }
        }
    }

    #endregion


}

