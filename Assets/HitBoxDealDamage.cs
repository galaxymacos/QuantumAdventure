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



    #endregion

    #region Property



    #endregion

    #region Private Field

    private PlayerManager owner;


    #endregion

    #region MonoBehavior Callback

    private void Awake()
    {
        if (GetComponent<PlayerManager>().photonView.IsMine || !PhotonNetwork.IsConnected)
        {
            foreach (var element in hitboxs.Values)
            {
                element.onHit += DealDamage;
            }
        }

        owner = GetComponent<PlayerManager>();

    }

    private void OnDestroy()
    {
        if (GetComponent<PlayerManager>().photonView.IsMine || !PhotonNetwork.IsConnected)
        {
            foreach (var element in hitboxs.Values)
            {
                element.onHit -= DealDamage;
            }
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
        if (owner.photonView.IsMine)
        {
            print($"{gameObject.name} try to deal damage to {e.hitCollider.gameObject.name}");
            var takeDamagePart = e.hitCollider.GetComponent<ITakeDamage>();
            if (takeDamagePart != null)
            {
                if (e.hitCollider.GetComponent<PlayerManager>() == null) return;
                NetworkEventFirer.DealDamage(skillDamage.GetSkillDamage(currentSkill), e.hitCollider.gameObject.GetComponent<PlayerManager>().playerName);
            }
        }



    }

    #endregion


}

