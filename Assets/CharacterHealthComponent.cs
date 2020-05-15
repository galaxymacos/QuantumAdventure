using UnityEngine;

public class CharacterHealthComponent : MonoBehaviour, ITakeDamage, IHealable
{
    public float healthPoint = 100;

    public void TakeDamage(DamageArgs args)
    {
        healthPoint -= args.damageAmount;
    }

    public void Heal(HealArgs args)
    {
        healthPoint += args.healAmount;
    }
}