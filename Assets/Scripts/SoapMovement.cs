using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using Event_Args;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CharacterController))]
public class SoapMovement : MonoBehaviourPun, IPunObservable
{
    
    #region Serialized Field

    [SerializeField] private SoapData soapData;

    [SerializeField] private float camMaxHeight = 3f;
    [SerializeField] private float camMinHeight = 0.5f;
    private Transform _camTransfrom;
    [SerializeField] private Vector3 gravity = new Vector3(0.0f, -9.8f, 0.0f);
    
    [SerializeField] public float mouseSentivity = 20;
    [SerializeField] public float runSpeed = 10f;
    [SerializeField] public float walkSpeed = 5f;
    [SerializeField] private Transform camLookTarget;
    
    [SerializeField] public Animator anim;
    [SerializeField] public LayerMask whatIsGround;
    [SerializeField] public Transform groundCheck;

    
    [FormerlySerializedAs("TriggerAnimationList")] [SerializeField] public string[] triggerAnimationList;

    #endregion

    #region Property

    public float DiveRollSpeed => soapData.diveRollSpeed;

    public float moveSpeed;
    public bool isGrounded => Physics.OverlapSphere(groundCheck.position, 0.05f, whatIsGround).Length>0;

    #endregion

    #region Private Field

    private CharacterController _characterController;
    private int _triggerAnimationRaisingFlag = -1;

    #endregion

    #region Event

    public event EventHandler<CharacterEventArgs> onPlayerGrounded;
    public event EventHandler<CharacterEventArgs> onPlayerJumped;
    public Action<SmbSoap> onAnimationEnter;
    public Action<SmbSoap> onAnimationUpdate;
    public Action<SmbSoap> onAnimationExit;

    #endregion

    #region MonoBehavior Callback

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _camTransfrom = Camera.main.transform;
    }

    

    private void Update()
    {

        if (!isGrounded)
        {
            _characterController.Move(gravity*Time.deltaTime);
        }
    }

    #endregion

    #region Public Methods

    public void Move(float horizontalInput, float verticalInput)
    {
        if (photonView.IsMine || !PhotonNetwork.IsConnected)
        {
            anim.SetFloat("Horizontal Input", horizontalInput);
            anim.SetFloat("Vertical Input", verticalInput);
            Vector3 forwardVector = transform.position - _camTransfrom.position;
            forwardVector.y = 0;
            forwardVector.Normalize();
            Vector3 rightVector = Vector3.Cross(forwardVector, Vector3.up).normalized;
            Vector3 movementVector = forwardVector * verticalInput + -rightVector * horizontalInput;
            movementVector.Normalize();
            _characterController.Move(movementVector * moveSpeed* Time.deltaTime);
            
        }
    }

    /// <summary>
    /// Move based on current rotation
    /// </summary>
    public void MoveForward(float horizontalInput, float verticalInput)
    {
        if (photonView.IsMine || !PhotonNetwork.IsConnected)
        {
            Vector3 forwardVector = transform.position - _camTransfrom.position;
            forwardVector.y = 0;
            forwardVector.Normalize();
            Vector3 rightVector = Vector3.Cross(forwardVector, Vector3.up).normalized;
            Vector3 movementVector = forwardVector * verticalInput + -rightVector * horizontalInput;
            movementVector.Normalize();
            _characterController.Move(movementVector * moveSpeed* Time.deltaTime);
            
        }
    }

    public Vector3 CalculateMoveDirection(float horizontalInput, float verticalInput)
    {
        Vector3 forwardVector = transform.position - _camTransfrom.position;
        forwardVector.y = 0;
        forwardVector.Normalize();
        Vector3 rightVector = Vector3.Cross(forwardVector, Vector3.up).normalized;
        Vector3 movementVector = forwardVector * verticalInput + -rightVector * horizontalInput;
        movementVector.Normalize();
        return movementVector;
    }

    public void MoveAlong(Vector3 directionVector)
    {
        print(moveSpeed);
        _characterController.Move(directionVector * moveSpeed* Time.deltaTime);

    }

    public void RotateCharacter()
    {
        if (photonView.IsMine || !PhotonNetwork.IsConnected)
        {
            
            Vector3 forwardVector = transform.position - _camTransfrom.position;
            forwardVector.y = 0;
            forwardVector.Normalize();

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(forwardVector), 0.1f);
            
            // transform.Rotate(Vector3.up, UserInput.cameraHorizontalMouseValue*Time.deltaTime*mouseSentivity);
            // float camTransfromY = camTransfrom.localPosition.y;

            
            // camTransfrom.Translate(Vector3.up * -UserInput.cameraVerticalMouseValue*Time.deltaTime*2);
            // camTransfromY = Mathf.Clamp(camTransfromY, camMinHeight, camMaxHeight);
            // camTransfrom.localPosition.Set(camTransfrom.localPosition.x, camTransfromY, camTransfrom.localPosition.z);
        }
    }

    public float InputToRotationAngle(float horizontalInput, float verticalInput)
    {
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg +
                                Camera.main.transform.eulerAngles.y;
            return targetAngle;
    }

    public void RotateCharacterImmediately(float targetAngle)
    {
        if (photonView.IsMine || !PhotonNetwork.IsConnected)
        {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, targetAngle, 0f), 0.2f);
        }
    }

    public void SetTriggerAnimation(string triggerName)
    {
        anim.SetTrigger(triggerName);
        for (int i = 0; i < triggerAnimationList.Length; i++)
        {
            if (triggerAnimationList[i] == triggerName)
            {
                _triggerAnimationRaisingFlag = i;
                break;
            }
        }
    }

    public void SetAnimationBool(string paramName, bool newValue)
    {
        anim.SetBool(paramName, newValue);
    }

    public void SetTriggerAnimationRaisingFlag(int index)
    {
        _triggerAnimationRaisingFlag = index;
    }

    #endregion

    #region Private Methods
    
    

    #endregion


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_triggerAnimationRaisingFlag);

            if (_triggerAnimationRaisingFlag >= 0)
            {
                _triggerAnimationRaisingFlag = -1;
            }
        }
        else
        {
            _triggerAnimationRaisingFlag = (int) stream.ReceiveNext();
            if (_triggerAnimationRaisingFlag >= 0)
            {
                SetTriggerAnimation(triggerAnimationList[_triggerAnimationRaisingFlag]);
                _triggerAnimationRaisingFlag = -1;
            }
        }
    }
}