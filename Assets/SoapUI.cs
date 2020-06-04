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

    [SerializeField] private GameObject crosshairGameObject;
    [SerializeField] private TMP_Text numOfBulletText;
    [SerializeField] private TMP_Text gunNameText;
    [SerializeField] private Image gunIcon;

    #endregion

    #region Property



    #endregion

    #region Private Field

    private bool setup;
    
    private PlayerManager target;
    private GunManager gunManager;
    private SoapMovement soapMovement;

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

    private void OnDestroy()
    {
        if (setup)
        {
            soapMovement.onAnimationEnter -= EnableCrossHairOnDiveRoll;
            soapMovement.onAnimationExit -= DisableCrossHairOnDiveRollEnd;
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
        gunManager = target.GetComponent<GunManager>();
        soapMovement = target.GetComponent<SoapMovement>();
        soapMovement.onAnimationEnter += DisableCrossHairOnDiveRollEnd;
        soapMovement.onAnimationExit += EnableCrossHairOnDiveRoll;

        setup = true;

    }


    public void EnableCrossHair()
    {
        crosshairGameObject.SetActive(true);
    }

    public void DisableCrossHair()
    {
        crosshairGameObject.SetActive(false);
    }

    #endregion

    #region Private Methods

    #region Bridges

    private void DisableCrossHairOnDiveRollEnd(SmbSoap animationClass)
    {
        if (animationClass is SMB_DiveRoll_Soap)
        {
            print("Disable cross hair");
            DisableCrossHair();
        }
    }

    private void EnableCrossHairOnDiveRoll(SmbSoap animationClass)
    {
        if (animationClass is SMB_DiveRoll_Soap)
        {
            print("Enable cross hair");
            EnableCrossHair();
        }
    }

    #endregion

    #endregion

    
}