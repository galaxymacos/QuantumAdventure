using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{

    #region Serialized Field

    [Tooltip("The prefab to use for representing the player")]
    public GameObject p1CharacterPrefab;
    public GameObject p2CharacterPrefab;

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
        if (p1CharacterPrefab == null)
        {
            Debug.LogError("player prefab reference. Please set it up in GameObject 'Game Manager'");
        }
        else
        {
            if (PlayerManager.LocalPlayerInstance == null)
            {
                switch (PhotonNetwork.CurrentRoom.PlayerCount)
                {
                    case 1:
                        PhotonNetwork.Instantiate(p1CharacterPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity);
                        break;
                    case 2:
                        PhotonNetwork.Instantiate(p2CharacterPrefab.name, new Vector3(-2f, 5f, 0f), Quaternion.identity, 0);
                        break;

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
