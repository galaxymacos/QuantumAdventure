using System;
using System.Collections;
using System.Collections.Generic;
using Event_Args;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviourPun, IPunObservable
{
    
    #region Serialized Field

    [SerializeField] private float camMaxHeight = 3f;
    [SerializeField] private float camMinHeight = 0.5f;
    [SerializeField] private Transform camTransfrom;
    [SerializeField] private Vector3 gravity = new Vector3(0.0f, -9.8f, 0.0f);
    
    
    [SerializeField] public float mouseSentivity = 20;
    [SerializeField] public float runSpeed = 10f;
    [SerializeField] public float walkSpeed = 5f;
    [SerializeField] private Transform camLookTarget;
    
    [SerializeField] public Animator anim;
    [SerializeField] public LayerMask whatIsGround;
    [SerializeField] public Transform groundCheck;

    
    [SerializeField] public string[] TriggerAnimationList;

    #endregion

    #region Property

    public float moveSpeed;
    public bool isGrounded => Physics.OverlapSphere(groundCheck.position, 0.05f, whatIsGround).Length>0;

    #endregion

    #region Private Field

    private CharacterController characterController;
    private int triggerAnimationRaisingFlag = -1;

    #endregion

    #region Event

    public event EventHandler<CharacterEventArgs> onPlayerGrounded;
    public event EventHandler<CharacterEventArgs> onPlayerJumped;

    #endregion

    #region MonoBehavior Callback

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    

    private void Update()
    {
        RotateCharacter();

        if (!isGrounded)
        {
            characterController.Move(gravity*Time.deltaTime);
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
            anim.SetFloat("Speed", 1);
            Vector3 forwardVector = transform.position - camTransfrom.position;
            forwardVector.y = 0;
            forwardVector.Normalize();
            Vector3 rightVector = Vector3.Cross(forwardVector, Vector3.up).normalized;
            Vector3 movementVector = forwardVector * verticalInput + -rightVector * horizontalInput;
            movementVector.Normalize();
            characterController.Move(movementVector * moveSpeed* Time.deltaTime);
            
        }
    }

    public void RotateCharacter()
    {
        if (photonView.IsMine || !PhotonNetwork.IsConnected)
        {
            transform.Rotate(Vector3.up, UserInput.cameraHorizontalMouseValue*Time.deltaTime*mouseSentivity);
            float camTransfromY = camTransfrom.localPosition.y;

            if (camTransfromY +  -UserInput.cameraVerticalMouseValue * Time.deltaTime * 2 > camMaxHeight || camTransfromY +  -UserInput.cameraVerticalMouseValue * Time.deltaTime * 2 < camMinHeight)
            {
                return;
            }
            camTransfrom.Translate(Vector3.up * -UserInput.cameraVerticalMouseValue*Time.deltaTime*2);
            camTransfromY = Mathf.Clamp(camTransfromY, camMinHeight, camMaxHeight);
            camTransfrom.localPosition.Set(camTransfrom.localPosition.x, camTransfromY, camTransfrom.localPosition.z);
        }
    }

    public void SetTriggerAnimation(string triggerName)
    {
        anim.SetTrigger(triggerName);
        for (int i = 0; i < TriggerAnimationList.Length; i++)
        {
            if (TriggerAnimationList[i] == triggerName)
            {
                triggerAnimationRaisingFlag = i;
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
        triggerAnimationRaisingFlag = index;
    }

    #endregion

    #region Private Methods
    
    

    #endregion


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(triggerAnimationRaisingFlag);

            if (triggerAnimationRaisingFlag >= 0)
            {
                triggerAnimationRaisingFlag = -1;
            }
        }
        else
        {
            triggerAnimationRaisingFlag = (int) stream.ReceiveNext();
            if (triggerAnimationRaisingFlag >= 0)
            {
                SetTriggerAnimation(TriggerAnimationList[triggerAnimationRaisingFlag]);
                triggerAnimationRaisingFlag = -1;
            }
        }
    }
}