using UnityEditor;
using UnityEngine;

namespace Rooms
{
    [CreateAssetMenu(menuName = "ScriptableObject/MasterManager", fileName = "MasterManager")]
    public class MasterManager: ScriptableObject
    {
        public GameSettings gameSettings;
    }
}