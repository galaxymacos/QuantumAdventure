using System.Linq;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace BehaviorDesigners.Actions
{
    public class SetUpWaypoints : Action
    {
        // The transform that the object is moving towards
        public SharedGameObjectList Target;

        public override TaskStatus OnUpdate()
        {
            var wayPointsInScene = GameObject.FindGameObjectsWithTag("Waypoint");
            if (wayPointsInScene.Length > 0)
            {
                Target.Value = wayPointsInScene.ToList();
                return TaskStatus.Success;
            }
            else
            {
                Debug.LogError("Waypoint has not been set up");
            }

            return TaskStatus.Running;
        }
    }
}