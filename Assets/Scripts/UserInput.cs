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
    public static bool leftMouseButtonPressed;
    public static bool rightMouseButtonPressed;
    public static bool interactKeyPressed;

    public static event Action skill1JustPressed;
    public static event Action skill2JustPressed;
    public static event Action LeftMouseButtonJustPressed;
    public static event Action RightMouseButtonJustPressed;
    public static event Action interactButtonJustPressed;

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


        if (player.GetButtonDown("Skill1"))
        {
            skill1JustPressed?.Invoke();
        }

        if (player.GetButtonDown("Skill2"))
        {
            skill2JustPressed?.Invoke();
        }

        if (player.GetButtonDown("Left Mouse Button"))
        {
            print("Left Mouse Button Pressed");
            LeftMouseButtonJustPressed?.Invoke();
        }

        if (player.GetButtonDown("Right Mouse Button"))
        {
            RightMouseButtonJustPressed?.Invoke();
        }
        
        if (player.GetButtonDown("Interact"))
        {
            interactButtonJustPressed?.Invoke();
        }
    }

    #endregion

    #region Public Methods



    #endregion

    #region Private Methods



    #endregion
    

}
