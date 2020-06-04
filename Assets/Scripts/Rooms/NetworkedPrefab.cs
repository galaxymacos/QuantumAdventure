using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class NetworkedPrefab
{
    [FormerlySerializedAs("Prefab")] public GameObject prefab;
    [FormerlySerializedAs("Path")] public string path;

    public NetworkedPrefab(GameObject prefab, string path)
    {
        this.prefab = prefab;
        this.path = ReturnPrefabPathModified(path);
    }

    #region Private method

    private string ReturnPrefabPathModified(string path)
    {
        int extensionLength = System.IO.Path.GetExtension(path).Length;
        int additionalLength = "Resources/".Length;
        int startIndex = path.ToLower().IndexOf("resources", StringComparison.Ordinal);

        if (startIndex == -1)
        {
            return string.Empty;
        }
        else
        {
            return path.Substring(startIndex+additionalLength, path.Length - (additionalLength + startIndex + extensionLength));
        }
    }

    #endregion
}