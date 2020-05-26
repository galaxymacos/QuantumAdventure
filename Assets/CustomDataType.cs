using System;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;

public class CustomDataType : MonoBehaviourPunCallbacks
{
    [SerializeField] private MyCustomSerialization _customSerialization;
    [SerializeField] private bool _sendAsTyped = true;

    private void Start()
    {
        PhotonPeer.RegisterType(typeof(MyCustomSerialization), (byte) 'M', MyCustomSerialization.Serialize,
            MyCustomSerialization.Deserialize);
        
    }

    private void Update()
    {
        if (_customSerialization.MyNumber != -1)
        {
            SendCustomSerialization(_customSerialization, _sendAsTyped);
            _customSerialization.MyNumber = -1;
            _customSerialization.MyString = string.Empty;
        }
    }

    private void SendCustomSerialization(MyCustomSerialization data, bool typed)
    {
        if (!typed)
        {
            photonView.RPC("RPC_ReceiveMyCustomSerialization", RpcTarget.AllViaServer, MyCustomSerialization.Serialize(data));
        }
        else
        {
            photonView.RPC("RPC_TypedReceiveMyCustomSerialization", RpcTarget.AllViaServer, _customSerialization);
        }
    }

    [PunRPC]
    private void RPC_ReceiveMyCustomSerialization(byte[] datas)
    {
        MyCustomSerialization result = (MyCustomSerialization) MyCustomSerialization.Deserialize(datas);
        print("Received byte array: "+result.MyNumber+", "+result.MyString);
        
    }

    private void RPC_TypedReceiveMyCustomSerialization(MyCustomSerialization data)
    {
        print($"Received typed: {data.MyNumber}, {data.MyString}");
    }
}