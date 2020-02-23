using ImmVis.Messages;
public class UnknownMessage : Message
{
    public string ReceivedMessage { get; private set; }

    private UnknownMessage(string receivedMessage) : base(MessageType)
    {
        ReceivedMessage = receivedMessage;
    }

    public static UnknownMessage Create(string receivedMessage)
    {
        return new UnknownMessage(receivedMessage);
    }

    public const string MessageType = "unknown_message";
}
