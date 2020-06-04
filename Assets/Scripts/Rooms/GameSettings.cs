using UnityEngine;
using UnityEngine.Serialization;

namespace Rooms
{
    [CreateAssetMenu(menuName = "ScriptableObject/GameSettings", fileName = "GameSettings")]
    public class GameSettings: ScriptableObject
    {
        [FormerlySerializedAs("GameVersion")] public string gameVersion;
    }
}