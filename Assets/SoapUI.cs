using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoapUI : MonoBehaviour
{

    #region Serialized Field

    [SerializeField] private Image crosshair;
    [SerializeField] private TMP_Text numOfBulletText;

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