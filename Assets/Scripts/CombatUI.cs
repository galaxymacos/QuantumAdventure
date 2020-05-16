using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class CombatUI : MonoBehaviour
{

    #region Serialized Field

    [SerializeField] private TMP_Text playerHealthText;
    [SerializeField] private TMP_Text playerNicknameText;

    #endregion

    #region Property



    #endregion

    #region Private Field

    private PlayerManager target;

    #endregion

    #region MonoBehavior Callback

    private void Awake()
    {
        transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        playerHealthText.text = target.Health.ToString(CultureInfo.InvariantCulture);
        playerNicknameText.text = target.photonView.Owner.NickName;


    }

    #endregion

    #region Public Methods

    public void SetTarget(PlayerManager _target)
    {
        if (_target == null)
        {
            Debug.LogError("Missing PlayerManager target for CombatUI.SetTarget.", this);
            return;
        }
        target = _target;

        
        
    }

    #endregion

    #region Private Methods



    #endregion

    
}
