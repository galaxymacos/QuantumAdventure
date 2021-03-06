﻿using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SMB_Grounded_Soap : SmbSoap
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        if (!PlayerManager.photonView.IsMine && PhotonNetwork.IsConnected)
        {
            return;
        }

        SoapMovement.moveSpeed = SoapMovement.walkSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        if (PlayerManager.isDialogueBoxOpen)
        {
            return;
        }
        if (!PlayerManager.photonView.IsMine && PhotonNetwork.IsConnected)
        {
            return;
        }
        SoapMovement.Move(UserInput.HorizontalValue, UserInput.VerticalValue);

        SoapMovement.RotateCharacter();

        if (UserInput.DiveRollPressed)
        {
            SoapMovement.SetTriggerAnimation("DiveRoll");
        }

    }
}