using System;
using UnityEngine;

public class GunManager: MonoBehaviour
{
    public GunPart[] gunParts;

    public EventHandler<SwitchGunArgs> onGunSwitch;
    
    public GunPart currentGunPart => gunParts[currentGunPartIndex];
    
    public int currentGunPartIndex;

    private void Awake()
    {
        currentGunPartIndex = 0;
        UserInput.onLeftMouseButtonPressed += Shoot;
    }

    private void OnDestroy()
    {
        UserInput.onLeftMouseButtonPressed -= Shoot;
    }


    public void Shoot()
    {
        currentGunPart.Shoot();
    }

    public void MoveToNextGun()
    {
        int oldGunIndex = currentGunPartIndex;
        if (currentGunPartIndex + 1 >= gunParts.Length)
        {
            currentGunPartIndex = 0;
        }
        else
        {
            currentGunPartIndex++;
        }
        onGunSwitch?.Invoke(this, new SwitchGunArgs{oldGun = gunParts[oldGunIndex], newGun = gunParts[currentGunPartIndex]});

    }

    public void MoveToPrevGun()
    {
        int oldGunIndex = currentGunPartIndex;
        if (currentGunPartIndex == 0)
        {
            currentGunPartIndex = gunParts.Length - 1;
        }
        else
        {
            currentGunPartIndex--;
        }
        onGunSwitch?.Invoke(this, new SwitchGunArgs { oldGun = gunParts[oldGunIndex], newGun = gunParts[currentGunPartIndex]});

    }
}