using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarCanvas : MonoBehaviour
{
    public HealthComponent healthComponent;
    public Image greenBar;
    public Image yellowBar;
    public Image redBar;
    
    
    // Start is called before the first frame update
    void Start()
    {
        healthComponent.onHealthChange += UpdateUi;
    }

    private void OnDestroy()
    {
        healthComponent.onHealthChange -= UpdateUi;
    }

    private void UpdateUi(object sender, HealthComponent.HealthChangedChangeArgs healthChangedChangeArgs)
    {
        greenBar.fillAmount = healthChangedChangeArgs.curHealth / healthComponent.HpMax;
        yellowBar.DOFillAmount(healthChangedChangeArgs.curHealth / healthComponent.HpMax, 0.6f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = Camera.main.transform.position - transform.position;
        v.x = v.z = 0.0f;
        transform.LookAt( Camera.main.transform.position - v ); 
        transform.Rotate(0,180,0);
    }
    
        
}
