using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUi : MonoBehaviour
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

    private PlayerManager _target;

    private float _characterControllerHeight = 0f;
    private Transform _targetTransform;
    private Renderer _targetRenderer;
    private CanvasGroup _canvasGroup;
    private Vector3 _targetPosition;

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
            playerHealthSlider.value = _target.healthComponent.healthPercentage;
        }

        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void LateUpdate()
    {
        if (_targetRenderer != null)
        {
            _canvasGroup.alpha = _targetRenderer.isVisible ? 1f : 0f;
        }

        if (_targetTransform != null)
        {
            _targetPosition = _targetTransform.position;
            _targetPosition.y += _characterControllerHeight;
            transform.position = Camera.main.WorldToScreenPoint(_targetPosition) + screenOffset;
        }
    }

    #endregion

    #region Public Methods

    public void SetTarget(PlayerManager target)
    {
        if (target == null)
        {
            Debug.LogError("Missing PlayerManager target for PlayerUI.SetTarget.", this);
            return;
        }

        this._target = target;
        _targetTransform = this._target.GetComponent<Transform>();
        _targetRenderer = this._target.GetComponent<Renderer>();
        CharacterController characterController = target.GetComponent<CharacterController>();
        if (characterController != null)
        {
            _characterControllerHeight = characterController.height;
        }
        if (playerNameText != null)
        {
            playerNameText.text = this._target.photonView.Owner.NickName;
        }
    }

    #endregion

    #region Private Methods
    
    

    #endregion


}
