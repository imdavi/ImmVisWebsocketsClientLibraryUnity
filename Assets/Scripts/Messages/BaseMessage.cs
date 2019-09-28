

using System;
using UnityEngine;

[Serializable]
public abstract class BaseMessage<M>
{
    [SerializeField] private string type;

    public string Type
    {
        get { return type; }
    }

    public BaseMessage(string type)
    {
        this.type = type;
    }

    public static M FromJson(string jsonString)
    {
        return JsonUtility.FromJson<M>(jsonString);
    }
}