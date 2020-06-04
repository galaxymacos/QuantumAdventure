using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace BehaviorDesigners.Conditions
{
    public class ShouldTakeDown : Conditional
    {
        #region Property

        #endregion

        #region Private Field

        private TakedownComponent _takeDownComponent;

        #endregion

        #region MonoBehavior Callback

        public override void OnAwake()
        {
            _takeDownComponent = gameObject.GetComponent<TakedownComponent>();
        }

        public override TaskStatus OnUpdate()
        {
            if (_takeDownComponent.TakeDownGauge <= 0)
            {
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
