using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class WarningCanvas : MonoBehaviourPunCallbacks
{
    #region Property

    public static WarningCanvas instance;

    #endregion

    #region Private Field

    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private GameObject warningPanel;

    #endregion

    #region MonoBehavior Callback

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    #endregion

    #region Public Method


    public void ShowWarningText(string text)
    {
        warningPanel.SetActive(true);
        warningText.text = text;
        StopAllCoroutines();
        StartCoroutine(HideWarningTextFieid());

    }

    #endregion

    #region Private Method

    private IEnumerator HideWarningTextFieid()
    {
        yield return new WaitForSeconds(3);
        warningPanel.SetActive(false);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        ShowWarningText($"Player {otherPlayer.NickName} left room");
    }

    #endregion
}
