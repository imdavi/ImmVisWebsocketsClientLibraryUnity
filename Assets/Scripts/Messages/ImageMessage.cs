

using System;
using UnityEngine;

[Serializable]
public class ImageMessage : BaseMessage<ImageMessage>
{

    [SerializeField] private string image;

    private byte[] image_bytes = null;

    [SerializeField] private string image_mode;

    [SerializeField] private string image_format;

    [SerializeField] private int image_width;

    [SerializeField] private int image_height;

    public string Mode
    {
        get
        {
            return image_mode;
        }
    }

    public string Format
    {
        get
        {
            return image_format;
        }
    }

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

    public int Width
    {
        get { return image_width; }
    }

    public int Height
    {
        get { return image_height; }
    }

    public ImageMessage(string type) : base(type) { }

}