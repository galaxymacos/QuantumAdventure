using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI: MonoBehaviour
{
        #region Serialized Field

    [Tooltip("UI Slider to display Enemy's Health")]
    [SerializeField] private Image healthBar;

    [Tooltip("Pixel offset from the player target")]
    [SerializeField] private Vector3 screenOffset = new Vector3(0f, 30f, 0f);

    #endregion

    #region Property



    #endregion

    #region Private Field

    private Enemy target;

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
        if (target == null)
        {
            print("destroy enemy UI");
            Destroy(gameObject);
            return;
        }
        
        if (healthBar != null)
        {
            healthBar.fillAmount = target.GetComponent<HealthComponent>().healthPercentage;
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

    public void SetTarget(Enemy _target)
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
    }

    #endregion

    #region Private Methods



    #endregion

}