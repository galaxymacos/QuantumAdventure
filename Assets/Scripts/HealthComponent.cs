using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviourPun, ITakeDamage, IHealable, IPunObservable
{
    [SerializeField] private float hpMax = 150;
    

    #region Properties

    public float healthPercentage => HpCurrent / HpMax;
    public float HpMax => hpMax;
    public float HpCurrent => hpCurrent;

    public UnityEvent onHealthReachZero;
    
    #endregion

    #region Private Fields

    private float hpCurrent;

    #endregion

    private void Awake()
    {
        hpCurrent = hpMax;
    }

    public void TakeDamage(DamageArgs args)
    {
        hpCurrent -= args.damageAmount;
        print($"{gameObject.name}'s health is {hpCurrent}");
        if (hpCurrent <= 0)
        {
            print($"{gameObject.name} is dead");
            onHealthReachZero?.Invoke();
        }
    }

    public void Heal(HealArgs args)
    {
        hpCurrent += args.healAmount;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(hpCurrent);
        }
        else
        {
            hpCurrent = (float) stream.ReceiveNext();
        }
    }
}