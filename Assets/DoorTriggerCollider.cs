using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class DoorTriggerCollider : MonoBehaviour
{
    private bool hasActivated;
    private void OnTriggerEnter(Collider other)
    {
        if (hasActivated)
        {
            return;
        }
        hasActivated = true;
        print($"DoorTriggerCollider collides with "+other.gameObject);
        StorylineEvent.instance.Story_MariaBreakRoom();
    }
}