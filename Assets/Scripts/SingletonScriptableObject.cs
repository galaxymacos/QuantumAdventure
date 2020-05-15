using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
{

    #region Serialized Field



    #endregion

    #region Property

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                T[] results = Resources.FindObjectsOfTypeAll<T>();
                if (results.Length == 0)
                {
                    Debug.LogError("SingletonScriptableObject -> Instance -> results length is 0 for type "+typeof(T) + ".");
                    return null;
                }

                if (results.Length > 1)
                {
                    Debug.LogError("SingletonScriptableObject -> Instance -> results length is greater than 1 for type "+typeof(T) + ".");
                    return null;
                }

                _instance = results[0];
            }

            return _instance;
        }
    }

    #endregion

    #region Private Field

    private static T _instance = null;

    #endregion

    #region MonoBehavior Callback



    #endregion

    #region Public Methods



    #endregion

    #region Private Methods



    #endregion


}
