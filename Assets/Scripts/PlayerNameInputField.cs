using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_InputField))]
public class PlayerNameInputField : MonoBehaviourPun
{

    #region Serialized Field
    
    #endregion

    #region Property



    #endregion

    #region Private Field

    private const string PlayerNamePrefKey = "PlayerName";

    #endregion

    #region MonoBehavior Callback

    private void Start()
    {
        string defaultName = string.Empty;
        TMP_InputField inputField = GetComponent<TMP_InputField>();
        if (inputField != null)
        {
            if (PlayerPrefs.HasKey(PlayerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(PlayerNamePrefKey);
                inputField.text = defaultName;
            }
        }

        PhotonNetwork.NickName = defaultName;
    }

    #endregion

    #region Public Methods

    public void SetplayerName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("Player Name is null or empty");
            return;
        }

        PhotonNetwork.NickName = value;
        
        PlayerPrefs.SetString(PlayerNamePrefKey, value);
    }
    
    #endregion

    #region Private Methods



    #endregion

    
}
