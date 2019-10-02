

using System;
using Newtonsoft.Json;
using UnityEngine;

[JsonObject]
public class ImageMessage : BaseMessage<ImageMessage>
{

    [JsonProperty] private string image;

    private byte[] image_bytes = null;

    [JsonProperty] private string image_mode;

    [JsonProperty] private string image_format;

    [JsonProperty] private int image_width;

    [JsonProperty] private int image_height;

    [JsonIgnore]
    public string Mode
    {
        get
        {
            return image_mode;
        }
    }

    [JsonIgnore]
    public string Format
    {
        get
        {
            return image_format;
        }
    }

    [JsonIgnore]
    public byte[] Bytes
    {
        get
        {
            if (image_bytes == null)
            {
                image_bytes = System.Convert.FromBase64String(image);
            }

            return image_bytes;
        }
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

    public ImageMessage(string type) : base("image") { }

}