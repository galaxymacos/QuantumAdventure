using System;
using UnityEngine;

namespace Event_Args
{
    public class CharacterEventArgs : EventArgs
    {
        public CharacterMovement characterMovement;

        public CharacterEventArgs(CharacterMovement characterMovement)
        {
            this.characterMovement = characterMovement;
        }
    }
}
