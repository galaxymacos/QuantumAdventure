using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public static class NetworkEventFirer
{
    public static byte EventCode_DealDamage = 1;
    public static byte EventCode_ShowMessage = 2;
    
    public static void DealDamage(float damage, string targetCharacterName)
    {
        object[] content = {damage, targetCharacterName};
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions {Receivers = ReceiverGroup.All};
        SendOptions sendOptions = new SendOptions {Reliability = true};
        PhotonNetwork.RaiseEvent(1, content, raiseEventOptions, sendOptions);
        Debug.LogWarning("fire damage event, target: "+targetCharacterName);
    }

    public static void ShowMessage(string speakerName, string message, string targetCharacterName)
    {
        object[] content = {speakerName, message, targetCharacterName};
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions {Receivers = ReceiverGroup.All};
        SendOptions sendOptions = new SendOptions {Reliability = true};
        PhotonNetwork.RaiseEvent(2, content, raiseEventOptions, sendOptions);
    }
    
    
    
}