using UnityEngine;

[CreateAssetMenu(menuName = "Data/BulletData")]
public class BulletData : ScriptableObject
{
    public string bulletName;
    public float damage;
    public int takeDownValue;
    public int angerValue;
}