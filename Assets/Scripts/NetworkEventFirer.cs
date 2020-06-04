using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public static class NetworkEventFirer
{
    public static byte EventCodeDealDamage = 1;
    public static byte EventCodeShowMessage = 2;

    // public static void RegisterCustomType()
    // {
        // PhotonPeer.RegisterType(typeof(DealDamageEventArgs), (byte)'M', DealDamageEventArgs.Serialize, DealDamageEventArgs.Deserialize);
    // }
    
    public static void DealDamage(int targetViewId, int damageOwnerViewId, int damage, Vector2 pushForce, int angerValue)
    {
        object[] content = {targetViewId, damageOwnerViewId,damage, pushForce, angerValue };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions {Receivers = ReceiverGroup.All};
        SendOptions sendOptions = new SendOptions {Reliability = true};
        PhotonNetwork.RaiseEvent(1, content, raiseEventOptions, sendOptions);
        Debug.LogWarning("fire damage event, target view id: "+targetViewId);
    }

    public static void ShowMessage(string speakerName, string message, string targetCharacterName)
    {
        object[] content = {speakerName, message, targetCharacterName};
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions {Receivers = ReceiverGroup.All};
        SendOptions sendOptions = new SendOptions {Reliability = true};
        PhotonNetwork.RaiseEvent(2, content, raiseEventOptions, sendOptions);
    }
    
    
    
}