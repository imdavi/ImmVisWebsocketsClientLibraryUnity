using Newtonsoft.Json;
using ImmVis.Messages;

[JsonObject]
public class LoadDatasetResult : Message
{
    public LoadDatasetResult() : base(MessageType) { }

    [JsonProperty("data")] private Data data;

    public Data Data {
        get { return data; }
    }

    public static string MessageType { get; } = "load_dataset_result";

    public static Message CreateMessage()
    {
        return new LoadDatasetResult();
    }
}
