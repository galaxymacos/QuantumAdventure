using System;
using UnityEngine;
using UnityEngine.Events;

namespace Component
{
    public class CollisionEvent : MonoBehaviour
    {
        public CollisionType collisionType;

        
        #region Serialized Field



        #endregion

        #region Property



        #endregion

        #region Private Field



        #endregion

        #region MonoBehavior Callback

        private void OnTriggerEnter(Collider other)
        {
            if (collisionType == CollisionType.Trigger)
            {
                
            }
        }

        #endregion

        #region Public Methods



        #endregion

        #region Private Methods



        #endregion


    }
    
    public enum CollisionType
    {
        Trigger,
        NonTrigger
    }
}
