


using System;
using UnityEngine;

public abstract class MessageListener : MonoBehaviour
{
    public abstract void ClientInitialized(ImmVisWebsocketClient client);
    public abstract void MessageReceived(Message message);
    public abstract void ClientEnded();
}