using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


public class BuffBall : MonoBehaviourPun, IPunOwnershipCallbacks {
    #region Property

    #endregion

    #region Private Field

    [SerializeField] private bool transferConfirmed = false;

    #endregion

    #region MonoBehavior Callback

    private void Awake()
    {
        UserInput.onLeftMouseButtonPressed += Use;
        UserInput.onRightMouseButtonPressed += TransferOwnership;
        
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDestroy()
    {
        UserInput.onLeftMouseButtonPressed -= Use;
        UserInput.onRightMouseButtonPressed -= TransferOwnership;
        
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    private void Update()
    {
        Player owner = photonView.Owner;
    }

    #endregion

    #region Public Method

    public void Use()
    {
        if (photonView.IsMine)
        {
            print("Buff ball used");
        }
    }

    public void TransferOwnership()
    {
        photonView.RequestOwnership();
    }

    #endregion

    #region Private Method

    #endregion

    #region Callback

    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        if (photonView != targetView)
        {
            return;
        }

        if (!transferConfirmed)
        {
            transferConfirmed = true;
            return;
        }
        // Should the current owner of this gameobject transfer its ownership to the requesting player?
        
        photonView.TransferOwnership(requestingPlayer);
    }

    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        if (photonView != targetView)
        {
            return;
        }

        transferConfirmed = false;
    }

    #endregion
}
