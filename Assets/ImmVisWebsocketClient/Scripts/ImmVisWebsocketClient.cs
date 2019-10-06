

using System;
using System.Text;
using Newtonsoft.Json;
using WebSocketSharp;

public class ImmVisWebsocketClient
{
    public enum State { Connected, Error, Disconnected }
    private const string DEFAULT_SERVER_ADDRESS = "localhost";
    private const int DEFAULT_SERVER_PORT = 8888;
    private WebSocket webSocket;
    public State ClientState { get; private set; } = State.Disconnected;
    public delegate void ClientConnectedAction();
    public event ClientConnectedAction Connected;
    public delegate void ClientDisconnectedAction();
    public event ClientDisconnectedAction Disconnected;
    public delegate void ClientErrorAction(Exception exception);
    public event ClientErrorAction Error;
    public delegate void MessageReceivedAction(Message message);
    public event MessageReceivedAction MessageReceived;

    public delegate void RawMessageReceivedAction(String message);
    public event RawMessageReceivedAction RawMessageReceived;

    public ImmVisWebsocketClient(string serverUrl)
    {
        webSocket = new WebSocket(serverUrl);
    }

    public ImmVisWebsocketClient(string address = DEFAULT_SERVER_ADDRESS, int port = DEFAULT_SERVER_PORT, string path = "")
     : this(BuildServerUrl(address, port, path)) { }

    private static string BuildServerUrl(string address, int port, string path)
    {
        StringBuilder stringBuilder = new StringBuilder("ws://");

        stringBuilder.Append(address.IsNullOrEmpty() ? DEFAULT_SERVER_ADDRESS : address);

        stringBuilder.Append($":{port}");

        if (!path.IsNullOrEmpty())
        {
            stringBuilder.Append($"/{path}");
        }

        return stringBuilder.ToString();
    }

    public void Initialize()
    {
        ClientState = State.Disconnected;
        webSocket.OnMessage += HandleWebsocketMessage;
        webSocket.OnOpen += HandleWebsocketOpened;
        webSocket.OnClose += HandleWebsocketClosed;
        webSocket.OnError += HandleWebsocketError;
        webSocket.Connect();
    }

    private void HandleWebsocketError(object sender, ErrorEventArgs e)
    {
        ClientState = State.Error;
        Error?.Invoke(e.Exception);
    }

    private void HandleWebsocketClosed(object sender, CloseEventArgs e) { Release(); }

    private void HandleWebsocketOpened(object sender, EventArgs e)
    {
        ClientState = State.Connected;
        Connected?.Invoke();
    }

    private void HandleWebsocketMessage(object sender, MessageEventArgs eventArgs)
    {
        var jsonPayload = eventArgs.Data;

        RawMessageReceived?.Invoke(jsonPayload);

        try
        {
            var message = SerializationUtils.DeserializeMessage(jsonPayload);

            if (message is ErrorMessage)
            {
                var cause = (message as ErrorMessage).Cause;
                Error?.Invoke(new ErrorMessageException(cause));
            }
            else
            {
                MessageReceived?.Invoke(message);
            }
        }
        catch (Exception exception)
        {
            Error?.Invoke(exception);
        }
    }

    public void Release()
    {
        ClientState = State.Disconnected;
        Disconnected?.Invoke();
        webSocket.OnMessage -= HandleWebsocketMessage;
        webSocket.OnOpen -= HandleWebsocketOpened;
        webSocket.OnClose -= HandleWebsocketClosed;
        webSocket.OnError -= HandleWebsocketError;
        webSocket.Close();
    }

    public void SendMessage(Message message)
    {
        string json = SerializationUtils.SerializeObject(message);
        SendMessage(json);
    }

    public void SendMessage(string message)
    {
        webSocket.Send(message);
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
