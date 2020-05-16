using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet : MonoBehaviour
{
    #region Serialized Field



    #endregion

    #region Property



    #endregion

    #region Private Field



    #endregion

    #region MonoBehavior Callback

    private void Update()
    {
        transform.Translate(Vector3.forward * 5* Time.deltaTime);
    }

    #endregion

    #region Public Methods



    #endregion

    #region Private Methods



    #endregion


}
