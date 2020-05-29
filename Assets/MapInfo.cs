using System.Collections.Generic;
using UnityEngine;

public class MapInfo : MonoBehaviour
{
    public static MapInfo instance;
    public List<DungeonInfo> dungeonsInfo;

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

    public DungeonInfo GetDungeonInfo(string dungeonName)
    {
        var targetDungeonInfo = dungeonsInfo.Find(x=>x.name == dungeonName);
        if (targetDungeonInfo == null)
        {
            Debug.LogError($"Can't find the dungeon with the name {dungeonName}");
        }
        return targetDungeonInfo;
    }

}