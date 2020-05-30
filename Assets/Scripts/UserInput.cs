﻿using System;
using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    public static float horizontalValue;
    public static float verticalValue;
    public static float cameraHorizontalMouseValue;
    public static float cameraVerticalMouseValue;
    public static bool Skill1Pressed;
    public static bool Skill2Pressed;
    public static bool runPressed;
    public static bool leftMouseButtonPressed;
    public static bool rightMouseButtonPressed;
    public static bool interactKeyPressed;
    public static bool returnButtonPressed;
    public static bool mouseWheelScrollUp;
    public static bool mouseWheelScrollDown;
    public static bool reloadPressed;
    public static bool diveRollPressed;

    public static event Action onSkill1Pressed;
    public static event Action onSkill2Pressed;
    public static event Action onLeftMouseButtonPressed;
    public static event Action onRightMouseButtonPressed;
    public static event Action onReturnButtonPressed;
    public static event Action onInteractButtonPressed;
    public static event Action onRunPressed;
    public static event Action onMouseWheelScrollUp;
    public static event Action onMouseWheelScrollDown;
    public static event Action onReloadPressed;
    public static event Action onDiveRollPressed;

    #region Serialized Field



    #endregion

    #region Property



    #endregion

    #region Private Field

    private static Player player;

    #endregion

    #region MonoBehavior Callback

    private void Start()
    {
        player = ReInput.players.GetPlayer(0);
    }

    private void Update()
    {
        horizontalValue = player.GetAxis("Move Horizontal");
        verticalValue = player.GetAxis("Move Vertical");
        cameraHorizontalMouseValue = player.GetAxis("Camera Rotate Horizontal");
        cameraVerticalMouseValue = player.GetAxis("Camera Rotate Vertical");
        Skill1Pressed = player.GetButton("Skill1");
        Skill2Pressed = player.GetButton("Skill2");
        interactKeyPressed = player.GetButton("Interact"); 
        leftMouseButtonPressed = player.GetButton("Left Mouse Button");
        rightMouseButtonPressed = player.GetButton("Right Mouse Button");
        mouseWheelScrollUp = player.GetButton("Wheel Scroll Up");
        mouseWheelScrollDown = player.GetButton("Wheel Scroll Down");
        runPressed = player.GetButton("Run");
        reloadPressed = player.GetButton("Reload");
        diveRollPressed = player.GetButton("DiveRoll");


        if (player.GetButtonDown("Skill1"))
        {
            onSkill1Pressed?.Invoke();
        }

        if (player.GetButtonDown("Skill2"))
        {
            onSkill2Pressed?.Invoke();
        }

        if (player.GetButtonDown("Left Mouse Button"))
        {
            onLeftMouseButtonPressed?.Invoke();
        }

        if (player.GetButtonDown("Right Mouse Button"))
        {
            onRightMouseButtonPressed?.Invoke();
        }
        
        if (player.GetButtonDown("Interact"))
        {
            onInteractButtonPressed?.Invoke();
        }

        if (player.GetButtonDown("Run"))
        {
            onRunPressed?.Invoke();
        }

        if (player.GetButtonDown("Confirm"))
        {
            onReturnButtonPressed?.Invoke();
        }

        if (player.GetButtonDown("Wheel Scroll Up"))
        {
            onMouseWheelScrollUp?.Invoke();
        }

        if (player.GetButtonDown("Wheel Scroll Down"))
        {
            onMouseWheelScrollDown?.Invoke();
        }

        if (player.GetButtonDown("Reload"))
        {
            print("reload");
            onReloadPressed?.Invoke();
        }

        if (player.GetButtonDown("DiveRoll"))
        {
            print("Player dives roll");
            onDiveRollPressed?.Invoke();
        }
        
    }

    #endregion

    #region Public Methods



    #endregion

    #region Private Methods



    #endregion
    

}
