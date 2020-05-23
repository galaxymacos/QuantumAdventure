using System;
using System.Collections;
using System.Collections.Generic;
using Rooms;
using UnityEngine;

public class RoomCanvases : MonoBehaviour
{
    #region Private field

    // Reference of child canvas
    [SerializeField] private CreateOrJoinRoomCanvas _createOrJoinRoomCanvas;
    [SerializeField] private CurrentRoomCanvas _currentRoomCanvas;

    #endregion

    #region Property

    public CreateOrJoinRoomCanvas createOrJoinRoomCanvas => _createOrJoinRoomCanvas;
    public CurrentRoomCanvas currentRoomCanvas => _currentRoomCanvas;

    #endregion


    

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