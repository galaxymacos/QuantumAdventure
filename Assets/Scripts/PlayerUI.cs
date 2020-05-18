using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{

    #region Serialized Field

    [Tooltip("UI Text to display Player's Name")]
    [SerializeField] private TMP_Text playerNameText;

    [Tooltip("UI Slider to displayer Player's Health")]
    [SerializeField] private Slider playerHealthSlider;

    [Tooltip("Pixel offset from the player target")]
    [SerializeField] private Vector3 screenOffset = new Vector3(0f, 30f, 0f);

    #endregion

    #region Property



    #endregion

    #region Private Field

    private PlayerManager target;

    private float characterControllerHeight = 0f;
    private Transform targetTransform;
    private Renderer targetRenderer;
    private CanvasGroup _canvasGroup;
    private Vector3 targetPosition;

    #endregion

    #region MonoBehavior Callback

    private void Awake()
    {
        transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
        
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (playerHealthSlider != null)
        {
            playerHealthSlider.value = target.healthComponent.HealthPoint;
        }

        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void LateUpdate()
    {
        if (targetRenderer != null)
        {
            _canvasGroup.alpha = targetRenderer.isVisible ? 1f : 0f;
        }

        if (targetTransform != null)
        {
            targetPosition = targetTransform.position;
            targetPosition.y += characterControllerHeight;
            transform.position = Camera.main.WorldToScreenPoint(targetPosition) + screenOffset;
        }
    }

    #endregion

    #region Public Methods

    public void SetTarget(PlayerManager _target)
    {
        if (_target == null)
        {
            Debug.LogError("Missing PlayerManager target for PlayerUI.SetTarget.", this);
            return;
        }

        target = _target;
        targetTransform = target.GetComponent<Transform>();
        targetRenderer = target.GetComponent<Renderer>();
        CharacterController characterController = _target.GetComponent<CharacterController>();
        if (characterController != null)
        {
            characterControllerHeight = characterController.height;
        }
        if (playerNameText != null)
        {
            playerNameText.text = target.photonView.Owner.NickName;
        }
    }

    #endregion

    #region Private Methods



    #endregion


}
