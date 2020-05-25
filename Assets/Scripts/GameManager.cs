﻿using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using Rooms;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{

    #region Serialized Field

    [Tooltip("The prefab to use for representing the player")]
    public GameObject MariaPrefab;
    public GameObject SoapPrefab;
    public List<PlayerManager> players;
    
    

    [SerializeField] public LayerMask whatIsGround;

    #endregion

    #region Property

    public static GameManager Instance;
    
    #endregion

    #region Private Field



    #endregion

    #region MonoBehavior Callback

    private void Start()
    {
        if (MariaPrefab == null)
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
                    newPlayer = MasterManager.NetworkInstantiate(MariaPrefab, new Vector3(0f, 5f, 0f),
                        Quaternion.identity);
                    // newPlayer = PhotonNetwork.Instantiate(MariaPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity);
                    players.Add(newPlayer.GetComponent<PlayerManager>());
                }
                else if((string) PhotonNetwork.LocalPlayer.CustomProperties["RandomNumber"] == "Soap"){
                    newPlayer = MasterManager.NetworkInstantiate(SoapPrefab, new Vector3(0f, 5f, 0f),
                        Quaternion.identity);
                    // newPlayer = PhotonNetwork.Instantiate(SoapPrefab.name, new Vector3(-2f, 5f, 0f), Quaternion.identity, 0);
                        // players.Add(newPlayer.GetComponent<PlayerManager>());
                }
            }
            
            
        }

        Instance = this;
    }

    #endregion

    #region Public Methods

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    #endregion

    #region Private Methods

    private void LoadArena()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("Trying to load a level but we are not the master client");
        }
        Debug.Log($"Loading level {PhotonNetwork.CurrentRoom.PlayerCount}");
        PhotonNetwork.LoadLevel($"Room for {PhotonNetwork.CurrentRoom.PlayerCount}");
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
        if (PhotonNetwork.IsMasterClient)
        {
            LoadArena();
        }
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    #endregion
}