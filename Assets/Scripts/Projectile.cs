using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Projectile : MonoBehaviourPun, IPunObservable
{
    #region Serialized Field

    #endregion

    #region Property

    public bool isMine;

    #endregion

    #region Private Field

    private float flySpeed;
    private Vector3 flyDirection;
    private float firingRange;
    private bool hasSetup;
    private float damage;

    private Rigidbody rb;
    private GameObject owner;

    private Vector3 originalLocation;

    #endregion

    #region MonoBehavior Callback

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if ((transform.position - originalLocation).magnitude > firingRange)
        {
            if (isMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    #endregion

    #region Public Methods

    public void Setup(ProjectileArgs args)
    {
        flySpeed = args.flySpeed;
        flyDirection = args.flyDirection;
        damage = args.damage;
        firingRange = args.firingRange;
        owner = args.owner;
        isMine = true;

        hasSetup = true;

        rb.velocity = flyDirection * flySpeed;

        originalLocation = transform.position;
    }

    #endregion

    #region Private Methods

    private void OnTriggerEnter(Collider other)
    {
        if (!isMine) return;
        if (other.transform.root.gameObject == owner || other.CompareTag("Bullet")) return;
        if (owner.GetComponent<PlayerManager>().photonView.IsMine)
        {
            var takeDamagePart = other.GetComponent<ITakeDamage>();
            if (takeDamagePart != null)
            {
                
                NetworkEventFirer.DealDamage(damage, other.gameObject.GetComponent<RoleTag>().RoleName);
            }
        }
        PhotonNetwork.Destroy(gameObject);
    }

    #endregion

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(rb.position);
            stream.SendNext(rb.rotation);
            stream.SendNext(rb.velocity);
        }
        else
        {
            rb.position = (Vector3) stream.ReceiveNext();
            rb.rotation = (Quaternion) stream.ReceiveNext();
            rb.velocity =(Vector3) stream.ReceiveNext();
            
            float lag = Mathf.Abs((float) (PhotonNetwork.Time - info.SentServerTime));
            rb.position += rb.velocity * lag;
        }
    }
}

public class ProjectileArgs
{
    public GameObject owner;
    public Vector3 flyDirection;
    public float flySpeed;
    public float firingRange;
    public float damage;
}