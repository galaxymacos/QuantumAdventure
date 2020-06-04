using System;
using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    public static float HorizontalValue;
    public static float VerticalValue;
    public static float CameraHorizontalMouseValue;
    public static float CameraVerticalMouseValue;
    public static bool Skill1Pressing;
    public static bool Skill2Pressing;
    public static bool RunPressing;
    public static bool LeftMouseButtonPressing;
    public static bool RightMouseButtonPressing;
    public static bool InteractKeyPressing;
    public static bool ReturnButtonPressed;
    public static bool MouseWheelScrollUp;
    public static bool MouseWheelScrollDown;
    public static bool ReloadPressed;
    public static bool DiveRollPressed;

    public static PressState LeftMouseButtonPressState;

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

    private static Player _player;

    #endregion

    #region MonoBehavior Callback

    private void Start()
    {
        _player = ReInput.players.GetPlayer(0);
    }

    private void Update()
    {
        HorizontalValue = _player.GetAxis("Move Horizontal");
        VerticalValue = _player.GetAxis("Move Vertical");
        CameraHorizontalMouseValue = _player.GetAxis("Camera Rotate Horizontal");
        CameraVerticalMouseValue = _player.GetAxis("Camera Rotate Vertical");
        Skill1Pressing = _player.GetButton("Skill1");
        Skill2Pressing = _player.GetButton("Skill2");
        InteractKeyPressing = _player.GetButton("Interact"); 
        LeftMouseButtonPressing = _player.GetButton("Left Mouse Button");
        RightMouseButtonPressing = _player.GetButton("Right Mouse Button");
        MouseWheelScrollUp = _player.GetButton("Wheel Scroll Up");
        MouseWheelScrollDown = _player.GetButton("Wheel Scroll Down");
        RunPressing = _player.GetButton("Run");
        ReloadPressed = _player.GetButton("Reload");
        DiveRollPressed = _player.GetButton("DiveRoll");


        if (_player.GetButtonDown("Skill1"))
        {
            onSkill1Pressed?.Invoke();
        }

        if (_player.GetButtonDown("Skill2"))
        {
            onSkill2Pressed?.Invoke();
        }

        if (_player.GetButtonDown("Left Mouse Button"))
        {
            onLeftMouseButtonPressed?.Invoke();
            LeftMouseButtonPressState = PressState.Down;
        }
        else if (_player.GetButtonUp("Left Mouse Button"))
        {
            LeftMouseButtonPressState = PressState.Up;
        }

        if (_player.GetButtonDown("Right Mouse Button"))
        {
            onRightMouseButtonPressed?.Invoke();
        }
        
        if (_player.GetButtonDown("Interact"))
        {
            onInteractButtonPressed?.Invoke();
        }

        if (_player.GetButtonDown("Run"))
        {
            onRunPressed?.Invoke();
        }

        if (_player.GetButtonDown("Confirm"))
        {
            onReturnButtonPressed?.Invoke();
        }

        if (_player.GetButtonDown("Wheel Scroll Up"))
        {
            onMouseWheelScrollUp?.Invoke();
        }

        if (_player.GetButtonDown("Wheel Scroll Down"))
        {
            onMouseWheelScrollDown?.Invoke();
        }

        if (_player.GetButtonDown("Reload"))
        {
            print("reload");
            onReloadPressed?.Invoke();
        }

        if (_player.GetButtonDown("DiveRoll"))
        {
            print("Player dives roll");
            onDiveRollPressed?.Invoke();
        }
        
    }
    

    #endregion


    public enum PressState
    {
        Up, DownButCancel, Down
    }
}
