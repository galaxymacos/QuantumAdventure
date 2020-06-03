using UnityEngine;

[CreateAssetMenu(menuName = "Data/BulletData")]
public class BulletData : ScriptableObject
{
    public string bulletName;
    public int damage;
    public int takeDownValue;
    public int angerValue;
}