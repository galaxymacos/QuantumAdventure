using System;
using UnityEngine;

namespace Event_Args
{
    public class CharacterEventArgs : EventArgs
    {
        public SoapMovement SoapMovement;

        public CharacterEventArgs(SoapMovement soapMovement)
        {
            this.SoapMovement = soapMovement;
        }
    }
}
