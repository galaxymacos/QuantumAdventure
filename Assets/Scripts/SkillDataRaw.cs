using System;
using UnityEngine;

[Serializable]
public class SkillDataRaw
{
    public int damage;
    public int takeDownValue;
    
    public float launchVerticalForce;
    public float launchHorizontalForce;
    
    [Tooltip("The anger value increased in the receiver towards the owner of the damage source after it is hit")]
    public int angerValue;
}