using System;
using UnityEngine;

[Serializable]
public class SkillDataRaw
{
    public int damage;
    public int takeDownValue;
    
    public Vector2 launchForce;
    
    [Tooltip("The anger value increased in the receiver towards the owner of the damage source after it is hit")]
    public int angerValue;
}