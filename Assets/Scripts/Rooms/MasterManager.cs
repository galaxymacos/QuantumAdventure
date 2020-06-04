using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Rooms
{
    [CreateAssetMenu(menuName = "ScriptableObject/MasterManager", fileName = "MasterManager")]
    public class MasterManager: SingletonScriptableObject<MasterManager>
    {
        [FormerlySerializedAs("_gameSettings")] [SerializeField] private GameSettings gameSettings;

        public static GameSettings GameSettings => Instance.gameSettings;

        [SerializeField]
        private List<NetworkedPrefab> networkPrefabs = new List<NetworkedPrefab>();
        
        public static GameObject NetworkInstantiate(GameObject obj, Vector3 position, Quaternion rotation)
        {
            foreach (var networkedPrefab in Instance.networkPrefabs)
            {
                if (networkedPrefab.prefab == obj)
                {
                    
                    if (networkedPrefab.path != string.Empty)
                    {
                        Debug.Log("Try to instantiate gameobject from "+networkedPrefab.path);
                        GameObject result = PhotonNetwork.Instantiate(networkedPrefab.path, position, rotation);
                        return result;
                    }
                    else
                    {
                        Debug.LogError("Path is empty for gameobject name "+networkedPrefab.prefab);
                        return null;
                    }
                }
            }

            Debug.LogError("Can't find network prefab " + obj);

            return null;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void PopulateNetworkedPrefabs()
        {

#if UNITY_EDITOR
            Instance.networkPrefabs = new List<NetworkedPrefab>();
            Instance.networkPrefabs.Clear();

            GameObject[] results = Resources.LoadAll<GameObject>("");
            for (int i = 0; i < results.Length; i++)
            {
                if (results[i].GetComponent<PhotonView>() != null)
                {
                    string path = AssetDatabase.GetAssetPath(results[i]);
                    Instance.networkPrefabs.Add(new NetworkedPrefab(results[i], path));
                }
            }
            
#endif
        }
    }
    
    
    
    
}