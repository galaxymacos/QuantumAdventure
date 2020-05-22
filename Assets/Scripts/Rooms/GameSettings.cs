using UnityEngine;

namespace Rooms
{
    [CreateAssetMenu(menuName = "ScriptableObject/GameSettings", fileName = "GameSettings")]
    public class GameSettings: ScriptableObject
    {
        public string GameVersion;
    }
}