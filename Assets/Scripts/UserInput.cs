using System;
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

    public static event Action onSkill1Pressed;
    public static event Action onSkill2Pressed;
    public static event Action onLeftMouseButtonPressed;
    public static event Action onRightMouseButtonPressed;
    public static event Action onReturnButtonPressed;
    public static event Action onInteractButtonPressed;
    public static event Action onRunPressed;

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
        runPressed = player.GetButton("Run");


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
            print("Left Mouse Button Pressed");
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

    }

    #endregion

    #region Public Methods



    #endregion

    #region Private Methods



    #endregion
    

}
