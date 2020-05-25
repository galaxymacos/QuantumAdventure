using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SMB_Grounded_Maria : SMB_Maria
{
    private static readonly int HeavyPunch = Animator.StringToHash("Heavy Punch");
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        characterMovement.moveSpeed = characterMovement.walkSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (!playerManager.photonView.IsMine && PhotonNetwork.IsConnected)
        {
            return;
        }

        if (playerManager.isDialogueBoxOpen)
        {
            return;
        }
        characterMovement.Move(UserInput.horizontalValue, UserInput.verticalValue);
        if (UserInput.Skill1Pressed)
        {
            characterMovement.SetTriggerAnimation("Kick");
        }

        if (UserInput.Skill2Pressed)
        {
            characterMovement.SetTriggerAnimation("Slash");
        }

        if (UserInput.runPressed && UserInput.verticalValue>0)
        {
            characterMovement.SetAnimationBool("Run", true);
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}