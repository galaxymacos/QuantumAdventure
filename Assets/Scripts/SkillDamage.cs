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


    public SkillData skillData;
    
    public SkillDataRaw GetSkillData(string skillName)
    {
        return skillDataDictionary.ContainsKey(skillName) ? skillDataDictionary[skillName] : null;
    }
    
}

