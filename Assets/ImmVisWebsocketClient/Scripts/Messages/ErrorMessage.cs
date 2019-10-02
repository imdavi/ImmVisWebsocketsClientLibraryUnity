using System;
using Newtonsoft.Json;
using UnityEngine;

[JsonObject]
class ErrorMessage : BaseMessage<ErrorMessage>
{
    [JsonProperty] private string cause;

    [JsonIgnore]
    public string Cause
    {
        get { return cause; }
    }

    public ErrorMessage(string cause) : base("error")
    {
        this.cause = cause;
    }

    public static ErrorMessage Create(string cause)
    {
        return new ErrorMessage(cause);
    }
}