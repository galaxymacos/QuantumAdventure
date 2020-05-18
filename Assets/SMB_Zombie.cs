using UnityEngine;

public class SMB_Zombie : StateMachineBehaviour
{
    public string skillName;
    protected Vampire vampire;
    protected SkillDamage skillDamage;
    protected HitBoxDealDamage hitBoxDealDamage;
    private bool hasSetup;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        if (!hasSetup)
        {
            vampire = animator.GetComponent<Vampire>();
            skillDamage = animator.GetComponent<SkillDamage>();
            hitBoxDealDamage = animator.GetComponent<HitBoxDealDamage>();
            hasSetup = true;
        }

        hitBoxDealDamage.currentSkill = skillName;
    }
    
    
}