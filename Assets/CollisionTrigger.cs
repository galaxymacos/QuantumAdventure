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
                photonView.RPC("MariaIn", RpcTarget.All);

                if (activationType == ColliderActivationType.MariaIn ||
                    activationType == ColliderActivationType.AnyPlayerIn)
                {
                    if (!PhotonNetwork.IsMasterClient)
                    {
                        photonView.RPC("TriggerEvent", RpcTarget.Others);
                    }
                    else
                    {
                        TriggerEvent();
                    }
                    photonView.RPC("SetInvalid", RpcTarget.All);
                }
            }
            else if (playerPhotonView.ViewID == GameManager.Instance.SoapViewID)
            {
                photonView.RPC("SoapIn", RpcTarget.All);
                print("Trigger SoapIn RPC");
                if (activationType == ColliderActivationType.SoapIn ||
                    activationType == ColliderActivationType.AnyPlayerIn)
                {
                    if (!PhotonNetwork.IsMasterClient)
                    {
                        photonView.RPC("TriggerEvent", RpcTarget.Others);
                    }
                    else
                    {
                        TriggerEvent();
                    }
                    print("trigger event because Soap steps in");
                    photonView.RPC("SetInvalid", RpcTarget.All);

                }

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
        print("MariaIn RPC Is Triggered");
        mariaIsIn = true;
        if (mariaIsIn && soapIsIn && activationType == ColliderActivationType.BothPlayersIn)
        {
                
            if (PhotonNetwork.IsMasterClient)
            {
                TriggerEvent();
            }
            photonView.RPC("SetInvalid", RpcTarget.All);
        }
    }

    [PunRPC]
    public void SoapIn()
    {
        print("SoapIn RPC Is Triggered");
        soapIsIn = true;
        if (mariaIsIn && soapIsIn && activationType == ColliderActivationType.BothPlayersIn)
        {
            TriggerEvent();
            photonView.RPC("SetInvalid", RpcTarget.All);
        }
    }

    [PunRPC]
    public void TriggerEvent()
    {
        print("TriggerEvent");
        if (PhotonNetwork.IsMasterClient)
        {
            print("TriggerEvent successfully");
            onTrigger?.Invoke();
        }
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