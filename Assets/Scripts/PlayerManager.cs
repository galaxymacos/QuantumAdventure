﻿using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PlayerManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
    #region Serialized Field

    [HideInInspector] public HealthComponent healthComponent;

    // [Tooltip("The Player's UI GameObject Prefab")] [SerializeField]
    // public GameObject otherPlayerUI;
    
    public GameObject dialogueUiPrefab;
    [SerializeField] public GameObject[] playerUiPrefabs;
    [SerializeField] public GameObject virtualCamera;
    [SerializeField] public CharacterPick characterName;

    #endregion

    #region Property

    public static GameObject LocalPlayerInstance;
    public bool isDialogueBoxOpen { get; set; }

    #endregion

    #region Private Field

    private GameObject _dialogueUi;

    #endregion

    #region MonoBehavior Callback

    private void Awake()
    {
        if ((string) photonView.Owner.CustomProperties["RandomNumber"] == "Maria")
        {
            GameManager.Instance.mariaViewId = photonView.ViewID;
        }
        else if ((string) photonView.Owner.CustomProperties["RandomNumber"] == "Soap")
        {
            GameManager.Instance.soapViewId = photonView.ViewID;
        }
        if (photonView.IsMine || !PhotonNetwork.IsConnected)
        {
            LocalPlayerInstance = gameObject;
            
            virtualCamera.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(gameObject);
        healthComponent = GetComponent<HealthComponent>();
    }

    private void Start()
    {
        InstantiateUi();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
    }

    


    void OnLevelWasLoaded(int level)
    {
        this.CalledOnLevelWasLoaded(level);
    }

    void CalledOnLevelWasLoaded(int sceneIndex)
    {
        if (sceneIndex == 0)
        {
            Destroy(gameObject);
        }
        if (!Physics.Raycast(transform.position, -Vector3.up, 5f))
        {
            transform.position = Vector3.zero;
        }

        transform.position = new Vector3(0f, 5f, 0f);

        InstantiateUi();
        
    }


    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    #endregion

    #region Public Methods

    public void LeaveRoom()
    {
        GameManager.Instance.LeaveRoom();
    }

    public void GameOver()
    {
        GetComponent<Animator>().SetTrigger("Dead");
    }

    #endregion

    #region Private Methods

    void OnSceneLoaded(Scene scene, LoadSceneMode loadingMode)
    {
        this.CalledOnLevelWasLoaded(scene.buildIndex);
    }


    private void InstantiateUi()
    {
        if (!PhotonNetwork.IsConnected)
        {
            print("Doesn't instantiate UI before player is not connected to photon");
            return;
        }

        if (photonView.IsMine)
        {
            print("Instantiate combat UI");
            foreach (GameObject playerUiPrefab in playerUiPrefabs)
            {
                var playerUi = Instantiate(playerUiPrefab);
                playerUi.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
            }
            _dialogueUi = Instantiate(dialogueUiPrefab);
            _dialogueUi.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        }
        else
        {
            print("Instantiate player UI");
            // GameObject _uiGo = Instantiate(otherPlayerUI); 
            GetComponent<HealthComponent>().healthBarCanvas.SetActive(true);
            // _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        }
        

        
    }


    [PunRPC]
    public void DisplayMessage(string message, string speakerName)
    {
        if (photonView.IsMine)
        {
            print("Receive RPC to display message");
            _dialogueUi.GetComponent<DialogueTest>().ShowMessage(message, speakerName);
        }
    }

    #endregion

    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == NetworkEventFirer.EventCodeDealDamage)
        {
            object[] data = (object[]) photonEvent.CustomData;
            int targetId = (int) data[0];
            int damageOwnerId = (int) data[1];
            int damage = (int) data[2];


            if (targetId == photonView.ViewID)
            {
                GetComponent<HealthComponent>().TakeDamage(damage);
            }
        }

        // 
        if (photonEvent.Code == NetworkEventFirer.EventCodeShowMessage)
        {
            object[] data = (object[]) photonEvent.CustomData;
            string speakerName = (string) data[0];
            string message = (string) data[1];
            string targetName = (string) data[2];
            if (targetName == characterName.ToString())
            {
                DisplayMessage(message, speakerName);
            }
            else
            {
            }
        }
    }
}