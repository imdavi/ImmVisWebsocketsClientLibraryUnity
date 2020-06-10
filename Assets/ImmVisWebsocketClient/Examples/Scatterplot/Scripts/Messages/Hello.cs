using Newtonsoft.Json;
using ImmVis.Messages;

[JsonObject]
public class Hello : Message
{
    public Hello() : base(MessageType) { }

    public static string MessageType { get; } = "hello";

    
    public static Message CreateMessage()
    {
        return new Hello();
    }
}