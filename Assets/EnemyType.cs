using UnityEngine;

[CreateAssetMenu(menuName = "data/EnemyType")]
public class EnemyType : ScriptableObject
{
    public string enemyType;
    public GameObject enemyAsset;
}