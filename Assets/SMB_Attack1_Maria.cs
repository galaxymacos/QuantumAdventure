﻿using System.Collections;
using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine;

public class SMB_Attack1_Maria : SmbMaria
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        if (!PhotonView.IsMine) return;
        UserInput.LeftMouseButtonPressState = UserInput.PressState.DownButCancel;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        if (!PhotonView.IsMine) return;
        
        if (UserInput.LeftMouseButtonPressState == UserInput.PressState.Down)
        {
            UserInput.LeftMouseButtonPressState = UserInput.PressState.DownButCancel;
            CharacterMovement.SetTriggerAnimation("Attack 2");
        }
    }

   
}