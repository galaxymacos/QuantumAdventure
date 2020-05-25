using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEditor;
using UnityEngine;

namespace Rooms
{
    [CreateAssetMenu(menuName = "ScriptableObject/MasterManager", fileName = "MasterManager")]
    public class MasterManager: SingletonScriptableObject<MasterManager>
    {
        [SerializeField] private GameSettings _gameSettings;

        public static GameSettings GameSettings => Instance._gameSettings;

        [SerializeField]
        private List<NetworkedPrefab> networkPrefabs = new List<NetworkedPrefab>();
        
        public static GameObject NetworkInstantiate(GameObject obj, Vector3 position, Quaternion rotation)
        {
            foreach (var networkedPrefab in Instance.networkPrefabs)
            {
                if (networkedPrefab.Prefab == obj)
                {
                    
                    if (networkedPrefab.Path != string.Empty)
                    {
                        Debug.Log("Try to instantiate gameobject from "+networkedPrefab.Path);
                        GameObject result = PhotonNetwork.Instantiate(networkedPrefab.Path, position, rotation);
                        return result;
                    }
                    else
                    {
                        Debug.LogError("Path is empty for gameobject name "+networkedPrefab.Prefab);
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