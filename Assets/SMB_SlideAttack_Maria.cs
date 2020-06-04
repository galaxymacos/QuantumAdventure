using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMB_SlideAttack_Maria : SmbMaria
{
    private float _horizontalInput;
    private float _verticalInput;
    private Vector3 _moveVector;
    private float _rotationAngle;
    [SerializeField] private AnimationCurve speedCurve;

    private float currentTime;
    private float animationLength;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        animationLength = stateInfo.length;
        currentTime = 0;

        _moveVector = CharacterMovement.CalculateMoveDirection(UserInput.HorizontalValue, UserInput.VerticalValue);
        _horizontalInput = UserInput.HorizontalValue;
        _verticalInput = UserInput.VerticalValue;
        _rotationAngle = CharacterMovement.InputToRotationAngle(_horizontalInput, _verticalInput);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        currentTime += Time.deltaTime;
        CharacterMovement.moveSpeed = CharacterMovement.walkSpeed*speedCurve.Evaluate(currentTime/animationLength);
        CharacterMovement.RotateCharacterImmediately(_rotationAngle);
        CharacterMovement.MoveAlong(_moveVector);
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
