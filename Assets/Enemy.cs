﻿using System;
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
    public GameObject worldHealthBarPrefab;

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

        var worldHealthBar = Instantiate(worldHealthBarPrefab, transform.position, Quaternion.identity);
        worldHealthBar.SendMessage("SetTarget", this);
    }
    

    public void OnEvent(EventData photonEvent)
    {
        Debug.LogWarning("Enemy takes damage");
        if (photonEvent.Code == NetworkEventFirer.EventCode_DealDamage)
        {
            object[] data = (object[]) photonEvent.CustomData;
            float damage = (float) data[0];
            string takeDamageObjectName = (string) data[1];
            if (takeDamageObjectName == GetComponent<RoleTag>().RoleName)
            {
                GetComponent<HealthComponent>().TakeDamage(damage);
            }
            else
            {
                print($"Don't deal damage to {GetComponent<RoleTag>().RoleName}");
            }
        }
    }
}