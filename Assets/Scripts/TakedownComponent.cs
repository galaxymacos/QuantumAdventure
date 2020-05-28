using System;
using Photon.Pun;
using UnityEngine;

public class TakedownComponent: MonoBehaviourPun
{
    [SerializeField]
    private int takeDownGaugeMax;

    [SerializeField]
    private int takeDownGauge;

    #region Property

    public int TakeDownGauge => takeDownGauge;

    #endregion

    private void Awake()
    {
        takeDownGauge = takeDownGaugeMax;
    }

    public void RecoverTakeDownGaugeToFull()
    {
        takeDownGauge = takeDownGaugeMax;
    }


    public void DecreaseTakeDownGauge(int value)
    {
        takeDownGauge = Mathf.Clamp(takeDownGauge-value, 0, takeDownGaugeMax);
    }
}