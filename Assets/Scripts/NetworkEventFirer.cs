using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkEventFirer: MonoBehaviour
{
    public static void DealDamage(float damage, string targetCharacterName)
    {
        object[] content = {damage, targetCharacterName};
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions {Receivers = ReceiverGroup.All};
        SendOptions sendOptions = new SendOptions {Reliability = true};
        PhotonNetwork.RaiseEvent(1, content, raiseEventOptions, sendOptions);
    }
    
    
    
}