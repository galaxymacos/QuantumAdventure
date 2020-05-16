using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MariaAnimationMessager : MonoBehaviour
{

    #region Serialized Field

    [SerializeField] private Maria maria;
    
    #endregion

    #region Property



    #endregion

    #region Private Field



    #endregion

    #region MonoBehavior Callback



    #endregion

    #region Public Methods

    public void AnimationEvent_ToggleRightLegHitBox(int state)
    {
        if (state == 0)
        {
            maria.rightLeg.DeactivateHitbox();
        }
        else if (state == 1)
        {
            maria.rightLeg.ActiveHitbox();
        }
    }
    
    public void AnimationEvent_ToggleSwordHitBox(int state)
    {
        if (state == 0)
        {
            maria.sword.DeactivateHitbox();
        }
        else if (state == 1)
        {
            maria.sword.ActiveHitbox();
        }
    }
    
    public void AnimationEvent_ToggleTest(bool isOn)
    {
    }
    
    
    
    

    #endregion

    #region Private Methods



    #endregion

    
}
