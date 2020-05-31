using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

public class Vampire : MonoBehaviour
{
    #region Serialized Field


    #endregion

    #region Property


    #endregion

    #region Private Field

    private NavMeshAgent agent;
    private Animator anim;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int PUNCH_ANIM_NAME = Animator.StringToHash("Punch");
    

    #endregion

    #region MonoBehavior Callback

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

    }
    
    private void Update()
    {
        SetAnimationValue();
    }

    #endregion

    #region Public Methods

    #endregion

    #region Private Methods

    private void SetAnimationValue()
    {
        anim.SetFloat("Speed", agent.velocity.magnitude / agent.speed);
    }

    public void Punch()
    {
        anim.SetTrigger(PUNCH_ANIM_NAME);
    }
    
    public void Destroy()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

    #endregion
}