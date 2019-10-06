using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using WebSocketSharp;

public class ImmVisWebsocketManager : MonoBehaviour
{
    private ImmVisWebsocketClient websocketClient;

    public MessageListener plotter;

    private static Queue<Action> ActionsQueue = new Queue<Action>();

    void Awake()
    {
        websocketClient = new ImmVisWebsocketClient(path: "websocket");

        websocketClient.Connected += ClientConnected;
        websocketClient.Disconnected += ClientDisconnected;
        websocketClient.MessageReceived += MessageReceived;
        websocketClient.RawMessageReceived += RawMessageReceived;
        websocketClient.Error += ClientError;

        websocketClient.Initialize();
    }

    private void RawMessageReceived(string message)
    {
        Debug.Log($"Raw message:\n{message}");
    }

    private void ClientError(Exception exception)
    {
        Debug.LogError(exception);
    }

    private void MessageReceived(Message message)
    {
        Debug.Log("Received a message!");

        if(message != null) 
        {
            plotter?.MessageReceived(message);
        }

                // ExecuteOnMainThread(() =>
        // {
        //     plotter?.PlotHeightmap(heightmapMessage);
        // });

    }

    private void ClientDisconnected()
    {
        Debug.Log("Disconnected from server.");
    }

    private void ClientConnected()
    {
        Debug.Log("Connected on server!");
        websocketClient.SendMessage(GetImage.Message);
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
        websocketClient.Connected -= ClientConnected;
        websocketClient.Disconnected -= ClientDisconnected;
        websocketClient.MessageReceived -= MessageReceived;
        websocketClient.Error -= ClientError;
        websocketClient.Release();
    }
}
