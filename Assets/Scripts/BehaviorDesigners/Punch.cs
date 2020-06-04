using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace BehaviorDesigners
{
    
    public class Punch : Action
    {
        public SharedFloat WaitTime;
        private float _waitTimeCounter;
        
        public override void OnStart()
        {
            GetComponent<Vampire>().Punch();
            _waitTimeCounter = WaitTime.Value;
        }

        public override TaskStatus OnUpdate()
        {
            if (_waitTimeCounter > 0)
            {
                _waitTimeCounter -= Time.deltaTime;
                if (_waitTimeCounter <= 0)
                {
                    return TaskStatus.Success;
                }
            }

            return TaskStatus.Running;
        }
    }
}
