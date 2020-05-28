using Photon.Pun;
using UnityEngine;
using UnityEngine.Animations;

public class SMB_Soap: StateMachineBehaviour
{
    protected Animator anim;
    protected SoapMovement SoapMovement;
    protected PlayerManager playerManager;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        anim = animator;
        SoapMovement = animator.GetComponent<SoapMovement>();
        playerManager = animator.GetComponent<PlayerManager>();
        SoapMovement.onAnimationEnter?.Invoke(this);

        if (!playerManager.photonView.IsMine)
        {
            return;
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        SoapMovement.onAnimationUpdate?.Invoke(this);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        SoapMovement.onAnimationExit?.Invoke(this);

    }
}