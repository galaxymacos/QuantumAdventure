using System;
using Event_Args;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Serialization;

public class MariaMovement : MonoBehaviourPun, IPunObservable
{
    
    #region Serialized Field

    [SerializeField] private GameObject freeLookCamera;
    [SerializeField] private Vector3 gravity = new Vector3(0.0f, -9.8f, 0.0f);
    
    
    [SerializeField] public float mouseSentivity = 20;
    [SerializeField] public float slideAttackMoveSpeed = 6f;
    [SerializeField] public float runSpeed = 10f;
    [SerializeField] public float walkSpeed = 5f;
    
    [SerializeField] public Animator anim;
    [SerializeField] public LayerMask whatIsGround;
    [SerializeField] public Transform groundCheck;

    
    [FormerlySerializedAs("TriggerAnimationList")] [SerializeField] public string[] triggerAnimationList;

    [SerializeField] private float turnSmoothTime = 0.1f;
    
    #endregion

    #region Property

    public float moveSpeed;
    public bool isGrounded => Physics.OverlapSphere(groundCheck.position, 0.05f, whatIsGround).Length>0;

    #endregion

    #region Private Field

    private CharacterController _characterController;
    private int _triggerAnimationRaisingFlag = -1;

    private float _turnSmoothVelocity;

    #endregion

    #region Event

    public event EventHandler<CharacterEventArgs> onPlayerGrounded;
    public event EventHandler<CharacterEventArgs> onPlayerJumped;

    #endregion

    #region MonoBehavior Callback

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        if (photonView.IsMine)
        {
            freeLookCamera.SetActive(true);
        }
    }

    

    private void Update()
    {
        // RotateCharacter();

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
            // anim.SetFloat("Horizontal Input", horizontalInput);
            // anim.SetFloat("Vertical Input", verticalInput);

            Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
                // float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                // transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                _characterController.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
                
                anim.SetFloat("Speed", 1, 0.3f, Time.deltaTime);
            }
            else
            {
                anim.SetFloat("Speed", 0,0.3f, Time.deltaTime);
            }
            // Vector3 forwardVector = transform.position - camTransfrom.position;
            // forwardVector.y = 0;
            // forwardVector.Normalize();
            // Vector3 rightVector = Vector3.Cross(forwardVector, Vector3.up).normalized;
            // Vector3 movementVector = forwardVector * verticalInput + -rightVector * horizontalInput;
            // movementVector.Normalize();
            // print("Move speed: "+moveSpeed);
            // characterController.Move(movementVector * moveSpeed* Time.deltaTime);

        }
    }
    
    public Vector3 CalculateMoveDirection(float horizontalInput, float verticalInput)
    {
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg +
                                Camera.main.transform.eulerAngles.y;

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            return moveDir.normalized;
    }

    public void MoveAlong(Vector3 directionVector)
    {
        _characterController.Move(directionVector * moveSpeed* Time.deltaTime);

    }

    public void RotateCharacter(float horizontalInput, float verticalInput)
    {
        // if (photonView.IsMine || !PhotonNetwork.IsConnected)
        // {
        //     transform.Rotate(Vector3.up, UserInput.cameraHorizontalMouseValue*Time.deltaTime*mouseSentivity);
        //     float camTransfromY = camTransfrom.localPosition.y;
        //
        //     if (camTransfromY +  -UserInput.cameraVerticalMouseValue * Time.deltaTime * 2 > camMaxHeight || camTransfromY +  -UserInput.cameraVerticalMouseValue * Time.deltaTime * 2 < camMinHeight)
        //     {
        //         return;
        //     }
        //     camTransfrom.Translate(Vector3.up * -UserInput.cameraVerticalMouseValue*Time.deltaTime*2);
        //     camTransfromY = Mathf.Clamp(camTransfromY, camMinHeight, camMaxHeight);
        //     camTransfrom.localPosition.Set(camTransfrom.localPosition.x, camTransfromY, camTransfrom.localPosition.z);
        // }

        if (photonView.IsMine || !PhotonNetwork.IsConnected)
        {
            // anim.SetFloat("Horizontal Input", horizontalInput);
            // anim.SetFloat("Vertical Input", verticalInput);

            Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg +
                                    Camera.main.transform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity,
                    turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                // Vector3 forwardVector = transform.position - camTransfrom.position;
                // forwardVector.y = 0;
                // forwardVector.Normalize();
                // Vector3 rightVector = Vector3.Cross(forwardVector, Vector3.up).normalized;
                // Vector3 movementVector = forwardVector * verticalInput + -rightVector * horizontalInput;
                // movementVector.Normalize();
                // print("Move speed: "+moveSpeed);
                // characterController.Move(movementVector * moveSpeed* Time.deltaTime);

            }

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