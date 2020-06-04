using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HealthComponent))]
public class HealthBarCanvas : MonoBehaviour
{
    private HealthComponent _healthComponent;
    public Image greenBar;
    public Image yellowBar;
    public Image redBar;
    private TweenerCore<float, float, FloatOptions> _tweener;


    private void Awake()
    {
        _healthComponent = GetComponentInParent<HealthComponent>();
        if (_healthComponent == null)
        {
            Debug.LogError("Can't find health component under game object: " + gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _healthComponent.onHealthChange += UpdateUi;
    }

    private void OnDestroy()
    {
        _healthComponent.onHealthChange -= UpdateUi;
    }

    private void UpdateUi(object sender, HealthComponent.HealthChangedChangeArgs healthChangedChangeArgs)
    {
        greenBar.fillAmount = healthChangedChangeArgs.CurHealth / _healthComponent.HpMax;
        _tweener?.Kill();
        _tweener = yellowBar.DOFillAmount(healthChangedChangeArgs.CurHealth / _healthComponent.HpMax, 0.6f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main != null)
        {
            var cameraPos = Camera.main.transform.position;
            Vector3 v = cameraPos - transform.position;
            v.x = v.z = 0.0f;
            transform.LookAt( cameraPos - v );
        }

        transform.Rotate(0,180,0);
    }
    
        
}
