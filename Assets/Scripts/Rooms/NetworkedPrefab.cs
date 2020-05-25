using System;
using UnityEngine;

[Serializable]
public class NetworkedPrefab
{
    public GameObject Prefab;
    public string Path;

    public NetworkedPrefab(GameObject prefab, string path)
    {
        Prefab = prefab;
        Path = ReturnPrefabPathModified(path);
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