using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using WebSocketSharp;

public class ImmVisWebsocketManager : UnityDispatcherBehaviour
{
    public string ServerAddress;
    public int ServerPort;
    public string Path;
    private WebSocket webSocket;

    public bool IsConnected
    {
        get
        {
            return webSocket != null && webSocket.ReadyState == WebSocketState.Open;
        }
    }
    public event ClientConnectedAction Connected;
    public event ClientDisconnectedAction Disconnected;
    public event ClientErrorAction Error;
    public event MessageReceivedAction MessageReceived;
    public event RawMessageReceivedAction RawMessageReceived;

    void OnApplicationQuit()
    {
        ReleaseClient();
    }

    public void InitializeClient()
    {
        if (!IsConnected)
        {
            var url = ClientUrlBuilder.BuildServerUrl(ServerAddress, ServerPort, Path);
            webSocket = new WebSocket(url);
            webSocket.OnMessage += HandleWebsocketMessage;
            webSocket.OnOpen += HandleWebsocketOpened;
            webSocket.OnClose += HandleWebsocketClosed;
            webSocket.OnError += HandleWebsocketError;
            webSocket.Connect();
        }
    }

    public void ReleaseClient()
    {
        if (IsConnected || webSocket.ReadyState == WebSocketState.Connecting)
        {
            Disconnected?.Invoke();
            webSocket.OnMessage -= HandleWebsocketMessage;
            webSocket.OnOpen -= HandleWebsocketOpened;
            webSocket.OnClose -= HandleWebsocketClosed;
            webSocket.OnError -= HandleWebsocketError;
            webSocket.Close();
        }
    }

    private void HandleWebsocketError(object sender, ErrorEventArgs e)
    {
        ExecuteOnMainThread(() => { Error?.Invoke(e.Exception); });
    }

    private void HandleWebsocketClosed(object sender, CloseEventArgs e)
    {
        ExecuteOnMainThread(() => { ReleaseClient(); });
    }

    private void HandleWebsocketOpened(object sender, EventArgs e)
    {
        ExecuteOnMainThread(() => { Connected?.Invoke(); });
    }

    private void HandleWebsocketMessage(object sender, MessageEventArgs eventArgs)
    {
        var jsonPayload = eventArgs.Data;

        ExecuteOnMainThread(() => { RawMessageReceived?.Invoke(jsonPayload); });

        try
        {
            var message = SerializationUtils.DeserializeMessage(jsonPayload);

            if (message is ErrorMessage)
            {
                var cause = (message as ErrorMessage).Cause;
                ExecuteOnMainThread(() => { Error?.Invoke(new ErrorMessageException(cause)); });
            }
            else
            {
                ExecuteOnMainThread(() => { MessageReceived?.Invoke(message); });
            }
        }
        catch (Exception exception)
        {
            ExecuteOnMainThread(() => { Error?.Invoke(exception); });
        }
    }

    public void Send(Message message)
    {
        string json = SerializationUtils.SerializeObject(message);
        Send(json);
    }

    public void Send(string message)
    {
        if (IsConnected)
        {
            webSocket.Send(message);
        }
    }

    public class ErrorMessageException : Exception
    {
        public string Cause { get; private set; }
        public ErrorMessageException(string cause) : base()
        {
            Cause = cause;
        }
    }
}
