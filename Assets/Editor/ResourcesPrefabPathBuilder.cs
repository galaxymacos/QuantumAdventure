#if UNITY_EDITOR
using Rooms;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Editor
{
    public class ResourcesPrefabPathBuilder : IPreprocessBuildWithReport
    {
        

        public int callbackOrder { get; }
        public void OnPreprocessBuild(BuildReport report)
        {
            MasterManager.PopulateNetworkedPrefabs();
        }
    }
}
#endif