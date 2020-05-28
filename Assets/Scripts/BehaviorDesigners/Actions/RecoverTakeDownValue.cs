using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace BehaviorDesigners.Actions
{
    public class RecoverTakeDownValue : Action
    {
        #region Property

        #endregion


        #region Private Field

        #endregion

        #region MonoBehavior Callback

        public override TaskStatus OnUpdate()
        {
            var takeDownComponent = gameObject.GetComponent<TakedownComponent>();
            if (takeDownComponent != null)
            {
                takeDownComponent.RecoverTakeDownGaugeToFull();
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }
    

        #endregion

        #region Public Method

        #endregion

        #region Private Method

        #endregion
    }
}
