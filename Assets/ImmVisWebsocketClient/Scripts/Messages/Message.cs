using System;
using Newtonsoft.Json;
using UnityEngine;

namespace ImmVis.Messages
{
    [JsonObject]
    public class Message
    {
        [JsonIgnore]
        public const string TypeField = "object_type";
        
        [JsonProperty(TypeField)] private string type;

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
}
