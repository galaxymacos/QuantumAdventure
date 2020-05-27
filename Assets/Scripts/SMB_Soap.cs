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
        
        if (!playerManager.photonView.IsMine)
        {
            return;
        }
    }
}