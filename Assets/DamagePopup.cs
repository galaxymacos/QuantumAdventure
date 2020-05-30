using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    public TextMeshPro textMesh;

    public static DamagePopup Create(Vector3 position, int damageAmount)
    {
        var damage = Instantiate(GameAssets.I.damagePopup, position, Quaternion.identity);
        DamagePopup damagePopup = damage.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount);
        return damagePopup;
    }

    public void Setup(int damageAmount)
    {
        textMesh.SetText(damageAmount.ToString());
    }

    private void Start()
    {
        transform.DOMove(transform.position + new Vector3(0, 4, 0), 1f).OnComplete(()=>Destroy(gameObject));
    }
}