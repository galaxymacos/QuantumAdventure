using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Sirenix.OdinInspector;
using UnityEngine;

public class HitBoxDealDamage : SerializedMonoBehaviour
{
    public Dictionary<string, HitBoxPart> hitboxs = new Dictionary<string, HitBoxPart>();
    public string currentSkill;
    public SkillDamage skillDamage;


    #region Serialized Field

    [SerializeField] private bool controlByPlayer;

    #endregion

    #region Property

    #endregion

    #region Private Field


    #endregion

    #region MonoBehavior Callback

    private void Awake()
    {
        if (GetComponent<PlayerManager>() != null)
        {
            if (GetComponent<PlayerManager>().photonView.IsMine || !PhotonNetwork.IsConnected)
            {
                RegisterHitBox();
            }
        }
        else
        {
            RegisterHitBox();
        }


    }

    private void RegisterHitBox()
    {
        foreach (var element in hitboxs.Values)
        {
            element.onHit += DealDamage;
        }
    }

    private void OnDestroy()
    {
        foreach (var element in hitboxs.Values)
        {
            element.onHit -= DealDamage;
        }
    }

    #endregion

    #region Public Methods

    public void TurnOnHitBox(string hitBoxName)
    {
        if (!hitboxs.ContainsKey(hitBoxName))
        {
            Debug.LogError($"Can't find hit box with name {hitBoxName} under {gameObject.name}");
            return;
        }

        hitboxs[hitBoxName].ActiveHitbox();
    }

    public void TurnOffHitBox(string hitBoxName)
    {
        if (!hitboxs.ContainsKey(hitBoxName))
        {
            Debug.LogError($"Can't find hit box with name {hitBoxName} under {gameObject.name}");
        }

        hitboxs[hitBoxName].DeactivateHitbox();
    }

    #endregion

    #region Private Methods

    private void DealDamage(object sender, HitEventArgs e)
    {
        if (!controlByPlayer || GetComponent<PlayerManager>().photonView.IsMine)
        {
            print($"{gameObject.name} try to deal damage to {e.hitCollider.gameObject.name}");
            var takeDamagePart = e.hitCollider.GetComponent<ITakeDamage>();
            if (takeDamagePart != null)
            {
                if (e.hitCollider.GetComponent<PlayerManager>() == null) return;
                NetworkEventFirer.DealDamage(skillDamage.GetSkillDamage(currentSkill),
                    e.hitCollider.GetComponent<PhotonView>().ViewID);
            }
        }
    }

    #endregion
}