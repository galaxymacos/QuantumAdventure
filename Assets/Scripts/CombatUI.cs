using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class CombatUi : MonoBehaviour
{

    #region Serialized Field

    [SerializeField] private TMP_Text playerHealthText;
    [SerializeField] private TMP_Text playerNicknameText;

    #endregion

    #region Property



    #endregion

    #region Private Field

    private PlayerManager _target;

    #endregion

    #region MonoBehavior Callback

    private void Awake()
    {
        transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
    }

    private void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        playerHealthText.text = _target.healthComponent.HpCurrent.ToString(CultureInfo.InvariantCulture);
        playerNicknameText.text = _target.photonView.Owner.NickName;


    }

    #endregion

    #region Public Methods

    public void SetTarget(PlayerManager target)
    {
        if (target == null)
        {
            Debug.LogError("Missing PlayerManager target for CombatUI.SetTarget.", this);
            return;
        }
        this._target = target;

        
        
    }

    #endregion

    #region Private Methods



    #endregion

    
}
