using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Photon.Pun;
using Rooms;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviourPun
{
    public TextMeshPro textMesh;

    public static DamagePopup Create(Vector3 position, int damageAmount)
    {
        var damage = MasterManager.NetworkInstantiate(GameAssets.I.damagePopup, position, Quaternion.identity);
        DamagePopup damagePopup = damage.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount);
        return damagePopup;
    }

    public void Setup(int damageAmount)
    {
        textMesh.SetText(damageAmount.ToString());
    }

    private void Update()
    {
            Vector3 v = Camera.main.transform.position - transform.position;
            v.x = v.z = 0.0f;
            transform.LookAt( Camera.main.transform.position - v ); 
            transform.Rotate(0,180,0);
    }

    private void Start()
    {
        transform.DOMove(transform.position + new Vector3(0, 4, 0), 1f).OnComplete(()=>Destroy(gameObject));
    }
}