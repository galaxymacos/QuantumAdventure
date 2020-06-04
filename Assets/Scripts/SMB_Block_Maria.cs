using UnityEngine;

public class SMB_Block_Maria : SmbMaria
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (!UserInput.RightMouseButtonPressing)
        {
            CharacterMovement.SetAnimationBool("Block",false);
        }
    }
}