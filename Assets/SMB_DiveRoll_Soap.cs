using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMB_DiveRoll_Soap : SMB_Soap
{
    private float _horizontalInput;
    private float _verticalInput;
    private Vector3 _moveVector;
    private float _rotationAngle;
    [SerializeField] private AnimationCurve diveRollSpeedCurve;
    private float currentTime;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        base.OnStateEnter(animator, stateInfo, layerIndex);


        _moveVector = SoapMovement.CalculateMoveDirection(UserInput.horizontalValue, UserInput.verticalValue);
        _horizontalInput = UserInput.horizontalValue;
        _verticalInput = UserInput.verticalValue;
        _rotationAngle = SoapMovement.InputToRotationAngle(_horizontalInput, _verticalInput);
        currentTime = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        currentTime += Time.deltaTime;
        
        
        SoapMovement.moveSpeed =
            SoapMovement.DiveRollSpeed * diveRollSpeedCurve.Evaluate(currentTime / stateInfo.length);
        Debug.Log(SoapMovement.moveSpeed);
        SoapMovement.RotateCharacterImmediately(_rotationAngle);
        SoapMovement.MoveAlong(_moveVector);
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
