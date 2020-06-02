using Photon.Pun;
using UnityEngine;

public class SMB_Maria: StateMachineBehaviour
{
    protected Animator anim;
    protected MariaMovement characterMovement;
    protected PlayerManager playerManager;
    protected Maria maria;
    protected PhotonView photonView;
    protected HitBoxDealDamage hitBoxDealDamage;
    [Tooltip("The skill name represented by this animation")]
    public string skillName;
    

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        anim = animator;
        photonView = anim.GetComponent<PhotonView>();
        characterMovement = animator.GetComponent<MariaMovement>();
        maria = animator.GetComponent<Maria>();
        hitBoxDealDamage = animator.GetComponent<HitBoxDealDamage>();
        hitBoxDealDamage.currentSkill = skillName;
        playerManager = animator.GetComponent<PlayerManager>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        
    }
}