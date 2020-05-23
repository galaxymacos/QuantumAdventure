using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SaveNickName : MonoBehaviour
{
    public void OnChanged_SaveNickName(string nickName)
    {
        PhotonNetwork.NickName = nickName;
    }
}
