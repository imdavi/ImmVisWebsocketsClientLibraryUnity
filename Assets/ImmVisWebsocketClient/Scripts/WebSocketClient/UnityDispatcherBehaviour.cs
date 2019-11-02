
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnityDispatcherBehaviour : MonoBehaviour
{
    private static Queue<Action> ActionsQueue = new Queue<Action>();

    protected void ExecuteOnMainThread(Action action)
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
}