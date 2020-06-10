using Newtonsoft.Json;
using ImmVis.Messages;

[JsonObject]
public class ListAvailableDatasetsResult : Message
{
    public ListAvailableDatasetsResult() : base(MessageType) { }

    [JsonProperty("data")] private string[] data;

    public string[] Data {
        get { return data; }
    }

    public static string MessageType { get; } = "list_datasets_result";

    public static Message CreateMessage()
    {
        return new ListAvailableDatasetsResult();
    }
}
