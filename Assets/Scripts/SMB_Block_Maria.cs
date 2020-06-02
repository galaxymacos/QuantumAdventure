using UnityEngine;

public class SMB_Block_Maria : SMB_Maria
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (!UserInput.rightMouseButtonPressing)
        {
            characterMovement.SetAnimationBool("Block",false);
        }
    }
}