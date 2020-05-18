using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace BehaviorDesigners
{
    
    public class Punch : Action
    {
        public SharedFloat waitTime;
        private float waitTimeCounter;
        public override void OnStart()
        {
            GetComponent<Vampire>().Punch();
            waitTimeCounter = waitTime.Value;
        }

        public override TaskStatus OnUpdate()
        {
            if (waitTimeCounter > 0)
            {
                waitTimeCounter -= Time.deltaTime;
                if (waitTimeCounter <= 0)
                {
                    return TaskStatus.Success;
                }
            }

            return TaskStatus.Running;
        }
    }
}
