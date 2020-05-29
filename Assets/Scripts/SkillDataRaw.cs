using System;
using UnityEngine;

[Serializable]
public class SkillDataRaw
{
    public float damage;
    public int takeDownValue;
    
    [Tooltip("The anger value increased in the receiver towards the owner of the damage source after it is hit")]
    public int angerValue;
}