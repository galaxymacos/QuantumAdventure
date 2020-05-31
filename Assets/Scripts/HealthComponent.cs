using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class HealthComponent : MonoBehaviourPun, ITakeDamage, IHealable, IPunObservable
{
    [SerializeField] private float hpMax = 150;
    
    #region Properties

    public float healthPercentage => HpCurrent / HpMax;
    public float HpMax => hpMax;
    public float HpCurrent => hpCurrent;

    public UnityEvent onHealthReachZero;
    public event Action<float> onTakeDamage;
    public event EventHandler<HealthChangedChangeArgs> onHealthChange;
    
    #endregion

    #region Private Fields

    private float hpCurrent;

    #endregion

    private void Awake()
    {
        hpCurrent = hpMax;
    }

    public void TakeDamage(float damage)
    {
        hpCurrent -= damage;
        onTakeDamage?.Invoke(damage);
        onHealthChange?.Invoke(this, new HealthChangedChangeArgs{curHealth = hpCurrent, prevHealth = hpCurrent+damage, maxHealth = hpMax});
        print($"{gameObject.name} takes damage");
        if (hpCurrent <= 0)
        {
            print($"{gameObject.name} is dead");
            onHealthReachZero?.Invoke();
        }
    }

    public void Heal(HealArgs args)
    {
        hpCurrent += args.healAmount;
        onHealthChange?.Invoke(this, new HealthChangedChangeArgs{curHealth = hpCurrent, prevHealth = hpCurrent-args.healAmount, maxHealth = hpMax});
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(hpCurrent);
        }
        else
        {
            this.hpCurrent = (float) stream.ReceiveNext();
        }
    }

    public class HealthChangedChangeArgs: EventArgs
    {
        public float prevHealth;
        public float curHealth;
        public float maxHealth;
    }
}