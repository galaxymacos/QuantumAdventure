using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMB_Grounded_ThinMouse : SMB_ThinMouse
{
    private static readonly int Kick = Animator.StringToHash("Kick");
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        characterMovement.Move(UserInput.horizontalValue, UserInput.verticalValue);
        if (UserInput.Skill1Pressed)
        {
            anim.SetTrigger(Kick);
        }

    }
}

public class SMB_ThinMouse: StateMachineBehaviour
{
    protected Animator anim;
    protected CharacterMovement characterMovement;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        anim = animator;
        characterMovement = animator.GetComponent<CharacterMovement>();
    }
}