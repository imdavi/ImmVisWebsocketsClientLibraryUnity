using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ImmVis.Messages
{
    [JsonObject]
    class ErrorMessage : Message
    {
        [JsonProperty] private string cause;

        [JsonIgnore]
        public string Cause
        {
            get { return cause; }
        }

        public ErrorMessage() : base(MessageType) {}

        public const string MessageType = "error";

        public static ErrorMessage Create()
        {
            return new ErrorMessage();
        }

        public override String ToString()
        {
            return $"Error {{ cause:\"{cause.ToString()}\" }}";
        }
    }
}
