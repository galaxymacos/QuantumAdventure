using UnityEngine;

public class SMB_Attack3_Maria : SmbMaria
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        if (!PhotonView.IsMine) return;
        animator.SetBool("Attack 3", false);
        UserInput.LeftMouseButtonPressState = UserInput.PressState.DownButCancel;

    }
   
}