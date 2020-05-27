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
    [SerializeField] private TMP_Text gunNameText;
    [SerializeField] private Image gunIcon;

    #endregion

    #region Property



    #endregion

    #region Private Field

    private PlayerManager target;
    private GunManager gunManager;

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

        numOfBulletText.text = $"{gunManager.currentGunPart.bulletLeft.ToString()} / {gunManager.currentGunPart.catridge.ToString()}   {gunManager.currentGunPart.bulletTotal}";
        gunNameText.text = gunManager.currentGunPart.gunName;
        gunIcon.sprite = gunManager.currentGunPart.icon;
        
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
        gunManager = target.GetComponent<GunManager>();
        


    }

    #endregion

    #region Private Methods



    #endregion

    
}