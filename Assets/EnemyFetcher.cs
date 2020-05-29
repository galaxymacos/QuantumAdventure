using System.Collections.Generic;
using UnityEngine;

public class EnemyFetcher : MonoBehaviour
{
    public static EnemyFetcher instance;
    
    public List<EnemyType> enemyTypes;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject Fetch(string enemyTypeName)
    {
        EnemyType target = enemyTypes.Find(x => x.enemyType == enemyTypeName);
        if (target == null)
        {
            Debug.LogError("Can't find enemy with the type "+enemyTypeName);
        }
        return target.enemyAsset;
    }
}