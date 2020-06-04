using Photon.Pun;
using UnityEngine;

public class SmbMaria: StateMachineBehaviour
{
    protected Animator Anim;
    protected MariaMovement CharacterMovement;
    protected PlayerManager PlayerManager;
    protected Maria Maria;
    protected PhotonView PhotonView;
    protected HitBoxDealDamage HitBoxDealDamage;
    [Tooltip("The skill name represented by this animation")]
    public string skillName;
    

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Anim = animator;
        PhotonView = Anim.GetComponent<PhotonView>();
        CharacterMovement = animator.GetComponent<MariaMovement>();
        Maria = animator.GetComponent<Maria>();
        HitBoxDealDamage = animator.GetComponent<HitBoxDealDamage>();
        HitBoxDealDamage.currentSkill = skillName;
        PlayerManager = animator.GetComponent<PlayerManager>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        
    }
}