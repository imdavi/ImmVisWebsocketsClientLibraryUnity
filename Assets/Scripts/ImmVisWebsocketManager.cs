using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class ImmVisWebsocketManager : MonoBehaviour
{
    private WebSocket ws;

    public Plotter plotter;

    private static Queue<Action> ActionsQueue = new Queue<Action>();

    void Awake()
    {
        ws = new WebSocket("ws://localhost:8888/websocket");
        ws.OnMessage += (sender, e) =>
        {
            var payload = e.Data;
            Debug.Log($"Answer: {payload}");

            try
            {
                var message = Message.FromJson(payload);

                switch (message.Type)
                {
                    case "image":
                        var imageMessage = ImageMessage.FromJson(payload);
                        Debug.Log($"Received an image with size {imageMessage.Bytes.Length}");

                        ExecuteOnMainThread(() =>
                        {
                            plotter?.Plot(imageMessage);
                        });

                        break;
                    case "error":
                        var errorMessage = ErrorMessage.FromJson(payload);
                        Debug.Log($"Error on server: {errorMessage}");
                        break;
                    default:
                        Debug.Log("Unknown type of message.");
                        break;
                }
            }
            catch (System.Exception exception)
            {
                Debug.Log(exception.Message);
            }
        };

        ws.OnOpen += (sender, e) =>
        {
            string json = JsonUtility.ToJson(GetImage.Create());
            ws.Send(json);
        };

        ws.Connect();
    }

    void ExecuteOnMainThread(Action action)
    {
        ActionsQueue.Enqueue(action);
    }

    void Update()
    {
        while (ActionsQueue.Count > 0)
        {
            ActionsQueue.Dequeue().Invoke();
        }
    }

    void OnApplicationQuit()
    {
        ws.Close();
    }
}
