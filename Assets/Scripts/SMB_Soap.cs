using Photon.Pun;
using UnityEngine;
using UnityEngine.Animations;

public class SmbSoap: StateMachineBehaviour
{
    protected Animator Anim;
    protected SoapMovement SoapMovement;
    protected PlayerManager PlayerManager;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        Anim = animator;
        SoapMovement = animator.GetComponent<SoapMovement>();
        PlayerManager = animator.GetComponent<PlayerManager>();
        SoapMovement.onAnimationEnter?.Invoke(this);

        if (!PlayerManager.photonView.IsMine)
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