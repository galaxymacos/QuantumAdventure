using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Launcher : MonoBehaviourPunCallbacks
{

    #region Serialized Field

    [SerializeField] private GameObject controlPanel;
    [SerializeField] private GameObject progressLabel;

    #endregion

    #region Property



    #endregion

    #region Private Field

    private string _gameVersion = "0.1";
    private bool _isConnecting;

    #endregion

    #region MonoBehavior Callback

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
    }

    #endregion

    #region Public Methods

    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            _isConnecting = PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = _gameVersion;
        }
        
        progressLabel.SetActive(true);
        controlPanel.SetActive(false);
    }
    
    
    #endregion

    #region Private Methods

    

    #endregion

    #region Pun Callbacks

    public override void OnConnectedToMaster()
    {
        print("Connected to master");
        if (_isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
            _isConnecting = false;
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Disconnect from master because "+cause);
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print("can't find any room, going to create one");
        PhotonNetwork.CreateRoom(string.Empty, new RoomOptions {MaxPlayers = 2});
    }

    public override void OnCreatedRoom()
    {
        print("created room");
    }

    public override void OnJoinedRoom()
    {
        print("join room");
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            PhotonNetwork.LoadLevel("Room for 1");
        }
    }

    #endregion
    
}
