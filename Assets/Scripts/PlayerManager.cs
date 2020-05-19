using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviourPunCallbacks
{

    #region Serialized Field

    [HideInInspector]
    public HealthComponent healthComponent;

    [Tooltip("The Player's UI GameObject Prefab")]
    [SerializeField] public GameObject otherPlayerUI;
    [SerializeField] public GameObject hostUI;

    [SerializeField] public GameObject[] playerUiPrefabs;
    
    
    [SerializeField] public GameObject virtualCamera;
    
    

    #endregion

    #region Property

    public static GameObject LocalPlayerInstance;
    public bool isDialogueBoxOpen { get; set; }

    #endregion

    #region Private Field

    private GameObject dialogueUI;

    #endregion

    #region MonoBehavior Callback

    private void Awake()
    {
        if (photonView.IsMine || !PhotonNetwork.IsConnected)
        {
            LocalPlayerInstance = gameObject;
            virtualCamera.SetActive(true);
        }
        
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
        
        healthComponent = GetComponent<HealthComponent>();
    }

    private void Start()
    {
        InstantiateUi();
    }

    

    

    void OnLevelWasLoaded(int level)
    {
        this.CalledOnLevelWasLoaded(level);
    }

    void CalledOnLevelWasLoaded(int level)
    {
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
        if (photonView.IsMine)
        {
            print("Instantiate combat UI");
            GameObject _uiGo = Instantiate(hostUI);
            _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
            foreach (GameObject playerUiPrefab in playerUiPrefabs)
            {
                var playerUi = Instantiate(playerUiPrefab);
                playerUi.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
            }
        }
        else
        {
            print("Instantiate player UI");
            GameObject _uiGo = Instantiate(otherPlayerUI);
            _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        }

        
    }
    
    
    [PunRPC]
    private void DisplayMessage(string message)
    {
        dialogueUI.GetComponent<DialogueTest>().ShowMessage(message);
    }
    
    #endregion

}
