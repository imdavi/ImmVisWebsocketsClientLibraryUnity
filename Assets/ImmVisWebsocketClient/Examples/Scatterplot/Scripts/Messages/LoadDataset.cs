using Newtonsoft.Json;
using ImmVis.Messages;

[JsonObject]
public class LoadDataset : Message
{
    [JsonProperty("file_path")] private string filePath;

    private LoadDataset(string filePath) : base(MessageType)
    {
        this.filePath = filePath;
    }

    public static string MessageType { get; } = "load_dataset";

    public static Message Create(string filePath) {
        return new LoadDataset(filePath);
    }
}