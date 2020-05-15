using UnityEngine;

[CreateAssetMenu(menuName = "Data/SkillDamage", fileName = "SkillDamage")]
public class SkillData: ScriptableObject
{
    public MariaSkill type;
    public float damage;
}