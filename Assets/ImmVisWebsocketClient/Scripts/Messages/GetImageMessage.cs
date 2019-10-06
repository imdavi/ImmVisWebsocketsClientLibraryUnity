

using System;
using Newtonsoft.Json;

[JsonObject]
class GetImage : Message
{
    private GetImage() : base(MessageType) { }

    public static string MessageType { get; } = "get_image";

    public static Message Message { get; private set; } = new GetImage();
}