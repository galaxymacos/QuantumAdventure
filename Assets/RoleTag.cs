using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class RoleTag : MonoBehaviour
{

    #region Serialized Field

    [SerializeField] private bool randomTag;
    [SerializeField] private string roleName;

    #endregion

    #region Property

    public string RoleName => roleName;

    #endregion

    #region Private Field



    #endregion

    #region MonoBehavior Callback

    private void Awake()
    {
        print(gameObject.name +" has the view id: "+GetComponent<PhotonView>().ViewID);
    }

    #endregion

    #region Public Methods



    #endregion

    #region Private Methods



    #endregion


}
