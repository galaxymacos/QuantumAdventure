using UnityEngine;

public class SmbBlockMaria : SmbMaria
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