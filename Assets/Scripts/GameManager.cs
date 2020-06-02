using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
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
    public int MariaViewID;
    public int SoapViewID;
    
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
        
        
        NetworkEventFirer.RegisterCustomType();
    }

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
                    newPlayer = MasterManager.NetworkInstantiate(MariaPrefab, new Vector3(2f, 5f, 2f),
                        Quaternion.identity);
                    players.Add(newPlayer.GetComponent<PlayerManager>());
                }
                else if((string) PhotonNetwork.LocalPlayer.CustomProperties["RandomNumber"] == "Soap"){
                    newPlayer = MasterManager.NetworkInstantiate(SoapPrefab, new Vector3(-2f, 5f, -2f),
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