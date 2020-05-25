﻿using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SMB_Run_Maria : SMB_Maria
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        characterMovement.moveSpeed = characterMovement.runSpeed;
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
        
        characterMovement.Move(0, UserInput.verticalValue);
        
        if (!UserInput.runPressed || UserInput.verticalValue<=0)
        {
            characterMovement.SetAnimationBool("Run", false);
        }
        
        if (UserInput.Skill1Pressed)
        {
            characterMovement.SetTriggerAnimation("Kick");
        }

        if (UserInput.Skill2Pressed)
        {
            characterMovement.SetTriggerAnimation("Slash");
        }
    }
    
}
