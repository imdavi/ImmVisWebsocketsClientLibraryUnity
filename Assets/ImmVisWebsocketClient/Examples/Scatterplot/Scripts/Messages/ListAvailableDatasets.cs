using Newtonsoft.Json;
using ImmVis.Messages;

[JsonObject]
public class ListAvailableDatasets : Message
{
    private ListAvailableDatasets() : base(MessageType) { }

    public static string MessageType { get; } = "list_datasets";

    public static Message Create()
    {
        return new ListAvailableDatasets();
    }
}