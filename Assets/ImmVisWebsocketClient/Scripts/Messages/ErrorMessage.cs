using System;
using Newtonsoft.Json;
using UnityEngine;

[JsonObject]
class ErrorMessage : Message
{
    [JsonProperty] private string cause;

    [JsonIgnore]
    public string Cause
    {
        get { return cause; }
    }

    private ErrorMessage(string cause) : base(MessageType)
    {
        this.cause = cause;
    }

    public static string MessageType { get; } = "error";

    public static ErrorMessage Create(string cause)
    {
        return new ErrorMessage(cause);
    }

    public override String ToString()
    {
        return $"Error {{ cause:\"{cause.ToString()}\" }}";
    }
}