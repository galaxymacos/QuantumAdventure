using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SMB_Run_Maria : SmbMaria
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        CharacterMovement.moveSpeed = CharacterMovement.runSpeed;
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

        if (!UserInput.RunPressing || (Math.Abs(Mathf.Abs(UserInput.VerticalValue)) < Mathf.Epsilon && Math.Abs(Mathf.Abs(UserInput.HorizontalValue)) < Mathf.Epsilon))
        {
            CharacterMovement.SetAnimationBool("Run", false);
        }
        
        if (UserInput.Skill1Pressing)
        {
            CharacterMovement.SetTriggerAnimation("Kick");
        }

        if (UserInput.Skill2Pressing)
        {
            CharacterMovement.SetTriggerAnimation("Slash");
        }

        if (UserInput.Skill1Pressing)
        {
            CharacterMovement.SetTriggerAnimation("Great Sword Slide");
        }
    }
    
}
