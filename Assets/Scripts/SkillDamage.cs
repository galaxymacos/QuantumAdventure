using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class SkillDamage: SerializedMonoBehaviour
{
    public static SkillDamage instance;

    #region Properties

    public Dictionary<string, int> skillsDamage = new Dictionary<string, int>();

    #endregion
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public int GetSkillDamage(string skillName)
    {
        if (skillsDamage.ContainsKey(skillName))
        {
            return skillsDamage[skillName];
        }
        else
        {
            Debug.LogError($"Can't find skill with the name: {skillName} under {gameObject.name}");
            return -1;
        }
    }
}