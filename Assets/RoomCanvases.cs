using System;
using System.Collections;
using System.Collections.Generic;
using Rooms;
using UnityEngine;

public class RoomCanvases : MonoBehaviour
{
    [SerializeField]
    private CreateOrJoinRoomCanvas _createOrJoinRoomCanvas;

    public CreateOrJoinRoomCanvas createOrJoinRoomCanvas => _createOrJoinRoomCanvas;

    [SerializeField]
    private CurrentRoomCanvas _currentRoomCanvas;

    public CurrentRoomCanvas currentRoomCanvas => _currentRoomCanvas;

    private void Awake()
    {
        FirstInitialize();
    }

    private void FirstInitialize()
    {
        _createOrJoinRoomCanvas.FirstInitialize(this);
        _currentRoomCanvas.FirstInitialize(this);
    }
}

