using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentCanvases : MonoBehaviour
{
    #region Property

    public static PersistentCanvases instance;

    #endregion

    #region Private Field
    
    

    #endregion

    #region MonoBehavior Callback

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;        
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    #region Public Method

    #endregion

    #region Private Method

    #endregion
}
