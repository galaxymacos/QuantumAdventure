using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
    #region Serialized Field

    [HideInInspector] public HealthComponent healthComponent;

    [Tooltip("The Player's UI GameObject Prefab")] [SerializeField]
    public GameObject otherPlayerUI;

    [SerializeField] public GameObject hostUI;
    [SerializeField] public GameObject dialogueUIPrefab;
    [SerializeField] public GameObject[] playerUiPrefabs;
    [SerializeField] public GameObject virtualCamera;

    #endregion

    #region Property

    public static GameObject LocalPlayerInstance;
    public bool isDialogueBoxOpen { get; set; }

    #endregion

    #region Private Field

    private RoleTag roleTag;

    private GameObject dialogueUI;

    #endregion

    #region MonoBehavior Callback

    private void Awake()
    {
        if (photonView.IsMine || !PhotonNetwork.IsConnected)
        {
            LocalPlayerInstance = gameObject;
            virtualCamera.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        roleTag = GetComponent<RoleTag>();
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;

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
        dialogueUI = Instantiate(dialogueUIPrefab);
        dialogueUI.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);

        
    }


    [PunRPC]
    public void DisplayMessage(string message, string speakerName)
    {
        if (photonView.IsMine)
        {
            print("Receive RPC to display message");
            dialogueUI.GetComponent<DialogueTest>().ShowMessage(message, speakerName);
        }
    }

    #endregion

    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == 1)
        {
            object[] data = (object[]) photonEvent.CustomData;
            float damage = (float) data[0];
            string takeDamageObjectName = (string) data[1];
            if (takeDamageObjectName == roleTag.RoleName)
            {
                print($"Deal damage to {roleTag.RoleName}");
                GetComponent<HealthComponent>().TakeDamage(damage);
            }
            else
            {
                print($"Don't deal damage to {roleTag.RoleName}");
            }
        }
    }
}