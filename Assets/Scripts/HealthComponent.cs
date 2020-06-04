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
    public float HpCurrent => _hpCurrent;

    public GameObject healthBarCanvas;

    public UnityEvent onHealthReachZero;
    public event Action<float> onTakeDamage;
    public event EventHandler<HealthChangedChangeArgs> onHealthChange;
    
    #endregion

    #region Private Fields

    private float _hpCurrent;

    #endregion

    private void Awake()
    {
        _hpCurrent = hpMax;
    }

    public void TakeDamage(float damage)
    {
        _hpCurrent -= damage;
        onTakeDamage?.Invoke(damage);
        onHealthChange?.Invoke(this, new HealthChangedChangeArgs{CurHealth = _hpCurrent, PrevHealth = _hpCurrent+damage, MaxHealth = hpMax});
        print($"{gameObject.name} takes damage");
        if (_hpCurrent <= 0)
        {
            print($"{gameObject.name} is dead");
            onHealthReachZero?.Invoke();
        }
    }

    public void Heal(HealArgs args)
    {
        _hpCurrent += args.HealAmount;
        onHealthChange?.Invoke(this, new HealthChangedChangeArgs{CurHealth = _hpCurrent, PrevHealth = _hpCurrent-args.HealAmount, MaxHealth = hpMax});
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_hpCurrent);
        }
        else
        {
            this._hpCurrent = (float) stream.ReceiveNext();
        }
    }

    public class HealthChangedChangeArgs: EventArgs
    {
        public float PrevHealth;
        public float CurHealth;
        public float MaxHealth;
    }
}