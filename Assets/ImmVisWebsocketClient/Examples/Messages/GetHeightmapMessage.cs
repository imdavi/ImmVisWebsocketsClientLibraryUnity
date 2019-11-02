

using System;
using Newtonsoft.Json;

[JsonObject]
class GetHeightmapMessage : Message
{
    private GetHeightmapMessage() : base(MessageType) { }

    public static string MessageType { get; } = "get_heightmap";
    public static Message Message { get; } = new GetHeightmapMessage();
}