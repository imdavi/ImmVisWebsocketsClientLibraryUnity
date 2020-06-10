using Newtonsoft.Json;

[JsonObject]
public class Data
{
    [JsonProperty("columns")] private string[] columns;
    [JsonProperty("columns_types")] private string[] columnsTypes;
    [JsonProperty("values")] private float[][] values;
    [JsonProperty("axis_labels")] private string[][] axisLabels;

    [JsonIgnore]
    public string[] Columns
    {
        get { return columns; }
    }

    [JsonIgnore]
    public string[] ColumnsTypes
    {
        get { return columnsTypes; }
    }

    [JsonIgnore]
    public float[][] Values
    {
        get { return values; }
    }

    [JsonIgnore]
    public string[][] AxisLabels
    {
        get { return axisLabels; }
    }
}
