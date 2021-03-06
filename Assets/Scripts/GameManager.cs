﻿using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Rooms;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviourPunCallbacks
{

    #region Serialized Field

    [FormerlySerializedAs("MariaPrefab")] [Tooltip("The prefab to use for representing the player")]
    public GameObject mariaPrefab;
    [FormerlySerializedAs("SoapPrefab")] public GameObject soapPrefab;
    public List<PlayerManager> players;
    
    

    [SerializeField] public LayerMask whatIsGround;

    #endregion

    #region Property

    public static GameManager Instance;
    [FormerlySerializedAs("MariaViewID")] public int mariaViewId;
    [FormerlySerializedAs("SoapViewID")] public int soapViewId;
    
    #endregion

    #region Private Field



    #endregion

    #region MonoBehavior Callback

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Destroy game manager because there are more than one GameManager");
            Destroy(gameObject);
        }
        
        
    }

    private void Start()
    {
        if (mariaPrefab == null)
        {
            Debug.LogError("player prefab reference. Please set it up in GameObject 'Game Manager'");
        }
        else
        {
            if (PlayerManager.LocalPlayerInstance == null)
            {
                print("Instantiate"+(string)PhotonNetwork.LocalPlayer.CustomProperties["RandomNumber"]);
                GameObject newPlayer;
                if ((string) PhotonNetwork.LocalPlayer.CustomProperties["RandomNumber"] == "Maria")
                {
                    newPlayer = MasterManager.NetworkInstantiate(mariaPrefab, new Vector3(2f, 5f, 2f),
                        Quaternion.identity);
                    players.Add(newPlayer.GetComponent<PlayerManager>());
                }
                else if((string) PhotonNetwork.LocalPlayer.CustomProperties["RandomNumber"] == "Soap"){
                    newPlayer = MasterManager.NetworkInstantiate(soapPrefab, new Vector3(-2f, 5f, -2f),
                        Quaternion.identity);
                    players.Add(newPlayer.GetComponent<PlayerManager>());
                }
            }
            
            
        }

    }

    #endregion

    #region Public Methods

    

    #endregion

    #region Private Methods

    private void LoadArena()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("Trying to load a level but we are not the master client");
        }
        PhotonNetwork.LoadLevel($"Room for 2");
    }

    #endregion

    #region Photon Callbacks

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            LoadArena();
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        print($"Player {PhotonNetwork.LocalPlayer} leaves room because {otherPlayer.NickName} has left room");
        PhotonNetwork.LoadLevel(0);
        PhotonNetwork.LeaveRoom();
    }

    // public override void OnLeftRoom()
    // {
        // photonView.RPC("LeaveRoom", RpcTarget.All);
        
    // }

    #endregion

    #region RPC
    
    // [PunRPC]
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    #endregion
}