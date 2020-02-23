using System;
using System.Collections.Generic;
using ImmVis.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ImmVis.Serialization
{
    class MessageConverter : JsonConverter
    {
        public delegate Message MessageCreationDelegate();

        private static Dictionary<String, MessageCreationDelegate> CreationDictionary = new Dictionary<string, MessageCreationDelegate>   {
            { ErrorMessage.MessageType, () => ErrorMessage.Create() }
        };

        public static void RegisterMessage(string messageType, MessageCreationDelegate factoryDelegate)
        {
            CreationDictionary.Add(messageType, factoryDelegate);
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(Message).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);

            var type = (string)jObject.Property(Message.TypeField);

            var target = CreationDictionary[type].Invoke();

            serializer.Populate(jObject.CreateReader(), target);

            return target;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}