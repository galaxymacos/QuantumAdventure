using UnityEngine;

[CreateAssetMenu(menuName = "Data/SkillDamage", fileName = "SkillDamage")]
public class SkillData: ScriptableObject
{
    public string skillName;
    public float damage;
}