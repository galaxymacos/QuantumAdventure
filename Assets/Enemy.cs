using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

/// <summary>
///  This script is used to implement common behavior of all the enemies
/// </summary>
public class Enemy : MonoBehaviourPun, IOnEventCallback
{
    // public GameObject worldHealthBarPrefab;
    

    #region Event


    #endregion
    
    #region Private fields

    

    #endregion
    
    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);

    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);

    }

    private void Awake()
    {
        var canvas = GameObject.Find("Canvas");
        if (canvas == null)
        {
            Debug.LogError("Can't find canvas in world");
            return;
        }    

        // var worldHealthBar = Instantiate(worldHealthBarPrefab, transform.position, Quaternion.identity);
        // worldHealthBar.SendMessage("SetTarget", this);

        
        
    }
    

    public void OnEvent(EventData photonEvent)
    {
        Debug.LogWarning("Enemy takes damage");
        if (photonEvent.Code == NetworkEventFirer.EventCodeDealDamage)
        {
            object[] data = (object[]) photonEvent.CustomData;
            int targetID = (int) data[0];
            int damageOwnerID = (int) data[1];
            int damage = (int) data[2];
            int angerValue = (int) data[3];
            if (targetID == photonView.ViewID)
            {
                GetComponent<HealthComponent>().TakeDamage(damage);
                GetComponent<TakedownComponent>().DecreaseTakeDownGauge(damage);
                GetComponent<EnemyAnger>().IncreaseAngerTowards(PhotonNetwork.GetPhotonView(damageOwnerID).GetComponent<PlayerManager>(), angerValue);
            }
            else
            {
                print($"Don't deal damage to {targetID == photonView.ViewID}");
            }
        }
    }

    
}