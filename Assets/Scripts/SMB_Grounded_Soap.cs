using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMB_Grounded_Soap : SMB_Soap
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        if (!playerManager.photonView.IsMine || playerManager.isDialogueBoxOpen)
        {
            return;
        }
        characterMovement.Move(UserInput.horizontalValue, UserInput.verticalValue);

    }
}