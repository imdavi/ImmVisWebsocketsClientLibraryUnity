

using System;
using Newtonsoft.Json;

[JsonObject]
class GetHeightmap : Message
{
    private GetHeightmap() : base(MessageType) { }

    public static string MessageType { get; } = "get_heightmap";
    public static Message Message { get; } = new GetHeightmap();
}