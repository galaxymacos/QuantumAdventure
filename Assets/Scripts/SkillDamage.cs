using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class SkillDamage: SerializedMonoBehaviour
{

    [SerializeField] private List<SkillData> skillDatas;
    
    #region Properties

    public Dictionary<string, SkillDataRaw> skillDataDictionary;

    #endregion

    #region MonoBehaviour callback

    private void Awake()
    {
        skillDataDictionary = new Dictionary<string, SkillDataRaw>();
        foreach (var skillData in skillDatas)
        {
            skillDataDictionary.Add(skillData.skillName, skillData.skillDataRaw);
        }
    }

    #endregion

    public float GetSkillDamage(string skillName)
    {
        if (skillDataDictionary.ContainsKey(skillName))
        {
            return skillDataDictionary[skillName].damage;
        }
        else
        {
            Debug.LogError($"Can't find skill with the name: {skillName} under {gameObject.name}");
            return -1;
        }
    }
    
    public int GetSkillTakeDownValue(string skillName)
    {
        if (skillDataDictionary.ContainsKey(skillName))
        {
            return skillDataDictionary[skillName].takeDownValue;
        }
        else
        {
            Debug.LogError($"Can't find skill with the name: {skillName} under {gameObject.name}");
            return -1;
        }
    }
    
}

