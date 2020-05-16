using Photon.Pun;
using UnityEngine;

public class CharacterHealthComponent : MonoBehaviour, ITakeDamage, IHealable, IPunObservable
{
    public float healthPoint = 100;

    public void TakeDamage(DamageArgs args)
    {
        healthPoint -= args.damageAmount;
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