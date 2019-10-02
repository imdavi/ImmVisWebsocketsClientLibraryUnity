

using System;
using Newtonsoft.Json;
using UnityEngine;

[JsonObject]
public class HeightmapMessage : BaseMessage<HeightmapMessage>
{

    [JsonProperty] private int image_width;

    [JsonProperty] private int image_height;

    [JsonProperty] private float[][] heightmap;

    [JsonIgnore]
    public float[][] Heightmap
    {
        get { return heightmap; }
    }

    [JsonIgnore]
    public int Width
    {
        get { return image_width; }
    }

    [JsonIgnore]
    public int Height
    {
        get { return image_height; }
    }

    public HeightmapMessage(string type) : base("heightmap") { }

}