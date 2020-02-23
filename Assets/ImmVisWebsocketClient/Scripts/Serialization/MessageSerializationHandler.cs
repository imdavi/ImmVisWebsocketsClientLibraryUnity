using System.Collections.Generic;
using ImmVis.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ImmVis.Serialization
{
    class MessageSerializationHandler
    {
        private static MessageSerializationHandler instance;

        public static MessageSerializationHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MessageSerializationHandler();
                }

                return instance;
            }
        }

        private MessageSerializationHandler() { }

        private JsonSerializerSettings Settings { get; } = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            Converters = new List<JsonConverter> {
            new MessageConverter()
        }
        };

        public string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public T DeserializeObject<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString, Settings);
        }

        public Message DeserializeMessage(string jsonString)
        {
            return DeserializeObject<Message>(jsonString);
        }

    }
}
