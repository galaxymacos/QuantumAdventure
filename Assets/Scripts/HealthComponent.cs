using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviourPun, ITakeDamage, IHealable, IPunObservable
{
    [SerializeField] private float healthPoint = 150;
    

    #region Properties

    public float HealthPoint => healthPoint;
    public UnityEvent onHealthReachZero;

    #endregion
    
    public void TakeDamage(DamageArgs args)
    {
        healthPoint -= args.damageAmount;
        if (healthPoint <= 0)
        {
            print($"{gameObject.name} is dead");
            onHealthReachZero?.Invoke();
            // PhotonNetwork.LeaveRoom();
        }
    }

    public void Heal(HealArgs args)
    {
        healthPoint += args.healAmount;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(healthPoint);
        }
        else
        {
            healthPoint = (float) stream.ReceiveNext();
        }
    }
}