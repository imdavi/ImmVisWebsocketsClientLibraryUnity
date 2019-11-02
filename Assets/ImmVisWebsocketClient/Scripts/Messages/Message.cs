

using System;
using Newtonsoft.Json;
using UnityEngine;

[JsonObject]
public class Message
{
    [JsonProperty] private string type;

    [JsonIgnore]
    public string Type
    {
        get { return type; }
    }

    public Message(string type)
    {
        this.type = type;
    }

    public override String ToString()
    {
        return $"Message {{ type:{type} }}";
    }
}