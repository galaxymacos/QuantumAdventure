using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

public class CollisionTrigger : MonoBehaviourPun
{
    private bool hasActivated;

    [Tooltip("When to activate the trigger")] [SerializeField]
    private ColliderActivationType activationType;

    public UnityEvent onTrigger;

    public bool soapIsIn;
    public bool mariaIsIn;


    private void OnTriggerEnter(Collider other)
    {
        if (hasActivated)
        {
            return;
        }

        if (!photonView.IsMine)
        {
            return;
        }

        if (activationType == ColliderActivationType.AnyThingIn)
        {
            
            print("trigger event because something steps in");
            onTrigger?.Invoke();
            photonView.RPC("SetInvalid", RpcTarget.All);
        }

        var playerPhotonView = other.GetComponent<PhotonView>();
        if (playerPhotonView != null)
        {
            if (playerPhotonView.ViewID == GameManager.Instance.MariaViewID)
            {
                mariaIsIn = true;
                if (activationType == ColliderActivationType.MariaIn ||
                    activationType == ColliderActivationType.AnyPlayerIn)
                {
                    photonView.RPC("SetInvalid", RpcTarget.All);
                    if (!PhotonNetwork.IsMasterClient)
                    {
                        photonView.RPC("TriggerEvent", RpcTarget.All);
                    }
                    else
                    {
                        TriggerEvent();
                    }
                }
            }
            else if (playerPhotonView.ViewID == GameManager.Instance.SoapViewID)
            {
                photonView.RPC("SoapIn", RpcTarget.All);
                if (activationType == ColliderActivationType.SoapIn ||
                    activationType == ColliderActivationType.AnyPlayerIn)
                {
                    print("trigger event because Soap steps in");
                    onTrigger?.Invoke();
                    photonView.RPC("SetInvalid", RpcTarget.All);

                }
            }

            if (mariaIsIn && soapIsIn && activationType == ColliderActivationType.BothPlayersIn)
            {
                print("trigger event because both players step in");
                onTrigger?.Invoke();
                photonView.RPC("SetInvalid", RpcTarget.All);
            }
        }

    }

    [PunRPC]
    public void SetInvalid()
    {
        hasActivated = true;
    }

    [PunRPC]
    public void MariaIn()
    {
        mariaIsIn = true;
    }

    [PunRPC]
    public void SoapIn()
    {
        soapIsIn = true;
    }

    [PunRPC]
    public void TriggerEvent()
    {
        onTrigger?.Invoke();
    }


    private enum ColliderActivationType
    {
        BothPlayersIn,
        AnyPlayerIn,
        AnyThingIn,
        MariaIn,
        SoapIn
    }
}