using System;

public class InvalidMessage : Message
{
    public string ReceivedMessage { get; private set; }

    public Exception Error { get; private set; }

    private InvalidMessage(string receivedMessage, Exception error) : base(MessageType)
    {
        ReceivedMessage = receivedMessage;
        Error = error;
    }

    public static InvalidMessage Create(string receivedMessage, Exception error)
    {
        return new InvalidMessage(receivedMessage, error);
    }

    public static string MessageType { get; } = "invalid_message";
}