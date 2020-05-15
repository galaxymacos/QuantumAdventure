using UnityEngine;

public class SMB_Maria: StateMachineBehaviour
{
    protected Animator anim;
    protected CharacterMovement characterMovement;
    protected Maria maria;
    public MariaSkill mariaSkill;
    

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        anim = animator;
        characterMovement = animator.GetComponentInParent<CharacterMovement>();
        maria = animator.GetComponentInParent<Maria>();
        maria.currentSkill = mariaSkill;
    }
}