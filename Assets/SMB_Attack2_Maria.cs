using UnityEngine;

public class SMB_Attack2_Maria : SMB_Maria
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        if (!photonView.IsMine) return;
        
        UserInput.leftMouseButtonPressing = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        if (!photonView.IsMine) return;
        
        if (UserInput.leftMouseButtonPressing)
        {
            characterMovement.SetTriggerAnimation("Attack 3");
        }
    }

   
}