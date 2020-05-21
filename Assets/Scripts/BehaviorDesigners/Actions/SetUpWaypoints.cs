﻿using System.Linq;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace BehaviorDesigners.Actions
{
    public class SetUpWaypoints : Action
    {
        // The transform that the object is moving towards
        public SharedGameObjectList target;

        public override TaskStatus OnUpdate()
        {
            var wayPointsInScene = GameObject.FindGameObjectsWithTag("Waypoint");
            if (wayPointsInScene.Length > 0)
            {
                target.Value = wayPointsInScene.ToList();
                return TaskStatus.Success;
            }

            return TaskStatus.Running;
        }
    }
}