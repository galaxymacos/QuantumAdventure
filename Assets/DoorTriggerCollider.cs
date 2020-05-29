using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class DoorTriggerCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print($"DoorTriggerCollider collides with "+other.gameObject);
        CutScenePlayer.instance.Play("BreakDoor");
    }
}

