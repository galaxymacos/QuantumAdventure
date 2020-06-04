using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SMB_Grounded_Maria : SmbMaria
{
    private static readonly int HeavyPunch = Animator.StringToHash("Heavy Punch");
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        CharacterMovement.moveSpeed = CharacterMovement.walkSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (!PlayerManager.photonView.IsMine && PhotonNetwork.IsConnected)
        {
            return;
        }

        if (PlayerManager.isDialogueBoxOpen)
        {
            return;
        }
        CharacterMovement.Move(UserInput.HorizontalValue, UserInput.VerticalValue);
        CharacterMovement.RotateCharacter(UserInput.HorizontalValue, UserInput.VerticalValue);
        if (UserInput.Skill1Pressing)
        {
            CharacterMovement.SetTriggerAnimation("Kick");
        }

        if (UserInput.Skill2Pressing)
        {
            CharacterMovement.SetTriggerAnimation("Slash");
        }

        if (UserInput.RightMouseButtonPressing)
        {
            CharacterMovement.SetAnimationBool("Block",true);
        }

        if (UserInput.LeftMouseButtonPressing)
        {
            CharacterMovement.SetTriggerAnimation("Attack 1");
        }

        if (UserInput.RunPressing)
        {
            if (Mathf.Abs(UserInput.HorizontalValue) > 0 || Mathf.Abs(UserInput.VerticalValue) > 0)
            {
                CharacterMovement.SetAnimationBool("Run", true);
            }
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