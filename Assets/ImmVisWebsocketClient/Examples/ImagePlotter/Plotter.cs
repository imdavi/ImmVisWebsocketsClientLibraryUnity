using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plotter : MonoBehaviour
{

    public ImmVisWebsocketManager WebsocketManager;

    void Awake()
    {
        RegisterMessageTypes();
        InitializeWebsocketClient();
    }

    private void InitializeWebsocketClient()
    {
        if (WebsocketManager != null && !WebsocketManager.IsConnected)
        {
            WebsocketManager.Connected += ClientConnected;
            WebsocketManager.MessageReceived += MessageReceived;
            WebsocketManager.InitializeClient();
        }
    }

    private void ClientConnected()
    {
        Debug.Log("Now you can send messages!");
        WebsocketManager.Send(GetImage.Message);
    }

    private void MessageReceived(Message message)
    {
        if (message is HeightmapMessage)
        {
            DisplayHeightmap(message as HeightmapMessage);
        }
        else if (message is ImageMessage)
        {
            DisplayImage(message as ImageMessage);
        }
    }

    private void DisplayHeightmap(HeightmapMessage heightmapMessage)
    {
        Debug.Log("Not implemented yet!");
    }
    private void DisplayImage(ImageMessage imageMessage)
    {
        Debug.Log("Render image!");
        var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        var texture = new Texture2D(2, 2);
        texture.LoadImage(imageMessage.Bytes);

        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        spriteRenderer.sprite = sprite;
    }

    private void RegisterMessageTypes()
    {
        SerializationUtils.RegisterMessageType<HeightmapMessage>(HeightmapMessage.MessageType);
        SerializationUtils.RegisterMessageType<ImageMessage>(ImageMessage.MessageType);
    }
}
