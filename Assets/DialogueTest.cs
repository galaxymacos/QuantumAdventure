using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueTest : MonoBehaviourPun
{
    #region Serialized Field

    [SerializeField] private TMP_InputField dialogueInputField;
    [SerializeField] private GameObject dialogueObject;

    [SerializeField] private GameObject dialogueDisplayBox;
    [SerializeField] private TMP_Text dialogueDisplayText;
    
    

    #endregion

    #region Property

    #endregion

    #region Private Field

    private PlayerManager target;

    #endregion

    #region MonoBehavior Callback

    private void Awake()
    {
        transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
        UserInput.onReturnButtonPressed += ToggleDialogueBox;
    }

    private void Update()
    {
        target.isDialogueBoxOpen = dialogueObject.activeSelf;
    }

    private void OnDestroy()
    {
        UserInput.onReturnButtonPressed -= ToggleDialogueBox;
    }

    #endregion

    #region Public Methods
    
    public void SetTarget(PlayerManager _target)
    {
        if (_target == null)
        {
            Debug.LogError("Missing PlayerManager target for CombatUI.SetTarget.", this);
            return;
        }
        target = _target;

        
        
    }

    #endregion

    #region Private Methods

    private void ToggleDialogueBox()
    {
        float damage = 10f;
        target.photonView.RPC("TakeDamage", RpcTarget.Others, damage);

        if (dialogueObject.activeSelf)
        {
            if (dialogueInputField.text != "")
            {
                target.photonView.RPC("DisplayMessage", RpcTarget.Others, dialogueInputField.text);
                dialogueInputField.text = "";
            }

            dialogueObject.SetActive(false);
        }
        else
        {
            dialogueObject.SetActive(true);
            dialogueInputField.Select();
            dialogueInputField.ActivateInputField();
        }
    }


    public void ShowMessage(string message)
    {
        if (!dialogueDisplayBox.activeSelf)
        {
            dialogueDisplayBox.SetActive(true);
            dialogueDisplayText.text = message;
            StopAllCoroutines();
            StartCoroutine(CloseDialogueDisplayBoxRoutine());
        }
        else
        {
            dialogueDisplayBox.SetActive(false);
        }

        print(message);
    }

    private IEnumerator CloseDialogueDisplayBoxRoutine()
    {
        yield return new WaitForSeconds(3);
        dialogueDisplayBox.SetActive(false);
    }

    
    
    #endregion
}