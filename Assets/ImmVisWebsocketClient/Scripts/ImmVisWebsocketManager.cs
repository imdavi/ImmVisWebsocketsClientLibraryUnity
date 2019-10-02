using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using WebSocketSharp;

public class ImmVisWebsocketManager : MonoBehaviour
{
    private WebSocket ws;

    public PlotterBehaviour plotter;

    private static Queue<Action> ActionsQueue = new Queue<Action>();

    void Awake()
    {
        ws = new WebSocket("ws://localhost:8888/websocket");
        ws.OnMessage += (sender, e) =>
        {
            var payload = e.Data;

            try
            {
                var message = Message.FromJson(payload);

                switch (message.Type)
                {
                    case "image":
                        var imageMessage = ImageMessage.FromJson(payload);

                        ExecuteOnMainThread(() =>
                        {
                            plotter?.PlotImage(imageMessage);
                        });

                        break;

                    case "heightmap":
                        var heightmapMessage = HeightmapMessage.FromJson(payload);

                        ExecuteOnMainThread(() =>
                        {
                            plotter?.PlotHeightmap(heightmapMessage);
                        });

                        break;

                    case "error":
                        var errorMessage = ErrorMessage.FromJson(payload);
                        Debug.LogError($"Error on server: {errorMessage.Cause}");
                        break;
                    default:
                        Debug.LogError("Unknown type of message.");
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
            string json = JsonConvert.SerializeObject(GetHeightmap.Create());
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
