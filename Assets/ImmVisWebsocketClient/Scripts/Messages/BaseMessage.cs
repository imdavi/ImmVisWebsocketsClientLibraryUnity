

using System;
using Newtonsoft.Json;
using UnityEngine;

[JsonObject]
public abstract class BaseMessage<M>
{
    [JsonProperty] private string type;

    [JsonIgnore]
    public string Type
    {
        get { return type; }
    }

    public BaseMessage(string type)
    {
        this.type = type;
    }

    public static M FromJson(string jsonString)
    {
        return JsonConvert.DeserializeObject<M>(jsonString);
    }
}