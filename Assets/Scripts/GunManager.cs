using System;
using Photon.Pun;
using UnityEngine;

public class GunManager: MonoBehaviourPun
{
    public GunPart[] gunParts;

    public event EventHandler<SwitchGunArgs> onGunSwitch;
    
    public GunPart currentGunPart => gunParts[currentGunPartIndex];
    
    public int currentGunPartIndex;

    private void Awake()
    {
        if (photonView.IsMine)
        {
            currentGunPartIndex = 0;
            UserInput.onLeftMouseButtonPressed += Shoot;
            UserInput.onMouseWheelScrollDown += MoveToNextGun;
            UserInput.onMouseWheelScrollUp += MoveToPrevGun;
            UserInput.onReloadPressed += Reload;
            onGunSwitch += GunSwitchAction;
        }
        
    }

    private void OnDestroy()
    {
        if (photonView.IsMine)
        {
            UserInput.onLeftMouseButtonPressed -= Shoot;
            UserInput.onMouseWheelScrollDown -= MoveToNextGun;
            UserInput.onMouseWheelScrollUp -= MoveToPrevGun;
            UserInput.onReloadPressed -= Reload;
            onGunSwitch -= GunSwitchAction;
        }
        
    }

    private void Update()
    {
        currentGunPart.holdingTrigger = UserInput.LeftMouseButtonPressing;
        currentGunPart.OnUpdate();
    }


    public void Shoot()
    {
            currentGunPart.Fire();
    }

    public void Reload()
    {
            print("reload current gun");
            GetComponent<SoapMovement>().SetTriggerAnimation("Reload");
            currentGunPart.Reload();
    }

    public void MoveToNextGun()
    {
        int oldGunIndex = currentGunPartIndex;
        if (currentGunPartIndex + 1 >= gunParts.Length)
        {
            print("1");
            currentGunPartIndex = 0;
        }
        else
        {
            print("2");
            currentGunPartIndex++;
        }
        onGunSwitch?.Invoke(this, new SwitchGunArgs{OldGun = gunParts[oldGunIndex], NewGun = gunParts[currentGunPartIndex]});

    }

    public void MoveToPrevGun()
    {
        int oldGunIndex = currentGunPartIndex;
        if (currentGunPartIndex == 0)
        {
            print("3");
            currentGunPartIndex = gunParts.Length - 1;
        }
        else
        {
            print("4");
            currentGunPartIndex--;
        }
        onGunSwitch?.Invoke(this, new SwitchGunArgs { OldGun = gunParts[oldGunIndex], NewGun = gunParts[currentGunPartIndex]});

    }

    #region Private methods

    private void GunSwitchAction(object sender, SwitchGunArgs switchGunArgs)
    {
        switchGunArgs.OldGun.holdingTrigger = false;
    }

    #endregion
}