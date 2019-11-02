using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class SerializationUtils
{

    private static JsonSerializerSettings Settings { get; } = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };

    public static string SerializeObject(object obj)
    {
        return JsonConvert.SerializeObject(obj, Settings);
    }

    public static T DeserializeObject<T>(string jsonString)
    {
        return JsonConvert.DeserializeObject<T>(jsonString, Settings);
    }

    private delegate Message MessageDeserializeDelegate(string json);

    private static Dictionary<String, MessageDeserializeDelegate> DeserializationDictionary = new Dictionary<string, MessageDeserializeDelegate>
    {
        { ErrorMessage.MessageType, (json) => DeserializeObject<ErrorMessage>(json) }
    };

    public static void RegisterMessageType<T>(string messageType) where T : Message
    {
        DeserializationDictionary.Add(messageType, (json) => DeserializeObject<T>(json));
    }

    public static Message DeserializeMessage(string messageJsonString)
    {
        Message message = null;

        try
        {
            message = DeserializeObject<Message>(messageJsonString);

            if (DeserializationDictionary.ContainsKey(message.Type))
            {
                message = DeserializationDictionary[message.Type].Invoke(messageJsonString);
            }
            else
            {
                message = UnknownMessage.Create(messageJsonString);
            }
        }
        catch (Exception exception)
        {
            message = InvalidMessage.Create(messageJsonString, exception);
        }

        return message;
    }
}
