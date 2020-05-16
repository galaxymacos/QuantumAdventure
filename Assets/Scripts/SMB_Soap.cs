﻿using UnityEngine;

public class SMB_Soap: StateMachineBehaviour
{
    protected Animator anim;
    protected CharacterMovement characterMovement;
    protected PlayerManager playerManager;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        anim = animator;
        characterMovement = animator.GetComponent<CharacterMovement>();
        playerManager = animator.GetComponent<PlayerManager>();
    }
}