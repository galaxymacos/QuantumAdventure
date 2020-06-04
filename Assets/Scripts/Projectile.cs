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

    [SerializeField] private BulletData bulletData;

    #endregion

    #region Property

    public bool isMine;

    #endregion

    #region Private Field

    private float _flySpeed;
    private Vector3 _flyDirection;
    private float _firingRange;
    private bool _hasSetup;
    private int _damage;

    private Rigidbody _rb;
    private GameObject _owner;

    private Vector3 _originalLocation;

    #endregion

    #region MonoBehavior Callback

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if ((transform.position - _originalLocation).magnitude > _firingRange)
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
        _flySpeed = args.FlySpeed;
        _flyDirection = args.FlyDirection;
        _damage = args.Damage;
        _firingRange = args.FiringRange;
        _owner = args.Owner;
        isMine = true;

        _hasSetup = true;

        _rb.velocity = _flyDirection * _flySpeed;

        _originalLocation = transform.position;
    }

    #endregion

    #region Private Methods

    private void OnTriggerEnter(Collider other)
    {
        if (!isMine) return;
        if (other.transform.root.gameObject == _owner || other.CompareTag("Bullet")) return;
        if (_owner.GetComponent<PlayerManager>().photonView.IsMine)
        {
            var takeDamagePart = other.GetComponent<ITakeDamage>();
            if (takeDamagePart != null)
            {
                NetworkEventFirer.DealDamage( other.gameObject.GetComponent<PhotonView>().ViewID, _owner.GetComponent<PhotonView>().ViewID, _damage,Vector2.zero, bulletData.angerValue);
            }
        }
        PhotonNetwork.Destroy(gameObject);
    }

    #endregion

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_rb.position);
            stream.SendNext(_rb.rotation);
            stream.SendNext(_rb.velocity);
        }
        else
        {
            _rb.position = (Vector3) stream.ReceiveNext();
            _rb.rotation = (Quaternion) stream.ReceiveNext();
            _rb.velocity =(Vector3) stream.ReceiveNext();
            
            float lag = Mathf.Abs((float) (PhotonNetwork.Time - info.SentServerTime));
            _rb.position += _rb.velocity * lag;
        }
    }
}

public class ProjectileArgs
{
    public GameObject Owner;
    public Vector3 FlyDirection;
    public float FlySpeed;
    public float FiringRange;
    public int Damage;
}