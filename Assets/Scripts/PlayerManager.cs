using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
{

    #region Serialized Field

    [Tooltip("The current health of our player")]
    [SerializeField] public float Health = 100f;

    [Tooltip("The Player's UI GameObject Prefab")]
    [SerializeField] public GameObject PlayerUiPrefab;
    [SerializeField] public GameObject PlayerCombatUIPrefab;
    
    [SerializeField] public GameObject virtualCamera;

    #endregion

    #region Property

    public static GameObject LocalPlayerInstance;

    #endregion

    #region Private Field



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
    }

    private void Start()
    {

        if (photonView.IsMine)
        {
            print("Instantiate combat UI");
            GameObject _uiGo = Instantiate(PlayerCombatUIPrefab);
            _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        }
        else
        {
            print("Instantiate player UI");
            GameObject _uiGo = Instantiate(PlayerUiPrefab);
            _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        }
    }

    private void Update()
    {

        if (Health <= 0f)
        {
            GameManager.Instance.LeaveRoom();
        }
        
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

        if (photonView.IsMine)
        {
            print("Instantiate combat UI");
            GameObject _uiGo = Instantiate(PlayerCombatUIPrefab);
            _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        }
        else
        {
            print("Instantiate player UI");
            GameObject _uiGo = Instantiate(PlayerUiPrefab);
            _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        }

        
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    #endregion

    #region Public Methods



    #endregion

    #region Private Methods

    void OnSceneLoaded(Scene scene, LoadSceneMode loadingMode)
    {
        this.CalledOnLevelWasLoaded(scene.buildIndex);
    }

    #endregion


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(Health);
        }
        else
        {
            Health = (float) stream.ReceiveNext();
        }
    }
}
