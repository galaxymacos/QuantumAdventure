using System;
using System.Runtime.ExceptionServices;
using Rooms;
using UnityEngine;
using UnityEngine.Serialization;

public class ChargedGun : GunPart
{
    public GameObject chargeBall;
    public float maxCharge;
    public float chargeIncreaseRate;
    public float chargeDecreaseRate;


    public float CurrentCharge => currentCharge;
    private float currentCharge = 0;

    public float FirstChargeThreshold => firstChargeThreshold;
    [SerializeField] private float firstChargeThreshold = 50;
    [SerializeField] private float secondChargeThreshold = 80;
    
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (holdingTrigger)
        {
            if (!chargeBall.activeInHierarchy)
            {
                chargeBall.SetActive(true);
            }
            
            
        }
        else
        {
            if (chargeBall.activeInHierarchy)
            {
                if (Math.Abs(currentCharge - maxCharge) < Mathf.Epsilon)
                {
                    print("Shoot max charge projectile");
                    currentCharge = 0;
                }
                else if (currentCharge >= secondChargeThreshold)
                {
                    print("Shoot second charge projectile");
                    currentCharge = 0;
                }
                else if (currentCharge >= firstChargeThreshold)
                {
                    print("Shoot first charge projectile");
                    currentCharge = 0;
                }

                currentCharge -= chargeDecreaseRate*Time.deltaTime;
                
            }
        }
    }

    public override void Fire()
    {
        if (bulletLeft == 0)
        {
            print("no bullet in catridge");
            return;
        }

        bulletLeft--;
        
        print("Bean gun shoot");
        if (Camera.main != null)
        {
            Ray ray = Camera.main.ViewportPointToRay (new Vector3(0.5f,0.5f));
            Vector3 bulletDirection = Vector3.zero;
            if (Physics.Raycast(ray,out RaycastHit hitinfo,400))
            {
                bulletDirection = (hitinfo.point - transform.position).normalized;
            }
            else
            {
                var point = ray.GetPoint(400);
                bulletDirection = (point - transform.position).normalized;

            }
            transform.SendMessage("Setup", new ProjectileArgs {flyDirection = bulletDirection, flySpeed = 50, firingRange = 400, damage = 10, owner = gameObject}, SendMessageOptions.RequireReceiver);
        }

    }
}

public class ChargedBall: MonoBehaviour
{
    [SerializeField] private float maxSize;
    [SerializeField] private float minSize;

    private ChargedGun _chargedGun;  
    
    private void FirstInitialize(ChargedGun chargedGun)
    {
        _chargedGun = chargedGun;
    }

    public void Setup(ProjectileArgs args)
    {
        
    }
    
    public void Fire()
    {
        
    }

    private void Update()
    {
        SetSize();
    }

    public void SetSize()
    {
        float percentage = (_chargedGun.CurrentCharge - _chargedGun.FirstChargeThreshold) /
                           (_chargedGun.maxCharge - _chargedGun.FirstChargeThreshold);
        float targetSize = (maxSize - minSize) * percentage+minSize;
        transform.localScale = new Vector3(targetSize,targetSize,targetSize);
    }
}
