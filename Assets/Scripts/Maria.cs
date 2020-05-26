using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Rooms;
using UnityEngine;

public class Maria : MonoBehaviourPun
{

    #region Serialized Field

    public GameObject buffBall;

    #endregion

    #region Property


    #endregion

    #region Private Field


    #endregion

    #region MonoBehavior Callback

    private void Start()
    {
        if (photonView.IsMine || !PhotonNetwork.IsConnected)
        {
            MasterManager.NetworkInstantiate(buffBall, transform.position, Quaternion.identity);
            
        }
    }

    #endregion

    #region Public Methods



    #endregion

    #region Private Methods



    #endregion


}