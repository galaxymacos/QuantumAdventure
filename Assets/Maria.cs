using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Maria : MonoBehaviour
{
    public MariaData data;

    #region Serialized Field

    public SkillData[] skillDamage;
    
    public HitBoxPart rightLeg;
    public HitBoxPart sword;


    #endregion

    #region Property

    public MariaSkill currentSkill;
    public SkillData CurrentSkillData => skillDamage.FirstOrDefault(skill => skill.type == currentSkill);

    #endregion

    #region Private Field
    

    #endregion

    #region MonoBehavior Callback

    private void Awake()
    {
        sword.onHit += DealDamage;
        rightLeg.onHit += DealDamage;
    }

    private void OnDestroy()
    {
        sword.onHit -= DealDamage;
        rightLeg.onHit -= DealDamage;
    }

    private void DealDamage(object sender, HitEventArgs e)
    {
        print($"try deal {GetCurrentSkillDamageArgs().damageAmount} to {e.hitCollider.name}");
        e.hitCollider.SendMessage("TakeDamage", GetCurrentSkillDamageArgs());
    }

    #endregion

    #region Public Methods

    public DamageArgs GetCurrentSkillDamageArgs()
    {
        return new DamageArgs(CurrentSkillData.damage);
    }

    #endregion

    #region Private Methods



    #endregion


}